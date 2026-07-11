Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text
Imports System.Xml.Linq
Imports System.Linq

Public Class RadioCallListManager
    Private savedRadioID As String
    Private savedCallsign As String
    Private savedIP As String
    Private savedMDTIP As String
    Private savedRadioUser As String
    Private savedGroupID As String
    Private isEditMode As Boolean = False
    Private lastSearchText As String = ""
    Private lastFoundRowIndex As Integer = -1

    Private Const DefaultWacnID As String = "DEE00"
    Private Const DefaultSystemID As String = "13B"

    Private Const HarrisListName As String = "CTRS"
    Private Const HarrisType As String = "Individual Call Set"
    Private Const HarrisDisplay As String = "Alpha & Numeric"

    Private Class RadioExportRecord

        Public Property RadioID As String
        Public Property AliasText As String

    End Class

    Private Class RadioXmlMetadata

        Public Property SystemName As String
        Public Property WacnID As String
        Public Property SystemID As String
        Public Property ReferenceKey As String

    End Class

    Private Sub tblValidationSummary_Paint(sender As Object, e As PaintEventArgs) Handles tblValidationSummary.Paint
        lblTotalValue.ForeColor = Color.RoyalBlue
        lblValidValue.ForeColor = Color.SeaGreen
        lblErrorsValue.ForeColor = Color.Firebrick
        lblDuplicatesValue.ForeColor = Color.Firebrick
        lblMissingValue.ForeColor = Color.DarkOrange
        lblLengthValue.ForeColor = Color.Firebrick
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tscbTargetFormat.Items.Clear()

        tscbTargetFormat.Items.AddRange(
        New Object() {
            "EF Johnson",
            "Motorola APX",
            "Motorola XTS",
            "Harris RPM"
        }
    )

        tscbTargetFormat.DropDownStyle =
        ComboBoxStyle.DropDownList

        tscbTargetFormat.SelectedItem =
        "Motorola APX"

        tsslWacnID.Text = DefaultWacnID
        tsslSysID.Text = DefaultSystemID
        tsslSysName.Text = HarrisListName

        FindToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.F
        FindToolStripMenuItem.ShowShortcutKeys = True

        EditSelectedRecordToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.E
        EditSelectedRecordToolStripMenuItem.ShowShortcutKeys = True

        ResetToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.R
        ResetToolStripMenuItem.ShowShortcutKeys = True

        ImportRadioIDFileToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.I
        ImportRadioIDFileToolStripMenuItem.ShowShortcutKeys = True

        DeleteSelectedRecordToolStripMenuItem.ShortcutKeys = Keys.Delete
        DeleteSelectedRecordToolStripMenuItem.ShowShortcutKeys = True

        AddNewRecordToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        AddNewRecordToolStripMenuItem.ShowShortcutKeys = True

        ExportCallListToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.E
        ExportCallListToolStripMenuItem.ShowShortcutKeys = True

    End Sub

    Private Sub ClearSelectedRecordDetails()

        txtRadioID.Clear()
        txtCallSign.Clear()
        txtIP.Clear()
        txtMDTIP.Clear()
        txtRadioUser.Clear()
        txtGroupID.Clear()
        txtNotes.Clear()

        txtNotes.BackColor = SystemColors.Control
        txtNotes.ForeColor = SystemColors.ControlText

    End Sub

    Private Sub UpdateStatusAppearance()

        Select Case txtNotes.Text.Trim().ToLower()

            Case "valid"
                txtNotes.BackColor = Color.Honeydew
                txtNotes.ForeColor = Color.SeaGreen

            Case "warning"
                txtNotes.BackColor = Color.LemonChiffon
                txtNotes.ForeColor = Color.DarkOrange

            Case "error"
                txtNotes.BackColor = Color.MistyRose
                txtNotes.ForeColor = Color.Firebrick

            Case Else
                txtNotes.BackColor = SystemColors.Control
                txtNotes.ForeColor = SystemColors.ControlText

        End Select

    End Sub

    Private Sub ShowSelectedRecordDetails()

        If dgvContacts.CurrentRow Is Nothing Then
            ClearSelectedRecordDetails()
            Return
        End If

        If dgvContacts.CurrentRow.IsNewRow Then
            ClearSelectedRecordDetails()
            Return
        End If

        Dim selectedRow As DataGridViewRow =
        dgvContacts.CurrentRow

        txtRadioID.Text =
        Convert.ToString(
            selectedRow.Cells("colRadioID").Value
        )

        txtCallSign.Text =
        Convert.ToString(
            selectedRow.Cells("colCallsign").Value
        )

        txtIP.Text =
        Convert.ToString(
            selectedRow.Cells("colIP").Value
        )

        txtMDTIP.Text =
        Convert.ToString(
            selectedRow.Cells("colMDTIP").Value
        )

        txtRadioUser.Text =
        Convert.ToString(
            selectedRow.Cells("colRadioUser").Value
        )

        txtGroupID.Text =
        Convert.ToString(
            selectedRow.Cells("colGroupID").Value
        )



        UpdateStatusAppearance()

    End Sub

    Private Sub SelectFirstImportedRecord()

        If dgvContacts.Rows.Count = 0 Then
            Return
        End If

        dgvContacts.ClearSelection()

        dgvContacts.Rows(0).Selected = True

        dgvContacts.CurrentCell =
        dgvContacts.Rows(0).
        Cells("colRadioID")

        ShowSelectedRecordDetails()

    End Sub

    Private Function IsBlankRow(fields As String()) As Boolean

        For Each field As String In fields

            If Not String.IsNullOrWhiteSpace(field) Then
                Return False
            End If

        Next

        Return True

    End Function

    Private Function GetCsvField(fields As String(), fieldIndex As Integer) As String

        If fieldIndex < 0 OrElse
       fieldIndex >= fields.Length Then

            Return ""

        End If

        Return fields(fieldIndex).Trim()

    End Function

    Private Function NormalizeHeader(value As String) As String

        If String.IsNullOrWhiteSpace(value) Then
            Return ""
        End If

        Dim result As New StringBuilder()

        For Each character As Char In value

            If Char.IsLetterOrDigit(character) Then
                result.Append(
                Char.ToUpperInvariant(character)
            )
            End If

        Next

        Return result.ToString()

    End Function

    Private Function FindHeaderIndex(headers As String(), ParamArray possibleNames As String()) As Integer

        For index As Integer = 0 To headers.Length - 1

            Dim currentHeader As String =
            NormalizeHeader(headers(index))

            For Each possibleName As String In possibleNames

                If currentHeader =
               NormalizeHeader(possibleName) Then

                    Return index

                End If

            Next

        Next

        Return -1

    End Function
    Private Sub ImportContactsCsv(filePath As String)

        dgvContacts.Rows.Clear()

        Using parser As New TextFieldParser(
        filePath,
        Encoding.UTF8,
        True
    )

            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")
            parser.HasFieldsEnclosedInQuotes = True
            parser.TrimWhiteSpace = True

            If parser.EndOfData Then
                Throw New InvalidDataException(
                "The selected CSV file is empty."
            )
            End If

            'First Row of csv
            Dim headers As String() = parser.ReadFields()

            If headers Is Nothing Then
                Throw New InvalidDataException(
                "The CSV header could not be read."
            )
            End If

            'Find field from first row
            Dim radioIDIndex As Integer =
            FindHeaderIndex(
                headers,
                "RADIO ID DEC",
                "RADIO ID",
                "UNIT ID"
            )

            Dim callsignIndex As Integer =
            FindHeaderIndex(
                headers,
                "CALLSIGN P90",
                "CALLSIGN",
                "Alias [P90]"
            )

            Dim ipIndex As Integer =
            FindHeaderIndex(
                headers,
                "IP"
            )

            Dim mdtIPIndex As Integer =
            FindHeaderIndex(
                headers,
                "MDT IP",
                "MDTIP"
            )

            Dim radioUserIndex As Integer =
            FindHeaderIndex(
                headers,
                "RADIO USER",
                "RADIOUSER"
            )

            Dim groupIDIndex As Integer =
            FindHeaderIndex(
                headers,
                "GROUP ID",
                "GROUPID"
            )

            'Check if rows are exist
            Dim missingHeaders As New List(Of String)

            If radioIDIndex = -1 Then
                missingHeaders.Add("RADIO ID (DEC)")
            End If

            If callsignIndex = -1 Then
                missingHeaders.Add("Callsign [P90]")
            End If

            If ipIndex = -1 Then
                missingHeaders.Add("IP")
            End If

            If mdtIPIndex = -1 Then
                missingHeaders.Add("MDT IP")
            End If

            If radioUserIndex = -1 Then
                missingHeaders.Add("RADIO USER")
            End If

            If groupIDIndex = -1 Then
                missingHeaders.Add("Group ID")
            End If

            If missingHeaders.Count > 0 Then
                Throw New InvalidDataException(
                "The CSV file is missing these columns:" &
                Environment.NewLine &
                String.Join(
                    Environment.NewLine,
                    missingHeaders
                )
            )
            End If

            'Read contacts row by row
            While Not parser.EndOfData

                Dim fields As String() = parser.ReadFields()

                If fields Is Nothing OrElse IsBlankRow(fields) Then
                    Continue While
                End If

                Dim rowIndex As Integer =
                dgvContacts.Rows.Add()

                Dim row As DataGridViewRow =
                dgvContacts.Rows(rowIndex)

                row.Cells("colRadioID").Value =
                GetCsvField(fields, radioIDIndex)

                row.Cells("colCallsign").Value =
                GetCsvField(fields, callsignIndex)

                row.Cells("colIP").Value =
                GetCsvField(fields, ipIndex)

                row.Cells("colMDTIP").Value =
                GetCsvField(fields, mdtIPIndex)

                row.Cells("colRadioUser").Value =
                GetCsvField(fields, radioUserIndex)

                row.Cells("colGroupID").Value =
                GetCsvField(fields, groupIDIndex)

                If dgvContacts.Columns.Contains(
                "colValidationStatus"
            ) Then
                    row.Cells("colValidationStatus").Value =
                    "Not Validated"
                End If

                If dgvContacts.Columns.Contains(
                "colErrorMessage"
            ) Then
                    row.Cells("colErrorMessage").Value = ""
                End If

            End While

        End Using

        SelectFirstImportedRecord()

    End Sub

    Private Function GetXmlFieldValue(
    parentElement As XElement,
    fieldName As String
) As String

        Dim field As XElement =
        parentElement.Descendants("Field").
        FirstOrDefault(
            Function(item)
                Return String.Equals(
                    CStr(item.Attribute("Name")),
                    fieldName,
                    StringComparison.OrdinalIgnoreCase
                )
            End Function
        )

        If field Is Nothing Then
            Return ""
        End If

        Return field.Value.Trim()

    End Function

    Private Function TryParseRadioReferenceKey(
    referenceKey As String,
    ByRef wacnHex As String,
    ByRef systemIDHex As String,
    ByRef unitID As String
) As Boolean

        wacnHex = ""
        systemIDHex = ""
        unitID = ""

        If String.IsNullOrWhiteSpace(referenceKey) Then
            Return False
        End If

        Dim parts As String() =
        referenceKey.Split("-"c)

        If parts.Length < 3 Then
            Return False
        End If

        Dim wacnDecimal As Integer
        Dim systemDecimal As Integer

        Dim wacnText As String =
        parts(parts.Length - 3).Trim()

        Dim systemText As String =
        parts(parts.Length - 2).Trim()

        unitID =
        parts(parts.Length - 1).Trim()

        If Not Integer.TryParse(
        wacnText,
        wacnDecimal
    ) Then
            Return False
        End If

        If Not Integer.TryParse(
        systemText,
        systemDecimal
    ) Then
            Return False
        End If

        wacnHex =
        wacnDecimal.ToString("X")

        systemIDHex =
        systemDecimal.ToString("X")

        Return True

    End Function

    Private Sub ImportUnifiedCallListXml(filePath As String)

        dgvContacts.Rows.Clear()

        Dim document As XDocument =
        XDocument.Load(filePath)

        Dim importedCount As Integer = 0
        Dim skippedCount As Integer = 0

        Dim contactNodes =
        document.Descendants("Node").
        Where(
            Function(node)
                Return String.Equals(
                    CStr(node.Attribute("Name")),
                    "Contacts",
                    StringComparison.OrdinalIgnoreCase
                )
            End Function
        )

        For Each contactNode As XElement In contactNodes

            Dim contactNameField As XElement =
            contactNode.Descendants("Field").
            FirstOrDefault(
                Function(field)
                    Return String.Equals(
                        CStr(field.Attribute("Name")),
                        "Contact Name",
                        StringComparison.OrdinalIgnoreCase
                    )
                End Function
            )

            Dim contactName As String = ""

            If contactNameField IsNot Nothing Then
                contactName = contactNameField.Value.Trim()
            End If

            'A Contact may have multiple ASTRO 25 ID
            Dim astroIDNodes =
            contactNode.Descendants("EmbeddedNode").
            Where(
                Function(node)
                    Return String.Equals(
                        CStr(node.Attribute("Name")),
                        "ASTRO 25 Trunking ID",
                        StringComparison.OrdinalIgnoreCase
                    )
                End Function
            )

            For Each astroNode As XElement In astroIDNodes

                Dim unitID As String =
                GetXmlFieldValue(
                    astroNode,
                    "Unit ID"
                )

                Dim systemName As String =
                GetXmlFieldValue(
                    astroNode,
                    "System Name"
                )

                Dim referenceKey As String =
                Convert.ToString(
                    astroNode.Attribute("ReferenceKey")
                ).Trim()

                If String.IsNullOrWhiteSpace(unitID) Then
                    skippedCount += 1
                    Continue For
                End If

                Dim wacnHex As String = ""
                Dim systemIDHex As String = ""
                Dim unitIDFromReference As String = ""

                TryParseRadioReferenceKey(
                referenceKey,
                wacnHex,
                systemIDHex,
                unitIDFromReference
            )

                Dim rowIndex As Integer =
                dgvContacts.Rows.Add()

                Dim row As DataGridViewRow =
                dgvContacts.Rows(rowIndex)

                row.Cells("colRadioID").Value =
                unitID

                'XML has Callsign, skip it
                row.Cells("colCallsign").Value =
                ""

                row.Cells("colIP").Value =
                ""

                row.Cells("colMDTIP").Value =
                ""

                row.Cells("colRadioUser").Value =
                contactName

                row.Cells("colGroupID").Value =
                ""

                'Save XML other info on temp Tag
                row.Tag =
                New RadioXmlMetadata With {
                    .systemName = systemName,
                    .WacnID = wacnHex,
                    .SystemID = systemIDHex,
                    .referenceKey = referenceKey
                }

                importedCount += 1

            Next

        Next

        If importedCount > 0 Then

            dgvContacts.ClearSelection()

            dgvContacts.Rows(0).Selected = True

            dgvContacts.CurrentCell =
            dgvContacts.Rows(0).
            Cells("colRadioID")

            ShowSelectedRecordDetails()

        Else

            ClearSelectedRecordDetails()

        End If

        txtNotes.Text =
        "XML import completed." &
        Environment.NewLine &
        "Records imported: " &
        importedCount.ToString() &
        Environment.NewLine &
        "Records skipped: " &
        skippedCount.ToString()

        txtNotes.ForeColor = Color.SeaGreen

    End Sub

    Private Sub ToolStripImport_Click(sender As Object, e As EventArgs) Handles ToolStripImport.Click
        Using dialog As New OpenFileDialog()


            dialog.Title = "Import Radio ID Contact File"
            dialog.Filter =
                "Supported Files (*.csv;*.xml)|*.csv;*.xml|" &
                "CSV Files (*.csv)|*.csv|" &
                "XML Files (*.xml)|*.xml|" &
                "All Files (*.*)|*.*"
            dialog.CheckFileExists = True
            dialog.Multiselect = False

            If dialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            Dim extension As String = Path.GetExtension(dialog.FileName).ToLowerInvariant()

            Try
                Select Case extension
                    Case ".csv"
                        ImportContactsCsv(dialog.FileName)
                    Case ".xml"
                        ImportUnifiedCallListXml(dialog.FileName)
                    Case Else
                        txtNotes.Text = "Unsupported file type. Please select a CSV or XML file."
                        txtNotes.ForeColor = Color.Firebrick
                        Return
                End Select

                txtNotes.Text = dgvContacts.Rows.Count.ToString() + " radio records imported successfully." + "Import Complete"

            Catch ex As Exception

                txtNotes.Text = "The CSV file could not be imported." + Environment.NewLine + Environment.NewLine + ex.Message

            End Try

        End Using
    End Sub

    Private Sub dgvContacts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvContacts.SelectionChanged
        ShowSelectedRecordDetails()
    End Sub

    Private Sub ImportRadioIDFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportRadioIDFileToolStripMenuItem.Click
        ToolStripImport_Click(sender, e)
    End Sub

    Private Sub SetRecordEditMode(isEditing As Boolean)

        isEditMode = isEditing

        txtRadioID.ReadOnly = Not isEditing
        txtCallSign.ReadOnly = Not isEditing
        txtIP.ReadOnly = Not isEditing
        txtMDTIP.ReadOnly = Not isEditing
        txtRadioUser.ReadOnly = Not isEditing
        txtGroupID.ReadOnly = Not isEditing

        If dgvContacts.CurrentRow Is Nothing Then

            MessageBox.Show(
            "Please select a record first.",
            "No Record Selected",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        )

            Return
        End If

        'Avoid to select other contacts while editing
        dgvContacts.Enabled = Not isEditing

        If isEditing Then
            txtRadioID.BackColor = Color.White
            txtCallSign.BackColor = Color.White
            txtIP.BackColor = Color.White
            txtMDTIP.BackColor = Color.White
            txtRadioUser.BackColor = Color.White
            txtGroupID.BackColor = Color.White
            txtNotes.Text = "Edit mode enabled. Modify the selected record, then click Apply Changes."
        Else
            txtRadioID.BackColor = SystemColors.Control
            txtCallSign.BackColor = SystemColors.Control
            txtIP.BackColor = SystemColors.Control
            txtMDTIP.BackColor = SystemColors.Control
            txtRadioUser.BackColor = SystemColors.Control
            txtGroupID.BackColor = SystemColors.Control
        End If

        savedRadioID = txtRadioID.Text
        savedCallsign = txtCallSign.Text
        savedIP = txtIP.Text
        savedMDTIP = txtMDTIP.Text
        savedRadioUser = txtRadioUser.Text
        savedGroupID = txtGroupID.Text

        EditSelectedRecordToolStripMenuItem.Enabled = False

    End Sub

    Private Sub EditSelectedRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditSelectedRecordToolStripMenuItem.Click
        If dgvContacts.CurrentRow Is Nothing Then

            MessageBox.Show(
                "Please select a record first.",
                "No Record Selected",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            Return
        End If

        SetRecordEditMode(True)

        txtRadioID.Focus()
        txtRadioID.SelectAll()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtRadioID.Text = savedRadioID
        txtCallSign.Text = savedCallsign
        txtIP.Text = savedIP
        txtMDTIP.Text = savedMDTIP
        txtRadioUser.Text = savedRadioUser
        txtGroupID.Text = savedGroupID

        'Exit Edit mode
        SetRecordEditMode(False)

        'Display back the previous saved data
        ShowSelectedRecordDetails()

        EditSelectedRecordToolStripMenuItem.Enabled = True
    End Sub

    Private Sub btnApplyChanges_Click(sender As Object, e As EventArgs) Handles btnApplyChanges.Click
        'if not on Edit Mode, display on Notes
        If Not isEditMode Then

            txtNotes.Text =
            "Please click Edit Selected Record before applying changes."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        If dgvContacts.CurrentRow Is Nothing Then

            txtNotes.Text =
            "No record is currently selected."

            txtNotes.ForeColor = Color.Firebrick
            Return

        End If

        If String.IsNullOrWhiteSpace(txtRadioID.Text) Then

            txtNotes.Text =
            "Radio ID is required."

            txtNotes.ForeColor = Color.Firebrick
            txtRadioID.Focus()
            Return

        End If

        If Not txtRadioID.Text.Trim().
        All(AddressOf Char.IsDigit) Then

            txtNotes.Text =
            "Radio ID must contain numbers only."

            txtNotes.ForeColor = Color.Firebrick
            txtRadioID.Focus()
            txtRadioID.SelectAll()
            Return

        End If

        Dim selectedRow As DataGridViewRow =
        dgvContacts.CurrentRow

        selectedRow.Cells("colRadioID").Value =
        txtRadioID.Text.Trim()

        selectedRow.Cells("colCallsign").Value =
        txtCallSign.Text.Trim()

        selectedRow.Cells("colIP").Value =
        txtIP.Text.Trim()

        selectedRow.Cells("colMDTIP").Value =
        txtMDTIP.Text.Trim()

        selectedRow.Cells("colRadioUser").Value =
        txtRadioUser.Text.Trim()

        selectedRow.Cells("colGroupID").Value =
        txtGroupID.Text.Trim()

        SetRecordEditMode(False)
        ShowSelectedRecordDetails()

        txtNotes.Text =
        "Changes applied successfully. The record must be validated again."

        txtNotes.ForeColor = Color.SeaGreen

        EditSelectedRecordToolStripMenuItem.Enabled = True
    End Sub

    Private Function CellContains(row As DataGridViewRow, columnName As String, searchText As String) As Boolean

        If Not dgvContacts.Columns.Contains(columnName) Then
            Return False
        End If

        Dim cellText As String =
        Convert.ToString(
            row.Cells(columnName).Value
        )

        Return cellText.IndexOf(
        searchText,
        StringComparison.OrdinalIgnoreCase
    ) >= 0

    End Function

    Private Function SearchRows(searchText As String, firstRow As Integer, lastRow As Integer) As Integer

        If firstRow < 0 Then firstRow = 0

        If lastRow >= dgvContacts.Rows.Count Then
            lastRow = dgvContacts.Rows.Count - 1
        End If

        For rowIndex As Integer = firstRow To lastRow

            Dim row As DataGridViewRow =
            dgvContacts.Rows(rowIndex)

            If row.IsNewRow Then Continue For

            If CellContains(row, "colRadioID", searchText) OrElse
           CellContains(row, "colCallsign", searchText) OrElse
           CellContains(row, "colIP", searchText) OrElse
           CellContains(row, "colMDTIP", searchText) OrElse
           CellContains(row, "colRadioUser", searchText) OrElse
           CellContains(row, "colGroupID", searchText) Then

                Return rowIndex

            End If

        Next

        Return -1

    End Function

    Private Sub FindContactRecord(searchText As String)

        If dgvContacts.Rows.Count = 0 Then Return

        Dim startRow As Integer

        'When searching new items, search from first row
        If Not searchText.Equals(
        lastSearchText,
        StringComparison.OrdinalIgnoreCase
    ) Then

            lastFoundRowIndex = -1
        End If

        lastSearchText = searchText
        startRow = lastFoundRowIndex + 1

        'Start searching downwards from the line following the current result
        Dim foundIndex As Integer =
        SearchRows(searchText, startRow, dgvContacts.Rows.Count - 1)

        'If not found, restart the search from the first line
        If foundIndex = -1 AndAlso startRow > 0 Then
            foundIndex = SearchRows(searchText, 0, startRow - 1)
        End If

        If foundIndex = -1 Then

            txtNotes.Text =
            "No matching record was found for: " & searchText

            txtNotes.ForeColor = Color.Firebrick
            lastFoundRowIndex = -1
            Return

        End If

        lastFoundRowIndex = foundIndex

        dgvContacts.ClearSelection()

        Dim foundRow As DataGridViewRow =
        dgvContacts.Rows(foundIndex)

        foundRow.Selected = True

        dgvContacts.CurrentCell =
        foundRow.Cells("colRadioID")

        dgvContacts.FirstDisplayedScrollingRowIndex =
        foundIndex

        ShowSelectedRecordDetails()

        txtNotes.Text =
        "Record found at row " &
        (foundIndex + 1).ToString() &
        ". Press Ctrl+F again to find the next match."

        txtNotes.ForeColor = Color.SeaGreen

    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        If dgvContacts.Rows.Count = 0 Then
            txtNotes.Text = "There are no records to search."
            txtNotes.ForeColor = Color.DarkOrange
            Return
        End If

        Dim searchText As String =
            InputBox(
                "Enter a Radio ID, callsign, IP address, radio user, or group ID:",
                "Find Record",
                lastSearchText
            ).Trim()

        If searchText = "" Then Return

        FindContactRecord(searchText)
    End Sub

    Private Sub DeleteSelectedRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteSelectedRecordToolStripMenuItem.Click
        'Disable the delete feature on Editing Mode
        If isEditMode Then

            txtNotes.Text =
            "Please apply or reset the current changes before deleting records."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        'No Record is selected
        If dgvContacts.SelectedRows.Count = 0 Then

            txtNotes.Text =
            "Please select one or more records to delete."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        Dim selectedCount As Integer =
        dgvContacts.SelectedRows.Count

        Dim message As String

        If selectedCount = 1 Then
            message =
            "Are you sure you want to delete the selected record?"
        Else
            message =
            "Are you sure you want to delete these " &
            selectedCount.ToString() &
            " selected records?"
        End If

        Dim result As DialogResult =
        MessageBox.Show(
            message,
            "Delete Selected Records",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
        )

        If result <> DialogResult.Yes Then
            txtNotes.Text = "Delete operation cancelled."
            txtNotes.ForeColor = Color.DimGray
            Return
        End If

        'First, copy the selected rows to avoid changes to the SelectedRows collection during the deletion process.
        Dim rowsToDelete As New List(Of DataGridViewRow)

        For Each row As DataGridViewRow In dgvContacts.SelectedRows

            If Not row.IsNewRow Then
                rowsToDelete.Add(row)
            End If

        Next

        'Delete from the bottom up to prevent row numbers from changing.
        rowsToDelete.Sort(
        Function(row1, row2)
            Return row2.Index.CompareTo(row1.Index)
        End Function
    )

        Dim nextRowIndex As Integer =
        rowsToDelete(rowsToDelete.Count - 1).Index

        For Each row As DataGridViewRow In rowsToDelete
            dgvContacts.Rows.Remove(row)
        Next

        'Select a nearby line after deletion
        If dgvContacts.Rows.Count > 0 Then

            If nextRowIndex >= dgvContacts.Rows.Count Then
                nextRowIndex = dgvContacts.Rows.Count - 1
            End If

            dgvContacts.ClearSelection()

            dgvContacts.Rows(nextRowIndex).Selected = True

            dgvContacts.CurrentCell =
            dgvContacts.Rows(nextRowIndex).
            Cells("colRadioID")

            ShowSelectedRecordDetails()

        Else

            ClearSelectedRecordDetails()

        End If

        txtNotes.Text =
        selectedCount.ToString() &
        " record(s) deleted successfully."

        txtNotes.ForeColor = Color.SeaGreen

        'Summary
        'UpdateValidationSummary()

    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        btnReset_Click(sender, e)
    End Sub

    Private Function GetGridCellText(row As DataGridViewRow, columnName As String) As String

        If Not dgvContacts.Columns.Contains(columnName) Then
            Return ""
        End If

        Return Convert.ToString(
        row.Cells(columnName).Value
    ).Trim()

    End Function

    Private Function CreateExportAlias(sourceText As String, radioID As String, aliasLength As Integer, idLength As Integer) As String

        Dim normalizedAlias As String =
        Convert.ToString(sourceText).Trim()

        radioID = Convert.ToString(radioID).Trim()

        'Delete Alias continues extra space
        While normalizedAlias.Contains("  ")
            normalizedAlias =
            normalizedAlias.Replace("  ", " ")
        End While

        Dim aliasPart As String

        'Read Alias from left to right
        If normalizedAlias.Length > aliasLength Then

            aliasPart =
            normalizedAlias.Substring(
                0,
                aliasLength
            ).TrimEnd()

        Else

            aliasPart = normalizedAlias

        End If

        Dim idPart As String

        'Read Radio ID from right to left
        If radioID.Length > idLength Then

            idPart =
            radioID.Substring(
                radioID.Length - idLength,
                idLength
            )

        Else

            idPart = radioID

        End If

        Return (
        aliasPart & " " & idPart
    ).Trim()

    End Function

    Private Function TryBuildExportRecords(aliasLength As Integer, idLength As Integer, ByRef exportRecords As List(Of RadioExportRecord)) As Boolean

        exportRecords = New List(Of RadioExportRecord)

        Dim errors As New List(Of String)

        Dim usedRadioIDs As New HashSet(Of String)(
        StringComparer.OrdinalIgnoreCase
    )

        For Each row As DataGridViewRow In dgvContacts.Rows

            If row.IsNewRow Then Continue For

            Dim displayedRowNumber As Integer =
            row.Index + 1

            Dim radioID As String =
            GetGridCellText(row, "colRadioID")

            Dim aliasSource As String =
            GetGridCellText(row, "colRadioUser")

            'Radio User is empty uses Callsign
            If String.IsNullOrWhiteSpace(aliasSource) Then
                aliasSource =
                GetGridCellText(row, "colCallsign")
            End If

            Dim errorCountBefore As Integer =
            errors.Count

            If String.IsNullOrWhiteSpace(radioID) Then

                errors.Add(
                "Row " &
                displayedRowNumber.ToString() &
                ": Radio ID is required."
            )

            Else

                If Not radioID.All(
                Function(character)
                    Return Char.IsDigit(character)
                End Function
            ) Then

                    errors.Add(
                    "Row " &
                    displayedRowNumber.ToString() &
                    ": Radio ID must contain numbers only."
                )

                End If

                If radioID.Length < idLength Then

                    errors.Add(
                        "Row " &
                        displayedRowNumber.ToString() &
                        ": Radio ID must contain at least " &
                        idLength.ToString() &
                        " digits to generate the user alias."
                    )

                End If

                If usedRadioIDs.Contains(radioID) Then

                    errors.Add(
                    "Row " &
                    displayedRowNumber.ToString() &
                    ": Duplicate Radio ID " &
                    radioID &
                    "."
                )

                Else
                    usedRadioIDs.Add(radioID)
                End If

            End If

            If String.IsNullOrWhiteSpace(aliasSource) Then

                errors.Add(
                "Row " &
                displayedRowNumber.ToString() &
                ": Radio User or Callsign is required."
            )

            End If

            Dim exportAlias As String = CreateExportAlias(
                aliasSource,
                radioID,
                aliasLength,
                idLength
            )

            'Add this line to the export list if there are no errors.
            If errors.Count = errorCountBefore Then

                exportRecords.Add(
                New RadioExportRecord With {
                    .RadioID = radioID,
                    .AliasText = exportAlias
                }
            )

            End If

        Next

        If errors.Count > 0 Then

            Dim displayedErrors As IEnumerable(Of String) =
            errors.Take(10)

            txtNotes.Text =
            "Export stopped because validation errors were found:" &
            Environment.NewLine &
            Environment.NewLine &
            String.Join(
                Environment.NewLine,
                displayedErrors
            )

            If errors.Count > 10 Then

                txtNotes.Text &=
                Environment.NewLine &
                "...and " &
                (errors.Count - 10).ToString() &
                " more error(s)."

            End If

            txtNotes.ForeColor = Color.Firebrick
            Return False

        End If

        If exportRecords.Count = 0 Then

            txtNotes.Text =
            "There are no records available to export."

            txtNotes.ForeColor = Color.DarkOrange
            Return False

        End If

        Return True

    End Function

    Private Function SelectExportFilePath(defaultFileName As String) As String

        Using dialog As New SaveFileDialog()

            dialog.Title = "Export Radio Call List"
            dialog.Filter =
            "CSV Files (*.csv)|*.csv"

            dialog.DefaultExt = "csv"
            dialog.AddExtension = True
            dialog.OverwritePrompt = True

            dialog.FileName =
            defaultFileName &
            "_" &
            DateTime.Now.ToString(
                "yyyyMMdd_HHmmss"
            ) &
            ".csv"

            If dialog.ShowDialog() =
           DialogResult.OK Then

                Return dialog.FileName

            End If

        End Using

        Return ""

    End Function

    Private Function EscapeCsvValue(value As String) As String

        If value Is Nothing Then
            Return ""
        End If

        If value.Contains(",") OrElse
       value.Contains("""") OrElse
       value.Contains(vbCr) OrElse
       value.Contains(vbLf) Then

            Return """" &
            value.Replace("""", """""") &
            """"

        End If

        Return value

    End Function

    Private Sub WriteCsvRow(writer As StreamWriter, ParamArray values As String())

        Dim escapedValues As New List(Of String)

        For Each value As String In values
            escapedValues.Add(
            EscapeCsvValue(value)
        )
        Next

        writer.WriteLine(
        String.Join(",", escapedValues)
    )

    End Sub

    Private Sub ShowExportSuccess(targetFormat As String, filePath As String, recordCount As Integer, aliasLength As Integer, idLength As Integer)

        txtNotes.ForeColor = Color.SeaGreen

        txtNotes.Text =
        targetFormat &
        " export completed successfully." &
        Environment.NewLine &
        "Records Exported: " &
        recordCount.ToString() &
        Environment.NewLine &
        "Alias Length: " &
        aliasLength.ToString() &
        Environment.NewLine &
        "Maximum ID Length: " &
        idLength.ToString() &
        Environment.NewLine &
        "File: " &
        filePath

    End Sub

    Private Sub ExportForMotorolaXTSRadio(
    aliasLength As Integer,
    idLength As Integer
)

        Dim records As List(Of RadioExportRecord) = Nothing

        If Not TryBuildExportRecords(
        aliasLength,
        idLength,
        records
    ) Then
            Return
        End If

        Dim filePath As String =
        SelectExportFilePath(
            "Motorola_XTS_Call_List"
        )

        If filePath = "" Then

            txtNotes.Text =
            "Motorola XTS export was cancelled."

            txtNotes.ForeColor = Color.DimGray
            Return

        End If

        Try

            Using writer As New StreamWriter(
            filePath,
            False,
            New UTF8Encoding(False)
        )
                WriteCsvRow(
                writer,
                "__File Format Version__",
                "0x01",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Feature__",
                "Trunking Call List",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Record__",
                "1 of 1 (""Trunking Call List"")",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Section__",
                "General",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Field Name__",
                " __Field Value__",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "List Alias",
                "Export List",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Section__",
                "List",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "__Field Names__",
                "",
                "",
                ""
            )

                WriteCsvRow(
                writer,
                "Call List ID",
                "Call List Text",
                "WACN ID",
                "System ID"
            )

                WriteCsvRow(
                writer,
                "__Field Values__",
                "",
                "",
                ""
            )

                For Each record As RadioExportRecord _
                In records

                    WriteCsvRow(
                    writer,
                    record.RadioID,
                    record.AliasText,
                    DefaultWacnID,
                    DefaultSystemID
                )

                Next

            End Using

            ShowExportSuccess(
            "Motorola XTS",
            filePath,
            records.Count,
            aliasLength,
            idLength
        )

        Catch ex As Exception

            txtNotes.Text =
            "Motorola XTS export failed." &
            Environment.NewLine &
            ex.Message

            txtNotes.ForeColor = Color.Firebrick

        End Try

    End Sub

    Private Sub ExportForMotorolaAPXRadio(aliasLength As Integer, idLength As Integer)

        Dim records As List(Of RadioExportRecord) = Nothing

        If Not TryBuildExportRecords(
            aliasLength,
            idLength,
            records
        ) Then
            Return
        End If

        Dim filePath As String =
        SelectExportFilePath(
            "Motorola_APX_Call_List"
        )

        If filePath = "" Then

            txtNotes.Text =
            "Motorola APX export was cancelled."

            txtNotes.ForeColor = Color.DimGray
            Return

        End If

        Try

            Using writer As New StreamWriter(
            filePath,
            False,
            New UTF8Encoding(False)
        )

                WriteCsvRow(
                writer,
                "Radio ID",
                "Radio User Alias"
            )

                For Each record As RadioExportRecord _
                In records

                    WriteCsvRow(
                    writer,
                    record.RadioID,
                    record.AliasText
                )

                Next

            End Using

            ShowExportSuccess(
            "Motorola APX",
            filePath,
            records.Count,
            aliasLength,
            idLength
        )

        Catch ex As Exception

            txtNotes.Text =
            "Motorola APX export failed." &
            Environment.NewLine &
            ex.Message

            txtNotes.ForeColor = Color.Firebrick

        End Try

    End Sub

    Private Sub ToolStripExport_Click(sender As Object, e As EventArgs) Handles ToolStripExport.Click
        Dim aliasLength As Integer
        Dim idLength As Integer

        If Not Integer.TryParse(
        tsbAliasLength.Text.Trim(),
        aliasLength
        ) OrElse aliasLength <= 0 Then

            txtNotes.Text =
            "Please enter a valid whole number for the Alias Length."

            txtNotes.ForeColor = Color.Firebrick
            Exit Sub
        End If

        If Not Integer.TryParse(
            tsbIDLength.Text.Trim(),
            idLength
        ) OrElse idLength <= 0 Then

            txtNotes.Text =
            "Please enter a valid whole number for the ID Length."

            txtNotes.ForeColor = Color.Firebrick
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(tscbTargetFormat.Text) Then

            txtNotes.Text =
            "Please select a target format before exporting."

            txtNotes.ForeColor = Color.DarkOrange
            Exit Sub
        End If

        Select Case tscbTargetFormat.Text.Trim()

            Case "EF Johnson"
                'ExportForEFJohnsonRadio(aliasLength, idLength)

            Case "Motorola XTS"
                ExportForMotorolaXTSRadio(aliasLength, idLength)

            Case "Motorola APX"
                ExportForMotorolaAPXRadio(aliasLength, idLength)

            Case "Harris RPM"
                'ExportForHarrisRPMRadio(aliasLength, idLength)

            Case Else
                txtNotes.Text =
                "The selected target format is not supported."

                txtNotes.ForeColor = Color.Firebrick

        End Select

    End Sub

    Private Sub ExportCallListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCallListToolStripMenuItem.Click
        ToolStripExport_Click(sender, e)
    End Sub
End Class