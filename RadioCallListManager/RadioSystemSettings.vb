Imports System.Globalization

Public Class frmRadioSystemSettings
    Public Property UpdatedSystemName As String
    Public Property UpdatedSystemID As String
    Public Property UpdatedWacnID As String

    Private Sub frmRadioSystemSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtSystemName.Text =
            My.Settings.RadioSystemName

        txtSystemID.Text =
            My.Settings.RadioSystemID

        txtWacnID.Text =
            My.Settings.RadioWacnID

    End Sub

    Private Function IsValidHexValue(value As String, requiredLength As Integer) As Boolean

        If String.IsNullOrWhiteSpace(value) Then
            Return False
        End If

        If value.Length <> requiredLength Then
            Return False
        End If

        Dim numericValue As ULong

        Return ULong.TryParse(
        value,
        NumberStyles.HexNumber,
        CultureInfo.InvariantCulture,
        numericValue
    )

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim systemName As String =
        txtSystemName.Text.Trim()

        Dim systemID As String =
        txtSystemID.Text.Trim().ToUpperInvariant()

        Dim wacnID As String =
        txtWacnID.Text.Trim().ToUpperInvariant()

        If String.IsNullOrWhiteSpace(systemName) Then

            MessageBox.Show(
            "System Name is required.",
            "Invalid Settings",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            txtSystemName.Focus()
            Return

        End If

        'System ID is 3 bit hexadecimal, so it should be 3 characters long
        If Not IsValidHexValue(systemID, 3) Then

            MessageBox.Show(
            "System ID must contain 3 hexadecimal characters.",
            "Invalid Settings",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            txtSystemID.Focus()
            txtSystemID.SelectAll()
            Return

        End If

        'WACN ID is 5 bit hexadecimal, so it should be 5 characters long
        If Not IsValidHexValue(wacnID, 5) Then

            MessageBox.Show(
            "WACN ID must contain 5 hexadecimal characters.",
            "Invalid Settings",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            txtWacnID.Focus()
            txtWacnID.SelectAll()
            Return

        End If

        My.Settings.RadioSystemName = systemName
        My.Settings.RadioSystemID = systemID
        My.Settings.RadioWacnID = wacnID

        My.Settings.Save()

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub
End Class