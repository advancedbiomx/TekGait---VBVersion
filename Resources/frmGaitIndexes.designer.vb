<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGaitIndexes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGaitIndexes))
        Me.pnlSymmetry = New System.Windows.Forms.Panel()
        Me.butSymmetry = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lblSymmetry = New System.Windows.Forms.Label()
        Me.pnlPurity = New System.Windows.Forms.Panel()
        Me.butPurity = New System.Windows.Forms.Button()
        Me.lblPurity = New System.Windows.Forms.Label()
        Me.pnlEnergy = New System.Windows.Forms.Panel()
        Me.butEnergy = New System.Windows.Forms.Button()
        Me.lblEnergy = New System.Windows.Forms.Label()
        Me.pnlOverall = New System.Windows.Forms.Panel()
        Me.butOverall = New System.Windows.Forms.Button()
        Me.lblOverall = New System.Windows.Forms.Label()
        Me.gbTrialList = New System.Windows.Forms.GroupBox()
        Me.chkLstFormsOpen = New System.Windows.Forms.CheckedListBox()
        Me.gbColors = New System.Windows.Forms.GroupBox()
        Me.lblColor10 = New System.Windows.Forms.Label()
        Me.lblColor9 = New System.Windows.Forms.Label()
        Me.lblColor8 = New System.Windows.Forms.Label()
        Me.lblColor7 = New System.Windows.Forms.Label()
        Me.lblColor6 = New System.Windows.Forms.Label()
        Me.lblColor5 = New System.Windows.Forms.Label()
        Me.lblColor4 = New System.Windows.Forms.Label()
        Me.lblColor3 = New System.Windows.Forms.Label()
        Me.lblColor2 = New System.Windows.Forms.Label()
        Me.lblColor1 = New System.Windows.Forms.Label()
        Me.pnlSymmetry.SuspendLayout()
        Me.pnlPurity.SuspendLayout()
        Me.pnlEnergy.SuspendLayout()
        Me.pnlOverall.SuspendLayout()
        Me.gbColors.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSymmetry
        '
        Me.pnlSymmetry.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSymmetry.Controls.Add(Me.butSymmetry)
        Me.pnlSymmetry.Controls.Add(Me.lblSymmetry)
        Me.pnlSymmetry.Location = New System.Drawing.Point(472, 17)
        Me.pnlSymmetry.Name = "pnlSymmetry"
        Me.pnlSymmetry.Size = New System.Drawing.Size(186, 62)
        Me.pnlSymmetry.TabIndex = 0
        '
        'butSymmetry
        '
        Me.butSymmetry.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butSymmetry.ImageList = Me.ImageList1
        Me.butSymmetry.Location = New System.Drawing.Point(163, 0)
        Me.butSymmetry.Name = "butSymmetry"
        Me.butSymmetry.Size = New System.Drawing.Size(21, 21)
        Me.butSymmetry.TabIndex = 2
        Me.butSymmetry.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "add.png")
        Me.ImageList1.Images.SetKeyName(1, "zoomout.png")
        '
        'lblSymmetry
        '
        Me.lblSymmetry.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblSymmetry.AutoSize = True
        Me.lblSymmetry.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSymmetry.Location = New System.Drawing.Point(51, 4)
        Me.lblSymmetry.Name = "lblSymmetry"
        Me.lblSymmetry.Size = New System.Drawing.Size(90, 13)
        Me.lblSymmetry.TabIndex = 1
        Me.lblSymmetry.Text = "CoP Efficiency"
        Me.lblSymmetry.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlPurity
        '
        Me.pnlPurity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPurity.Controls.Add(Me.butPurity)
        Me.pnlPurity.Controls.Add(Me.lblPurity)
        Me.pnlPurity.Location = New System.Drawing.Point(273, 17)
        Me.pnlPurity.Name = "pnlPurity"
        Me.pnlPurity.Size = New System.Drawing.Size(173, 74)
        Me.pnlPurity.TabIndex = 1
        '
        'butPurity
        '
        Me.butPurity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.butPurity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.butPurity.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.butPurity.ImageList = Me.ImageList1
        Me.butPurity.Location = New System.Drawing.Point(148, -1)
        Me.butPurity.Margin = New System.Windows.Forms.Padding(0)
        Me.butPurity.Name = "butPurity"
        Me.butPurity.Size = New System.Drawing.Size(21, 21)
        Me.butPurity.TabIndex = 1
        Me.butPurity.UseVisualStyleBackColor = True
        '
        'lblPurity
        '
        Me.lblPurity.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblPurity.AutoSize = True
        Me.lblPurity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurity.Location = New System.Drawing.Point(41, 4)
        Me.lblPurity.Name = "lblPurity"
        Me.lblPurity.Size = New System.Drawing.Size(74, 13)
        Me.lblPurity.TabIndex = 0
        Me.lblPurity.Text = "Purity Index"
        Me.lblPurity.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlEnergy
        '
        Me.pnlEnergy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlEnergy.Controls.Add(Me.butEnergy)
        Me.pnlEnergy.Controls.Add(Me.lblEnergy)
        Me.pnlEnergy.Location = New System.Drawing.Point(273, 110)
        Me.pnlEnergy.Name = "pnlEnergy"
        Me.pnlEnergy.Size = New System.Drawing.Size(173, 79)
        Me.pnlEnergy.TabIndex = 2
        '
        'butEnergy
        '
        Me.butEnergy.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butEnergy.ImageList = Me.ImageList1
        Me.butEnergy.Location = New System.Drawing.Point(150, -2)
        Me.butEnergy.Name = "butEnergy"
        Me.butEnergy.Size = New System.Drawing.Size(21, 21)
        Me.butEnergy.TabIndex = 2
        Me.butEnergy.UseVisualStyleBackColor = True
        '
        'lblEnergy
        '
        Me.lblEnergy.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEnergy.AutoSize = True
        Me.lblEnergy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnergy.Location = New System.Drawing.Point(52, 4)
        Me.lblEnergy.Name = "lblEnergy"
        Me.lblEnergy.Size = New System.Drawing.Size(81, 13)
        Me.lblEnergy.TabIndex = 1
        Me.lblEnergy.Text = "Energy Index"
        Me.lblEnergy.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlOverall
        '
        Me.pnlOverall.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlOverall.Controls.Add(Me.butOverall)
        Me.pnlOverall.Controls.Add(Me.lblOverall)
        Me.pnlOverall.Location = New System.Drawing.Point(472, 110)
        Me.pnlOverall.Name = "pnlOverall"
        Me.pnlOverall.Size = New System.Drawing.Size(186, 79)
        Me.pnlOverall.TabIndex = 3
        '
        'butOverall
        '
        Me.butOverall.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.butOverall.ImageList = Me.ImageList1
        Me.butOverall.Location = New System.Drawing.Point(163, -2)
        Me.butOverall.Name = "butOverall"
        Me.butOverall.Size = New System.Drawing.Size(21, 21)
        Me.butOverall.TabIndex = 3
        Me.butOverall.UseVisualStyleBackColor = True
        '
        'lblOverall
        '
        Me.lblOverall.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblOverall.AutoSize = True
        Me.lblOverall.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverall.Location = New System.Drawing.Point(32, 5)
        Me.lblOverall.Name = "lblOverall"
        Me.lblOverall.Size = New System.Drawing.Size(109, 13)
        Me.lblOverall.TabIndex = 2
        Me.lblOverall.Text = "Overall Gait Index"
        Me.lblOverall.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbTrialList
        '
        Me.gbTrialList.Location = New System.Drawing.Point(12, 17)
        Me.gbTrialList.Name = "gbTrialList"
        Me.gbTrialList.Size = New System.Drawing.Size(154, 178)
        Me.gbTrialList.TabIndex = 26
        Me.gbTrialList.TabStop = False
        Me.gbTrialList.Text = "Choose Trials to View"
        '
        'chkLstFormsOpen
        '
        Me.chkLstFormsOpen.FormattingEnabled = True
        Me.chkLstFormsOpen.Location = New System.Drawing.Point(-3, 0)
        Me.chkLstFormsOpen.Name = "chkLstFormsOpen"
        Me.chkLstFormsOpen.Size = New System.Drawing.Size(115, 259)
        Me.chkLstFormsOpen.TabIndex = 27
        Me.chkLstFormsOpen.Visible = False
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
        Me.gbColors.Location = New System.Drawing.Point(118, 0)
        Me.gbColors.Name = "gbColors"
        Me.gbColors.Size = New System.Drawing.Size(59, 248)
        Me.gbColors.TabIndex = 28
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
        'frmGaitIndexes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 292)
        Me.Controls.Add(Me.gbTrialList)
        Me.Controls.Add(Me.pnlOverall)
        Me.Controls.Add(Me.pnlEnergy)
        Me.Controls.Add(Me.pnlPurity)
        Me.Controls.Add(Me.pnlSymmetry)
        Me.Controls.Add(Me.gbColors)
        Me.Controls.Add(Me.chkLstFormsOpen)
        Me.Name = "frmGaitIndexes"
        Me.Text = "frmGaitIndexes"
        Me.pnlSymmetry.ResumeLayout(False)
        Me.pnlSymmetry.PerformLayout()
        Me.pnlPurity.ResumeLayout(False)
        Me.pnlPurity.PerformLayout()
        Me.pnlEnergy.ResumeLayout(False)
        Me.pnlEnergy.PerformLayout()
        Me.pnlOverall.ResumeLayout(False)
        Me.pnlOverall.PerformLayout()
        Me.gbColors.ResumeLayout(False)
        Me.gbColors.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSymmetry As System.Windows.Forms.Panel
    Friend WithEvents pnlPurity As System.Windows.Forms.Panel
    Friend WithEvents pnlEnergy As System.Windows.Forms.Panel
    Friend WithEvents pnlOverall As System.Windows.Forms.Panel
    Friend WithEvents gbTrialList As System.Windows.Forms.GroupBox
    Friend WithEvents chkLstFormsOpen As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbColors As System.Windows.Forms.GroupBox
    Friend WithEvents lblColor10 As System.Windows.Forms.Label
    Friend WithEvents lblColor9 As System.Windows.Forms.Label
    Friend WithEvents lblColor8 As System.Windows.Forms.Label
    Friend WithEvents lblColor7 As System.Windows.Forms.Label
    Friend WithEvents lblColor6 As System.Windows.Forms.Label
    Friend WithEvents lblColor5 As System.Windows.Forms.Label
    Friend WithEvents lblColor4 As System.Windows.Forms.Label
    Friend WithEvents lblColor3 As System.Windows.Forms.Label
    Friend WithEvents lblColor2 As System.Windows.Forms.Label
    Friend WithEvents lblColor1 As System.Windows.Forms.Label
    Friend WithEvents lblPurity As System.Windows.Forms.Label
    Friend WithEvents lblSymmetry As System.Windows.Forms.Label
    Friend WithEvents lblEnergy As System.Windows.Forms.Label
    Friend WithEvents lblOverall As System.Windows.Forms.Label
    Friend WithEvents butPurity As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents butSymmetry As System.Windows.Forms.Button
    Friend WithEvents butEnergy As System.Windows.Forms.Button
    Friend WithEvents butOverall As System.Windows.Forms.Button
End Class
