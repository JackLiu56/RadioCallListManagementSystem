Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text
Imports System.Xml.Linq
Imports System.Linq

Public Class frmRadioCallListManager
    Private savedRadioID As String
    Private savedCallsign As String
    Private savedIP As String
    Private savedMDTIP As String
    Private savedRadioUser As String
    Private savedGroupID As String
    Private isEditMode As Boolean = False
    Private lastSearchText As String = ""
    Private lastFoundRowIndex As Integer = -1
    Private currentImportFilePath As String = ""
    Private hasUnsavedChanges As Boolean = False
    Private isAddingNewRecord As Boolean = False
    Private editingRow As DataGridViewRow = Nothing
    Private rightClickedRow As DataGridViewRow = Nothing

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

    Private Sub AddExportLog(
    targetFormat As String,
    aliasLength As Integer,
    idLength As Integer,
    recordCount As Integer,
    status As String,
    filePath As String,
    message As String
)

        Dim fileName As String = ""

        If Not String.IsNullOrWhiteSpace(filePath) Then
            fileName =
            System.IO.Path.GetFileName(filePath)
        End If

        Dim rowIndex As Integer =
            dgvExportLog.Rows.Add(
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            fileName,
            targetFormat,
            recordCount.ToString(),
            status,
            message
        )

        Dim logRow As DataGridViewRow =
        dgvExportLog.Rows(rowIndex)

        Select Case status.Trim().ToLowerInvariant()

            Case "success"

                logRow.DefaultCellStyle.BackColor =
                Color.Honeydew

            Case "failed"

                logRow.DefaultCellStyle.BackColor =
                Color.MistyRose

            Case "cancelled"

                logRow.DefaultCellStyle.BackColor =
                Color.LemonChiffon

        End Select

    End Sub

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
            "EF Johnson (Not Support yet)",
            "Motorola APX",
            "Motorola XTS",
            "Harris RPM (Not Support yet)"
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

        SaveRecordsToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        SaveRecordsToolStripMenuItem.ShowShortcutKeys = True

        SaveRecordsAsToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.S
        SaveRecordsAsToolStripMenuItem.ShowShortcutKeys = True

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If Not hasUnsavedChanges Then Return

        Dim result As DialogResult =
        MessageBox.Show(
            "There are unsaved contact changes. Close without saving?",
            "Unsaved Changes",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
        )

        If result <> DialogResult.Yes Then
            e.Cancel = True
        End If

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
                    .SystemName = systemName,
                    .WacnID = wacnHex,
                    .SystemID = systemIDHex,
                    .ReferenceKey = referenceKey
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

                currentImportFilePath = dialog.FileName
                Me.Text = "Radio Call List Manager" + " (" + currentImportFilePath + ")"

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
        ToolStripImport.PerformClick()
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

        isAddingNewRecord = False
        editingRow = dgvContacts.CurrentRow

        SetRecordEditMode(True)

        txtRadioID.Focus()
        txtRadioID.SelectAll()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If Not isEditMode Then

            txtNotes.Text =
            "Reset is only available while editing or adding a record."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        Dim wasAddingNewRecord As Boolean =
        isAddingNewRecord

        isAddingNewRecord = False
        editingRow = Nothing

        SetRecordEditMode(False)

        If wasAddingNewRecord Then

            ClearRecordEntryFields()

            If dgvContacts.Rows.Count > 0 Then

                dgvContacts.Rows(0).Selected = True
                dgvContacts.CurrentCell =
                dgvContacts.Rows(0).
                Cells("colRadioID")

                ShowSelectedRecordDetails()

            End If

            txtNotes.Text =
            "The new record was discarded."

        Else

            ShowSelectedRecordDetails()

            txtNotes.Text =
            "Unsaved changes were discarded."

        End If

        txtNotes.ForeColor = Color.DimGray

        EditSelectedRecordToolStripMenuItem.Enabled = True
    End Sub

    Private Function IsDuplicateRadioID(radioID As String, currentRow As DataGridViewRow) As Boolean

        For Each row As DataGridViewRow In dgvContacts.Rows

            If row.IsNewRow Then Continue For

            'Not compare to self
            If row Is currentRow Then Continue For

            Dim existingID As String =
            Convert.ToString(
                row.Cells("colRadioID").Value
            ).Trim()

            If existingID.Equals(
            radioID,
            StringComparison.OrdinalIgnoreCase
        ) Then

                Return True

            End If

        Next

        Return False

    End Function

    Private Sub btnApplyChanges_Click(sender As Object, e As EventArgs) Handles btnApplyChanges.Click
        If Not isEditMode Then

            txtNotes.Text =
            "Please enter Edit Mode before applying changes."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        Dim radioID As String =
        txtRadioID.Text.Trim()

        If radioID = "" Then

            txtNotes.Text =
            "Radio ID is required."

            txtNotes.ForeColor = Color.Firebrick
            txtRadioID.Focus()
            Return

        End If

        If Not radioID.All(AddressOf Char.IsDigit) Then

            txtNotes.Text =
            "Radio ID must contain numbers only."

            txtNotes.ForeColor = Color.Firebrick
            txtRadioID.Focus()
            txtRadioID.SelectAll()
            Return

        End If

        Dim targetRow As DataGridViewRow

        If isAddingNewRecord Then

            If IsDuplicateRadioID(radioID, Nothing) Then

                txtNotes.Text =
                "Radio ID " & radioID & " already exists."

                txtNotes.ForeColor = Color.Firebrick
                Return

            End If

            Dim newRowIndex As Integer =
            dgvContacts.Rows.Add()

            targetRow =
            dgvContacts.Rows(newRowIndex)

        Else

            If editingRow Is Nothing Then

                txtNotes.Text =
                "The selected record is no longer available."

                txtNotes.ForeColor = Color.Firebrick
                Return

            End If

            If IsDuplicateRadioID(radioID, editingRow) Then

                txtNotes.Text =
                "Radio ID " & radioID & " already exists."

                txtNotes.ForeColor = Color.Firebrick
                Return

            End If

            targetRow = editingRow

        End If

        targetRow.Cells("colRadioID").Value =
        radioID

        targetRow.Cells("colCallsign").Value =
        txtCallSign.Text.Trim()

        targetRow.Cells("colIP").Value =
        txtIP.Text.Trim()

        targetRow.Cells("colMDTIP").Value =
        txtMDTIP.Text.Trim()

        targetRow.Cells("colRadioUser").Value =
        txtRadioUser.Text.Trim()

        targetRow.Cells("colGroupID").Value =
        txtGroupID.Text.Trim()

        Dim recordWasAdded As Boolean =
        isAddingNewRecord

        isAddingNewRecord = False
        editingRow = Nothing
        hasUnsavedChanges = True

        SetRecordEditMode(False)

        dgvContacts.ClearSelection()
        targetRow.Selected = True
        dgvContacts.CurrentCell =
        targetRow.Cells("colRadioID")

        ShowSelectedRecordDetails()

        If recordWasAdded Then

            txtNotes.Text =
            "The new record was added successfully."

        Else

            txtNotes.Text =
            "Changes were applied successfully."

        End If

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

    Private Sub DeleteSelectedRecords()

        If isEditMode Then
            txtNotes.Text =
            "Please apply or reset the current changes before deleting records."
            txtNotes.ForeColor = Color.DarkOrange
            Return
        End If

        If dgvContacts.SelectedRows.Count = 0 Then
            txtNotes.Text =
            "Please select one or more records to delete."
            txtNotes.ForeColor = Color.DarkOrange
            Return
        End If

        Dim selectedCount As Integer =
        dgvContacts.SelectedRows.Count

        Dim result As DialogResult =
        MessageBox.Show(
            "Are you sure you want to delete " &
            selectedCount.ToString() &
            " selected record(s)?",
            "Delete Records",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
        )

        If result <> DialogResult.Yes Then
            txtNotes.Text = "Delete operation cancelled."
            txtNotes.ForeColor = Color.DimGray
            Return
        End If

        Dim rowsToDelete As New List(Of DataGridViewRow)

        For Each row As DataGridViewRow In dgvContacts.SelectedRows
            If Not row.IsNewRow Then
                rowsToDelete.Add(row)
            End If
        Next

        'Delete from the bottom up to prevent row numbers from changing.
        rowsToDelete.Sort(
        Function(a, b)
            Return b.Index.CompareTo(a.Index)
        End Function
    )

        For Each row As DataGridViewRow In rowsToDelete
            dgvContacts.Rows.Remove(row)
        Next

        hasUnsavedChanges = True

        If dgvContacts.Rows.Count > 0 Then

            dgvContacts.ClearSelection()
            dgvContacts.Rows(0).Selected = True
            dgvContacts.CurrentCell =
            dgvContacts.Rows(0).Cells("colRadioID")

            ShowSelectedRecordDetails()

        Else
            ClearSelectedRecordDetails()
        End If

        txtNotes.Text =
        selectedCount.ToString() &
        " record(s) deleted successfully."

        txtNotes.ForeColor = Color.SeaGreen

    End Sub

    Private Sub DeleteSelectedRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteSelectedRecordToolStripMenuItem.Click
        DeleteSelectedRecords()
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        btnReset.PerformClick()
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

        AddExportLog(
        targetFormat,
        aliasLength,
        idLength,
        recordCount,
        "Success",
        filePath,
        filePath
    )

    End Sub

    Private Sub ShowExportFailure(targetFormat As String, aliasLength As Integer, idLength As Integer, filePath As String, errorMessage As String)

        txtNotes.Text =
        targetFormat &
        " export failed." &
        Environment.NewLine &
        errorMessage

        txtNotes.ForeColor = Color.Firebrick

        AddExportLog(
        targetFormat,
        aliasLength,
        idLength,
        0,
        "Failed",
        filePath,
        errorMessage
    )

    End Sub

    Private Sub ExportForMotorolaXTSRadio(aliasLength As Integer, idLength As Integer)

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

            ShowExportFailure(
                "Motorola XTS",
                aliasLength,
                idLength,
                filePath,
                ex.Message
            )

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

            ShowExportFailure(
                "Harris RPM",
                aliasLength,
                idLength,
                filePath,
                ex.Message
            )

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
        ToolStripExport.PerformClick()
    End Sub

    Private Sub RefreshImportedFile()

        Try

            Dim extension As String =
            System.IO.Path.GetExtension(
                currentImportFilePath
            ).ToLowerInvariant()

            Select Case extension

                Case ".csv"
                    ImportContactsCsv(currentImportFilePath)

                Case ".xml"
                    ImportUnifiedCallListXml(currentImportFilePath)

                Case Else

                    txtNotes.Text =
                    "The current file type is not supported: " &
                    extension

                    txtNotes.ForeColor = Color.Firebrick
                    Return

            End Select

            txtNotes.Text =
            "Records refreshed successfully." &
            Environment.NewLine &
            "Source: " &
            currentImportFilePath &
            Environment.NewLine &
            "Records loaded: " &
            dgvContacts.Rows.Count.ToString()

            txtNotes.ForeColor = Color.SeaGreen

        Catch ex As Exception

            txtNotes.Text =
            "Refresh failed." &
            Environment.NewLine &
            ex.Message

            txtNotes.ForeColor = Color.Firebrick

        End Try

    End Sub

    Private Sub ToolStripRefresh_Click(sender As Object, e As EventArgs) Handles ToolStripRefresh.Click
        If isEditMode Then

            txtNotes.Text =
                "Please apply or reset the current changes before refreshing."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        If String.IsNullOrWhiteSpace(currentImportFilePath) Then

            txtNotes.Text =
                "No source file has been imported yet."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        If Not System.IO.File.Exists(currentImportFilePath) Then

            txtNotes.Text =
                "The original source file could not be found:" &
                Environment.NewLine &
                currentImportFilePath

            txtNotes.ForeColor = Color.Firebrick
            Return

        End If

        Dim result As DialogResult =
            MessageBox.Show(
                "Refreshing will reload the original file and discard unsaved changes in the table." &
                Environment.NewLine &
                Environment.NewLine &
                "Continue?",
                "Refresh Records",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

        If result <> DialogResult.Yes Then

            txtNotes.Text = "Refresh cancelled."
            txtNotes.ForeColor = Color.DimGray
            Return

        End If

        RefreshImportedFile()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        ToolStripRefresh.PerformClick()
    End Sub

    Private Sub SaveContactsAsCsv()

        Using dialog As New SaveFileDialog()

            dialog.Title = "Save Contact Records"

            dialog.Filter =
            "CSV Files (*.csv)|*.csv"

            dialog.DefaultExt = "csv"
            dialog.AddExtension = True
            dialog.OverwritePrompt = True

            dialog.FileName =
            "Radio_Contacts_" &
            DateTime.Now.ToString("yyyyMMdd_HHmmss") &
            ".csv"

            If dialog.ShowDialog() <> DialogResult.OK Then

                txtNotes.Text =
                "Save operation cancelled."

                txtNotes.ForeColor = Color.DimGray
                Return

            End If

            SaveContactsToCsv(dialog.FileName)

            currentImportFilePath = dialog.FileName
            Me.Text = "Radio Call List Manager" + " (" + currentImportFilePath + ")"

        End Using

    End Sub

    Private Sub SaveContactsToCsv(filePath As String)

        Try

            Using writer As New StreamWriter(
            filePath,
            False,
            New UTF8Encoding(True)
        )

                WriteCsvRow(
                writer,
                "RADIO ID (DEC)",
                "Callsign [P90]",
                "IP",
                "MDT IP",
                "RADIO USER",
                "Group ID"
            )

                For Each row As DataGridViewRow In dgvContacts.Rows

                    If row.IsNewRow Then Continue For

                    WriteCsvRow(
                    writer,
                    GetGridCellText(row, "colRadioID"),
                    GetGridCellText(row, "colCallsign"),
                    GetGridCellText(row, "colIP"),
                    GetGridCellText(row, "colMDTIP"),
                    GetGridCellText(row, "colRadioUser"),
                    GetGridCellText(row, "colGroupID")
                )

                Next

            End Using

            hasUnsavedChanges = False

            txtNotes.Text =
            "Contact records saved successfully." &
            Environment.NewLine &
            "Records saved: " &
            dgvContacts.Rows.Count.ToString() &
            Environment.NewLine &
            "File: " &
            filePath

            txtNotes.ForeColor = Color.SeaGreen

        Catch ex As Exception

            txtNotes.Text =
            "The contact file could not be saved." &
            Environment.NewLine &
            ex.Message

            txtNotes.ForeColor = Color.Firebrick

        End Try

    End Sub

    Private Sub ToolStripSave_Click(sender As Object, e As EventArgs) Handles ToolStripSave.Click
        If isEditMode Then

            txtNotes.Text =
                "Please apply or reset the current changes before saving."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        If dgvContacts.Rows.Count = 0 Then

            txtNotes.Text =
                "There are no contact records to save."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        If String.IsNullOrWhiteSpace(currentImportFilePath) Then

            SaveContactsAsCsv()
            Return

        End If

        Dim extension As String =
            System.IO.Path.GetExtension(
                currentImportFilePath
            ).ToLowerInvariant()

        Select Case extension

            Case ".csv"
                SaveContactsToCsv(currentImportFilePath)

            Case ".xml"
                txtNotes.Text =
                    "The imported file is XML. Please save the edited records as a new CSV file."

                txtNotes.ForeColor = Color.DarkOrange

                SaveContactsAsCsv()

            Case Else
                SaveContactsAsCsv()

        End Select
    End Sub

    Private Sub SaveRecordsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveRecordsToolStripMenuItem.Click
        ToolStripSave.PerformClick()
    End Sub

    Private Sub SaveRecordsAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveRecordsAsToolStripMenuItem.Click
        If isEditMode Then
            txtNotes.Text =
                "Please apply or reset the current changes before saving."

            txtNotes.ForeColor = Color.DarkOrange
            Return
        End If

        If dgvContacts.Rows.Count = 0 Then
            txtNotes.Text =
                "There are no contact records to save."

            txtNotes.ForeColor = Color.DarkOrange
            Return
        End If

        SaveContactsAsCsv()
    End Sub

    Private Sub ClearRecordEntryFields()

        txtRadioID.Clear()
        txtCallSign.Clear()
        txtIP.Clear()
        txtMDTIP.Clear()
        txtRadioUser.Clear()
        txtGroupID.Clear()

    End Sub

    Private Sub AddNewRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewRecordToolStripMenuItem.Click
        If isEditMode Then

            txtNotes.Text =
            "Please apply or reset the current changes before adding a new record."

            txtNotes.ForeColor = Color.DarkOrange
            Return

        End If

        isAddingNewRecord = True
        editingRow = Nothing

        SetRecordEditMode(True)

        dgvContacts.ClearSelection()

        ClearRecordEntryFields()

        txtRadioID.Focus()

        txtNotes.Text =
        "Enter the new record details, then click Apply Changes."

        txtNotes.ForeColor = Color.RoyalBlue
    End Sub

    Private Sub AddSelectedRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSelectedRecordToolStripMenuItem.Click
        AddNewRecordToolStripMenuItem.PerformClick()
    End Sub

    Private Sub EditSelectedRecordToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditSelectedRecordToolStripMenuItem1.Click
        EditSelectedRecordToolStripMenuItem.PerformClick()
    End Sub

    Private Sub DeleteSelectedRecordToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteSelectedRecordToolStripMenuItem1.Click
        DeleteSelectedRecords()
    End Sub

    Private Sub dgvContacts_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvContacts.MouseDown

        If e.Button <> MouseButtons.Right Then
            Return
        End If

        Dim hitInfo As DataGridView.HitTestInfo =
        dgvContacts.HitTest(e.X, e.Y)

        If hitInfo.RowIndex < 0 Then
            rightClickedRow = Nothing
            Return
        End If

        rightClickedRow =
        dgvContacts.Rows(hitInfo.RowIndex)

        If Not rightClickedRow.Selected Then

            'If right-clicked row is not selected, select it and deselect others
            dgvContacts.ClearSelection()
            rightClickedRow.Selected = True

            If hitInfo.ColumnIndex >= 0 Then
                dgvContacts.CurrentCell =
                rightClickedRow.Cells(hitInfo.ColumnIndex)
            Else
                dgvContacts.CurrentCell =
                rightClickedRow.Cells("colRadioID")
            End If

        Else

        End If

        'When right-clicking a row, show the context menu only if one row is selected and not in edit mode.
        If dgvContacts.SelectedRows.Count = 1 AndAlso Not isEditMode Then

            ShowSelectedRecordDetails()

        End If

    End Sub
End Class