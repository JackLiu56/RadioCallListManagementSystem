<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRadioSystemSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblSystemName = New System.Windows.Forms.Label()
        Me.lblSysID = New System.Windows.Forms.Label()
        Me.lblWacnID = New System.Windows.Forms.Label()
        Me.txtSystemName = New System.Windows.Forms.TextBox()
        Me.txtSystemID = New System.Windows.Forms.TextBox()
        Me.txtWacnID = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblSystemName
        '
        Me.lblSystemName.AutoSize = True
        Me.lblSystemName.Location = New System.Drawing.Point(12, 9)
        Me.lblSystemName.Name = "lblSystemName"
        Me.lblSystemName.Size = New System.Drawing.Size(72, 13)
        Me.lblSystemName.TabIndex = 0
        Me.lblSystemName.Text = "System Name"
        '
        'lblSysID
        '
        Me.lblSysID.AutoSize = True
        Me.lblSysID.Location = New System.Drawing.Point(12, 35)
        Me.lblSysID.Name = "lblSysID"
        Me.lblSysID.Size = New System.Drawing.Size(55, 13)
        Me.lblSysID.TabIndex = 1
        Me.lblSysID.Text = "System ID"
        '
        'lblWacnID
        '
        Me.lblWacnID.AutoSize = True
        Me.lblWacnID.Location = New System.Drawing.Point(12, 61)
        Me.lblWacnID.Name = "lblWacnID"
        Me.lblWacnID.Size = New System.Drawing.Size(50, 13)
        Me.lblWacnID.TabIndex = 2
        Me.lblWacnID.Text = "Wacn ID"
        '
        'txtSystemName
        '
        Me.txtSystemName.Location = New System.Drawing.Point(90, 6)
        Me.txtSystemName.Name = "txtSystemName"
        Me.txtSystemName.Size = New System.Drawing.Size(154, 20)
        Me.txtSystemName.TabIndex = 3
        '
        'txtSystemID
        '
        Me.txtSystemID.Location = New System.Drawing.Point(90, 32)
        Me.txtSystemID.Name = "txtSystemID"
        Me.txtSystemID.Size = New System.Drawing.Size(154, 20)
        Me.txtSystemID.TabIndex = 4
        '
        'txtWacnID
        '
        Me.txtWacnID.Location = New System.Drawing.Point(90, 58)
        Me.txtWacnID.Name = "txtWacnID"
        Me.txtWacnID.Size = New System.Drawing.Size(154, 20)
        Me.txtWacnID.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(169, 84)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(12, 84)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmRadioSystemSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(256, 118)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtWacnID)
        Me.Controls.Add(Me.txtSystemID)
        Me.Controls.Add(Me.txtSystemName)
        Me.Controls.Add(Me.lblWacnID)
        Me.Controls.Add(Me.lblSysID)
        Me.Controls.Add(Me.lblSystemName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmRadioSystemSettings"
        Me.Text = "Radio System Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSystemName As Label
    Friend WithEvents lblSysID As Label
    Friend WithEvents lblWacnID As Label
    Friend WithEvents txtSystemName As TextBox
    Friend WithEvents txtSystemID As TextBox
    Friend WithEvents txtWacnID As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
End Class
