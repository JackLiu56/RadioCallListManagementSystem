<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRadioCallListManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRadioCallListManager))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportRadioIDFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveRecordsAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportCallListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportValidationReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditSelectedRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteSelectedRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindDuplicatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidateRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateAliasesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CleanUnsupportedCharactersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FieldMappingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VendorFormatSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowValidationSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowRecordDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowExportLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetLayoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserGuideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFileFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TroubleshootingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutRadioCallListManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripImport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripValidate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tscbTargetFormat = New System.Windows.Forms.ToolStripComboBox()
        Me.tslTargetFormat = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbIDLength = New System.Windows.Forms.ToolStripTextBox()
        Me.tslIDLength = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbAliasLength = New System.Windows.Forms.ToolStripTextBox()
        Me.tslAliasLength = New System.Windows.Forms.ToolStripLabel()
        Me.dgvContacts = New System.Windows.Forms.DataGridView()
        Me.colRadioID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCallsign = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMDTIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRadioUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGroupID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.grpExportLog = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslWacnID = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSysID = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSysName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvExportLog = New System.Windows.Forms.DataGridView()
        Me.colExportTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExportFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExportFormat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExportRecords = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExportStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExportMessage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpRecordDetails = New System.Windows.Forms.GroupBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnApplyChanges = New System.Windows.Forms.Button()
        Me.tblRecordDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRadioID = New System.Windows.Forms.Label()
        Me.lblCallsign = New System.Windows.Forms.Label()
        Me.lblIP = New System.Windows.Forms.Label()
        Me.lblMDTIP = New System.Windows.Forms.Label()
        Me.lblRadioUser = New System.Windows.Forms.Label()
        Me.lblGroupID = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtRadioID = New System.Windows.Forms.TextBox()
        Me.txtCallSign = New System.Windows.Forms.TextBox()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtMDTIP = New System.Windows.Forms.TextBox()
        Me.txtRadioUser = New System.Windows.Forms.TextBox()
        Me.txtGroupID = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.grpValidationSummary = New System.Windows.Forms.GroupBox()
        Me.tblValidationSummary = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTotalText = New System.Windows.Forms.Label()
        Me.lblValidText = New System.Windows.Forms.Label()
        Me.lblErrorsText = New System.Windows.Forms.Label()
        Me.lblDuplicatesText = New System.Windows.Forms.Label()
        Me.lblMissingText = New System.Windows.Forms.Label()
        Me.lblLengthText = New System.Windows.Forms.Label()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblValidValue = New System.Windows.Forms.Label()
        Me.lblErrorsValue = New System.Windows.Forms.Label()
        Me.lblDuplicatesValue = New System.Windows.Forms.Label()
        Me.lblMissingValue = New System.Windows.Forms.Label()
        Me.lblLengthValue = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.grpExportLog.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvExportLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRecordDetails.SuspendLayout()
        Me.tblRecordDetails.SuspendLayout()
        Me.grpValidationSummary.SuspendLayout()
        Me.tblValidationSummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1230, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportRadioIDFileToolStripMenuItem, Me.SaveRecordsToolStripMenuItem, Me.SaveRecordsAsToolStripMenuItem, Me.ExportCallListToolStripMenuItem, Me.ExportValidationReportToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ImportRadioIDFileToolStripMenuItem
        '
        Me.ImportRadioIDFileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ImportRadioIDFileToolStripMenuItem.Name = "ImportRadioIDFileToolStripMenuItem"
        Me.ImportRadioIDFileToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ImportRadioIDFileToolStripMenuItem.Text = "Import Radio ID File"
        '
        'SaveRecordsToolStripMenuItem
        '
        Me.SaveRecordsToolStripMenuItem.Name = "SaveRecordsToolStripMenuItem"
        Me.SaveRecordsToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SaveRecordsToolStripMenuItem.Text = "Save Records"
        '
        'SaveRecordsAsToolStripMenuItem
        '
        Me.SaveRecordsAsToolStripMenuItem.Name = "SaveRecordsAsToolStripMenuItem"
        Me.SaveRecordsAsToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SaveRecordsAsToolStripMenuItem.Text = "Save Records As"
        '
        'ExportCallListToolStripMenuItem
        '
        Me.ExportCallListToolStripMenuItem.Name = "ExportCallListToolStripMenuItem"
        Me.ExportCallListToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ExportCallListToolStripMenuItem.Text = "Export Call List"
        '
        'ExportValidationReportToolStripMenuItem
        '
        Me.ExportValidationReportToolStripMenuItem.Name = "ExportValidationReportToolStripMenuItem"
        Me.ExportValidationReportToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ExportValidationReportToolStripMenuItem.Text = "Export Validation Report"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewRecordToolStripMenuItem, Me.EditSelectedRecordToolStripMenuItem, Me.DeleteSelectedRecordToolStripMenuItem, Me.ResetToolStripMenuItem, Me.FindToolStripMenuItem, Me.FindDuplicatesToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'AddNewRecordToolStripMenuItem
        '
        Me.AddNewRecordToolStripMenuItem.Name = "AddNewRecordToolStripMenuItem"
        Me.AddNewRecordToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.AddNewRecordToolStripMenuItem.Text = "Add New Record"
        '
        'EditSelectedRecordToolStripMenuItem
        '
        Me.EditSelectedRecordToolStripMenuItem.Name = "EditSelectedRecordToolStripMenuItem"
        Me.EditSelectedRecordToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.EditSelectedRecordToolStripMenuItem.Text = "Edit Selected Record"
        '
        'DeleteSelectedRecordToolStripMenuItem
        '
        Me.DeleteSelectedRecordToolStripMenuItem.Name = "DeleteSelectedRecordToolStripMenuItem"
        Me.DeleteSelectedRecordToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.DeleteSelectedRecordToolStripMenuItem.Text = "Delete Selected Record"
        '
        'ResetToolStripMenuItem
        '
        Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
        Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.ResetToolStripMenuItem.Text = "Reset"
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FindToolStripMenuItem.Text = "Find"
        '
        'FindDuplicatesToolStripMenuItem
        '
        Me.FindDuplicatesToolStripMenuItem.Name = "FindDuplicatesToolStripMenuItem"
        Me.FindDuplicatesToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FindDuplicatesToolStripMenuItem.Text = "Find Duplicates"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidateRecordsToolStripMenuItem, Me.GenerateAliasesToolStripMenuItem, Me.CleanUnsupportedCharactersToolStripMenuItem, Me.FieldMappingToolStripMenuItem, Me.VendorFormatSettingsToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'ValidateRecordsToolStripMenuItem
        '
        Me.ValidateRecordsToolStripMenuItem.Name = "ValidateRecordsToolStripMenuItem"
        Me.ValidateRecordsToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.ValidateRecordsToolStripMenuItem.Text = "Validate Records"
        '
        'GenerateAliasesToolStripMenuItem
        '
        Me.GenerateAliasesToolStripMenuItem.Name = "GenerateAliasesToolStripMenuItem"
        Me.GenerateAliasesToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.GenerateAliasesToolStripMenuItem.Text = "Generate Aliases"
        '
        'CleanUnsupportedCharactersToolStripMenuItem
        '
        Me.CleanUnsupportedCharactersToolStripMenuItem.Name = "CleanUnsupportedCharactersToolStripMenuItem"
        Me.CleanUnsupportedCharactersToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.CleanUnsupportedCharactersToolStripMenuItem.Text = "Clean Unsupported Characters"
        '
        'FieldMappingToolStripMenuItem
        '
        Me.FieldMappingToolStripMenuItem.Name = "FieldMappingToolStripMenuItem"
        Me.FieldMappingToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.FieldMappingToolStripMenuItem.Text = "Field Mapping"
        '
        'VendorFormatSettingsToolStripMenuItem
        '
        Me.VendorFormatSettingsToolStripMenuItem.Name = "VendorFormatSettingsToolStripMenuItem"
        Me.VendorFormatSettingsToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.VendorFormatSettingsToolStripMenuItem.Text = "Vendor Format Settings"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowValidationSummaryToolStripMenuItem, Me.ShowRecordDetailsToolStripMenuItem, Me.ShowExportLogToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ResetLayoutToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ShowValidationSummaryToolStripMenuItem
        '
        Me.ShowValidationSummaryToolStripMenuItem.Name = "ShowValidationSummaryToolStripMenuItem"
        Me.ShowValidationSummaryToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ShowValidationSummaryToolStripMenuItem.Text = "Show Validation Summary"
        '
        'ShowRecordDetailsToolStripMenuItem
        '
        Me.ShowRecordDetailsToolStripMenuItem.Name = "ShowRecordDetailsToolStripMenuItem"
        Me.ShowRecordDetailsToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ShowRecordDetailsToolStripMenuItem.Text = "Show Record Details"
        '
        'ShowExportLogToolStripMenuItem
        '
        Me.ShowExportLogToolStripMenuItem.Name = "ShowExportLogToolStripMenuItem"
        Me.ShowExportLogToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ShowExportLogToolStripMenuItem.Text = "Show Export Log"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ResetLayoutToolStripMenuItem
        '
        Me.ResetLayoutToolStripMenuItem.Name = "ResetLayoutToolStripMenuItem"
        Me.ResetLayoutToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ResetLayoutToolStripMenuItem.Text = "Reset Layout"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserGuideToolStripMenuItem, Me.ImportFileFormatToolStripMenuItem, Me.TroubleshootingToolStripMenuItem, Me.AboutRadioCallListManagerToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'UserGuideToolStripMenuItem
        '
        Me.UserGuideToolStripMenuItem.Name = "UserGuideToolStripMenuItem"
        Me.UserGuideToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.UserGuideToolStripMenuItem.Text = "User Guide"
        '
        'ImportFileFormatToolStripMenuItem
        '
        Me.ImportFileFormatToolStripMenuItem.Name = "ImportFileFormatToolStripMenuItem"
        Me.ImportFileFormatToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.ImportFileFormatToolStripMenuItem.Text = "Import File Format"
        '
        'TroubleshootingToolStripMenuItem
        '
        Me.TroubleshootingToolStripMenuItem.Name = "TroubleshootingToolStripMenuItem"
        Me.TroubleshootingToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.TroubleshootingToolStripMenuItem.Text = "Troubleshooting"
        '
        'AboutRadioCallListManagerToolStripMenuItem
        '
        Me.AboutRadioCallListManagerToolStripMenuItem.Name = "AboutRadioCallListManagerToolStripMenuItem"
        Me.AboutRadioCallListManagerToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.AboutRadioCallListManagerToolStripMenuItem.Text = "About RadioCallListManager"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripImport, Me.ToolStripSeparator1, Me.ToolStripValidate, Me.ToolStripSeparator5, Me.ToolStripExport, Me.ToolStripSeparator4, Me.ToolStripSave, Me.ToolStripSeparator3, Me.ToolStripRefresh, Me.tscbTargetFormat, Me.tslTargetFormat, Me.ToolStripSeparator2, Me.tsbIDLength, Me.tslIDLength, Me.ToolStripSeparator6, Me.tsbAliasLength, Me.tslAliasLength})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1230, 54)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripImport
        '
        Me.ToolStripImport.Image = CType(resources.GetObject("ToolStripImport.Image"), System.Drawing.Image)
        Me.ToolStripImport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripImport.Name = "ToolStripImport"
        Me.ToolStripImport.Size = New System.Drawing.Size(115, 51)
        Me.ToolStripImport.Text = "Import Radio ID File"
        Me.ToolStripImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 54)
        '
        'ToolStripValidate
        '
        Me.ToolStripValidate.Image = CType(resources.GetObject("ToolStripValidate.Image"), System.Drawing.Image)
        Me.ToolStripValidate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripValidate.Name = "ToolStripValidate"
        Me.ToolStripValidate.Size = New System.Drawing.Size(97, 51)
        Me.ToolStripValidate.Text = "Validate Records"
        Me.ToolStripValidate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 54)
        '
        'ToolStripExport
        '
        Me.ToolStripExport.Image = CType(resources.GetObject("ToolStripExport.Image"), System.Drawing.Image)
        Me.ToolStripExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripExport.Name = "ToolStripExport"
        Me.ToolStripExport.Size = New System.Drawing.Size(88, 51)
        Me.ToolStripExport.Text = "Export Call List"
        Me.ToolStripExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 54)
        '
        'ToolStripSave
        '
        Me.ToolStripSave.Image = CType(resources.GetObject("ToolStripSave.Image"), System.Drawing.Image)
        Me.ToolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSave.Name = "ToolStripSave"
        Me.ToolStripSave.Size = New System.Drawing.Size(80, 51)
        Me.ToolStripSave.Text = "Save Records"
        Me.ToolStripSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 54)
        '
        'ToolStripRefresh
        '
        Me.ToolStripRefresh.Image = CType(resources.GetObject("ToolStripRefresh.Image"), System.Drawing.Image)
        Me.ToolStripRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripRefresh.Name = "ToolStripRefresh"
        Me.ToolStripRefresh.Size = New System.Drawing.Size(50, 51)
        Me.ToolStripRefresh.Text = "Refresh"
        Me.ToolStripRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tscbTargetFormat
        '
        Me.tscbTargetFormat.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tscbTargetFormat.Name = "tscbTargetFormat"
        Me.tscbTargetFormat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tscbTargetFormat.Size = New System.Drawing.Size(121, 54)
        Me.tscbTargetFormat.Tag = ""
        '
        'tslTargetFormat
        '
        Me.tslTargetFormat.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslTargetFormat.Name = "tslTargetFormat"
        Me.tslTargetFormat.Size = New System.Drawing.Size(84, 51)
        Me.tslTargetFormat.Text = "Target Format:"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 54)
        '
        'tsbIDLength
        '
        Me.tsbIDLength.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbIDLength.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsbIDLength.Name = "tsbIDLength"
        Me.tsbIDLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tsbIDLength.Size = New System.Drawing.Size(121, 54)
        Me.tsbIDLength.Tag = ""
        '
        'tslIDLength
        '
        Me.tslIDLength.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslIDLength.Name = "tslIDLength"
        Me.tslIDLength.Size = New System.Drawing.Size(61, 51)
        Me.tslIDLength.Text = "ID Length:"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 54)
        '
        'tsbAliasLength
        '
        Me.tsbAliasLength.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbAliasLength.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsbAliasLength.Name = "tsbAliasLength"
        Me.tsbAliasLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tsbAliasLength.Size = New System.Drawing.Size(121, 54)
        Me.tsbAliasLength.Tag = ""
        '
        'tslAliasLength
        '
        Me.tslAliasLength.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslAliasLength.Name = "tslAliasLength"
        Me.tslAliasLength.Size = New System.Drawing.Size(75, 51)
        Me.tslAliasLength.Text = "Alias Length:"
        '
        'dgvContacts
        '
        Me.dgvContacts.AllowUserToAddRows = False
        Me.dgvContacts.AllowUserToDeleteRows = False
        Me.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRadioID, Me.colCallsign, Me.colIP, Me.ColMDTIP, Me.colRadioUser, Me.colGroupID})
        Me.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvContacts.Location = New System.Drawing.Point(0, 0)
        Me.dgvContacts.Name = "dgvContacts"
        Me.dgvContacts.ReadOnly = True
        Me.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvContacts.Size = New System.Drawing.Size(696, 383)
        Me.dgvContacts.TabIndex = 2
        '
        'colRadioID
        '
        Me.colRadioID.HeaderText = "Radio ID"
        Me.colRadioID.Name = "colRadioID"
        Me.colRadioID.ReadOnly = True
        '
        'colCallsign
        '
        Me.colCallsign.HeaderText = "Callsign"
        Me.colCallsign.Name = "colCallsign"
        Me.colCallsign.ReadOnly = True
        '
        'colIP
        '
        Me.colIP.HeaderText = "IP"
        Me.colIP.Name = "colIP"
        Me.colIP.ReadOnly = True
        '
        'ColMDTIP
        '
        Me.ColMDTIP.HeaderText = "MDT IP"
        Me.ColMDTIP.Name = "ColMDTIP"
        Me.ColMDTIP.ReadOnly = True
        '
        'colRadioUser
        '
        Me.colRadioUser.HeaderText = "Radio User"
        Me.colRadioUser.Name = "colRadioUser"
        Me.colRadioUser.ReadOnly = True
        '
        'colGroupID
        '
        Me.colGroupID.HeaderText = "Group ID"
        Me.colGroupID.Name = "colGroupID"
        Me.colGroupID.ReadOnly = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 78)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grpRecordDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.grpValidationSummary)
        Me.SplitContainer1.Size = New System.Drawing.Size(1230, 593)
        Me.SplitContainer1.SplitterDistance = 696
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvContacts)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpExportLog)
        Me.SplitContainer2.Size = New System.Drawing.Size(696, 593)
        Me.SplitContainer2.SplitterDistance = 383
        Me.SplitContainer2.TabIndex = 4
        '
        'grpExportLog
        '
        Me.grpExportLog.Controls.Add(Me.StatusStrip1)
        Me.grpExportLog.Controls.Add(Me.dgvExportLog)
        Me.grpExportLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpExportLog.Location = New System.Drawing.Point(0, 0)
        Me.grpExportLog.Name = "grpExportLog"
        Me.grpExportLog.Size = New System.Drawing.Size(696, 206)
        Me.grpExportLog.TabIndex = 0
        Me.grpExportLog.TabStop = False
        Me.grpExportLog.Text = "Export Log"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.tsslWacnID, Me.ToolStripStatusLabel2, Me.tsslSysID, Me.ToolStripStatusLabel3, Me.tsslSysName})
        Me.StatusStrip1.Location = New System.Drawing.Point(3, 181)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(690, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(51, 17)
        Me.ToolStripStatusLabel1.Text = "Wacn ID"
        '
        'tsslWacnID
        '
        Me.tsslWacnID.Name = "tsslWacnID"
        Me.tsslWacnID.Size = New System.Drawing.Size(48, 17)
        Me.tsslWacnID.Text = "WacnID"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(59, 17)
        Me.ToolStripStatusLabel2.Text = "System ID"
        '
        'tsslSysID
        '
        Me.tsslSysID.Name = "tsslSysID"
        Me.tsslSysID.Size = New System.Drawing.Size(35, 17)
        Me.tsslSysID.Text = "SysID"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel3.Text = "System Name"
        '
        'tsslSysName
        '
        Me.tsslSysName.Name = "tsslSysName"
        Me.tsslSysName.Size = New System.Drawing.Size(56, 17)
        Me.tsslSysName.Text = "SysName"
        '
        'dgvExportLog
        '
        Me.dgvExportLog.AllowUserToAddRows = False
        Me.dgvExportLog.AllowUserToDeleteRows = False
        Me.dgvExportLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExportLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colExportTime, Me.colExportFile, Me.colExportFormat, Me.colExportRecords, Me.colExportStatus, Me.colExportMessage})
        Me.dgvExportLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvExportLog.Location = New System.Drawing.Point(3, 16)
        Me.dgvExportLog.MultiSelect = False
        Me.dgvExportLog.Name = "dgvExportLog"
        Me.dgvExportLog.ReadOnly = True
        Me.dgvExportLog.RowHeadersVisible = False
        Me.dgvExportLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvExportLog.Size = New System.Drawing.Size(690, 187)
        Me.dgvExportLog.TabIndex = 0
        '
        'colExportTime
        '
        Me.colExportTime.HeaderText = "Time"
        Me.colExportTime.Name = "colExportTime"
        Me.colExportTime.ReadOnly = True
        Me.colExportTime.Width = 145
        '
        'colExportFile
        '
        Me.colExportFile.HeaderText = "File Name"
        Me.colExportFile.Name = "colExportFile"
        Me.colExportFile.ReadOnly = True
        Me.colExportFile.Width = 180
        '
        'colExportFormat
        '
        Me.colExportFormat.HeaderText = "Format"
        Me.colExportFormat.Name = "colExportFormat"
        Me.colExportFormat.ReadOnly = True
        '
        'colExportRecords
        '
        Me.colExportRecords.HeaderText = "Records"
        Me.colExportRecords.Name = "colExportRecords"
        Me.colExportRecords.ReadOnly = True
        Me.colExportRecords.Width = 70
        '
        'colExportStatus
        '
        Me.colExportStatus.HeaderText = "Status"
        Me.colExportStatus.Name = "colExportStatus"
        Me.colExportStatus.ReadOnly = True
        Me.colExportStatus.Width = 80
        '
        'colExportMessage
        '
        Me.colExportMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colExportMessage.HeaderText = "Message / Output Path"
        Me.colExportMessage.Name = "colExportMessage"
        Me.colExportMessage.ReadOnly = True
        '
        'grpRecordDetails
        '
        Me.grpRecordDetails.Controls.Add(Me.btnReset)
        Me.grpRecordDetails.Controls.Add(Me.btnApplyChanges)
        Me.grpRecordDetails.Controls.Add(Me.tblRecordDetails)
        Me.grpRecordDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpRecordDetails.Location = New System.Drawing.Point(0, 240)
        Me.grpRecordDetails.Name = "grpRecordDetails"
        Me.grpRecordDetails.Size = New System.Drawing.Size(530, 353)
        Me.grpRecordDetails.TabIndex = 1
        Me.grpRecordDetails.TabStop = False
        Me.grpRecordDetails.Text = "Selected Record Details"
        '
        'btnReset
        '
        Me.btnReset.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnReset.Location = New System.Drawing.Point(367, 312)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(160, 38)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnApplyChanges
        '
        Me.btnApplyChanges.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnApplyChanges.Location = New System.Drawing.Point(3, 312)
        Me.btnApplyChanges.Name = "btnApplyChanges"
        Me.btnApplyChanges.Size = New System.Drawing.Size(160, 38)
        Me.btnApplyChanges.TabIndex = 1
        Me.btnApplyChanges.Text = "Apply Changes"
        Me.btnApplyChanges.UseVisualStyleBackColor = True
        '
        'tblRecordDetails
        '
        Me.tblRecordDetails.ColumnCount = 2
        Me.tblRecordDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.tblRecordDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.tblRecordDetails.Controls.Add(Me.lblRadioID, 0, 0)
        Me.tblRecordDetails.Controls.Add(Me.lblCallsign, 0, 1)
        Me.tblRecordDetails.Controls.Add(Me.lblIP, 0, 2)
        Me.tblRecordDetails.Controls.Add(Me.lblMDTIP, 0, 3)
        Me.tblRecordDetails.Controls.Add(Me.lblRadioUser, 0, 4)
        Me.tblRecordDetails.Controls.Add(Me.lblGroupID, 0, 5)
        Me.tblRecordDetails.Controls.Add(Me.lblNotes, 0, 6)
        Me.tblRecordDetails.Controls.Add(Me.txtRadioID, 1, 0)
        Me.tblRecordDetails.Controls.Add(Me.txtCallSign, 1, 1)
        Me.tblRecordDetails.Controls.Add(Me.txtIP, 1, 2)
        Me.tblRecordDetails.Controls.Add(Me.txtMDTIP, 1, 3)
        Me.tblRecordDetails.Controls.Add(Me.txtRadioUser, 1, 4)
        Me.tblRecordDetails.Controls.Add(Me.txtGroupID, 1, 5)
        Me.tblRecordDetails.Controls.Add(Me.txtNotes, 1, 6)
        Me.tblRecordDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.tblRecordDetails.Location = New System.Drawing.Point(3, 16)
        Me.tblRecordDetails.Name = "tblRecordDetails"
        Me.tblRecordDetails.Padding = New System.Windows.Forms.Padding(10)
        Me.tblRecordDetails.RowCount = 7
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tblRecordDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRecordDetails.Size = New System.Drawing.Size(524, 296)
        Me.tblRecordDetails.TabIndex = 0
        '
        'lblRadioID
        '
        Me.lblRadioID.AutoSize = True
        Me.lblRadioID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRadioID.Location = New System.Drawing.Point(13, 10)
        Me.lblRadioID.Name = "lblRadioID"
        Me.lblRadioID.Size = New System.Drawing.Size(170, 34)
        Me.lblRadioID.TabIndex = 0
        Me.lblRadioID.Text = "Radio ID:"
        Me.lblRadioID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCallsign
        '
        Me.lblCallsign.AutoSize = True
        Me.lblCallsign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCallsign.Location = New System.Drawing.Point(13, 44)
        Me.lblCallsign.Name = "lblCallsign"
        Me.lblCallsign.Size = New System.Drawing.Size(170, 34)
        Me.lblCallsign.TabIndex = 1
        Me.lblCallsign.Text = "Callsign:"
        Me.lblCallsign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIP
        '
        Me.lblIP.AutoSize = True
        Me.lblIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIP.Location = New System.Drawing.Point(13, 78)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(170, 34)
        Me.lblIP.TabIndex = 2
        Me.lblIP.Text = "IP:"
        Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMDTIP
        '
        Me.lblMDTIP.AutoSize = True
        Me.lblMDTIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMDTIP.Location = New System.Drawing.Point(13, 112)
        Me.lblMDTIP.Name = "lblMDTIP"
        Me.lblMDTIP.Size = New System.Drawing.Size(170, 34)
        Me.lblMDTIP.TabIndex = 3
        Me.lblMDTIP.Text = "MDT IP:"
        Me.lblMDTIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRadioUser
        '
        Me.lblRadioUser.AutoSize = True
        Me.lblRadioUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRadioUser.Location = New System.Drawing.Point(13, 146)
        Me.lblRadioUser.Name = "lblRadioUser"
        Me.lblRadioUser.Size = New System.Drawing.Size(170, 34)
        Me.lblRadioUser.TabIndex = 5
        Me.lblRadioUser.Text = "Radio User:"
        Me.lblRadioUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGroupID
        '
        Me.lblGroupID.AutoSize = True
        Me.lblGroupID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGroupID.Location = New System.Drawing.Point(13, 180)
        Me.lblGroupID.Name = "lblGroupID"
        Me.lblGroupID.Size = New System.Drawing.Size(170, 34)
        Me.lblGroupID.TabIndex = 6
        Me.lblGroupID.Text = "Group ID:"
        Me.lblGroupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNotes.Location = New System.Drawing.Point(13, 214)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(170, 72)
        Me.lblNotes.TabIndex = 7
        Me.lblNotes.Text = "Notes:"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRadioID
        '
        Me.txtRadioID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRadioID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRadioID.Location = New System.Drawing.Point(189, 13)
        Me.txtRadioID.Name = "txtRadioID"
        Me.txtRadioID.ReadOnly = True
        Me.txtRadioID.Size = New System.Drawing.Size(322, 20)
        Me.txtRadioID.TabIndex = 8
        '
        'txtCallSign
        '
        Me.txtCallSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCallSign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCallSign.Location = New System.Drawing.Point(189, 47)
        Me.txtCallSign.Name = "txtCallSign"
        Me.txtCallSign.ReadOnly = True
        Me.txtCallSign.Size = New System.Drawing.Size(322, 20)
        Me.txtCallSign.TabIndex = 9
        '
        'txtIP
        '
        Me.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtIP.Location = New System.Drawing.Point(189, 81)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.ReadOnly = True
        Me.txtIP.Size = New System.Drawing.Size(322, 20)
        Me.txtIP.TabIndex = 10
        '
        'txtMDTIP
        '
        Me.txtMDTIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMDTIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMDTIP.Location = New System.Drawing.Point(189, 115)
        Me.txtMDTIP.Name = "txtMDTIP"
        Me.txtMDTIP.ReadOnly = True
        Me.txtMDTIP.Size = New System.Drawing.Size(322, 20)
        Me.txtMDTIP.TabIndex = 11
        '
        'txtRadioUser
        '
        Me.txtRadioUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRadioUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRadioUser.Location = New System.Drawing.Point(189, 149)
        Me.txtRadioUser.Name = "txtRadioUser"
        Me.txtRadioUser.ReadOnly = True
        Me.txtRadioUser.Size = New System.Drawing.Size(322, 20)
        Me.txtRadioUser.TabIndex = 12
        '
        'txtGroupID
        '
        Me.txtGroupID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGroupID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGroupID.Location = New System.Drawing.Point(189, 183)
        Me.txtGroupID.Name = "txtGroupID"
        Me.txtGroupID.ReadOnly = True
        Me.txtGroupID.Size = New System.Drawing.Size(322, 20)
        Me.txtGroupID.TabIndex = 13
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNotes.Location = New System.Drawing.Point(189, 217)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ReadOnly = True
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(322, 66)
        Me.txtNotes.TabIndex = 14
        '
        'grpValidationSummary
        '
        Me.grpValidationSummary.Controls.Add(Me.tblValidationSummary)
        Me.grpValidationSummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpValidationSummary.Location = New System.Drawing.Point(0, 0)
        Me.grpValidationSummary.Name = "grpValidationSummary"
        Me.grpValidationSummary.Size = New System.Drawing.Size(530, 240)
        Me.grpValidationSummary.TabIndex = 0
        Me.grpValidationSummary.TabStop = False
        Me.grpValidationSummary.Text = "Validation Summary"
        '
        'tblValidationSummary
        '
        Me.tblValidationSummary.ColumnCount = 2
        Me.tblValidationSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tblValidationSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblValidationSummary.Controls.Add(Me.lblTotalText, 0, 0)
        Me.tblValidationSummary.Controls.Add(Me.lblValidText, 0, 1)
        Me.tblValidationSummary.Controls.Add(Me.lblErrorsText, 0, 2)
        Me.tblValidationSummary.Controls.Add(Me.lblDuplicatesText, 0, 3)
        Me.tblValidationSummary.Controls.Add(Me.lblMissingText, 0, 4)
        Me.tblValidationSummary.Controls.Add(Me.lblLengthText, 0, 5)
        Me.tblValidationSummary.Controls.Add(Me.lblTotalValue, 1, 0)
        Me.tblValidationSummary.Controls.Add(Me.lblValidValue, 1, 1)
        Me.tblValidationSummary.Controls.Add(Me.lblErrorsValue, 1, 2)
        Me.tblValidationSummary.Controls.Add(Me.lblDuplicatesValue, 1, 3)
        Me.tblValidationSummary.Controls.Add(Me.lblMissingValue, 1, 4)
        Me.tblValidationSummary.Controls.Add(Me.lblLengthValue, 1, 5)
        Me.tblValidationSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblValidationSummary.Location = New System.Drawing.Point(3, 16)
        Me.tblValidationSummary.Name = "tblValidationSummary"
        Me.tblValidationSummary.Padding = New System.Windows.Forms.Padding(10)
        Me.tblValidationSummary.RowCount = 6
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblValidationSummary.Size = New System.Drawing.Size(524, 221)
        Me.tblValidationSummary.TabIndex = 0
        '
        'lblTotalText
        '
        Me.lblTotalText.AutoSize = True
        Me.lblTotalText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalText.Location = New System.Drawing.Point(13, 10)
        Me.lblTotalText.Name = "lblTotalText"
        Me.lblTotalText.Size = New System.Drawing.Size(346, 33)
        Me.lblTotalText.TabIndex = 0
        Me.lblTotalText.Text = "Total Records:"
        Me.lblTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblValidText
        '
        Me.lblValidText.AutoSize = True
        Me.lblValidText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblValidText.Location = New System.Drawing.Point(13, 43)
        Me.lblValidText.Name = "lblValidText"
        Me.lblValidText.Size = New System.Drawing.Size(346, 33)
        Me.lblValidText.TabIndex = 1
        Me.lblValidText.Text = "Valid:"
        Me.lblValidText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblErrorsText
        '
        Me.lblErrorsText.AutoSize = True
        Me.lblErrorsText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblErrorsText.Location = New System.Drawing.Point(13, 76)
        Me.lblErrorsText.Name = "lblErrorsText"
        Me.lblErrorsText.Size = New System.Drawing.Size(346, 33)
        Me.lblErrorsText.TabIndex = 2
        Me.lblErrorsText.Text = "Errors:"
        Me.lblErrorsText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDuplicatesText
        '
        Me.lblDuplicatesText.AutoSize = True
        Me.lblDuplicatesText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDuplicatesText.Location = New System.Drawing.Point(13, 109)
        Me.lblDuplicatesText.Name = "lblDuplicatesText"
        Me.lblDuplicatesText.Size = New System.Drawing.Size(346, 33)
        Me.lblDuplicatesText.TabIndex = 3
        Me.lblDuplicatesText.Text = "Duplicates:"
        Me.lblDuplicatesText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMissingText
        '
        Me.lblMissingText.AutoSize = True
        Me.lblMissingText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMissingText.Location = New System.Drawing.Point(13, 142)
        Me.lblMissingText.Name = "lblMissingText"
        Me.lblMissingText.Size = New System.Drawing.Size(346, 33)
        Me.lblMissingText.TabIndex = 4
        Me.lblMissingText.Text = "Missing IDs:"
        Me.lblMissingText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLengthText
        '
        Me.lblLengthText.AutoSize = True
        Me.lblLengthText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLengthText.Location = New System.Drawing.Point(13, 175)
        Me.lblLengthText.Name = "lblLengthText"
        Me.lblLengthText.Size = New System.Drawing.Size(346, 36)
        Me.lblLengthText.TabIndex = 5
        Me.lblLengthText.Text = "Length Violations:"
        Me.lblLengthText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalValue
        '
        Me.lblTotalValue.AutoSize = True
        Me.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalValue.Location = New System.Drawing.Point(365, 10)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(146, 33)
        Me.lblTotalValue.TabIndex = 6
        Me.lblTotalValue.Text = "0"
        Me.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblValidValue
        '
        Me.lblValidValue.AutoSize = True
        Me.lblValidValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblValidValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValidValue.Location = New System.Drawing.Point(365, 43)
        Me.lblValidValue.Name = "lblValidValue"
        Me.lblValidValue.Size = New System.Drawing.Size(146, 33)
        Me.lblValidValue.TabIndex = 7
        Me.lblValidValue.Text = "0"
        Me.lblValidValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblErrorsValue
        '
        Me.lblErrorsValue.AutoSize = True
        Me.lblErrorsValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblErrorsValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorsValue.Location = New System.Drawing.Point(365, 76)
        Me.lblErrorsValue.Name = "lblErrorsValue"
        Me.lblErrorsValue.Size = New System.Drawing.Size(146, 33)
        Me.lblErrorsValue.TabIndex = 8
        Me.lblErrorsValue.Text = "0"
        Me.lblErrorsValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDuplicatesValue
        '
        Me.lblDuplicatesValue.AutoSize = True
        Me.lblDuplicatesValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDuplicatesValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDuplicatesValue.Location = New System.Drawing.Point(365, 109)
        Me.lblDuplicatesValue.Name = "lblDuplicatesValue"
        Me.lblDuplicatesValue.Size = New System.Drawing.Size(146, 33)
        Me.lblDuplicatesValue.TabIndex = 9
        Me.lblDuplicatesValue.Text = "0"
        Me.lblDuplicatesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMissingValue
        '
        Me.lblMissingValue.AutoSize = True
        Me.lblMissingValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMissingValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMissingValue.Location = New System.Drawing.Point(365, 142)
        Me.lblMissingValue.Name = "lblMissingValue"
        Me.lblMissingValue.Size = New System.Drawing.Size(146, 33)
        Me.lblMissingValue.TabIndex = 10
        Me.lblMissingValue.Text = "0"
        Me.lblMissingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLengthValue
        '
        Me.lblLengthValue.AutoSize = True
        Me.lblLengthValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLengthValue.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLengthValue.Location = New System.Drawing.Point(365, 175)
        Me.lblLengthValue.Name = "lblLengthValue"
        Me.lblLengthValue.Size = New System.Drawing.Size(146, 36)
        Me.lblLengthValue.TabIndex = 11
        Me.lblLengthValue.Text = "0"
        Me.lblLengthValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmRadioCallListManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1230, 671)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmRadioCallListManager"
        Me.Text = "Radio Call List Manager"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.grpExportLog.ResumeLayout(False)
        Me.grpExportLog.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvExportLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRecordDetails.ResumeLayout(False)
        Me.tblRecordDetails.ResumeLayout(False)
        Me.tblRecordDetails.PerformLayout()
        Me.grpValidationSummary.ResumeLayout(False)
        Me.tblValidationSummary.ResumeLayout(False)
        Me.tblValidationSummary.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportRadioIDFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportCallListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportValidationReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddNewRecordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditSelectedRecordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteSelectedRecordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindDuplicatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ValidateRecordsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GenerateAliasesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CleanUnsupportedCharactersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FieldMappingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VendorFormatSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowValidationSummaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowRecordDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowExportLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResetLayoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserGuideToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportFileFormatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TroubleshootingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutRadioCallListManagerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripImport As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripValidate As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripExport As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSave As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripRefresh As ToolStripButton
    Friend WithEvents tscbTargetFormat As ToolStripComboBox
    Friend WithEvents tslTargetFormat As ToolStripLabel
    Friend WithEvents dgvContacts As DataGridView
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents grpValidationSummary As GroupBox
    Friend WithEvents tblValidationSummary As TableLayoutPanel
    Friend WithEvents lblTotalText As Label
    Friend WithEvents lblValidText As Label
    Friend WithEvents lblErrorsText As Label
    Friend WithEvents lblDuplicatesText As Label
    Friend WithEvents lblMissingText As Label
    Friend WithEvents lblLengthText As Label
    Friend WithEvents lblTotalValue As Label
    Friend WithEvents lblValidValue As Label
    Friend WithEvents lblErrorsValue As Label
    Friend WithEvents lblDuplicatesValue As Label
    Friend WithEvents lblMissingValue As Label
    Friend WithEvents lblLengthValue As Label
    Friend WithEvents grpRecordDetails As GroupBox
    Friend WithEvents tblRecordDetails As TableLayoutPanel
    Friend WithEvents lblRadioID As Label
    Friend WithEvents lblCallsign As Label
    Friend WithEvents lblIP As Label
    Friend WithEvents lblMDTIP As Label
    Friend WithEvents lblRadioUser As Label
    Friend WithEvents lblGroupID As Label
    Friend WithEvents txtRadioID As TextBox
    Friend WithEvents txtCallSign As TextBox
    Friend WithEvents txtIP As TextBox
    Friend WithEvents txtMDTIP As TextBox
    Friend WithEvents txtRadioUser As TextBox
    Friend WithEvents txtGroupID As TextBox
    Friend WithEvents grpExportLog As GroupBox
    Friend WithEvents dgvExportLog As DataGridView
    Friend WithEvents colExportTime As DataGridViewTextBoxColumn
    Friend WithEvents colExportFile As DataGridViewTextBoxColumn
    Friend WithEvents colExportFormat As DataGridViewTextBoxColumn
    Friend WithEvents colExportRecords As DataGridViewTextBoxColumn
    Friend WithEvents colExportStatus As DataGridViewTextBoxColumn
    Friend WithEvents colExportMessage As DataGridViewTextBoxColumn
    Friend WithEvents lblNotes As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents btnReset As Button
    Friend WithEvents btnApplyChanges As Button
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tslIDLength As ToolStripLabel
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents tslAliasLength As ToolStripLabel
    Friend WithEvents tsbIDLength As ToolStripTextBox
    Friend WithEvents tsbAliasLength As ToolStripTextBox
    Friend WithEvents colRadioID As DataGridViewTextBoxColumn
    Friend WithEvents colCallsign As DataGridViewTextBoxColumn
    Friend WithEvents colIP As DataGridViewTextBoxColumn
    Friend WithEvents ColMDTIP As DataGridViewTextBoxColumn
    Friend WithEvents colRadioUser As DataGridViewTextBoxColumn
    Friend WithEvents colGroupID As DataGridViewTextBoxColumn
    Friend WithEvents ResetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents tsslWacnID As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents tsslSysID As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents tsslSysName As ToolStripStatusLabel
    Friend WithEvents SaveRecordsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveRecordsAsToolStripMenuItem As ToolStripMenuItem
End Class
