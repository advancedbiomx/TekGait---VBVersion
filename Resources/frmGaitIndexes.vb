Public Class frmGaitIndexes
    Dim i, j, k As Integer
    Dim nNumbOfTrials, nNumbOfChecks As Integer
    Dim colTrialCheckBoxes As Collection
    Dim Color1, Color2, Color3, Color4, Color5, Color6, Color7, Color8, Color9, Color10 As Color
    Dim bAnyCheckedBoxes As Boolean = False
    Dim bFormLoading As Boolean = False
    Dim nMaxWidthOfCheckBoxes, nMaxHeightOfCheckBoxes As Integer
    Dim clntRect As Rectangle
    Dim colPurity As New Collection
    Dim colCoP As New Collection
    Dim colSymmetry As New Collection
    Dim colEnergy As New Collection
    Dim colOverall As New Collection

    Private Sub frmGaitIndexes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        bFormLoading = True

        Me.MdiParent = System.Windows.Forms.Application.OpenForms.Item(0) 'This sets this as a child form
        Me.Width = 0.9 * (ParentForm.Width)
        Me.Height = 0.7 * ParentForm.Height
        Me.Top = 0.05 * ParentForm.Height
        Me.Left = 0.05 * ParentForm.Width
        Me.ResizeRedraw = True

        lblSymmetry.Text = "CoP Efficiency Index"
        lblOverall.Text = "Overall Gait Index"

        'Add all the names of the Graphs to the checkbox list
        chkLstFormsOpen.Sorted = True
        chkLstFormsOpen.Items.Clear()
        nNumbOfTrials = 0
        nNumbOfChecks = 0
        For Me.i = 1 To System.Windows.Forms.Application.OpenForms.Count 'This for block puts the checkboxes on the left side.
            If System.Windows.Forms.Application.OpenForms(i - 1).Name = "frmGraph" Then
                chkLstFormsOpen.Items.Add(System.Windows.Forms.Application.OpenForms(i - 1).Text, isChecked:=False)
                nNumbOfTrials = nNumbOfTrials + 1
            End If
        Next i
        For Me.i = 1 To nNumbOfTrials 'List the checkboxes
            Dim chkTrial As New CheckBox
            chkTrial.Name = "chkBox" & nNumbOfTrials
            chkTrial.Tag = nNumbOfTrials
            chkTrial.AutoSize = True
            chkTrial.Parent = gbTrialList
            chkTrial.Left = 5
            chkTrial.Top = 12 + i * 1.1 * chkTrial.Height
            chkTrial.Visible = True
            chkTrial.BringToFront()
            chkTrial.Text = chkLstFormsOpen.Items.Item(i - 1)
            'chkTrial.Text = System.Windows.Forms.Application.OpenForms(i - 1).Text
            nMaxHeightOfCheckBoxes = chkTrial.Height
            If chkTrial.Width > nMaxWidthOfCheckBoxes Then nMaxWidthOfCheckBoxes = chkTrial.Width
            AddHandler chkTrial.Click, AddressOf Clicked_On_MyCheckBox 'Tells what to do if you click on one of these check boxes.
        Next i

        gbTrialList.Width = nMaxWidthOfCheckBoxes + 15
        gbTrialList.Height = 30 + nNumbOfTrials * 1.1 * nMaxHeightOfCheckBoxes
        clntRect.X = gbTrialList.Left + gbTrialList.Width + 10
        clntRect.Width = Me.ClientRectangle.Width - clntRect.X - 20
        clntRect.Y = gbTrialList.Top
        clntRect.Height = Me.ClientRectangle.Height - 2 * gbTrialList.Top

        Color1 = lblColor1.ForeColor
        Color2 = lblColor2.ForeColor
        Color3 = lblColor3.ForeColor
        Color4 = lblColor4.ForeColor
        Color5 = lblColor5.ForeColor
        Color6 = lblColor6.ForeColor
        Color7 = lblColor7.ForeColor
        Color8 = lblColor8.ForeColor
        Color9 = lblColor9.ForeColor
        Color10 = lblColor10.ForeColor

        subPlaceAll4Panels()
        bFormLoading = False
    End Sub

    Private Sub frmGaitIndexes_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        If bFormLoading = True Then Exit Sub
        clntRect.X = gbTrialList.Left + gbTrialList.Width + 10
        clntRect.Width = Me.ClientRectangle.Width - clntRect.X - 10
        clntRect.Y = gbTrialList.Top
        clntRect.Height = Me.ClientRectangle.Height - 2 * gbTrialList.Top
    End Sub

    Private Sub pnlPurity_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlPurity.Resize
        lblPurity.Left = 0.5 * (pnlPurity.ClientRectangle.Width - lblPurity.Width)
        lblPurity.Top = 3
    End Sub

    Private Sub pnlSymmetry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlSymmetry.Resize
        lblSymmetry.Left = 0.5 * (pnlSymmetry.ClientRectangle.Width - lblSymmetry.Width)
        lblSymmetry.Top = 3
    End Sub

    Private Sub pnlEnergy_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlEnergy.Resize
        lblEnergy.Left = 0.5 * (pnlEnergy.ClientRectangle.Width - lblEnergy.Width)
        lblEnergy.Top = 3
    End Sub

    Private Sub pnlOverall_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlOverall.Resize
        lblOverall.Left = 0.5 * (pnlOverall.ClientRectangle.Width - lblOverall.Width)
        lblOverall.Top = 3
    End Sub

    Private Sub butPurity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPurity.Click
        If butPurity.Tag = "Small" Then
            butPurity.ImageIndex = 1
            butPurity.Tag = "Large"
        Else
            butPurity.ImageIndex = 0
            butPurity.Tag = "Small"
        End If
    End Sub

    Private Sub butSymmetry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSymmetry.Click
        If butSymmetry.Tag = "Small" Then
            butSymmetry.ImageIndex = 1
            butSymmetry.Tag = "Large"
        Else
            butSymmetry.ImageIndex = 0
            butSymmetry.Tag = "Small"
        End If
    End Sub

    Private Sub subPlaceAll4Panels()
        pnlPurity.Visible = True
        pnlSymmetry.Visible = True
        pnlEnergy.Visible = True
        pnlOverall.Visible = True

        pnlPurity.Left = clntRect.Left + 2
        pnlPurity.Top = clntRect.Top
        pnlPurity.Width = 0.5 * clntRect.Width - 4
        pnlPurity.Height = 0.5 * clntRect.Height - 2

        pnlSymmetry.Left = pnlPurity.Left + pnlPurity.Width + 4
        pnlSymmetry.Top = pnlPurity.Top
        pnlSymmetry.Width = pnlPurity.Width
        pnlSymmetry.Height = pnlPurity.Height

        pnlEnergy.Left = pnlPurity.Left
        pnlEnergy.Top = pnlPurity.Top + pnlPurity.Height + 4
        pnlEnergy.Width = pnlPurity.Width
        pnlEnergy.Height = pnlPurity.Height

        pnlOverall.Left = pnlSymmetry.Left
        pnlOverall.Top = pnlEnergy.Top
        pnlOverall.Width = pnlSymmetry.Width
        pnlOverall.Height = pnlSymmetry.Height

        butPurity.ImageIndex = 0
        butPurity.Location = New Point(0, 0)
        butPurity.ImageAlign = ContentAlignment.BottomRight
        butSymmetry.ImageIndex = 0
        butSymmetry.Location = New Point(0, 0)
        butSymmetry.ImageAlign = ContentAlignment.BottomRight
        butEnergy.ImageIndex = 0
        butEnergy.Location = New Point(0, 0)
        butEnergy.ImageAlign = ContentAlignment.BottomRight
        butOverall.ImageIndex = 0
        butOverall.Location = New Point(0, 0)
        butOverall.ImageAlign = ContentAlignment.BottomRight

        pnlPurity.Tag = "Small"
        pnlSymmetry.Tag = "Small"
        pnlEnergy.Tag = "Small"
        pnlOverall.Tag = "Small"

    End Sub

    Private Sub Clicked_On_MyCheckBox(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TypeOf sender Is CheckBox Then
            Dim chkChkBox As CheckBox = sender
            Dim tempGI As New structGaitIndices
            Dim sShortName As String = ""
            Dim sLongName As String = ""
            i = 0
            Do Until sShortName <> ""
                i = i + 1
                If colBriefFileName(i) = chkChkBox.Text Then sShortName = colBriefFileName(i)
            Loop
            sLongName = colFileName(i)
            tempGI = colGI(i)
            If chkChkBox.CheckState = CheckState.Checked Then
                nNumbOfChecks = nNumbOfChecks + 1
                colPurity.Add(tempGI.Purity, sShortName)
                colCoP.Add(tempGI.CoP, sShortName)
                colSymmetry.Add(tempGI.Symmetry, sShortName)
                colEnergy.Add(tempGI.Energy, sShortName)
                colOverall.Add(tempGI.Overall, sShortName)
            Else
                If nNumbOfChecks <> 0 Then
                    colPurity.Remove(sShortName)
                    colCoP.Remove(sShortName)
                    colSymmetry.Remove(sShortName)
                    colEnergy.Remove(sShortName)
                    colOverall.Remove(sShortName)
                End If
            End If
        Else
            Exit Sub
        End If

        subSetColors()
        pnlPurity.Refresh()
        pnlSymmetry.Refresh()
        pnlEnergy.Refresh()
        pnlOverall.Refresh()

    End Sub

    Private Sub subSetColors()
        Dim nDiv, nRemainder As Integer
        k = 0
        For Me.i = (gbTrialList.Controls.Count - 1) To 0 Step -1
            Dim contControl As CheckBox = gbTrialList.Controls.Item(i)
            If contControl.CheckState = CheckState.Checked Then
                k = k + 1
                nDiv = Math.DivRem(k, 10, nRemainder)
                Select Case nRemainder
                    Case 1
                        contControl.ForeColor = Color1
                    Case 2
                        contControl.ForeColor = Color2
                    Case 3
                        contControl.ForeColor = Color3
                    Case 4
                        contControl.ForeColor = Color4
                    Case 5
                        contControl.ForeColor = Color5
                    Case 6
                        contControl.ForeColor = Color6
                    Case 7
                        contControl.ForeColor = Color7
                    Case 8
                        contControl.ForeColor = Color8
                    Case 9
                        contControl.ForeColor = Color9
                    Case 0
                        contControl.ForeColor = Color10
                End Select
            Else
                contControl.ForeColor = Color.Black
            End If
        Next i

    End Sub
    Private Sub pnlPurity_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlPurity.Paint

        Dim chkChk As CheckBox
        If bFormLoading = True Then Exit Sub
        If pnlPurity.Visible = False Then Exit Sub
        If nNumbOfChecks = 0 Then Exit Sub

        Dim grP As Graphics = e.Graphics
        grP.Clear(pnlPurity.BackColor)
        Dim penP As New Pen(pnlPurity.BackColor)
        Dim rectB As Rectangle
        Dim rectG As Rectangle
        Dim sizFont As SizeF
        Dim fontG As Font
        Dim brushG As New Drawing.SolidBrush(pnlPurity.BackColor)
        Dim colorG As Color

        'STEP 1: Identify the maximum width and height of the graphing area
        rectB.X = 0
        rectB.Y = lblPurity.Top + lblPurity.Height + 3
        rectB.Width = pnlPurity.ClientRectangle.Width / (nNumbOfChecks * 2 + 1)
        rectB.Height = pnlPurity.ClientRectangle.Height - rectB.Y

        'STEP 2: Draw a shaded area indicating 100%
        For Me.i = 0 To 10
            colorG = Color.FromArgb(20 + i * 19, 0, 0, 0)
            brushG.Color = colorG
            rectG = rectB
            rectG.Width = pnlPurity.ClientRectangle.Width
            rectG.Y = rectB.Y + (1 - 0.1 * i) * rectB.Height
            rectG.Height = 0.1 * rectB.Height
            grP.FillRectangle(brushG, rectG)
        Next i

        'STEP 3: Put the bar on the graph
        i = 0
        fontG = lblPurity.Font
        For Me.j = gbTrialList.Controls.Count To 1 Step -1
            chkChk = gbTrialList.Controls.Item(j - 1)
            If chkChk.CheckState = CheckState.Checked And i <= nNumbOfChecks Then
                i = i + 1
                rectG = rectB
                rectG.Y = rectB.Y + (1 - colPurity(chkChk.Text)) * rectB.Height
                rectG.Height = rectB.Height * colPurity(chkChk.Text)
                rectG.X = (2 * (i - 1) + 1) * rectB.Width
                penP.Color = chkChk.ForeColor
                brushG.Color = chkChk.ForeColor
                grP.FillRectangle(brushG, rectG)
                sizFont = grP.MeasureString(FormatNumber(colPurity(chkChk.Text), ".000"), fontG)
                Do Until sizFont.Width < rectB.Width
                    fontG = New Font(lblPurity.Font.Name, fontG.SizeInPoints - 1, lblPurity.Font.Style)
                    sizFont = grP.MeasureString(FormatNumber(colPurity(chkChk.Text), ".000"), fontG)
                Loop
                'Put the label on the bar, just below the color line
                If rectG.Y + sizFont.Height < pnlPurity.ClientRectangle.Height Then
                    grP.DrawString(FormatNumber(colPurity(chkChk.Text), 2), fontG, Brushes.AntiqueWhite, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y)
                Else
                    grP.DrawString(FormatNumber(colPurity(chkChk.Text), 2), fontG, brushG, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y - 1.05 * sizFont.Height)
                End If
            End If
        Next j

    End Sub

    Private Sub pnlSymmetry_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlSymmetry.Paint
        Dim chkChk As CheckBox
        If bFormLoading = True Then Exit Sub
        If pnlSymmetry.Visible = False Then Exit Sub
        If nNumbOfChecks = 0 Then Exit Sub

        Dim grP As Graphics = e.Graphics
        grP.Clear(pnlSymmetry.BackColor)
        Dim penP As New Pen(pnlSymmetry.BackColor)
        Dim rectB As Rectangle
        Dim rectG As Rectangle
        Dim sizFont As SizeF
        Dim fontG As Font
        Dim brushG As New Drawing.SolidBrush(pnlSymmetry.BackColor)
        Dim colorG As Color

        'STEP 1: Identify the maximum width and height of the graphing area
        rectB.X = 0
        rectB.Y = lblSymmetry.Top + lblSymmetry.Height + 3
        rectB.Width = pnlSymmetry.ClientRectangle.Width / (nNumbOfChecks * 2 + 1)
        rectB.Height = pnlSymmetry.ClientRectangle.Height - rectB.Y

        'STEP 2: Draw a shaded area indicating 100%
        For Me.i = 0 To 10
            colorG = Color.FromArgb(20 + i * 19, 0, 0, 0)
            brushG.Color = colorG
            rectG = rectB
            rectG.Width = pnlSymmetry.ClientRectangle.Width
            rectG.Y = rectB.Y + (1 - 0.1 * i) * rectB.Height
            rectG.Height = 0.1 * rectB.Height
            grP.FillRectangle(brushG, rectG)
        Next i

        'STEP 3: Put the bar on the graph
        i = 0
        fontG = lblSymmetry.Font
        For Me.j = gbTrialList.Controls.Count To 1 Step -1
            chkChk = gbTrialList.Controls.Item(j - 1)
            If chkChk.CheckState = CheckState.Checked And i <= nNumbOfChecks Then
                i = i + 1
                rectG = rectB
                rectG.Y = rectB.Y + (1 - colCoP(chkChk.Text)) * rectB.Height
                rectG.Height = rectB.Height * colCoP(chkChk.Text)
                rectG.X = (2 * (i - 1) + 1) * rectB.Width
                penP.Color = chkChk.ForeColor
                brushG.Color = chkChk.ForeColor
                grP.FillRectangle(brushG, rectG)
                sizFont = grP.MeasureString(FormatNumber(colCoP(chkChk.Text), ".000"), fontG)
                Do Until sizFont.Width < rectB.Width
                    fontG = New Font(lblSymmetry.Font.Name, fontG.SizeInPoints - 1, lblSymmetry.Font.Style)
                    sizFont = grP.MeasureString(FormatNumber(colCoP(chkChk.Text), ".000"), fontG)
                Loop
                'Put the label on the bar, just below the color line
                If rectG.Y + sizFont.Height < pnlSymmetry.ClientRectangle.Height Then
                    grP.DrawString(FormatNumber(colCoP(chkChk.Text), 2), fontG, Brushes.AntiqueWhite, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y)
                Else
                    grP.DrawString(FormatNumber(colCoP(chkChk.Text), 2), fontG, brushG, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y - 1.05 * sizFont.Height)
                End If
            End If
        Next j
    End Sub

    Private Sub pnlEnergy_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlEnergy.Paint
        Dim chkChk As CheckBox
        If bFormLoading = True Then Exit Sub
        If pnlEnergy.Visible = False Then Exit Sub
        If nNumbOfChecks = 0 Then Exit Sub

        Dim grP As Graphics = e.Graphics
        grP.Clear(pnlEnergy.BackColor)
        Dim penP As New Pen(pnlEnergy.BackColor)
        Dim rectB As Rectangle
        Dim rectG As Rectangle
        Dim sizFont As SizeF
        Dim fontG As Font
        Dim brushG As New Drawing.SolidBrush(pnlEnergy.BackColor)
        Dim colorG As Color

        'STEP 1: Identify the maximum width and height of the graphing area
        rectB.X = 0
        rectB.Y = lblEnergy.Top + lblEnergy.Height + 3
        rectB.Width = pnlEnergy.ClientRectangle.Width / (nNumbOfChecks * 2 + 1)
        rectB.Height = pnlEnergy.ClientRectangle.Height - rectB.Y

        'STEP 2: Draw a shaded area indicating 100%
        For Me.i = 0 To 10
            colorG = Color.FromArgb(20 + i * 19, 0, 0, 0)
            brushG.Color = colorG
            rectG = rectB
            rectG.Width = pnlEnergy.ClientRectangle.Width
            rectG.Y = rectB.Y + (1 - 0.1 * i) * rectB.Height
            rectG.Height = 0.1 * rectB.Height
            grP.FillRectangle(brushG, rectG)
        Next i

        'STEP 3: Put the bar on the graph
        i = 0
        fontG = lblEnergy.Font
        For Me.j = gbTrialList.Controls.Count To 1 Step -1
            chkChk = gbTrialList.Controls.Item(j - 1)
            If chkChk.CheckState = CheckState.Checked And i <= nNumbOfChecks Then
                i = i + 1
                rectG = rectB
                rectG.Y = rectB.Y + (1 - colEnergy(chkChk.Text)) * rectB.Height
                rectG.Height = rectB.Height * colEnergy(chkChk.Text)
                rectG.X = (2 * (i - 1) + 1) * rectB.Width
                penP.Color = chkChk.ForeColor
                brushG.Color = chkChk.ForeColor
                grP.FillRectangle(brushG, rectG)
                sizFont = grP.MeasureString(FormatNumber(colEnergy(chkChk.Text), ".000"), fontG)
                Do Until sizFont.Width < rectB.Width
                    fontG = New Font(lblEnergy.Font.Name, fontG.SizeInPoints - 1, lblEnergy.Font.Style)
                    sizFont = grP.MeasureString(FormatNumber(colEnergy(chkChk.Text), ".000"), fontG)
                Loop
                'Put the label on the bar, just below the color line
                If rectG.Y + sizFont.Height < pnlEnergy.ClientRectangle.Height Then
                    grP.DrawString(FormatNumber(colEnergy(chkChk.Text), 2), fontG, Brushes.AntiqueWhite, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y)
                Else
                    grP.DrawString(FormatNumber(colEnergy(chkChk.Text), 2), fontG, brushG, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y - 1.05 * sizFont.Height)
                End If
            End If
        Next j
    End Sub

    Private Sub pnlOverall_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlOverall.Paint
        Dim chkChk As CheckBox
        If bFormLoading = True Then Exit Sub
        If pnlOverall.Visible = False Then Exit Sub
        If nNumbOfChecks = 0 Then Exit Sub

        Dim grP As Graphics = e.Graphics
        grP.Clear(pnlOverall.BackColor)
        Dim penP As New Pen(pnlOverall.BackColor)
        Dim rectB As Rectangle
        Dim rectG As Rectangle
        Dim sizFont As SizeF
        Dim fontG As Font
        Dim brushG As New Drawing.SolidBrush(pnlOverall.BackColor)
        Dim colorG As Color

        'STEP 1: Identify the maximum width and height of the graphing area
        rectB.X = 0
        rectB.Y = lblOverall.Top + lblOverall.Height + 3
        rectB.Width = pnlOverall.ClientRectangle.Width / (nNumbOfChecks * 2 + 1)
        rectB.Height = pnlOverall.ClientRectangle.Height - rectB.Y

        'STEP 2: Draw a shaded area indicating 100%
        For Me.i = 0 To 10
            colorG = Color.FromArgb(20 + i * 19, 0, 0, 0)
            brushG.Color = colorG
            rectG = rectB
            rectG.Width = pnlOverall.ClientRectangle.Width
            rectG.Y = rectB.Y + (1 - 0.1 * i) * rectB.Height
            rectG.Height = 0.1 * rectB.Height
            grP.FillRectangle(brushG, rectG)
        Next i

        'STEP 3: Put the bar on the graph
        i = 0
        fontG = lblOverall.Font
        For Me.j = gbTrialList.Controls.Count To 1 Step -1
            chkChk = gbTrialList.Controls.Item(j - 1)
            If chkChk.CheckState = CheckState.Checked And i <= nNumbOfChecks Then
                i = i + 1
                rectG = rectB
                rectG.Y = rectB.Y + (1 - colOverall(chkChk.Text)) * rectB.Height
                rectG.Height = rectB.Height * colOverall(chkChk.Text)
                rectG.X = (2 * (i - 1) + 1) * rectB.Width
                penP.Color = chkChk.ForeColor
                brushG.Color = chkChk.ForeColor
                grP.FillRectangle(brushG, rectG)
                sizFont = grP.MeasureString(FormatNumber(colOverall(chkChk.Text), ".000"), fontG)
                Do Until sizFont.Width < rectB.Width
                    fontG = New Font(lblOverall.Font.Name, fontG.SizeInPoints - 1, lblOverall.Font.Style)
                    sizFont = grP.MeasureString(FormatNumber(colOverall(chkChk.Text), ".000"), fontG)
                Loop
                'Put the label on the bar, just below the color line
                If rectG.Y + sizFont.Height < pnlOverall.ClientRectangle.Height Then
                    grP.DrawString(FormatNumber(colOverall(chkChk.Text), 2), fontG, Brushes.AntiqueWhite, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y)
                Else
                    grP.DrawString(FormatNumber(colOverall(chkChk.Text), 2), fontG, brushG, rectG.X + 0.5 * (rectG.Width - sizFont.Width), rectG.Y - 1.05 * sizFont.Height)
                End If
            End If
        Next j
    End Sub
End Class