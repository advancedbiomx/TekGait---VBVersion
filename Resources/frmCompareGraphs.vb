Public Class frmCompareGraphs

    Dim i, j, k As Integer
    Dim nNumbOfTrials, nNumbOfChecks As Integer
    Dim colTrialCheckBoxes As Collection
    Dim Color1, Color2, Color3, Color4, Color5, Color6, Color7, Color8, Color9, Color10 As Color
    Dim bAnyCheckedBoxes As Boolean = False
    Dim bFormLoading As Boolean = False
    Dim nGraphType As Integer
    Dim nMaxWidthOfCheckBoxes As Integer
    Dim nMaxYLabelWidth As Integer
    Dim nMaximumYValue As Double 'This is for the maximum Y values to graph.
    Dim colCompareDisplacementGraphs As New Collection
    Dim colCompareForceGraphs As New Collection
    Dim colCompareVelocityGraphs As New Collection
    Dim colComparePowerGraphs As New Collection
    Dim colCompareWorkGraphs As New Collection
    Dim colCompareEnergyGraphs As New Collection
    Dim colCompareSpringConstantGraphs As New Collection
    Dim colCompareGraphNames As New Collection 'This is the name of the graphs as listed in the checkboxes.
    Dim colCompareColors As New Collection
    Dim colComparePhases As New Collection

    Private Sub frmCompareGraphs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        bFormLoading = True
        Me.MdiParent = System.Windows.Forms.Application.OpenForms.Item(0) 'This sets this as a child form
        Me.Width = 0.9 * (ParentForm.Width)
        Me.Height = 0.9 * ParentForm.Height
        Me.Top = 0.05 * ParentForm.Height
        Me.Left = 0.05 * ParentForm.Width
        Me.ResizeRedraw = True

        'Add all the names of the Graphs to the checkbox list
        chkLstFormsOpen.Sorted = True
        chkLstFormsOpen.Items.Clear()
        gbColors.AutoSize = True
        nNumbOfTrials = 0
        nNumbOfChecks = 0
        For Me.i = 1 To System.Windows.Forms.Application.OpenForms.Count 'This for block puts the checkboxes on the left side.
            If System.Windows.Forms.Application.OpenForms(i - 1).Name = "frmGraph" Then
                chkLstFormsOpen.Items.Add(System.Windows.Forms.Application.OpenForms(i - 1).Text, isChecked:=False)
                nNumbOfTrials = nNumbOfTrials + 1
            End If
        Next i
        For Me.i = 1 To nNumbOfTrials
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
             If chkTrial.Width > nMaxWidthOfCheckBoxes Then nMaxWidthOfCheckBoxes = chkTrial.Width
            AddHandler chkTrial.Click, AddressOf Clicked_On_MyCheckBox 'Tells what to do if you click on one of these check boxes.
        Next i
        gbTrialList.Width = nMaxWidthOfCheckBoxes + 15
        gbGraphType.Left = gbTrialList.Left + gbTrialList.Width + 10

        For Me.i = 0 To 10 'Need 11 X labels and 11 Y labels
            Dim lblX As New Label
            Dim lblY As New Label
            lblX.Tag = "X Label " & i
            lblY.Tag = "Y Label " & i
            lblX.Text = i * 10 & "%"
            lblY.Text = i
            lblX.AutoSize = True
            lblY.AutoSize = True
            Me.Controls.Add(lblX)
            lblX.Visible = False
            Me.Controls.Add(lblY)
            lblY.Visible = False
        Next

        pnlCompGraph.Visible = False
        chkLstFormsOpen.Visible = False
        pnlCompGraph.BorderStyle = BorderStyle.FixedSingle
        Me.Height = 0.8 * MdiParent.Height
        Me.Width = 0.8 * MdiParent.Width
        Me.CenterToParent()

        pnlCompGraph.Left = gbTrialList.Left + gbTrialList.Width + nMaxYLabelWidth + 10
        pnlCompGraph.Width = Me.Width - pnlCompGraph.Left - 40
        pnlCompGraph.Top = gbGraphType.Top + gbGraphType.Height + 20
        pnlCompGraph.Height = Me.ClientRectangle.Height - pnlCompGraph.Top - 3 * lblXLabels.Height

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
        bFormLoading = False
    End Sub

    Private Sub frmCompareGraphs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Me.i = chkLstFormsOpen.Items.Count To 1 Step -1
            chkLstFormsOpen.Items.Clear()
        Next
    End Sub

    Private Sub frmCompareGraphs_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        If bFormLoading = True Then Exit Sub
        pnlCompGraph.Left = gbTrialList.Left + gbTrialList.Width + nMaximumYValue + 10
        pnlCompGraph.Width = Me.Width - pnlCompGraph.Left - 40
        pnlCompGraph.Top = gbGraphType.Top + gbGraphType.Height + 20
        pnlCompGraph.Height = Me.ClientRectangle.Height - pnlCompGraph.Top - 3 * lblXLabels.Height
        pnlCompGraph.Visible = False
        pnlCompGraph.Visible = True
    End Sub

    Private Sub radbutDisplacement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutDisplacement.Click
        nGraphType = conDisplacement_Vert
        If bFormLoading = False Then subDetermineMaximumYValue()
    End Sub

    Private Sub radbutVelocity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutVelocity.Click
        nGraphType = conVelocity_Vert
        subDetermineMaximumYValue()
    End Sub

    Private Sub radbutForce_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutForce.Click
        nGraphType = conForce_As_BW
        subDetermineMaximumYValue()
    End Sub

    Private Sub radbutPower_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutPower.Click
        nGraphType = conPower_Vert
        subDetermineMaximumYValue()
        If bAnyCheckedBoxes = True Then pnlCompGraph.Refresh()
    End Sub

    Private Sub radbutWork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutWork.Click
        nGraphType = conWork_Vert
        subDetermineMaximumYValue()
    End Sub

    Private Sub radbutEnergy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutEnergy.Click
        nGraphType = conEnergy
        subDetermineMaximumYValue()
    End Sub

    Private Sub radbutSpringConstants_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutSpringConstants.Click
        nGraphType = conSpringConstants
        subDetermineMaximumYValue()
    End Sub

    Private Sub pnlCompGraph_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlCompGraph.Resize
        If nNumbOfChecks = 0 Then Exit Sub
        pnlCompGraph.Visible = False
        pnlCompGraph.Visible = True
        For Me.k = 0 To Me.Controls.Count - 1
            If Microsoft.VisualBasic.Left(Me.Controls(k).Tag, 7) = "X Label" Then
                j = Int(Val(Me.Controls(k).Text))
                Me.Controls(k).Top = pnlCompGraph.Top + pnlCompGraph.Height + 10
                Me.Controls(k).Left = pnlCompGraph.Left + 0.01 * j * pnlCompGraph.Width - 0.5 * Me.Controls(k).Width
            End If
            If Microsoft.VisualBasic.Left(Me.Controls(k).Tag, 7) = "Y Label" Then
                j = Val(Microsoft.VisualBasic.Right(Me.Controls(k).Tag, 2))
                Me.Controls(k).Top = pnlCompGraph.Top + (1 - 0.1 * j) * pnlCompGraph.Height - 0.5 * Me.Controls(k).Height
                Me.Controls(k).Left = pnlCompGraph.Left - Me.Controls(k).Width - 5
            End If
        Next k
    End Sub

    Private Sub subSetColors()
        k = 0
        For Me.i = (gbTrialList.Controls.Count - 1) To 0 Step -1
            Dim contControl As CheckBox = gbTrialList.Controls.Item(i)
            If contControl.CheckState = CheckState.Checked Then
                k = k + 1
                Select Case k
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
                    Case 10
                        contControl.ForeColor = Color10
                End Select
            Else
                contControl.ForeColor = Color.Black
            End If
        Next i

    End Sub

    Private Sub subCheckForAnyCheckedBoxes()
        'This sub is to find out if any of the 
        bAnyCheckedBoxes = False
        Dim chk As CheckBox
        For Me.i = 1 To gbTrialList.Controls.Count
            chk = gbTrialList.Controls(i - 1)
            If chk.CheckState = CheckState.Checked Then
                bAnyCheckedBoxes = True
                Exit For
            End If
        Next
    End Sub

    Private Sub subDetermineMaximumYValue()
        If bFormLoading = True Then Exit Sub
        Dim cChk As CheckBox
        Dim sTrial As String
        Dim nArray(100) As Double
        ' Dim nNum As Double
        'Dim lblLabel As Label
        Dim g As Integer 'This is a counter used in this sub only

        If nNumbOfChecks = 0 Then Exit Sub 'You only run this sub if you have one or more trials checked.

        nMaximumYValue = 0
        For Me.j = 0 To gbTrialList.Controls.Count - 1
            cChk = gbTrialList.Controls(j)
            If cChk.CheckState = CheckState.Checked Then
                sTrial = cChk.Text
                For g = 1 To colBriefFileName.Count
                    If cChk.Text = colBriefFileName(g) Then
                        Exit For
                    End If
                Next
                Select Case nGraphType
                    Case conForce_As_BW
                        nArray = colBodyWeight(g)
                    Case conDisplacement_Vert
                        nArray = colDisplacement(g)
                    Case conVelocity_Vert
                        nArray = colVelocity(g)
                    Case conPower_Vert
                        nArray = colPower(g)
                    Case conWork_Vert
                        nArray = colWork(g)
                    Case conEnergy
                        nArray = colEnergy_Total(g)
                    Case conSpringConstants
                        nArray = colSpringConstants(g)
                End Select
                For Me.k = 0 To 100
                    If Math.Abs(nArray(k)) > nMaximumYValue Then nMaximumYValue = Math.Abs(nArray(k))
                Next k
            End If
        Next
        'Round off the absolute value to the next highest number.
        If nMaximumYValue <> 0 Then
            Select Case nGraphType
                Case conForce_As_BW
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = Int(nMaximumYValue + 1)
                    Else
                        nMaximumYValue = Int(nMaximumYValue * Lbs_To_Kgs + 1)
                    End If
                Case conDisplacement_Vert
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = 0.25 * Int(nMaximumYValue * Feet_To_In * 4 + 1)
                    Else
                        nMaximumYValue = 0.5 * Int(nMaximumYValue * Feet_To_cm * 2 + 1)
                    End If
                Case conVelocity_Vert
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = 0.5 * Int(nMaximumYValue * Feet_To_In * 2 + 1)
                    Else
                        nMaximumYValue = Int(nMaximumYValue * Feet_To_cm + 1)
                    End If
                Case conPower_Vert
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = Int(nMaximumYValue + 1)
                    Else
                        nMaximumYValue = Int(nMaximumYValue * FtLbs_To_NewtonM + 1)
                    End If
                Case conWork_Vert, conEnergy
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = 0.1 * Int(nMaximumYValue * 10 + 1)
                    Else
                        nMaximumYValue = 0.1 * Int(nMaximumYValue * 10 * FtLbs_To_NewtonM + 1)
                    End If
                Case conSpringConstants
                    If bEnglishOrMetricUnits = False Then
                        nMaximumYValue = Int(nMaximumYValue / Feet_To_In + 1)
                    Else
                        nMaximumYValue = Int(nMaximumYValue * Lbs_To_Kgs / Feet_To_cm + 1)
                    End If
            End Select
        Else
            Exit Sub
        End If

        'Put the values in the Y Labels
        nMaxYLabelWidth = 0
        For Me.i = 0 To Me.Controls.Count - 1
            If Microsoft.VisualBasic.Left(Me.Controls(i).Tag, 7) = "Y Label" Then
                k = Val(Microsoft.VisualBasic.Right(Me.Controls(i).Tag, 2))
                Me.Controls(i).Visible = True
                Select Case nGraphType
                    Case conForce_As_BW
                        Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 1)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = Me.Controls(i).Text & " Lbs."
                        Else
                            Me.Controls(i).Text = Me.Controls(i).Text & " Kgs."
                        End If
                    Case conDisplacement_Vert
                        Me.Controls(i).Text = 0.2 * nMaximumYValue * (k - 5)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 2) & " in."
                        Else
                            Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 1) & " cm."
                        End If
                    Case conVelocity_Vert
                        Me.Controls(i).Text = 0.2 * nMaximumYValue * (k - 5)
                        Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 1)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = Me.Controls(i).Text & " in./sec."
                        Else
                            Me.Controls(i).Text = Me.Controls(i).Text & " cm./sec."
                        End If
                    Case conPower_Vert
                        Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 1)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = Me.Controls(i).Text & " Ft.-lbs./sec."
                        Else
                            Me.Controls(i).Text = Me.Controls(i).Text & " N.-m./sec."
                        End If
                    Case conWork_Vert
                        Me.Controls(i).Text = FormatNumber(0.2 * nMaximumYValue * (k - 5), 2)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = Me.Controls(i).Text & " Ft.-Lbs."
                        Else
                            Me.Controls(i).Text = Me.Controls(i).Text & " N.-m."
                        End If
                    Case conEnergy
                        If k = 0 Then
                            Me.Controls(i).Text = "minimum"
                        Else
                            Me.Controls(i).Text = "+" & FormatNumber(0.1 * nMaximumYValue * k, 2)
                            If bEnglishOrMetricUnits = False Then
                                Me.Controls(i).Text = Me.Controls(i).Text & " Ft.-Lbs."
                            Else
                                Me.Controls(i).Text = Me.Controls(i).Text & " N.-m."
                            End If
                        End If
                        
                    Case conSpringConstants
                        Me.Controls(i).Text = FormatNumber(0.1 * nMaximumYValue * k, 1)
                        If bEnglishOrMetricUnits = False Then
                            Me.Controls(i).Text = Me.Controls(i).Text & " Lbs./in."
                        Else
                            Me.Controls(i).Text = Me.Controls(i).Text & " Kg./cm."
                        End If
                End Select
                If Me.Controls(i).Width > nMaxYLabelWidth Then nMaxYLabelWidth = Me.Controls(i).Width 'Finds the maximum width of any Y label
            End If
        Next i

        'First find the maximum width of a Y label then reset the left side of the graph panel and the width of the graph panel.

        pnlCompGraph.Left = gbTrialList.Left + gbTrialList.Width + 10 + nMaxYLabelWidth
        pnlCompGraph.Width = Me.Width - pnlCompGraph.Left - 40

    End Sub

    Private Sub Clicked_On_MyCheckBox(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TypeOf sender Is CheckBox Then
        Else
            Exit Sub
        End If

        Dim cChk As CheckBox
        cChk = sender
        subSetColors()
        If cChk.CheckState = CheckState.Checked Then
            nNumbOfChecks = nNumbOfChecks + 1
            If bAnyCheckedBoxes = False Then
                bAnyCheckedBoxes = True
                pnlCompGraph.Visible = True
                For Me.i = 0 To Me.Controls.Count - 1
                    If Microsoft.VisualBasic.Left(Me.Controls(i).Tag, 7) = "X Label" Then
                        Me.Controls(i).Visible = True
                    ElseIf Microsoft.VisualBasic.Left(Me.Controls(i).Tag, 7) = "Y Label" Then
                        Me.Controls(i).Visible = True
                    End If
                Next
            End If
            subDetermineMaximumYValue()
        Else
            nNumbOfChecks = nNumbOfChecks - 1
            If nNumbOfChecks = 0 Then
                bAnyCheckedBoxes = False
                pnlCompGraph.Visible = False
                For Me.i = 0 To Me.Controls.Count - 1
                    If Microsoft.VisualBasic.Left(Me.Controls(i).Tag, 7) = "X Label" Then
                        Me.Controls(i).Visible = False
                    ElseIf Microsoft.VisualBasic.Left(Me.Controls(i).Tag, 7) = "Y Label" Then
                        Me.Controls(i).Visible = False
                    End If
                Next
            Else 'If you've unchecked a box, but you still have boxes that are checked.
                subDetermineMaximumYValue()
            End If
        End If

        'Put all the checkboxed arrays in their respective collections.
        If colCompareColors.Count <> 0 Then
            colCompareColors.Clear()
            colCompareDisplacementGraphs.Clear()
            colCompareEnergyGraphs.Clear()
            colCompareForceGraphs.Clear()
            colCompareGraphNames.Clear()
            colComparePowerGraphs.Clear()
            colCompareSpringConstantGraphs.Clear()
            colCompareVelocityGraphs.Clear()
            colCompareWorkGraphs.Clear()
            colComparePhases.Clear()
        End If

        'Now add all the checked boxes arrays to their respective collections.
        If nNumbOfChecks <> 0 Then
            For Me.i = 0 To gbTrialList.Controls.Count - 1
                cChk = gbTrialList.Controls(i)
                If cChk.CheckState = CheckState.Checked Then
                    j = 1
                    If j <= colBriefFileName.Count Then
                        Do Until colBriefFileName(j) = cChk.Text
                            j = j + 1
                        Loop
                    End If
                    colCompareColors.Add(cChk.ForeColor)
                    colCompareDisplacementGraphs.Add(colDisplacement(j))
                    colCompareEnergyGraphs.Add(colEnergy_Total(j))
                    colCompareForceGraphs.Add(colBodyWeight(j))
                    colCompareGraphNames.Add(colBriefFileName(j))
                    colComparePowerGraphs.Add(colPower(j))
                    colCompareSpringConstantGraphs.Add(colSpringConstants(j))
                    colCompareVelocityGraphs.Add(colVelocity(j))
                    colCompareWorkGraphs.Add(colWork(j))
                    colComparePhases.Add(colGaitPhase(j))
                End If
            Next i
        End If

        pnlCompGraph.Refresh()
    End Sub

    Private Sub pnlCompGraph_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlCompGraph.Paint
        Dim graphixC As Graphics = e.Graphics
        'Dim sizeGraphix As SizeF
        'Dim lblLbl As Label
        Dim nPixelsPerPctGait As Double
        Dim nPixelsPerYUnit As Double
        Dim Point1 As Point
        Dim Point2 As Point
        Dim penPix As New Pen(colorGrid)
        'Dim pnlBorder As SizeF

        'STEP 1: Calculate the nPixelsPerPct for Gait. and the npixelsPerYUnit
        nPixelsPerPctGait = 0.01 * pnlCompGraph.ClientRectangle.Width
        Select Case nGraphType
            Case conEnergy, conSpringConstants
                nPixelsPerYUnit = pnlCompGraph.ClientRectangle.Height / nMaximumYValue
            Case Else
                nPixelsPerYUnit = 0.5 * pnlCompGraph.ClientRectangle.Height / nMaximumYValue
        End Select

        'STEP 2:  Draw the Vertical reference lines at each Percent of the Gait cycle.
        graphixC.Clear(pnlCompGraph.BackColor)
        penPix.Color = colorGrid
        penPix.Width = 2
        penPix.DashStyle = Drawing2D.DashStyle.Dot
        Point1.Y = 0
        Point2.Y = pnlCompGraph.Height
        For Me.i = 0 To 10
            Point1.X = 0.1 * i * pnlCompGraph.ClientRectangle.Width
            graphixC.DrawLine(penPix, Point1.X, Point1.Y, Point1.X, Point2.Y)
        Next i

        'STEP 3 'Draw the horizontal lines.
        Point1.X = 0
        Point2.X = pnlCompGraph.Width
        For Me.i = 0 To 10
            Point1.Y = 0.1 * i * pnlCompGraph.ClientRectangle.Height
            If i = 5 Then
                Select Case nGraphType
                    Case conEnergy, conSpringConstants
                        penPix.DashStyle = Drawing2D.DashStyle.Solid
                    Case Else
                        penPix.DashStyle = Drawing2D.DashStyle.Dot
                End Select
            Else
                penPix.DashStyle = Drawing2D.DashStyle.Dot
            End If
            graphixC.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point1.Y)
        Next i

        'STEP 4:  Draw the lines
        If nNumbOfChecks <> 0 Then
            For Me.i = 1 To nNumbOfChecks
                penPix.Color = colCompareColors(i)
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                For Me.j = 1 To 100
                    Point1.X = (j - 1) * nPixelsPerPctGait
                    Point2.X = j * nPixelsPerPctGait
                    Select Case nGraphType
                        Case conDisplacement_Vert
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = 0.5 * pnlCompGraph.Height - colCompareDisplacementGraphs(i)(j - 1) * Feet_To_In * nPixelsPerYUnit
                                    Point2.Y = 0.5 * pnlCompGraph.Height - colCompareDisplacementGraphs(i)(j) * Feet_To_In * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conVelocity_Vert
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = 0.5 * pnlCompGraph.Height - colCompareVelocityGraphs(i)(j - 1) * Feet_To_In * nPixelsPerYUnit
                                    Point2.Y = 0.5 * pnlCompGraph.Height - colCompareVelocityGraphs(i)(j) * Feet_To_In * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conForce_As_BW
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = 0.5 * pnlCompGraph.Height - colCompareForceGraphs(i)(j - 1) * nPixelsPerYUnit
                                    Point2.Y = 0.5 * pnlCompGraph.Height - colCompareForceGraphs(i)(j) * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conPower_Vert
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = 0.5 * pnlCompGraph.Height - colComparePowerGraphs(i)(j - 1) * nPixelsPerYUnit
                                    Point2.Y = 0.5 * pnlCompGraph.Height - colComparePowerGraphs(i)(j) * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conWork_Vert
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = 0.5 * pnlCompGraph.Height - colCompareWorkGraphs(i)(j - 1) * nPixelsPerYUnit
                                    Point2.Y = 0.5 * pnlCompGraph.Height - colCompareWorkGraphs(i)(j) * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conEnergy
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = pnlCompGraph.Height - colCompareEnergyGraphs(i)(j - 1) * nPixelsPerYUnit
                                    Point2.Y = pnlCompGraph.Height - colCompareEnergyGraphs(i)(j) * nPixelsPerYUnit
                                Case True
                            End Select
                        Case conSpringConstants
                            Select Case bEnglishOrMetricUnits
                                Case False 'The units are in in./sec.
                                    Point1.Y = pnlCompGraph.Height - (colCompareSpringConstantGraphs(i)(j - 1) / Feet_To_In) * nPixelsPerYUnit
                                    Point2.Y = pnlCompGraph.Height - (colCompareSpringConstantGraphs(i)(j) / Feet_To_In) * nPixelsPerYUnit
                                Case True
                            End Select
                    End Select
                    Select colComparePhases(i)(j)
                        Case con_L_Double_Support, con_R_Double_Support
                            If colComparePhases(i)(j) = colComparePhases(i)(j - 1) Then
                                penPix.Width = 6
                            Else
                                penPix.Width = 2
                            End If
                        Case Else
                            penPix.Width = 2
                    End Select
                    graphixC.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                Next j
            Next i
        End If

    End Sub

End Class