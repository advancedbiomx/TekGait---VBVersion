<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompareGraphs
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
        Me.chkLstFormsOpen = New System.Windows.Forms.CheckedListBox
        Me.pnlCompGraph = New System.Windows.Forms.Panel
        Me.gbColors = New System.Windows.Forms.GroupBox
        Me.lblColor10 = New System.Windows.Forms.Label
        Me.lblColor9 = New System.Windows.Forms.Label
        Me.lblColor8 = New System.Windows.Forms.Label
        Me.lblColor7 = New System.Windows.Forms.Label
        Me.lblColor6 = New System.Windows.Forms.Label
        Me.lblColor5 = New System.Windows.Forms.Label
        Me.lblColor4 = New System.Windows.Forms.Label
        Me.lblColor3 = New System.Windows.Forms.Label
        Me.lblColor2 = New System.Windows.Forms.Label
        Me.lblColor1 = New System.Windows.Forms.Label
        Me.gbGraphType = New System.Windows.Forms.GroupBox
        Me.radbutSpringConstants = New System.Windows.Forms.RadioButton
        Me.radbutEnergy = New System.Windows.Forms.RadioButton
        Me.radbutWork = New System.Windows.Forms.RadioButton
        Me.radbutPower = New System.Windows.Forms.RadioButton
        Me.radbutForce = New System.Windows.Forms.RadioButton
        Me.radbutVelocity = New System.Windows.Forms.RadioButton
        Me.radbutDisplacement = New System.Windows.Forms.RadioButton
        Me.lblXLabels = New System.Windows.Forms.Label
        Me.lblYLabels = New System.Windows.Forms.Label
        Me.gbTrialList = New System.Windows.Forms.GroupBox
        Me.gbColors.SuspendLayout()
        Me.gbGraphType.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkLstFormsOpen
        '
        Me.chkLstFormsOpen.FormattingEnabled = True
        Me.chkLstFormsOpen.Location = New System.Drawing.Point(179, 163)
        Me.chkLstFormsOpen.Name = "chkLstFormsOpen"
        Me.chkLstFormsOpen.Size = New System.Drawing.Size(115, 259)
        Me.chkLstFormsOpen.TabIndex = 0
        Me.chkLstFormsOpen.Visible = False
        '
        'pnlCompGraph
        '
        Me.pnlCompGraph.Location = New System.Drawing.Point(191, 91)
        Me.pnlCompGraph.Name = "pnlCompGraph"
        Me.pnlCompGraph.Size = New System.Drawing.Size(96, 63)
        Me.pnlCompGraph.TabIndex = 2
        '
        'gbColors
        '
        Me.gbColors.Controls.Add(Me.lblColor10)
        Me.gbColors.Controls.Add(Me.lblColor9)
        Me.gbColors.Controls.Add(Me.lblColor8)
        Me.gbColors.Controls.Add(Me.lblColor7)
        Me.gbColors.Controls.Add(Me.lblColor6)
        Me.gbColors.Controls.Add(Me.lblColor5)
        Me.gbColors.Controls.Add(Me.lblColor4)
        Me.gbColors.Controls.Add(Me.lblColor3)
        Me.gbColors.Controls.Add(Me.lblColor2)
        Me.gbColors.Controls.Add(Me.lblColor1)
        Me.gbColors.Location = New System.Drawing.Point(293, 75)
        Me.gbColors.Name = "gbColors"
        Me.gbColors.Size = New System.Drawing.Size(59, 248)
        Me.gbColors.TabIndex = 3
        Me.gbColors.TabStop = False
        Me.gbColors.Text = "Graphing Colors"
        Me.gbColors.Visible = False
        '
        'lblColor10
        '
        Me.lblColor10.AutoSize = True
        Me.lblColor10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblColor10.Location = New System.Drawing.Point(9, 220)
        Me.lblColor10.Name = "lblColor10"
        Me.lblColor10.Size = New System.Drawing.Size(50, 13)
        Me.lblColor10.TabIndex = 9
        Me.lblColor10.Tag = "False"
        Me.lblColor10.Text = "Color10"
        '
        'lblColor9
        '
        Me.lblColor9.AutoSize = True
        Me.lblColor9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblColor9.Location = New System.Drawing.Point(9, 198)
        Me.lblColor9.Name = "lblColor9"
        Me.lblColor9.Size = New System.Drawing.Size(43, 13)
        Me.lblColor9.TabIndex = 8
        Me.lblColor9.Tag = "False"
        Me.lblColor9.Text = "Color9"
        '
        'lblColor8
        '
        Me.lblColor8.AutoSize = True
        Me.lblColor8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblColor8.Location = New System.Drawing.Point(9, 176)
        Me.lblColor8.Name = "lblColor8"
        Me.lblColor8.Size = New System.Drawing.Size(43, 13)
        Me.lblColor8.TabIndex = 7
        Me.lblColor8.Tag = "False"
        Me.lblColor8.Text = "Color8"
        '
        'lblColor7
        '
        Me.lblColor7.AutoSize = True
        Me.lblColor7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.lblColor7.Location = New System.Drawing.Point(9, 154)
        Me.lblColor7.Name = "lblColor7"
        Me.lblColor7.Size = New System.Drawing.Size(43, 13)
        Me.lblColor7.TabIndex = 6
        Me.lblColor7.Tag = "False"
        Me.lblColor7.Text = "Color7"
        '
        'lblColor6
        '
        Me.lblColor6.AutoSize = True
        Me.lblColor6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblColor6.Location = New System.Drawing.Point(9, 132)
        Me.lblColor6.Name = "lblColor6"
        Me.lblColor6.Size = New System.Drawing.Size(43, 13)
        Me.lblColor6.TabIndex = 5
        Me.lblColor6.Tag = "False"
        Me.lblColor6.Text = "Color6"
        '
        'lblColor5
        '
        Me.lblColor5.AutoSize = True
        Me.lblColor5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblColor5.Location = New System.Drawing.Point(9, 110)
        Me.lblColor5.Name = "lblColor5"
        Me.lblColor5.Size = New System.Drawing.Size(43, 13)
        Me.lblColor5.TabIndex = 4
        Me.lblColor5.Tag = "False"
        Me.lblColor5.Text = "Color5"
        '
        'lblColor4
        '
        Me.lblColor4.AutoSize = True
        Me.lblColor4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblColor4.Location = New System.Drawing.Point(9, 88)
        Me.lblColor4.Name = "lblColor4"
        Me.lblColor4.Size = New System.Drawing.Size(43, 13)
        Me.lblColor4.TabIndex = 3
        Me.lblColor4.Tag = "False"
        Me.lblColor4.Text = "Color4"
        '
        'lblColor3
        '
        Me.lblColor3.AutoSize = True
        Me.lblColor3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblColor3.Location = New System.Drawing.Point(9, 66)
        Me.lblColor3.Name = "lblColor3"
        Me.lblColor3.Size = New System.Drawing.Size(43, 13)
        Me.lblColor3.TabIndex = 2
        Me.lblColor3.Tag = "False"
        Me.lblColor3.Text = "Color3"
        '
        'lblColor2
        '
        Me.lblColor2.AutoSize = True
        Me.lblColor2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblColor2.Location = New System.Drawing.Point(9, 44)
        Me.lblColor2.Name = "lblColor2"
        Me.lblColor2.Size = New System.Drawing.Size(43, 13)
        Me.lblColor2.TabIndex = 1
        Me.lblColor2.Tag = "False"
        Me.lblColor2.Text = "Color2"
        '
        'lblColor1
        '
        Me.lblColor1.AutoSize = True
        Me.lblColor1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblColor1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblColor1.Location = New System.Drawing.Point(9, 22)
        Me.lblColor1.Name = "lblColor1"
        Me.lblColor1.Size = New System.Drawing.Size(43, 13)
        Me.lblColor1.TabIndex = 0
        Me.lblColor1.Tag = "False"
        Me.lblColor1.Text = "Color1"
        '
        'gbGraphType
        '
        Me.gbGraphType.Controls.Add(Me.radbutSpringConstants)
        Me.gbGraphType.Controls.Add(Me.radbutEnergy)
        Me.gbGraphType.Controls.Add(Me.radbutWork)
        Me.gbGraphType.Controls.Add(Me.radbutPower)
        Me.gbGraphType.Controls.Add(Me.radbutForce)
        Me.gbGraphType.Controls.Add(Me.radbutVelocity)
        Me.gbGraphType.Controls.Add(Me.radbutDisplacement)
        Me.gbGraphType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGraphType.Location = New System.Drawing.Point(179, 9)
        Me.gbGraphType.Name = "gbGraphType"
        Me.gbGraphType.Size = New System.Drawing.Size(669, 60)
        Me.gbGraphType.TabIndex = 4
        Me.gbGraphType.TabStop = False
        Me.gbGraphType.Text = "Which Graph Type Do You Want to Compare?"
        '
        'radbutSpringConstants
        '
        Me.radbutSpringConstants.AutoSize = True
        Me.radbutSpringConstants.Location = New System.Drawing.Point(525, 27)
        Me.radbutSpringConstants.Name = "radbutSpringConstants"
        Me.radbutSpringConstants.Size = New System.Drawing.Size(115, 17)
        Me.radbutSpringConstants.TabIndex = 6
        Me.radbutSpringConstants.TabStop = True
        Me.radbutSpringConstants.Text = "Spring Constant"
        Me.radbutSpringConstants.UseVisualStyleBackColor = True
        '
        'radbutEnergy
        '
        Me.radbutEnergy.AutoSize = True
        Me.radbutEnergy.Location = New System.Drawing.Point(453, 27)
        Me.radbutEnergy.Name = "radbutEnergy"
        Me.radbutEnergy.Size = New System.Drawing.Size(64, 17)
        Me.radbutEnergy.TabIndex = 5
        Me.radbutEnergy.TabStop = True
        Me.radbutEnergy.Text = "Energy"
        Me.radbutEnergy.UseVisualStyleBackColor = True
        '
        'radbutWork
        '
        Me.radbutWork.AutoSize = True
        Me.radbutWork.Location = New System.Drawing.Point(390, 27)
        Me.radbutWork.Name = "radbutWork"
        Me.radbutWork.Size = New System.Drawing.Size(55, 17)
        Me.radbutWork.TabIndex = 4
        Me.radbutWork.TabStop = True
        Me.radbutWork.Text = "Work"
        Me.radbutWork.UseVisualStyleBackColor = True
        '
        'radbutPower
        '
        Me.radbutPower.AutoSize = True
        Me.radbutPower.Location = New System.Drawing.Point(322, 27)
        Me.radbutPower.Name = "radbutPower"
        Me.radbutPower.Size = New System.Drawing.Size(60, 17)
        Me.radbutPower.TabIndex = 3
        Me.radbutPower.TabStop = True
        Me.radbutPower.Text = "Power"
        Me.radbutPower.UseVisualStyleBackColor = True
        '
        'radbutForce
        '
        Me.radbutForce.AutoSize = True
        Me.radbutForce.Location = New System.Drawing.Point(257, 27)
        Me.radbutForce.Name = "radbutForce"
        Me.radbutForce.Size = New System.Drawing.Size(57, 17)
        Me.radbutForce.TabIndex = 2
        Me.radbutForce.TabStop = True
        Me.radbutForce.Text = "Force"
        Me.radbutForce.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.radbutForce.UseVisualStyleBackColor = True
        '
        'radbutVelocity
        '
        Me.radbutVelocity.AutoSize = True
        Me.radbutVelocity.Location = New System.Drawing.Point(150, 27)
        Me.radbutVelocity.Name = "radbutVelocity"
        Me.radbutVelocity.Size = New System.Drawing.Size(99, 17)
        Me.radbutVelocity.TabIndex = 1
        Me.radbutVelocity.TabStop = True
        Me.radbutVelocity.Text = "CoM Velocity"
        Me.radbutVelocity.UseVisualStyleBackColor = True
        '
        'radbutDisplacement
        '
        Me.radbutDisplacement.AutoSize = True
        Me.radbutDisplacement.Location = New System.Drawing.Point(12, 27)
        Me.radbutDisplacement.Name = "radbutDisplacement"
        Me.radbutDisplacement.Size = New System.Drawing.Size(130, 17)
        Me.radbutDisplacement.TabIndex = 0
        Me.radbutDisplacement.TabStop = True
        Me.radbutDisplacement.Text = "CoM Displacement"
        Me.radbutDisplacement.UseVisualStyleBackColor = True
        '
        'lblXLabels
        '
        Me.lblXLabels.AutoSize = True
        Me.lblXLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblXLabels.Location = New System.Drawing.Point(409, 89)
        Me.lblXLabels.Name = "lblXLabels"
        Me.lblXLabels.Size = New System.Drawing.Size(142, 13)
        Me.lblXLabels.TabIndex = 24
        Me.lblXLabels.Text = "lblXLabel - Number of Labels"
        Me.lblXLabels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblXLabels.Visible = False
        '
        'lblYLabels
        '
        Me.lblYLabels.AutoSize = True
        Me.lblYLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblYLabels.Location = New System.Drawing.Point(405, 75)
        Me.lblYLabels.Name = "lblYLabels"
        Me.lblYLabels.Size = New System.Drawing.Size(146, 13)
        Me.lblYLabels.TabIndex = 23
        Me.lblYLabels.Text = "LblYLabel - Number of Labels"
        Me.lblYLabels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblYLabels.Visible = False
        '
        'gbTrialList
        '
        Me.gbTrialList.Location = New System.Drawing.Point(19, 9)
        Me.gbTrialList.Name = "gbTrialList"
        Me.gbTrialList.Size = New System.Drawing.Size(154, 462)
        Me.gbTrialList.TabIndex = 25
        Me.gbTrialList.TabStop = False
        Me.gbTrialList.Text = "Choose Trials to Compare"
        '
        'frmCompareGraphs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1089, 625)
        Me.Controls.Add(Me.gbTrialList)
        Me.Controls.Add(Me.lblXLabels)
        Me.Controls.Add(Me.lblYLabels)
        Me.Controls.Add(Me.pnlCompGraph)
        Me.Controls.Add(Me.gbGraphType)
        Me.Controls.Add(Me.gbColors)
        Me.Controls.Add(Me.chkLstFormsOpen)
        Me.Name = "frmCompareGraphs"
        Me.Text = "Graph Comparisons"
        Me.gbColors.ResumeLayout(False)
        Me.gbColors.PerformLayout()
        Me.gbGraphType.ResumeLayout(False)
        Me.gbGraphType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkLstFormsOpen As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlCompGraph As System.Windows.Forms.Panel
    Friend WithEvents gbColors As System.Windows.Forms.GroupBox
    Friend WithEvents lblColor1 As System.Windows.Forms.Label
    Friend WithEvents lblColor5 As System.Windows.Forms.Label
    Friend WithEvents lblColor4 As System.Windows.Forms.Label
    Friend WithEvents lblColor3 As System.Windows.Forms.Label
    Friend WithEvents lblColor2 As System.Windows.Forms.Label
    Friend WithEvents lblColor7 As System.Windows.Forms.Label
    Friend WithEvents lblColor6 As System.Windows.Forms.Label
    Friend WithEvents lblColor9 As System.Windows.Forms.Label
    Friend WithEvents lblColor8 As System.Windows.Forms.Label
    Friend WithEvents lblColor10 As System.Windows.Forms.Label
    Friend WithEvents gbGraphType As System.Windows.Forms.GroupBox
    Friend WithEvents radbutDisplacement As System.Windows.Forms.RadioButton
    Friend WithEvents radbutVelocity As System.Windows.Forms.RadioButton
    Friend WithEvents radbutForce As System.Windows.Forms.RadioButton
    Friend WithEvents radbutEnergy As System.Windows.Forms.RadioButton
    Friend WithEvents radbutWork As System.Windows.Forms.RadioButton
    Friend WithEvents radbutPower As System.Windows.Forms.RadioButton
    Friend WithEvents radbutSpringConstants As System.Windows.Forms.RadioButton
    Friend WithEvents lblXLabels As System.Windows.Forms.Label
    Friend WithEvents lblYLabels As System.Windows.Forms.Label
    Friend WithEvents gbTrialList As System.Windows.Forms.GroupBox
End Class
