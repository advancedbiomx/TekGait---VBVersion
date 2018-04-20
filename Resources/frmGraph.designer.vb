<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGraph
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGraph))
        Me.MenuStrip_Main = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrintForm = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrintReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCopyScreen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuScaleYAxis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScaleXAxis = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuColors = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorBoth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorBackground = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorGridline = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColorHarmonic = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuColorDefault = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuChangeToMetricUnits = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoMGraphs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplacement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVelocity = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuForceBW = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuHarmonics = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEnergy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPower = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWork = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuForceAverage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuForceAllSteps = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSpringConstant = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuStatistics = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoPGraphs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoPDisplacementPosteriorToAnterior = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoPDisplacementLateralToMedial = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCoPVelocityPosteriorToAnterior = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoPVelocityLateralToMedial = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCoPAccelerationPosteriorToAnterior = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCoPAccelerationLateralToMedial = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCompareGraphs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGaitIndices = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWindow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuContents = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlGraph = New System.Windows.Forms.Panel()
        Me.butSuperimpose = New System.Windows.Forms.Button()
        Me.gbLegend = New System.Windows.Forms.GroupBox()
        Me.lblDoubleSupport = New System.Windows.Forms.Label()
        Me.lblLegendRight = New System.Windows.Forms.Label()
        Me.lblLegendLeft = New System.Windows.Forms.Label()
        Me.butChangeToAngleFormula = New System.Windows.Forms.Button()
        Me.gbxScale = New System.Windows.Forms.GroupBox()
        Me.butUpIncrement = New System.Windows.Forms.Button()
        Me.butDownIncrement = New System.Windows.Forms.Button()
        Me.txtScaleValue = New System.Windows.Forms.TextBox()
        Me.lblScaleUnits = New System.Windows.Forms.Label()
        Me.butScaleCancel = New System.Windows.Forms.Button()
        Me.butScaleOK = New System.Windows.Forms.Button()
        Me.gbxPickHarmonics = New System.Windows.Forms.GroupBox()
        Me.gbxHarmGraphType = New System.Windows.Forms.GroupBox()
        Me.radbutHarmWorkGraph = New System.Windows.Forms.RadioButton()
        Me.radbutHarmPowerGraph = New System.Windows.Forms.RadioButton()
        Me.radbutHarmForceGraph = New System.Windows.Forms.RadioButton()
        Me.radbutHarmVelGraph = New System.Windows.Forms.RadioButton()
        Me.radbutHarmDisplGraph = New System.Windows.Forms.RadioButton()
        Me.gbxHarmSumOrDiffOrAmpOrEq = New System.Windows.Forms.GroupBox()
        Me.radbutEquation = New System.Windows.Forms.RadioButton()
        Me.radbutAmplitudes = New System.Windows.Forms.RadioButton()
        Me.radbutDifference = New System.Windows.Forms.RadioButton()
        Me.radbutSum = New System.Windows.Forms.RadioButton()
        Me.butCancelHarmonics = New System.Windows.Forms.Button()
        Me.lblHarmonicPickInstructions = New System.Windows.Forms.Label()
        Me.gbxHarmonicEasy = New System.Windows.Forms.GroupBox()
        Me.radbutOtherHarm = New System.Windows.Forms.RadioButton()
        Me.radbutOddHarm = New System.Windows.Forms.RadioButton()
        Me.radbutEvenHarm = New System.Windows.Forms.RadioButton()
        Me.radbutPureHarm = New System.Windows.Forms.RadioButton()
        Me.listCheckedHarmonicBoxes = New System.Windows.Forms.CheckedListBox()
        Me.butOKHarmonics = New System.Windows.Forms.Button()
        Me.gbxStatistics = New System.Windows.Forms.GroupBox()
        Me.butStatisticsExit = New System.Windows.Forms.Button()
        Me.pnlStatisticsLeft = New System.Windows.Forms.GroupBox()
        Me.lblTimeInRightSingleSupport = New System.Windows.Forms.Label()
        Me.lblTimeInRightDoubleSupport = New System.Windows.Forms.Label()
        Me.lblTimeInLeftSingleSupport = New System.Windows.Forms.Label()
        Me.lblTimeInLeftDoubleSupport = New System.Windows.Forms.Label()
        Me.lblBodyMass = New System.Windows.Forms.Label()
        Me.lblCadence = New System.Windows.Forms.Label()
        Me.lblNumberOfCompleteStrides = New System.Windows.Forms.Label()
        Me.lblExamDate = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.lblGraphTitle = New System.Windows.Forms.Label()
        Me.lblYLabels = New System.Windows.Forms.Label()
        Me.lblXLabels = New System.Windows.Forms.Label()
        Me.lblMaximumYValue = New System.Windows.Forms.Label()
        Me.lblFullFileNameR = New System.Windows.Forms.Label()
        Me.lbl_TimePerPercent = New System.Windows.Forms.Label()
        Me.lbl_YUnitsPerPixel = New System.Windows.Forms.Label()
        Me.lblOneYGridlineValue = New System.Windows.Forms.Label()
        Me.lblMaximumXValue = New System.Windows.Forms.Label()
        Me.lblScaleWhichAxis = New System.Windows.Forms.Label()
        Me.lblWhichGraph = New System.Windows.Forms.Label()
        Me.lblFullFileNameL = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.pnlYLabel = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mnubutDisplacement = New System.Windows.Forms.ToolStripButton()
        Me.mnubutHarmonics = New System.Windows.Forms.ToolStripButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.dropdownCenterOfMass = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFScanFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblProgressBar = New System.Windows.Forms.Label()
        Me.pnlGaitIndices = New System.Windows.Forms.Panel()
        Me.lblCoMPurityIndex = New System.Windows.Forms.Label()
        Me.lblCoMAmplitude = New System.Windows.Forms.Label()
        Me.MenuStrip_Main.SuspendLayout()
        Me.pnlGraph.SuspendLayout()
        Me.gbLegend.SuspendLayout()
        Me.gbxScale.SuspendLayout()
        Me.gbxPickHarmonics.SuspendLayout()
        Me.gbxHarmGraphType.SuspendLayout()
        Me.gbxHarmSumOrDiffOrAmpOrEq.SuspendLayout()
        Me.gbxHarmonicEasy.SuspendLayout()
        Me.gbxStatistics.SuspendLayout()
        Me.pnlStatisticsLeft.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlGaitIndices.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip_Main
        '
        Me.MenuStrip_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuCoMGraphs, Me.mnuCoPGraphs, Me.mnuCompareGraphs, Me.mnuGaitIndices, Me.mnuWindow, Me.mnuHelp})
        Me.MenuStrip_Main.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip_Main.Name = "MenuStrip_Main"
        Me.MenuStrip_Main.Padding = New System.Windows.Forms.Padding(9, 2, 0, 2)
        Me.MenuStrip_Main.Size = New System.Drawing.Size(935, 24)
        Me.MenuStrip_Main.TabIndex = 1
        Me.MenuStrip_Main.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpen, Me.mnuClose, Me.ToolStripMenuItem1, Me.mnuPrint, Me.ToolStripMenuItem2, Me.mnuExit})
        Me.mnuFile.MergeAction = System.Windows.Forms.MergeAction.Replace
        Me.mnuFile.MergeIndex = 0
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.ShowShortcutKeys = False
        Me.mnuOpen.Size = New System.Drawing.Size(103, 22)
        Me.mnuOpen.Text = "&Open"
        '
        'mnuClose
        '
        Me.mnuClose.Name = "mnuClose"
        Me.mnuClose.Size = New System.Drawing.Size(103, 22)
        Me.mnuClose.Text = "&Close"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(100, 6)
        '
        'mnuPrint
        '
        Me.mnuPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrintForm, Me.mnuPrintReport})
        Me.mnuPrint.Name = "mnuPrint"
        Me.mnuPrint.Size = New System.Drawing.Size(103, 22)
        Me.mnuPrint.Text = "Print"
        '
        'mnuPrintForm
        '
        Me.mnuPrintForm.Name = "mnuPrintForm"
        Me.mnuPrintForm.Size = New System.Drawing.Size(109, 22)
        Me.mnuPrintForm.Text = "Form"
        '
        'mnuPrintReport
        '
        Me.mnuPrintReport.Name = "mnuPrintReport"
        Me.mnuPrintReport.Size = New System.Drawing.Size(109, 22)
        Me.mnuPrintReport.Text = "Report"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(100, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(103, 22)
        Me.mnuExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopyData, Me.mnuCopyScreen, Me.ToolStripMenuItem3, Me.mnuScaleYAxis, Me.mnuScaleXAxis, Me.ToolStripMenuItem4, Me.mnuColors, Me.ToolStripMenuItem5, Me.mnuChangeToMetricUnits})
        Me.mnuEdit.MergeAction = System.Windows.Forms.MergeAction.Replace
        Me.mnuEdit.MergeIndex = 1
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuCopyData
        '
        Me.mnuCopyData.Name = "mnuCopyData"
        Me.mnuCopyData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuCopyData.Size = New System.Drawing.Size(196, 22)
        Me.mnuCopyData.Text = "Copy Data"
        '
        'mnuCopyScreen
        '
        Me.mnuCopyScreen.Name = "mnuCopyScreen"
        Me.mnuCopyScreen.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuCopyScreen.Size = New System.Drawing.Size(196, 22)
        Me.mnuCopyScreen.Text = "Copy Screen"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(193, 6)
        '
        'mnuScaleYAxis
        '
        Me.mnuScaleYAxis.Name = "mnuScaleYAxis"
        Me.mnuScaleYAxis.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.mnuScaleYAxis.Size = New System.Drawing.Size(196, 22)
        Me.mnuScaleYAxis.Text = "Scale &Y Axis"
        '
        'mnuScaleXAxis
        '
        Me.mnuScaleXAxis.Name = "mnuScaleXAxis"
        Me.mnuScaleXAxis.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuScaleXAxis.Size = New System.Drawing.Size(196, 22)
        Me.mnuScaleXAxis.Text = "Scale &X Axis"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(193, 6)
        '
        'mnuColors
        '
        Me.mnuColors.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColorLeft, Me.mnuColorRight, Me.mnuColorBoth, Me.mnuColorBackground, Me.mnuColorGridline, Me.mnuColorHarmonic, Me.ToolStripMenuItem11, Me.mnuColorDefault})
        Me.mnuColors.Name = "mnuColors"
        Me.mnuColors.Size = New System.Drawing.Size(196, 22)
        Me.mnuColors.Text = "Change Colors"
        '
        'mnuColorLeft
        '
        Me.mnuColorLeft.Name = "mnuColorLeft"
        Me.mnuColorLeft.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorLeft.Text = "Left Foot Color"
        '
        'mnuColorRight
        '
        Me.mnuColorRight.Name = "mnuColorRight"
        Me.mnuColorRight.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorRight.Text = "Right Foot Color"
        '
        'mnuColorBoth
        '
        Me.mnuColorBoth.Name = "mnuColorBoth"
        Me.mnuColorBoth.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorBoth.Text = "Double Support Color"
        '
        'mnuColorBackground
        '
        Me.mnuColorBackground.Name = "mnuColorBackground"
        Me.mnuColorBackground.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorBackground.Text = "Background Color"
        '
        'mnuColorGridline
        '
        Me.mnuColorGridline.Name = "mnuColorGridline"
        Me.mnuColorGridline.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorGridline.Text = "Gridline Color"
        '
        'mnuColorHarmonic
        '
        Me.mnuColorHarmonic.Name = "mnuColorHarmonic"
        Me.mnuColorHarmonic.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorHarmonic.Text = "Harmonic Line Color"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(186, 6)
        '
        'mnuColorDefault
        '
        Me.mnuColorDefault.Name = "mnuColorDefault"
        Me.mnuColorDefault.Size = New System.Drawing.Size(189, 22)
        Me.mnuColorDefault.Text = "Default Colors"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(193, 6)
        '
        'mnuChangeToMetricUnits
        '
        Me.mnuChangeToMetricUnits.Name = "mnuChangeToMetricUnits"
        Me.mnuChangeToMetricUnits.Size = New System.Drawing.Size(196, 22)
        Me.mnuChangeToMetricUnits.Text = "Change to Metric &Units"
        '
        'mnuCoMGraphs
        '
        Me.mnuCoMGraphs.Checked = True
        Me.mnuCoMGraphs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuCoMGraphs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDisplacement, Me.mnuVelocity, Me.mnuForceBW, Me.ToolStripMenuItem6, Me.mnuHarmonics, Me.ToolStripMenuItem12, Me.mnuEnergy, Me.mnuPower, Me.mnuWork, Me.ToolStripMenuItem7, Me.mnuForceAverage, Me.mnuForceAllSteps, Me.ToolStripMenuItem13, Me.mnuSpringConstant, Me.ToolStripMenuItem10, Me.mnuStatistics})
        Me.mnuCoMGraphs.MergeAction = System.Windows.Forms.MergeAction.Replace
        Me.mnuCoMGraphs.MergeIndex = 2
        Me.mnuCoMGraphs.Name = "mnuCoMGraphs"
        Me.mnuCoMGraphs.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuCoMGraphs.Size = New System.Drawing.Size(85, 20)
        Me.mnuCoMGraphs.Text = "Co&M Graphs"
        '
        'mnuDisplacement
        '
        Me.mnuDisplacement.Name = "mnuDisplacement"
        Me.mnuDisplacement.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuDisplacement.Size = New System.Drawing.Size(276, 22)
        Me.mnuDisplacement.Text = "&Displacement"
        '
        'mnuVelocity
        '
        Me.mnuVelocity.Name = "mnuVelocity"
        Me.mnuVelocity.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.mnuVelocity.Size = New System.Drawing.Size(276, 22)
        Me.mnuVelocity.Text = "&Velocity"
        '
        'mnuForceBW
        '
        Me.mnuForceBW.Name = "mnuForceBW"
        Me.mnuForceBW.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.mnuForceBW.Size = New System.Drawing.Size(276, 22)
        Me.mnuForceBW.Text = "Force In Terms of &Body Weight"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(273, 6)
        '
        'mnuHarmonics
        '
        Me.mnuHarmonics.Name = "mnuHarmonics"
        Me.mnuHarmonics.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.mnuHarmonics.Size = New System.Drawing.Size(276, 22)
        Me.mnuHarmonics.Text = "&Harmonics"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(273, 6)
        '
        'mnuEnergy
        '
        Me.mnuEnergy.Name = "mnuEnergy"
        Me.mnuEnergy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuEnergy.Size = New System.Drawing.Size(276, 22)
        Me.mnuEnergy.Text = "&Energy"
        '
        'mnuPower
        '
        Me.mnuPower.Name = "mnuPower"
        Me.mnuPower.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mnuPower.Size = New System.Drawing.Size(276, 22)
        Me.mnuPower.Text = "&Power"
        '
        'mnuWork
        '
        Me.mnuWork.Name = "mnuWork"
        Me.mnuWork.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.mnuWork.Size = New System.Drawing.Size(276, 22)
        Me.mnuWork.Text = "&Work"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(273, 6)
        '
        'mnuForceAverage
        '
        Me.mnuForceAverage.Name = "mnuForceAverage"
        Me.mnuForceAverage.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.mnuForceAverage.Size = New System.Drawing.Size(276, 22)
        Me.mnuForceAverage.Text = "Force - &Average - Actual"
        '
        'mnuForceAllSteps
        '
        Me.mnuForceAllSteps.Name = "mnuForceAllSteps"
        Me.mnuForceAllSteps.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuForceAllSteps.Size = New System.Drawing.Size(276, 22)
        Me.mnuForceAllSteps.Text = "Force - All &Steps"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(273, 6)
        '
        'mnuSpringConstant
        '
        Me.mnuSpringConstant.Name = "mnuSpringConstant"
        Me.mnuSpringConstant.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.mnuSpringConstant.Size = New System.Drawing.Size(276, 22)
        Me.mnuSpringConstant.Text = "Sprin&g Constant"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(273, 6)
        '
        'mnuStatistics
        '
        Me.mnuStatistics.Name = "mnuStatistics"
        Me.mnuStatistics.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.mnuStatistics.Size = New System.Drawing.Size(276, 22)
        Me.mnuStatistics.Text = "S&tatistics"
        '
        'mnuCoPGraphs
        '
        Me.mnuCoPGraphs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCoPDisplacementPosteriorToAnterior, Me.mnuCoPDisplacementLateralToMedial, Me.ToolStripMenuItem8, Me.mnuCoPVelocityPosteriorToAnterior, Me.mnuCoPVelocityLateralToMedial, Me.ToolStripMenuItem9, Me.mnuCoPAccelerationPosteriorToAnterior, Me.mnuCoPAccelerationLateralToMedial})
        Me.mnuCoPGraphs.MergeIndex = 3
        Me.mnuCoPGraphs.Name = "mnuCoPGraphs"
        Me.mnuCoPGraphs.Size = New System.Drawing.Size(81, 20)
        Me.mnuCoPGraphs.Text = "Co&P Graphs"
        '
        'mnuCoPDisplacementPosteriorToAnterior
        '
        Me.mnuCoPDisplacementPosteriorToAnterior.Name = "mnuCoPDisplacementPosteriorToAnterior"
        Me.mnuCoPDisplacementPosteriorToAnterior.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPDisplacementPosteriorToAnterior.Text = "Displacement - Posterior to Anterior"
        '
        'mnuCoPDisplacementLateralToMedial
        '
        Me.mnuCoPDisplacementLateralToMedial.Name = "mnuCoPDisplacementLateralToMedial"
        Me.mnuCoPDisplacementLateralToMedial.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPDisplacementLateralToMedial.Text = "Displacement - Lateral to Medial"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(261, 6)
        '
        'mnuCoPVelocityPosteriorToAnterior
        '
        Me.mnuCoPVelocityPosteriorToAnterior.Name = "mnuCoPVelocityPosteriorToAnterior"
        Me.mnuCoPVelocityPosteriorToAnterior.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPVelocityPosteriorToAnterior.Text = "Velocity - Posterior to Anterior"
        '
        'mnuCoPVelocityLateralToMedial
        '
        Me.mnuCoPVelocityLateralToMedial.Name = "mnuCoPVelocityLateralToMedial"
        Me.mnuCoPVelocityLateralToMedial.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPVelocityLateralToMedial.Text = "Velocity - Lateral to Medial"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(261, 6)
        '
        'mnuCoPAccelerationPosteriorToAnterior
        '
        Me.mnuCoPAccelerationPosteriorToAnterior.Name = "mnuCoPAccelerationPosteriorToAnterior"
        Me.mnuCoPAccelerationPosteriorToAnterior.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPAccelerationPosteriorToAnterior.Text = "Acceleration - Posterior to Anterior"
        '
        'mnuCoPAccelerationLateralToMedial
        '
        Me.mnuCoPAccelerationLateralToMedial.Name = "mnuCoPAccelerationLateralToMedial"
        Me.mnuCoPAccelerationLateralToMedial.Size = New System.Drawing.Size(264, 22)
        Me.mnuCoPAccelerationLateralToMedial.Text = "Acceleration - Lateral to Medial"
        '
        'mnuCompareGraphs
        '
        Me.mnuCompareGraphs.MergeIndex = 4
        Me.mnuCompareGraphs.Name = "mnuCompareGraphs"
        Me.mnuCompareGraphs.Size = New System.Drawing.Size(98, 20)
        Me.mnuCompareGraphs.Text = "Compa&re Trials"
        '
        'mnuGaitIndices
        '
        Me.mnuGaitIndices.MergeIndex = 5
        Me.mnuGaitIndices.Name = "mnuGaitIndices"
        Me.mnuGaitIndices.Size = New System.Drawing.Size(80, 20)
        Me.mnuGaitIndices.Text = "Gait &Indices"
        '
        'mnuWindow
        '
        Me.mnuWindow.MergeIndex = 6
        Me.mnuWindow.Name = "mnuWindow"
        Me.mnuWindow.Size = New System.Drawing.Size(63, 20)
        Me.mnuWindow.Text = "&Window"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuContents, Me.mnuAbout})
        Me.mnuHelp.MergeIndex = 7
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuContents
        '
        Me.mnuContents.Name = "mnuContents"
        Me.mnuContents.Size = New System.Drawing.Size(122, 22)
        Me.mnuContents.Text = "Contents"
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(122, 22)
        Me.mnuAbout.Text = "About"
        '
        'pnlGraph
        '
        Me.pnlGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGraph.Controls.Add(Me.butSuperimpose)
        Me.pnlGraph.Controls.Add(Me.gbLegend)
        Me.pnlGraph.Location = New System.Drawing.Point(14, 35)
        Me.pnlGraph.Name = "pnlGraph"
        Me.pnlGraph.Size = New System.Drawing.Size(209, 122)
        Me.pnlGraph.TabIndex = 2
        Me.pnlGraph.Visible = False
        '
        'butSuperimpose
        '
        Me.butSuperimpose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butSuperimpose.AutoSize = True
        Me.butSuperimpose.Location = New System.Drawing.Point(116, 18)
        Me.butSuperimpose.Name = "butSuperimpose"
        Me.butSuperimpose.Size = New System.Drawing.Size(89, 23)
        Me.butSuperimpose.TabIndex = 17
        Me.butSuperimpose.Text = "Superimpose"
        Me.ToolTip1.SetToolTip(Me.butSuperimpose, "Superimpose the Left and Right CoP Graphs")
        Me.butSuperimpose.UseVisualStyleBackColor = True
        Me.butSuperimpose.Visible = False
        '
        'gbLegend
        '
        Me.gbLegend.Controls.Add(Me.lblDoubleSupport)
        Me.gbLegend.Controls.Add(Me.lblLegendRight)
        Me.gbLegend.Controls.Add(Me.lblLegendLeft)
        Me.gbLegend.Location = New System.Drawing.Point(3, 12)
        Me.gbLegend.Name = "gbLegend"
        Me.gbLegend.Size = New System.Drawing.Size(160, 100)
        Me.gbLegend.TabIndex = 16
        Me.gbLegend.TabStop = False
        Me.gbLegend.Text = "Legend"
        '
        'lblDoubleSupport
        '
        Me.lblDoubleSupport.AutoSize = True
        Me.lblDoubleSupport.BackColor = System.Drawing.SystemColors.Control
        Me.lblDoubleSupport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblDoubleSupport.ForeColor = System.Drawing.Color.Red
        Me.lblDoubleSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDoubleSupport.Location = New System.Drawing.Point(36, 66)
        Me.lblDoubleSupport.Name = "lblDoubleSupport"
        Me.lblDoubleSupport.Size = New System.Drawing.Size(81, 13)
        Me.lblDoubleSupport.TabIndex = 3
        Me.lblDoubleSupport.Text = "Double Support"
        '
        'lblLegendRight
        '
        Me.lblLegendRight.AutoSize = True
        Me.lblLegendRight.BackColor = System.Drawing.SystemColors.Control
        Me.lblLegendRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblLegendRight.ForeColor = System.Drawing.Color.Green
        Me.lblLegendRight.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLegendRight.Location = New System.Drawing.Point(33, 45)
        Me.lblLegendRight.Name = "lblLegendRight"
        Me.lblLegendRight.Size = New System.Drawing.Size(104, 13)
        Me.lblLegendRight.TabIndex = 2
        Me.lblLegendRight.Text = "Right Single Support"
        '
        'lblLegendLeft
        '
        Me.lblLegendLeft.AutoSize = True
        Me.lblLegendLeft.BackColor = System.Drawing.SystemColors.Control
        Me.lblLegendLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblLegendLeft.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblLegendLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLegendLeft.Location = New System.Drawing.Point(33, 22)
        Me.lblLegendLeft.Name = "lblLegendLeft"
        Me.lblLegendLeft.Size = New System.Drawing.Size(97, 13)
        Me.lblLegendLeft.TabIndex = 1
        Me.lblLegendLeft.Text = "Left Single Support"
        '
        'butChangeToAngleFormula
        '
        Me.butChangeToAngleFormula.Location = New System.Drawing.Point(389, 66)
        Me.butChangeToAngleFormula.Name = "butChangeToAngleFormula"
        Me.butChangeToAngleFormula.Size = New System.Drawing.Size(84, 26)
        Me.butChangeToAngleFormula.TabIndex = 17
        Me.butChangeToAngleFormula.Text = "Change to Angle Format"
        Me.butChangeToAngleFormula.UseVisualStyleBackColor = True
        Me.butChangeToAngleFormula.Visible = False
        '
        'gbxScale
        '
        Me.gbxScale.Controls.Add(Me.butUpIncrement)
        Me.gbxScale.Controls.Add(Me.butDownIncrement)
        Me.gbxScale.Controls.Add(Me.txtScaleValue)
        Me.gbxScale.Controls.Add(Me.lblScaleUnits)
        Me.gbxScale.Controls.Add(Me.butScaleCancel)
        Me.gbxScale.Controls.Add(Me.butScaleOK)
        Me.gbxScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbxScale.Location = New System.Drawing.Point(221, 35)
        Me.gbxScale.Name = "gbxScale"
        Me.gbxScale.Size = New System.Drawing.Size(165, 131)
        Me.gbxScale.TabIndex = 3
        Me.gbxScale.TabStop = False
        Me.gbxScale.Text = "Scale"
        Me.gbxScale.Visible = False
        '
        'butUpIncrement
        '
        Me.butUpIncrement.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.butUpIncrement.FlatAppearance.BorderSize = 0
        Me.butUpIncrement.Font = New System.Drawing.Font("Symbol", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.butUpIncrement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.butUpIncrement.Location = New System.Drawing.Point(8, 28)
        Me.butUpIncrement.Name = "butUpIncrement"
        Me.butUpIncrement.Size = New System.Drawing.Size(24, 21)
        Me.butUpIncrement.TabIndex = 38
        Me.butUpIncrement.Text = "Ù"
        Me.butUpIncrement.UseVisualStyleBackColor = False
        '
        'butDownIncrement
        '
        Me.butDownIncrement.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.butDownIncrement.FlatAppearance.BorderSize = 0
        Me.butDownIncrement.Font = New System.Drawing.Font("Symbol", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.butDownIncrement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.butDownIncrement.Location = New System.Drawing.Point(8, 46)
        Me.butDownIncrement.Name = "butDownIncrement"
        Me.butDownIncrement.Size = New System.Drawing.Size(24, 21)
        Me.butDownIncrement.TabIndex = 37
        Me.butDownIncrement.Text = "Ú"
        Me.butDownIncrement.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.butDownIncrement.UseVisualStyleBackColor = False
        '
        'txtScaleValue
        '
        Me.txtScaleValue.Location = New System.Drawing.Point(31, 36)
        Me.txtScaleValue.Name = "txtScaleValue"
        Me.txtScaleValue.Size = New System.Drawing.Size(76, 23)
        Me.txtScaleValue.TabIndex = 35
        Me.txtScaleValue.Text = "Value"
        Me.txtScaleValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblScaleUnits
        '
        Me.lblScaleUnits.AutoSize = True
        Me.lblScaleUnits.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblScaleUnits.Location = New System.Drawing.Point(110, 39)
        Me.lblScaleUnits.Name = "lblScaleUnits"
        Me.lblScaleUnits.Size = New System.Drawing.Size(45, 17)
        Me.lblScaleUnits.TabIndex = 3
        Me.lblScaleUnits.Text = "Units"
        '
        'butScaleCancel
        '
        Me.butScaleCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butScaleCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.butScaleCancel.Location = New System.Drawing.Point(77, 72)
        Me.butScaleCancel.Name = "butScaleCancel"
        Me.butScaleCancel.Size = New System.Drawing.Size(78, 39)
        Me.butScaleCancel.TabIndex = 2
        Me.butScaleCancel.Text = "Cancel"
        Me.butScaleCancel.UseVisualStyleBackColor = True
        '
        'butScaleOK
        '
        Me.butScaleOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butScaleOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.butScaleOK.Location = New System.Drawing.Point(8, 72)
        Me.butScaleOK.Name = "butScaleOK"
        Me.butScaleOK.Size = New System.Drawing.Size(63, 39)
        Me.butScaleOK.TabIndex = 0
        Me.butScaleOK.Text = "OK"
        Me.butScaleOK.UseVisualStyleBackColor = True
        '
        'gbxPickHarmonics
        '
        Me.gbxPickHarmonics.Controls.Add(Me.gbxHarmGraphType)
        Me.gbxPickHarmonics.Controls.Add(Me.gbxHarmSumOrDiffOrAmpOrEq)
        Me.gbxPickHarmonics.Controls.Add(Me.butCancelHarmonics)
        Me.gbxPickHarmonics.Controls.Add(Me.lblHarmonicPickInstructions)
        Me.gbxPickHarmonics.Controls.Add(Me.gbxHarmonicEasy)
        Me.gbxPickHarmonics.Controls.Add(Me.listCheckedHarmonicBoxes)
        Me.gbxPickHarmonics.Controls.Add(Me.butOKHarmonics)
        Me.gbxPickHarmonics.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbxPickHarmonics.Location = New System.Drawing.Point(221, 442)
        Me.gbxPickHarmonics.Name = "gbxPickHarmonics"
        Me.gbxPickHarmonics.Size = New System.Drawing.Size(707, 267)
        Me.gbxPickHarmonics.TabIndex = 4
        Me.gbxPickHarmonics.TabStop = False
        Me.gbxPickHarmonics.Text = "Pick Harmonics"
        Me.gbxPickHarmonics.Visible = False
        '
        'gbxHarmGraphType
        '
        Me.gbxHarmGraphType.BackColor = System.Drawing.SystemColors.Control
        Me.gbxHarmGraphType.Controls.Add(Me.radbutHarmWorkGraph)
        Me.gbxHarmGraphType.Controls.Add(Me.radbutHarmPowerGraph)
        Me.gbxHarmGraphType.Controls.Add(Me.radbutHarmForceGraph)
        Me.gbxHarmGraphType.Controls.Add(Me.radbutHarmVelGraph)
        Me.gbxHarmGraphType.Controls.Add(Me.radbutHarmDisplGraph)
        Me.gbxHarmGraphType.Location = New System.Drawing.Point(48, 73)
        Me.gbxHarmGraphType.Name = "gbxHarmGraphType"
        Me.gbxHarmGraphType.Size = New System.Drawing.Size(395, 56)
        Me.gbxHarmGraphType.TabIndex = 5
        Me.gbxHarmGraphType.TabStop = False
        '
        'radbutHarmWorkGraph
        '
        Me.radbutHarmWorkGraph.AutoSize = True
        Me.radbutHarmWorkGraph.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radbutHarmWorkGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutHarmWorkGraph.Location = New System.Drawing.Point(325, 20)
        Me.radbutHarmWorkGraph.Name = "radbutHarmWorkGraph"
        Me.radbutHarmWorkGraph.Size = New System.Drawing.Size(53, 19)
        Me.radbutHarmWorkGraph.TabIndex = 9
        Me.radbutHarmWorkGraph.Text = "Work"
        Me.radbutHarmWorkGraph.UseVisualStyleBackColor = True
        '
        'radbutHarmPowerGraph
        '
        Me.radbutHarmPowerGraph.AutoSize = True
        Me.radbutHarmPowerGraph.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radbutHarmPowerGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutHarmPowerGraph.Location = New System.Drawing.Point(260, 20)
        Me.radbutHarmPowerGraph.Name = "radbutHarmPowerGraph"
        Me.radbutHarmPowerGraph.Size = New System.Drawing.Size(60, 19)
        Me.radbutHarmPowerGraph.TabIndex = 8
        Me.radbutHarmPowerGraph.Text = "Power"
        Me.radbutHarmPowerGraph.UseVisualStyleBackColor = True
        '
        'radbutHarmForceGraph
        '
        Me.radbutHarmForceGraph.AutoSize = True
        Me.radbutHarmForceGraph.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radbutHarmForceGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutHarmForceGraph.Location = New System.Drawing.Point(194, 20)
        Me.radbutHarmForceGraph.Name = "radbutHarmForceGraph"
        Me.radbutHarmForceGraph.Size = New System.Drawing.Size(56, 19)
        Me.radbutHarmForceGraph.TabIndex = 7
        Me.radbutHarmForceGraph.Text = "Force"
        Me.radbutHarmForceGraph.UseVisualStyleBackColor = True
        '
        'radbutHarmVelGraph
        '
        Me.radbutHarmVelGraph.AutoSize = True
        Me.radbutHarmVelGraph.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radbutHarmVelGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutHarmVelGraph.Location = New System.Drawing.Point(118, 20)
        Me.radbutHarmVelGraph.Name = "radbutHarmVelGraph"
        Me.radbutHarmVelGraph.Size = New System.Drawing.Size(66, 19)
        Me.radbutHarmVelGraph.TabIndex = 6
        Me.radbutHarmVelGraph.Text = "Velocity"
        Me.radbutHarmVelGraph.UseVisualStyleBackColor = True
        '
        'radbutHarmDisplGraph
        '
        Me.radbutHarmDisplGraph.AutoSize = True
        Me.radbutHarmDisplGraph.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radbutHarmDisplGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutHarmDisplGraph.Location = New System.Drawing.Point(9, 20)
        Me.radbutHarmDisplGraph.Name = "radbutHarmDisplGraph"
        Me.radbutHarmDisplGraph.Size = New System.Drawing.Size(101, 19)
        Me.radbutHarmDisplGraph.TabIndex = 5
        Me.radbutHarmDisplGraph.Text = "Displacement"
        Me.radbutHarmDisplGraph.UseVisualStyleBackColor = True
        '
        'gbxHarmSumOrDiffOrAmpOrEq
        '
        Me.gbxHarmSumOrDiffOrAmpOrEq.Controls.Add(Me.radbutEquation)
        Me.gbxHarmSumOrDiffOrAmpOrEq.Controls.Add(Me.radbutAmplitudes)
        Me.gbxHarmSumOrDiffOrAmpOrEq.Controls.Add(Me.radbutDifference)
        Me.gbxHarmSumOrDiffOrAmpOrEq.Controls.Add(Me.radbutSum)
        Me.gbxHarmSumOrDiffOrAmpOrEq.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.gbxHarmSumOrDiffOrAmpOrEq.Location = New System.Drawing.Point(7, 132)
        Me.gbxHarmSumOrDiffOrAmpOrEq.Name = "gbxHarmSumOrDiffOrAmpOrEq"
        Me.gbxHarmSumOrDiffOrAmpOrEq.Size = New System.Drawing.Size(355, 124)
        Me.gbxHarmSumOrDiffOrAmpOrEq.TabIndex = 2
        Me.gbxHarmSumOrDiffOrAmpOrEq.TabStop = False
        '
        'radbutEquation
        '
        Me.radbutEquation.AutoSize = True
        Me.radbutEquation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutEquation.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutEquation.Location = New System.Drawing.Point(12, 98)
        Me.radbutEquation.Name = "radbutEquation"
        Me.radbutEquation.Size = New System.Drawing.Size(175, 17)
        Me.radbutEquation.TabIndex = 3
        Me.radbutEquation.TabStop = True
        Me.radbutEquation.Text = "Fourier Equation for Graph Data"
        Me.radbutEquation.UseVisualStyleBackColor = True
        '
        'radbutAmplitudes
        '
        Me.radbutAmplitudes.AutoSize = True
        Me.radbutAmplitudes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutAmplitudes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutAmplitudes.Location = New System.Drawing.Point(12, 71)
        Me.radbutAmplitudes.Name = "radbutAmplitudes"
        Me.radbutAmplitudes.Size = New System.Drawing.Size(150, 17)
        Me.radbutAmplitudes.TabIndex = 2
        Me.radbutAmplitudes.TabStop = True
        Me.radbutAmplitudes.Text = "Amplitude of All Harmonics"
        Me.radbutAmplitudes.UseVisualStyleBackColor = True
        '
        'radbutDifference
        '
        Me.radbutDifference.AutoSize = True
        Me.radbutDifference.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutDifference.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutDifference.Location = New System.Drawing.Point(12, 44)
        Me.radbutDifference.Name = "radbutDifference"
        Me.radbutDifference.Size = New System.Drawing.Size(264, 17)
        Me.radbutDifference.TabIndex = 1
        Me.radbutDifference.TabStop = True
        Me.radbutDifference.Text = "Difference between Harmonics Checked and Data"
        Me.radbutDifference.UseVisualStyleBackColor = True
        '
        'radbutSum
        '
        Me.radbutSum.AutoSize = True
        Me.radbutSum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutSum.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutSum.Location = New System.Drawing.Point(12, 17)
        Me.radbutSum.Name = "radbutSum"
        Me.radbutSum.Size = New System.Drawing.Size(157, 17)
        Me.radbutSum.TabIndex = 0
        Me.radbutSum.TabStop = True
        Me.radbutSum.Text = "Sum of Harmonics Checked"
        Me.radbutSum.UseVisualStyleBackColor = True
        '
        'butCancelHarmonics
        '
        Me.butCancelHarmonics.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.butCancelHarmonics.Location = New System.Drawing.Point(593, 24)
        Me.butCancelHarmonics.Name = "butCancelHarmonics"
        Me.butCancelHarmonics.Size = New System.Drawing.Size(84, 29)
        Me.butCancelHarmonics.TabIndex = 4
        Me.butCancelHarmonics.Text = "Cancel"
        Me.butCancelHarmonics.UseVisualStyleBackColor = True
        '
        'lblHarmonicPickInstructions
        '
        Me.lblHarmonicPickInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHarmonicPickInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblHarmonicPickInstructions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHarmonicPickInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHarmonicPickInstructions.Location = New System.Drawing.Point(13, 24)
        Me.lblHarmonicPickInstructions.Name = "lblHarmonicPickInstructions"
        Me.lblHarmonicPickInstructions.Size = New System.Drawing.Size(467, 36)
        Me.lblHarmonicPickInstructions.TabIndex = 4
        Me.lblHarmonicPickInstructions.Text = "Put the instructions here"
        '
        'gbxHarmonicEasy
        '
        Me.gbxHarmonicEasy.Controls.Add(Me.radbutOtherHarm)
        Me.gbxHarmonicEasy.Controls.Add(Me.radbutOddHarm)
        Me.gbxHarmonicEasy.Controls.Add(Me.radbutEvenHarm)
        Me.gbxHarmonicEasy.Controls.Add(Me.radbutPureHarm)
        Me.gbxHarmonicEasy.Location = New System.Drawing.Point(369, 132)
        Me.gbxHarmonicEasy.Name = "gbxHarmonicEasy"
        Me.gbxHarmonicEasy.Size = New System.Drawing.Size(185, 124)
        Me.gbxHarmonicEasy.TabIndex = 1
        Me.gbxHarmonicEasy.TabStop = False
        '
        'radbutOtherHarm
        '
        Me.radbutOtherHarm.AutoSize = True
        Me.radbutOtherHarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutOtherHarm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutOtherHarm.Location = New System.Drawing.Point(8, 98)
        Me.radbutOtherHarm.Name = "radbutOtherHarm"
        Me.radbutOtherHarm.Size = New System.Drawing.Size(104, 17)
        Me.radbutOtherHarm.TabIndex = 3
        Me.radbutOtherHarm.TabStop = True
        Me.radbutOtherHarm.Text = "Other Harmonics"
        Me.radbutOtherHarm.UseVisualStyleBackColor = True
        '
        'radbutOddHarm
        '
        Me.radbutOddHarm.AutoSize = True
        Me.radbutOddHarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutOddHarm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutOddHarm.Location = New System.Drawing.Point(9, 71)
        Me.radbutOddHarm.Name = "radbutOddHarm"
        Me.radbutOddHarm.Size = New System.Drawing.Size(98, 17)
        Me.radbutOddHarm.TabIndex = 2
        Me.radbutOddHarm.TabStop = True
        Me.radbutOddHarm.Text = "Odd Harmonics"
        Me.radbutOddHarm.UseVisualStyleBackColor = True
        '
        'radbutEvenHarm
        '
        Me.radbutEvenHarm.AutoSize = True
        Me.radbutEvenHarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutEvenHarm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutEvenHarm.Location = New System.Drawing.Point(9, 44)
        Me.radbutEvenHarm.Name = "radbutEvenHarm"
        Me.radbutEvenHarm.Size = New System.Drawing.Size(103, 17)
        Me.radbutEvenHarm.TabIndex = 1
        Me.radbutEvenHarm.TabStop = True
        Me.radbutEvenHarm.Text = "Even Harmonics"
        Me.radbutEvenHarm.UseVisualStyleBackColor = True
        '
        'radbutPureHarm
        '
        Me.radbutPureHarm.AutoSize = True
        Me.radbutPureHarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.radbutPureHarm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radbutPureHarm.Location = New System.Drawing.Point(9, 17)
        Me.radbutPureHarm.Name = "radbutPureHarm"
        Me.radbutPureHarm.Size = New System.Drawing.Size(95, 17)
        Me.radbutPureHarm.TabIndex = 0
        Me.radbutPureHarm.TabStop = True
        Me.radbutPureHarm.Text = "Pure Harmonic"
        Me.radbutPureHarm.UseVisualStyleBackColor = True
        '
        'listCheckedHarmonicBoxes
        '
        Me.listCheckedHarmonicBoxes.BackColor = System.Drawing.SystemColors.Control
        Me.listCheckedHarmonicBoxes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.listCheckedHarmonicBoxes.FormattingEnabled = True
        Me.listCheckedHarmonicBoxes.Items.AddRange(New Object() {"1st Harmonic", "2nd Harmonic", "3rd Harmonic", "4th Harmonic", "5th Harmonic", "6th Harmonic", "7th Harmonic", "8th Harmonic", "9th Harmonic", "10th Harmonic", "11th Harmonic", "12th Harmonic"})
        Me.listCheckedHarmonicBoxes.Location = New System.Drawing.Point(563, 72)
        Me.listCheckedHarmonicBoxes.Margin = New System.Windows.Forms.Padding(6, 4, 3, 34)
        Me.listCheckedHarmonicBoxes.Name = "listCheckedHarmonicBoxes"
        Me.listCheckedHarmonicBoxes.Size = New System.Drawing.Size(133, 184)
        Me.listCheckedHarmonicBoxes.TabIndex = 1
        '
        'butOKHarmonics
        '
        Me.butOKHarmonics.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.butOKHarmonics.Location = New System.Drawing.Point(499, 24)
        Me.butOKHarmonics.Name = "butOKHarmonics"
        Me.butOKHarmonics.Size = New System.Drawing.Size(77, 29)
        Me.butOKHarmonics.TabIndex = 3
        Me.butOKHarmonics.Text = "OK"
        Me.butOKHarmonics.UseVisualStyleBackColor = True
        '
        'gbxStatistics
        '
        Me.gbxStatistics.Controls.Add(Me.pnlGaitIndices)
        Me.gbxStatistics.Controls.Add(Me.butStatisticsExit)
        Me.gbxStatistics.Controls.Add(Me.pnlStatisticsLeft)
        Me.gbxStatistics.Controls.Add(Me.lblBodyMass)
        Me.gbxStatistics.Controls.Add(Me.lblCadence)
        Me.gbxStatistics.Controls.Add(Me.lblNumberOfCompleteStrides)
        Me.gbxStatistics.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbxStatistics.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.gbxStatistics.Location = New System.Drawing.Point(8, 168)
        Me.gbxStatistics.Name = "gbxStatistics"
        Me.gbxStatistics.Size = New System.Drawing.Size(506, 268)
        Me.gbxStatistics.TabIndex = 17
        Me.gbxStatistics.TabStop = False
        Me.gbxStatistics.Text = "Statistics"
        Me.gbxStatistics.Visible = False
        '
        'butStatisticsExit
        '
        Me.butStatisticsExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butStatisticsExit.Location = New System.Drawing.Point(484, 8)
        Me.butStatisticsExit.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.butStatisticsExit.Name = "butStatisticsExit"
        Me.butStatisticsExit.Size = New System.Drawing.Size(22, 22)
        Me.butStatisticsExit.TabIndex = 6
        Me.butStatisticsExit.Text = "X"
        Me.butStatisticsExit.UseVisualStyleBackColor = True
        '
        'pnlStatisticsLeft
        '
        Me.pnlStatisticsLeft.Controls.Add(Me.lblTimeInRightSingleSupport)
        Me.pnlStatisticsLeft.Controls.Add(Me.lblTimeInRightDoubleSupport)
        Me.pnlStatisticsLeft.Controls.Add(Me.lblTimeInLeftSingleSupport)
        Me.pnlStatisticsLeft.Controls.Add(Me.lblTimeInLeftDoubleSupport)
        Me.pnlStatisticsLeft.Location = New System.Drawing.Point(16, 65)
        Me.pnlStatisticsLeft.Name = "pnlStatisticsLeft"
        Me.pnlStatisticsLeft.Size = New System.Drawing.Size(468, 78)
        Me.pnlStatisticsLeft.TabIndex = 5
        Me.pnlStatisticsLeft.TabStop = False
        '
        'lblTimeInRightSingleSupport
        '
        Me.lblTimeInRightSingleSupport.AutoSize = True
        Me.lblTimeInRightSingleSupport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTimeInRightSingleSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeInRightSingleSupport.Location = New System.Drawing.Point(248, 51)
        Me.lblTimeInRightSingleSupport.Name = "lblTimeInRightSingleSupport"
        Me.lblTimeInRightSingleSupport.Size = New System.Drawing.Size(150, 13)
        Me.lblTimeInRightSingleSupport.TabIndex = 3
        Me.lblTimeInRightSingleSupport.Text = "Time in Single Support - Right:"
        '
        'lblTimeInRightDoubleSupport
        '
        Me.lblTimeInRightDoubleSupport.AutoSize = True
        Me.lblTimeInRightDoubleSupport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTimeInRightDoubleSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeInRightDoubleSupport.Location = New System.Drawing.Point(248, 25)
        Me.lblTimeInRightDoubleSupport.Name = "lblTimeInRightDoubleSupport"
        Me.lblTimeInRightDoubleSupport.Size = New System.Drawing.Size(155, 13)
        Me.lblTimeInRightDoubleSupport.TabIndex = 2
        Me.lblTimeInRightDoubleSupport.Text = "Time in Double Support - Right:"
        '
        'lblTimeInLeftSingleSupport
        '
        Me.lblTimeInLeftSingleSupport.AutoSize = True
        Me.lblTimeInLeftSingleSupport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTimeInLeftSingleSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeInLeftSingleSupport.Location = New System.Drawing.Point(9, 50)
        Me.lblTimeInLeftSingleSupport.Name = "lblTimeInLeftSingleSupport"
        Me.lblTimeInLeftSingleSupport.Size = New System.Drawing.Size(143, 13)
        Me.lblTimeInLeftSingleSupport.TabIndex = 1
        Me.lblTimeInLeftSingleSupport.Text = "Time in Single Support - Left:"
        '
        'lblTimeInLeftDoubleSupport
        '
        Me.lblTimeInLeftDoubleSupport.AutoSize = True
        Me.lblTimeInLeftDoubleSupport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTimeInLeftDoubleSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeInLeftDoubleSupport.Location = New System.Drawing.Point(9, 25)
        Me.lblTimeInLeftDoubleSupport.Name = "lblTimeInLeftDoubleSupport"
        Me.lblTimeInLeftDoubleSupport.Size = New System.Drawing.Size(148, 13)
        Me.lblTimeInLeftDoubleSupport.TabIndex = 0
        Me.lblTimeInLeftDoubleSupport.Text = "Time in Double Support - Left:"
        '
        'lblBodyMass
        '
        Me.lblBodyMass.AutoSize = True
        Me.lblBodyMass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblBodyMass.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBodyMass.Location = New System.Drawing.Point(13, 42)
        Me.lblBodyMass.Name = "lblBodyMass"
        Me.lblBodyMass.Size = New System.Drawing.Size(115, 13)
        Me.lblBodyMass.TabIndex = 3
        Me.lblBodyMass.Text = "Calculated Body Mass:"
        '
        'lblCadence
        '
        Me.lblCadence.AutoSize = True
        Me.lblCadence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblCadence.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCadence.Location = New System.Drawing.Point(13, 25)
        Me.lblCadence.Name = "lblCadence"
        Me.lblCadence.Size = New System.Drawing.Size(56, 13)
        Me.lblCadence.TabIndex = 2
        Me.lblCadence.Text = "Cadence: "
        '
        'lblNumberOfCompleteStrides
        '
        Me.lblNumberOfCompleteStrides.AutoSize = True
        Me.lblNumberOfCompleteStrides.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblNumberOfCompleteStrides.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumberOfCompleteStrides.Location = New System.Drawing.Point(196, 25)
        Me.lblNumberOfCompleteStrides.Name = "lblNumberOfCompleteStrides"
        Me.lblNumberOfCompleteStrides.Size = New System.Drawing.Size(141, 13)
        Me.lblNumberOfCompleteStrides.TabIndex = 1
        Me.lblNumberOfCompleteStrides.Text = "Number of Complete Strides:"
        '
        'lblExamDate
        '
        Me.lblExamDate.AutoSize = True
        Me.lblExamDate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblExamDate.Location = New System.Drawing.Point(605, 42)
        Me.lblExamDate.Name = "lblExamDate"
        Me.lblExamDate.Size = New System.Drawing.Size(64, 13)
        Me.lblExamDate.TabIndex = 20
        Me.lblExamDate.Text = "ExamDate"
        Me.lblExamDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExamDate.Visible = False
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPatientName.Location = New System.Drawing.Point(386, 44)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(83, 13)
        Me.lblPatientName.TabIndex = 19
        Me.lblPatientName.Text = "Patient Name"
        Me.lblPatientName.Visible = False
        '
        'lblGraphTitle
        '
        Me.lblGraphTitle.AutoSize = True
        Me.lblGraphTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblGraphTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGraphTitle.Location = New System.Drawing.Point(489, 40)
        Me.lblGraphTitle.Name = "lblGraphTitle"
        Me.lblGraphTitle.Size = New System.Drawing.Size(85, 17)
        Me.lblGraphTitle.TabIndex = 18
        Me.lblGraphTitle.Text = "GraphTitle"
        '
        'lblYLabels
        '
        Me.lblYLabels.AutoSize = True
        Me.lblYLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblYLabels.Location = New System.Drawing.Point(549, 462)
        Me.lblYLabels.Name = "lblYLabels"
        Me.lblYLabels.Size = New System.Drawing.Size(174, 13)
        Me.lblYLabels.TabIndex = 21
        Me.lblYLabels.Text = "LblYLabel - Number of Labels"
        Me.lblYLabels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblYLabels.Visible = False
        '
        'lblXLabels
        '
        Me.lblXLabels.AutoSize = True
        Me.lblXLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblXLabels.Location = New System.Drawing.Point(553, 476)
        Me.lblXLabels.Name = "lblXLabels"
        Me.lblXLabels.Size = New System.Drawing.Size(170, 13)
        Me.lblXLabels.TabIndex = 22
        Me.lblXLabels.Text = "lblXLabel - Number of Labels"
        Me.lblXLabels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblXLabels.Visible = False
        '
        'lblMaximumYValue
        '
        Me.lblMaximumYValue.AutoSize = True
        Me.lblMaximumYValue.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblMaximumYValue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMaximumYValue.Location = New System.Drawing.Point(524, 170)
        Me.lblMaximumYValue.Name = "lblMaximumYValue"
        Me.lblMaximumYValue.Size = New System.Drawing.Size(108, 13)
        Me.lblMaximumYValue.TabIndex = 31
        Me.lblMaximumYValue.Text = "lbMaximumYValue"
        Me.lblMaximumYValue.Visible = False
        '
        'lblFullFileNameR
        '
        Me.lblFullFileNameR.AutoSize = True
        Me.lblFullFileNameR.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblFullFileNameR.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFullFileNameR.Location = New System.Drawing.Point(524, 157)
        Me.lblFullFileNameR.Name = "lblFullFileNameR"
        Me.lblFullFileNameR.Size = New System.Drawing.Size(101, 13)
        Me.lblFullFileNameR.TabIndex = 30
        Me.lblFullFileNameR.Text = "lblFullFileNameR"
        Me.lblFullFileNameR.Visible = False
        '
        'lbl_TimePerPercent
        '
        Me.lbl_TimePerPercent.AutoSize = True
        Me.lbl_TimePerPercent.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lbl_TimePerPercent.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_TimePerPercent.Location = New System.Drawing.Point(524, 131)
        Me.lbl_TimePerPercent.Name = "lbl_TimePerPercent"
        Me.lbl_TimePerPercent.Size = New System.Drawing.Size(127, 13)
        Me.lbl_TimePerPercent.TabIndex = 29
        Me.lbl_TimePerPercent.Text = "Seconds Per Percent"
        Me.lbl_TimePerPercent.Visible = False
        '
        'lbl_YUnitsPerPixel
        '
        Me.lbl_YUnitsPerPixel.AutoSize = True
        Me.lbl_YUnitsPerPixel.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lbl_YUnitsPerPixel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_YUnitsPerPixel.Location = New System.Drawing.Point(524, 116)
        Me.lbl_YUnitsPerPixel.Name = "lbl_YUnitsPerPixel"
        Me.lbl_YUnitsPerPixel.Size = New System.Drawing.Size(118, 13)
        Me.lbl_YUnitsPerPixel.TabIndex = 28
        Me.lbl_YUnitsPerPixel.Text = "lbl Y Units per Pixel"
        Me.lbl_YUnitsPerPixel.Visible = False
        '
        'lblOneYGridlineValue
        '
        Me.lblOneYGridlineValue.AutoSize = True
        Me.lblOneYGridlineValue.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblOneYGridlineValue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOneYGridlineValue.Location = New System.Drawing.Point(524, 103)
        Me.lblOneYGridlineValue.Name = "lblOneYGridlineValue"
        Me.lblOneYGridlineValue.Size = New System.Drawing.Size(142, 13)
        Me.lblOneYGridlineValue.TabIndex = 27
        Me.lblOneYGridlineValue.Text = "lbl One Y Gridline Value"
        Me.lblOneYGridlineValue.Visible = False
        '
        'lblMaximumXValue
        '
        Me.lblMaximumXValue.AutoSize = True
        Me.lblMaximumXValue.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblMaximumXValue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMaximumXValue.Location = New System.Drawing.Point(524, 76)
        Me.lblMaximumXValue.Name = "lblMaximumXValue"
        Me.lblMaximumXValue.Size = New System.Drawing.Size(125, 13)
        Me.lblMaximumXValue.TabIndex = 26
        Me.lblMaximumXValue.Text = "lblMaximum_X_Value"
        Me.lblMaximumXValue.Visible = False
        '
        'lblScaleWhichAxis
        '
        Me.lblScaleWhichAxis.AutoSize = True
        Me.lblScaleWhichAxis.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblScaleWhichAxis.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblScaleWhichAxis.Location = New System.Drawing.Point(524, 89)
        Me.lblScaleWhichAxis.Name = "lblScaleWhichAxis"
        Me.lblScaleWhichAxis.Size = New System.Drawing.Size(123, 13)
        Me.lblScaleWhichAxis.TabIndex = 25
        Me.lblScaleWhichAxis.Text = "lbl Scale Which Axis"
        Me.lblScaleWhichAxis.Visible = False
        '
        'lblWhichGraph
        '
        Me.lblWhichGraph.AutoSize = True
        Me.lblWhichGraph.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblWhichGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblWhichGraph.Location = New System.Drawing.Point(524, 63)
        Me.lblWhichGraph.Name = "lblWhichGraph"
        Me.lblWhichGraph.Size = New System.Drawing.Size(90, 13)
        Me.lblWhichGraph.TabIndex = 23
        Me.lblWhichGraph.Text = "lblWhichGraph"
        Me.lblWhichGraph.Visible = False
        '
        'lblFullFileNameL
        '
        Me.lblFullFileNameL.AutoSize = True
        Me.lblFullFileNameL.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblFullFileNameL.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFullFileNameL.Location = New System.Drawing.Point(524, 144)
        Me.lblFullFileNameL.Name = "lblFullFileNameL"
        Me.lblFullFileNameL.Size = New System.Drawing.Size(99, 13)
        Me.lblFullFileNameL.TabIndex = 24
        Me.lblFullFileNameL.Text = "lblFullFileNameL"
        Me.lblFullFileNameL.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 4000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        'pnlYLabel
        '
        Me.pnlYLabel.Location = New System.Drawing.Point(384, 108)
        Me.pnlYLabel.Name = "pnlYLabel"
        Me.pnlYLabel.Size = New System.Drawing.Size(104, 49)
        Me.pnlYLabel.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnubutDisplacement, Me.mnubutHarmonics})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(898, 25)
        Me.ToolStrip1.TabIndex = 32
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.Visible = False
        '
        'mnubutDisplacement
        '
        Me.mnubutDisplacement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnubutDisplacement.Image = CType(resources.GetObject("mnubutDisplacement.Image"), System.Drawing.Image)
        Me.mnubutDisplacement.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnubutDisplacement.Name = "mnubutDisplacement"
        Me.mnubutDisplacement.Size = New System.Drawing.Size(23, 22)
        Me.mnubutDisplacement.Text = "Displacement"
        Me.mnubutDisplacement.ToolTipText = "Displacement of Center of Mass"
        '
        'mnubutHarmonics
        '
        Me.mnubutHarmonics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnubutHarmonics.Image = CType(resources.GetObject("mnubutHarmonics.Image"), System.Drawing.Image)
        Me.mnubutHarmonics.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnubutHarmonics.Name = "mnubutHarmonics"
        Me.mnubutHarmonics.Size = New System.Drawing.Size(23, 22)
        Me.mnubutHarmonics.Text = "ToolStripButton2"
        Me.mnubutHarmonics.ToolTipText = "Harmonics"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.dropdownCenterOfMass})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(189, 26)
        '
        'dropdownCenterOfMass
        '
        Me.dropdownCenterOfMass.Name = "dropdownCenterOfMass"
        Me.dropdownCenterOfMass.Size = New System.Drawing.Size(188, 22)
        Me.dropdownCenterOfMass.Text = "ToolStripMenuItem14"
        '
        'OpenFScanFileDialog
        '
        Me.OpenFScanFileDialog.FileName = "OpenFileDialog1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(549, 233)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 23)
        Me.ProgressBar1.TabIndex = 33
        '
        'lblProgressBar
        '
        Me.lblProgressBar.AutoSize = True
        Me.lblProgressBar.Location = New System.Drawing.Point(551, 217)
        Me.lblProgressBar.Name = "lblProgressBar"
        Me.lblProgressBar.Size = New System.Drawing.Size(45, 13)
        Me.lblProgressBar.TabIndex = 34
        Me.lblProgressBar.Text = "Label1"
        '
        'pnlGaitIndices
        '
        Me.pnlGaitIndices.Controls.Add(Me.lblCoMAmplitude)
        Me.pnlGaitIndices.Controls.Add(Me.lblCoMPurityIndex)
        Me.pnlGaitIndices.Location = New System.Drawing.Point(16, 155)
        Me.pnlGaitIndices.Name = "pnlGaitIndices"
        Me.pnlGaitIndices.Size = New System.Drawing.Size(468, 112)
        Me.pnlGaitIndices.TabIndex = 7
        '
        'lblCoMPurityIndex
        '
        Me.lblCoMPurityIndex.AutoSize = True
        Me.lblCoMPurityIndex.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoMPurityIndex.Location = New System.Drawing.Point(11, 10)
        Me.lblCoMPurityIndex.Name = "lblCoMPurityIndex"
        Me.lblCoMPurityIndex.Size = New System.Drawing.Size(96, 13)
        Me.lblCoMPurityIndex.TabIndex = 0
        Me.lblCoMPurityIndex.Text = "CoM Purity Index - "
        '
        'lblCoMAmplitude
        '
        Me.lblCoMAmplitude.AutoSize = True
        Me.lblCoMAmplitude.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoMAmplitude.Location = New System.Drawing.Point(163, 10)
        Me.lblCoMAmplitude.Name = "lblCoMAmplitude"
        Me.lblCoMAmplitude.Size = New System.Drawing.Size(87, 13)
        Me.lblCoMAmplitude.TabIndex = 1
        Me.lblCoMAmplitude.Text = "CoM Amplitude - "
        '
        'frmGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(935, 721)
        Me.Controls.Add(Me.lblProgressBar)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.pnlYLabel)
        Me.Controls.Add(Me.butChangeToAngleFormula)
        Me.Controls.Add(Me.lblMaximumYValue)
        Me.Controls.Add(Me.lblFullFileNameR)
        Me.Controls.Add(Me.lbl_TimePerPercent)
        Me.Controls.Add(Me.lbl_YUnitsPerPixel)
        Me.Controls.Add(Me.gbxPickHarmonics)
        Me.Controls.Add(Me.lblOneYGridlineValue)
        Me.Controls.Add(Me.lblMaximumXValue)
        Me.Controls.Add(Me.lblScaleWhichAxis)
        Me.Controls.Add(Me.lblWhichGraph)
        Me.Controls.Add(Me.lblFullFileNameL)
        Me.Controls.Add(Me.lblXLabels)
        Me.Controls.Add(Me.lblYLabels)
        Me.Controls.Add(Me.lblExamDate)
        Me.Controls.Add(Me.gbxStatistics)
        Me.Controls.Add(Me.lblPatientName)
        Me.Controls.Add(Me.lblGraphTitle)
        Me.Controls.Add(Me.gbxScale)
        Me.Controls.Add(Me.pnlGraph)
        Me.Controls.Add(Me.MenuStrip_Main)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmGraph"
        Me.Text = "frmGraph"
        Me.MenuStrip_Main.ResumeLayout(False)
        Me.MenuStrip_Main.PerformLayout()
        Me.pnlGraph.ResumeLayout(False)
        Me.pnlGraph.PerformLayout()
        Me.gbLegend.ResumeLayout(False)
        Me.gbLegend.PerformLayout()
        Me.gbxScale.ResumeLayout(False)
        Me.gbxScale.PerformLayout()
        Me.gbxPickHarmonics.ResumeLayout(False)
        Me.gbxHarmGraphType.ResumeLayout(False)
        Me.gbxHarmGraphType.PerformLayout()
        Me.gbxHarmSumOrDiffOrAmpOrEq.ResumeLayout(False)
        Me.gbxHarmSumOrDiffOrAmpOrEq.PerformLayout()
        Me.gbxHarmonicEasy.ResumeLayout(False)
        Me.gbxHarmonicEasy.PerformLayout()
        Me.gbxStatistics.ResumeLayout(False)
        Me.gbxStatistics.PerformLayout()
        Me.pnlStatisticsLeft.ResumeLayout(False)
        Me.pnlStatisticsLeft.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlGaitIndices.ResumeLayout(False)
        Me.pnlGaitIndices.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip_Main As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCopyData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCopyScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuScaleYAxis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScaleXAxis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuColors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorBoth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorBackground As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorGridline As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColorHarmonic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuColorDefault As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuChangeToMetricUnits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoMGraphs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplacement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVelocity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuForceBW As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuHarmonics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEnergy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPower As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWork As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuForceAverage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuForceAllSteps As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSpringConstant As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuStatistics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoPGraphs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoPDisplacementPosteriorToAnterior As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoPDisplacementLateralToMedial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCoPVelocityPosteriorToAnterior As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoPVelocityLateralToMedial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCoPAccelerationPosteriorToAnterior As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCoPAccelerationLateralToMedial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompareGraphs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGaitIndices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents rotYAxisName As AxROTEXTLib.AxRotext
    Friend WithEvents pnlGraph As System.Windows.Forms.Panel
    Friend WithEvents gbLegend As System.Windows.Forms.GroupBox
    Friend WithEvents lblDoubleSupport As System.Windows.Forms.Label
    Friend WithEvents lblLegendRight As System.Windows.Forms.Label
    Friend WithEvents lblLegendLeft As System.Windows.Forms.Label
    Friend WithEvents gbxScale As System.Windows.Forms.GroupBox
    Friend WithEvents lblScaleUnits As System.Windows.Forms.Label
    Friend WithEvents butScaleCancel As System.Windows.Forms.Button
    Friend WithEvents butScaleOK As System.Windows.Forms.Button
    Friend WithEvents gbxPickHarmonics As System.Windows.Forms.GroupBox
    Friend WithEvents gbxHarmSumOrDiffOrAmpOrEq As System.Windows.Forms.GroupBox
    Friend WithEvents radbutEquation As System.Windows.Forms.RadioButton
    Friend WithEvents radbutAmplitudes As System.Windows.Forms.RadioButton
    Friend WithEvents radbutDifference As System.Windows.Forms.RadioButton
    Friend WithEvents radbutSum As System.Windows.Forms.RadioButton
    Friend WithEvents butCancelHarmonics As System.Windows.Forms.Button
    Friend WithEvents lblHarmonicPickInstructions As System.Windows.Forms.Label
    Friend WithEvents gbxHarmonicEasy As System.Windows.Forms.GroupBox
    Friend WithEvents radbutOtherHarm As System.Windows.Forms.RadioButton
    Friend WithEvents radbutOddHarm As System.Windows.Forms.RadioButton
    Friend WithEvents radbutEvenHarm As System.Windows.Forms.RadioButton
    Friend WithEvents radbutPureHarm As System.Windows.Forms.RadioButton
    Friend WithEvents listCheckedHarmonicBoxes As System.Windows.Forms.CheckedListBox
    Friend WithEvents butOKHarmonics As System.Windows.Forms.Button
    Friend WithEvents gbxStatistics As System.Windows.Forms.GroupBox
    Friend WithEvents pnlStatisticsLeft As System.Windows.Forms.GroupBox
    Friend WithEvents lblTimeInRightSingleSupport As System.Windows.Forms.Label
    Friend WithEvents lblTimeInRightDoubleSupport As System.Windows.Forms.Label
    Friend WithEvents lblTimeInLeftSingleSupport As System.Windows.Forms.Label
    Friend WithEvents lblTimeInLeftDoubleSupport As System.Windows.Forms.Label
    Friend WithEvents lblBodyMass As System.Windows.Forms.Label
    Friend WithEvents lblCadence As System.Windows.Forms.Label
    Friend WithEvents lblNumberOfCompleteStrides As System.Windows.Forms.Label
    Friend WithEvents lblExamDate As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents lblGraphTitle As System.Windows.Forms.Label
    Friend WithEvents lblYLabels As System.Windows.Forms.Label
    Friend WithEvents lblXLabels As System.Windows.Forms.Label
    Friend WithEvents lblMaximumYValue As System.Windows.Forms.Label
    Friend WithEvents lblFullFileNameR As System.Windows.Forms.Label
    Friend WithEvents lbl_TimePerPercent As System.Windows.Forms.Label
    Friend WithEvents lbl_YUnitsPerPixel As System.Windows.Forms.Label
    Friend WithEvents lblOneYGridlineValue As System.Windows.Forms.Label
    Friend WithEvents lblMaximumXValue As System.Windows.Forms.Label
    Friend WithEvents lblScaleWhichAxis As System.Windows.Forms.Label
    Friend WithEvents lblWhichGraph As System.Windows.Forms.Label
    Friend WithEvents lblFullFileNameL As System.Windows.Forms.Label
    Friend WithEvents butChangeToAngleFormula As System.Windows.Forms.Button
    Friend WithEvents txtScaleValue As System.Windows.Forms.TextBox
    Friend WithEvents butUpIncrement As System.Windows.Forms.Button
    Friend WithEvents butDownIncrement As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents pnlYLabel As System.Windows.Forms.Panel
    Friend WithEvents butStatisticsExit As System.Windows.Forms.Button
    Friend WithEvents butSuperimpose As System.Windows.Forms.Button
    Friend WithEvents gbxHarmGraphType As System.Windows.Forms.GroupBox
    Friend WithEvents radbutHarmWorkGraph As System.Windows.Forms.RadioButton
    Friend WithEvents radbutHarmPowerGraph As System.Windows.Forms.RadioButton
    Friend WithEvents radbutHarmForceGraph As System.Windows.Forms.RadioButton
    Friend WithEvents radbutHarmVelGraph As System.Windows.Forms.RadioButton
    Friend WithEvents radbutHarmDisplGraph As System.Windows.Forms.RadioButton
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrintForm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents mnubutDisplacement As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnubutHarmonics As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents dropdownCenterOfMass As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFScanFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgressBar As System.Windows.Forms.Label
    Friend WithEvents pnlGaitIndices As System.Windows.Forms.Panel
    Friend WithEvents lblCoMPurityIndex As System.Windows.Forms.Label
    Friend WithEvents lblCoMAmplitude As System.Windows.Forms.Label
End Class
