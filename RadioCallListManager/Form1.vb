Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text

Public Class Form1
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

    Private Sub ToolStripImport_Click(sender As Object, e As EventArgs) Handles ToolStripImport.Click
        Using dialog As New OpenFileDialog()

            dialog.Title = "Import Radio ID Contact File"
            dialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dialog.CheckFileExists = True
            dialog.Multiselect = False

            If dialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            Try
                ImportContactsCsv(dialog.FileName)

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
                'ExportForMotorolaXTSRadio(aliasLength, idLength)

            Case "Motorola APX"
                'ExportForMotorolaAPXRadio(aliasLength, idLength)

            Case "Harris RPM"
                'ExportForHarrisRPMRadio(aliasLength, idLength)

            Case Else
                txtNotes.Text =
                "The selected target format is not supported."

                txtNotes.ForeColor = Color.Firebrick

        End Select

    End Sub
End Class