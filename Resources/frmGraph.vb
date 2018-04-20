Option Explicit On

Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Public Class frmGraph

    Dim i, j, k, g, n, p, q, r, s As Integer 'To be used as counters.
    Dim t As Double 'This double can be used for all types of calculations, when you need a counter that is also a decimal

    'The following variables are transferred from the OpenFileForm
    Dim sPatientFileName As String
    Dim sLeftFileName, sRightFileName As String 'These divide the sPatientFileName into Left and Right
    Dim sFirstName, sLastName, sChartNumber As String
    Dim DateAndTime As Date
    Dim bWearingCustomShoes, bWearingOrthotics As Boolean
    Dim bHeelLift As Boolean
    Dim sHeelLiftSide, nHeelLiftHeight As String
    Dim bDiabetic, bNeuropathic, bPVD As Boolean
    Dim sComments, sOrthoticRx As String
    Dim nNumberOfStrides As Integer 'This tells us the total number of strides that is sampled for this trial.
    Dim bWalkingOrRunning As Boolean 'If this is a walking trial, it will be FALSE, if running it will be TRUE

    Dim bFormLoading As Boolean = True 'When the form starts loading, this flag is set to true, other events checks it and if true, do not fire.
    Dim bReScaleLoading As Boolean
    Dim bPanelDrawOrder As Boolean = False
    Dim bCopyScreen As Boolean = False
    Dim bFileSelected = False 'This flag is for if you decide not to open a file, then it doesn't go through the formclosing sub.

    Dim FootWidthLeft, FootWidthRight, FootLengthLeft, FootLengthRight As Double 'The foot length and width for each foot.
    Dim AverageWeight As Double
    Dim BodyMass As Double
    Dim Cadence As Single
    Dim nCriticalDampening As Double
    Dim nPctSingleSupport_L, nPctSingleSupport_R As Integer
    Dim nPctDoubleSupport_L, nPctDoubleSupport_R As Integer
    Dim nPctFloat_L, nPctFloat_R As Integer
    Dim nStancePhase_PctSupport_L(2) As Integer 'This array is for dividing the Left Stance phase into 3 parts, double support(0), single support(1), and another double support(2)
    Dim nStancePhase_PctSupport_R(2) As Integer 'This arrasy for dividing the Right Stance phase into 3 parts, double support (0), single support(1), and another double support(2)

    'The following arrays will be calculated and used in this trial
    Dim AverageData(100) As structPercentData
    Dim GI As structGaitIndices 'this is for the 4 gait indices.
    Dim CoPSymmetryIndex As Single
    Dim CoPPurityIndex_L As Single
    Dim CoPPurityIndex_R As Single
    Dim CoPPurityIndex_Avg As Single
    Dim SpringConsistencyIndex As Single
    Dim arAllLeftForces(,) As Double
    Dim arAllRightForces(,) As Double
    Dim arAllTotalForces(,) As Double
    Dim arAllTimes(,) As Double
    Dim arAvgLeftSenselForce(100, 59, 20) 'This array is for the force on each sensel Left foot, for average gait cycle
    Dim arAvgRightSenselForce(100, 59, 20) 'This array is for the force on each sensel Right foot, for an average gait cycle
    Dim arLeftForce(100) As Double 'This is the array for the total force under the Left foot, for average gait cycle.
    Dim arRightForce(100) As Double 'This is the array for the total force under the Right foot, for average gait cycle
    Dim arTotalForce(100) As Double 'This is the array for the total force under both feet, for average gait cycle
    Dim arBodyWeight(100) As Double 'This contains the force in terms of body weight, for average gait cycle
    Dim arBodyWeightPct(100) As Double 'This contains the % of body weight for each percent of body weight
    Dim arGaitPhase(100) As Double 'This array says what phase each pecent the gait cycle is in.
    Dim arGaitTime(100) As Double 'This is the time for each% of the gait cycle
    Dim arCoMVelocity(100) As Double 'This array is for the vertical velocity of the CoM
    Dim arDisplacement(100) As Double 'This is an array for the Displacement.
    Dim arPower(100) As Double 'This is an array for the Power
    Dim arWork(100) As Double 'This is an array for the Work
    Dim arEnergy_Potential(100) As Double
    Dim arEnergy_Kinetic(100) As Double
    Dim arEnergy_Total(100) As Double
    Dim arSpringConstants(100) As Double 'This array holds the body spring constants for each % of the gait cycle
    Dim arCoPLoc_ML_L(100) As Double 'This is the location of the center of Pressure - Left Foot
    Dim arCoPLoc_ML_R(100) As Double 'This is the location of the center of Pressure - Right Foot
    Dim arCoPLoc_AP_L(100) As Double 'This is the location of the AP center of Pressure - Left Foot
    Dim arCoPLoc_Stance_AP_L(100) As Double 'This is the location of the AP center of Pressure, normalized to the Stance Period of Gait on the Left.
    Dim arCoPLoc_AP_R(100) As Double 'This is the location of the AP CoP - Right Foot
    Dim arCoPLoc_Stance_AP_R(100) As Double 'This is the location of the AP center of Pressure, normalized to the Stance Period of Gait on the Right
    Dim arCoPVel_ML_L(100) As Double 'This is the velocity of the ML CoP - Left foot
    Dim arCoPVel_ML_R(100) As Double 'This is the velocity of the ML CoP - Right foot
    Dim arCoPVel_AP_L(100) As Double 'This is the velocity of the AP CoP- Left foot
    Dim arCoPVel_Stance_AP_L(100) As Double 'This is the velocity of the AP CoP - Left Foot, normalized to the Stance Period of Gait.
    Dim arCoPVel_AP_R(100) As Double 'This is the velocity of the AP CoP - Right foot
    Dim arCoPVel_Stance_AP_R(100) As Double 'This is the velocity of the AP CoP - Right Foot, normalized to the Stance Period of Gait.
    Dim arCoPAcc_ML_L(100) As Double 'This is the acceleration of the ML CoP - Left foot
    Dim arCoPAcc_ML_R(100) As Double 'This is the acceleration of the ML CoP-Right foot
    Dim arCoPAcc_AP_L(100) As Double 'This is the acceleration of the AP-CoP- Left foot
    Dim arCoPAcc_AP_R(100) As Double 'This is the acceleration of the AP CoP - Right foot
    'Harmonic values, first dimension is harmonic #, second dimension is 0 for Cos, 1 for Sin, 2 for Amplitude
    Dim arHarmonicValuesDisplacement(12, 2) As Double 'Harmonic values for Displacement
    Dim arHarmonicValuesVelocity(12, 2) As Double
    Dim arHarmonicValuesForce(12, 2) As Double 'Harmonic values for Force
    Dim arHarmonicValuesPower(12, 2) As Double
    Dim arHarmonicValuesWork(12, 2) As Double

    Dim arHarmonicsGraphingValuesDisplacement(100) As Double 'When you have checked Harmonics, this is the value of the sum checked values for each percent of the gait cycle.
    Dim arHarmonicsGraphingValuesVelocity(100) As Double
    Dim arHarmonicsGraphingValuesForce(100) As Double
    Dim arHarmonicsGraphingValuesPower(100) As Double
    Dim arHarmonicsGraphingValuesWork(100) As Double

    Dim chkHarmonicsDisplacement(12) As Boolean 'these are to keep track of which harmonics the person wants to look at
    Dim chkHarmonicsVelocity(12) As Boolean
    Dim chkHarmonicsForce(12) As Boolean
    Dim chkHarmonicsPower(12) As Boolean
    Dim chkHarmonicsWork(12) As Boolean

    Dim nStDev_Displacement As Double
    Dim nStDev_ForceBW As Double
    Dim nStDev_Velocity As Double
    Dim nStDev_Power As Double
    Dim nStDev_Energy As Double
    Dim nStDev_Work As Double
    Dim nStDev_SpringConstant As Double
    Dim nStDev_CoP As Double

    Dim arMaximumXValues(100) As Double 'For each graph there is a maximum X value.
    Dim arMaximumYValues(100) As Double 'For each graph there is a maximum Y value.
    Dim arPreviousYMax(100) As Double
    Dim arNextYMax(100) As Double
    Dim updownLowerIncrement As Double
    Dim updownHigherIncrement As Double
    Dim updownPreviousValue As Double
    Dim arSetMaximumYValuesOnce(100) As Boolean 'Once you've reset the maximum Y values once, this turns from false to true.

    Dim colXLabels As New Collection 'This collection is for the X Axis Labels
    Dim colYLabels As New Collection 'This collection is for the Y axis Labels
    Dim lblX() As Label
    Dim lblY() As Label

    Dim bitGraph01 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This the bitmap for the AllForces Graph
    Dim bitGraph02 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is the bitmap for the AverageForce Graph
    Dim bitGraph03 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Radial Graph for Force
    Dim bitGraph10 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force in Terms of BW
    Dim bitGraph11 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force Harmonics - Sum
    Dim bitGraph12 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force Harmonics - Diff
    Dim bitGraph13 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force Harmonics - BarGraph
    Dim bitGraph14 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force Harmonics - Equation
    Dim bitGraph15 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Force Harmonics - Equation Phase Angle
    Dim bitGraph20 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement
    Dim bitGraph21 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement Harmonics - Sum
    Dim bitGraph22 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement Harmonics - Diff
    Dim bitGraph23 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement Harmonics - BarGraph
    Dim bitGraph24 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement Harmonics - Equation
    Dim bitGraph25 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Displacement Harmonics - Equation Phase Angle
    Dim bitGraph30 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity
    Dim bitGraph31 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity Harmonics - Sum
    Dim bitGraph32 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity Harmonics - Diff
    Dim bitGraph33 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity Harmonics - BarGraph
    Dim bitGraph34 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity Harmonics - Equation
    Dim bitGraph35 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Velocity Harmonics - Equation Phase Angle
    Dim bitGraph40 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power
    Dim bitGraph41 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power Harmonics - Sum
    Dim bitGraph42 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power Harmonics - Diff
    Dim bitGraph43 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power Harmonics - BarGraph
    Dim bitGraph44 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power Harmonics - Equation
    Dim bitGraph45 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Power Harmonics - Equation Phase Angle
    Dim bitGraph50 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work
    Dim bitGraph51 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work Harmonics - Sum
    Dim bitGraph52 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work Harmonics - Diff
    Dim bitGraph53 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work Harmonics - BarGraph
    Dim bitGraph54 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work Harmonics - Equation
    Dim bitGraph55 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Work Harmonics - Equation Phase Angle
    Dim bitGraph60 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Energy
    Dim bitGraph70 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the Spring Constants
    Dim bitGraph80 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP - ML vs. AP
    Dim bitGraph81 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Displacement - AP 
    Dim bitGraph82 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Displacement - ML
    Dim bitGraph83 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Velocity - AP 
    Dim bitGraph84 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Velocity - ML
    Dim bitGraph85 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Acceleration - AP 
    Dim bitGraph86 As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height) 'This is th bitmap for the CoP Acceleration - ML

    Dim graphixPanel As Graphics
    Dim graphixForm As Graphics
    Dim graphixLabel As Graphics

    Dim graphix01 As Graphics 'This the graphix object for the AllForces Graph
    Dim graphix02 As Graphics 'This the graphix object for the AverageForce Graph
    Dim graphix03 As Graphics 'This the graphix object for the Radial Graph for Force
    Dim graphix10 As Graphics 'This the graphix object for the Force in Terms of BW
    Dim graphix11 As Graphics 'This the graphix object for the Force Harmonics - Sum
    Dim graphix12 As Graphics 'This the graphix object for the Force Harmonics - Diff
    Dim graphix13 As Graphics 'This the graphix object for the Force Harmonics - BarGraph
    Dim graphix14 As Graphics 'This the graphix object for the Force Harmonics - Equation
    Dim graphix15 As Graphics 'This the graphix object for the Force Harmonics - Equation Phase Angle
    Dim graphix20 As Graphics 'This the graphix object for the Displacement
    Dim graphix21 As Graphics 'This the graphix object for the Displacement Harmonics - Sum
    Dim graphix22 As Graphics 'This the graphix object for the Displacement Harmonics - Diff
    Dim graphix23 As Graphics 'This the graphix object for the Displacement Harmonics - BarGraph
    Dim graphix24 As Graphics 'This the graphix object for the Displacement Harmonics - Equation
    Dim graphix25 As Graphics 'This the graphix object for the Displacement Harmonics - Equation Phase Angle
    Dim graphix30 As Graphics 'This the graphix object for the Velocity
    Dim graphix31 As Graphics 'This the graphix object for the Velocity Harmonics - Sum
    Dim graphix32 As Graphics 'This the graphix object for the Velocity Harmonics - Diff
    Dim graphix33 As Graphics 'This the graphix object for the Velocity Harmonics - BarGraph
    Dim graphix34 As Graphics 'This the graphix object for the Velocity Harmonics - Equation
    Dim graphix35 As Graphics 'This the graphix object for the Velocity Harmonics - Equation Phase Angle
    Dim graphix40 As Graphics 'This the graphix object for the Power
    Dim graphix41 As Graphics 'This the graphix object for the Power Harmonics - Sum
    Dim graphix42 As Graphics 'This the graphix object for the Power Harmonics - Diff
    Dim graphix43 As Graphics 'This the graphix object for the Power Harmonics - BarGraph
    Dim graphix44 As Graphics 'This the graphix object for the Power Harmonics - Equation
    Dim graphix45 As Graphics 'This the graphix object for the Power Harmonics - Equation Phase Angle
    Dim graphix50 As Graphics 'This the graphix object for the Work
    Dim graphix51 As Graphics 'This the graphix object for the Work Harmonics - Sum
    Dim graphix52 As Graphics 'This the graphix object for the Work Harmonics - Diff
    Dim graphix53 As Graphics 'This the graphix object for the Work Harmonics - BarGraph
    Dim graphix54 As Graphics 'This the graphix object for the Work Harmonics - Equation
    Dim graphix55 As Graphics 'This the graphix object for the Work Harmonics - Equation Phase Angle
    Dim graphix60 As Graphics 'This the graphix object for the Energy
    Dim graphix70 As Graphics 'This the graphix object for the Spring Constants
    Dim graphix80 As Graphics 'This the graphix object for the CoP - ML vs. AP
    Dim graphix81 As Graphics 'This the graphix object for the CoP Displacement - AP 
    Dim graphix82 As Graphics 'This the graphix object for the CoP Displacement - ML
    Dim graphix83 As Graphics 'This the graphix object for the CoP Velocity - AP 
    Dim graphix84 As Graphics 'This the graphix object for the CoP Velocity - ML
    Dim graphix85 As Graphics 'This the graphix object for the CoP Acceleration - AP 
    Dim graphix86 As Graphics 'This the graphix object for the CoP Acceleration - ML
    Dim graphix87 As Graphics 'This is the graphix object for the AP CoP movement in Stance Phase, Left and Right are overlayed.

    Dim penPix As New Pen(colorLeft) 'This pen will do wll the drawing
    Dim brushPix As Brush
    Dim rectText As SizeF

    Dim tempSmoothingArray(100) As Double 'This array will be used for passing array numbers to the Butterworth Smoothing Filter and back.
    Dim tempSpline() As structSpline

    Dim pointMousePosition(1) As Point
    Private Declare Auto Function BitBlt Lib "gdi32.dll" (ByVal hdcDest As IntPtr, ByVal nXDest As Integer, ByVal YDest As Integer, ByVal nWidth As Integer, ByVal _
    nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As System.Int32) As Boolean
    Private Const SRCCOPY As Integer = &HCC0020

    Private m_PrintBitmap As Bitmap
    Private WithEvents m_PrintDocument As PrintDocument

    Private Sub frmGraph_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        frmActiveForm = Me
        bFormLoading = True

        Me.MdiParent = System.Windows.Forms.Application.OpenForms.Item(0) 'This sets this as a child form
        Me.ResizeRedraw = True
        Me.MaximumSize = Me.ParentForm.ClientRectangle.Size
        Dim nWidth, nHeight As Integer
        nWidth = ParentForm.Size.Width
        nHeight = ParentForm.Size.Height
        Me.Size = New Size(0.8 * nWidth, 0.8 * nHeight)
        gbxStatistics.Top = 0

        'Translate all the Menu Items
        Select Case nLCID
            Case 1033
                butSuperimpose.Text = "Compare Left and Right"
            Case Else
                TranslateMenuItems()
        End Select

        'Set the default HarmonicChecks
        For Me.i = 1 To 12
            Select Case i
                Case 2
                    chkHarmonicsDisplacement(i) = True
                    chkHarmonicsVelocity(i) = True
                    chkHarmonicsForce(i) = True
                Case 4
                    chkHarmonicsPower(i) = True
                    chkHarmonicsWork(i) = True
            End Select
        Next

        graphixPanel = pnlGraph.CreateGraphics
        graphixLabel = pnlYLabel.CreateGraphics
        graphixForm = Me.CreateGraphics 'This graphics object will print the label on the side of the Y axis.

        'Now you're ready to open the dialog box to choose an fscan file
        Dim resultFileName As DialogResult = OpenFScanFileDialog.ShowDialog()

        'If you do not click on a valid file result then the entire form is closed
        If resultFileName <> Windows.Forms.DialogResult.OK Then
            Me.Close()
        Else
            bFileSelected = True
        End If

        If Me.Text <> "" Then ' Make sure that file name was chosen in the Open Dialog Box
            Me.Text = Microsoft.VisualBasic.Left(sLeftFileName, Len(sLeftFileName) - 5) 'Put the title in the top of the form
        Else
            Exit Sub
        End If

        Do Until InStr(Me.Text, "\") = 0 'Take out the name of the path except for the file name itself minuse the "L.fsx" at the end
            Me.Text = Microsoft.VisualBasic.Right(Me.Text, Len(Me.Text) - 1)
        Loop
        colBriefFileName.Add(Me.Text, lblFullFileNameL.Text) 'Add the text at the top to the brief file names.

        '*******Do all the Calculations*********
        If ProgressBar1.Visible = False Then ProgressBar1.Visible = True
        If lblProgressBar.Visible = False Then lblProgressBar.Visible = True
        subReadAndCalculateData()
        If ProgressBar1.Visible = True Then ProgressBar1.Visible = False
        If lblProgressBar.Visible = True Then lblProgressBar.Visible = False
        If pnlGraph.Visible = False Then pnlGraph.Visible = True
        'Make the default Graph number the displacement with 2nd Harmonic.
        lblWhichGraph.Text = conDisp_Harm_Sum

        'Put the default maximum percent in the scale box at 3 double supports, or just 100% for running.
        lblMaximumXValue.Text = "100" 'Puts 100% in the maximum x value box.
        i = 0 'This is the percent counter
        If bWalkingOrRunning = False Then
            Do Until arGaitPhase(i) = con_L_Single_Support
                i = i + 1
            Loop
            lblMaximumXValue.Text = Str(Val(lblMaximumXValue.Text) + i - 1) 'This marks the last frame of the first double suport.
        End If

        'Put the superimbose button in the top left hand of the box
        butSuperimpose.Left = 3
        butSuperimpose.Top = 3

        'Set all the graphics objects
        graphix01 = Graphics.FromImage(bitGraph01)
        graphix02 = Graphics.FromImage(bitGraph02)
        graphix03 = Graphics.FromImage(bitGraph03)
        graphix10 = Graphics.FromImage(bitGraph10)
        graphix11 = Graphics.FromImage(bitGraph11)
        graphix12 = Graphics.FromImage(bitGraph12)
        graphix13 = Graphics.FromImage(bitGraph13)
        graphix14 = Graphics.FromImage(bitGraph14)
        graphix15 = Graphics.FromImage(bitGraph15)
        graphix20 = Graphics.FromImage(bitGraph20)
        graphix21 = Graphics.FromImage(bitGraph21)
        graphix22 = Graphics.FromImage(bitGraph22)
        graphix23 = Graphics.FromImage(bitGraph23)
        graphix24 = Graphics.FromImage(bitGraph24)
        graphix25 = Graphics.FromImage(bitGraph25)
        graphix30 = Graphics.FromImage(bitGraph30)
        graphix31 = Graphics.FromImage(bitGraph31)
        graphix32 = Graphics.FromImage(bitGraph32)
        graphix33 = Graphics.FromImage(bitGraph33)
        graphix34 = Graphics.FromImage(bitGraph34)
        graphix35 = Graphics.FromImage(bitGraph35)
        graphix40 = Graphics.FromImage(bitGraph40)
        graphix41 = Graphics.FromImage(bitGraph41)
        graphix42 = Graphics.FromImage(bitGraph42)
        graphix43 = Graphics.FromImage(bitGraph43)
        graphix44 = Graphics.FromImage(bitGraph44)
        graphix45 = Graphics.FromImage(bitGraph45)
        graphix50 = Graphics.FromImage(bitGraph50)
        graphix51 = Graphics.FromImage(bitGraph51)
        graphix52 = Graphics.FromImage(bitGraph52)
        graphix53 = Graphics.FromImage(bitGraph53)
        graphix54 = Graphics.FromImage(bitGraph54)
        graphix55 = Graphics.FromImage(bitGraph55)
        graphix60 = Graphics.FromImage(bitGraph60)
        graphix70 = Graphics.FromImage(bitGraph70)
        graphix80 = Graphics.FromImage(bitGraph80)
        graphix81 = Graphics.FromImage(bitGraph81)
        graphix82 = Graphics.FromImage(bitGraph82)
        graphix83 = Graphics.FromImage(bitGraph83)
        graphix84 = Graphics.FromImage(bitGraph84)
        graphix85 = Graphics.FromImage(bitGraph85)

        gbLegend.Visible = False

        Me.Top = 10
        Me.Height = 0.7 * Me.ParentForm.Height
        Me.Width = 0.7 * Me.ParentForm.Width

        lblExamDate.Top = 10
        lblPatientName.Top = 10
        lblGraphTitle.Top = 10

        lblYLabels.Text = "10.35 in."
        pnlGraph.Left = lblYLabels.Width + 20 '+1.2 * rotYAxisName.Width 
        pnlGraph.Width = 0.93 * Me.Width - pnlGraph.Left
        pnlGraph.Top = lblGraphTitle.Top + 1.2 * lblGraphTitle.Height
        pnlGraph.Height = Me.ClientRectangle.Height - pnlGraph.Top - 2 * lblXLabels.Height
        pnlYLabel.Top = pnlGraph.Top
        pnlYLabel.Height = pnlGraph.Height
        pnlYLabel.SendToBack()
        pnlGraph.BringToFront()

        lblExamDate.Left = pnlGraph.Left + pnlGraph.Width - lblExamDate.Width
        lblPatientName.Left = pnlGraph.Left
        lblGraphTitle.Left = (pnlGraph.Width - lblGraphTitle.Width) / 2

        graphixPanel.DrawLine(Pens.Black, 1, 1, 200, 200)

        For Me.i = 0 To 10
            Dim labelYGridLines As New Label
            labelYGridLines.Top = pnlGraph.Top + i * 0.1 * pnlGraph.Height
            labelYGridLines.Text = i & " Units"
            labelYGridLines.Visible = True
            labelYGridLines.Left = 0.5 * pnlGraph.Left
        Next i

        ToolTip1.ToolTipTitle = ""
        ToolTip1.SetToolTip(pnlGraph, " ")
        pnlGraph.Refresh()

        bFormLoading = False
        graphixForm.DrawLine(Pens.AliceBlue, 0, 0, Me.Width, Me.Height)

        Dim menuMeName As New MenuItem
        menuMeName.Text = Me.Text

        'bOpeningNewFile = True
        'frmGaitIndexes.Show()

    End Sub
    Private Sub TranslateMenuItems()
        mnuFile.Text = funTranslateMenuItem("File")
        mnuOpen.Text = funTranslateMenuItem("Open")
        mnuClose.Text = funTranslateMenuItem("Close")
        mnuPrint.Text = funTranslateMenuItem("Print")
        mnuExit.Text = funTranslateMenuItem("Exit")
        mnuEdit.Text = funTranslateMenuItem("Edit")
        mnuCopyData.Text = funTranslateMenuItem("Copy Data")
        mnuCopyScreen.Text = funTranslateMenuItem("Copy Screen")
        mnuScaleXAxis.Text = funTranslateMenuItem("Scale Y Axis")
        mnuScaleYAxis.Text = funTranslateMenuItem("Scale X Axis")
        mnuColors.Text = funTranslateMenuItem("Change Colors")
        mnuColorLeft.Text = funTranslateMenuItem("Color - Left Foot")
        mnuColorRight.Text = funTranslateMenuItem("Color - Right Foot")
        mnuColorBoth.Text = funTranslateMenuItem("Color - Double Support")
        mnuColorBackground.Text = funTranslateMenuItem("Color - Background")
        mnuColorGridline.Text = funTranslateMenuItem("Color - Gridlines")
        mnuColorHarmonic.Text = funTranslateMenuItem("Color - Harmonic Line")
        mnuColorDefault.Text = funTranslateMenuItem("Default Colors")
        If bEnglishOrMetricUnits = False Then mnuChangeToMetricUnits.Text = funTranslateMenuItem("Change to Metric Units")
        If bEnglishOrMetricUnits = True Then mnuChangeToMetricUnits.Text = funTranslateMenuItem("Change to English Units")
        mnuCoMGraphs.Text = funTranslateMenuItem("Center of Mass")
        mnuDisplacement.Text = funTranslateMenuItem("Displacement")
        mnuVelocity.Text = funTranslateMenuItem("Velocity")
        mnuForceBW.Text = funTranslateMenuItem("Force") & ": " & funTranslateMenuItem("Body Weight")
        mnuHarmonics.Text = funTranslateMenuItem("Harmonics")
        mnuEnergy.Text = funTranslateMenuItem("Energy")
        mnuPower.Text = funTranslateMenuItem("Power")
        mnuWork.Text = funTranslateMenuItem("Work")
        mnuForceAverage.Text = funTranslateMenuItem("Force - Average - Actual")
        mnuForceAllSteps.Text = funTranslateMenuItem("Force - All Steps")
        mnuSpringConstant.Text = funTranslateMenuItem("Spring Constant")
        mnuStatistics.Text = ("Statistics")
        mnuCoPGraphs.Text = funTranslateMenuItem("Center of Pressure Graphs")
        mnuCoPDisplacementPosteriorToAnterior.Text = funTranslateMenuItem("Displacement - Posterior to Anterior")
        mnuCoPDisplacementLateralToMedial.Text = funTranslateMenuItem("Displacement - Lateral to Medial")
        mnuCoPVelocityLateralToMedial.Text = funTranslateMenuItem("Velocity - Posterior or Anterior")
        mnuCoPVelocityLateralToMedial.Text = funTranslateMenuItem("Velocity - Lateral or Medial")
        mnuCoPAccelerationLateralToMedial.Text = funTranslateMenuItem("Acceleration - Lateral or Medial")
        mnuCoPAccelerationPosteriorToAnterior.Text = funTranslateMenuItem("Acceleration - Posterior or Anterior")
        mnuCompareGraphs.Text = funTranslateMenuItem("Compare Graphs")
        mnuGaitIndices.Text = funTranslateMenuItem("Gait Indices")
        mnuWindow.Text = funTranslateMenuItem("Window")
        mnuHelp.Text = funTranslateMenuItem("Help")
        mnuContents.Text = funTranslateMenuItem("Contents")
        mnuAbout.Text = funTranslateMenuItem("About")
        butSuperimpose.Text = funTranslateButton("Compare Left and Right")

    End Sub
    Private Sub frmGraph_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        frmActiveForm = Me

        MenuStrip_Main.Visible = True 'Make the menu visible

        mnuWindow.DropDownItems.Clear() 'Clear all the items from the menu

        For Me.i = 1 To System.Windows.Forms.Application.OpenForms.Count 'Add all the forms to the window list.
            Dim menuForms As New ToolStripMenuItem
            If System.Windows.Forms.Application.OpenForms.Item(i - 1).Name = "frmGraph" Then
                menuForms.Text = System.Windows.Forms.Application.OpenForms.Item(i - 1).Text
                If menuForms.Text <> "frmGraph" And menuForms.Text <> Me.Text Then
                    mnuWindow.DropDownItems.Add(menuForms.Text)
                End If
            End If
            menuForms.Dispose()
        Next

    End Sub
    Private Sub frmGraph_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        MenuStrip_Main.Visible = False 'Make the menu invisible

    End Sub

    Private Sub frmGraph_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Dim nMaximumYLabelWidth As Integer
        For Me.i = 0 To Me.Controls.Count - 1 'Find the maximum Label width.
            If Mid(Me.Controls(i).Tag, 1, 6) = "YLabel" Then
                If nMaximumYLabelWidth < Me.Controls(i).Width Then nMaximumYLabelWidth = Me.Controls(i).Width
            End If
        Next i
        Select Case lblWhichGraph.Text
            Case 3, 14, 15, 24, 25, 34, 35, 44, 45, 54, 54
                pnlGraph.Left = 0.05 * Me.Width
            Case Else
                pnlGraph.Left = 20 + pnlYLabel.Left + pnlYLabel.Width + nMaximumYLabelWidth
                pnlGraph.Height = Me.ClientRectangle.Height - pnlGraph.Top - 3 * lblXLabels.Height
        End Select
        pnlGraph.Width = 0.93 * Me.Width - pnlGraph.Left
        pnlGraph.Top = lblGraphTitle.Top + 1.2 * lblGraphTitle.Height
        pnlYLabel.Top = pnlGraph.Top
        pnlYLabel.Height = pnlGraph.Height
        pnlYLabel.Refresh()
        pnlGraph.BorderStyle = BorderStyle.None

    End Sub

    Private Sub frmGraph_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        If e.Equals(MouseButtons.Right) Then
            If ContextMenuStrip1.Visible = False Then
                ContextMenuStrip1.Visible = True
            Else
                ContextMenuStrip1.Visible = False
            End If
        End If
    End Sub

    Private Sub frmGraph_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If you didn't just close the form by hitting the cancel button from the OpenFormDialog, then you have to remove data from all the collections.
        If bFileSelected = True Then
            colFileName.Remove(lblFullFileNameL.Text)
            colBriefFileName.Remove(lblFullFileNameL.Text)
            colDisplacement.Remove(lblFullFileNameL.Text)
            colVelocity.Remove(lblFullFileNameL.Text)
            colBodyWeight.Remove(lblFullFileNameL.Text)
            colBodyWeightPct.Remove(lblFullFileNameL.Text)
            colLeftForce.Remove(lblFullFileNameL.Text)
            colRightForce.Remove(lblFullFileNameL.Text)
            colTotalForce.Remove(lblFullFileNameL.Text)
            colGaitPhase.Remove(lblFullFileNameL.Text)
            colPower.Remove(lblFullFileNameL.Text)
            colWork.Remove(lblFullFileNameL.Text)
            colSpringConstants.Remove(lblFullFileNameL.Text)
            colCoPLoc_AP_L.Remove(lblFullFileNameL.Text)
            colCoPLoc_AP_R.Remove(lblFullFileNameL.Text)
            colCoPLoc_ML_L.Remove(lblFullFileNameL.Text)
            colCoPLoc_ML_R.Remove(lblFullFileNameL.Text)
            colCoPVel_AP_L.Remove(lblFullFileNameL.Text)
            colCoPVel_AP_R.Remove(lblFullFileNameL.Text)
            colCoPVel_ML_L.Remove(lblFullFileNameL.Text)
            colCoPVel_ML_R.Remove(lblFullFileNameL.Text)
            colCoPAcc_AP_L.Remove(lblFullFileNameL.Text)
            colCoPAcc_AP_R.Remove(lblFullFileNameL.Text)
            colCoPAcc_ML_L.Remove(lblFullFileNameL.Text)
            colCoPAcc_ML_R.Remove(lblFullFileNameL.Text)
            colEnergy_Potential.Remove(lblFullFileNameL.Text)
            colEnergy_Kinetic.Remove(lblFullFileNameL.Text)
            colEnergy_Total.Remove(lblFullFileNameL.Text)
            colGI.Remove(lblFullFileNameL.Text)
            colCoPSymmetryIndex.Remove(lblFullFileNameL.Text)
            colCoPPurityIndex_L.Remove(lblFullFileNameL.Text)
            colCoPPurityIndex_R.Remove(lblFullFileNameL.Text)
            colCoPPurityIndex_Avg.Remove(lblFullFileNameL.Text)
        End If
    End Sub

    Private Sub mnuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOpen.Click

        For Me.i = 1 To System.Windows.Forms.Application.OpenForms.Count
            If System.Windows.Forms.Application.OpenForms(i - 1).Name = "frmCompareGraphs" Then
                System.Windows.Forms.Application.OpenForms(i - 1).Close()
            ElseIf System.Windows.Forms.Application.OpenForms(i - 1).Name = "frmGaitIndexes" Then
                System.Windows.Forms.Application.OpenForms(i - 1).Close()
            End If
        Next i
       
            Dim formGraph As New frmGraph
            formGraph.Show()
       
    End Sub

    Private Sub mnuClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        Me.Close()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

        Me.Close()

    End Sub

    Private Sub mnuPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPrint.Click
        Dim prd As PrintDocument
        prd = New PrintDocument
        AddHandler prd.PrintPage, AddressOf OnPrintPage
        prd.Print()

    End Sub

    Private Sub mnuCopyScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopyScreen.Click
        bCopyScreen = True
        pnlGraph.Refresh()
        Dim bm As New Bitmap(Me.Width, Me.Height)
        Dim g As Graphics = Graphics.FromImage(bm)
        g.CopyFromScreen(Me.Location, New Point(0, 0), New Size(Me.Width, Me.Height))
    End Sub

    Private Sub mnuCopyData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopyData.Click
        Dim sDataText As String = ""
        Dim nGraph As Integer = Val(lblWhichGraph.Text)
        Clipboard.Clear()
        Select Case nGraph
            Case 2, 3
                sDataText = "Average Force Under the Feet" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Left Force" & vbTab & "Right Force" & vbTab & "Total Force" & vbCr
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arLeftForce(i), "0.0") & " lbs. "
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arRightForce(i), "0.0") & " lbs. "
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arTotalForce(i), "0.0") & " lbs. " & vbCr
                    Else
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arLeftForce(i) * Lbs_To_Kgs, "0.0") & " kgs. "
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arRightForce(i) * Lbs_To_Kgs, "0.0") & " kgs. "
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arTotalForce(i) * Lbs_To_Kgs, "0.0") & " kgs. " & vbCr
                    End If
                Next i

            Case 14, 15
                sDataText = "Fourier Equation for Vertical Force" & vbCr
                sDataText = "Fourier Equation for Displacement Harmonics" & vbCr
                sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesForce(1, 0), "#.00##") & " cos(x) "
                If arHarmonicValuesForce(1, 1) > 0 Then
                    sDataText = sDataText & "+ "
                End If
                sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesForce(1, 1), "#.00##") & " sin(x) " & vbCr
                For Me.i = 2 To 12
                    If arHarmonicValuesForce(i, 0) > 0 Then sDataText = sDataText & "+"
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesForce(i, 0), "#.00##") & " cos(" & i & "x) "
                    If arHarmonicValuesForce(i, 1) > 0 Then
                        sDataText = sDataText & " +"
                    End If
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesForce(i, 1), "#.00##") & "sin(" & i & "x)" & vbCr
                Next i

            Case 20
                sDataText = "Displacement of Center of Body Mass" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Displacement" & vbCr
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arDisplacement(i) * Feet_To_In, "0.00") & " in. "
                    Else
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arBodyWeight(i) * Feet_To_cm, "0.0") & "cm. "
                    End If
                Next i
            Case 21, 22
                sDataText = "Displacement of Center of Body Mass" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Displacement"
                If nGraph = 11 Then
                    sDataText = sDataText & vbTab & "Sum of Harmonics:" & vbCr
                ElseIf nGraph = 12 Then
                    sDataText = sDataText & vbTab & "Difference between Displacement and Checked Harmonics:" & vbCr
                End If
                j = 0
                For Me.i = 1 To 12
                    If chkHarmonicsDisplacement(i) = True Then
                        j = j + 1
                    End If
                Next i
                For Me.i = 1 To 12
                    If chkHarmonicsDisplacement(i) = True Then
                        sDataText = sDataText & "i"
                        If j > 1 Then
                            sDataText = sDataText & ", "
                            j = j - 1
                        Else
                            sDataText = sDataText & vbCr
                        End If
                    End If
                Next
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "% " & vbTab & arDisplacement(i) * Feet_To_In & " in." & vbTab & arHarmonicsGraphingValuesDisplacement(i) * Feet_To_In & " in."
                    Else
                        sDataText = sDataText & i & "% " & vbTab & arDisplacement(i) * Feet_To_cm & " cm." & vbTab & arHarmonicsGraphingValuesDisplacement(i) * Feet_To_cm & " cm."
                    End If
                Next i
            Case 23
                sDataText = "Amplitude of Displacement Harmonics" & vbCr & vbCr
                For Me.i = 1 To 12
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & "Harmonic: " & i & vbTab & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(i, 2) * Feet_To_In, "#.#####") & vbCr
                    Else
                        sDataText = sDataText & "Harmonic: " & i & vbTab & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(i, 2) * Feet_To_In, "#.#####") & vbCr
                    End If
                Next i
            Case 24 'copy the Fourier equation for the Displacement 
                sDataText = "Fourier Equation for Displacement Harmonics" & vbCr
                sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(1, 0) * Feet_To_In, "#.####") & " cos(x) "
                If arHarmonicValuesDisplacement(1, 1) < 0 Then
                    sDataText = sDataText & "- "
                Else
                    sDataText = sDataText & "+ "
                End If
                sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(1, 1) * Feet_To_In, "#.####") & " sin(x) " & vbCr
                For Me.i = 2 To 12
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(i, 0) * Feet_To_In, "#.####") & " cos(" & i & "x)"
                    If arHarmonicValuesDisplacement(i, 1) < 0 Then
                    Else
                        sDataText = sDataText & "+"
                    End If
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesDisplacement(i, 1) * Feet_To_In, "#.00##") & "sin(" & i & "x)" & vbCr
                Next i

            Case 34 'Copy the velocity fourier equation
                sDataText = "Fourier Equation for Velocity Harmonics" & vbCr
                sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesVelocity(1, 0) * Feet_To_In, "#.####") & " cos(x) "
                If arHarmonicValuesVelocity(1, 1) < 0 Then
                    sDataText = sDataText & "- "
                Else
                    sDataText = sDataText & "+ "
                End If
                sDataText = sDataText & Microsoft.VisualBasic.Format(Math.Abs(arHarmonicValuesVelocity(1, 1)) * Feet_To_In, "#.####") & " sin(x) " & vbCr
                For Me.i = 2 To 12
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesVelocity(i, 0) * Feet_To_In, "#.####") & " cos(" & i & "x)"
                    If arHarmonicValuesVelocity(i, 1) < 0 Then
                    Else
                        sDataText = sDataText & "+"
                    End If
                    sDataText = sDataText & Microsoft.VisualBasic.Format(arHarmonicValuesVelocity(i, 1) * Feet_To_In, "#.00##") & "sin(" & i & "x)" & vbCr
                Next i


            Case 60
                sDataText = "Energy" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Potential Energy" & vbTab & "Kinetic Energy" & vbTab & "Total Energy" & vbCr
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arEnergy_Potential(i), "0.00") & " Ft.-lbs." & vbTab
                        sDataText = sDataText & Microsoft.VisualBasic.Format(arEnergy_Kinetic(i), "0.00") & " Ft.-lbs." & vbTab
                        sDataText = sDataText & Microsoft.VisualBasic.Format(arEnergy_Total(i), "0.00") & " Ft.-lbs. " & vbCr
                    Else
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arEnergy_Potential(i) * FtLbs_To_NewtonM, "0.##") & " N.-m." & vbTab
                        sDataText = sDataText & Microsoft.VisualBasic.Format(arEnergy_Kinetic(i) * FtLbs_To_NewtonM, "0.##") & " N.-m." & vbTab
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arEnergy_Total(i) * FtLbs_To_NewtonM, "0.##") & " N.-m. " & vbCr
                    End If
                Next i
            Case conSpringConstants 'case 70
                sDataText = "Spring Constant" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Spring Constant" & vbCr
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arSpringConstants(i) / 12, "0.00") & " lbs./in." & vbCr
                    Else
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arSpringConstants(i) * Lbs_To_Kgs / Feet_To_cm, "0.00") & " lbs./in." & vbCr
                    End If
                Next i
            Case conCoP_AP 'case 81
                sDataText = "Center of Pressure - Posterior to Anterior" & vbCr
                sDataText = sDataText & "Pct Gait" & vbTab & "Left" & vbTab & "Right" & vbCr
                For Me.i = 0 To 100
                    If bEnglishOrMetricUnits = False Then
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arCoPLoc_AP_L(i) * 0.2, "0.0") & " in. " & vbTab
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arCoPLoc_AP_R(i) * 0.2, "0.0") & " in. " & vbCr
                    Else
                        sDataText = sDataText & i & "%" & vbTab & Microsoft.VisualBasic.Format(arCoPLoc_AP_L(i) * 0.2 * 2.54, "0.0") & " cm. " & vbTab
                        sDataText = sDataText & vbTab & Microsoft.VisualBasic.Format(arCoPLoc_AP_R(i) * 0.2 * 2.54, "0.0") & " cm. " & vbCr
                    End If
                Next i
            Case Else
                sDataText = "No Data Copied"
        End Select
        Clipboard.SetText(sDataText)
    End Sub

    Private Sub mnuScaleXAxis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScaleXAxis.Click
        Dim nGraph As Integer
        nGraph = Val(lblWhichGraph.Text)

        Select Case nGraph
            Case 1, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55, 80, 81, 82
                Exit Sub
        End Select

        gbxScale.Text = "Scale X Axis"
        gbxScale.Tag = "X"
        gbxScale.Visible = True
        gbxScale.BringToFront()
        lblScaleUnits.Text = "%"
        txtScaleValue.Text = arMaximumXValues(nGraph)

    End Sub

    Private Sub mnuScaleYAxis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScaleYAxis.Click
        Dim nGraph As Integer
        nGraph = Val(lblWhichGraph.Text)

        Select Case nGraph
            Case 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55, 80, 81, 82
                Exit Sub
        End Select

        bReScaleLoading = True

        gbxScale.Text = "Scale Y Axis"
        gbxScale.Tag = "Y"
        gbxScale.Visible = True
        gbxScale.BringToFront()
        Select Case nGraph
            Case 1, 2, 3, 10, 11, 12
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "Lbs."
                    txtScaleValue.Text = Int(arMaximumYValues(nGraph))
                Else
                    lblScaleUnits.Text = "Kgs."
                    txtScaleValue.Text = Int(arMaximumYValues(nGraph) * Lbs_To_Kgs)
                End If
            Case 20, 21, 22
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "In."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Feet_To_In
                Else
                    lblScaleUnits.Text = "Cm."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Feet_To_cm
                End If
                If Val(txtScaleValue.Text) < 15 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.00")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case 30, 31, 32
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "In./sec"
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Feet_To_In
                Else
                    lblWhichGraph.Text = "Cm./sec"
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Feet_To_cm
                End If
                If Val(txtScaleValue.Text) < 15 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.00")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case 40, 41, 42
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "Ft.-Lbs./sec."
                    txtScaleValue.Text = arMaximumYValues(nGraph)
                Else
                    lblScaleUnits.Text = "N.-m./sec."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * FtLbs_To_NewtonM
                End If
                If Val(txtScaleValue.Text) < 5 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.00")
                ElseIf Val(txtScaleValue.Text) < 25 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.0")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case 50, 51, 52, 60
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "Ft.-Lbs."
                    txtScaleValue.Text = arMaximumYValues(nGraph)
                Else
                    lblScaleUnits.Text = "N.-m."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * FtLbs_To_NewtonM
                End If
                If Val(txtScaleValue.Text) < 5 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.00")
                ElseIf Val(txtScaleValue.Text) < 25 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.0")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case 70
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "Lbs./in."
                    txtScaleValue.Text = Int(arMaximumYValues(nGraph) / Feet_To_In)
                Else
                    lblScaleUnits.Text = "Kg./cm."
                    txtScaleValue.Text = Int(arMaximumYValues(nGraph) * Lbs_To_Kgs / Feet_To_cm)
                End If
            Case 83, 84
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "In./sec."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Sensels_To_Inches
                Else
                    lblScaleUnits.Text = "cm./sec."
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Sensels_To_Cm
                End If
                If Val(txtScaleValue.Text) < 25 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.0")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case 85, 86
                If bEnglishOrMetricUnits = False Then
                    lblScaleUnits.Text = "In./sec/sec"
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Sensels_To_Inches
                Else
                    lblScaleUnits.Text = "Cm./sec/sec"
                    txtScaleValue.Text = arMaximumYValues(nGraph) * Sensels_To_Cm
                End If
                If Val(txtScaleValue.Text) < 25 Then
                    txtScaleValue.Text = FormatNumber(Val(txtScaleValue.Text), "0.0")
                Else
                    txtScaleValue.Text = Int(Val(txtScaleValue.Text))
                End If
            Case Else
        End Select

        bReScaleLoading = False
    End Sub

    Private Sub mnuColorLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColorLeft.Click
        With ColorDialog1
            ColorDialog1.Tag = mnuColorLeft
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorLeft
            ColorDialog1.ShowDialog()
            colorLeft = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColorRight.Click
        With ColorDialog1
            ColorDialog1.Tag = "mnuColorRight"
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorRight
            ColorDialog1.ShowDialog()
            colorRight = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorBoth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColorBoth.Click
        With ColorDialog1
            ColorDialog1.Tag = "mnuColorBoth"
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorBoth
            ColorDialog1.ShowDialog()
            colorBoth = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorBackground_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColorBackground.Click
        With ColorDialog1
            ColorDialog1.Tag = "mnuColorBackground"
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorBackground
            ColorDialog1.ShowDialog()
            colorBackground = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorGridline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColorGridline.Click
        With ColorDialog1
            ColorDialog1.Tag = "mnuColorGridline"
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorGrid
            ColorDialog1.ShowDialog()
            colorGrid = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorHarmonic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColorHarmonic.Click
        With ColorDialog1
            ColorDialog1.Tag = "mnuColorHarmonic"
            ColorDialog1.AnyColor = True
            ColorDialog1.Color = colorHarm
            ColorDialog1.ShowDialog()
            colorHarm = ColorDialog1.Color
        End With
        Me.Refresh()
    End Sub

    Private Sub mnuColorDefault_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColorDefault.Click
        colorLeft = ColorTranslator.FromOle(RGB(0, 0, 255))
        colorRight = ColorTranslator.FromOle(RGB(0, 150, 60))
        colorBoth = ColorTranslator.FromOle(RGB(255, 0, 0))
        colorBackground = ColorTranslator.FromOle(RGB(225, 225, 225))
        colorGrid = ColorTranslator.FromOle(RGB(192, 192, 192))
        colorHarm = ColorTranslator.FromOle(RGB(180, 150, 100))
        Me.Refresh()
    End Sub

    Private Sub mnuChangeToMetricUnits_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuChangeToMetricUnits.Click
        If bEnglishOrMetricUnits = False Then
            bEnglishOrMetricUnits = True
            mnuChangeToMetricUnits.Text = "Change to English Units"
        ElseIf bEnglishOrMetricUnits = True Then
            bEnglishOrMetricUnits = False
            mnuChangeToMetricUnits.Text = "Change to Metric Units"
        End If
    End Sub

    Private Sub mnuDisplacement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDisplacement.Click
        lblWhichGraph.Text = conDisplacement_Vert
    End Sub

    Private Sub mnuForceBW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuForceBW.Click
        lblWhichGraph.Text = conForce_As_BW
    End Sub

    Private Sub mnuVelocity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVelocity.Click
        lblWhichGraph.Text = conVelocity_Vert
    End Sub

    Private Sub mnuPower_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPower.Click
        lblWhichGraph.Text = conPower_Vert
    End Sub

    Private Sub mnuWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWork.Click
        lblWhichGraph.Text = conWork_Vert
    End Sub

    Private Sub mnuEnergy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEnergy.Click
        lblWhichGraph.Text = conEnergy
    End Sub

    Private Sub mnuForceAverage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuForceAverage.Click
        lblWhichGraph.Text = conForce_Avg
    End Sub

    Private Sub mnuForceAllSteps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuForceAllSteps.Click
        lblWhichGraph.Text = conForce_AllSteps
    End Sub

    Private Sub mnuSpringConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpringConstant.Click
        lblWhichGraph.Text = conSpringConstants
    End Sub

    Private Sub mnuStatistics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuStatistics.Click
        gbxStatistics.Visible = True
        gbxStatistics.BringToFront()
    End Sub

    Private Sub mnuCoPDisplacementPosteriorToAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPDisplacementPosteriorToAnterior.Click
        lblWhichGraph.Text = conCoP_AP
    End Sub

    Private Sub mnuCoPDisplacementLateralToMedial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPDisplacementLateralToMedial.Click
        lblWhichGraph.Text = conCoP_ML
    End Sub

    Private Sub mnuCoPVelocityPosteriorToAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPVelocityPosteriorToAnterior.Click
        lblWhichGraph.Text = conCoP_AP_Vel
    End Sub

    Private Sub mnuCoPVelocityLateralToMedial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPVelocityLateralToMedial.Click
        lblWhichGraph.Text = conCoP_ML_Vel
    End Sub

    Private Sub mnuCoPAccelerationPosteriorToAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPAccelerationPosteriorToAnterior.Click
        lblWhichGraph.Text = conCoP_AP_Acc
    End Sub

    Private Sub mnuCoPAccelerationLateralToMedial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoPAccelerationLateralToMedial.Click
        lblWhichGraph.Text = conCoP_ML_Acc
    End Sub

    Private Sub mnuHarmonics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHarmonics.Click
        gbxPickHarmonics.Visible = True
        gbxPickHarmonics.Top = 10
        Dim nGraphNum As Integer = Val(lblWhichGraph.Text)
        Select Case nGraphNum
            Case 1, 2, 3, 10, 11, 12, 13, 14, 15
                radbutHarmForceGraph.Checked = True
                If nGraphNum = 2 Or nGraphNum = 3 Or nGraphNum = 10 Or nGraphNum = 11 Then radbutSum.Checked = True
                If nGraphNum = 12 Then radbutDifference.Checked = True
                If nGraphNum = 13 Then radbutAmplitudes.Checked = True
                If nGraphNum = 14 Or nGraphNum = 15 Then radbutEquation.Checked = True
            Case 20, 21, 22, 23, 24, 25
                radbutHarmDisplGraph.Checked = True
                If nGraphNum = 20 Or nGraphNum = 21 Then radbutSum.Checked = True
                If nGraphNum = 22 Then radbutDifference.Checked = True
                If nGraphNum = 23 Then radbutAmplitudes.Checked = True
                If nGraphNum = 24 Or nGraphNum = 25 Then radbutEquation.Checked = True
            Case 30, 31, 32, 33, 34, 35
                radbutHarmVelGraph.Checked = True
                If nGraphNum = 30 Or nGraphNum = 31 Then radbutSum.Checked = True
                If nGraphNum = 32 Then radbutDifference.Checked = True
                If nGraphNum = 33 Then radbutAmplitudes.Checked = True
                If nGraphNum = 34 Or nGraphNum = 35 Then radbutEquation.Checked = True
            Case 40, 41, 42, 43, 44, 45
                radbutHarmPowerGraph.Checked = True
                If nGraphNum = 40 Or nGraphNum = 41 Then radbutSum.Checked = True
                If nGraphNum = 42 Then radbutDifference.Checked = True
                If nGraphNum = 43 Then radbutAmplitudes.Checked = True
                If nGraphNum = 44 Or nGraphNum = 45 Then radbutEquation.Checked = True
            Case 50, 51, 52, 53, 54, 55
                radbutHarmWorkGraph.Checked = True
                If nGraphNum = 50 Or nGraphNum = 51 Then radbutSum.Checked = True
                If nGraphNum = 52 Then radbutDifference.Checked = True
                If nGraphNum = 53 Then radbutAmplitudes.Checked = True
                If nGraphNum = 54 Or nGraphNum = 55 Then radbutEquation.Checked = True
        End Select

        Select Case nGraphNum
            Case 10, 11, 12
                For Me.i = 0 To 11
                    If chkHarmonicsForce(i + 1) = True Then
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                    Else
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                    End If
                    If chkHarmonicsForce(1) = False And chkHarmonicsForce(2) = True And chkHarmonicsForce(3) = False And chkHarmonicsForce(4) = False And chkHarmonicsForce(5) = False And chkHarmonicsForce(6) = False And chkHarmonicsForce(7) = False And chkHarmonicsForce(8) = False And chkHarmonicsForce(9) = False And chkHarmonicsForce(10) = False And chkHarmonicsForce(11) = False And chkHarmonicsForce(12) = False Then
                        radbutPureHarm.Checked = True
                    ElseIf chkHarmonicsForce(1) = False And chkHarmonicsForce(2) = True And chkHarmonicsForce(3) = False And chkHarmonicsForce(4) = True And chkHarmonicsForce(5) = False And chkHarmonicsForce(6) = True And chkHarmonicsForce(7) = False And chkHarmonicsForce(8) = True And chkHarmonicsForce(9) = False And chkHarmonicsForce(10) = True And chkHarmonicsForce(11) = False And chkHarmonicsForce(12) = True Then
                        radbutEvenHarm.Checked = True
                    ElseIf chkHarmonicsForce(2) = False And chkHarmonicsForce(1) = True And chkHarmonicsForce(4) = False And chkHarmonicsForce(3) = True And chkHarmonicsForce(6) = False And chkHarmonicsForce(5) = True And chkHarmonicsForce(8) = False And chkHarmonicsForce(7) = True And chkHarmonicsForce(10) = False And chkHarmonicsForce(9) = True And chkHarmonicsForce(12) = False And chkHarmonicsForce(11) = True Then
                        radbutOddHarm.Checked = True
                    Else
                        radbutOtherHarm.Checked = True
                    End If
                Next i
            Case 20, 21, 22
                For Me.i = 0 To 11
                    If chkHarmonicsDisplacement(i + 1) = True Then
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                    Else
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                    End If
                    If chkHarmonicsDisplacement(1) = False And chkHarmonicsDisplacement(2) = True And chkHarmonicsDisplacement(3) = False And chkHarmonicsDisplacement(4) = False And chkHarmonicsDisplacement(5) = False And chkHarmonicsDisplacement(6) = False And chkHarmonicsDisplacement(7) = False And chkHarmonicsDisplacement(8) = False And chkHarmonicsDisplacement(9) = False And chkHarmonicsDisplacement(10) = False And chkHarmonicsDisplacement(11) = False And chkHarmonicsDisplacement(12) = False Then
                        radbutPureHarm.Checked = True
                    ElseIf chkHarmonicsDisplacement(1) = False And chkHarmonicsDisplacement(2) = True And chkHarmonicsDisplacement(3) = False And chkHarmonicsDisplacement(4) = True And chkHarmonicsDisplacement(5) = False And chkHarmonicsDisplacement(6) = True And chkHarmonicsDisplacement(7) = False And chkHarmonicsDisplacement(8) = True And chkHarmonicsDisplacement(9) = False And chkHarmonicsDisplacement(10) = True And chkHarmonicsDisplacement(11) = False And chkHarmonicsDisplacement(12) = True Then
                        radbutEvenHarm.Checked = True
                    ElseIf chkHarmonicsDisplacement(2) = False And chkHarmonicsDisplacement(1) = True And chkHarmonicsDisplacement(4) = False And chkHarmonicsDisplacement(3) = True And chkHarmonicsDisplacement(6) = False And chkHarmonicsDisplacement(5) = True And chkHarmonicsDisplacement(8) = False And chkHarmonicsDisplacement(7) = True And chkHarmonicsDisplacement(10) = False And chkHarmonicsDisplacement(9) = True And chkHarmonicsDisplacement(12) = False And chkHarmonicsDisplacement(11) = True Then
                        radbutOddHarm.Checked = True
                    Else
                        radbutOtherHarm.Checked = True
                    End If
                Next i
            Case 30, 31, 32
                For Me.i = 0 To 11
                    If chkHarmonicsVelocity(i + 1) = True Then
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                    Else
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                    End If
                    If chkHarmonicsVelocity(1) = False And chkHarmonicsVelocity(2) = True And chkHarmonicsVelocity(3) = False And chkHarmonicsVelocity(4) = False And chkHarmonicsVelocity(5) = False And chkHarmonicsVelocity(6) = False And chkHarmonicsVelocity(7) = False And chkHarmonicsVelocity(8) = False And chkHarmonicsVelocity(9) = False And chkHarmonicsVelocity(10) = False And chkHarmonicsVelocity(11) = False And chkHarmonicsVelocity(12) = False Then
                        radbutPureHarm.Checked = True
                    ElseIf chkHarmonicsVelocity(1) = False And chkHarmonicsVelocity(2) = True And chkHarmonicsVelocity(3) = False And chkHarmonicsVelocity(4) = True And chkHarmonicsVelocity(5) = False And chkHarmonicsVelocity(6) = True And chkHarmonicsVelocity(7) = False And chkHarmonicsVelocity(8) = True And chkHarmonicsVelocity(9) = False And chkHarmonicsVelocity(10) = True And chkHarmonicsVelocity(11) = False And chkHarmonicsVelocity(12) = True Then
                        radbutEvenHarm.Checked = True
                    ElseIf chkHarmonicsVelocity(2) = False And chkHarmonicsVelocity(1) = True And chkHarmonicsVelocity(4) = False And chkHarmonicsVelocity(3) = True And chkHarmonicsVelocity(6) = False And chkHarmonicsVelocity(5) = True And chkHarmonicsVelocity(8) = False And chkHarmonicsVelocity(7) = True And chkHarmonicsVelocity(10) = False And chkHarmonicsVelocity(9) = True And chkHarmonicsVelocity(12) = False And chkHarmonicsVelocity(11) = True Then
                        radbutOddHarm.Checked = True
                    Else
                        radbutOtherHarm.Checked = True
                    End If
                Next i
            Case 40, 41, 42
                For Me.i = 0 To 11
                    If chkHarmonicsPower(i + 1) = True Then
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                    Else
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                    End If
                    If chkHarmonicsPower(1) = False And chkHarmonicsPower(2) = False And chkHarmonicsPower(3) = False And chkHarmonicsPower(4) = True And chkHarmonicsPower(5) = False And chkHarmonicsPower(6) = False And chkHarmonicsPower(7) = False And chkHarmonicsPower(8) = False And chkHarmonicsPower(9) = False And chkHarmonicsPower(10) = False And chkHarmonicsPower(11) = False And chkHarmonicsPower(12) = False Then
                        radbutPureHarm.Checked = True
                    ElseIf chkHarmonicsPower(1) = False And chkHarmonicsPower(2) = True And chkHarmonicsPower(3) = False And chkHarmonicsPower(4) = True And chkHarmonicsPower(5) = False And chkHarmonicsPower(6) = True And chkHarmonicsPower(7) = False And chkHarmonicsPower(8) = True And chkHarmonicsPower(9) = False And chkHarmonicsPower(10) = True And chkHarmonicsPower(11) = False And chkHarmonicsPower(12) = True Then
                        radbutEvenHarm.Checked = True
                    ElseIf chkHarmonicsPower(2) = False And chkHarmonicsPower(1) = True And chkHarmonicsPower(4) = False And chkHarmonicsPower(3) = True And chkHarmonicsPower(6) = False And chkHarmonicsPower(5) = True And chkHarmonicsPower(8) = False And chkHarmonicsPower(7) = True And chkHarmonicsPower(10) = False And chkHarmonicsPower(9) = True And chkHarmonicsPower(12) = False And chkHarmonicsPower(11) = True Then
                        radbutOddHarm.Checked = True
                    Else
                        radbutOtherHarm.Checked = True
                    End If
                Next i
            Case 50, 51, 52
                For Me.i = 0 To 11
                    If chkHarmonicsWork(i + 1) = True Then
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                    Else
                        listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                    End If
                    If chkHarmonicsWork(1) = False And chkHarmonicsWork(2) = False And chkHarmonicsWork(3) = False And chkHarmonicsWork(4) = True And chkHarmonicsWork(5) = False And chkHarmonicsWork(6) = False And chkHarmonicsWork(7) = False And chkHarmonicsWork(8) = False And chkHarmonicsWork(9) = False And chkHarmonicsWork(10) = False And chkHarmonicsWork(11) = False And chkHarmonicsWork(12) = False Then
                        radbutPureHarm.Checked = True
                    ElseIf chkHarmonicsWork(1) = False And chkHarmonicsWork(2) = True And chkHarmonicsWork(3) = False And chkHarmonicsWork(4) = True And chkHarmonicsWork(5) = False And chkHarmonicsWork(6) = True And chkHarmonicsWork(7) = False And chkHarmonicsWork(8) = True And chkHarmonicsWork(9) = False And chkHarmonicsWork(10) = True And chkHarmonicsWork(11) = False And chkHarmonicsWork(12) = True Then
                        radbutEvenHarm.Checked = True
                    ElseIf chkHarmonicsWork(2) = False And chkHarmonicsWork(1) = True And chkHarmonicsWork(4) = False And chkHarmonicsWork(3) = True And chkHarmonicsWork(6) = False And chkHarmonicsWork(5) = True And chkHarmonicsWork(8) = False And chkHarmonicsWork(7) = True And chkHarmonicsWork(10) = False And chkHarmonicsWork(9) = True And chkHarmonicsWork(12) = False And chkHarmonicsWork(11) = True Then
                        radbutOddHarm.Checked = True
                    Else
                        radbutOtherHarm.Checked = True
                    End If
                Next i
        End Select
    End Sub

    Private Sub mnuCompareGraphs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompareGraphs.Click
        frmCompareGraphs.Show()
    End Sub

    Private Sub mnuGaitIndices_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGaitIndices.Click
        frmGaitIndexes.Show()
    End Sub

    Private Sub mnuWindow_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuWindow.DropDownItemClicked
        Dim sForm As String
        Dim frmShowForm As Form

        sForm = e.ClickedItem.Text
        For Me.i = System.Windows.Forms.Application.OpenForms.Count To 1 Step -1
            If System.Windows.Forms.Application.OpenForms.Item(i - 1).Text = sForm Then
                frmShowForm = System.Windows.Forms.Application.OpenForms.Item(i - 1)
                frmShowForm.Focus()
                i = 0
            End If
        Next i

    End Sub

    Private Sub mnuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        frmAbout.Show()
    End Sub

    Private Sub butUpIncrement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butUpIncrement.Click
        Dim nText As Single = Val(txtScaleValue.Text)
        Select Case Val(lblWhichGraph.Text)
            Case 1, 2, 3
                If nText <= 50 Then
                    txtScaleValue.Text = Int(nText + 10)
                Else
                    txtScaleValue.Text = Int(nText) + 20
                End If
            Case 10, 11, 12
                If nText < 5 Then
                    txtScaleValue.Text = Int(nText + 1)
                ElseIf nText < 50 Then
                    txtScaleValue.Text = Int(nText + 5)
                ElseIf nText < 100 Then
                    txtScaleValue.Text = Int(nText + 10)
                Else
                    txtScaleValue.Text = Int(nText + 20)
                End If
            Case 20, 21, 22, 30, 31, 32
                If nText < 1.5 Then
                    txtScaleValue.Text = FormatNumber(nText + 0.25, "0.00")
                ElseIf nText < 5 Then
                    txtScaleValue.Text = FormatNumber(nText + 0.5, "0.00")
                ElseIf nText < 7.5 Then
                    txtScaleValue.Text = FormatNumber(nText + 1.25, "0.00")
                ElseIf nText < 15 Then
                    txtScaleValue.Text = FormatNumber(nText + 2.5, "0.00")
                ElseIf nText < 50 Then
                    txtScaleValue.Text = Int(nText + 5)
                ElseIf nText < 100 Then
                    txtScaleValue.Text = Int(nText + 10)
                Else
                    Exit Sub
                End If
            Case 40, 41, 42, 50, 51, 52, 60
                If nText < 0.25 Then
                    txtScaleValue.Text = FormatNumber(nText + 0.05, "0.00")
                ElseIf nText < 5 Then
                    txtScaleValue.Text = FormatNumber(nText + 0.25, "0.00")
                ElseIf nText < 25 Then
                    txtScaleValue.Text = FormatNumber(nText + 2.5, "0.0")
                ElseIf nText < 100 Then
                    txtScaleValue.Text = Int(nText + 5)
                ElseIf nText < 250 Then
                    txtScaleValue.Text = Int(nText + 10)
                Else
                    Exit Sub
                End If
            Case 70
                txtScaleValue.Text = Int(nText + 10)
            Case 83, 84, 85, 86
                If nText < 5 Then
                    txtScaleValue.Text = FormatNumber(nText + 0.5, "0.0")
                ElseIf nText < 25 Then
                    txtScaleValue.Text = FormatNumber(nText + 2.5, "0.0")
                ElseIf nText < 125 Then
                    txtScaleValue.Text = Int(nText + 5)
                ElseIf nText < 500 Then
                    txtScaleValue.Text = Int(nText + 25)
                ElseIf nText < 1250 Then
                    txtScaleValue.Text = Int(nText + 50)
                Else
                    Exit Sub
                End If
        End Select
    End Sub

    Private Sub butDownIncrement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDownIncrement.Click
        Dim nText As Single = Val(txtScaleValue.Text)
        Dim nValue As Single
        Select Case Val(lblWhichGraph.Text)
            Case 1, 2, 3
                If nText <= 10 Then
                    Exit Sub
                ElseIf nText <= 60 Then
                    txtScaleValue.Text = Int(nText - 10)
                Else
                    txtScaleValue.Text = Int(nText - 20)
                End If
            Case 10, 11, 12
                If nText <= 1 Then
                    Exit Sub
                ElseIf nText <= 6 Then
                    txtScaleValue.Text = Int(nText - 1)
                ElseIf nText <= 60 Then
                    txtScaleValue.Text = Int(nText - 5)
                ElseIf nText <= 100 Then
                    txtScaleValue.Text = Int(nText - 10)
                Else
                    txtScaleValue.Text = Int(nText - 20)
                End If
            Case 20, 21, 22, 30, 31, 32
                If nText <= 0.5 Then
                    Exit Sub
                ElseIf nText <= 1.5 Then
                    txtScaleValue.Text = FormatNumber(nText - 0.25, "0.00")
                ElseIf nText <= 5 Then
                    txtScaleValue.Text = FormatNumber(nText - 0.5, "0.00")
                ElseIf nText <= 7.5 Then
                    txtScaleValue.Text = FormatNumber(nText - 1.25, "0.00")
                ElseIf nText <= 15 Then
                    txtScaleValue.Text = FormatNumber(nText - 2.5, "0.00")
                ElseIf nText <= 50 Then
                    txtScaleValue.Text = Int(nText - 5)
                Else
                    txtScaleValue.Text = Int(nText - 10)
                End If
            Case 40, 41, 42, 50, 51, 52, 60
                If nText <= 0.05 Then
                    Exit Sub
                ElseIf nText <= 0.25 Then
                    txtScaleValue.Text = FormatNumber(nText - 0.05, "0.00")
                ElseIf nText <= 5 Then
                    txtScaleValue.Text = FormatNumber(nText - 0.25, "0.00")
                ElseIf nText <= 25 Then
                    txtScaleValue.Text = FormatNumber(nText - 2.5, "0.0")
                ElseIf nText <= 100 Then
                    txtScaleValue.Text = Int(nText - 5)
                ElseIf nText <= 250 Then
                    txtScaleValue.Text = Int(nText - 10)
                End If
            Case 70
                If nText <= 10 Then
                    Exit Sub
                Else
                    txtScaleValue.Text = Int(nText - 10)
                End If
            Case 83, 84, 85, 86
                If nText <= 0.5 Then
                    Exit Sub
                ElseIf nText <= 5 Then
                    nValue = nText - 0.5
                    txtScaleValue.Text = nText.ToString("0.0")
                ElseIf nText <= 25 Then
                    nValue = nText - 2.5
                    txtScaleValue.Text = nValue.ToString("0.0")
                ElseIf nText <= 125 Then
                    txtScaleValue.Text = Int(nText - 5)
                ElseIf nText <= 500 Then
                    txtScaleValue.Text = Int(nText - 25)
                ElseIf nText <= 1250 Then
                    txtScaleValue.Text = Int(nText - 50)
                End If
        End Select
    End Sub

    Private Sub butScaleOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butScaleOK.Click
        If Val(txtScaleValue.Text) = 0 Then Exit Sub
        Dim nGraph As Integer
        nGraph = Val(lblWhichGraph.Text)
        Select Case gbxScale.Tag
            Case "X"
                arMaximumXValues(Val(lblWhichGraph.Text)) = Val(txtScaleValue.Text)
            Case "Y"
                Select Case nGraph
                    Case 1, 2, 3, 10, 11, 12
                        If bEnglishOrMetricUnits = False Then
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text)
                        Else
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / Lbs_To_Kgs
                        End If
                    Case 20, 21, 22, 30, 31, 32
                        If bEnglishOrMetricUnits = False Then
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / Feet_To_In
                        Else
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / Feet_To_cm
                        End If
                    Case 40, 41, 42, 50, 51, 52, 60
                        If bEnglishOrMetricUnits = False Then
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text)
                        Else
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / FtLbs_To_NewtonM
                        End If
                    Case 70
                        If bEnglishOrMetricUnits = False Then
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) * Feet_To_In
                        Else
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) * Feet_To_cm / Lbs_To_Kgs
                        End If
                    Case 83, 84, 85, 86
                        If bEnglishOrMetricUnits = False Then
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / Sensels_To_Inches
                        Else
                            arMaximumYValues(nGraph) = Val(txtScaleValue.Text) / Sensels_To_Cm
                        End If
                End Select
                subCreateYLabels()
        End Select

        gbxScale.Visible = False
    End Sub

    Private Sub butScaleCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butScaleCancel.Click
        gbxScale.Visible = False
    End Sub

    Private Sub butStatisticsExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStatisticsExit.Click
        gbxStatistics.Visible = False
    End Sub

    Private Sub butOKHarmonics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOKHarmonics.Click
        Dim bAtLeastOneCheckBoxIsChecked As Boolean
        lblWhichGraph.Text = "" 'By erasing the text, you trigger the lblWhichGraph Click Event.

        'Make sure that one of the 5 types of graphs is picked:
        If radbutHarmDisplGraph.Checked = False And radbutHarmVelGraph.Checked = False And radbutHarmForceGraph.Checked = False And radbutHarmPowerGraph.Checked = False And radbutHarmWorkGraph.Checked = False Then
            lblHarmonicPickInstructions.Text = "You must choose one of the 5 types of Graphs - Displacement, Velocity, Force, Power or Work"
            Exit Sub
        End If

        If radbutSum.Checked = True Or radbutDifference.Checked = True Then
            For Me.i = 1 To listCheckedHarmonicBoxes.Items.Count 'This FOR routine makes sure that at least one harmonic box is checked.
                If radbutHarmDisplGraph.Checked = True Then
                    If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                        chkHarmonicsDisplacement(i) = True
                        bAtLeastOneCheckBoxIsChecked = True
                    End If
                ElseIf radbutHarmVelGraph.Checked = True Then
                    If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                        chkHarmonicsVelocity(i) = True
                        bAtLeastOneCheckBoxIsChecked = True
                    End If
                ElseIf radbutHarmForceGraph.Checked = True Then
                    If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                        chkHarmonicsForce(i) = True
                        bAtLeastOneCheckBoxIsChecked = True
                    End If
                ElseIf radbutHarmPowerGraph.Checked = True Then
                    If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                        chkHarmonicsPower(i) = True
                        bAtLeastOneCheckBoxIsChecked = True
                    End If
                ElseIf radbutHarmWorkGraph.Checked = True Then
                    If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                        chkHarmonicsWork(i) = True
                        bAtLeastOneCheckBoxIsChecked = True
                    End If
                Else
                End If
            Next
            If bAtLeastOneCheckBoxIsChecked = False Then 'Make sure that at least one harmonic number is checked.
                lblHarmonicPickInstructions.Text = "You must check at least one of the Harmonic box Numbers on the right."
                Exit Sub
            End If
        End If

        'Now change the boolean values to indicate whether a box is checked.
        'Change the HarmonicValuesChecked for Each Percent of the Gait Cycle.
        If radbutHarmForceGraph.Checked = True Then 'This is for radiobutton FORCE
            For Me.i = 1 To 12
                If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                    chkHarmonicsForce(i) = True
                Else
                    chkHarmonicsForce(i) = False
                End If
            Next i
            For Me.i = 0 To 100
                arHarmonicsGraphingValuesForce(i) = 0
                For Me.j = 1 To 12
                    If chkHarmonicsForce(j - 1) = True Then
                        arHarmonicsGraphingValuesDisplacement(i) = arHarmonicsGraphingValuesForce(i) + arHarmonicValuesForce(j, 0) * System.Math.Cos((j * i * 0.01) * 2 * Math.PI) + arHarmonicValuesForce(j, 1) * System.Math.Sin((j * i * 0.01) * 2 * Math.PI)
                    End If
                Next j
            Next i
            If radbutSum.Checked = True Then
                lblWhichGraph.Text = conForce_Harm_Sum
            ElseIf radbutDifference.Checked = True Then
                lblWhichGraph.Text = conForce_Harm_Diff
            ElseIf radbutAmplitudes.Checked = True Then
                lblWhichGraph.Text = conForce_Harm_Amp
            ElseIf radbutEquation.Checked = True Then
                lblWhichGraph.Text = conForce_Harm_Eq
            Else
                lblHarmonicPickInstructions.Text = "You must pick either the Harmonic Sum, the Harmonic Difference, the Harmonic Amplitudes, or the Harmonic Equation button "
            End If

        ElseIf radbutHarmDisplGraph.Checked = True Then
            For Me.i = 1 To 12
                If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                    chkHarmonicsDisplacement(i) = True
                Else
                    chkHarmonicsDisplacement(i) = False
                End If
            Next i
            For Me.i = 0 To 100
                arHarmonicsGraphingValuesDisplacement(i) = 0
                For Me.j = 1 To 12
                    If chkHarmonicsDisplacement(j) = True Then
                        arHarmonicsGraphingValuesDisplacement(i) = arHarmonicsGraphingValuesDisplacement(i) + arHarmonicValuesDisplacement(j, 0) * System.Math.Cos((j * i * 0.01) * 2 * Math.PI) + arHarmonicValuesDisplacement(j, 1) * System.Math.Sin((j * i * 0.01) * 2 * Math.PI)
                    End If
                Next j
            Next i
            If radbutSum.Checked = True Then
                lblWhichGraph.Text = conDisp_Harm_Sum
            ElseIf radbutDifference.Checked = True Then
                lblWhichGraph.Text = conDisp_Harm_Diff
            ElseIf radbutAmplitudes.Checked = True Then
                lblWhichGraph.Text = conDisp_Harm_Amp
            ElseIf radbutEquation.Checked = True Then
                lblWhichGraph.Text = conDisp_Harm_Eq
            Else
                lblHarmonicPickInstructions.Text = "You must pick either the Harmonic Sum, the Harmonic Difference, the Harmonic Amplitudes, or the Harmonic Equation button "
            End If

        ElseIf radbutHarmVelGraph.Checked = True Then
            For Me.i = 1 To 12
                If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                    chkHarmonicsVelocity(i) = True
                Else
                    chkHarmonicsVelocity(i) = False
                End If
            Next i
            For Me.i = 0 To 100
                arHarmonicsGraphingValuesVelocity(i) = 0
                For Me.j = 1 To 12
                    If chkHarmonicsVelocity(j) = True Then
                        arHarmonicsGraphingValuesVelocity(i) = arHarmonicsGraphingValuesVelocity(i) + arHarmonicValuesVelocity(j, 0) * System.Math.Cos((j * i * 0.01) * 2 * Math.PI) + arHarmonicValuesVelocity(j, 1) * System.Math.Sin((j * i * 0.01) * 2 * Math.PI)
                    End If
                Next j
            Next i
            If radbutSum.Checked = True Then
                lblWhichGraph.Text = conVel_Harm_Sum
            ElseIf radbutDifference.Checked = True Then
                lblWhichGraph.Text = conVel_Harm_Diff
            ElseIf radbutAmplitudes.Checked = True Then
                lblWhichGraph.Text = conVel_Harm_Amp
            ElseIf radbutEquation.Checked = True Then
                lblWhichGraph.Text = conVel_Harm_Eq
            Else
                lblHarmonicPickInstructions.Text = "You must pick either the Harmonic Sum, the Harmonic Difference, the Harmonic Amplitudes, or the Harmonic Equation button "
            End If

        ElseIf radbutHarmPowerGraph.Checked = True Then
            For Me.i = 1 To 12
                If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                    chkHarmonicsPower(i) = True
                Else
                    chkHarmonicsPower(i) = False
                End If
            Next i
            For Me.i = 0 To 100
                arHarmonicsGraphingValuesPower(i) = 0
                For Me.j = 1 To 12
                    If chkHarmonicsPower(j) = True Then
                        arHarmonicsGraphingValuesPower(i) = arHarmonicsGraphingValuesPower(i) + arHarmonicValuesPower(j, 0) * System.Math.Cos((j * i * 0.01) * 2 * Math.PI) + arHarmonicValuesPower(j, 1) * System.Math.Sin((j * i * 0.01) * 2 * Math.PI)
                    End If
                Next j
            Next i
            If radbutSum.Checked = True Then
                lblWhichGraph.Text = conPower_Harm_Sum
            ElseIf radbutDifference.Checked = True Then
                lblWhichGraph.Text = conPower_Harm_Diff
            ElseIf radbutAmplitudes.Checked = True Then
                lblWhichGraph.Text = conPower_Harm_Amp
            ElseIf radbutEquation.Checked = True Then
                lblWhichGraph.Text = conPower_Harm_Eq
            Else
                lblHarmonicPickInstructions.Text = "You must pick either the Harmonic Sum, the Harmonic Difference, the Harmonic Amplitudes, or the Harmonic Equation button "
            End If

        ElseIf radbutHarmWorkGraph.Checked = True Then
            For Me.i = 1 To 12
                If listCheckedHarmonicBoxes.GetItemCheckState(i - 1) = CheckState.Checked Then
                    chkHarmonicsWork(i) = True
                Else
                    chkHarmonicsWork(i) = False
                End If
            Next i
            For Me.i = 0 To 100
                arHarmonicsGraphingValuesWork(i) = 0
                For Me.j = 1 To 12
                    If chkHarmonicsWork(j) = True Then
                        arHarmonicsGraphingValuesWork(i) = arHarmonicsGraphingValuesWork(i) + arHarmonicValuesWork(j, 0) * System.Math.Cos((j * i * 0.01) * 2 * Math.PI) + arHarmonicValuesWork(j, 1) * System.Math.Sin((j * i * 0.01) * 2 * Math.PI)
                    End If
                Next j
            Next i
            If radbutSum.Checked = True Then
                lblWhichGraph.Text = conWork_Harm_Sum
            ElseIf radbutDifference.Checked = True Then
                lblWhichGraph.Text = conWork_Harm_Diff
            ElseIf radbutAmplitudes.Checked = True Then
                lblWhichGraph.Text = conWork_Harm_Amp
            ElseIf radbutEquation.Checked = True Then
                lblWhichGraph.Text = conWork_Harm_Eq
            Else
                lblHarmonicPickInstructions.Text = "You must pick either the Harmonic Sum, the Harmonic Difference, the Harmonic Amplitudes, or the Harmonic Equation button "
            End If

        End If

        gbxPickHarmonics.Visible = False
        pnlGraph.Refresh()
    End Sub

    Private Sub butCancelHarmonics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancelHarmonics.Click
        gbxPickHarmonics.Visible = False
    End Sub

    Private Sub butSuperimpose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSuperimpose.Click
        If butSuperimpose.Text = "Compare Left and Right" Then
            butSuperimpose.Text = "Show full gait cycle"
            butSuperimpose.FlatStyle = FlatStyle.Flat
        Else
            butSuperimpose.Text = "Compare Left and Right"
            butSuperimpose.FlatStyle = FlatStyle.Standard
        End If
        pnlGraph.Refresh()
    End Sub

    Private Sub radbutHarmDisplGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If i = 1 Then
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                Else
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                End If
            Next
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutHarmVelGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If i = 1 Then
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                Else
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                End If
            Next
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutHarmForceGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If i = 1 Then
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                Else
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                End If
            Next
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutHarmPowerGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If i = 3 Then
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                Else
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                End If
            Next
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutHarmWorkGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If i = 3 Then
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
                Else
                    listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
                End If
            Next
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutSum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutSum.Click
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutDifference_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutDifference.Click
        subAddInstruxToPickHarmonics()
    End Sub
    Private Sub radbutAmplitudes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutAmplitudes.Click
        For Me.i = 0 To 11
            listCheckedHarmonicBoxes.SetItemChecked(i, False)
        Next
        radbutEvenHarm.Checked = False
        radbutOddHarm.Checked = False
        radbutPureHarm.Checked = False
        radbutOtherHarm.Checked = False
        listCheckedHarmonicBoxes.Enabled = False
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutEquation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutEquation.Click
        For Me.i = 0 To 11
            listCheckedHarmonicBoxes.SetItemChecked(i, False)
        Next
        radbutEvenHarm.Checked = False
        radbutOddHarm.Checked = False
        radbutPureHarm.Checked = False
        radbutOtherHarm.Checked = False
        listCheckedHarmonicBoxes.Enabled = False
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutPureHarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutPureHarm.Click
        listCheckedHarmonicBoxes.Enabled = True
        If radbutPureHarm.Checked = True Then
            For Me.i = 0 To 11
                If radbutHarmDisplGraph.Checked = True Or radbutHarmVelGraph.Checked = True Or radbutHarmForceGraph.Checked = True Then
                    Select Case i
                        Case 1
                            listCheckedHarmonicBoxes.SetItemChecked(i, True)
                        Case Else
                            listCheckedHarmonicBoxes.SetItemChecked(i, False)
                    End Select
                ElseIf radbutHarmPowerGraph.Checked = True Or radbutHarmWorkGraph.Checked = True Then
                    Select Case i
                        Case 3
                            listCheckedHarmonicBoxes.SetItemChecked(i, True)
                        Case Else
                            listCheckedHarmonicBoxes.SetItemChecked(i, False)
                    End Select
                End If
            Next i
        End If
        If radbutAmplitudes.Checked = True Or radbutEquation.Checked = True Then  'If the amplitude or the equation boxes are checked then go to the sum by default.
            radbutSum.Checked = True
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutEvenHarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutEvenHarm.Click
        gbxPickHarmonics.Enabled = True
        If radbutEvenHarm.Checked = True Then
            For Me.i = 0 To 5
                listCheckedHarmonicBoxes.SetItemChecked(2 * i, False)
                listCheckedHarmonicBoxes.SetItemChecked(2 * i + 1, True)
            Next i
        Else
            For Me.i = 0 To 11
                listCheckedHarmonicBoxes.SetItemChecked(i, False)
            Next i
        End If
        If radbutAmplitudes.Checked = True Or radbutEquation.Checked = True Then  'If the amplitude or the equation boxes are checked then go to the sum by default.
            radbutSum.Checked = True
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutOddHarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutOddHarm.Click
        gbxPickHarmonics.Enabled = True
        If radbutOddHarm.Checked = True Then
            For Me.i = 0 To 5
                listCheckedHarmonicBoxes.SetItemChecked(2 * i, True)
                listCheckedHarmonicBoxes.SetItemChecked(2 * i + 1, False)
            Next
        Else
            For Me.i = 0 To 11
                listCheckedHarmonicBoxes.SetItemChecked(i, False)
            Next i
        End If
        If radbutAmplitudes.Checked = True Or radbutEquation.Checked = True Then  'If the amplitude or the equation boxes are checked then go to the sum by default.
            radbutSum.Checked = True
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub radbutOtherHarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radbutOtherHarm.Click
        listCheckedHarmonicBoxes.Enabled = True
    End Sub

    Private Sub listCheckedHarmonicBoxes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles listCheckedHarmonicBoxes.Click
        If radbutAmplitudes.Checked = True Or radbutEquation.Checked = True Then 'If the amplitude or the equation boxes are checked then go to the sum by default.
            Exit Sub
        End If

        i = listCheckedHarmonicBoxes.SelectedIndex
        If listCheckedHarmonicBoxes.GetItemCheckState(i) = CheckState.Checked Then
            listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Unchecked)
        Else
            listCheckedHarmonicBoxes.SetItemCheckState(i, CheckState.Checked)
        End If

        Dim bCh(11) As Boolean
        For Me.i = 0 To 11
            bCh(i) = listCheckedHarmonicBoxes.GetItemCheckState(i)
        Next
        If bCh(0) = True And bCh(2) = True And bCh(4) = True And bCh(6) = True And bCh(8) = True And bCh(10) = True Then
            If bCh(1) = False And bCh(3) = False And bCh(5) = False And bCh(7) = False And bCh(9) = False And bCh(11) = False Then
                radbutOddHarm.Checked = True
            Else
                radbutOtherHarm.Checked = True
            End If
        ElseIf bCh(1) = True And bCh(3) = True And bCh(5) = True And bCh(7) = True And bCh(9) = True And bCh(11) = True Then
            If bCh(0) = False And bCh(2) = False And bCh(4) = False And bCh(6) = False And bCh(8) = False And bCh(10) = False Then
                radbutEvenHarm.Checked = True
            Else
                radbutOtherHarm.Checked = True
            End If
        ElseIf bCh(0) = False And bCh(1) = True And bCh(2) = False And bCh(3) = False And bCh(4) = False And bCh(5) = False And bCh(6) = False And bCh(7) = False And bCh(8) = False And bCh(9) = False And bCh(10) = False And bCh(11) = False Then
            If radbutHarmDisplGraph.Checked = True Or radbutHarmVelGraph.Checked = True Or radbutHarmForceGraph.Checked = True Then
                radbutPureHarm.Checked = True
            End If
        ElseIf bCh(0) = False And bCh(1) = False And bCh(2) = False And bCh(3) = True And bCh(4) = False And bCh(5) = False And bCh(6) = False And bCh(7) = False And bCh(8) = False And bCh(9) = False And bCh(10) = False And bCh(11) = False Then
            If radbutHarmPowerGraph.Checked = True Or radbutHarmWorkGraph.Checked = True Then
                radbutPureHarm.Checked = True
            End If
        Else
            radbutOtherHarm.Checked = True
        End If
        subAddInstruxToPickHarmonics()
    End Sub

    Private Sub lblWhichGraph_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblWhichGraph.TextChanged

        Dim nGraph As Integer
        nGraph = Val(lblWhichGraph.Text) 'This is the graph number.
        If nGraph < 1 Then Exit Sub

        Select Case nGraph
            Case 1
                lblGraphTitle.Text = "Force Under Feet - All Steps"
            Case 2, 3
                lblGraphTitle.Text = "Average Force Under Each Foot"
            Case 10, 11, 12, 13, 14, 15 'FORCE GRAPHS
                lblGraphTitle.Text = "Total Average Force Under Feet"
            Case 20, 21, 22, 23, 24, 25 'DISPLACEMENT GRAPHS
                lblGraphTitle.Text = "Displacement of Center of Mass"
            Case 30, 31, 32, 33, 34, 35 'VELOCITY GRAPHS
                lblGraphTitle.Text = "Velocity of Center of Mass"
            Case 40, 41, 42, 43, 44, 45 'POWER GRAPHS
                lblGraphTitle.Text = "Power Exerted on Center of Mass"
            Case 50, 51, 52, 53, 54, 55 'WORK GRAPHS
                lblGraphTitle.Text = "Work by Ground on Center of Mass"
            Case 60 'ENERGY GRAPH
                lblGraphTitle.Text = "Energy of Center of Mass"
            Case 70 'SPRING CONSTANT
                lblGraphTitle.Text = "Spring Constant of Lower Extremities"
            Case conCoP_AP_ML
                lblGraphTitle.Text = "COP - Location - Lateral to Medial vs. Posterior to Anterior"
            Case conCoP_AP
                lblGraphTitle.Text = "COP - Location - Posterior to Anterior"
            Case conCoP_ML
                lblGraphTitle.Text = "COP - Location - Lateral to Medial"
            Case conCoP_AP_Vel
                lblGraphTitle.Text = "COP - Velocity - Posterior to Anterior"
            Case conCoP_ML_Vel
                lblGraphTitle.Text = "COP - Velocity - Lateral to Medial"
            Case conCoP_AP_Acc
                lblGraphTitle.Text = "COP - Acceleration - Posterior to Anterior"
            Case conCoP_ML_Acc
                lblGraphTitle.Text = "COP - Acceleration - Lateral to Medial"
        End Select

        Select Case Val(lblWhichGraph.Text) 'This Select section shows the Change to Angle button if you are showing the equation.
            Case 1
                lblXLabels.Text = nNumberOfStrides
                pnlYLabel.Visible = True
                butChangeToAngleFormula.Visible = False
            Case 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 33, 41, 42, 43, 51, 52, 53, 60, 70, 80, 81, 82, 83, 84, 85
                lblXLabels.Text = Int(0.1 * arMaximumXValues(Val(lblWhichGraph.Text)))
                pnlYLabel.Visible = True
                butChangeToAngleFormula.Visible = False
            Case 13, 23, 33, 43, 53
                lblXLabels.Text = "Harmonic Amplitudes"
                pnlYLabel.Visible = False
                butChangeToAngleFormula.Visible = False
            Case 14, 24, 34, 44, 54
                butChangeToAngleFormula.Visible = True
                butChangeToAngleFormula.Text = "Change to Angle Formula"
                lblXLabels.Text = "Harmonic Formula"
                pnlYLabel.Visible = False
            Case 15, 25, 35, 45, 55
                butChangeToAngleFormula.Visible = True
                butChangeToAngleFormula.Text = "Change to Standard Formula"
                pnlYLabel.Visible = False
        End Select

        Select Case nGraph
            Case 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55
                mnuScaleYAxis.Enabled = False
                mnuScaleXAxis.Enabled = False
            Case 1, 3
                mnuScaleYAxis.Enabled = True
                mnuScaleXAxis.Enabled = False
            Case 80, 81, 82
                mnuScaleYAxis.Enabled = False
                mnuScaleXAxis.Enabled = True
            Case Else
                mnuScaleYAxis.Enabled = True
                mnuScaleXAxis.Enabled = True
        End Select

        subCreateYLabels()  'Redo the Y axis labels

        subChangePanelFontSize()
        pnlGraph.Refresh()

    End Sub

    Private Sub lblMaximumXValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMaximumXValue.TextChanged
        'Whenever this value changes, then the value of the arMaximumXValues has to change
        If bFormLoading = True Then Exit Sub
        lblXLabels.Text = Int(0.1 * arMaximumXValues(Val(lblWhichGraph.Text)))
    End Sub

    Private Sub lblMaximumYValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMaximumYValue.TextChanged
        If bFormLoading = True Then Exit Sub
        If Val(lblWhichGraph.Text) > 0 Then
            subCreateYLabels()
        End If
    End Sub
    Private Sub lblXLabels_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblXLabels.TextChanged
        'This sub tells you how many X labels you need
        If lblXLabels.Text = "lblXLabel - Number of Labels" Then Exit Sub 'This is the label when the form is first loading.
        Select Case lblWhichGraph.Text
            Case 1
                subCreateXAxisLabels(nNumberOfStrides)
            Case 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 81, 82, 83, 84, 85, 86
                subCreateXAxisLabels(Int(Val(lblXLabels.Text)))
            Case 13, 23, 33, 43, 53
                subCreateXAxisLabels(12)
        End Select

    End Sub

    Private Sub butChangeToAngleFormula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butChangeToAngleFormula.Click
        Select Case Val(lblWhichGraph.Text)
            Case 14
                lblWhichGraph.Text = conForce_Harm_Eq_Angle
            Case 15
                lblWhichGraph.Text = conForce_Harm_Eq
            Case 24
                lblWhichGraph.Text = conDisp_Harm_Eq_Angle
            Case 25
                lblWhichGraph.Text = conDisp_Harm_Eq
            Case 34
                lblWhichGraph.Text = conVel_Harm_Eq_Angle
            Case 35
                lblWhichGraph.Text = conVel_Harm_Eq
            Case 44
                lblWhichGraph.Text = conPower_Harm_Eq_Angle
            Case 45
                lblWhichGraph.Text = conPower_Harm_Eq
            Case 54
                lblWhichGraph.Text = conWork_Harm_Eq_Angle
            Case 55
                lblWhichGraph.Text = conWork_Harm_Eq
        End Select
    End Sub

    Private Sub butChangeToAngleFormula_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles butChangeToAngleFormula.VisibleChanged
        If butChangeToAngleFormula.Visible = True Then
            Dim graphixBut As Graphics
            Dim graphixFontBox As SizeF
            graphixBut = butChangeToAngleFormula.CreateGraphics
            graphixFontBox = graphixBut.MeasureString(butChangeToAngleFormula.Text, butChangeToAngleFormula.Font)
            butChangeToAngleFormula.Width = 6 + graphixFontBox.Width
            butChangeToAngleFormula.Top = pnlGraph.Height - 1.1 * butChangeToAngleFormula.Height
        End If
    End Sub

    Private Sub gbxStatistics_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gbxStatistics.VisibleChanged
        Dim nValue As Single
        If gbxStatistics.Visible = True Then
            butStatisticsExit.Top = 8
            butStatisticsExit.Left = gbxStatistics.Width - butStatisticsExit.Width
            lblCadence.Text = "Cadence:  " & Int(Cadence + 0.5) & " Steps/Min."
            If bEnglishOrMetricUnits = 0 Then
                nValue = BodyMass / Lbs_To_Slugs
                lblBodyMass.Text = "Calculated Body Weight:  " & nValue.ToString("0.0") & " Lbs."
            Else
                nValue = Lbs_To_Kgs * BodyMass / Lbs_To_Slugs
                lblBodyMass.Text = "Calculated Body Weight:  " & nValue.ToString("0.0") & " Kgs."
            End If
            lblTimeInLeftDoubleSupport.Text = "Time in Double Support - Left:  " & nPctDoubleSupport_L & "%"
            lblTimeInLeftSingleSupport.Text = "Time in Single Support - Left:  " & nPctSingleSupport_L & "%"
            lblTimeInRightDoubleSupport.Text = "Time in Double Support - Right:  " & nPctDoubleSupport_R & "%"
            lblTimeInRightSingleSupport.Text = "Time in Single Support - Right:  " & nPctSingleSupport_R & "%"
            nValue = 0
            For Me.i = 1 To 12 'Calculate the total amplitude of the CoM movement.
                nValue = nValue + arHarmonicValuesDisplacement(i, con_Amp) ^ 2
            Next i
            nValue = nValue ^ 0.5
            If bEnglishOrMetricUnits = 0 Then
                lblCoMAmplitude.Text = "Total Amplitude CoM Movement: " & Format(nValue * 12, "0.00") & " in."
            Else
                lblCoMAmplitude.Text = "Total Amplitude CoM Movement: " & Format(nValue * 12 * 2.54, "0.00") & " cm."
            End If
            lblCoMPurityIndex.Text = "CoM Movement Purity Index: " & Format(GI.Purity, "0.00") 'Put the Purity Index in place.
            lblCoMAmplitude.AutoSize = True
            lblCoMAmplitude.Left = 2
            lblCoMPurityIndex.Left = lblCoMAmplitude.Left + lblCoMAmplitude.Width + 10

            lblTimeInRightDoubleSupport.Left = lblTimeInLeftDoubleSupport.Left + lblTimeInLeftDoubleSupport.Width + 10
            lblTimeInRightSingleSupport.Left = lblTimeInRightDoubleSupport.Left
        End If
    End Sub

    Private Sub gbxPickHarmonics_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gbxPickHarmonics.VisibleChanged
        gbxPickHarmonics.BringToFront()
        If gbxPickHarmonics.Visible = True Then
            gbxPickHarmonics.Left = 9
            If gbxHarmGraphType.Visible = False Then gbxHarmGraphType.Visible = True
            Select Case lblWhichGraph.Text
                Case 10, 11, 12, 13, 14, 15
                    radbutHarmForceGraph.Checked = True
                    Select Case lblWhichGraph.Text
                        Case 11
                            radbutSum.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsDisplacement(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 12
                            radbutDifference.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsDisplacement(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 13
                            radbutAmplitudes.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                        Case 14, 15
                            radbutEquation.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                    End Select
                Case 20, 21, 22, 23, 24, 25
                    radbutHarmDisplGraph.Checked = True
                    Select Case lblWhichGraph.Text
                        Case 21
                            radbutSum.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsDisplacement(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 22
                            radbutDifference.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsDisplacement(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 23
                            radbutAmplitudes.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                        Case 24, 25
                            radbutEquation.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                    End Select
                Case 30, 31, 32, 33, 34, 35
                    radbutHarmVelGraph.Checked = True
                    Select Case lblWhichGraph.Text
                        Case 31
                            radbutSum.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsVelocity(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 32
                            radbutDifference.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsVelocity(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 33
                            radbutAmplitudes.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                        Case 34, 35
                            radbutEquation.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                    End Select
                Case 40, 41, 42, 43, 44, 45
                    radbutHarmPowerGraph.Checked = True
                    Select Case lblWhichGraph.Text
                        Case 41
                            radbutSum.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsPower(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 42
                            radbutDifference.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsPower(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 43
                            radbutAmplitudes.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                        Case 44, 45
                            radbutEquation.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                    End Select
                Case 50, 51, 52, 53, 54, 55
                    radbutHarmWorkGraph.Checked = True
                    Select Case lblWhichGraph.Text
                        Case 51
                            radbutSum.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsWork(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 52
                            radbutDifference.Checked = True
                            For Me.i = 0 To 11
                                If chkHarmonicsWork(i + 1) = True Then listCheckedHarmonicBoxes.SetItemChecked(i, CheckState.Checked)
                            Next
                        Case 53
                            radbutAmplitudes.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                        Case 54, 55
                            radbutEquation.Checked = True
                            listCheckedHarmonicBoxes.Enabled = False
                    End Select
                Case Else
                    radbutHarmForceGraph.Checked = False
                    radbutHarmDisplGraph.Checked = False
                    radbutHarmVelGraph.Checked = False
                    radbutHarmPowerGraph.Checked = False
                    radbutHarmWorkGraph.Checked = False
            End Select
            subAddInstruxToPickHarmonics()
        End If
    End Sub

    Private Function subHarmonicNumberString(ByVal ii As Integer) As String
        subHarmonicNumberString = ""
        Dim sHarmonic(12) As String
        Select Case ii
            Case 1
                subHarmonicNumberString = "First Harmonic"
            Case 2
                subHarmonicNumberString = "Second Harmonic"
            Case 3
                subHarmonicNumberString = "Third Harmonic"
            Case 4
                subHarmonicNumberString = "Fourth Harmonic"
            Case 5
                subHarmonicNumberString = "Fifth Harmonic"
            Case 6
                subHarmonicNumberString = "Sixth Harmonic"
            Case 7
                subHarmonicNumberString = "Seventh Harmonic"
            Case 8
                subHarmonicNumberString = "Eighth Harmonic"
            Case 9
                subHarmonicNumberString = "Ninth Harmonic"
            Case 10
                subHarmonicNumberString = "Tenth Harmonic"
            Case 11
                subHarmonicNumberString = "Eleventh Harmonic"
            Case 12
                subHarmonicNumberString = "Twelfth Harmonic"
        End Select
    End Function

    Private Sub subAddInstruxToPickHarmonics()
        Dim sHarmon() As String = {"1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th"}
        Dim sCheckedHarmonics As String = ""
        For Me.i = 0 To 11
            If sCheckedHarmonics = "" Then
                If listCheckedHarmonicBoxes.GetItemCheckState(i) = CheckState.Checked Then
                    sCheckedHarmonics = "the " & sHarmon(i) & " Harmonic"
                    k = 1 'k will be used as a counter for how many check boxes are checked.
                End If
            Else 'If you already added at least one harmonic and want to add another
                If listCheckedHarmonicBoxes.GetItemCheckState(i) = CheckState.Checked Then
                    n = InStr(sCheckedHarmonics, "Harmonic")
                    If k = 1 Then
                        sCheckedHarmonics = Microsoft.VisualBasic.Left(sCheckedHarmonics, (n - 1))
                        sCheckedHarmonics = sCheckedHarmonics & "and " & sHarmon(i) & " Harmonics"
                        k = k + 1
                    Else
                        sCheckedHarmonics = Microsoft.VisualBasic.Left(sCheckedHarmonics, (n - 1))
                        sCheckedHarmonics = Replace(sCheckedHarmonics, " and ", ", ")
                        sCheckedHarmonics = sCheckedHarmonics & "and " & sHarmon(i) & " Harmonics"
                        k = k + 1
                    End If
                End If
            End If
        Next i

        If radbutHarmDisplGraph.Checked = True Then
            If radbutSum.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Even Harmonics for the Vertical Displacement of the Center of Mass. The greater the amplitude of this curve, the more symmetry is present between Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Odd Harmonics for the Vertical Displacement of the Center of Mass.  The greater the amplitude of this curve, the more asymmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the portion of the gait that is the Pure Harmonic for the Vertical Displacement of the Center of Mass.  The closer this is to the actual data, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    If sCheckedHarmonics <> "" Then
                        lblHarmonicPickInstructions.Text = "This is the sum of the " & sCheckedHarmonics & " for the Vertical Displacement of the Center of Mass."
                    Else
                        lblHarmonicPickInstructions.Text = "Pick at least one of the 12 Harmonics."
                    End If
                Else
                    lblHarmonicPickInstructions.Text = "Pick one of the four options to the right: Even Harmonics, Odd Harmonics, Pure Harmonic, or some other combination"
                End If
            ElseIf radbutDifference.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Even Harmonics for the Vertical Displacement of the Center of Mass and the actual data. The greater the amplitude of this curve, the more asymmetrical the gait between the Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Odd Harmonics for the Vertical Displacement of the Center of Mass and the actual data.  The greater the amplitude of this curve, the more symmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is difference between the Pure Harmonic for the Vertical Displacement of the Center of Mass and the actual data.  The closer this curve is to zero, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    If sCheckedHarmonics <> "" Then
                        lblHarmonicPickInstructions.Text = "This is the difference between the sum of the " & sCheckedHarmonics & " for the Vertical Displacement of the Center of Mass and the actual data."
                    Else
                        lblHarmonicPickInstructions.Text = "Pick at least one of the 12 Harmonics."
                    End If
                Else
                    lblHarmonicPickInstructions.Text = "Pick one of the four options to the right: Even Harmonics, Odd Harmonics, Pure Harmonic, or some other combination"
                End If
            ElseIf radbutAmplitudes.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows a bar graph for the Amplitude of the First 12 harmonics for the Displacement of the Center of Mass"
            ElseIf radbutEquation.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows the Fourier Equation for the Displacement of the Center of Mass"
            Else
                lblHarmonicPickInstructions.Text = "Pick whether you want to see the sum of harmonics, the difference between the harmonics and the data, the amplitude of all the harmonics, or the Fourier equation"
            End If
        ElseIf radbutHarmVelGraph.Checked = True Then
            If radbutSum.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Even Harmonics for the Vertical Velocity of the Center of Mass. The greater the amplitude of this curve, the more symmetry is present between Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Odd Harmonics for the Vertical Velocity of the Center of Mass.  The greater the amplitude of this curve, the more asymmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the portion of the gait that is the Pure Harmonic for the Vertical Velocity of the Center of Mass.  The closer this is to the actual data, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    If sCheckedHarmonics <> "" Then
                        lblHarmonicPickInstructions.Text = "This is the sum of the " & sCheckedHarmonics & " for the Vertical Velocity of the Center of Mass."
                    Else
                        lblHarmonicPickInstructions.Text = "Pick at least one of the 12 Harmonics."
                    End If
                End If
            ElseIf radbutDifference.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Even Harmonics for the Vertical Velocity of the Center of Mass and the actual data. The greater the amplitude of this curve, the more asymmetrical the gait between the Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Odd Harmonics for the Vertical Velocity of the Center of Mass and the actual data.  The greater the amplitude of this curve, the more symmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is difference between the Pure Harmonic for the Vertical Velocity of the Center of Mass and the actual data.  The closer this curve is to zero, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    If sCheckedHarmonics <> "" Then
                        lblHarmonicPickInstructions.Text = "This is the difference between the sum of the " & sCheckedHarmonics & " for the Vertical Velocity of the Center of Mass and the actual data."
                    Else
                        lblHarmonicPickInstructions.Text = "Pick at least one of the 12 Harmonics."
                    End If
                End If
            ElseIf radbutAmplitudes.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows a bar graph for the Amplitude of the First 12 harmonics for the Velocity of the Center of Mass"
            ElseIf radbutEquation.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows the Fourier Equation for the Velocity of the Center of Mass"
            Else
                lblHarmonicPickInstructions.Text = "Pick whether you want to see the sum of harmonics, the difference between the harmonics and the data, the amplitude of all the harmonics, or the Fourier equation"
            End If
        ElseIf radbutHarmForceGraph.Checked = True Then
            If radbutSum.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Even Harmonics for the Vertical Ground Force. The greater the amplitude of this curve, the more symmetry is present between Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Odd Harmonics for the Vertical Ground Force.  The greater the amplitude of this curve, the more asymmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the portion of the gait that is the Pure Harmonic for the Vertical Ground Force.  The closer this is to the actual data, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the sum of the " & sCheckedHarmonics & " for the Vertical Ground Force."
                End If
            ElseIf radbutDifference.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Even Harmonics for the Vertical Ground Force and the actual data. The greater the amplitude of this curve, the more asymmetrical the gait between the Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Odd Harmonics for the Vertical Ground Force and the actual data.  The greater the amplitude of this curve, the more symmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is difference between the Pure Harmonic for the Vertical Ground Force and the actual data.  The closer this curve is to zero, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the difference between the sum of the " & sCheckedHarmonics & " for the Vertical Ground Force and the actual data."
                End If
            ElseIf radbutAmplitudes.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows a bar graph for the Amplitude of the First 12 harmonics for the Force of the Ground"
            ElseIf radbutEquation.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows the Fourier Equation for the Force Exerted by the Ground, in terms of the Body Weight"
            Else
                lblHarmonicPickInstructions.Text = "Pick whether you want to see the sum of harmonics, the difference between the harmonics and the data, the amplitude of all the harmonics, or the Fourier equation"
            End If
        ElseIf radbutHarmPowerGraph.Checked = True Then
            If radbutSum.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Even Harmonics for the Vertical Power of the Body. The greater the amplitude of this curve, the more symmetry is present between Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the summation of the Odd Harmonics for the Vertical Power of the Body.  The greater the amplitude of this curve, the more asymmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the portion of the gait that is the Pure Harmonic for the Vertical Power of the Body.  The closer this is to the actual data, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the sum of the " & sCheckedHarmonics & " for the Vertical Power of the Body."
                End If
            ElseIf radbutDifference.Checked = True Then
                If radbutEvenHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Even Harmonics for the Vertical Power of the Body and the actual data. The greater the amplitude of this curve, the more asymmetrical the gait between the Left and Right sides."
                ElseIf radbutOddHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the difference between the Odd Harmonics for the Vertical Power of the Body and the actual data.  The greater the amplitude of this curve, the more symmetrical the gait."
                ElseIf radbutPureHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is difference between the Pure Harmonic for the Vertical Power of the Body and the actual data.  The closer this curve is to zero, the better the gait pattern."
                ElseIf radbutOtherHarm.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This is the difference between the sum of the " & sCheckedHarmonics & " for the Vertical Power of the Body and the actual data."
                End If
            ElseIf radbutAmplitudes.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows a bar graph for the Amplitude of the First 12 harmonics for the Power of the Body."
            ElseIf radbutEquation.Checked = True Then
                lblHarmonicPickInstructions.Text = "This shows the Fourier Equation for the Power Exerted on the Body."
            Else
                lblHarmonicPickInstructions.Text = "Pick whether you want to see the sum of harmonics, the difference between the harmonics and the data, the amplitude of all the harmonics, or the Fourier equation"
            End If
        ElseIf radbutHarmWorkGraph.Checked = True Then
            If radbutSum.Checked = True Then
                If radbutSum.Checked = True Then
                    If radbutEvenHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This shows the summation of the Even Harmonics for the Work Done by the Body. The greater the amplitude of this curve, the more symmetry is present between Left and Right sides."
                    ElseIf radbutOddHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This shows the summation of the Odd Harmonics for the Work Done by the Body.  The greater the amplitude of this curve, the more asymmetrical the gait."
                    ElseIf radbutPureHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This is the portion of the gait that is the Pure Harmonic for the Work Done by the Body.  The closer this is to the actual data, the better the gait pattern."
                    ElseIf radbutOtherHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This is the sum of the " & sCheckedHarmonics & " for the Work Done by the Body."
                    End If
                ElseIf radbutDifference.Checked = True Then
                    If radbutEvenHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This shows the difference between the Even Harmonics for the Work Done by the Body and the actual data. The greater the amplitude of this curve, the more asymmetrical the gait between the Left and Right sides."
                    ElseIf radbutOddHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This shows the difference between the Odd Harmonics for the Work Done by the Body and the actual data.  The greater the amplitude of this curve, the more symmetrical the gait."
                    ElseIf radbutPureHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This is difference between the Pure Harmonic for the Work Done by the Body and the actual data.  The closer this curve is to zero, the better the gait pattern."
                    ElseIf radbutOtherHarm.Checked = True Then
                        lblHarmonicPickInstructions.Text = "This is the difference between the sum of the " & sCheckedHarmonics & " for the Work Done by the Body and the actual data."
                    End If
                ElseIf radbutAmplitudes.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows a bar graph for the Amplitude of the First 12 harmonics for the Work Done by the Body."
                ElseIf radbutEquation.Checked = True Then
                    lblHarmonicPickInstructions.Text = "This shows the Fourier Equation for the Work Done by the Body"
                Else
                    lblHarmonicPickInstructions.Text = "Pick whether you want to see the sum of harmonics, the difference between the harmonics and the data, the amplitude of all the harmonics, or the Fourier equation"
                End If
            End If
        Else 'If none of the top buttons are pushed
            lblHarmonicPickInstructions.Text = "Click whether you want to see Displacement, Work, Force, Power or Work Harmonics"
        End If
    End Sub
    Private Sub subChangeGraph()
        Select Case Val(lblWhichGraph.Text)
            Case conForce_AllSteps
                pnlGraph.BackgroundImage = bitGraph01
            Case conForce_Avg
                pnlGraph.BackgroundImage = bitGraph02
            Case conForce_Radial
                pnlGraph.BackgroundImage = bitGraph03
            Case conForce_As_BW
                pnlGraph.BackgroundImage = bitGraph10
            Case conForce_Harm_Sum
                pnlGraph.BackgroundImage = bitGraph11
            Case conForce_Harm_Diff
                pnlGraph.BackgroundImage = bitGraph12
            Case conForce_Harm_Amp
                pnlGraph.BackgroundImage = bitGraph13
            Case conForce_Harm_Eq
                pnlGraph.BackgroundImage = bitGraph14
            Case conForce_Harm_Eq_Angle
                pnlGraph.BackgroundImage = bitGraph15
            Case conDisplacement_Vert
                pnlGraph.BackgroundImage = bitGraph20
            Case conDisp_Harm_Sum
                pnlGraph.BackgroundImage = bitGraph21
            Case conDisp_Harm_Diff
                pnlGraph.BackgroundImage = bitGraph22
            Case conDisp_Harm_Amp
                pnlGraph.BackgroundImage = bitGraph23
            Case conDisp_Harm_Eq
                pnlGraph.BackgroundImage = bitGraph24
            Case conDisp_Harm_Eq_Angle
                pnlGraph.BackgroundImage = bitGraph25
            Case conVelocity_Vert
                pnlGraph.BackgroundImage = bitGraph30
            Case conVel_Harm_Sum
                pnlGraph.BackgroundImage = bitGraph31
            Case conVel_Harm_Diff
                pnlGraph.BackgroundImage = bitGraph32
            Case conVel_Harm_Amp
                pnlGraph.BackgroundImage = bitGraph33
            Case conVel_Harm_Eq
                pnlGraph.BackgroundImage = bitGraph34
            Case conVel_Harm_Eq_Angle
                pnlGraph.BackgroundImage = bitGraph35
            Case conPower_Vert
                pnlGraph.BackgroundImage = bitGraph40
            Case conPower_Harm_Sum
                pnlGraph.BackgroundImage = bitGraph41
            Case conPower_Harm_Diff
                pnlGraph.BackgroundImage = bitGraph42
            Case conPower_Harm_Amp
                pnlGraph.BackgroundImage = bitGraph43
            Case conPower_Harm_Eq
                pnlGraph.BackgroundImage = bitGraph44
            Case conPower_Harm_Eq_Angle
                pnlGraph.BackgroundImage = bitGraph45
            Case conWork_Vert
                pnlGraph.BackgroundImage = bitGraph50
            Case conWork_Harm_Sum
                pnlGraph.BackgroundImage = bitGraph51
            Case conWork_Harm_Diff
                pnlGraph.BackgroundImage = bitGraph52
            Case conWork_Harm_Amp
                pnlGraph.BackgroundImage = bitGraph53
            Case conWork_Harm_Eq
                pnlGraph.BackgroundImage = bitGraph54
            Case conWork_Harm_Eq_Angle
                pnlGraph.BackgroundImage = bitGraph55
            Case conEnergy
                pnlGraph.BackgroundImage = bitGraph60
            Case conSpringConstants
                pnlGraph.BackgroundImage = bitGraph70
            Case conCoP_AP_ML
                pnlGraph.BackgroundImage = bitGraph80
            Case conCoP_AP
                pnlGraph.BackgroundImage = bitGraph81
            Case conCoP_ML
                pnlGraph.BackgroundImage = bitGraph82
            Case conCoP_AP_Vel
                pnlGraph.BackgroundImage = bitGraph83
            Case conCoP_ML_Vel
                pnlGraph.BackgroundImage = bitGraph84
            Case conCoP_AP_Acc
                pnlGraph.BackgroundImage = bitGraph85
            Case conCoP_ML_Acc
                pnlGraph.BackgroundImage = bitGraph86
        End Select
    End Sub

    Private Sub subChangePanelFontSize()
        'When the panel changes its size, you have to change the font size for the title and also the equation.
        Dim sLine As String = " "
        Dim nValue As Single
        Select Case Val(lblWhichGraph.Text)
            Case 14, 15
                If bEnglishOrMetricUnits = False Then
                    nValue = arHarmonicValuesForce(2, 0)
                    sLine = nValue.ToString("00.00") & "cos(12*x)"
                    nValue = arHarmonicValuesForce(2, 1)
                    sLine = sLine + nValue.ToString("00.00") & " sin(12*x)"
                Else
                    nValue = arHarmonicValuesForce(2, 0) * Lbs_To_Kgs
                    sLine = nValue.ToString("00.00") & "cos(12*x)"
                    nValue = arHarmonicValuesForce(2, 1) * Lbs_To_Kgs
                    sLine = sLine + nValue.ToString("00.00") & " sin(12*x)"
                End If
            Case 24, 25
                If bEnglishOrMetricUnits = False Then
                    nValue = arHarmonicValuesDisplacement(2, 0) * Feet_To_In
                    sLine = nValue.ToString("00.00") & " cos(12*x) + "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_cm
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                Else
                    nValue = arHarmonicValuesDisplacement(2, 0) * Feet_To_cm
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_cm
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                End If
            Case 34, 35
                If bEnglishOrMetricUnits = False Then
                    nValue = arHarmonicValuesVelocity(2, 0) * Feet_To_In
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_In
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                Else
                    nValue = arHarmonicValuesDisplacement(2, 0) * Feet_To_cm
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_cm
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                End If
            Case 44, 45
                If bEnglishOrMetricUnits = False Then
                    nValue = arHarmonicValuesVelocity(2, 0) * Feet_To_In
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_In
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                Else
                    nValue = arHarmonicValuesDisplacement(2, 0) * Feet_To_cm
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_cm
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                End If
            Case 54, 55
                If bEnglishOrMetricUnits = False Then
                    nValue = arHarmonicValuesVelocity(2, 0) * Feet_To_In
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_In
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                Else
                    nValue = arHarmonicValuesDisplacement(2, 0) * Feet_To_cm
                    sLine = nValue.ToString("00.00") & " cos(12*x)  +  "
                    nValue = arHarmonicValuesDisplacement(2, 1) * Feet_To_cm
                    sLine = sLine & nValue.ToString("00.00") & " sin(12*x)"
                End If
            Case Else
                Exit Sub 'For all other graphs exi't the sub.
        End Select
        Dim gP As Graphics
        gP = pnlGraph.CreateGraphics
        Dim nFntSz As Integer = 1
        Dim fntA As New Font(pnlGraph.Font.Name, nFntSz)
        rectText = gP.MeasureString(sLine, fntA, pnlGraph.Width)
        Dim nTextWidth As Integer = rectText.Width
        Dim nTextHeight As Integer = rectText.Height
        Do Until nTextWidth > pnlGraph.Width
            nFntSz = nFntSz + 1
            fntA = New Font(pnlGraph.Font.Name, nFntSz)
            rectText = gP.MeasureString(sLine, fntA)
            nTextWidth = rectText.Width
        Loop
        nFntSz = nFntSz - 1
        fntA = New Font(pnlGraph.Font.Name, nFntSz)
        pnlGraph.Font = fntA
        If nFntSz > 1 Then        'Now that the font size is set, make sure that it is not too tall, if so reduce it back down.
            rectText = gP.MeasureString(sLine, fntA)
            Do Until rectText.Height <= pnlGraph.Height / 20
                nFntSz = nFntSz - 1
                fntA = New Font(pnlGraph.Font.Name, nFntSz)
                rectText = gP.MeasureString(sLine, fntA)
            Loop
        End If
        pnlGraph.Font = fntA

    End Sub

    Private Sub Graph20()
        'This sub draws the displacement vs. time graph

        Dim nNumberOfXMajorGridLines As Short 'This is to tell you how many X gridlines to draw.
        Dim nMaximumYValue As Double 'This is to determine the default Maximum Y value, if it does not exist.
        Dim nPixelsPerPercent_X As Double 'This is how many pixels one percent of the gait cycle is.
        Dim nPixelsPerUnit_Y As Double 'This is how many pixels one unit of Y is on the Y axis.
        Dim nUnitsForOneGridline_Y As Double 'This is how many feet one Y gridline represents
        Dim nX1, nY1, nX2, nY2 As Integer 'This is the beginning and ending points of any line segment.

        'Step 1:  Find the Units that we are going to graph Maximum and minimum of Velocity
        If arMaximumXValues(20) = 0 Then
            nMaximumYValue = 0
            For Me.i = 1 To 99
                If System.Math.Abs(arDisplacement(i)) > nMaximumYValue Then nMaximumYValue = System.Math.Abs(arDisplacement(i))
            Next i
            arMaximumYValues(20) = nMaximumYValue
        End If
        lblMaximumYValue.Text = arMaximumYValues(20)

        'Step 2: Need to now how many Major Gridlines to draw along the X axis.  Every gridline should be 10% of the gait cycle.
        If arMaximumXValues(20) = 0 Then
            If bWalkingOrRunning = True Then
                arMaximumXValues(20) = 100 'If you are running then the default maximum X value is 100%
            Else
                i = 1
                Do Until arGaitPhase(i) <> arGaitPhase(i - 1)
                    i = i + 1 'This finds out where the end of the first double support phase is.
                Loop
                arMaximumXValues(20) = 100 + i - 1
            End If
        End If
        lblMaximumXValue.Text = arMaximumXValues(20)
        nNumberOfXMajorGridLines = Int(arMaximumXValues(20) / 10)

        'Step 3: How many pixels make up the each percent of the gait cycle along the X axis.
        nPixelsPerPercent_X = (bitGraph20.Width - 4) / arMaximumXValues(20)
        nPixelsPerUnit_Y = (bitGraph20.Height) / arMaximumYValues(20)

        'Step 4:  Get the number of pixels for one Foot of displacement along the Y axis.
        nPixelsPerUnit_Y = bitGraph20.Height / (2 * arMaximumYValues(20))

        'Step 5: Draw the X Axis axis across the center of the bitmap
        nY1 = bitGraph20.Height / 2
        penPix.Width = 4
        penPix.Color = colorGrid
        penPix.DashStyle = Drawing2D.DashStyle.Solid
        graphix20.DrawLine(penPix, 0, nY1, bitGraph20.Width, nY1)

        'Step 6: Draw the Y axis along the left side of the bitmap
        graphix20.DrawLine(penPix, 0, 0, 0, bitGraph20.Height)

        'Step 7: Draw the Major gridlines of the X axis with a dashed line.
        penPix.Width = 2
        penPix.DashStyle = Drawing2D.DashStyle.Dash
        penPix.Color = colorGrid
        For Me.i = 1 To nNumberOfXMajorGridLines
            g = 4 + Int(i * 10 * nPixelsPerPercent_X - 1)
            graphix20.DrawLine(penPix, g, 0, g, bitGraph20.Height)
        Next i

        'Step 8:  Draw the Major Gridlines for the Y axis with a dashed line.
        For Me.i = -5 To 5
            If i <> 0 Then
                nY1 = bitGraph20.Height / 2 - (i * 0.1 * bitGraph20.Height)
                graphix20.DrawLine(penPix, 0, nY1, bitGraph20.Width, nY1)
            End If
        Next i

        'Step 9: Draw the little tickmarks for the X axis. 4 small ones a then the 5th one just a little longer.
        penPix.DashStyle = Drawing2D.DashStyle.Dot
        nY1 = 0.48 * bitGraph20.Height
        nY2 = 0.52 * bitGraph20.Height
        For Me.i = 1 To Val(lblMaximumXValue.Text)
            For Me.j = 1 To 4 'the four short vertical lines
                nX1 = Int(i * nPixelsPerPercent_X) + 4
                graphix20.DrawLine(penPix, nX1, nY1, nX1, nY2)
                i = i + 1
            Next j
            nY1 = 0.46 * bitGraph20.Height
            nY2 = 0.54 * bitGraph20.Height
            nX1 = Int(i * nPixelsPerPercent_X) + 4
            graphix20.DrawLine(penPix, nX1, nY1, nX1, nY2)
            nY1 = 0.48 * bitGraph20.Height
            nY2 = 0.52 * bitGraph20.Height
        Next i

        'Step 10: Find out how many units each major Y gridline represents, in multiples of 0.25"
        nUnitsForOneGridline_Y = bitGraph20.Height / 2 'divide the graph into 2 halves
        nUnitsForOneGridline_Y = nUnitsForOneGridline_Y / nPixelsPerUnit_Y 'Find out how many units in feet the half graph is.
        nUnitsForOneGridline_Y = nUnitsForOneGridline_Y * 12 'Find out how many inches in the top or bottom half of the graph.
        nUnitsForOneGridline_Y = nUnitsForOneGridline_Y / 5 'Find out how many inches between two major gridlines.
        'Set one y gridline to 0.1", 0.25" 0.5", 0.75", 1.0", etc.
        Dim nVal As Single = 0.25
        If nUnitsForOneGridline_Y <= 0.1 Then
            nUnitsForOneGridline_Y = 0.1 / 12 'How many feet is one Y gridline
            arMaximumYValues(20) = nUnitsForOneGridline_Y * 5
        Else
            i = 1
            Do Until nUnitsForOneGridline_Y <= i * nVal
                i = i + 1
            Loop
            nUnitsForOneGridline_Y = i * nVal / 12
            arMaximumYValues(20) = nUnitsForOneGridline_Y * 5
        End If

        'Step 11: Figure out how many pixels one foot of displacement is equal to.
        nPixelsPerUnit_Y = 0.5 * bitGraph20.Height / arMaximumYValues(20)

        'Step12: Draw the Displaement graph
        penPix.Width = 3
        penPix.DashStyle = Drawing2D.DashStyle.Solid
        nX1 = 4 'X at zero is at the right side of the Y axis
        nY1 = 0.5 * bitGraph20.Height - nPixelsPerUnit_Y * arDisplacement(0)
        For f = 1 To Val(lblMaximumXValue.Text)
            If f < 101 Then
                j = f
            Else : j = f - 100 * Int(f / 100)
            End If
            penPix.Color = graphFunctionSelectGraphingColor(j) 'Determine the color of the segment to be drawn.
            nX2 = 4 + f * nPixelsPerPercent_X
            nY2 = 0.5 * bitGraph20.Height - nPixelsPerUnit_Y * arDisplacement(j)
            graphix20.DrawLine(penPix, nX1, nY1, nX2, nY2)
            nX1 = nX2
            nY1 = nY2
        Next f

    End Sub
    Private Sub graph21()

    End Sub
    Private Function graphFunctionSelectGraphingColor(ByVal jj As Integer) As Color
        'This sub selects the color of the segment to be drawn
        Select Case arGaitPhase(jj)
            Case con_L_Double_Support, con_R_Double_Support
                If arGaitPhase(jj - 1) = con_L_Double_Support Or arGaitPhase(jj - 1) = con_R_Double_Support Then
                    graphFunctionSelectGraphingColor = colorBoth
                ElseIf arGaitPhase(jj - 1) = con_L_Single_Support Then
                    graphFunctionSelectGraphingColor = colorLeft
                ElseIf arGaitPhase(jj - 1) = con_R_Single_Support Then
                    graphFunctionSelectGraphingColor = colorRight
                End If
            Case con_L_Single_Support
                If arGaitPhase(jj - 1) = con_L_Float Then
                    graphFunctionSelectGraphingColor = colorBoth
                Else
                    graphFunctionSelectGraphingColor = colorLeft
                End If
            Case con_R_Single_Support
                If arGaitPhase(jj - 1) = con_R_Float Then
                    graphFunctionSelectGraphingColor = colorBoth
                Else
                    graphFunctionSelectGraphingColor = colorRight
                End If
            Case con_L_Float, con_R_Float
                graphFunctionSelectGraphingColor = colorBoth
        End Select

    End Function
    Sub subTransferPatientInfoFromMDI()
        sLeftFileName = mdi_sLeftFileName
        sRightFileName = mdi_sRightFileName
        sFirstName = mdi_sFirstName
        sLastName = mdi_sLastName
        sChartNumber = mdi_sChartNumber
        DateAndTime = mdi_DateAndTime
        bWearingOrthotics = mdi_bWearingOrthotics
        bWearingCustomShoes = mdi_bWearingCustomShoes
        bDiabetic = mdi_bDiabetic
        bNeuropathic = mdi_bNeuropathic
        bPVD = mdi_bPVD
        bHeelLift = mdi_bHeelLift
        sHeelLiftSide = mdi_sHeelLiftSide
        nHeelLiftHeight = mdi_nHeelLiftHeight
        sComments = mdi_sComments
        sOrthoticRx = mdi_sOrthoticRx
    End Sub

    Sub subReadAndCalculateData()
        'When the form first opens up you need to do all your calculations.
        'In this sub you will read the data files, then divide them into their individual frames and then do all the calculations.

        Dim nLocation(1) As Integer    'This variable is for identifying where you are in the record.  The first member is for identifying the beginning of the string and the second member is for identifying the end of the string.
        Dim tempNumbOfSensels As Integer 'This is used for identifying how many sensels need to be counted in any packet.
        Dim sEndHeader As String = Chr(&HBA) & Chr(&HBA) 'The characters where the header ends
        Dim nBlockLength As Integer = 0 'This variable is used to determine how long a block is.
        Dim nNumberOfFrames As Integer = 0 'This is the total number of frames in each file.
        Dim formProgress As New frmProgress 'Put the Progress Form up to show your user where you are in reading and calculating.
        Dim nZeroVelocityInStancePct(1) As Long
        ProgressBar1.Show()
        lblProgressBar.Hide()

        ProgressBar1.Maximum = 72
        ProgressBar1.Show()

1:      'STEP1:  '  Read the Left and right files as the Strings fileFSXLeft and file FSXRight 
        lblProgressBar.Text = "Step 1: Reading Left and Right Foot File From Disk"
        ProgressBar1.Value = 1
        Dim fileFSXLeft As New IO.FileStream(lblFullFileNameL.Text, IO.FileMode.Open, IO.FileAccess.Read)
        Dim fileFSXRight As New IO.FileStream(lblFullFileNameR.Text, IO.FileMode.Open, IO.FileAccess.Read)
        Dim dataLeft(fileFSXLeft.Length - 1) As Byte
        Dim dataRight(fileFSXRight.Length - 1) As Byte
        If dataLeft.Length <> 0 Then
            fileFSXLeft.Read(dataLeft, 0, dataLeft.Length - 1)
        Else
            Dim sResult As MsgBoxResult
            Dim msg As String
            msg = "The file " & lblFullFileNameL.Text & " has no data.  Please resave and run the program again."
            sResult = MsgBox(msg, MsgBoxStyle.OkOnly, "Error reading file")
        End If
        If dataRight.Length <> 0 Then 'Fill in the data as a string for the Right Foot.
            fileFSXLeft.Read(dataRight, 0, dataRight.Length - 1)
        Else
            Dim sResult As MsgBoxResult
            Dim msg As String
            msg = "The file " & lblFullFileNameR.Text & " has no data.  Please resave and run the program again."
            sResult = MsgBox(msg, MsgBoxStyle.OkOnly, "Error reading file")
        End If
        fileFSXRight.Read(dataRight, 0, dataRight.Length - 1)
        fileFSXLeft.Close() 'Close the Left File
        fileFSXRight.Close() 'Close the Right File
        Dim myDataLeft As String = System.Text.Encoding.Default.GetString(dataLeft)
        Dim myDataRight As String = System.Text.Encoding.Default.GetString(dataRight)
        fileFSXLeft = Nothing
        fileFSXRight = Nothing

2:      'STEP 2: Separate out the Header Information and put in a header string:
        lblProgressBar.Text = "Step 2: Separating Out the Headers"
        ProgressBar1.Value = 2
        Dim sHeaderStringLeft, sHeaderStringRight As String
        Dim nHeaderLeft As Integer = InStr(1, myDataLeft, sEndHeader, CompareMethod.Text)
        Dim nHeaderRight As Integer = InStr(myDataRight, sEndHeader)
        sHeaderStringLeft = Mid(myDataLeft, 1, nHeaderLeft + 1)
        sHeaderStringRight = Mid(myDataRight, 1, nHeaderRight + 1)

        'STEP 2A:  Put the pressure data into their own string.
        Dim sPressureBlockLeft, sPressureBlockRight As String
        sPressureBlockLeft = Mid(myDataLeft, nHeaderLeft + 2, Len(myDataLeft))
        sPressureBlockRight = Mid(myDataRight, nHeaderRight + 2, Len(myDataRight))

3:      'STEP 3:  Find out how many frames there are
        lblProgressBar.Text = "Step 3: Finding the Number of Frames"
        ProgressBar1.Value = 3
        nLocation(0) = InStr(1, myDataLeft, "FRAMES")
        nLocation(1) = InStr(nLocation(0), myDataLeft, Chr(10))
        nNumberOfFrames = Val(Mid(myDataLeft, nLocation(0) + 7, nLocation(1) - nLocation(0) - 7))

4:      'STEP 4:  Divide the long strings into Frame Strings
        lblProgressBar.Text = "Step 4: Dividing the Data Portion of the File Into Individual Frames"
        ProgressBar1.Value = 4
        Dim sIndividualFrameLeft(nNumberOfFrames, 1) '(frame#,0) is the data string; (frame#,1) is the date stamp string.  'NOTE:  Frame 0 is NEVER USED in the First Dimension!!!
        Dim sIndividualFrameRight(nNumberOfFrames, 1)
        Dim sFrameEnd1 As String = Chr(&HFF) & Chr(&HFF) & Chr(&H1) & Chr(&H0) 'FF FF 01 00 marks the end of each frame data before the time stamp
        Dim sFrameEnd2 As String = Chr(&HFF) & Chr(&HFF) & Chr(&H2) & Chr(&H0) 'this is an alternate time stamp form.

        'look to see whether time stamp 1 or time stamp 2 is used.
        Dim sFrameEnd As String
        If InStr(sPressureBlockLeft, sFrameEnd2) = 0 Then
            sFrameEnd = sFrameEnd1
        Else : sFrameEnd = sFrameEnd2
        End If

        lblProgressBar.Text = "Step 4A: Dividing the Left File Into Individual Frames"
        nLocation(conStart) = 1 'Find the first place to start reading sensel values
        nLocation(conEnd) = InStr(nLocation(conStart), sPressureBlockLeft, sFrameEnd, CompareMethod.Binary) 'Find the first place to end
        For Me.i = 1 To nNumberOfFrames 'This First Loop reads the Left Foot File
            'lblprogress.text = "Dividing the Left File Into Individual Frames - Frame #" & i
            sIndividualFrameLeft(i, 0) = Mid(sPressureBlockLeft, nLocation(conStart), nLocation(conEnd) - nLocation(conStart)) 'Read the Data string for the frame #i
            nLocation(conStart) = nLocation(conEnd) + 2 'Go the first byte after "FF FF"
            nLocation(conEnd) = InStr(nLocation(0), sPressureBlockLeft, Chr(&HFF) & Chr(&HFF), CompareMethod.Binary) 'Find the end of the Date Stamp location for frame #i
            sIndividualFrameLeft(i, 1) = Mid(sPressureBlockLeft, nLocation(0), nLocation(1) - nLocation(0)) 'Read the time stamp for frame #i - Left Foot
            nLocation(conStart) = nLocation(1) + 2 'Go the byte after "FF FF" to begin the next Frame Data string
            nLocation(conEnd) = InStr(nLocation(0), sPressureBlockLeft, sFrameEnd, CompareMethod.Binary) 'Find the end of the next Data string for the next frame #i+1
        Next i

        lblProgressBar.Text = "Step 4B: Dividing the Right File Into Individual Frames"
        nLocation(conStart) = 1 'Find the first place to start -Right Foot File
        nLocation(conEnd) = InStr(nLocation(conStart), sPressureBlockRight, sFrameEnd, CompareMethod.Binary) 'Find the first place to end -Right Foot file
        For Me.i = 1 To nNumberOfFrames 'This First Loop reads the Left Foot File
            'lblprogress.text = "Dividing the Right File Into Individual Frames - Frame " & i
            sIndividualFrameRight(i, 0) = Mid(sPressureBlockRight, nLocation(conStart), nLocation(conEnd) - nLocation(conStart)) 'Read the Data string for the frame #i
            nLocation(conStart) = nLocation(1) + 2 'Go the byte after FF FF
            nLocation(1) = InStr(nLocation(conStart), sPressureBlockRight, Chr(&HFF) & Chr(&HFF), CompareMethod.Binary) 'Find the end of the Date Stamp location for frame #i
            sIndividualFrameRight(i, 1) = Mid(sPressureBlockRight, nLocation(conStart), nLocation(conEnd) - nLocation(conStart)) 'Read the time stamp for frame #i - Right Foot
            nLocation(0) = nLocation(1) + 2 'Go the byte after FF FF
            nLocation(1) = InStr(nLocation(0), sPressureBlockRight, sFrameEnd, CompareMethod.Binary) 'Find the end of the next data string location for frame #i+1 Right foot
        Next i

5:      'STEP 5:  Count the Number of Packets and the Number of Sensels activated for each frame
        lblProgressBar.Text = "Step 5: Counting the Number of Sensels"
        ProgressBar1.Value = 5
        Dim nPacketCountLeft(nNumberOfFrames) As Integer 'This is the number of packets to be counted for each frame, Left Foot
        Dim nPacketCountRight(nNumberOfFrames) As Integer '"       "      "       "      "           "      "           "      "       "  , Right Foot
        Dim nSenselCountLeft(nNumberOfFrames) As Short 'This is the total sensel count in for each frame, Left Foot
        Dim nSenselCountRight(nNumberOfFrames) As Short ' "   "      "   "      "       "      "   "      "       "  , Right Foot
        For Me.i = 1 To nNumberOfFrames 'The i is for the frame number
            If Me.i = 207 Then
                i = 207
            End If
            For Me.j = 1 To Len(sIndividualFrameLeft(i, 0))
                nPacketCountLeft(i) = nPacketCountLeft(i) + 1 'Increase the packet count
                j = j + 2 'The first two bytes identify the location of the first sensel, so Move two bytes to the right to get the number of sensels.
                nSenselCountLeft(i) = nSenselCountLeft(i) + Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), j, 1)))) 'Increase the sensel count
                j = j + Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), j, 1))))  'Move to the right the number of sensels
            Next j
            For Me.j = 1 To Len(sIndividualFrameRight(i, 0))
                nPacketCountRight(i) = nPacketCountRight(i) + 1 'Increase the packet count for the right foot
                j = j + 2 'Move two bytes to the right
                nSenselCountRight(i) = nSenselCountRight(i) + Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), j, 1)))) 'Increase the sensel count
                j = j + Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), j, 1)))) 'Move to the right the number of sensels
            Next j
        Next i

6:      'STEP 6: Populate the Sensel Matrix for The Left and Right Foot
        lblProgressBar.Text = "Step 6: Populating the Sensel Matrices"
        ProgressBar1.Value = 6
        Dim nIndividualFrameValueLeft(nNumberOfFrames, 1259) 'The value for each sensel in each frame,  Left Foot
        Dim nIndividualFrameValueRight(nNumberOfFrames, 1259) ' "   "       "      "       "      "   "       "      , Right Foot
        Dim nSenselMatrixLeft(nNumberOfFrames, 59, 20) 'This matrix is the same as the nIndividualFrameValueLeft, except that it is by the individual rows and columns, not just a sensel Number
        Dim nSenselMatrixRight(nNumberOfFrames, 59, 20) ' "       "      "   "   "      "   "   nIndividualFrameValueRight,    "       "  "   "  "   "      "           "          "       "
        Dim nSenselNumber, nRow, nColumn As Integer
        Dim nRawTotalForceLeft(nNumberOfFrames), nRawTotalForceRight(nNumberOfFrames) As Integer 'The total force in terms of unconverted numbers, for each frame.
        Dim nRawColumnForceLeft(nNumberOfFrames, 20), nRawColumnForceRight(nNumberOfFrames, 20) As Integer 'The total force in unconverted numbers for each column for each frame (frame#, column#)
        Dim nRawRowForceLeft(nNumberOfFrames, 59), nRawRowForceRight(nNumberOfFrames, 59) As Integer 'The total force in unconverted numbers for each row for each frame. (frame #, row #)
        For Me.i = 1 To nNumberOfFrames '- i keeps track of the frame numbers
            nLocation(0) = 0 'nLocation(0) will be used for keeping track of where you are in the Left Foot String
            nLocation(1) = 0 'nLocation(1) will be used for keeping track of where you are in the Right Foot Sring
            If nPacketCountLeft(i) <> 0 Then 'Start on th Left Foot
                For Me.j = 1 To nPacketCountLeft(i)
                    nLocation(0) = nLocation(0) + 1 'Move to the first byte of the Sensel# that starts the packet
                    nSenselNumber = Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), nLocation(0), 1)))) + 256 * Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), nLocation(0) + 1, 1))))
                    nLocation(0) = nLocation(0) + 2 'Move two bytes to the right
                    tempNumbOfSensels = Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), nLocation(0), 1)))) 'This is the number of continuous sensels to read in any packet
                    For Me.k = 1 To tempNumbOfSensels
                        nLocation(0) = nLocation(0) + 1
                        nRow = Math.DivRem(nSenselNumber + k - 1, 21, nColumn) 'Get the row and column number
                        nIndividualFrameValueLeft(i, nSenselNumber + k - 1) = Val(CStr(Asc(Mid(sIndividualFrameLeft(i, 0), nLocation(0), 1)))) 'Note SenselMatrix is from 0 to 1259
                        nSenselMatrixLeft(i, nRow, nColumn) = nIndividualFrameValueLeft(i, nSenselNumber + k - 1) 'Put the value in the proper row and column
                        nRawColumnForceLeft(i, nColumn) = nRawColumnForceLeft(i, nColumn) + nSenselMatrixLeft(i, nRow, nColumn)
                        nRawRowForceLeft(i, nRow) = nRawRowForceLeft(i, nRow) + nSenselMatrixLeft(i, nRow, nColumn)
                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) + nSenselMatrixLeft(i, nRow, nColumn)
                    Next k
                Next j
            End If
            If nPacketCountRight(i) <> 0 Then 'Then Do the Right Foot"
                For Me.j = 1 To nPacketCountRight(i)
                    nLocation(1) = nLocation(1) + 1
                    nSenselNumber = Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), nLocation(1), 1)))) + 256 * Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), nLocation(1) + 1, 1)))) 'This is the sensel # to start on.
                    nLocation(1) = nLocation(1) + 2
                    tempNumbOfSensels = Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), nLocation(1), 1)))) 'This is the number of continuous sensels to read in any packet
                    For Me.k = 1 To tempNumbOfSensels
                        nLocation(1) = nLocation(1) + 1
                        nRow = Math.DivRem(nSenselNumber + k - 1, 21, nColumn) 'Get the row and column number
                        nIndividualFrameValueRight(i, nSenselNumber + k - 1) = Val(CStr(Asc(Mid(sIndividualFrameRight(i, 0), nLocation(1), 1)))) 'Note SenselMatrix is from 0 to 1259
                        nSenselMatrixRight(i, nRow, nColumn) = nIndividualFrameValueRight(i, nSenselNumber + k - 1)
                        nRawColumnForceRight(i, nColumn) = nRawColumnForceRight(i, nColumn) + nSenselMatrixRight(i, nRow, nColumn)
                        nRawRowForceRight(i, nRow) = nRawRowForceRight(i, nRow) + nSenselMatrixRight(i, nRow, nColumn)
                        nRawTotalForceRight(i) = nRawTotalForceRight(i) + nSenselMatrixRight(i, nRow, nColumn)
                    Next k
                Next j
            End If
        Next i

7:      'STEP 7:  Get rid of any lone sensels.
        lblProgressBar.Text = "Step 7: Clearing the Lone Sensels"
        ProgressBar1.Value = 7
        Dim bZeroOutThisFrameLeft, bZeroOutThisFrameRight As Boolean
        For Me.i = 1 To nNumberOfFrames
            For Me.j = 0 To 59
                For Me.k = 0 To 20
                    If nSenselMatrixLeft(i, j, k) > 0 Then
                        Select Case j
                            Case 0
                                Select Case k
                                    Case 0
                                        If nSenselMatrixLeft(i, j, k + 1) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case 20
                                        If nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case Else
                                        If nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j, k + 1) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                End Select
                            Case 59
                                Select Case k
                                    Case 0
                                        If nSenselMatrixLeft(i, j, k + 1) = 0 And nSenselMatrixLeft(i, j - 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case 20
                                        If nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j - 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case Else
                                        If nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j, k + 1) = 0 And nSenselMatrixLeft(i, j - 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                End Select
                            Case Else
                                Select Case k
                                    Case 0
                                        If nSenselMatrixLeft(i, j, k + 1) = 0 And nSenselMatrixLeft(i, j - 1, k) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case 20
                                        If nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j - 1, k) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 Then bZeroOutThisFrameLeft = True
                                    Case Else
                                        If nSenselMatrixLeft(i, j - 1, k) = 0 And nSenselMatrixLeft(i, j + 1, k) = 0 And nSenselMatrixLeft(i, j, k - 1) = 0 And nSenselMatrixLeft(i, j, k + 1) = 0 Then bZeroOutThisFrameLeft = True
                                End Select
                        End Select
                    End If
                    If nSenselMatrixRight(i, j, k) > 0 Then
                        Select Case j
                            Case 0
                                Select Case k
                                    Case 0
                                        If nSenselMatrixRight(i, j, k + 1) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case 20
                                        If nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case Else
                                        If nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j, k + 1) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 Then bZeroOutThisFrameRight = True
                                End Select
                            Case 59
                                Select Case k
                                    Case 0
                                        If nSenselMatrixRight(i, j, k + 1) = 0 And nSenselMatrixRight(i, j - 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case 20
                                        If nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j - 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case Else
                                        If nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j, k + 1) = 0 And nSenselMatrixRight(i, j - 1, k) = 0 Then bZeroOutThisFrameRight = True
                                End Select
                            Case Else
                                Select Case k
                                    Case 0
                                        If nSenselMatrixRight(i, j, k + 1) = 0 And nSenselMatrixRight(i, j - 1, k) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case 20
                                        If nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j - 1, k) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 Then bZeroOutThisFrameRight = True
                                    Case Else
                                        If nSenselMatrixRight(i, j - 1, k) = 0 And nSenselMatrixRight(i, j + 1, k) = 0 And nSenselMatrixRight(i, j, k - 1) = 0 And nSenselMatrixRight(i, j, k + 1) = 0 Then bZeroOutThisFrameRight = True
                                End Select
                        End Select
                    End If
                    If bZeroOutThisFrameLeft = True Then
                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                        nSenselMatrixLeft(i, j, k) = 0
                        bZeroOutThisFrameLeft = False
                    End If
                    If bZeroOutThisFrameRight = True Then
                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                        nSenselMatrixRight(i, j, k) = 0
                        bZeroOutThisFrameRight = False
                    End If
                Next k
            Next j
        Next i

8:      'STEP 8: Get rid of any remaining sensels that occupy only a single row by themselves or a single column, with zero on either side.
        lblProgressBar.Text = "Step 8: Removing stray row and stray column Sensels."
        ProgressBar1.Value = 8
        For Me.i = 1 To nNumberOfFrames
            If nSenselCountLeft(i) > 0 Then 'Start on th Left Foot
                For Me.k = 0 To 20 'Do the columns first for the Left Foot
                    Select Case k
                        Case 0
                            If nRawColumnForceLeft(i, 0) > 0 And nRawColumnForceLeft(i, 1) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixLeft(i, j, k) > 0 Then 'i is the frame #, j the row # and k the column #
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawColumnForceLeft(i, k) = 0 Then Exit For
                                Next j
                            End If
                        Case 20
                            If nRawColumnForceLeft(i, 20) <> 0 And nRawColumnForceLeft(i, 19) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixLeft(i, j, k) <> 0 Then
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawColumnForceLeft(i, k) = 0 Then Exit For
                                Next j
                            End If
                        Case Else
                            If nRawColumnForceLeft(i, k) <> 0 And nRawColumnForceLeft(i, k - 1) = 0 And nRawColumnForceLeft(i, k + 1) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixLeft(i, j, k) <> 0 Then
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawColumnForceLeft(i, k) = 0 Then Exit For
                                Next j
                            End If
                    End Select
                Next k
                For Me.j = 0 To 59 'Check the rows
                    Select Case j
                        Case 0
                            If nRawRowForceLeft(i, 0) <> 0 And nRawRowForceLeft(i, 1) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixLeft(i, j, k) <> 0 Then
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawRowForceLeft(i, 0) = 0 Then Exit For
                                Next k
                            End If
                        Case 59
                            If nRawRowForceLeft(i, 59) <> 0 And nRawRowForceLeft(i, 58) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixLeft(i, j, k) <> 0 Then
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawRowForceLeft(i, 59) = 0 Then Exit For
                                Next k
                            End If
                        Case Else
                            If nRawRowForceLeft(i, j) <> 0 And nRawRowForceLeft(i, j - 1) = 0 And nRawRowForceLeft(i, j + 1) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixLeft(i, j, k) <> 0 Then
                                        nRawTotalForceLeft(i) = nRawTotalForceLeft(i) - nSenselMatrixLeft(i, j, k)
                                        nRawColumnForceLeft(i, k) = nRawColumnForceLeft(i, k) - nSenselMatrixLeft(i, j, k)
                                        nRawRowForceLeft(i, j) = nRawRowForceLeft(i, j) - nSenselMatrixLeft(i, j, k)
                                        nSenselCountLeft(i) = nSenselCountLeft(i) - 1
                                        nSenselMatrixLeft(i, j, k) = 0
                                    End If
                                    If nRawRowForceLeft(i, j) = 0 Then Exit For
                                Next k
                            End If
                    End Select
                Next j
            End If
            If nSenselCountRight(i) <> 0 Then 'Do the Right foot
                For Me.k = 0 To 20 'Do the columns first for the Right Foot
                    Select Case k
                        Case 0 'First Column
                            If nRawColumnForceRight(i, 0) <> 0 And nRawColumnForceRight(i, 1) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next j
                            End If
                        Case 20 'Last column
                            If nRawColumnForceRight(i, 20) <> 0 And nRawColumnForceRight(i, 19) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next j
                            End If
                        Case Else
                            If nRawColumnForceRight(i, k) <> 0 And nRawColumnForceRight(i, k - 1) = 0 And nRawColumnForceRight(i, k + 1) = 0 Then
                                For Me.j = 0 To 59
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next j
                            End If
                    End Select
                Next k
                For Me.j = 0 To 59 'Check the rows
                    Select Case j
                        Case 0
                            If nRawRowForceRight(i, 0) <> 0 And nRawRowForceRight(i, 1) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next k
                            End If
                        Case 59
                            If nRawRowForceRight(i, 59) <> 0 And nRawRowForceRight(i, 58) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next k
                            End If
                        Case Else
                            If nRawRowForceRight(i, j) <> 0 And nRawRowForceRight(i, j - 1) = 0 And nRawRowForceRight(i, j + 1) = 0 Then
                                For Me.k = 0 To 20
                                    If nSenselMatrixRight(i, j, k) <> 0 Then
                                        nRawTotalForceRight(i) = nRawTotalForceRight(i) - nSenselMatrixRight(i, j, k)
                                        nRawColumnForceRight(i, k) = nRawColumnForceRight(i, k) - nSenselMatrixRight(i, j, k)
                                        nRawRowForceRight(i, j) = nRawRowForceRight(i, j) - nSenselMatrixRight(i, j, k)
                                        nSenselCountRight(i) = nSenselCountRight(i) - 1
                                        nSenselMatrixRight(i, j, k) = 0
                                    End If
                                Next k
                            End If
                    End Select
                Next j
            End If
        Next i

9:      'STEP9:  Get the cutoff values for the amount of force that represents noise during swing
        lblProgressBar.Text = "Step 9: Get the minimum cut off force that defines swing"
        ProgressBar1.Value = 9
        Dim nCutOffNoiseLeft, nCutOffNoiseRight As Single
        Dim nMinimumForceLeft, nMinimumForceRight, nMaximumForceLeft, nMaximumForceRight As Single
        nMinimumForceLeft = nRawTotalForceLeft(1)
        nMaximumForceLeft = nRawTotalForceLeft(1)
        nMinimumForceRight = nRawTotalForceRight(1)
        nMaximumForceRight = nRawTotalForceRight(1)
        For Me.i = 2 To nNumberOfFrames
            If nMinimumForceLeft > nRawTotalForceLeft(i) Then nMinimumForceLeft = nRawTotalForceLeft(i)
            If nMaximumForceLeft < nRawTotalForceLeft(i) Then nMaximumForceLeft = nRawTotalForceLeft(i)
            If nMinimumForceRight > nRawTotalForceRight(i) Then nMinimumForceRight = nRawTotalForceRight(i)
            If nMaximumForceRight < nRawTotalForceRight(i) Then nMaximumForceRight = nRawTotalForceRight(i)
        Next i
        nCutOffNoiseLeft = 0.05 * (nMaximumForceLeft - nMinimumForceLeft) + nMinimumForceLeft
        nCutOffNoiseRight = 0.05 * (nMaximumForceRight - nMinimumForceRight) + nMinimumForceRight

10:     'STEP 10: Zero out all the sensels in those frames in which total force is less than the CutOffNoise Threshold.
        lblProgressBar.Text = "Step 10: Clearing noise during swing phase"
        ProgressBar1.Value = 10
        For Me.i = 1 To nNumberOfFrames
            If nRawTotalForceLeft(i) < nCutOffNoiseLeft Then
                For Me.k = 0 To 20
                    For Me.j = 0 To 59
                        nSenselMatrixLeft(i, j, k) = 0 'zero out all the sensels for that frame
                        If nRawRowForceLeft(i, j) <> 0 Then nRawRowForceLeft(i, j) = 0 'zero out all the row forces for that frame
                    Next j
                    If nRawColumnForceLeft(i, k) <> 0 Then nRawColumnForceLeft(i, k) = 0 'zero out all the column forces for that frame
                Next k
                nSenselCountLeft(i) = 0
                nRawTotalForceLeft(i) = 0 'zero out the total force for that frame
            End If
            If nRawTotalForceRight(i) < nCutOffNoiseRight Then
                For Me.k = 0 To 20
                    For Me.j = 0 To 59
                        nSenselMatrixRight(i, j, k) = 0 'zero out all the sensels for that frame
                        If nRawRowForceRight(i, j) <> 0 Then nRawRowForceRight(i, j) = 0 'zero out all the row forces for that frame
                    Next j
                    If nRawColumnForceRight(i, k) <> 0 Then nRawColumnForceRight(i, k) = 0 'zero out all the column forces for that frame
                Next k
                nSenselCountRight(i) = 0
                nRawTotalForceRight(i) = 0 'zero out the total force for that frame
            End If
        Next i

11:     'STEP 11: Determine the CoP in terms of the sensels.
        lblProgressBar.Text = "Step 11: Calculating the Center of Pressure"
        ProgressBar1.Value = 11
        Dim nRawCoPMLLeft(nNumberOfFrames), nRawCoPMLRight(nNumberOfFrames) As Double
        Dim nRawCoPAPLeft(nNumberOfFrames), nRawCoPAPRight(nNumberOfFrames) As Double
        For Me.i = 1 To nNumberOfFrames
            For Me.j = 0 To 59 'This is for the rows
                nRawCoPAPLeft(i) = nRawCoPAPLeft(i) + j * nRawRowForceLeft(i, j)
                nRawCoPAPRight(i) = nRawCoPAPRight(i) + j * nRawRowForceRight(i, j)
            Next j
            If nRawTotalForceLeft(i) <> 0 Then nRawCoPAPLeft(i) = nRawCoPAPLeft(i) / nRawTotalForceLeft(i)
            If nRawTotalForceRight(i) <> 0 Then nRawCoPAPRight(i) = nRawCoPAPRight(i) / nRawTotalForceRight(i)
            For Me.k = 0 To 20 'now do the columns
                nRawCoPMLLeft(i) = nRawCoPMLLeft(i) + k * nRawColumnForceLeft(i, k)
                nRawCoPMLRight(i) = nRawCoPMLRight(i) + k * nRawColumnForceRight(i, k)
            Next k
            If nRawTotalForceLeft(i) <> 0 Then nRawCoPMLLeft(i) = nRawCoPMLLeft(i) / nRawTotalForceLeft(i)
            If nRawTotalForceRight(i) <> 0 Then nRawCoPMLRight(i) = nRawCoPMLRight(i) / nRawTotalForceRight(i)
        Next i

12:     'STEP 12: Find the length and width of each foot.
        lblProgressBar.Text = "Step 12: Calculating Foot Length and Width"
        ProgressBar1.Value = 12
        Dim nFootLengthLeft(1), nFootLengthRight(1) As Single 'This is the length of the foot with the 0 being the front end of the foot and 1 as the back end of the foot
        Dim nFootWidthLeft(1), nFootWidthRight(1) As Single 'This is the width of the foot, with 0 being the left side and 1 being the right.  Given in terms of sensels.
        nFootLengthLeft(0) = 59
        nFootLengthRight(0) = 59
        nFootWidthLeft(0) = 20
        nFootWidthRight(0) = 20
        nFootLengthLeft(1) = 0
        nFootLengthRight(1) = 0
        nFootLengthRight(1) = 0
        nFootWidthRight(1) = 0
        For Me.i = 1 To nNumberOfFrames 'This for statement gets the minimum number of row and column for each foot.
            j = 0
            Do Until nRawRowForceLeft(i, j) <> 0 Or j = 59
                j = j + 1
            Loop
            If nFootLengthLeft(0) > j Then nFootLengthLeft(0) = j 'Set the front of the left foot length
            j = 0
            Do Until nRawRowForceRight(i, j) <> 0 Or j = 59
                j = j + 1
            Loop
            If nFootLengthRight(0) > j Then nFootLengthRight(0) = j 'Set the front of the right foot length
            k = 0
            Do Until nRawColumnForceLeft(i, k) <> 0 Or k = 20
                k = k + 1
            Loop
            If nFootWidthLeft(0) > k Then nFootWidthLeft(0) = k 'Set the left side of the left foot width
            k = 0
            Do Until nRawColumnForceRight(i, k) <> 0 Or k = 20
                k = k + 1
            Loop
            If nFootWidthRight(0) > k Then nFootWidthRight(0) = k 'Set the right side of the right foot width
        Next i

        For Me.i = 1 To nNumberOfFrames 'Now get the maximum number for the length and width.
            j = 59
            Do Until nRawRowForceLeft(i, j) <> 0 Or j = 0
                j = j - 1
            Loop
            If nFootLengthLeft(1) < j Then nFootLengthLeft(1) = j
            j = 59
            Do Until nRawRowForceRight(i, j) <> 0 Or j = 0
                j = j - 1
            Loop
            If nFootLengthRight(1) < j Then nFootLengthRight(1) = j
            k = 20
            Do Until nRawColumnForceLeft(i, k) <> 0 Or k = 0
                k = k - 1
            Loop
            If nFootWidthLeft(1) < k Then nFootWidthLeft(1) = k
            k = 20
            Do Until nRawColumnForceRight(i, k) <> 0 Or k = 0
                k = k - 1
            Loop
            If nFootWidthRight(1) < k Then nFootWidthRight(1) = k
        Next i

13:     'STEP 13: Find the factor to multiply all the sensel values by to get the actual force in pounds
        lblProgressBar.Text = "Step 13: Determining the force multiplier"
        ProgressBar1.Value = 13
        'Find the "CAL_FPI_1" value in the header.
        Dim nCalibratedForceLeft, nCalibratedForceRight As Double
        Dim nSumRawNumberLeft, nSumRawNumberRight As Double
        Dim nForceFactorLeft, nForceFactorRight As Double
        Dim nFinalForceMultiplierLeft, nFinalForceMultiplierRight As Double
        nLocation(0) = InStr(1, sHeaderStringLeft, "CAL_FPI_1") 'Find the CAL_FPI_1 in the Left Header
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringLeft, Chr(10))
            nCalibratedForceLeft = Val(Mid(sHeaderStringLeft, nLocation(0) + 10, nLocation(1) - nLocation(0) - 10))
        Else
            nCalibratedForceLeft = 1
        End If
        nLocation(0) = InStr(1, sHeaderStringRight, "CAL_FPI_1") 'Find the CAL_FPI_1 in the Left Header
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringRight, Chr(10))
            nCalibratedForceRight = Val(Mid(sHeaderStringRight, nLocation(0) + 10, nLocation(1) - nLocation(0) - 10))
        Else
            nCalibratedForceRight = 1
        End If

        'Find the CAL_RSI_1 value in the header.
        nLocation(0) = InStr(1, sHeaderStringLeft, "CAL_RSI_1")
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringLeft, Chr(10))
            nSumRawNumberLeft = Val(Mid(sHeaderStringLeft, nLocation(0) + 10, nLocation(1) - nLocation(0) - 10))
        Else
            nSumRawNumberLeft = 1
        End If
        nLocation(0) = InStr(1, sHeaderStringRight, "CAL_RSI_1") 'Find the CAL_RSI_1 value for the right foot
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringRight, Chr(10))
            nSumRawNumberRight = Val(Mid(sHeaderStringRight, nLocation(0) + 10, nLocation(1) - nLocation(0) - 10))
        Else
            nSumRawNumberRight = 1
        End If

        'Find the "EXT_CAL_FACTOR" in the header
        nLocation(0) = InStr(1, sHeaderStringLeft, "EXT_CAL_FACTOR")
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringLeft, Chr(10))
            nForceFactorLeft = Val(Mid(sHeaderStringLeft, nLocation(0) + 15, nLocation(1) - nLocation(0) - 15))
        Else
            nForceFactorLeft = 1
        End If
        nLocation(0) = InStr(1, sHeaderStringRight, "EXT_CAL_FACTOR")
        If nLocation(0) <> 0 Then
            nLocation(1) = InStr(nLocation(0), sHeaderStringRight, Chr(10))
            nForceFactorRight = Val(Mid(sHeaderStringRight, nLocation(0) + 15, nLocation(1) - nLocation(0) - 15))
        Else
            nForceFactorRight = 1
        End If
        'Now calculate the final multiplier
        nFinalForceMultiplierLeft = (nCalibratedForceLeft / nSumRawNumberLeft) * nForceFactorLeft
        nFinalForceMultiplierRight = (nCalibratedForceRight / nSumRawNumberRight) * nForceFactorRight

14:     'STEP 14: Determine the number of strides 
        lblProgressBar.Text = "Step 14: Count the Number of Strides"
        ProgressBar1.Value = 14
        Dim bBeginLeftOrRight As Boolean 'This False for Starting on the Left Foot, True for Starting on the right foot.
        nNumberOfStrides = 0
        i = 1
        Do Until (nRawTotalForceLeft(i) = 0 And nRawTotalForceLeft(i + 1) > 0) Or (nRawTotalForceRight(i) = 0 And nRawTotalForceRight(i + 1) > 0) Or i = nNumberOfFrames - 1
            i = i + 1
        Loop 'This loop finds the frame just before the first heel strike
        If nRawTotalForceLeft(i) = 0 And nRawTotalForceLeft(i + 1) > 0 Then 'The Left foot hits first
            bBeginLeftOrRight = False
        ElseIf nRawTotalForceRight(i) = 0 And nRawTotalForceRight(i + 1) > 0 Then 'The Right foot hits first
            bBeginLeftOrRight = True
        End If

        For Me.j = i + 1 To nNumberOfFrames - 1 'This For block will count the number of strides in the sample
            Select Case bBeginLeftOrRight
                Case False 'Starting on the left foot
                    If nRawTotalForceLeft(j) = 0 And nRawTotalForceLeft(j + 1) > 0 Then
                        nNumberOfStrides = nNumberOfStrides + 1 'When you find the beginning of a new heel strike  you increase the stride number
                    End If
                Case True 'Starting on the right foot
                    If nRawTotalForceRight(j) = 0 And nRawTotalForceRight(j + 1) > 0 Then
                        nNumberOfStrides = nNumberOfStrides + 1
                    End If
            End Select
        Next j
        lblNumberOfCompleteStrides.Text = "Number of Strides: " & nNumberOfStrides 'Put the number of strides in the hidden label box for this value.

15:     'STEP 15:  Find the Frame# that divides the strides.  This Frame number will be a fraction and will be the 0% and 100% points of each stride.
        lblProgressBar.Text = "Step 15: Finding the Frame Numbers that divide the strides"
        ProgressBar1.Value = 15
        Dim nBeginAndEndOfStrides(nNumberOfStrides) 'Remember that stride #0 is not used.
        j = 0 'This counts the number of strides.  0 begins the first stride, 1 ends the first stride and begins the 2nd, etc.
        i = 1 'i is the frame counter
        Do Until j = nNumberOfStrides + 1
            Select Case bBeginLeftOrRight
                Case False 'If the sample starts with a left foot strike
                    If nRawTotalForceLeft(i) = 0 And nRawTotalForceLeft(i + 1) > 0 Then
                        If (nRawTotalForceLeft(i + 1) / (nRawTotalForceLeft(i + 2) - nRawTotalForceLeft(i + 1))) < 1 Then
                            If 0 < (nRawTotalForceLeft(i + 1) / (nRawTotalForceLeft(i + 2) - nRawTotalForceLeft(i + 1))) Then
                                nBeginAndEndOfStrides(j) = i + (1 - nRawTotalForceLeft(i + 1) / (nRawTotalForceLeft(i + 2) - nRawTotalForceLeft(i + 1)))
                            Else
                                nBeginAndEndOfStrides(j) = i
                            End If
                        Else
                            nBeginAndEndOfStrides(j) = i
                        End If
                        j = j + 1
                    End If
                    i = i + 1
                Case True 'If the sample starts with a right foot strike
                    If nRawTotalForceRight(i) = 0 And nRawTotalForceRight(i + 1) > 0 Then
                        If (nRawTotalForceRight(i + 1) / (nRawTotalForceRight(i + 2) - nRawTotalForceRight(i + 1))) < 1 And i < nNumberOfFrames + 2 Then
                            nBeginAndEndOfStrides(j) = i + (1 - nRawTotalForceRight(i + 1) / (nRawTotalForceRight(i + 2) - nRawTotalForceRight(i + 1)))
                        Else
                            nBeginAndEndOfStrides(j) = i
                        End If
                        j = j + 1
                    End If
                    i = i + 1 'Go to the next frame
            End Select
        Loop

16:     'STEP 16 Put the time for each frame into the array called nFrameTime by reading the time stamps.
        lblProgressBar.Text = "Step 16: Reading the time stamps for each frame."
        ProgressBar1.Value = 16
        Dim nTimePerFrame As Single
        Dim nFrameTime(nNumberOfFrames)
        Me.i = Len(sIndividualFrameLeft(2, 1)) 'Start at the end of the frame number and work backward until you find a value <>0
        Do Until Val(CStr(Asc(Mid(sIndividualFrameLeft(2, 1), i, 1)))) <> 0
            Me.i = Me.i - 1
        Loop
        Me.j = Me.i
        Do Until Val(CStr(Asc(Mid(sIndividualFrameLeft(2, 1), j - 1, 1)))) = 0 'make sure that the time stamp is an single byte, if not, then back up to find the least significant byte.
            j = j - 1
        Loop
        If Me.j = Me.i Then
            nTimePerFrame = 0.001 * Val(CStr(Asc(Mid(sIndividualFrameLeft(2, 1), i, 1))))
        Else
            k = 0
            Do Until j = i + 1
                nTimePerFrame = nTimePerFrame + 256 ^ k * Val(CStr(Asc(Mid(sIndividualFrameLeft(2, 1), j, 1))))
                j = j + 1
                k = k + 1
            Loop
            nTimePerFrame = 0.001 * nTimePerFrame
        End If
        For Me.i = 1 To nNumberOfFrames

        Next i

        'STEP 16A:  Calculate the time for each frame, and the frame number for each percent of the gait cycle, and the time for each percent of the gait cycle, for each frame.
        lblProgressBar.Text = "Step 16A: Dividing the Strides into their Individual Percentages"
        Dim nInterpolatedFrame(nNumberOfStrides, 100) As Single 'Remember that we will not use the 0 dimension of the stride number
        Dim nInterpolatedTime(nNumberOfStrides, 100) As Single 'This will hold the time from the beginning of the heel strike for that stride.
        For Me.i = 1 To nNumberOfStrides
            For Me.j = 0 To 100
                nInterpolatedFrame(i, j) = nBeginAndEndOfStrides(i - 1) + 0.01 * j * (nBeginAndEndOfStrides(i) - nBeginAndEndOfStrides(i - 1))
                nInterpolatedTime(i, j) = nTimePerFrame * 0.01 * j * (nBeginAndEndOfStrides(i) - nBeginAndEndOfStrides(i - 1))
            Next j
        Next i

17:     'STEP 17: Assign a percent of the gait cycle to each raw frame number
        lblProgressBar.Text = "Step 17: Percent of the gait cycle for each raw frame number"
        ProgressBar1.Value = 17
        Dim nPercentForEachRawFrameNumber(nNumberOfFrames) As Double
        Dim tempSlope As Double
        For Me.i = 1 To nNumberOfStrides
            tempSlope = nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)
            For Me.j = Int(nInterpolatedFrame(i, 0)) To Int(nInterpolatedFrame(i, 99))
                nPercentForEachRawFrameNumber(j) = ((j - nInterpolatedFrame(i, 0)) / tempSlope + (i - 1)) * 100
            Next j
        Next i

18:     'STEP 18: Calculate the interpolated frame at which the Stance Period Begins and Ends
        lblProgressBar.Text = "Step 18: Calculating the Interpolated Frame at Which Stance Periods Begin"
        ProgressBar1.Value = 18
        Dim nInterpolatedFrameBeginLeft(nNumberOfStrides), nInterpolatedFrameBeginRight(nNumberOfStrides) As Double
        Dim nInterpolatedFrameEndLeft(nNumberOfStrides), nInterpolatedFrameEndRight(nNumberOfStrides) As Double
        p = 1
        q = 1
        r = 1
        s = 1
        For Me.i = nInterpolatedFrame(1, 0) To nNumberOfFrames - 1
            If nRawTotalForceLeft(i) = 0 And nRawTotalForceLeft(i + 1) > 0 Then
                If p <= nNumberOfStrides Then
                    If i < nNumberOfFrames - 1 Then
                        If bBeginLeftOrRight = True Then 'Only need to do this if your beginning foot is the right foot
                            tempSlope = nRawTotalForceLeft(i + 2) - nRawTotalForceLeft(i + 1)
                            nInterpolatedFrameBeginLeft(p) = i + 1 - (nRawTotalForceLeft(i + 1) / tempSlope)
                            If nInterpolatedFrameBeginLeft(p) < i Or nInterpolatedFrameBeginLeft(p) > i + 1 Then
                                nInterpolatedFrameBeginLeft(p) = i 'If you calculate that the beginning is less than the last frame that is 0, then you set the beginning to the last frame that is 0.
                            End If
                        Else 'If the beginning foot is the left foot
                            nInterpolatedFrameBeginLeft(p) = nInterpolatedFrame(p, 0)
                        End If
                        p = p + 1
                    End If
                End If
            End If
            If nRawTotalForceRight(i) = 0 And nRawTotalForceRight(i + 1) > 0 Then 'Does the force on the right change from 0 to positive value.  If so the beginning of the stride has occurred.
                If q <= nNumberOfStrides Then
                    If i < nNumberOfFrames - 1 Then
                        If bBeginLeftOrRight = False Then 'Only need to do this if your beginning foot is the Left Foot.
                            tempSlope = nRawTotalForceRight(i + 2) - nRawTotalForceRight(i + 1)
                            nInterpolatedFrameBeginRight(q) = i + 1 - (nRawTotalForceRight(i + 1) / tempSlope)
                            If nInterpolatedFrameBeginRight(q) < i Or nInterpolatedFrameBeginRight(q) > i + 1 Then 'If the interpolated from is not within i and i+1 frame, then make the beginning at the i frame.
                                nInterpolatedFrameBeginRight(q) = i
                            End If
                        Else 'If the beginning foot is the right foot
                            nInterpolatedFrameBeginRight(q) = nInterpolatedFrame(q, 0)
                        End If
                        q = q + 1
                    End If
                End If
            End If
            If nRawTotalForceLeft(i) > 0 And nRawTotalForceLeft(i + 1) = 0 Then 'Does the force on the left drop to 0 betweeen this frame and the next?
                If r <= nNumberOfStrides Then
                    If i > 1 Then
                        tempSlope = nRawTotalForceLeft(i) - nRawTotalForceLeft(i - 1)
                        nInterpolatedFrameEndLeft(r) = i - nRawTotalForceLeft(i) / tempSlope
                        If nInterpolatedFrameEndLeft(r) > i + 1 Or nInterpolatedFrameEndLeft(r) < i Then 'if the interpolated frame is not between i and i+1, then make the end at point i+1.
                            nInterpolatedFrameEndLeft(r) = i + 1
                        End If
                        r = r + 1
                    End If
                End If
            End If
            If nRawTotalForceRight(i) > 0 And nRawTotalForceRight(i + 1) = 0 Then 'Does the force on the right drop from + to 0 between this frame and the next?
                If s <= nNumberOfStrides Then
                    If i > 1 Then
                        tempSlope = nRawTotalForceRight(i) - nRawTotalForceRight(i - 1)
                        nInterpolatedFrameEndRight(s) = i - nRawTotalForceRight(i) / tempSlope
                        If nInterpolatedFrameEndRight(s) > i + 1 Or nInterpolatedFrameEndRight(s) < i Then
                            nInterpolatedFrameEndRight(s) = i + 1
                        End If
                        s = s + 1
                    End If
                End If
            End If
            If p > nNumberOfStrides And q > nNumberOfStrides And r > nNumberOfStrides And s > nNumberOfStrides Then Exit For
        Next i

19:     'STEP 19: Now correct any of these beginning or ending values to get as close as possible to the first or last zero valued frame number
        lblProgressBar.Text = "Step 19: Correcting Beginning and Ending Values for Stance Period."
        ProgressBar1.Value = 19
        Dim kFrame, nFramesBetweenOnePercent As Double
        For Me.i = 1 To nNumberOfStrides
            nFramesBetweenOnePercent = 0.5 * (nInterpolatedFrame(i, 1) - nInterpolatedFrame(i, 0)) 'nFramesBetweenOnePercent is the part of the frame between each percent of the stride
            kFrame = Int(nInterpolatedFrameEndLeft(i)) 'Start with the ending of the left foot.
            If nRawTotalForceLeft(kFrame) = 0 Then 'correct for the frame closest to toe off
                Do Until nInterpolatedFrameEndLeft(i) - kFrame < nFramesBetweenOnePercent
                    nInterpolatedFrameEndLeft(i) = nInterpolatedFrameEndLeft(i) - 2 * nFramesBetweenOnePercent
                Loop
            End If
            kFrame = Int(nInterpolatedFrameEndRight(i)) 'Now for the end of the right foot.
            If nRawTotalForceRight(kFrame) = 0 Then 'Do only if the the full frame number less than the calculated ending frame is zero
                Do Until nInterpolatedFrameEndRight(i) - kFrame < nFramesBetweenOnePercent
                    nInterpolatedFrameEndRight(i) = nInterpolatedFrameEndRight(i) - 2 * nFramesBetweenOnePercent
                Loop
            End If
            kFrame = Int(nInterpolatedFrameBeginLeft(i)) + 1 'Now for the beginning of the left foot.
            If bBeginLeftOrRight = True And nRawTotalForceLeft(kFrame) = 0 Then
                Do Until kFrame - nInterpolatedFrameBeginLeft(i) < nFramesBetweenOnePercent
                    nInterpolatedFrameBeginLeft(i) = nInterpolatedFrameBeginLeft(i) + 2 * nFramesBetweenOnePercent
                Loop
            End If
            kFrame = Int(nInterpolatedFrameBeginRight(i)) + 1 'To correct the beginning frame number for the right foot
            If bBeginLeftOrRight = False And nRawTotalForceRight(kFrame) = 0 Then
                Do Until kFrame - nInterpolatedFrameBeginRight(i) < nFramesBetweenOnePercent
                    nInterpolatedFrameBeginRight(i) = nInterpolatedFrameBeginRight(i) + 2 * nFramesBetweenOnePercent
                Loop
            End If
        Next i

20:     'STEP 20: Determine whether this is a walking or running gait.  Walking is a false value, Running is a True value.
        lblProgressBar.Text = "Step 20: Determining whether this is walking or running gait"
        ProgressBar1.Value = 20
        bWalkingOrRunning = False 'The default value is Walking
        For Me.i = Int(nInterpolatedFrame(1, 0)) To Int(nInterpolatedFrame(1, 100))
            If nRawTotalForceLeft(i) = 0 And nRawTotalForceRight(i) = 0 Then
                bWalkingOrRunning = True 'If you find one frame in which both feet have zero force, then you make this a running gait.
                Exit For 'You don't need to go any further
            End If
        Next i

21:     'STEP 21: Calculate the Percent of the gait cycle that you will label as the begin and end of the stance phase of gait for each stride
        lblProgressBar.Text = "Step 21: Calculating the Beginning and Ending of Stance Phase for Each Stride"
        ProgressBar1.Value = 21
        Dim nBeginLeftStancePercent(nNumberOfStrides), nBeginRightStancePercent(nNumberOfStrides) As Integer
        Dim nEndLeftStancePercent(nNumberOfStrides), nEndRightStancePercent(nNumberOfStrides) As Integer
        Dim tempDiff As Double
        For Me.i = 1 To nNumberOfStrides
            If bBeginLeftOrRight = False Then 'If you are starting out on the lef foot
                nBeginLeftStancePercent(i) = 0
                Select Case bWalkingOrRunning
                    Case False
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameEndRight(i)) - (i - 1) * 100 - 3 'Get the percent making the end of the Left Stance Period.
                        tempDiff = 10 'This is an arbitary high value to start with
                        For Me.j = k To k + 7 'Check for 2 percentage points around the interpolated frame end.
                            If Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j))
                                nEndRightStancePercent(i) = j
                            End If
                        Next j
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameBeginRight(i)) - (i - 1) * 100 - 3 'Get the percent making the beginning of the Left Stance Period
                        tempDiff = 10 'This is an arbitary high value to start with
                        For Me.j = k To k + 7 'Check for 2 percentage points around the interpolated frame beginning.
                            If Math.Abs(nInterpolatedFrameBeginRight(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameBeginRight(i) - nInterpolatedFrame(i, j))
                                nBeginRightStancePercent(i) = j
                            End If
                        Next j
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameEndLeft(i)) - (i - 1) * 100 - 3 'Get the percent that marks the end of the Right Stance Period.
                        tempDiff = 10
                        For Me.j = k To k + 7
                            If Math.Abs(nInterpolatedFrameEndLeft(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameEndLeft(i) - nInterpolatedFrame(i, j))
                                nEndLeftStancePercent(i) = j
                            End If
                        Next
                    Case True 'This is if you are running
                        Dim nFramesToEvent As Single
                        nFramesToEvent = nInterpolatedFrameEndLeft(i) - nInterpolatedFrameBeginLeft(i)
                        nEndLeftStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of the gait cycle when the left foot leaves the ground.
                        nFramesToEvent = nInterpolatedFrameBeginRight(i) - nInterpolatedFrameBeginLeft(i)
                        nBeginRightStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of the gait cycle when the right foot makes contact with the ground.
                        nFramesToEvent = nInterpolatedFrameEndRight(i) - nInterpolatedFrameBeginLeft(i)
                        nEndRightStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of running gait cycle when right foot leaves the ground.
                End Select
            ElseIf bBeginLeftOrRight = True Then
                nBeginRightStancePercent(i) = 0 'If starting on the right foot, than beginning right stance always begins at 0%
                Select Case bWalkingOrRunning
                    Case False 'You are walking
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameEndRight(i)) - (i - 1) * 100 - 3 'Get the percent making the end of the Left Stance Period.
                        tempDiff = 10 'This is an arbitary high value to start with
                        For Me.j = k To k + 7 'Check for 2 percentage points around the interpolated frame end.
                            If Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j))
                                nEndLeftStancePercent(i) = j
                            End If
                        Next j
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameBeginLeft(i)) - (i - 1) * 100 - 3 'Get the percent making the beginning of the Left Stance Period
                        tempDiff = 10 'This is an arbitary high value to start with
                        For Me.j = k To k + 7 'Check for 2 percentage points around the interpolated frame beginning.
                            If Math.Abs(nInterpolatedFrameBeginLeft(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameBeginLeft(i) - nInterpolatedFrame(i, j))
                                nBeginLeftStancePercent(i) = j
                            End If
                        Next j
                        k = nPercentForEachRawFrameNumber(nInterpolatedFrameEndRight(i)) - (i - 1) * 100 - 3 'Get the percent that marks the end of the Right Stance Period.
                        tempDiff = 10
                        For Me.j = k To k + 7
                            If Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j)) < tempDiff Then
                                tempDiff = Math.Abs(nInterpolatedFrameEndRight(i) - nInterpolatedFrame(i, j))
                                nEndRightStancePercent(i) = j
                            End If
                        Next
                    Case True 'If you are running
                        Dim nFramesToEvent As Single
                        nFramesToEvent = nInterpolatedFrameEndRight(i) - nInterpolatedFrameBeginRight(i)
                        nEndRightStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of the gait cycle when the right foot leaves the ground.
                        nFramesToEvent = nInterpolatedFrameBeginLeft(i) - nInterpolatedFrameBeginLeft(i)
                        nBeginLeftStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of the gait cycle when the leftt foot makes contact with the ground.
                        nFramesToEvent = nInterpolatedFrameEndLeft(i) - nInterpolatedFrameBeginLeft(i)
                        nEndLeftStancePercent(i) = Int(100 * nFramesToEvent / (nInterpolatedFrame(i, 100) - nInterpolatedFrame(i, 0)) + 0.5) 'calculates the percent of running gait cycle when left foot leaves the ground for each stride.
                End Select
            End If
        Next i

22:     'STEP 22:  'Calculate the AllStepForces for Each foot for Each Stride in the Sample.
        lblProgressBar.Text = "Step 22: Calculating Force for Each Percent of Each Stride"
        ProgressBar1.Value = 22
        ReDim arAllLeftForces(nNumberOfStrides, 100)
        ReDim arAllRightForces(nNumberOfStrides, 100)
        ReDim arAllTotalForces(nNumberOfStrides, 100)
        Dim nInt As Integer 'This integer is for the frame number that corresponds to the percent of gait.
        Dim nFract As Double 'This is the fraction portion of the frame number for any percent of gait.
        For Me.i = 1 To nNumberOfStrides
            nFramesBetweenOnePercent = nInterpolatedFrame(i, 1) - nInterpolatedFrame(i, 0)
            For Me.j = 0 To 99
                nInt = Int(nInterpolatedFrame(i, j)) 'Get the integer portion of the frame
                nFract = nInterpolatedFrame(i, j) - nInt 'Get the fraction of the frame number between the two.
                If nRawTotalForceLeft(nInt) <> 0 And nRawTotalForceLeft(nInt + 1) <> 0 Then 'If the force of the both the lower and upper frames > 0
                    tempSlope = nRawTotalForceLeft(nInt + 1) - nRawTotalForceLeft(nInt)
                    arAllLeftForces(i, j) = nRawTotalForceLeft(nInt) + nFract * tempSlope 'Fill in the all Left Forces for each percent.
                ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) <> 0 Then 'If heel strike occurred between the two frames
                    If bBeginLeftOrRight = True Or j <> 0 Then 'If the right foot begins the gait cycle
                        If nInterpolatedFrameBeginLeft(i) < nInt Then
                            tempSlope = nRawTotalForceLeft(nInt + 1)
                        ElseIf Math.Abs(nInterpolatedFrame(i, j) - nInterpolatedFrameBeginLeft(i)) < 0.5 * nFramesBetweenOnePercent Then
                            tempSlope = 0
                        ElseIf nInterpolatedFrame(i, j) < nInterpolatedFrameBeginLeft(i) Then
                            tempSlope = 0
                        Else 'This means that you have not reached the next full frame # but are > .5 percent greater than the beginning of heel strike
                            tempSlope = nInt + 1 - nInterpolatedFrameBeginLeft(i)
                            tempSlope = nRawTotalForceLeft(nInt + 1) / tempSlope
                        End If
                    Else 'The left foot begins the gait cycle
                        tempSlope = 0
                    End If
                    If tempSlope = 0 Then
                        arAllLeftForces(i, j) = 0
                    Else
                        arAllLeftForces(i, j) = nRawTotalForceLeft(nInt + 1) - tempSlope * (1 - nFract)
                    End If
                ElseIf nRawTotalForceLeft(nInt) <> 0 And nRawTotalForceLeft(nInt + 1) = 0 Then 'If Toe Off occurred between the two frames
                    If nInt > 1 Then
                        If nInterpolatedFrameEndLeft(i) - nInt >= 1 Then 'If the 0% is all the way to the next full frame.
                            tempSlope = -nRawTotalForceLeft(nInt)
                        ElseIf Math.Abs(nInterpolatedFrame(i, j) - nInterpolatedFrameEndLeft(i)) < 0.5 * nFramesBetweenOnePercent Then
                            tempSlope = 0
                        ElseIf nInterpolatedFrame(i, j) - nInterpolatedFrameEndLeft(i) > 0 Then
                            tempSlope = 0
                        Else
                            tempSlope = -nRawTotalForceLeft(nInt) / (nInterpolatedFrameEndLeft(i) - nInt)
                        End If
                        If tempSlope <> 0 Then
                            arAllLeftForces(i, j) = nRawTotalForceLeft(nInt) + tempSlope * nFract
                        Else
                            arAllLeftForces(i, j) = 0
                        End If
                    Else 'This occurs only if you are on frame 0 or 1
                        tempSlope = nRawTotalForceLeft(nInt - 1)
                        arAllLeftForces(i, j) = nRawTotalForceLeft(nInt) + nFract * tempSlope
                    End If
                ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) = 0 Then 'If Both frames are in swing.
                    arAllLeftForces(i, j) = 0
                End If
                'Now do the same thing for the RIGHT foot, determining the force at the percent of the gait cycle
                If nRawTotalForceRight(nInt) <> 0 And nRawTotalForceRight(nInt + 1) <> 0 Then 'If the force of the lower and upper frames>0
                    tempSlope = nRawTotalForceRight(nInt + 1) - nRawTotalForceRight(nInt)
                    arAllRightForces(i, j) = nRawTotalForceRight(nInt) + nFract * tempSlope
                ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) <> 0 Then 'If heel strike occurred between the two frames
                    If bBeginLeftOrRight = False Or j <> 0 Then 'If the Left foot strike is the beginning of the stride.
                        If nInterpolatedFrameBeginRight(i) < nInt Then
                            tempSlope = nRawTotalForceRight(nInt + 1)
                        ElseIf Math.Abs(nInterpolatedFrame(i, j) - nInterpolatedFrameBeginRight(i)) < 0.5 * nFramesBetweenOnePercent Then
                            tempSlope = 0
                        ElseIf nInterpolatedFrame(i, j) < nInterpolatedFrameBeginRight(i) Then
                            tempSlope = 0
                        Else
                            tempSlope = nInt + 1 - nInterpolatedFrameBeginRight(i)
                            tempSlope = nRawTotalForceRight(nInt + 1) / tempSlope
                        End If
                    Else
                        tempSlope = 0
                    End If
                    If tempSlope = 0 Then 'In this case if there is another percentage point in the same frame that was greater than or equal to zero
                        arAllRightForces(i, j) = 0
                    Else
                        arAllRightForces(i, j) = nRawTotalForceRight(nInt + 1) - tempSlope * (1 - nFract)
                    End If
                ElseIf nRawTotalForceRight(nInt) <> 0 And nRawTotalForceRight(nInt + 1) = 0 Then 'If Toe Off occurred between the two frames
                    If nInterpolatedFrameEndRight(i) - nInt >= 1 Then
                        tempSlope = -nRawTotalForceRight(nInt)
                    ElseIf Math.Abs(nInterpolatedFrame(i, j) - nInterpolatedFrameEndRight(i)) < 0.5 * nFramesBetweenOnePercent Then
                        tempSlope = 0
                    ElseIf nInterpolatedFrame(i, j) - nInterpolatedFrameEndRight(i) > 0 Then
                        tempSlope = 0
                    Else
                        tempSlope = -nRawTotalForceRight(nInt) / (nInterpolatedFrameEndRight(i) - nInt)
                    End If
                    If tempSlope <> 0 Then
                        arAllRightForces(i, j) = nRawTotalForceRight(nInt) + tempSlope * nFract
                    Else
                        arAllRightForces(i, j) = 0
                    End If
                ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) = 0 Then 'If Both frames are in swing.
                    arAllRightForces(i, j) = 0
                End If
                'Finally add the Left and Right forces
                arAllTotalForces(i, j) = arAllLeftForces(i, j) + arAllRightForces(i, j)
            Next j
        Next i

23:     'STEP 23: Make the 100% the same as the 0% of the next stride
        lblProgressBar.Text = "Step 23: make 100% the same as 0% of the next stride"
        For Me.i = 1 To nNumberOfStrides - 1
            arAllLeftForces(i, 100) = arAllLeftForces(i + 1, 0)
            arAllRightForces(i, 100) = arAllRightForces(i + 1, 0)
            arAllTotalForces(i, 100) = arAllTotalForces(i + 1, 0)
        Next i
        'then calculate 100% for the final stride
        nInt = Int(nInterpolatedFrame(nNumberOfStrides, 100)) 'Get the integer portion of the frame
        nFract = nInterpolatedFrame(nNumberOfStrides, 100) - nInt 'Get the fraction of the frame number between the two.
        If nFract = 0 Then 'If you're ending on an exact frame, the calculations are easy.
            arAllLeftForces(nNumberOfStrides, 100) = nRawTotalForceLeft(nInt)
            arAllRightForces(nNumberOfStrides, 100) = nRawTotalForceRight(nInt)
        Else 'If the end of the data is not an exact frame number
            If bBeginLeftOrRight = False Then
                arAllLeftForces(nNumberOfStrides, 100) = 0
                If nRawTotalForceRight(nInt - 1) > 0 Then
                    tempSlope = nRawTotalForceRight(nInt) - nRawTotalForceRight(nInt - 1)
                    arAllRightForces(nNumberOfStrides, 100) = nRawTotalForceRight(nInt) + tempSlope * nFract
                Else
                    arAllRightForces(nNumberOfStrides, 100) = 0
                End If
            ElseIf bBeginLeftOrRight = True Then
                arAllRightForces(nNumberOfStrides, 100) = 0
                If nRawTotalForceLeft(nInt - 1) > 0 Then
                    tempSlope = nRawTotalForceLeft(nInt) - nRawTotalForceLeft(nInt - 1)
                    arAllLeftForces(nNumberOfStrides, 100) = nRawTotalForceLeft(nInt) + tempSlope * nFract
                Else
                    arAllLeftForces(nNumberOfStrides, 100) = 0
                End If
            End If
        End If

        'Step 23A:  Go back and eliminate any forces that were inadvertently positive because of interpolation
        lblProgressBar.Text = "Step 23A:  Eliminating any forces inadvertently added by interpolation."
        For Me.i = 1 To nNumberOfStrides
            Select Case bWalkingOrRunning
                Case False 'This is for a walking gait
                    If bBeginLeftOrRight = False Then 'This is if you are beginning on the left foot.
                        Me.j = 1
                        Do Until arAllLeftForces(i, j) = 0
                            j = j + 1 'Find the last force zero force in stance
                        Loop
                        Do
                            j = j + 1
                            arAllLeftForces(i, j) = 0 'Make all the rest of the forces in stance zero
                        Loop Until j = 100
                        j = 99
                        Do Until arAllRightForces(i, j) = 0 'Find the beginning of the right stance period
                            j = j - 1
                        Loop
                        k = 1
                        Do Until arAllRightForces(i, k) = 0 'Find the end of the right stance period
                            k = k + 1
                        Loop
                        Do
                            k = k + 1
                            arAllRightForces(i, k) = 0 'Make all the right forces between the end and beginning of stance zero.
                        Loop Until k = j - 1
                    ElseIf bBeginLeftOrRight = True Then 'This is you are beginning on the right foot
                        Me.j = 1
                        Do Until arAllRightForces(i, j) = 0
                            j = j + 1 'Find the last force zero force in stance for the right foot
                        Loop
                        Do
                            j = j + 1
                            arAllRightForces(i, j) = 0 'Make all the rest of the forces in stance phase zero
                        Loop Until j = 100
                        j = 99
                        Do Until arAllLeftForces(i, j) = 0 'Find the beginning of the left stance period
                            j = j - 1
                        Loop
                        k = 1
                        Do Until arAllLeftForces(i, k) = 0 'Find the end of the left stance period
                            k = k + 1
                        Loop
                        Do
                            k = k + 1
                            arAllLeftForces(i, k) = 0 'Make all the right forces between the end and beginning of stance zero.
                        Loop Until k = j - 1
                    End If
                Case True 'This is for a running gait
                    Me.j = 1
                    Do Until arAllLeftForces(i, j) > 0 'Find the end of the stance phase of running
                        j = j + 1
                    Loop
                    Do Until arAllLeftForces(i, j) = 0
                        j = j + 1
                    Loop
                    Do 'Zero out all other frames until the end of the gait cycle
                        j = j + 1
                        arAllLeftForces(i, j) = 0
                    Loop Until j = 100
                    Me.j = 1
                    Do Until arAllRightForces(i, j) > 0
                        j = j + 1
                    Loop
                    Do Until arAllRightForces(i, j) = 0
                        j = j + 1
                    Loop
                    Do
                        j = j + 1
                        arAllRightForces(i, j) = 0
                    Loop Until j = 100
            End Select
        Next i

24:     'STEP 24: Interpolate the individual Sensel values for each percent of the gait cycle of each stride
        lblProgressBar.Text = "Step 24:  Find the value for the individual sensels for each stride of the gait cycle, from 0-100%"
        ProgressBar1.Value = 24
        Dim arAllSenselForcesLeft(nNumberOfStrides, 100, 59, 20) 'This array will calculate each sensel force for each stride in each percent of the gait cycle - Left Foot
        Dim arAllSenselForcesRight(nNumberOfStrides, 100, 59, 20) 'This array will calculate each sensel force for each stride in each percent of the gait cycle - Right Foot
        For Me.i = 1 To nNumberOfStrides 'Counter for the number of strides
            nFramesBetweenOnePercent = nInterpolatedFrame(i, 1) - nInterpolatedFrame(i, 0)
            For Me.j = 0 To 99 'For each percent of the stride cycle, from 0 to 99
                If arAllLeftForces(i, j) <> 0 Then 'If the total force in that percent of the gait cycle is 0, then fill all the sensels with zero values.
                    nInt = Int(nInterpolatedFrame(i, j)) 'Get the integer portion of the frame number for that percent of the gait cycle.
                    nFract = nInterpolatedFrame(i, j) - nInt 'Get the fraction of the frame number between the two.
                    For Me.r = 0 To 59 'The next two For-Next routine Interpolates the sensel matrix for that percent of the gait cycle
                        For Me.s = 0 To 20
                            If nSenselMatrixLeft(nInt, r, s) = 0 And nSenselMatrixLeft(nInt + 1, r, s) = 0 Then 'If the sensel matrices on the upper and lower frames are zero then the value for the sensel is zero
                                arAllSenselForcesLeft(i, j, r, s) = 0
                            ElseIf nFract = 0 Then 'If you are right on an original frame number, no interpolation is needed.
                                arAllSenselForcesLeft(i, j, r, s) = nSenselMatrixLeft(i, r, s)
                            Else 'If the percent of the gait cycle is between the two original frame numbers, then you need to interpolate using straight line interpolation.
                                tempSlope = nSenselMatrixLeft(nInt + 1, r, s) - nSenselMatrixLeft(nInt, r, s) 'subtract the sensel value of frame n from sensel value of frame n+1
                                arAllSenselForcesLeft(i, j, r, s) = nSenselMatrixLeft(nInt, r, s) + nFract * tempSlope
                            End If
                        Next s
                    Next r
                End If
            Next j
        Next i
        'STEP 29A: Do the same thing for the Right foot to get all the sensel values at each percent of the gait cycle.
        For Me.i = 1 To nNumberOfStrides 'Counter for the number of strides
            nFramesBetweenOnePercent = nInterpolatedFrame(i, 1) - nInterpolatedFrame(i, 0)
            For Me.j = 0 To 100 'For each percent of the stride cycle, from 0 to 99
                If arAllRightForces(i, j) <> 0 Then 'If the total force in that percent of the gait cycle is 0, then fill all the sensel values.  Otherwise they will all be left as nothing.
                    nInt = Int(nInterpolatedFrame(i, j)) 'Get the integer portion of the frame number for that percent of the gait cycle.
                    nFract = nInterpolatedFrame(i, j) - nInt 'Get the fraction of the frame number between the two.
                    For Me.r = 0 To 59 'The next two For-Next routine Interpolates the sensel matrix for that percent of the gait cycle
                        For Me.s = 0 To 20
                            If nSenselMatrixRight(nInt, r, s) = 0 And nSenselMatrixRight(nInt + 1, r, s) = 0 Then 'If the sensel matrices on the upper and lower frames are zero then the value for the sensel is zero
                                arAllSenselForcesRight(i, j, r, s) = 0
                            ElseIf nFract = 0 Then 'If you are right on an original frame number, no interpolation is needed.
                                arAllSenselForcesRight(i, j, r, s) = nSenselMatrixRight(i, r, s)
                            Else 'If the percent of the gait cycle is between the two original frame numbers, then you need to interpolate using straight line interpolation.
                                tempSlope = nSenselMatrixRight(nInt + 1, r, s) - nSenselMatrixRight(nInt, r, s) 'subtract the sensel value of frame n from sensel value of frame n+1
                                arAllSenselForcesRight(i, j, r, s) = nSenselMatrixRight(nInt, r, s) + nFract * tempSlope
                            End If
                        Next s
                    Next r
                End If
            Next j
        Next i

25:     'STEP 25: Interpolate the CoP values for each percent of each gait cycle.
        lblProgressBar.Text = "Step 25: Determine the CoP Value for Each Sensel for Each Percent of each stride."
        ProgressBar1.Value = 25
        Dim nCoP_ML_AllFrames_Left(nNumberOfStrides, 100), nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) As Double
        Dim nCoP_AP_AllFrames_Left(nNumberOfStrides, 100), nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) As Double
        For Me.i = 1 To nNumberOfStrides
            For Me.j = 0 To 99
                nInt = Int(nInterpolatedFrame(i, j)) 'Get the integer portion of the frame
                nFract = nInterpolatedFrame(i, j) - nInt 'Get the fraction of the frame number between the two.
                'Step 25A: Find the CoP on the Left foot
                If j <> 0 Then
                    If arAllLeftForces(i, j) <> 0 Then 'Condition 25A(1) - If the force is not zero, you have to calculate the CoP
                        If nRawTotalForceLeft(nInt) <> 0 And nRawTotalForceLeft(nInt + 1) > 0 Then 'The frames on either side are both > zero
                            tempSlope = nRawCoPAPLeft(nInt + 1) - nRawCoPAPLeft(nInt)
                            nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt) + nFract * tempSlope
                        ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) > 0 Then 'If you are in between  a frame 0 and  frame>0 force, but not at 0% of stance.
                            If nInt < nNumberOfFrames - 1 Then
                                tempSlope = nRawCoPAPLeft(nInt + 2) - nRawCoPAPLeft(nInt + 1)
                                nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt + 1) - (1 - nFract) * tempSlope
                            Else
                                tempSlope = 0
                                nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt + 1)
                            End If
                        ElseIf nRawCoPAPLeft(nInt) <> 0 And nRawCoPAPLeft(nInt + 1) = 0 Then 'This is the special case when you are at 99% and the 100% point is before the next full frame.
                            If nInt > 1 Then
                                tempSlope = nRawCoPAPLeft(nInt) - nRawCoPAPLeft(nInt - 1)
                                nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt) + nFract * tempSlope
                            Else
                                nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt)
                            End If
                        End If
                    ElseIf arAllLeftForces(i, j) = 0 And arAllLeftForces(i, j + 1) > 0 Then 'Condition 25A(2) - If this is the initiation of foot strike at 0% of the stance period.
                        k = 1
                        If nInt < nNumberOfFrames - 1 Then 'This is the slope if Left foot starts. Just make sure you're not at the next to last frame in the entire sample.
                            Do Until nRawCoPAPLeft(nInt + k) > 0 Or nInt + k + 1 = nNumberOfFrames
                                k = k + 1
                            Loop
                            tempSlope = nRawCoPAPLeft(nInt + k + 1) - nRawCoPAPLeft(nInt + k)
                        Else 'This little else catches the condition in which you at the next to last frame.
                            tempSlope = 0
                        End If
                        nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt + k) - (k - nFract) * tempSlope
                    ElseIf arAllLeftForces(i, j - 1) > 0 And arAllLeftForces(i, j) = 0 Then 'Condition 25A(3)If this is the last percent of the stance phase, before entering swing
                        If nInt > 1 Then
                            tempSlope = (nCoP_AP_AllFrames_Left(i, j - 1) - nCoP_AP_AllFrames_Left(i, j - 2))
                            nCoP_AP_AllFrames_Left(i, j) = nCoP_AP_AllFrames_Left(i, j - 1) + tempSlope
                        Else
                            nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt)
                        End If
                    Else 'Condition 25A(4) If the Foot is in Swing.
                        nCoP_AP_AllFrames_Left(i, j) = 0
                    End If
                ElseIf j = 0 Then 'Condition 29A(5) 'If we are at zero percent of the gait cycle
                    If nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) = 0 Then
                        nCoP_AP_AllFrames_Left(i, j) = 0
                    ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) > 0 Then
                        If nInt < nNumberOfFrames - 1 Then 'make sure that there are at least two frames at the end
                            tempSlope = nRawCoPAPLeft(n + 2) - nRawCoPAPLeft(n + 1)
                            nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt + 1) - (1 - nFract) * tempSlope
                        End If
                    ElseIf nRawTotalForceLeft(nInt) > 0 And nRawTotalForceLeft(nInt + 1) = 0 Then
                        If nInt > 2 Then
                            tempSlope = nRawCoPAPLeft(n - 1) - nRawCoPAPLeft(n - 2)
                            nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt) + nFract * tempSlope
                        End If
                    ElseIf nRawTotalForceLeft(nInt) > 0 And nRawTotalForceLeft(nInt + 1) > 0 Then
                        tempSlope = nRawCoPAPLeft(nInt + 1) - nRawCoPAPLeft(nInt)
                        nCoP_AP_AllFrames_Left(i, j) = nRawCoPAPLeft(nInt) + nFract * tempSlope
                    End If
                End If
                'Step 25B  - Find the CoP location for all frames on the RIGHT foot in the AP direction.
                If j <> 0 Then
                    If arAllRightForces(i, j) <> 0 Then 'Condition 25B(1) - If there is a positive force, you have to have a CoP Location
                        If nRawTotalForceRight(nInt) <> 0 And nRawTotalForceRight(nInt + 1) > 0 Then 'If the force of the lower and upper frames>0
                            tempSlope = nRawCoPAPRight(nInt + 1) - nRawCoPAPRight(nInt)
                            nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt) + nFract * tempSlope
                        ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) > 0 Then 'If your are between a zero total force frame and a postive total force frame.
                            If nInt < nNumberOfFrames - 1 Then 'Make sure there are two full frames betwen you and the end of the sample
                                tempSlope = nRawCoPAPRight(nInt + 2) - nRawCoPAPRight(nInt + 1)
                                nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt + 1) - (1 - nFract) * tempSlope
                            Else
                                tempSlope = 0
                                nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt + 1)
                            End If
                        ElseIf nRawTotalForceRight(nInt) <> 0 And nRawTotalForceRight(nInt + 1) = 0 Then 'If you are between a postive force for the last frame and the next full frame has a 0 force.
                            tempSlope = nCoP_AP_AllFrames_Right(i, j - 1) - nCoP_AP_AllFrames_Right(i, j - 2)
                            nCoP_AP_AllFrames_Right(i, j) = nCoP_AP_AllFrames_Right(i, j - 1) + tempSlope
                        End If
                    ElseIf arAllRightForces(i, j) = 0 And arAllRightForces(i, j + 1) > 0 Then 'Condition 25B(2) - If heel strike occurrs at j percent
                        k = 1
                        If nInt < nNumberOfFrames - 1 Then
                            Do Until nRawCoPAPRight(nInt + k) > 0 Or nInt + k + 1 = nNumberOfFrames
                                k = k + 1
                            Loop
                            tempSlope = nRawCoPAPRight(nInt + k + 1) - nRawCoPAPRight(nInt + k)
                        Else
                            tempSlope = 0
                        End If
                        nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt + k) - (k - nFract) * tempSlope
                    ElseIf arAllRightForces(i, j - 1) <> 0 And arAllRightForces(i, j) = 0 Then 'Condition 25B(3) - If you are on the toe off percent.
                        If nInt > 1 Then
                            tempSlope = nCoP_AP_AllFrames_Right(i, j - 1) - nCoP_AP_AllFrames_Right(i, j - 2)
                            nCoP_AP_AllFrames_Right(i, j) = nCoP_AP_AllFrames_Right(i, j - 1) + tempSlope
                        Else
                            nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt)
                        End If
                    Else 'If Both frames are in swing.
                        nCoP_AP_AllFrames_Right(i, j) = 0
                    End If
                ElseIf j = 0 Then
                    If nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) = 0 Then
                        nCoP_AP_AllFrames_Right(i, j) = 0
                    ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) > 0 Then
                        If nInt < nNumberOfFrames - 1 Then 'make sure that there are at least two frames at the end
                            tempSlope = nRawCoPAPRight(n + 2) - nRawCoPAPRight(n + 1)
                            nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt + 1) - (1 - nFract) * tempSlope
                        End If
                    ElseIf nRawTotalForceRight(nInt) > 0 And nRawTotalForceRight(nInt + 1) = 0 Then
                        If nInt > 2 Then
                            tempSlope = nRawCoPAPRight(n - 1) - nRawCoPAPRight(n - 2)
                            nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt) + nFract * tempSlope
                        End If
                    ElseIf nRawTotalForceRight(nInt) > 0 And nRawTotalForceRight(nInt + 1) > 0 Then
                        tempSlope = nRawCoPAPRight(nInt + 1) - nRawCoPAPRight(nInt)
                        nCoP_AP_AllFrames_Right(i, j) = nRawCoPAPRight(nInt) + nFract * tempSlope
                    End If
                End If

                'Step 25C - Find the ML location of the CoP in the LEFT Foot.
                 If j <> 0 Then
                    If arAllLeftForces(i, j) > 0 Then 'Condition 25C(1) If there is a positive Left Force, you have to calculate the CoP
                        If nRawTotalForceLeft(nInt) > 0 And nRawTotalForceLeft(nInt + 1) > 0 Then ' If the force of the lower and upper frames>0
                            tempSlope = nRawCoPMLLeft(nInt + 1) - nRawCoPMLLeft(nInt)
                            nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt) + nFract * tempSlope
                        ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) > 0 Then 'If heel strike occurred between the two frames
                            If nInt < nNumberOfFrames - 1 Then 'Makes sure that you're not at the next to last frame in the sample.
                                tempSlope = nRawCoPMLLeft(nInt + 2) - nRawCoPMLLeft(nInt + 1)
                                nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt + 1) - (1 - nFract) * tempSlope
                            Else
                                tempSlope = 0
                                nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt + 1)
                            End If
                        ElseIf nRawTotalForceLeft(nInt) <> 0 And nRawTotalForceLeft(nInt + 1) = 0 Then 'You are between a frames that are ending the stance period.
                            If nInt > 1 Then 'Make sure that you're not on the first frame
                                tempSlope = nCoP_ML_AllFrames_Left(i, j - 1) - nCoP_ML_AllFrames_Left(i, j - 2)
                                nCoP_ML_AllFrames_Left(i, j) = nCoP_ML_AllFrames_Left(i, j - 1) + tempSlope
                            Else
                                nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt)
                            End If
                        End If
                    ElseIf arAllLeftForces(i, j) = 0 And arAllLeftForces(i, j + 1) > 0 Then 'Condition 25C(2) If heel strikes occurs at j percent
                        k = 1
                        If nInt < nNumberOfFrames - 1 Then
                            Do Until nRawCoPMLLeft(nInt + k) > 0 Or nInt + k + 1 = nNumberOfFrames
                                k = k + 1
                            Loop
                            tempSlope = nRawCoPMLLeft(nInt + k + 1) - nRawCoPMLLeft(nInt + k)
                        Else
                            tempSlope = 0
                        End If
                        nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt + k) - tempSlope * (k - nFract)
                    ElseIf arAllLeftForces(i, j - 1) <> 0 And arAllLeftForces(i, j) = 0 Then 'If you are at the toe-off percent.
                        If nInt > 1 And j > 0 Then
                            tempSlope = nCoP_ML_AllFrames_Left(i, j - 1) - nCoP_ML_AllFrames_Left(i, j - 2)
                            nCoP_ML_AllFrames_Left(i, j) = nCoP_ML_AllFrames_Left(i, j - 1) + tempSlope
                        Else
                            nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt)
                        End If
                    ElseIf arAllLeftForces(i, j - 1) = 0 And arAllLeftForces(i, j + 1) = 0 Then 'If you are in swing.
                        nCoP_ML_AllFrames_Left(i, j) = 0
                    End If
                ElseIf j = 0 Then
                    If nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) = 0 Then 'if both the first and 2nd raw frames are zero.
                        nCoP_ML_AllFrames_Left(i, j) = 0
                    ElseIf nRawTotalForceLeft(nInt) = 0 And nRawTotalForceLeft(nInt + 1) > 0 Then
                        If nInt < nNumberOfFrames - 1 Then 'make sure that there are at least two frames at the end
                            tempSlope = nRawCoPMLLeft(n + 2) - nRawCoPMLLeft(n + 1)
                            nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt + 1) - (1 - nFract) * tempSlope
                        End If
                    ElseIf nRawTotalForceLeft(nInt) > 0 And nRawTotalForceLeft(nInt + 1) = 0 Then
                        If nInt > 2 Then
                            tempSlope = nRawCoPMLLeft(n - 1) - nRawCoPMLLeft(n - 2)
                            nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt) + nFract * tempSlope
                        End If
                    ElseIf nRawTotalForceLeft(nInt) > 0 And nRawTotalForceLeft(nInt + 1) > 0 Then
                        tempSlope = nRawCoPMLLeft(nInt + 1) - nRawCoPMLLeft(nInt)
                        nCoP_ML_AllFrames_Left(i, j) = nRawCoPMLLeft(nInt) + nFract * tempSlope
                    End If
                End If

                'Step 25D - Find the CoP location for all frames on the RIGHT foot in the ML direction.
                If j <> 0 Then
                    If arAllRightForces(i, j) > 0 Then 'If you have a postive Right force you have to calculate the CoP.
                        If nRawTotalForceRight(nInt) > 0 And nRawTotalForceRight(nInt + 1) > 0 Then 'If the force of the lower and upper frames>0
                            tempSlope = nRawCoPMLRight(nInt + 1) - nRawCoPMLRight(nInt)
                            nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt) + nFract * tempSlope
                        ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) <> 0 Then 'If heel strike occurred between the two frames
                            If nInt < nNumberOfFrames - 1 Then 'Makes sure that you're not at the next to last frame in the sample.
                                tempSlope = nRawCoPMLRight(nInt + 2) - nRawCoPMLRight(nInt + 1)
                                nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt + 1) - (1 - nFract) * tempSlope
                            Else
                                tempSlope = 0
                                nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt + 1)
                            End If
                        ElseIf nRawTotalForceRight(nInt) <> 0 And nRawTotalForceRight(nInt + 1) = 0 Then 'You are between a frames that are ending the stance period.
                            If nInt > 1 Then 'Make sure that you're not on the first frame
                                tempSlope = nCoP_ML_AllFrames_Right(i, j - 1) - nCoP_ML_AllFrames_Right(i, j - 2)
                                nCoP_ML_AllFrames_Right(i, j) = nCoP_ML_AllFrames_Right(i, j - 1) + tempSlope
                            Else
                                nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt)
                            End If
                        End If
                    ElseIf arAllRightForces(i, j) = 0 And arAllRightForces(i, j + 1) <> 0 Then 'Conition 25D(2) If heel strikes occurs at j percent
                        k = 1
                        If nInt < nNumberOfFrames - 1 Then
                            Do Until nRawCoPMLRight(nInt + k) > 0 Or nInt + k + 1 = nNumberOfFrames
                                k = k + 1
                            Loop
                            tempSlope = nRawCoPMLRight(nInt + k + 1) - nRawCoPMLRight(nInt + k)
                        Else
                            tempSlope = 0
                        End If
                        nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt + k) - tempSlope * (k - nFract)
                    ElseIf arAllRightForces(i, j - 1) <> 0 And arAllRightForces(i, j) = 0 Then 'If you are at the toe-off percent.
                        If nInt > 1 And j > 0 Then
                            tempSlope = nCoP_ML_AllFrames_Right(i, j - 1) - nCoP_ML_AllFrames_Right(i, j - 2)
                            nCoP_ML_AllFrames_Right(i, j) = nCoP_ML_AllFrames_Right(i, j - 1) + tempSlope
                        Else
                            nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt)
                        End If
                    ElseIf arAllRightForces(i, j - 1) = 0 And arAllRightForces(i, j + 1) = 0 Then 'If you are in swing.
                        nCoP_ML_AllFrames_Right(i, j) = 0
                    End If
                ElseIf j = 0 Then
                    If nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) = 0 Then 'if both the first and 2nd raw frames are zero.
                        nCoP_ML_AllFrames_Right(i, j) = 0
                    ElseIf nRawTotalForceRight(nInt) = 0 And nRawTotalForceRight(nInt + 1) > 0 Then
                        If nInt < nNumberOfFrames - 1 Then 'make sure that there are at least two frames at the end
                            tempSlope = nRawCoPMLRight(n + 2) - nRawCoPMLRight(n + 1)
                            nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt + 1) - (1 - nFract) * tempSlope
                        End If
                    ElseIf nRawTotalForceRight(nInt) > 0 And nRawTotalForceRight(nInt + 1) = 0 Then
                        If nInt > 2 Then
                            tempSlope = nRawCoPMLRight(n - 1) - nRawCoPMLRight(n - 2)
                            nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt) + nFract * tempSlope
                        End If
                    ElseIf nRawTotalForceRight(nInt) > 0 And nRawTotalForceRight(nInt + 1) > 0 Then
                        tempSlope = nRawCoPMLRight(nInt + 1) - nRawCoPMLRight(nInt)
                        nCoP_ML_AllFrames_Right(i, j) = nRawCoPMLRight(nInt) + nFract * tempSlope
                    End If
                End If

            Next j 'Next percent
        Next i 'Next stride

26:     'STEP 26: Fill in the 100% phase for the CoP values 
        lblProgressBar.Text = "Step 26: Fill in the 100% phase for the CoP values."
        ProgressBar1.Value = 26
        For Me.i = 1 To nNumberOfStrides - 1 'This loop is true for all but the last stride.
            nCoP_AP_AllFrames_Left(i, 100) = nCoP_AP_AllFrames_Left(i + 1, 0)
            nCoP_AP_AllFrames_Right(i, 100) = nCoP_AP_AllFrames_Right(i + 1, 0)
            nCoP_ML_AllFrames_Left(i, 100) = nCoP_ML_AllFrames_Left(i + 1, 0)
            nCoP_ML_AllFrames_Right(i, 100) = nCoP_ML_AllFrames_Right(i + 1, 0)
        Next i
        nInt = Int(nInterpolatedFrame(nNumberOfStrides, 100)) 'Get the integer portion of the frame
        nFract = nInterpolatedFrame(nNumberOfStrides, 100) - nInt 'Get the fraction of the frame number between the two.
        If nFract = 0 Then 'If you're ending on an exact frame, the calculations are easy.
            nCoP_AP_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPAPLeft(nInt)
            nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPAPRight(nInt)
            nCoP_ML_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPMLLeft(nInt)
            nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPMLRight(nInt)
        Else 'If the end of the data is not an exact frame number
            If nInt < UBound(nRawTotalForceLeft) - 1 Then 'If the last integer is not the last frame in the sample
                'Do the Left Foot CoP First
                If arAllLeftForces(nNumberOfStrides, 99) <> 0 Then 'This would be true if the right foot is the start foot and you are walking.
                    tempSlope = nRawCoPAPLeft(nInt + 1) - nRawCoPAPLeft(nInt)
                    nCoP_AP_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPAPLeft(nInt) + nFract * tempSlope
                    tempSlope = nRawCoPMLLeft(nInt + 1) - nRawCoPMLLeft(nInt)
                    nCoP_ML_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPMLLeft(nInt) + nFract * tempSlope
                ElseIf bBeginLeftOrRight = True Then 'This would only be true if you were running and the right foot was first to hit the ground.
                    nCoP_AP_AllFrames_Left(nNumberOfStrides, 100) = 0
                    nCoP_ML_AllFrames_Left(nNumberOfStrides, 100) = 0
                Else 'This would be true if the Left foot is the Start foot and you are not at the last frame.
                    tempSlope = nRawCoPAPLeft(nInt + 2) - nRawCoPAPLeft(nInt + 1)
                    nCoP_AP_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPAPLeft(n + 1) - (1 - nFract) * tempSlope
                    tempSlope = nRawCoPMLLeft(nInt + 2) - nRawCoPMLLeft(nInt + 1)
                    nCoP_ML_AllFrames_Left(nNumberOfStrides, 100) = nRawCoPMLLeft(n + 1) - (1 - nFract) * tempSlope
                End If
                'Now do the Right Foot CoP 100% at the last stride.
                If arAllRightForces(nNumberOfStrides, 99) <> 0 Then 'This would be true if the left foot is the start foot and you are walking.
                    tempSlope = nRawCoPAPRight(nInt + 1) - nRawCoPAPRight(nInt)
                    nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPAPRight(nInt) + tempSlope * nFract
                    tempSlope = nRawCoPMLRight(nInt + 1) - nRawCoPMLRight(nInt)
                    nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPMLRight(nInt) + tempSlope * nFract
                ElseIf bBeginLeftOrRight = False Then 'This would be true if you are running and the Left foot is initiating the gait cycle.
                    nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) = 0
                    nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) = 0
                Else 'This would be true if the Right foot is the start foot.
                    tempSlope = nRawCoPAPRight(nInt + 2) - nRawCoPAPRight(nInt + 1)
                    nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPAPRight(n + 1) - (1 - nFract) * tempSlope
                    tempSlope = nRawCoPMLRight(nInt + 2) - nRawCoPMLRight(nInt + 1)
                    nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) = nRawCoPMLRight(n + 1) - (1 - nFract) * tempSlope
                End If
            Else 'If this is the last frame of the sample .
                nCoP_AP_AllFrames_Left(nNumberOfStrides, 100) = nCoP_AP_AllFrames_Left(nNumberOfStrides, 0)
                nCoP_AP_AllFrames_Right(nNumberOfStrides, 100) = nCoP_AP_AllFrames_Right(nNumberOfStrides, 0)
                nCoP_ML_AllFrames_Left(nNumberOfStrides, 100) = nCoP_ML_AllFrames_Left(nNumberOfStrides, 0)
                nCoP_ML_AllFrames_Right(nNumberOfStrides, 100) = nCoP_ML_AllFrames_Right(nNumberOfStrides, 0)
            End If
        End If

27:     'STEP 32:  Fill in whether each percent of each stride has zero, 1 or 2 feet on the ground.
        lblProgressBar.Text = "Step 27: Determining Single, Double or Float Phase of Gait for Each stride."
        ProgressBar1.Value = 27
        Dim nNumberOfFeetOnGround(nNumberOfStrides, 100)
        For Me.i = 1 To nNumberOfStrides
            For Me.j = 0 To 100
                If nCoP_AP_AllFrames_Left(i, j) = 0 And nCoP_AP_AllFrames_Right(i, j) = 0 Then
                    nNumberOfFeetOnGround(i, j) = 0
                ElseIf nCoP_AP_AllFrames_Left(i, j) > 0 And nCoP_AP_AllFrames_Right(i, j) = 0 Then
                    nNumberOfFeetOnGround(i, j) = 1
                ElseIf nCoP_AP_AllFrames_Left(i, j) = 0 And nCoP_AP_AllFrames_Right(i, j) > 0 Then
                    nNumberOfFeetOnGround(i, j) = 1
                ElseIf nCoP_AP_AllFrames_Left(i, j) > 0 And nCoP_AP_AllFrames_Right(i, j) > 0 Then
                    nNumberOfFeetOnGround(i, j) = 2
                End If
            Next j
        Next i

28:     'STEP 28: Average out the frames to get the AverageData Force and CoP locations and the Average Sensel Force. We will be taking the Average of the 2 or 3values around the median values
        lblProgressBar.Text = "Step 28: Calculate the Force for an Average Gait Cycle"
        ProgressBar1.Value = 28
        Dim tempAvg As New Collection
        Dim nNumberToAverage As Short
        Dim nItemToRemove As Short 'This becomes the item number to delete
        Dim tempMax, tempMin As Double
        If nNumberOfStrides < 4 Then
            nNumberToAverage = nNumberOfStrides
        ElseIf nNumberOfStrides / 2 - Int(nNumberOfStrides / 2) = 0 Then
            nNumberToAverage = 2
        Else
            nNumberToAverage = 3
        End If
        For Me.j = 0 To 99
            'Step 28A - First do the AverageData.LeftForce First.
            For Me.i = 1 To nNumberOfStrides
                tempAvg.Add(arAllLeftForces(i, j)) 'add the items for that percent of the gait cycle to the collection called tempAvg.
            Next i
            For Me.i = tempAvg.Count To 1 Step -1 'remove all the zero values
                If tempAvg.Item(i) = 0 Then
                    tempAvg.Remove(i)
                End If
            Next
            If tempAvg.Count < 0.5 * nNumberOfStrides Then 'If more than half of the items are zero, then set to zero.
                AverageData(j).nLForce = 0
            Else
                Do Until tempAvg.Count <= 3  'If you have too may things in the collection, you have to get rid of all the extra items.
                    tempMax = tempAvg.Item(1)
                    nItemToRemove = 1 'Set the default item to remove at the first item.
                    For Me.k = 1 To tempAvg.Count 'Find the largest item in the collection
                        If tempMax < tempAvg.Item(k) Then
                            tempMax = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove) 'Remove the largest value
                    tempMin = tempAvg.Item(1)
                    nItemToRemove = 1 'the default item to remove
                    For Me.k = 1 To tempAvg.Count
                        If tempMin > tempAvg.Item(k) Then
                            tempMin = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove) 'Remove the smallest item.
                Loop
                For Me.k = 1 To tempAvg.Count 'Do the actual averaging
                    AverageData(j).nLForce = AverageData(j).nLForce + tempAvg(k)
                Next k
                AverageData(j).nLForce = AverageData(j).nLForce / tempAvg.Count
            End If
            Do Until tempAvg.Count = 0 'This loop finishes removing all items from the collection.
                tempAvg.Remove(1)
            Loop
            'Step 28B: Next Average the Right Force for the "j" percent
            For Me.i = 1 To nNumberOfStrides 'Add all the items for this percent of the gait cycle to the collection
                tempAvg.Add(arAllRightForces(i, j))
            Next i
            For Me.i = tempAvg.Count To 1 Step -1 'Remove all the zero values.
                If tempAvg.Item(i) <= 0 Then
                    tempAvg.Remove(i)
                End If
            Next i
            If tempAvg.Count < 0.5 * nNumberOfStrides Then 'If you have less than half of the number of strides left to average, zero out the force.
                AverageData(j).nRForce = 0
            Else
                Do Until tempAvg.Count <= 3 'If you have too may things in the collection, you have to get rid of them.
                    tempMax = tempAvg.Item(1)
                    nItemToRemove = 1 'The default item to remove is #1
                    For Me.k = 1 To tempAvg.Count
                        If tempMax < tempAvg.Item(k) Then
                            tempMax = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove) 'Remove the largest value
                    tempMin = tempAvg.Item(1)
                    nItemToRemove = 1 'the default item is item #1
                    For Me.k = 1 To tempAvg.Count
                        If tempMin > tempAvg.Item(k) Then
                            tempMin = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove) 'Remove the smallest value
                Loop
                For Me.k = 1 To tempAvg.Count
                    AverageData(j).nRForce = AverageData(j).nRForce + tempAvg(k)
                Next k
                If tempAvg.Count <> 0 Then AverageData(j).nRForce = AverageData(j).nRForce / tempAvg.Count
            End If
            Do Until tempAvg.Count = 0 'This loop reduces the number of items back to zero for step C
                tempAvg.Remove(1)
            Loop
        Next j

        For Me.j = 0 To 99 'Steps 32C, D, E, F get the Average CoP Values
            Dim bCalc As Boolean
            'Step 28C - Do the CoP AP Left average
            If Me.j = 0 Or Me.j = 99 Or AverageData(j).nLForce <> 0 Then
                bCalc = True
            ElseIf (Me.j <> 0 And Me.j <> 99 And AverageData(j - 1).nLForce <> 0) Or (Me.j <> 0 And Me.j <> 99 And AverageData(j + 1).nLForce <> 0) Then
                bCalc = True
            ElseIf Me.j > 0 And Me.j < 99 And AverageData(j).nLForce = 0 And ((AverageData(j - 1).nLForce = 0 And AverageData(j + 1).nLForce = 0) Or (AverageData(j - 1).nLForce = 0 And AverageData(j + 1).nLForce > 0) Or (AverageData(j - 1).nLForce > 0 And AverageData(j + 1).nLForce = 0)) Then
                AverageData(j).nCoP_AP_L = 0
            End If
            If bCalc = True Then
                For Me.i = 1 To nNumberOfStrides 'Add all the items for the "j" percent of gait.
                    tempAvg.Add(nCoP_AP_AllFrames_Left(i, j))
                Next i
                For Me.i = tempAvg.Count To 1 Step -1 'remove all zero numbers
                    If tempAvg.Item(i) <= 0 Then
                        tempAvg.Remove(i)
                    End If
                Next i
                If tempAvg.Count < 0.5 * nNumberOfStrides Then
                    AverageData(j).nCoP_AP_L = 0
                Else
                    Do Until tempAvg.Count <= 3 'If you have too may things in the collection, you have to get rid of them, you will only average 2 or 3.
                        tempMax = tempAvg.Item(1)
                        nItemToRemove = 1 'Set the default number to remove at the first item.
                        For Me.k = 1 To tempAvg.Count
                            If tempMax < tempAvg.Item(k) Then
                                tempMax = tempAvg.Item(k)
                                nItemToRemove = k
                            End If
                        Next k
                        tempAvg.Remove(nItemToRemove)
                        tempMin = tempAvg.Item(1)
                        nItemToRemove = 1 'Reset the default item number to remove at the first item.
                        For Me.k = 1 To tempAvg.Count
                            If tempMin > tempAvg.Item(k) Then
                                tempMin = tempAvg.Item(k)
                                nItemToRemove = k
                            End If
                        Next k
                        tempAvg.Remove(nItemToRemove)
                    Loop
                    For Me.k = 1 To tempAvg.Count 'Do the actual averaging
                        AverageData(j).nCoP_AP_L = AverageData(j).nCoP_AP_L + tempAvg(k)
                    Next k
                    AverageData(j).nCoP_AP_L = AverageData(j).nCoP_AP_L / tempAvg.Count
                    bCalc = False
                End If
                Do Until tempAvg.Count = 0
                    tempAvg.Remove(1)
                Loop
            End If
            'Step 28D Average out the CoP AP Right Average 
            If Me.j = 0 Or Me.j = 99 Or AverageData(j).nRForce <> 0 Then
                bCalc = True
            ElseIf AverageData(j - 1).nRForce <> 0 Or AverageData(j + 1).nRForce <> 0 Then
                bCalc = True
            Else
                AverageData(j).nCoP_AP_R = 0
            End If
            If bCalc = True Then
                For Me.i = 1 To nNumberOfStrides 'Load up the collection of items to average.
                    tempAvg.Add(nCoP_AP_AllFrames_Right(i, j))
                Next i
                For Me.i = tempAvg.Count To 1 Step -1 'remove all zero numbers
                    If tempAvg.Item(i) <= 0 Then
                        tempAvg.Remove(i)
                    End If
                Next i
                If tempAvg.Count < 0.5 * nNumberOfStrides Then
                    AverageData(j).nCoP_AP_R = 0
                Else
                    Do Until tempAvg.Count <= 3 'If you have too may things in the collection, you have to get rid of them.
                        tempMax = tempAvg.Item(1)
                        nItemToRemove = 1 'Set the default item to remove at #1.
                        For Me.k = 1 To tempAvg.Count
                            If tempMax < tempAvg.Item(k) Then
                                tempMax = tempAvg.Item(k)
                                nItemToRemove = k
                            End If
                        Next k
                        tempAvg.Remove(nItemToRemove)
                        tempMin = tempAvg.Item(1) 'Make the first item the default minimum
                        nItemToRemove = 1 'Set the default item to remove back to #1 for removing the minimum value
                        For Me.k = 1 To tempAvg.Count
                            If tempMin > tempAvg.Item(k) Then
                                tempMin = tempAvg.Item(k)
                                nItemToRemove = k
                            End If
                        Next k
                        tempAvg.Remove(nItemToRemove)
                    Loop
                    For Me.k = 1 To tempAvg.Count
                        AverageData(j).nCoP_AP_R = AverageData(j).nCoP_AP_R + tempAvg(k)
                    Next k
                    AverageData(j).nCoP_AP_R = AverageData(j).nCoP_AP_R / tempAvg.Count
                    bCalc = False
                End If
                Do Until tempAvg.Count = 0 'Remove all of the remaining values in the tempAvg collection
                    tempAvg.Remove(1)
                Loop
            End If
            'Step 28E Average the CoP ML Left
            If AverageData(j).nCoP_AP_L = 0 Then
                AverageData(j).nCoP_ML_L = 0
            Else
                For Me.i = 1 To nNumberOfStrides 'Load up the collection of items to average for this percent of the gait cycle.
                    tempAvg.Add(nCoP_ML_AllFrames_Left(i, j))
                Next i
                Do Until tempAvg.Count <= 3 'If you have too may things in the collection, you have to get rid of them.
                    tempMax = tempAvg.Item(1)
                    nItemToRemove = 1 'Set the default item to remove to #1
                    For Me.k = 1 To tempAvg.Count
                        If tempMax < tempAvg.Item(k) Then
                            tempMax = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove)
                    tempMin = tempAvg.Item(1)
                    nItemToRemove = 1 'Set the default item to remove to #1
                    For Me.k = 1 To tempAvg.Count
                        If tempMin > tempAvg.Item(k) Then
                            tempMin = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove)
                Loop
                For Me.k = 1 To tempAvg.Count
                    AverageData(j).nCoP_ML_L = AverageData(j).nCoP_ML_L + tempAvg(k)
                Next k
                AverageData(j).nCoP_ML_L = AverageData(j).nCoP_ML_L / tempAvg.Count
            End If
            Do Until tempAvg.Count = 0 'Remove all of the items in the collection.
                tempAvg.Remove(1)
            Loop
            'Step 28F - Do the CoP ML Right
            If AverageData(j).nCoP_AP_R = 0 Then
                AverageData(j).nCoP_ML_R = 0
            Else
                For Me.i = 1 To nNumberOfStrides
                    tempAvg.Add(nCoP_ML_AllFrames_Right(i, j))
                Next i
                Do Until tempAvg.Count <= 3 'If you have too may things in the collection, you have to get rid of them.
                    tempMax = tempAvg.Item(1)
                    nItemToRemove = 1 'Set the default number to remove at item #1.
                    For Me.k = 1 To tempAvg.Count
                        If tempMax < tempAvg.Item(k) Then
                            tempMax = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove) 'Take away the biggest value.
                    tempMin = tempAvg.Item(1)
                    nItemToRemove = 1 'Reset the default item to remove at item #1.
                    For Me.k = 1 To tempAvg.Count
                        If tempMin > tempAvg.Item(k) Then
                            tempMin = tempAvg.Item(k)
                            nItemToRemove = k
                        End If
                    Next k
                    tempAvg.Remove(nItemToRemove)
                Loop
                For Me.k = 1 To tempAvg.Count
                    AverageData(j).nCoP_ML_R = AverageData(j).nCoP_ML_R + tempAvg(k)
                Next k
                AverageData(j).nCoP_ML_R = AverageData(j).nCoP_ML_R / tempAvg.Count
            End If
            Do Until tempAvg.Count = 0 'Remove all the remaining items from the averaging collection.
                tempAvg.Remove(1)
            Loop
            'STEP 28G: Now do the same thing for all the sensel values on the Left Foot
            If AverageData(j).nLForce <> 0 Then 'Only do this for stance phase gait percentages
                For Me.r = 0 To 59
                    For Me.s = 0 To 20
                        For Me.i = 1 To nNumberOfStrides
                            tempAvg.Add(arAllSenselForcesLeft(i, j, r, s))  'Add to the averaging array the value for that sensel at that percent of the gait cycle
                        Next i
                        For Me.i = tempAvg.Count To 1 Step -1
                            If tempAvg.Item(i) = 0 Then tempAvg.Remove(i) 'remove all the zero values
                        Next i
                        If tempAvg.Count < 0.5 * nNumberOfStrides Then 'If most of the values in the array were zero, then just set the sensel average to zero.
                            arAvgLeftSenselForce(i, r, s) = 0
                        Else
                            Do Until tempAvg.Count <= 3
                                tempMax = tempAvg(1)
                                nItemToRemove = 1 'This is the default item number which will be removed as the maximum value
                                For Me.k = 1 To tempAvg.Count 'This for loop finds the largest value to remove, and identifies it's number in the collection.
                                    If tempMax < tempAvg.Item(k) Then
                                        tempMax = tempAvg.Item(k)
                                        nItemToRemove = k
                                    End If
                                Next k
                                tempAvg.Remove(nItemToRemove) 'Removes the largest value
                                tempMin = tempAvg.Item(1)
                                nItemToRemove = 1 'again set the default minimum item as item #1
                                For Me.k = 1 To tempAvg.Count
                                    If tempMin > tempAvg.Item(k) Then
                                        tempMin = tempAvg.Item(k)
                                        nItemToRemove = k
                                    End If
                                Next k
                                tempAvg.Remove(nItemToRemove) 'This removes the smallest value from the array.
                            Loop
                            For Me.k = 1 To tempAvg.Count 'Now you are going to do the actual averaging
                                arAvgLeftSenselForce(i, r, s) = arAvgLeftSenselForce(i, r, s) + tempAvg(k)
                            Next k
                            arAvgLeftSenselForce(i, r, s) = arAvgLeftSenselForce(i, r, s) / tempAvg.Count 'This is the actual sum divided by the number of items.
                        End If
                        Do Until tempAvg.Count = 0 'This loop removes all the remaining items in the tempAvg collection
                            tempAvg.Remove(1)
                        Loop
                    Next s
                Next r
            End If
            'STEP 28H: 'Average each sensel for each percent of the gait cycle for the right foot
            If AverageData(j).nRForce <> 0 Then 'This loop only executes for those strides where the average data is not zero.
                For Me.r = 0 To 59
                    For Me.s = 0 To 20
                        For Me.i = 1 To nNumberOfStrides
                            tempAvg.Add(arAllSenselForcesRight(i, j, r, s))
                        Next i
                        For Me.i = tempAvg.Count To 1 Step -1
                            If tempAvg.Item(i) = 0 Then tempAvg.Remove(i) 'remove the zero values
                        Next i
                        If tempAvg.Count < 0.5 * nNumberOfStrides Then 'If most of the values in the collection were zero, then zero out the value for that sensel for the average.
                            arAvgRightSenselForce(i, r, s) = 0
                        Else
                            Do Until tempAvg.Count <= 3
                                tempMax = tempAvg(1)
                                nItemToRemove = 1 'The default number which will be removed as the maximum
                                For Me.k = 1 To tempAvg.Count 'This loop finds the maximum value in the collection to remove and its number in the collection
                                    If tempMax < tempAvg.Item(k) Then
                                        tempMax = tempAvg.Item(k)
                                        nItemToRemove = k
                                    End If
                                Next k
                                tempAvg.Remove(nItemToRemove) 'Removes the largest value from the collection
                                tempMin = tempAvg(1) 'Starts looking for the minimum value
                                nItemToRemove = 1
                                For Me.k = 1 To tempAvg.Count
                                    If tempMin > tempAvg.Item(k) Then
                                        tempMin = tempAvg.Item(k)
                                        nItemToRemove = k
                                    End If
                                Next k
                                tempAvg.Remove(nItemToRemove) 'This removes the smallest value from the array.
                            Loop
                            For Me.k = 1 To tempAvg.Count 'Now you have the 2 or 3 values around the median, you are going to average those for the final value
                                arAvgRightSenselForce(i, r, s) = arAvgRightSenselForce(i, r, s) + tempAvg(k)
                            Next k
                            arAvgRightSenselForce(i, r, s) = arAvgRightSenselForce(i, r, s) / tempAvg.Count 'the actual average for that sensel
                        End If
                        Do Until tempAvg.Count = 0
                            tempAvg.Remove(1)
                        Loop
                    Next s
                Next r
            End If
        Next j

        'STEP 28I: 'Set the 100% value to the 0% value.
        AverageData(100).nLForce = AverageData(0).nLForce
        AverageData(100).nRForce = AverageData(0).nRForce
        AverageData(100).nCoP_AP_L = AverageData(0).nCoP_AP_L
        AverageData(100).nCoP_AP_R = AverageData(0).nCoP_AP_R
        AverageData(100).nCoP_ML_L = AverageData(0).nCoP_ML_L
        AverageData(100).nCoP_ML_R = AverageData(0).nCoP_ML_R

29:     'STEP 29: Smooth Out the CoP Data
        lblProgressBar.Text = "Step 29: Smoothing Out the Average CoP Data"
        ProgressBar1.Value = 29
        For Me.j = 1 To 3 'The smoothing operation will be run three times
            For Me.i = 0 To 100
                tempSmoothingArray(i) = AverageData(i).nCoP_AP_L
            Next i
            subSmoothDataWithButterworthFilter() 'Smooth out the data
            For Me.i = 0 To 100
                AverageData(i).nCoP_AP_L = tempSmoothingArray(i)
            Next i
            For Me.i = 0 To 100
                tempSmoothingArray(i) = AverageData(i).nCoP_AP_R
            Next i
            subSmoothDataWithButterworthFilter()
            For Me.i = 0 To 100
                AverageData(i).nCoP_AP_R = tempSmoothingArray(i)
            Next
            For Me.i = 0 To 100
                tempSmoothingArray(i) = AverageData(i).nCoP_ML_L
            Next i
            subSmoothDataWithButterworthFilter()
            For Me.i = 0 To 100
                AverageData(i).nCoP_ML_L = tempSmoothingArray(i)
            Next
            For Me.i = 0 To 100
                tempSmoothingArray(i) = AverageData(i).nCoP_ML_R
            Next i
            subSmoothDataWithButterworthFilter()
            For Me.i = 0 To 100
                AverageData(i).nCoP_ML_R = tempSmoothingArray(i)
            Next i
        Next j

30:     'STEP 30: Reset the most posterior CoP sensel that is activated as the 0 sensel row and all rows forward measured from that in a positive direction 
        lblProgressBar.Text = "Step 30: Resetting the CoP to match the size of the foot."
        ProgressBar1.Value = 30
        For Me.i = 0 To 100 'Set the CoP colums so that 0 is the lateral-most column and as the pressure moves medially it gets larger.
            If AverageData(i).nCoP_AP_L <> 0 Then arCoPLoc_AP_L(i) = 59 - AverageData(i).nCoP_AP_L - (59 - nFootLengthLeft(1))
            If AverageData(i).nCoP_AP_R <> 0 Then arCoPLoc_AP_R(i) = 59 - AverageData(i).nCoP_AP_R - (59 - nFootLengthRight(1))
            If AverageData(i).nCoP_ML_R <> 0 Then arCoPLoc_ML_R(i) = 20 - AverageData(i).nCoP_ML_R - (20 - nFootWidthRight(1)) 'Reverses the data points so that 0 is most lateral
            If AverageData(i).nCoP_ML_R <> 0 Then arCoPLoc_ML_R(i) = arCoPLoc_ML_R(i) - nFootWidthRight(0)
            If AverageData(i).nCoP_ML_L <> 0 Then arCoPLoc_ML_L(i) = AverageData(i).nCoP_ML_L - nFootWidthLeft(0)
        Next i
        FootLengthLeft = nFootLengthLeft(1) - nFootLengthLeft(0)
        FootLengthRight = nFootLengthRight(1) - nFootLengthRight(0)
        FootWidthLeft = nFootWidthLeft(1) - nFootWidthLeft(0)
        FootWidthRight = nFootWidthRight(1) - nFootWidthRight(0)
        Dim nFootCenterLeft As Double = (nFootWidthLeft(1) - nFootWidthRight(0)) / 2
        Dim nFootCenterRight As Double = (nFootWidthRight(1) - nFootWidthRight(0)) / 2
        For Me.i = 0 To 100 'Reset the coP in the ML direction according to the center of the foot.  Medial is +
            arCoPLoc_ML_L(i) = arCoPLoc_ML_L(i) - nFootCenterLeft
            arCoPLoc_ML_R(i) = arCoPLoc_ML_R(i) - nFootCenterRight
        Next i

31:     'STEP 31: Convert the allForces arrays to actual pounds
        lblProgressBar.Text = "Step 31: convert all forces to lbs."
        ProgressBar1.Value = 31
        For Me.i = 1 To nNumberOfStrides
            For Me.j = 0 To 100
                arAllLeftForces(i, j) = arAllLeftForces(i, j) * nFinalForceMultiplierLeft
                arAllRightForces(i, j) = arAllRightForces(i, j) * nFinalForceMultiplierRight
                arAllTotalForces(i, j) = arAllLeftForces(i, j) + arAllRightForces(i, j)
            Next j
        Next i
        'STEP 31A:  Convert the AvgSensel array to actual pounds
        For Me.j = 0 To 100
            If AverageData(j).nLForce <> 0 Then
                For Me.r = 0 To 59
                    For Me.s = 0 To 20
                        If arAvgLeftSenselForce(j, r, s) <> 0 Then arAvgLeftSenselForce(j, r, s) = arAvgLeftSenselForce(j, r, s) * nFinalForceMultiplierLeft
                    Next s
                Next r
            End If
            If AverageData(j).nRForce <> 0 Then
                For Me.r = 0 To 59
                    For Me.s = 0 To 20
                        If arAvgRightSenselForce(j, r, s) <> 0 Then arAvgRightSenselForce(j, r, s) = arAvgRightSenselForce(j, r, s) * nFinalForceMultiplierRight
                    Next s
                Next r
            End If
        Next j

32:     'STEP 32: 'Find the Average Weight
        lblProgressBar.Text = "Step 32: Calculating the Average Weight"
        ProgressBar1.Value = 32
        For Me.i = 0 To 99
            AverageData(i).nLForce = AverageData(i).nLForce * nFinalForceMultiplierLeft
            AverageData(i).nRForce = AverageData(i).nRForce * nFinalForceMultiplierRight
            AverageData(i).nTotalForce = AverageData(i).nLForce + AverageData(i).nRForce
            AverageWeight = AverageWeight + AverageData(i).nTotalForce
        Next i
        AverageData(100).nLForce = AverageData(0).nLForce
        AverageData(100).nRForce = AverageData(0).nRForce
        AverageData(100).nTotalForce = AverageData(0).nTotalForce
        AverageWeight = AverageWeight / 100


33:     'STEP 33: Populate the Average Time for each Percent
        lblProgressBar.Text = "Step 33: Calculating the AverageTime for each Percent of the Gait Cycle"
        ProgressBar1.Value = 33
        Dim nFramesPerStride(nNumberOfStrides) As Double
        For Me.i = 1 To nNumberOfStrides
            nFramesPerStride(i) = nBeginAndEndOfStrides(i) - nBeginAndEndOfStrides(i - 1)
        Next i
        Dim nAverageFramesPerStride As Single
        nAverageFramesPerStride = 0
        For Me.i = 1 To nNumberOfStrides
            nAverageFramesPerStride = nAverageFramesPerStride + nFramesPerStride(i)
        Next i
        nAverageFramesPerStride = nAverageFramesPerStride / nNumberOfStrides
        Dim nTimePerPercent As Double
        nTimePerPercent = 0.01 * nAverageFramesPerStride * nTimePerFrame 'n 'InterpolatedFrame(nNumberOfStrides, 100) - nInterpolatedFrame(1, 0) 'Find out the total number of frames in the sample
        For Me.i = 0 To 100
            AverageData(i).nTime = i * nTimePerPercent
        Next i

34:     'STEP 34:  Calculate the average cadence in steps per minute
        Cadence = 120 / AverageData(100).nTime

35:     'STEP 41: Populate the AllTimes array, which is two dimensions, the first is the step number and the second is the percent number
        lblProgressBar.Text = "Step 35: Populating the AllTimes Array"
        ProgressBar1.Value = 35
        ReDim arAllTimes(nNumberOfStrides, 100)
        For Me.i = 1 To nNumberOfStrides
            For Me.j = 0 To 100
                arAllTimes(i, j) = nTimePerFrame * (nInterpolatedFrame(i, j) - nInterpolatedFrame(1, 0))
            Next j
        Next i

36:     'STEP 36: Put the force in terms of Body weight
        lblProgressBar.Text = "Step 36: Expressing Force in Terms of Body Weight"
        ProgressBar1.Value = 36
        For Me.i = 0 To 100
            arTotalForce(i) = AverageData(i).nTotalForce
            arLeftForce(i) = AverageData(i).nRForce
            arRightForce(i) = AverageData(i).nRForce
            arBodyWeight(i) = AverageData(i).nTotalForce - AverageWeight
            arBodyWeightPct(i) = AverageData(i).nTotalForce / AverageWeight
        Next i

37:     'STEP 37:  'Now integrate the body weight to get the velocity curve
        lblProgressBar.Text = "Step 37: Calculating the Velocity of the CoM"
        ProgressBar1.Value = 37
        BodyMass = AverageWeight / 32 'This gives you the body mass in slugs.  Divide the weight by body mass to get the acceleration
        Dim nAverageVelocity As Double
        arCoMVelocity(0) = 0
        For Me.i = 1 To 100 'Use the trapezoidal rule to get the area under each part of the cycle.
            arCoMVelocity(i) = arCoMVelocity(i - 1) + AverageData(1).nTime * (arBodyWeight(i - 1) + arBodyWeight(i)) / (2 * BodyMass)
        Next i
        arCoMVelocity(0) = arCoMVelocity(100)
        For Me.i = 0 To 99
            nAverageVelocity = nAverageVelocity + arCoMVelocity(i)
        Next i
        nAverageVelocity = nAverageVelocity / 100
        For Me.i = 0 To 100
            arCoMVelocity(i) = arCoMVelocity(i) - nAverageVelocity
        Next i

38:     'STEP 38: Integrate the Velocity curve to Calculate the Vertical Displacement of the CoM
        lblProgressBar.Text = "Step 38: Calculating the Vertical Displacement of the CoM"
        ProgressBar1.Value = 38
        Dim nAverageDisplacement As Double
        arDisplacement(0) = 0
        For Me.i = 1 To 100
            arDisplacement(i) = arDisplacement(i - 1) + AverageData(1).nTime * (arCoMVelocity(i - 1) + arCoMVelocity(i)) / 2
        Next i
        arDisplacement(0) = arDisplacement(100)
        For Me.i = 0 To 99
            nAverageDisplacement = nAverageDisplacement + arDisplacement(i)
        Next i
        nAverageDisplacement = nAverageDisplacement / 100
        For Me.i = 0 To 100
            arDisplacement(i) = arDisplacement(i) - nAverageDisplacement
        Next i

39:     'STEP 39: 'Fill in the Phase of Gait number in the AverageData.
        lblProgressBar.Text = "Step 39: Determining the Phase of Gait at Each Percentage Point."
        ProgressBar1.Value = 39
        If bBeginLeftOrRight = False Then
            If bWalkingOrRunning = False Then
                AverageData(0).nPhase = con_L_Double_Support
            Else
                AverageData(0).nPhase = con_L_Single_Support
            End If
        Else
            If bWalkingOrRunning = False Then 'If you are walking and the right foot starts, then 0% is the start of double support
                AverageData(0).nPhase = con_R_Double_Support
            Else 'If you are running and starting on the right foot, then 0% is the start of single support
                AverageData(0).nPhase = con_R_Single_Support
            End If
        End If
        AverageData(100).nPhase = AverageData(0).nPhase '100% is the same as 0%
        For Me.i = 1 To 99
            If AverageData(i).nLForce > 0 And AverageData(i).nRForce > 0 Then 'If both forces are >0
                AverageData(i).nPhase = AverageData(i - 1).nPhase
            ElseIf AverageData(i).nLForce = 0 And AverageData(i).nRForce = 0 Then 'If both are zero you are running
                If AverageData(i - 1).nLForce > 0 Or AverageData(i + 1).nLForce > 0 Then
                    AverageData(i).nPhase = con_L_Single_Support
                ElseIf AverageData(i - 1).nRForce > 0 Or AverageData(i + 1).nRForce > 0 Then
                    AverageData(i).nPhase = con_R_Single_Support
                ElseIf AverageData(i - 1).nPhase = con_R_Single_Support Then 'This would be the beginning of Left Float phase
                    AverageData(i).nPhase = con_L_Float
                ElseIf AverageData(i - 1).nPhase = con_L_Single_Support Then
                    AverageData(i).nPhase = con_R_Float
                Else
                    AverageData(i).nPhase = AverageData(i - 1).nPhase
                End If
            ElseIf AverageData(i).nLForce > 0 And AverageData(i).nRForce = 0 Then
                If AverageData(i - 1).nRForce > 0 Then 'This is the end of the Left Double Support
                    AverageData(i).nPhase = con_L_Double_Support
                ElseIf AverageData(i + 1).nRForce > 0 Then 'This is the beginning of the Right Double Support
                    AverageData(i).nPhase = con_R_Double_Support
                Else
                    AverageData(i).nPhase = con_L_Single_Support
                End If
            ElseIf AverageData(i).nLForce = 0 And AverageData(i).nRForce > 0 Then
                If AverageData(i - 1).nLForce > 0 Then 'This is the end of the Right Double Support
                    AverageData(i).nPhase = con_R_Double_Support
                ElseIf AverageData(i + 1).nLForce > 0 Then 'This is the beginning of the Left Double Support
                    AverageData(i).nPhase = con_L_Double_Support
                Else
                    AverageData(i).nPhase = con_R_Single_Support
                End If
            End If
        Next i

40:     'STEP 40: Calculate the Power by Multiplying the Force x Velocity
        lblProgressBar.Text = "Step 40: Calculating the Power of the Ground on the CoM"
        ProgressBar1.Value = 40
        For Me.i = 0 To 99
            arPower(i) = arBodyWeight(i) * arCoMVelocity(i)
        Next i
        arPower(100) = arPower(0)

41:     'STEP 41: Calculate the Work done by the ground by muliplying the Force x the Change in Distance
        lblProgressBar.Text = "Step 41: Calculating the Work done by the Ground on the CoM"
        ProgressBar1.Value = 41
        Dim nAvgWork As Double
        For Me.i = 1 To 100
            arWork(i) = arWork(i - 1) + (arBodyWeight(i - 1) + arBodyWeight(i) / 2) * (arDisplacement(i) - arDisplacement(i - 1))
        Next i
        For Me.i = 1 To 100
            nAvgWork = nAvgWork + arWork(i)
        Next
        nAvgWork = 0.01 * nAvgWork
        For Me.i = 1 To 100
            arWork(i) = arWork(i) - nAvgWork
        Next
        arWork(0) = arWork(100)

42:     'STEP 42: Calculate the Harmonic Values for Displacement
        lblProgressBar.Text = "Step 42: Calculating the Harmonic Values for the Average Displacement vs. Time"
        ProgressBar1.Value = 42
        For Me.i = 1 To 12
            arHarmonicValuesDisplacement(i, 0) = 0 'This initiates the first harmonic cosine value as zero
            arHarmonicValuesDisplacement(i, 1) = 0 'This initiates the first harmonic sine value to zero
            For Me.j = 0 To 99
                arHarmonicValuesDisplacement(i, con_Cos) = arHarmonicValuesDisplacement(i, con_Cos) + arDisplacement(j) * System.Math.Cos(i * j * Math.PI / 50)
                arHarmonicValuesDisplacement(i, con_Sin) = arHarmonicValuesDisplacement(i, con_Sin) + arDisplacement(j) * System.Math.Sin(i * j * Math.PI / 50)
            Next j
            arHarmonicValuesDisplacement(i, con_Cos) = arHarmonicValuesDisplacement(i, con_Cos) / 50
            arHarmonicValuesDisplacement(i, con_Sin) = arHarmonicValuesDisplacement(i, con_Sin) / 50
            arHarmonicValuesDisplacement(i, con_Amp) = System.Math.Sqrt(arHarmonicValuesDisplacement(i, con_Cos) ^ 2 + arHarmonicValuesDisplacement(i, con_Sin) ^ 2)
        Next i

43:     'STEP 43: Calculate the Harmonic Values for Velocity
        lblProgressBar.Text = "Step 43: Calculating the Harmonic Values for the Average Velocity vs. Time"
        ProgressBar1.Value = 43
        For Me.i = 1 To 12
            arHarmonicValuesVelocity(i, 0) = 0 'This initiates the first harmonic cosine value as zero
            arHarmonicValuesVelocity(i, 1) = 0 'This initiates the first harmonic sine value to zero
            For Me.j = 0 To 99
                arHarmonicValuesVelocity(i, con_Cos) = arHarmonicValuesVelocity(i, con_Cos) + arCoMVelocity(j) * System.Math.Cos(i * j * Math.PI / 50)
                arHarmonicValuesVelocity(i, con_Sin) = arHarmonicValuesVelocity(i, con_Sin) + arCoMVelocity(j) * System.Math.Sin(i * j * Math.PI / 50)
            Next j
            arHarmonicValuesVelocity(i, con_Cos) = arHarmonicValuesVelocity(i, con_Cos) / 50
            arHarmonicValuesVelocity(i, con_Sin) = arHarmonicValuesVelocity(i, con_Sin) / 50
            arHarmonicValuesVelocity(i, con_Amp) = System.Math.Sqrt(arHarmonicValuesVelocity(i, con_Cos) ^ 2 + arHarmonicValuesVelocity(i, con_Sin) ^ 2)
        Next i

44:     'STEP 44: Calculate the Harmonic Values for Force
        lblProgressBar.Text = "Step 44: Calculating the Harmonic Values for the Average Force vs. Time"
        ProgressBar1.Value = 44
        For Me.i = 1 To 12
            arHarmonicValuesForce(i, con_Cos) = 0
            arHarmonicValuesForce(i, con_Sin) = 0
            For Me.j = 0 To 99
                arHarmonicValuesForce(i, con_Cos) = arHarmonicValuesForce(i, con_Cos) + arBodyWeight(j) * System.Math.Cos(i * j * Math.PI / 50)
                arHarmonicValuesForce(i, con_Sin) = arHarmonicValuesForce(i, con_Sin) + arBodyWeight(j) * System.Math.Sin(i * j * Math.PI / 50)
            Next j
            arHarmonicValuesForce(i, con_Cos) = arHarmonicValuesForce(i, con_Cos) / 50
            arHarmonicValuesForce(i, con_Sin) = arHarmonicValuesForce(i, con_Sin) / 50
            arHarmonicValuesForce(i, con_Amp) = System.Math.Sqrt(arHarmonicValuesForce(i, con_Cos) ^ 2 + arHarmonicValuesForce(i, con_Sin) ^ 2)
        Next i

45:     'STEP 45: Calculate the Harmonic Values for Power
        lblProgressBar.Text = "Step 45: Calculating the Harmonic Values for the Power vs. Time"
        ProgressBar1.Value = 45
        For Me.i = 1 To 12
            arHarmonicValuesPower(i, con_Cos) = 0
            arHarmonicValuesPower(i, con_Sin) = 0
            For Me.j = 0 To 99
                arHarmonicValuesPower(i, con_Cos) = arHarmonicValuesPower(i, con_Cos) + arPower(j) * System.Math.Cos(i * j * Math.PI / 50)
                arHarmonicValuesPower(i, con_Sin) = arHarmonicValuesPower(i, con_Sin) + arPower(j) * System.Math.Sin(i * j * Math.PI / 50)
            Next j
            arHarmonicValuesPower(i, con_Cos) = arHarmonicValuesPower(i, con_Cos) / 50
            arHarmonicValuesPower(i, con_Sin) = arHarmonicValuesPower(i, con_Sin) / 50
            arHarmonicValuesPower(i, con_Amp) = System.Math.Sqrt(arHarmonicValuesPower(i, con_Cos) ^ 2 + arHarmonicValuesPower(i, con_Sin) ^ 2)
        Next i

46:     'STEP 46: Calculate the Harmonic Values for Work
        lblProgressBar.Text = "Step 46: Calculate the Harmonic Values for the Work vs. Time"
        ProgressBar1.Value = 46
        For Me.i = 1 To 12
            arHarmonicValuesWork(i, con_Cos) = 0
            arHarmonicValuesWork(i, con_Sin) = 0
            For Me.j = 0 To 99
                arHarmonicValuesWork(i, con_Cos) = arHarmonicValuesWork(i, con_Cos) + arWork(j) * System.Math.Cos(i * j * Math.PI / 50)
                arHarmonicValuesWork(i, con_Sin) = arHarmonicValuesWork(i, con_Sin) + arWork(j) * System.Math.Sin(i * j * Math.PI / 50)
            Next j
            arHarmonicValuesWork(i, con_Cos) = arHarmonicValuesWork(i, con_Cos) / 50
            arHarmonicValuesWork(i, con_Sin) = arHarmonicValuesWork(i, con_Sin) / 50
            arHarmonicValuesWork(i, con_Amp) = System.Math.Sqrt(arHarmonicValuesWork(i, con_Cos) ^ 2 + arHarmonicValuesWork(i, con_Sin) ^ 2)
        Next i

47:     'STEP 47: Calculate the Spring Constants
        lblProgressBar.Text = "Step 47: Calculating the Spring Constant"
        ProgressBar1.Value = 47
        Dim nNatural_ω As Double 'This is the natural angular velocity of our spring.
        Dim nNaturalKValue As Double
        Dim nNaturalDisplacement As Double
        Dim nAdjustedDisplacement(100) As Double
        nNatural_ω = 2 * Math.PI * (Cadence / 60) 'The natural angular velocity in radians/second
        nNaturalKValue = nNatural_ω ^ 2 * BodyMass
        nNaturalDisplacement = -AverageWeight / nNaturalKValue
        If bWalkingOrRunning = False Then
            For Me.i = 0 To 100
                nAdjustedDisplacement(i) = arDisplacement(i) + nNaturalDisplacement
            Next i
            For Me.i = 0 To 100
                arSpringConstants(i) = -(AverageWeight + arBodyWeight(i)) / nAdjustedDisplacement(i)
            Next i

        ElseIf bWalkingOrRunning = True Then 'if you are RUNNING the spring constant is calculated very differently.
            For Me.i = 1 To 99
                If arTotalForce(i) > 0 Then
                    If arTotalForce(i - 1) > 0 And arTotalForce(i + 1) > 0 Then
                        If arTotalForce(i) > arTotalForce(i - 1) And arTotalForce(i) > arTotalForce(i + 1) Then
                            j = i  'Find the displacements at zero force before and after, and the average diplacement.
                            Do Until arTotalForce(j) = 0
                                j = j - 1
                            Loop
                            k = i
                            Do Until arTotalForce(k) = 0
                                k = k + 1
                            Loop
                            arSpringConstants(i) = -arTotalForce(i) / (arDisplacement(i) - ((arDisplacement(j) + arDisplacement(k) / 2)))
                        Else
                            arSpringConstants(i) = -(arTotalForce(i + 1) - arTotalForce(i - 1)) / (arDisplacement(i + 1) - arDisplacement(i - 1))
                        End If
                    ElseIf arTotalForce(i - 1) = 0 Then
                        arSpringConstants(i) = -(arTotalForce(i + 1) - arTotalForce(i)) / (arDisplacement(i + 1) - arDisplacement(i))
                    ElseIf arTotalForce(i + 1) = 0 Then
                        arSpringConstants(i) = -(arTotalForce(i) - arTotalForce(i - 1)) / (arDisplacement(i) - arDisplacement(i - 1))
                    End If
                End If
            Next

            Dim nZeroVelocityRunningPct(1, 1) As Double 'This variable defines the pct of gait when the zero velocity is reached.  It should be at the lowest point in stance and the highest point in NWB. The first variable is foot 1 (0) or foot 2 (1), the 2nd is lowes point (0) or highest point (1)
            For Me.j = 0 To 3
                Me.i = 1
                Do Until (arCoMVelocity(i - 1) < 0 And arCoMVelocity(i) > 0) Or (arCoMVelocity(i - 1) > 0 And arCoMVelocity(i) < 0) 'find the point where the velocity changes from negative to positive
                    i = i + 1
                Loop
                Select Case j
                    Case 0
                        nZeroVelocityRunningPct(0, 0) = i + arCoMVelocity(i - 1) / (arCoMVelocity(i) - arCoMVelocity(i - 1)) 'Low Point Percent for foot 1 (midstance)
                    Case 1
                        nZeroVelocityRunningPct(0, 1) = i + arCoMVelocity(i - 1) / (arCoMVelocity(i) - arCoMVelocity(i - 1)) 'High point percent for float, after foot 1
                    Case 2
                        nZeroVelocityRunningPct(1, 0) = i + arCoMVelocity(i - 1) / (arCoMVelocity(i) - arCoMVelocity(i - 1)) 'Low point for foot 2 in midstance
                    Case 3
                        nZeroVelocityRunningPct(1, 1) = i + arCoMVelocity(i - 1) / (arCoMVelocity(i) - arCoMVelocity(i - 1)) 'High point for for float after foot 2
                End Select

                'STEP 47B: Determine the cubic spline equation for the displacement around the minimal point
                Dim arCubicSplineFactors(3) As Double 'This is for y = ax^3 + b x^2 + cx +d, where a is the 0 and d is 3 number in the array.
                Dim arCubicInverseMatrix(3, 3) As Double 'This is the inverse matrix for multiplying the y matrix by to get the a-d spline factors
                arCubicInverseMatrix(0, 0) = -0.166666666666666
                arCubicInverseMatrix(0, 1) = 0.5
                arCubicInverseMatrix(0, 2) = -0.5
                arCubicInverseMatrix(0, 3) = -arCubicInverseMatrix(0, 0)
                arCubicInverseMatrix(1, 0) = 1.5
                arCubicInverseMatrix(1, 1) = -4
                arCubicInverseMatrix(1, 2) = 3.5
                arCubicInverseMatrix(1, 3) = -1
                arCubicInverseMatrix(2, 0) = -4.33333333333333
                arCubicInverseMatrix(2, 1) = 9.5
                arCubicInverseMatrix(2, 2) = -7
                arCubicInverseMatrix(2, 3) = 1.83333333333333
                arCubicInverseMatrix(3, 0) = 4
                arCubicInverseMatrix(3, 1) = -6
                arCubicInverseMatrix(3, 2) = 4
                arCubicInverseMatrix(3, 3) = -1

                'Step 47C: calculate the values a,b,c,&d for the cubic spline across the lowest point
                arCubicSplineFactors(0) = arCubicInverseMatrix(0, 0) * arDisplacement(i - 2) + arCubicInverseMatrix(0, 1) * arDisplacement(i - 1) + arCubicInverseMatrix(0, 2) * arDisplacement(i) + arCubicInverseMatrix(0, 3) * arDisplacement(i + 1)
                arCubicSplineFactors(1) = arCubicInverseMatrix(1, 0) * arDisplacement(i - 2) + arCubicInverseMatrix(1, 1) * arDisplacement(i - 1) + arCubicInverseMatrix(1, 2) * arDisplacement(i) + arCubicInverseMatrix(1, 3) * arDisplacement(i + 1)
                arCubicSplineFactors(2) = arCubicInverseMatrix(2, 0) * arDisplacement(i - 2) + arCubicInverseMatrix(2, 1) * arDisplacement(i - 1) + arCubicInverseMatrix(2, 2) * arDisplacement(i) + arCubicInverseMatrix(2, 3) * arDisplacement(i + 1)
                arCubicSplineFactors(3) = arCubicInverseMatrix(3, 0) * arDisplacement(i - 2) + arCubicInverseMatrix(3, 1) * arDisplacement(i - 1) + arCubicInverseMatrix(0, 2) * arDisplacement(i) + arCubicInverseMatrix(0, 3) * arDisplacement(i + 1)
                'Step 47D: the zero value of X is the solution for the quadratic equation of the derivative, which is 3ax^2 + 2bx + c = 0

                nZeroVelocityInStancePct(j) = (-2 * arCubicSplineFactors(1) + Math.Sqrt(4 * arCubicSplineFactors(1) ^ 2 - 4 * 3 * arCubicSplineFactors(0) * arCubicSplineFactors(2))) / (2 * 3 * arCubicSplineFactors(0))
                If 0 > nZeroVelocityInStancePct(j) Or 4 < nZeroVelocityInStancePct(j) Then 'This is the - possibility for the solving of the quadratic equation.
                    nZeroVelocityInStancePct(0) = (-2 * arCubicSplineFactors(1) - Math.Sqrt(4 * arCubicSplineFactors(1) ^ 2 - 4 * 3 * arCubicSplineFactors(0) * arCubicSplineFactors(2))) / (2 * 3 * arCubicSplineFactors(0))
                End If
                'Step 47E - the value of the displacement at zero velocity:
                nZeroVelocityInStancePct(j) = i - 3 + nZeroVelocityInStancePct(j)
                'Step 47F - find the spring constants for those values from the beginning of the stance to with 2 of the zero velocity. Calculate as the difference between n-2 and n+2
                If j = 0 Then
                    k = 2
                Else
                    k = nZeroVelocityInStancePct(0) + 2
                    Do Until arTotalForce(k) = 0 And arTotalForce(k + 1) > 0
                        k = k + 1
                    Loop
                End If
                Do Until nZeroVelocityInStancePct(j) - k < 2
                    arSpringConstants(k) = (arTotalForce(k + 2) - arTotalForce(k - 2)) / (arDisplacement(k + 2) - arDisplacement(k - 2))
                    k = k + 1
                Loop
                k = k + 4
                Do Until arTotalForce(k + 1) = 0
                    arSpringConstants(k) = (arTotalForce(k + 2) - arTotalForce(k - 2)) / (arDisplacement(k + 2) - arDisplacement(k - 2))
                    k = k + 1
                Loop
                'Step 47G:  Interpolate the 2 values on each side of the zero velocity with a cubic spline
                k = Int(nZeroVelocityInStancePct(j)) - 2
                arCubicSplineFactors(0) = arCubicInverseMatrix(0, 0) * arSpringConstants(k - 1) + arCubicInverseMatrix(0, 1) * arSpringConstants(k) + arCubicInverseMatrix(0, 2) * arSpringConstants(k + 5) + arCubicInverseMatrix(0, 3) * arSpringConstants(k + 6)
                arCubicSplineFactors(1) = arCubicInverseMatrix(1, 0) * arSpringConstants(k - 1) + arCubicInverseMatrix(1, 1) * arSpringConstants(k) + arCubicInverseMatrix(1, 2) * arSpringConstants(k + 5) + arCubicInverseMatrix(1, 3) * arSpringConstants(k + 6)
                arCubicSplineFactors(2) = arCubicInverseMatrix(2, 0) * arSpringConstants(k - 1) + arCubicInverseMatrix(2, 1) * arSpringConstants(k) + arCubicInverseMatrix(2, 2) * arSpringConstants(k + 5) + arCubicInverseMatrix(2, 3) * arSpringConstants(k + 6)
                arCubicSplineFactors(3) = arCubicInverseMatrix(3, 0) * arSpringConstants(k - 1) + arCubicInverseMatrix(3, 1) * arSpringConstants(k) + arCubicInverseMatrix(0, 2) * arSpringConstants(k + 5) + arCubicInverseMatrix(0, 3) * arSpringConstants(k + 6)

                If (j = 0 And bBeginLeftOrRight = False) Or (j = 1 And bBeginLeftOrRight = True) Then
                    nZeroVelocityInStancePct(0) = i - arCoMVelocity(i) / (arCoMVelocity(i) - arCoMVelocity(i - 1))
                ElseIf (j = 0 And bBeginLeftOrRight = True) Or (j = 1 And bBeginLeftOrRight = False) Then
                    nZeroVelocityInStancePct(1) = i - arCoMVelocity(i) / (arCoMVelocity(i) - arCoMVelocity(i - 1))
                End If
                If nZeroVelocityInStancePct(0) = 0 Or nZeroVelocityInStancePct(1) = 0 Then
                    i = i + 1
                End If
            Next j
            Dim nSpringVelocityContrib(100) As Double
            For Me.i = 1 To 100            'Find the velocity that the spring is contributing
                nSpringVelocityContrib(i) = arCoMVelocity(i) - (arCoMVelocity(i - 1) - 32 * nTimePerPercent)
            Next i
            Dim nFirstAndLastPct(1, 1) As Integer  'The first dimension is for the first or second half of the gait cycle.  the 2nd dimension is for begin and end percent.
            Me.k = 0
            For Me.i = 0 To 99
                If arTotalForce(i) = 0 And arTotalForce(i + 1) > 0 Then
                    If k = 0 Then
                        nFirstAndLastPct(0, 0) = i 'Determine the first pct of the 1st stride
                    Else
                        nFirstAndLastPct(1, 0) = i 'Determine the first pct of the 2nd step\
                    End If
                ElseIf arTotalForce(i) > 0 And arTotalForce(i + 1) = 0 Then
                    If k = 0 Then
                        nFirstAndLastPct(0, 1) = i + 1 'Determine the last pct of the 1st stride
                        k = 1
                    Else
                        nFirstAndLastPct(1, 1) = i + 1 'Determine the last pct of the 2nd stride
                    End If
                End If
                If nFirstAndLastPct(0, 0) <> 0 And nFirstAndLastPct(0, 1) <> 0 And nFirstAndLastPct(1, 0) <> 0 And nFirstAndLastPct(1, 1) <> 0 Then Exit For
            Next i
            For Me.j = 1 To 99
                If arTotalForce(j) <> 0 Then
                    If Math.Abs(j - nZeroVelocityInStancePct(0)) > 1 And Math.Abs(j - nZeroVelocityInStancePct(1)) > 1 Then
                        arSpringConstants(j) = (arTotalForce(j + 1) - arTotalForce(j - 1)) / (arDisplacement(j + 1) - arDisplacement(j - 1))
                    ElseIf (0 <= j - nZeroVelocityInStancePct(0) And j - nZeroVelocityInStancePct(0) <= 1) Or (0 <= j - nZeroVelocityInStancePct(1) And j - nZeroVelocityInStancePct(1) <= 1) Then
                        k = j + 1
                        Do Until arTotalForce(k) = 0 'Find the point when the force was zero at the end of the step
                            k = k + 1
                        Loop
                        arSpringConstants(j) = (arTotalForce(j)) / (arDisplacement(j) - arDisplacement(k))
                    ElseIf (0 <= nZeroVelocityInStancePct(0) - j And nZeroVelocityInStancePct(0) - j <= 1) Or (0 <= nZeroVelocityInStancePct(1) - j And nZeroVelocityInStancePct(1) - j <= 1) Then
                        k = j
                        Do Until arTotalForce(k) = 0 'Find the point when the force was zero at the beginning of the step
                            k = k - 1
                        Loop
                        arSpringConstants(j) = (arTotalForce(j)) / (arDisplacement(j) - arDisplacement(k))
                    End If
                Else
                    arSpringConstants(j) = 0
                End If
            Next j
        End If

48:     'STEP 48:  Calculate the Energy - Potential and Kinetic, for each phase of the gait cycle.
        lblProgressBar.Text = "Step 48: Calculate the Potential and Kinetic Energy of the CoM"
        ProgressBar1.Value = 48
        For Me.i = 0 To 100 'First calculate the Kinetic Energy
            arEnergy_Kinetic(i) = 0.5 * BodyMass * arCoMVelocity(i) ^ 2
        Next i
        If bWalkingOrRunning = False Then
            For Me.i = 0 To 100  'Calculate the Potential Energy for walking
                arEnergy_Potential(i) = 0.5 * arSpringConstants(i) * arDisplacement(i) ^ 2
            Next i
            For Me.i = 0 To 100
                arEnergy_Total(i) = arEnergy_Kinetic(i) + arEnergy_Potential(i)
            Next i
        ElseIf bWalkingOrRunning = True Then 'We need to calculate the Potential Energy for running
            Dim nBeginLFloat, nBeginLSingleStance, nBeginRFloat As Integer 'Begin of Right Single force is always Percent 0.  These other three numbers define the beginning of the other 3 phases
            Dim tempFloatPotentialEnergy As Single
            For i = 1 To 99
                If AverageData(i).nPhase = con_R_Single_Support And AverageData(i + 1).nPhase = con_L_Float Then
                    nBeginLFloat = i
                ElseIf AverageData(i).nPhase = con_L_Float And AverageData(i + 1).nPhase = con_L_Single_Support Then
                    nBeginLSingleStance = i + 1
                ElseIf AverageData(i).nPhase = con_L_Single_Support And AverageData(i + 1).nPhase = con_R_Float Then
                    nBeginRFloat = i
                End If
            Next i
            For i = 1 To 100
                If i >= 0 And i < nBeginLFloat Then
                    arEnergy_Potential(i) = 0.5 * arSpringConstants(i) * (arDisplacement(i) - arDisplacement(0)) ^ 2 'energy = 0.5 * k * s^2
                ElseIf i >= nBeginLFloat And i < nBeginLSingleStance Then
                    arEnergy_Potential(i) = BodyMass * 32 * (arDisplacement(i) - arDisplacement(nBeginLFloat - 1)) 'energy = mgh
                ElseIf i >= nBeginLSingleStance And i < nBeginRFloat Then
                    arEnergy_Potential(i) = 0.5 * arSpringConstants(i) * (arDisplacement(i) - arDisplacement(nBeginLSingleStance - 1)) ^ 2
                ElseIf i >= nBeginRFloat And i < 100 Then
                    arEnergy_Potential(i) = BodyMass * 32 * (arDisplacement(i) - arDisplacement(nBeginRFloat - 1))
                ElseIf i = 100 Then
                    arEnergy_Potential(100) = arEnergy_Potential(0)
                End If
                arEnergy_Total(i) = arEnergy_Kinetic(i) + arEnergy_Potential(i)
            Next i
        End If

49:     'STEP 49: Calulate the Critical Dampening Factor for the system
        nCriticalDampening = 2 * (BodyMass * nNatural_ω) ^ 0.5

50:     'STEP 50: Do the Spline for the AP - CoP Displacement Left to get the CoP Velocity
        lblProgressBar.Text = "Step 50: Calculating the Cubic Spline for the CoP AP Left"
        ProgressBar1.Value = 50
        Dim nBegPct, nEndPct As Short
        'Find out how to redim the temp spline array.
        If bBeginLeftOrRight = False Then 'Identify the beginning of the left support
            nBegPct = 0
        Else
            For Me.i = 0 To 99
                If (AverageData(i).nPhase = con_R_Single_Support And AverageData(i + 1).nPhase = con_L_Double_Support) Or (AverageData(i).nPhase = con_L_Float And AverageData(i + 1).nPhase = con_L_Single_Support) Then
                    nBegPct = i + 1
                    Exit For
                End If
            Next
        End If
        For Me.i = 0 To 99 'Identify the end of the left support
            If (AverageData(i).nPhase = con_R_Double_Support And AverageData(i + 1).nPhase = con_R_Single_Support) Or (AverageData(i).nPhase = con_L_Single_Support And AverageData(i + 1).nPhase = con_R_Float) Then
                nEndPct = i
                Exit For
            End If
        Next i
        If nBegPct > nEndPct Then
            nEndPct = nEndPct + 100
        End If
        ReDim tempSpline(nEndPct - nBegPct) 'Make the number of items in the tempspline
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPLoc_AP_L(nBegPct + i)
            Else
                tempSpline(i).a = arCoPLoc_AP_L(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPVel_AP_L(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPVel_AP_L(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

51:     'STEP 51: Do the Spline and Smoothing to get the Velocity ML CoP - Left Foot.
        lblProgressBar.Text = "Step 51: Calculating the Velocity for the CoP ML Left"
        ProgressBar1.Value = 51
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPLoc_ML_L(nBegPct + i)
            Else
                tempSpline(i).a = arCoPLoc_ML_L(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPVel_ML_L(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPVel_ML_L(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

52:     'STEP 52: Find the acceleration AP - CoP  Left Foot
        lblProgressBar.Text = "Step 52: Calculating the Acceleration CoP AP Left"
        ProgressBar1.Value = 52
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPVel_AP_L(nBegPct + i)
            Else
                tempSpline(i).a = arCoPVel_AP_L(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPAcc_AP_L(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPAcc_AP_L(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

53:     'STEP 53: Calculate the Acceleration ML CoP for the Left Foot
        lblProgressBar.Text = "Step 53: Calculating the Acceleration CoP ML Left"
        ProgressBar1.Value = 53
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPVel_ML_L(nBegPct + i)
            Else
                tempSpline(i).a = arCoPVel_ML_L(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPAcc_ML_L(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPAcc_ML_L(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

54:     'STEP 54: Do the Spline for Calculating the AP Right CoP Velocity
        lblProgressBar.Text = "Step 54: Calculating the Spline for Velocity of the CoP AP Right"
        ProgressBar1.Value = 54
        If bBeginLeftOrRight = True Then 'Identify the beginning of the Right support
            nBegPct = 0
        Else
            For Me.i = 0 To 99
                If (AverageData(i).nPhase = con_L_Single_Support And AverageData(i + 1).nPhase = con_R_Double_Support) Or (AverageData(i).nPhase = con_L_Float And AverageData(i + 1).nPhase = con_L_Single_Support) Then
                    nBegPct = i + 1
                    Exit For
                End If
            Next
        End If
        For Me.i = 0 To 99 'Identify the end of the Right support
            If (AverageData(i).nPhase = con_L_Double_Support And AverageData(i + 1).nPhase = con_L_Single_Support) Or (AverageData(i).nPhase = con_R_Single_Support And AverageData(i + 1).nPhase = con_L_Float) Then
                nEndPct = i
                Exit For
            End If
        Next i
        If nBegPct > nEndPct Then
            nEndPct = nEndPct + 100
        End If
        ReDim tempSpline(nEndPct - nBegPct) 'Make the number of items in the tempspline
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPLoc_AP_R(nBegPct + i)
            Else
                tempSpline(i).a = arCoPLoc_AP_R(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPVel_AP_R(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPVel_AP_R(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i
        If arCoPVel_AP_R(0) <> arCoPVel_AP_R(100) Then 'If you haven't reset the 0 or 100% to equal each other.
            If arCoPVel_AP_R(0) > arCoPVel_AP_R(100) Then
                arCoPVel_AP_R(100) = arCoPVel_AP_R(0)
            Else
                arCoPVel_AP_R(0) = arCoPVel_AP_R(100)
            End If
        End If

61:     'STEP 61: Calculate the  ML - CoP Velocity for the RIGHT Foot
        lblPatientName.Text = "Step 61: Calculating the Velocity for the CoP ML Right"
        ProgressBar1.Value = 61
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPLoc_ML_R(nBegPct + i)
            Else
                tempSpline(i).a = arCoPLoc_ML_R(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPVel_ML_R(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPVel_ML_R(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

62:     'STEP 62: Calculate the Acceleration for the AP - CoP Right
        lblProgressBar.Text = "Step 62: Calculating the Acceleration CoP AP Left"
        ProgressBar1.Value = 62
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPVel_AP_R(nBegPct + i)
            Else
                tempSpline(i).a = arCoPVel_AP_R(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPAcc_AP_R(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPAcc_AP_R(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

63:     'STEP 63: Calculate the Acceleration ML - CoP - Right
        lblProgressBar.Text = "Step 63: Calculating the Acceleration CoP ML Right"
        ProgressBar1.Value = 63
        For Me.i = 0 To UBound(tempSpline) 'First set the values in the tempSpline to pass
            If i + nBegPct < 101 Then
                tempSpline(i).a = arCoPVel_ML_R(nBegPct + i)
            Else
                tempSpline(i).a = arCoPVel_ML_R(nBegPct + i - 100)
            End If
            tempSpline(i).x = AverageData(i).nTime
            tempSpline(i).b = 0
            tempSpline(i).c = 0
            tempSpline(i).d = 0
            tempSpline(i).h = 0
            tempSpline(i).alpha = 0
            tempSpline(i).l = 0
            tempSpline(i).mu = 0
            tempSpline(i).z = 0
        Next i
        subCreateCubicSpline() 'Do the spline
        For Me.i = 0 To 100 'Get Ready to smooth the velocity
            If i < UBound(tempSpline) Then 'The spline only gets the velocity up to the next-to-last pct point of the stance period.
                tempSmoothingArray(i) = tempSpline(i).b
            ElseIf i = UBound(tempSpline) Then 'This is the velocity when you get to the last point of the stance period.
                tempSmoothingArray(i) = tempSpline(i - 1).b + 0.5 * (tempSpline(i).c - tempSpline(i - 1).c) * (tempSpline(i).x - tempSpline(i - 1).x)
            Else
                tempSmoothingArray(i) = 0
            End If
        Next i
        subSmoothDataWithButterworthFilter() 'First smoothing
        subSmoothDataWithButterworthFilter() 'Second smoothing of AP velocity for the left foot.
        For Me.i = 0 To 100 'Put the smoothing array back into the CoPVel_AP_L array.
            If (i + nBegPct) <= 100 Then
                arCoPAcc_ML_R(i + nBegPct) = tempSmoothingArray(i)
            ElseIf (i + nBegPct) > 100 Then
                arCoPAcc_ML_R(i + nBegPct - 100) = tempSmoothingArray(i)
            End If
        Next i

64:     'STEP 64: Because of the smoothing functions, the CoP has to be zeroed out for the nonstance periods of gait.
        lblProgressBar.Text = "Step 64: Correcting the CoP during the Swing Phase"
        ProgressBar1.Value = 64
        For Me.i = 0 To 100 'If you are in a swing phase, then the CoP values all have to be zero.
            If AverageData(i).nPhase = con_L_Single_Support Or AverageData(i).nPhase = con_L_Float Or AverageData(i).nPhase = con_R_Float Then
                arCoPLoc_ML_R(i) = 0
                arCoPLoc_AP_R(i) = 0
                arCoPVel_ML_R(i) = 0
                arCoPVel_AP_R(i) = 0
                arCoPAcc_ML_R(i) = 0
                arCoPAcc_AP_R(i) = 0
            End If
            If AverageData(i).nPhase = con_R_Single_Support Or AverageData(i).nPhase = con_L_Float Or AverageData(i).nPhase = con_R_Float Then
                arCoPLoc_ML_L(i) = 0
                arCoPLoc_AP_L(i) = 0
                arCoPVel_ML_L(i) = 0
                arCoPVel_AP_L(i) = 0
                arCoPAcc_ML_L(i) = 0
                arCoPAcc_AP_L(i) = 0
            End If
        Next i

65:     'STEP 65:  Calculate the CoP, normalized to the percent of Stance Phase only, in the AP direction for the Position, the Velocity and the Acceleration
        lblProgressBar.Text = "Step 65: calculating the CoP in terms of the stance percent."
        ProgressBar1.Value = 65
        arCoPLoc_Stance_AP_L(0) = arCoPLoc_AP_L(0)
        arCoPLoc_Stance_AP_R(0) = arCoPLoc_AP_R(0)
        arCoPVel_Stance_AP_L(0) = arCoPVel_AP_L(0)
        arCoPVel_Stance_AP_R(0) = arCoPVel_AP_R(0)
        Dim nFirstPct, nFinalPct As Integer
        Dim nTempPct As Single
        Me.i = 0
        '      Start with the Left Foot
        If bWalkingOrRunning = False Then 'If you are walking.
            If AverageData(0).nLForce = 0 Then 'Find the first pct of the stance period of gait for the Left
                nFirstPct = 0
            Else 'Find where the Left Foot begins the Stance Phase of Gait gait cycle if the Right Foot starts
                Me.i = 1
                Do Until arCoPLoc_AP_L(i) <> 0 And arCoPLoc_AP_L(i - 1) = 0
                    i = i + 1
                Loop
                nFirstPct = i
            End If
        ElseIf bWalkingOrRunning = True Then 'If you are running.
            Me.i = 0
            Do Until arCoPLoc_AP_L(i) <> 0
                Me.i = Me.i + 1
            Loop
            nFirstPct = i
        End If

        i = 0 'Find the last percent of the stance period of Gait for the Left
        Do Until arCoPLoc_AP_L(i) > 0 And arCoPLoc_AP_L(i + 1) = 0
            i = i + 1
        Loop
        nFinalPct = i

        If nFinalPct < nFirstPct Then 'For walking you need to reset the final pct if the left foot is not the start of the gait cycle.
            nFinalPct = nFinalPct + 100
        End If

        arCoPLoc_Stance_AP_L(0) = arCoPLoc_AP_L(nFirstPct) 'set the beginning and ending locations of the stance phase
        arCoPVel_Stance_AP_L(0) = arCoPVel_AP_L(nFirstPct) 'set the beginning and ending velocities of the stance percent of the stance phase
        If nFinalPct < 100 Then
            arCoPLoc_Stance_AP_L(100) = arCoPLoc_AP_L(nFinalPct) 'Set the 100% of the Stance for Location
            arCoPVel_Stance_AP_L(100) = arCoPVel_AP_L(nFinalPct) 'Set the 100% of the Stance for Velocity
        Else
            arCoPLoc_Stance_AP_L(100) = arCoPLoc_AP_L(nFinalPct - 100)
            arCoPVel_Stance_AP_L(100) = arCoPVel_AP_L(nFinalPct - 100)
        End If

        For Me.i = 1 To 99 'Now fill in the rest of the percents between the beginning and ending percent.
            nTempPct = i * ((nFinalPct - nFirstPct) * 0.01) + nFirstPct
            j = Int(nTempPct)
            If j < 100 Then
                arCoPLoc_Stance_AP_L(i) = arCoPLoc_AP_L(j) + (nTempPct - j) * (arCoPLoc_AP_L(j + 1) - arCoPLoc_AP_L(j))
                arCoPVel_Stance_AP_L(i) = arCoPVel_AP_L(j) + (nTempPct - j) * (arCoPVel_AP_L(j + 1) - arCoPVel_AP_L(j))
            Else
                arCoPLoc_Stance_AP_L(i) = arCoPLoc_AP_L(j - 100) + (nTempPct - j) * (arCoPLoc_AP_L(j - 99) - arCoPLoc_AP_L(j - 100))
                arCoPVel_Stance_AP_L(i) = arCoPVel_AP_L(j - 100) + (nTempPct - j) * (arCoPVel_AP_L(j - 99) - arCoPVel_AP_L(j - 100))
            End If
        Next i

        '    Now do the Right Foot
        If bWalkingOrRunning = False Then
            If AverageData(0).nRForce = 0 Then 'Find the first pct of the stance period of gait for the Right
                nFirstPct = 0
            Else
                Me.i = 1
                Do Until arCoPLoc_AP_R(i) <> 0 And arCoPLoc_AP_R(i - 1) = 0
                    i = i + 1
                Loop
                nFirstPct = i
            End If
        ElseIf bWalkingOrRunning = True Then '(running) to find the first pct.
            Me.i = 0
            Do Until arCoPLoc_AP_R(i) <> 0
                Me.i = Me.i + 1
            Loop
            nFirstPct = i
        End If

        i = 0 'Find the last pct of the stance period of gait for the Right
        Do Until arCoPLoc_AP_R(i) > 0 And arCoPLoc_AP_R(i + 1) = 0
            i = i + 1
        Loop
        nFinalPct = i

        If nFinalPct < nFirstPct Then
            nFinalPct = nFinalPct + 100
        End If

        arCoPLoc_Stance_AP_R(0) = arCoPLoc_AP_R(nFirstPct)
        arCoPVel_Stance_AP_R(0) = arCoPVel_AP_R(nFirstPct)
        If nFinalPct < 100 Then 'Set the 100% of the Stance
            arCoPLoc_Stance_AP_R(100) = arCoPLoc_AP_R(nFinalPct)
            arCoPVel_Stance_AP_R(100) = arCoPVel_AP_R(nFinalPct)
        Else
            arCoPLoc_Stance_AP_R(100) = arCoPLoc_AP_R(nFinalPct - 100)
            arCoPVel_Stance_AP_R(100) = arCoPVel_AP_R(nFinalPct - 100)
        End If
        For Me.i = 1 To 99 'Fill in the percents between the beginning and ending percent.
            nTempPct = i * ((nFinalPct - nFirstPct) * 0.01) + nFirstPct
            j = Int(nTempPct)
            If j < 100 Then
                arCoPLoc_Stance_AP_R(i) = arCoPLoc_AP_R(j) + (nTempPct - j) * (arCoPLoc_AP_R(j + 1) - arCoPLoc_AP_R(j))
                arCoPVel_Stance_AP_R(i) = arCoPVel_AP_R(j) + (nTempPct - j) * (arCoPVel_AP_R(j + 1) - arCoPVel_AP_R(j))
            Else
                arCoPLoc_Stance_AP_R(i) = arCoPLoc_AP_R(j - 100) + (nTempPct - j) * (arCoPLoc_AP_R(j - 99) - arCoPLoc_AP_R(j - 100))
                arCoPVel_Stance_AP_R(i) = arCoPVel_AP_R(j - 100) + (nTempPct - j) * (arCoPVel_AP_R(j - 99) - arCoPVel_AP_R(j - 100))
            End If
        Next i

66:     'STEP 66: Calculate the Gait Indices
        lblProgressBar.Text = "Step 66: Calculating the Gait Indices"
        ProgressBar1.Value = 66
        GI.sName = lblFullFileNameL.Text
        Dim nAmpGIDisp, nAmpGIForce, nAmpGIVel, nAmpGIWork, nAmpGIPower As Double
        For Me.i = 1 To 12 'Get the square of the amplitude of each of the harmonic values
            nAmpGIDisp = nAmpGIDisp + arHarmonicValuesDisplacement(i, con_Amp) ^ 2
            nAmpGIForce = nAmpGIForce + arHarmonicValuesForce(i, con_Amp) ^ 2
            nAmpGIVel = nAmpGIVel + arHarmonicValuesVelocity(i, con_Amp) ^ 2
            nAmpGIWork = nAmpGIWork + arHarmonicValuesWork(i, con_Amp) ^ 2
            nAmpGIPower = nAmpGIPower + arHarmonicValuesPower(i, con_Amp) ^ 2
        Next i
        GI.Purity = arHarmonicValuesDisplacement(2, con_Amp) / Math.Sqrt(nAmpGIDisp)
        GI.Symmetry = arHarmonicValuesDisplacement(2, con_Amp) ^ 2 + arHarmonicValuesDisplacement(4, con_Amp) ^ 2 + arHarmonicValuesDisplacement(6, con_Amp) ^ 2 + arHarmonicValuesDisplacement(8, con_Amp) ^ 2 + arHarmonicValuesDisplacement(10, con_Amp) ^ 2 + arHarmonicValuesDisplacement(12, con_Amp) ^ 2
        GI.Symmetry = Math.Sqrt((GI.Symmetry) / nAmpGIDisp)
        'Calculate the Energy Efficiency Index
        Dim tempPotDif, tempKinDif, tempTotalDif As Single
        Dim tempAverage As Single
        tempPotDif = 0
        tempKinDif = 0
        tempTotalDif = 0
        tempAverage = 0
        For Me.i = 0 To 99
            tempPotDif = tempPotDif + Math.Abs(arEnergy_Potential(i + 1) - arEnergy_Potential(i))
            tempKinDif = tempKinDif + Math.Abs(arEnergy_Kinetic(i + 1) - arEnergy_Kinetic(i))
            tempTotalDif = tempTotalDif + Math.Abs(arEnergy_Total(i + 1) - arEnergy_Total(i))
        Next i
        GI.Energy = 1 - tempTotalDif / (tempPotDif + tempKinDif)

        GI.Overall = GI.Purity
        GI.Overall = GI.Overall + arHarmonicValuesVelocity(2, con_Amp) / Math.Sqrt(nAmpGIVel)
        GI.Overall = GI.Overall + arHarmonicValuesForce(2, con_Amp) / Math.Sqrt(nAmpGIForce)
        GI.Overall = GI.Overall + arHarmonicValuesPower(4, con_Amp) / Math.Sqrt(nAmpGIPower)
        GI.Overall = GI.Overall + arHarmonicValuesWork(4, con_Amp) / Math.Sqrt(nAmpGIWork)
        GI.Overall = GI.Overall / 5

        'STEP 66A:  Calculate the CoP Symmetry Index
        Dim arTempAvgCurve(100) As Single
        CoPSymmetryIndex = 0
        For Me.i = 0 To 100
            arTempAvgCurve(i) = (arCoPLoc_Stance_AP_L(i) + arCoPLoc_Stance_AP_R(i)) / 2
        Next i
        For Me.i = 1 To 99 'Find the area between the curves
            CoPSymmetryIndex = CoPSymmetryIndex + Math.Abs(arCoPLoc_Stance_AP_L(i) - arCoPLoc_Stance_AP_R(i))
        Next
        CoPSymmetryIndex = CoPSymmetryIndex + 0.5 * Math.Abs(arCoPLoc_Stance_AP_L(0) - arCoPLoc_Stance_AP_R(0))
        CoPSymmetryIndex = CoPSymmetryIndex + 0.5 * Math.Abs(arCoPLoc_Stance_AP_L(100) - arCoPLoc_Stance_AP_R(100))
        Dim ntempAvgArea As Single 'This temporary variable is to add up the area under the average curve (arTempCurve)
        ntempAvgArea = 0
        For Me.i = 1 To 99
            ntempAvgArea = ntempAvgArea + arTempAvgCurve(i)
        Next i
        ntempAvgArea = ntempAvgArea + 0.5 * (arTempAvgCurve(0) + arTempAvgCurve(100))
        CoPSymmetryIndex = 1 - CoPSymmetryIndex / ntempAvgArea 'This is the final calculation for the symmetry index

        'STEP 66B:  Calculate the CoP Purity Indices
        Dim ntempMax, ntempMin As Single
        ntempMin = arCoPLoc_Stance_AP_L(0) 'The next For-Next block finds the two end points for the perfect purity line.
        ntempMax = arCoPLoc_Stance_AP_L(0)
        If ntempMin > arCoPLoc_Stance_AP_R(0) Then ntempMin = arCoPLoc_Stance_AP_R(0)
        If ntempMax < arCoPLoc_Stance_AP_R(0) Then ntempMax = arCoPLoc_Stance_AP_R(0)
        For Me.i = 1 To 100
            If ntempMin > arCoPLoc_Stance_AP_L(i) Then ntempMin = arCoPLoc_Stance_AP_L(i)
            If ntempMin > arCoPLoc_Stance_AP_R(i) Then ntempMin = arCoPLoc_Stance_AP_R(i)
            If ntempMax < arCoPLoc_Stance_AP_L(i) Then ntempMax = arCoPLoc_Stance_AP_L(i)
            If ntempMax < arCoPLoc_Stance_AP_R(i) Then ntempMax = arCoPLoc_Stance_AP_R(i)
        Next i
        Dim ntempSlope As Single
        ntempSlope = 0.01 * (ntempMax - ntempMin) 'Get the slope of the perfect line.
        arTempAvgCurve(0) = 0 'Set the minimum endpoint of the straight line
        arTempAvgCurve(100) = 100 * ntempSlope 'Set the maximum of the perfect straight line
        For Me.i = 1 To 99 'Get the rest of the points for the perfect straight line.
            arTempAvgCurve(i) = i * ntempSlope
        Next i
        CoPPurityIndex_L = 0
        CoPPurityIndex_R = 0
        CoPPurityIndex_Avg = 0
        CoPPurityIndex_L = CoPPurityIndex_L + 0.5 * Math.Abs(arCoPLoc_Stance_AP_L(0) - ntempMin)
        CoPPurityIndex_R = CoPPurityIndex_R + 0.5 * Math.Abs(arCoPLoc_Stance_AP_R(0) - ntempMin)
        ntempAvgArea = 50 * (ntempMax - ntempMin)
        For Me.i = 1 To 99 'Find the area between the CoP paths and the perfect movement forward path.
            CoPPurityIndex_L = CoPPurityIndex_L + Math.Abs(arCoPLoc_Stance_AP_L(i) - ntempMin - arTempAvgCurve(i))
            CoPPurityIndex_R = CoPPurityIndex_R + Math.Abs(arCoPLoc_Stance_AP_R(i) - ntempMin - arTempAvgCurve(i))
        Next i
        CoPPurityIndex_L = CoPPurityIndex_L + 0.5 * Math.Abs(arCoPLoc_Stance_AP_L(100) - ntempMin - arTempAvgCurve(100))
        CoPPurityIndex_R = CoPPurityIndex_R + 0.5 * Math.Abs(arCoPLoc_Stance_AP_R(100) - ntempMin - arTempAvgCurve(100))
        CoPPurityIndex_L = 1 - CoPPurityIndex_L / ntempAvgArea
        CoPPurityIndex_R = 1 - CoPPurityIndex_R / ntempAvgArea
        GI.Symmetry = CoPSymmetryIndex
        CoPPurityIndex_Avg = (CoPPurityIndex_L + CoPPurityIndex_R) / 2
        GI.CoP = CoPPurityIndex_Avg
        GI.Overall = (GI.Purity * GI.Energy * CoPPurityIndex_Avg) ^ (1 / 3)

        'STEP 66C:  Calculate the Spring Consistency Index
        Dim ntempSpring As Single
        For Me.i = 0 To 99
            SpringConsistencyIndex = SpringConsistencyIndex + arSpringConstants(i)
        Next i
        SpringConsistencyIndex = SpringConsistencyIndex / 100 'This is the average spring constant
        For Me.i = 0 To 99
            ntempSpring = ntempSpring + (arSpringConstants(i) - SpringConsistencyIndex) ^ 2
        Next i
        ntempSpring = ntempSpring / 99
        SpringConsistencyIndex = 1 - (Math.Sqrt(ntempSpring)) / SpringConsistencyIndex

67:     'STEP 67: Set the GaitPhase Array and the GaitTime Array and the arLeftForce and the arRightForce arrays
        For Me.i = 0 To 100
            arLeftForce(i) = AverageData(i).nLForce
            arRightForce(i) = AverageData(i).nRForce
            arGaitPhase(i) = AverageData(i).nPhase
            arGaitTime(i) = AverageData(i).nTime
        Next i

        'STEP 66: If the 0 percent begins with the Right Foot hitting the ground, we need to make 0 percent at the moment of Left Foot Contact
        lblProgressBar.Text = "Step 66: Setting the Left Foot Strike as the Beginning of the Gait Cycle"
        ProgressBar1.Value = 66
        If bBeginLeftOrRight = True Then
            Dim tmpSwitch(100) As Double
            Dim nBegin As Short
            For Me.i = 0 To 99
                If AverageData(i).nPhase = con_R_Single_Support And AverageData(i + 1).nPhase = con_L_Double_Support Then
                    nBegin = i + 1
                    Exit For
                ElseIf AverageData(i).nPhase = con_L_Float And AverageData(i + 1).nPhase = con_L_Single_Support Then
                    nBegin = i + 1
                    Exit For
                End If
            Next
            For Me.i = 0 To 100 'First switch the Displacement
                tmpSwitch(i) = arDisplacement(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arDisplacement(i) = tmpSwitch(i + nBegin)
                Else
                    arDisplacement(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Second switch the Velocity
                tmpSwitch(i) = arCoMVelocity(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoMVelocity(i) = tmpSwitch(i + nBegin)
                Else
                    arCoMVelocity(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Third switch the Force
                tmpSwitch(i) = arBodyWeight(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arBodyWeight(i) = tmpSwitch(i + nBegin)
                Else
                    arBodyWeight(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Fourth switch the BodyWeightPercent
                tmpSwitch(i) = arBodyWeightPct(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arBodyWeightPct(i) = tmpSwitch(i + nBegin)
                Else
                    arBodyWeightPct(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Fifth switch the LeftForce Array
                tmpSwitch(i) = arLeftForce(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arLeftForce(i) = tmpSwitch(i + nBegin)
                Else
                    arLeftForce(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Sixth switch the RightForce Array
                tmpSwitch(i) = arRightForce(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arRightForce(i) = tmpSwitch(i + nBegin)
                Else
                    arRightForce(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Seventh switch the Total Force Array
                tmpSwitch(i) = arTotalForce(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arTotalForce(i) = tmpSwitch(i + nBegin)
                Else
                    arTotalForce(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Eighth switch the GaitPhase Array
                tmpSwitch(i) = arGaitPhase(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arGaitPhase(i) = tmpSwitch(i + nBegin)
                Else
                    arGaitPhase(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Ninth switch the Power Array
                tmpSwitch(i) = arPower(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arPower(i) = tmpSwitch(i + nBegin)
                Else
                    arPower(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Tenth switch the Work Array
                tmpSwitch(i) = arWork(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arWork(i) = tmpSwitch(i + nBegin)
                Else
                    arWork(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Eleventh swith the Spring Constants array
                tmpSwitch(i) = arSpringConstants(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arSpringConstants(i) = tmpSwitch(i + nBegin)
                Else
                    arSpringConstants(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twelfth switch the CoPLoc_AP_L array
                tmpSwitch(i) = arCoPLoc_AP_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPLoc_AP_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPLoc_AP_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Thirteenth switch the CoPLoc_AP_R array
                tmpSwitch(i) = arCoPLoc_AP_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPLoc_AP_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPLoc_AP_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Fourteenth switch the CoPLoc_ML_L Array
                tmpSwitch(i) = arCoPLoc_ML_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPLoc_ML_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPLoc_ML_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Fifteenth switch the CoPLoc_ML_R Array
                tmpSwitch(i) = arCoPLoc_ML_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPLoc_ML_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPLoc_ML_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Sixteenth switch the CoPVel_AP_L array
                tmpSwitch(i) = arCoPVel_AP_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPVel_AP_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPVel_AP_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Seventeenth switch the CoPVel_AP_R Array
                tmpSwitch(i) = arCoPVel_AP_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPVel_AP_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPVel_AP_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Eighteenth switch the CoPVel_ML_L Array
                tmpSwitch(i) = arCoPVel_ML_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPVel_ML_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPVel_ML_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Nineteenth switch the CoPVel_ML_R Array
                tmpSwitch(i) = arCoPVel_ML_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPVel_ML_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPVel_ML_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twentieth switch the CoPAcc_AP_L Array
                tmpSwitch(i) = arCoPAcc_AP_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPAcc_AP_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPAcc_AP_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-first switch the CoPAcc_AP_R Array
                tmpSwitch(i) = arCoPAcc_AP_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPAcc_AP_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPAcc_AP_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-second switch the CoPAcc_ML_L Array
                tmpSwitch(i) = arCoPAcc_ML_L(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPAcc_ML_L(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPAcc_ML_L(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-third switch the CoPAcc_ML_R Array
                tmpSwitch(i) = arCoPAcc_ML_R(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arCoPAcc_ML_R(i) = tmpSwitch(i + nBegin)
                Else
                    arCoPAcc_ML_R(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-fourth switch the Energy_Potential Array
                tmpSwitch(i) = arEnergy_Potential(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arEnergy_Potential(i) = tmpSwitch(i + nBegin)
                Else
                    arEnergy_Potential(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-fifth switch the Energy_Kinetic Array
                tmpSwitch(i) = arEnergy_Kinetic(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arEnergy_Kinetic(i) = tmpSwitch(i + nBegin)
                Else
                    arEnergy_Kinetic(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
            For Me.i = 0 To 100 'Twenty-sixth switch the Energy_Total Array
                tmpSwitch(i) = arEnergy_Total(i)
            Next i
            For Me.i = 0 To 100
                If i + nBegin < 101 Then
                    arEnergy_Total(i) = tmpSwitch(i + nBegin)
                Else
                    arEnergy_Total(i) = tmpSwitch(i + nBegin - 100)
                End If
            Next i
        End If


68:     'STEP 67: Calculate the percent in each phase of the gait cycle, i.e. single, double or float phase.
        lblProgressBar.Text = "Step 67: calculate the percent in each phase of the gait cycle"
        ProgressBar1.Value = 67
        i = 0
        If bWalkingOrRunning = False Then
            Do Until (arGaitPhase(i) = con_L_Single_Support)
                i = i + 1
            Loop
            nPctDoubleSupport_L = i - 1
            Do Until arGaitPhase(i) = con_R_Double_Support
                i = i + 1
            Loop
            nPctSingleSupport_L = i - nPctDoubleSupport_L
            Do Until arGaitPhase(i) = con_R_Single_Support
                i = i + 1
            Loop
            nPctDoubleSupport_R = i - 1 - (nPctDoubleSupport_L + nPctSingleSupport_L)
            nPctSingleSupport_R = 100 - (i - 1)
        Else
            Do Until arGaitPhase(i) = con_R_Float
                i = i + 1
            Loop
            nPctSingleSupport_L = i - 1
            Do Until arGaitPhase(i) = con_R_Single_Support
                i = i + 1
            Loop
            nPctFloat_R = i - nPctSingleSupport_L
            Do Until arGaitPhase(i) = con_L_Float
                i = i + 1
            Loop
            nPctSingleSupport_R = i - 1 - nPctFloat_R - nPctSingleSupport_L
            nPctFloat_R = 100 - (i - 1)
        End If

        'Step 67A: Divide each stance phase into its components of double and single or single & float
        If bWalkingOrRunning = con_Walking Then
            lblProgressBar.Text = "Step 67A: divine each stance phase into double, and single"
            nStancePhase_PctSupport_L(0) = Int(100 * nPctDoubleSupport_L / (nPctDoubleSupport_L + nPctSingleSupport_L + nPctDoubleSupport_R))
            nStancePhase_PctSupport_L(1) = Int(100 * nPctSingleSupport_L / (nPctDoubleSupport_L + nPctSingleSupport_L + nPctDoubleSupport_R))
            nStancePhase_PctSupport_L(2) = Int(100 * nPctDoubleSupport_R / (nPctDoubleSupport_L + nPctSingleSupport_L + nPctDoubleSupport_R))
            nStancePhase_PctSupport_R(0) = Int(100 * nPctDoubleSupport_R / (nPctDoubleSupport_R + nPctSingleSupport_R + nPctDoubleSupport_L))
            nStancePhase_PctSupport_R(1) = Int(100 * nPctSingleSupport_R / (nPctDoubleSupport_R + nPctSingleSupport_R + nPctDoubleSupport_L))
            nStancePhase_PctSupport_R(2) = Int(100 * nPctDoubleSupport_L / (nPctDoubleSupport_R + nPctSingleSupport_R + nPctDoubleSupport_L))
        ElseIf bWalkingOrRunning = con_Running Then
        End If

69:     'STEP 68: Now put all the arrays into the general collection
        lblProgressBar.Text = "Step 68: Adding All arrays to their Respective Collections"
        ProgressBar1.Value = 68
        colFileName.Add(lblFullFileNameL.Text, lblFullFileNameL.Text)
        colDisplacement.Add(arDisplacement, lblFullFileNameL.Text)
        colVelocity.Add(arCoMVelocity, lblFullFileNameL.Text)
        colBodyWeight.Add(arBodyWeight, lblFullFileNameL.Text)
        colBodyWeightPct.Add(arBodyWeightPct, lblFullFileNameL.Text)
        colLeftForce.Add(arLeftForce, lblFullFileNameL.Text)
        colRightForce.Add(arRightForce, lblFullFileNameL.Text)
        colTotalForce.Add(arTotalForce, lblFullFileNameL.Text)
        colGaitPhase.Add(arGaitPhase, lblFullFileNameL.Text)
        colPower.Add(arPower, lblFullFileNameL.Text)
        colWork.Add(arWork, lblFullFileNameL.Text)
        colSpringConstants.Add(arSpringConstants, lblFullFileNameL.Text)
        colCoPLoc_AP_L.Add(arCoPLoc_AP_L, lblFullFileNameL.Text)
        colCoPLoc_AP_R.Add(arCoPLoc_AP_R, lblFullFileNameL.Text)
        colCoPLoc_ML_L.Add(arCoPLoc_ML_L, lblFullFileNameL.Text)
        colCoPLoc_ML_R.Add(arCoPLoc_ML_R, lblFullFileNameL.Text)
        colCoPVel_AP_L.Add(arCoPVel_AP_L, lblFullFileNameL.Text)
        colCoPVel_AP_R.Add(arCoPVel_AP_R, lblFullFileNameL.Text)
        colCoPVel_ML_L.Add(arCoPVel_ML_L, lblFullFileNameL.Text)
        colCoPVel_ML_R.Add(arCoPVel_ML_R, lblFullFileNameL.Text)
        colCoPAcc_AP_L.Add(arCoPAcc_AP_L, lblFullFileNameL.Text)
        colCoPAcc_AP_R.Add(arCoPAcc_AP_R, lblFullFileNameL.Text)
        colCoPAcc_ML_L.Add(arCoPAcc_ML_L, lblFullFileNameL.Text)
        colCoPAcc_ML_R.Add(arCoPAcc_ML_R, lblFullFileNameL.Text)
        colEnergy_Potential.Add(arEnergy_Potential, lblFullFileNameL.Text)
        colEnergy_Kinetic.Add(arEnergy_Kinetic, lblFullFileNameL.Text)
        colEnergy_Total.Add(arEnergy_Total, lblFullFileNameL.Text)
        colGI.Add(GI, lblFullFileNameL.Text)
        colCoPSymmetryIndex.Add(CoPSymmetryIndex, lblFullFileNameL.Text)
        colCoPPurityIndex_L.Add(CoPPurityIndex_L, lblFullFileNameL.Text)
        colCoPPurityIndex_R.Add(CoPPurityIndex_R, lblFullFileNameL.Text)
        colCoPPurityIndex_Avg.Add(CoPPurityIndex_Avg, lblFullFileNameL.Text)

70:     'STEP 69: Set default Maximum Y values
        lblProgressBar.Text = "Step 69: Setting the Initial Maximum Y Values for Graphing"
        ProgressBar1.Value = 69
        If nFootLengthLeft(1) - nFootLengthLeft(0) >= nFootLengthRight(1) - nFootLengthRight(0) Then 'The CoPLocation Max Values cannot be changed.
            arMaximumYValues(conCoP_AP_ML) = nFootLengthLeft(1) - nFootLengthLeft(0)
        Else
            arMaximumYValues(conCoP_AP_ML) = nFootLengthRight(1) - nFootLengthRight(0)
        End If
        arMaximumYValues(conCoP_AP) = arMaximumYValues(80)
        If nFootWidthLeft(1) - nFootWidthLeft(0) >= nFootWidthRight(1) - nFootWidthRight(0) Then 'The CoPLocation Max Values cannot be changed.
            arMaximumYValues(conCoP_ML) = nFootWidthLeft(1) - nFootWidthLeft(0)
        Else
            arMaximumYValues(conCoP_ML) = nFootWidthRight(1) - nFootWidthRight(0)
        End If

        For Me.i = 1 To 12 'These are the maximums for graphs 13,23,33,43&53
            If arMaximumYValues(13) < arHarmonicValuesForce(i, 2) Then arMaximumYValues(13) = arHarmonicValuesForce(i, 2)
            If arMaximumYValues(23) < arHarmonicValuesDisplacement(i, 2) Then arMaximumYValues(23) = arHarmonicValuesDisplacement(i, 2)
            If arMaximumYValues(33) < arHarmonicValuesVelocity(i, 2) Then arMaximumYValues(33) = arHarmonicValuesVelocity(i, 2)
            If arMaximumYValues(43) < arHarmonicValuesPower(i, 2) Then arMaximumYValues(43) = arHarmonicValuesPower(i, 2)
            If arMaximumYValues(53) < arHarmonicValuesWork(i, 2) Then arMaximumYValues(53) = arHarmonicValuesWork(i, 2)
        Next i

        For Me.i = 0 To 99
            For Me.j = 1 To nNumberOfStrides
                If System.Math.Abs(arAllTotalForces(j, i)) > arMaximumYValues(conForce_AllSteps) Then arMaximumYValues(conForce_AllSteps) = System.Math.Abs(arAllTotalForces(j, i))
            Next
            If System.Math.Abs(arTotalForce(i)) > arMaximumYValues(conForce_Avg) Then arMaximumYValues(conForce_Avg) = System.Math.Abs(arTotalForce(i))
            If arMaximumYValues(conForce_Radial) <> arMaximumYValues(conForce_Avg) Then arMaximumYValues(conForce_Radial) = arMaximumYValues(conForce_Avg)
            If System.Math.Abs(arBodyWeight(i)) > arMaximumYValues(conForce_As_BW) Then
                arMaximumYValues(conForce_As_BW) = System.Math.Abs(arBodyWeight(i))
                arMaximumYValues(conForce_Harm_Sum) = arMaximumYValues(10)
                arMaximumYValues(conForce_Harm_Diff) = arMaximumYValues(11)
            End If
            If System.Math.Abs(arDisplacement(i)) > arMaximumYValues(conDisplacement_Vert) Then
                arMaximumYValues(conDisplacement_Vert) = System.Math.Abs(arDisplacement(i))
                arMaximumYValues(conDisp_Harm_Sum) = arMaximumYValues(20)
                arMaximumYValues(conDisp_Harm_Diff) = arMaximumYValues(21)
            End If
            If System.Math.Abs(arCoMVelocity(i)) > arMaximumYValues(conVelocity_Vert) Then
                arMaximumYValues(conVelocity_Vert) = System.Math.Abs(arCoMVelocity(i))
                arMaximumYValues(conVel_Harm_Sum) = arMaximumYValues(30)
                arMaximumYValues(conVel_Harm_Diff) = arMaximumYValues(31)
            End If
            If System.Math.Abs(arPower(i)) > arMaximumYValues(conPower_Vert) Then
                arMaximumYValues(conPower_Vert) = System.Math.Abs(arPower(i))
                arMaximumYValues(conPower_Harm_Sum) = arMaximumYValues(40)
                arMaximumYValues(conPower_Harm_Diff) = arMaximumYValues(41)
            End If
            If System.Math.Abs(arWork(i)) > arMaximumYValues(conWork_Vert) Then
                arMaximumYValues(conWork_Vert) = System.Math.Abs(arWork(i))
                arMaximumYValues(conWork_Harm_Sum) = arMaximumYValues(50)
                arMaximumYValues(conWork_Harm_Diff) = arMaximumYValues(51)
            End If
            If System.Math.Abs(arEnergy_Total(i)) > arMaximumYValues(conEnergy) Then arMaximumYValues(conEnergy) = System.Math.Abs(arEnergy_Total(i))
            If System.Math.Abs(arSpringConstants(i)) > arMaximumYValues(conSpringConstants) Then arMaximumYValues(conSpringConstants) = System.Math.Abs(arSpringConstants(i))
            If System.Math.Abs(arCoPVel_AP_L(i)) > arMaximumYValues(conCoP_AP_Vel) Then arMaximumYValues(conCoP_AP_Vel) = System.Math.Abs(arCoPVel_AP_L(i))
            If System.Math.Abs(arCoPVel_AP_R(i)) > arMaximumYValues(conCoP_AP_Vel) Then arMaximumYValues(conCoP_AP_Vel) = System.Math.Abs(arCoPVel_AP_R(i))
            If System.Math.Abs(arCoPVel_ML_L(i)) > arMaximumYValues(conCoP_ML_Vel) Then arMaximumYValues(conCoP_ML_Vel) = System.Math.Abs(arCoPVel_ML_L(i))
            If System.Math.Abs(arCoPVel_ML_R(i)) > arMaximumYValues(conCoP_ML_Vel) Then arMaximumYValues(conCoP_ML_Vel) = System.Math.Abs(arCoPVel_ML_R(i))
            If System.Math.Abs(arCoPAcc_AP_L(i)) > arMaximumYValues(conCoP_AP_Acc) Then arMaximumYValues(conCoP_AP_Acc) = System.Math.Abs(arCoPAcc_AP_L(i))
            If System.Math.Abs(arCoPAcc_AP_R(i)) > arMaximumYValues(conCoP_AP_Acc) Then arMaximumYValues(conCoP_AP_Acc) = System.Math.Abs(arCoPAcc_AP_R(i))
            If System.Math.Abs(arCoPAcc_ML_L(i)) > arMaximumYValues(conCoP_ML_Acc) Then arMaximumYValues(conCoP_ML_Acc) = System.Math.Abs(arCoPAcc_ML_L(i))
            If System.Math.Abs(arCoPAcc_ML_R(i)) > arMaximumYValues(conCoP_ML_Acc) Then arMaximumYValues(conCoP_ML_Acc) = System.Math.Abs(arCoPAcc_ML_R(i))
        Next i

        For Me.i = 1 To 86 'Reset the values.
            If arMaximumYValues(i) <> 0 Then
                subSetYMaximumValues(i, arMaximumYValues(i))
            End If
        Next i

71:     'STEP 70: Set all the default maximum X values at 3 double phase cycles.
        ProgressBar1.Value = 70
        i = 0
        Do Until arGaitPhase(i) <> arGaitPhase(i + 1)
            i = i + 1
        Loop
        For Me.j = 0 To 86
            Select Case j
                Case 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 81, 82, 83, 84, 85, 86
                    If bWalkingOrRunning = False Then
                        arMaximumXValues(j) = 100 + i
                    ElseIf bWalkingOrRunning = True Then
                        arMaximumXValues(j) = 100
                    End If
                Case 13, 23, 33, 43, 53 'This is the harmonic amplitude graphs
                    arMaximumXValues(j) = arMaximumYValues(j - 3)
            End Select
        Next j

72:     'STEP 71: Set the Default Values of the Harmonics to Graph at the Pure Harmonics
        ProgressBar1.Value = 71
        For Me.i = 1 To 12
            Select Case i
                Case 2
                    chkHarmonicsDisplacement(i) = True
                    chkHarmonicsVelocity(i) = True
                    chkHarmonicsForce(i) = True
                    chkHarmonicsPower(i) = False
                    chkHarmonicsWork(i) = False
                Case 4
                    chkHarmonicsDisplacement(i) = False
                    chkHarmonicsVelocity(i) = False
                    chkHarmonicsForce(i) = False
                    chkHarmonicsPower(i) = True
                    chkHarmonicsWork(i) = True
                Case Else
                    chkHarmonicsDisplacement(i) = False
                    chkHarmonicsVelocity(i) = False
                    chkHarmonicsForce(i) = False
                    chkHarmonicsPower(i) = False
                    chkHarmonicsWork(i) = False
            End Select
        Next i
        For Me.i = 0 To 100
            arHarmonicsGraphingValuesDisplacement(i) = arHarmonicValuesDisplacement(2, con_Cos) * System.Math.Cos((2 * i * 0.01) * 2 * Math.PI) + arHarmonicValuesDisplacement(2, con_Sin) * System.Math.Sin((2 * i * 0.01) * 2 * Math.PI)
            arHarmonicsGraphingValuesVelocity(i) = arHarmonicValuesVelocity(2, con_Cos) * System.Math.Cos((2 * i * 0.01) * 2 * Math.PI) + arHarmonicValuesVelocity(2, con_Sin) * System.Math.Sin((2 * i * 0.01) * 2 * Math.PI)
            arHarmonicsGraphingValuesForce(i) = arHarmonicValuesForce(2, con_Cos) * System.Math.Cos((2 * i * 0.01) * 2 * Math.PI) + arHarmonicValuesForce(2, con_Sin) * System.Math.Sin((2 * i * 0.01) * 2 * Math.PI)
            arHarmonicsGraphingValuesPower(i) = arHarmonicValuesPower(4, con_Cos) * System.Math.Cos((4 * i * 0.01) * 2 * Math.PI) + arHarmonicValuesPower(4, con_Sin) * System.Math.Sin((4 * i * 0.01) * 2 * Math.PI)
            arHarmonicsGraphingValuesWork(i) = arHarmonicValuesWork(4, con_Cos) * System.Math.Cos((4 * i * 0.01) * 2 * Math.PI) + arHarmonicValuesWork(4, con_Sin) * System.Math.Sin((4 * i * 0.01) * 2 * Math.PI)
        Next
        'finally close the progress bar.
        lblProgressBar.Visible = False
        ProgressBar1.Visible = False


    End Sub

    Public Sub subSmoothDataWithButterworthFilter()
        'This sub is used to smooth an array of 100 elements using a butterworth filter.

        Dim tempVal(102) As Double
        Dim nTempFilter(102, 2) As Double

        'Start by setting the tempValue array for the first 102 values.
        For Me.i = 0 To 102
            If i < 100 Then
                tempVal(i) = tempSmoothingArray(i)
            Else
                tempVal(i) = tempSmoothingArray(i - 100)
            End If
        Next i

        'Do the forward smoothing for the array:
        For Me.i = 0 To 101
            If Me.i < 101 Then
                If tempVal(i) = 0 Then
                    nTempFilter(i, 1) = 0
                Else
                    If i = 0 Or i = 1 Then
                        nTempFilter(i, 1) = tempVal(i)
                    ElseIf i > 1 And i <= 100 Then
                        If tempVal(i - 2) = 0 Then
                            nTempFilter(i, 1) = tempVal(i)
                        Else
                            nTempFilter(i, 1) = 0.29289 * tempVal(i) + 0.58579 * tempVal(i - 1) + 0.29289 * tempVal(i - 2) - 0.17157 * nTempFilter(i - 2, 1)
                        End If
                    End If
                End If
            Else 'Redo 1% values by letting i =  101
                If tempVal(i - 100) = 0 Then
                    nTempFilter(i - 100, 1) = 0
                ElseIf tempVal(i - 2) = 0 Then
                    nTempFilter(i - 100, 1) = tempVal(i)
                Else
                    nTempFilter(i - 100, 1) = 0.29289 * tempVal(i) + 0.58579 * tempVal(i - 1) + 0.29289 * tempVal(i - 2) - 0.17157 * nTempFilter(i - 2, 1)
                End If
            End If
        Next i

        'Now do the reverse pass for the Left Force
        For Me.i = 102 To 0 Step -1
            If i = 101 Or i = 102 Then 'Special case for the 101 and 102 cases
                nTempFilter(i - 100, 2) = tempVal(i)
            ElseIf tempVal(i) = 0 Then
                nTempFilter(i, 2) = 0
            Else
                If tempVal(i + 2) = 0 Then
                    nTempFilter(i, 2) = tempVal(i)
                ElseIf i = 100 Or i = 99 Then
                    nTempFilter(i, 2) = 0.29289 * tempVal(i) + 0.58579 * tempVal(i + 1) + 0.29289 * tempVal(i + 2) - 0.17157 * nTempFilter(i + 2 - 100, 2)
                Else
                    nTempFilter(i, 2) = 0.29289 * tempVal(i) + 0.58579 * tempVal(i + 1) + 0.29289 * tempVal(i + 2) - 0.17157 * nTempFilter(i + 2, 2)
                End If
            End If
        Next i

        For Me.i = 100 To 0 Step -1
            tempSmoothingArray(i) = (nTempFilter(i, 1) + nTempFilter(i, 2)) / 2
        Next i

        tempSmoothingArray(0) = tempSmoothingArray(100)
    End Sub

    Public Sub subCreateCubicSpline()
        'This sub creates a natural spline covering 100 percentage points, where the 1st derivative at the end points is the slope between the two end points.
        'Find the slope of the curve at the endpoints
        Dim nBegSlope, nEndSlope As Double
        g = UBound(tempSpline)
        'STEP A:
        nBegSlope = (tempSpline(1).a - tempSpline(0).a) / (tempSpline(1).x - tempSpline(0).x)
        nEndSlope = (tempSpline(g).a - tempSpline(g - 1).a) / (tempSpline(g).x - tempSpline(g - 1).x)
        For Me.i = 0 To g - 1
            tempSpline(i).h = tempSpline(i + 1).x - tempSpline(i).x
        Next
        'STEP B:
        For Me.i = 1 To g - 1
            tempSpline(i).alpha = (3 * (tempSpline(i + 1).a * tempSpline(i - 1).h - tempSpline(i).a * (tempSpline(i + 1).x - tempSpline(i - 1).x) + tempSpline(i - 1).a * tempSpline(i).h)) / (tempSpline(i - 1).h * tempSpline(i).h)
        Next i
        'STEP C:
        tempSpline(0).l = 1
        tempSpline(0).mu = 0
        tempSpline(0).z = 0
        'STEP D:
        For Me.i = 1 To g - 1
            tempSpline(i).l = 2 * (tempSpline(i + 1).x - tempSpline(i - 1).x) - tempSpline(i - 1).h * tempSpline(i - 1).mu
            tempSpline(i).mu = tempSpline(i).h / tempSpline(i).l
            tempSpline(i).z = (tempSpline(i).alpha - tempSpline(i - 1).h * tempSpline(i - 1).z) / tempSpline(i).l
        Next i
        'STEP E:
        tempSpline(g).l = 1
        tempSpline(g).z = 0
        tempSpline(g).c = tempSpline(g).z
        'STEP F:
        For Me.i = g - 1 To 0 Step -1
            tempSpline(i).c = tempSpline(i).z - tempSpline(i).mu * tempSpline(i + 1).c
            tempSpline(i).b = (tempSpline(i + 1).a - tempSpline(i).a) / tempSpline(i).h - tempSpline(i).h * (tempSpline(i + 1).c + 2 * tempSpline(i).c) / 3
            tempSpline(i).d = (tempSpline(i + 1).c - tempSpline(i).c) / (3 * tempSpline(i).h)
        Next i
    End Sub

    Private Sub subCreateXAxisLabels(ByVal num As Integer)
        'num is the number of labels you need on the form.
        If num = 0 Then Exit Sub

        For Me.n = Me.Controls.Count To 1 Step -1 'Delete all the X axis labels.
            If Mid(Me.Controls(n - 1).Tag, 1, 6) = "XLabel" Then
                Me.Controls.Remove(Me.Controls(n - 1))
            End If
        Next n

        Dim nNum() As String = {"1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th"}

        For Me.n = 0 To num
            Dim nLabelX As New Label
            nLabelX.Tag = "XLabel " & n
            Select Case lblWhichGraph.Text
                Case 1
                    nLabelX.Text = nNum(n) & " Stride"
                Case 13, 23, 33, 43, 53
                    If n <> 0 Then
                        nLabelX.Text = nNum(n - 1) & " Harmonic"
                    End If
                Case Else
                    nLabelX.Text = n * 10 & "%"
            End Select
            Me.Controls.Add(nLabelX)
            nLabelX.Visible = True
            nLabelX.Top = Me.Height - 1.1 * nLabelX.Height
            nLabelX.Left = pnlGraph.Left + n * (pnlGraph.Width / num) - 0.5 * nLabelX.Width
            nLabelX.AutoSize = True
        Next n

    End Sub

    Private Sub subCreateYLabels()
        Dim num As Integer
        Dim nMaxYLabelWidth As Integer
        Dim nGraph As Integer = Val(lblWhichGraph.Text)

        If nGraph < 1 Then Exit Sub

        For Me.n = Me.Controls.Count To 1 Step -1 'First remove all the previous Y labels
            If Mid(Me.Controls(n - 1).Tag, 1, 6) = "YLabel" Then
                Me.Controls.Remove(Me.Controls(n - 1))
            End If
        Next n

        Dim controlN As Control
        For Each controlN In Me.Controls
            If controlN.Name.StartsWith("btn") Then
                MsgBox(controlN.Name)
            End If
        Next

        Select Case nGraph
            Case 1, 2, 3, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 83, 84, 85, 86
                num = 10
            Case 13, 23, 33, 43, 53
                num = 12
            Case 14, 24, 34, 44, 54
                num = 26
            Case 15, 25, 35, 45, 55
                num = 14
        End Select

        For Me.n = 0 To num
            Dim nLabel As New Label
            nLabel.Tag = "YLabel " & n
            nLabel.AutoSize = True
            Dim sLabelText As String
            Dim nLabelValue As Single
            Select Case nGraph
                Case 1, 2, 3
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph)
                        nLabel.Text = nLabelValue.ToString("0") & " Lbs."
                    Else
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) * Lbs_To_Kgs
                        nLabel.Text = nLabelValue.ToString("0") & " Kgs."
                    End If
                Case 10, 11, 12
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph)
                        nLabel.Text = nLabelValue.ToString("0.0") & " Lbs."
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Lbs_To_Kgs
                        nLabel.Text = nLabelValue.ToString("0.0") & " Kgs."
                    End If
                Case 13
                    If n = 0 Then 'For the amplitudes, you don't need a 0th harmonic.
                        Exit Select
                    End If
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = arHarmonicValuesForce(i, 2)
                        nLabel.Text = nLabelValue.ToString("0.0###") & " Lbs."
                    Else
                        nLabelValue = arHarmonicValuesForce(i, 2) * Lbs_To_Kgs
                        nLabel.Text = nLabelValue.ToString("0.0###") & " Kgs."
                    End If
                Case 20, 21, 22
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Feet_To_In
                        nLabel.Text = nLabelValue.ToString("F2") & " in."
                    Else
                        nLabelValue = 0.2 * (n - 5) * Val(lblMaximumYValue.Text) * Feet_To_cm
                        nLabel.Text = nLabelValue.ToString("F1") & " cm."
                    End If
                Case 23
                    If n = 0 Then 'For the amplitudes, you don't need a 0th harmonic.
                        Exit Select
                    End If
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = arHarmonicValuesDisplacement(n, 2) * Feet_To_In
                        nLabel.Text = nLabelValue.ToString("0.0###") & " in."
                    Else
                        nLabelValue = arHarmonicValuesDisplacement(n, 2) * Feet_To_cm
                        nLabel.Text = nLabelValue.ToString("0.0###") & " cm."
                    End If
                Case 30, 31, 32
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Feet_To_In
                        nLabel.Text = nLabelValue.ToString("0.0") & " in./sec."
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Feet_To_cm
                        nLabel.Text = nLabelValue.ToString("0.0") & " cm./sec."
                    End If
                Case 33
                    If n = 0 Then 'For the amplitudes, you don't need a 0th harmonic.
                        Exit Select
                    End If
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = arHarmonicValuesVelocity(n, 2) * Feet_To_In
                        nLabel.Text = nLabelValue.ToString("0.0##") & " in./sec."
                    Else
                        nLabelValue = arHarmonicValuesVelocity(n, 2) * Feet_To_cm
                        nLabel.Text = nLabelValue.ToString("0.0##") & " cm./sec."
                    End If
                Case 40, 41, 42
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph)
                        nLabel.Text = nLabelValue.ToString("0.0##") & " Ft.-Lbs./sec."
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * FtLbs_To_NewtonM
                        nLabel.Text = nLabelValue.ToString("0.0##") & " N-m/sec."
                    End If
                Case 43
                    If n = 0 Then 'For the amplitudes, you don't need a 0th harmonic.
                        Exit Select
                    End If
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = arHarmonicValuesPower(n, 2)
                        nLabel.Text = nLabelValue.ToString("0.###") & " Ft-Lbs./sec."
                    Else
                        nLabelValue = arHarmonicValuesPower(n, 2) * FtLbs_To_NewtonM
                        nLabel.Text = nLabelValue.ToString("0.###") & " N-m/sec."
                    End If
                Case 50, 51, 52
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph)
                        nLabel.Text = nLabelValue.ToString("0.####") & " Ft.-Lbs."
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * FtLbs_To_NewtonM
                        nLabel.Text = nLabelValue.ToString("0.####") & " N-m."
                    End If
                Case 53
                    If n = 0 Then 'For the amplitudes, you don't need a 0th harmonic.
                        Exit Select
                    End If
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = arHarmonicValuesWork(n, 2)
                        nLabel.Text = nLabelValue.ToString("0.####") & " Ft.-Lbs."
                    Else
                        nLabelValue = arHarmonicValuesWork(n, 2) * FtLbs_To_NewtonM
                        nLabel.Text = nLabelValue.ToString("0.####") & " N.-m."
                    End If
                Case 60
                    If bEnglishOrMetricUnits = False Then
                        If n = 0 Then
                            nLabel.Text = "minimum"
                        Else
                            nLabelValue = 0.1 * n * arMaximumYValues(nGraph)
                            nLabel.Text = "+" & nLabelValue.ToString("0.##") & " Ft.-lbs."
                        End If
                    Else
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) * FtLbs_To_NewtonM
                        nLabel.Text = nLabelValue.ToString("0.##") & " N.-m."
                    End If
                Case 70
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) / Feet_To_In
                        nLabel.Text = nLabelValue.ToString("0.#") & " Lbs./in."
                    Else
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) * Lbs_To_Kgs / Feet_To_cm
                        nLabel.Text = nLabelValue.ToString("0.##") & " Kg./cm."
                    End If
                Case 81
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) * Sensels_To_Inches
                        nLabel.Text = nLabelValue.ToString("0.#") & " in."
                    Else
                        nLabelValue = 0.1 * n * arMaximumYValues(nGraph) * Sensels_To_Cm
                        nLabel.Text = nLabelValue.ToString("0.0") & " cm."
                    End If
                Case 82
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.1 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Inches
                        nLabel.Text = nLabelValue.ToString("0.0") & " in."
                    Else
                        nLabelValue = 0.1 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Cm
                        nLabel.Text = nLabelValue.ToString("0.0") & " cm."
                    End If
                Case 83, 84
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Inches
                        nLabel.Text = nLabelValue.ToString("0.0") & " in./sec"
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Cm
                        nLabel.Text = nLabelValue.ToString("0.0") & " cm./sec"
                    End If
                Case 85, 86
                    If bEnglishOrMetricUnits = False Then
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Inches
                        nLabel.Text = nLabelValue.ToString("0.0") & " in./sec/sec"
                    Else
                        nLabelValue = 0.2 * (n - 5) * arMaximumYValues(nGraph) * Sensels_To_Cm
                        nLabel.Text = nLabelValue.ToString("0.0") & " cm./sec/sec"
                    End If
            End Select
            Me.Controls.Add(nLabel)
            nLabel.Visible = True
            If nMaxYLabelWidth < nLabel.Width Then nMaxYLabelWidth = nLabel.Width 'This gets the maximum width of a Y Label
            nLabel.Top = pnlGraph.Top + n * pnlGraph.Height / 10 + 0.5 * nLabel.Height
        Next n

        'Reset the left value and the width value of the Graph Panel
        For Me.n = Me.Controls.Count To 1 'This finds the label that is the widest
            Select Case nGraph
                Case 1, 2, 60, 70, 81
                    If Me.Controls.Item(n - 1).Tag = "YLabel 10" Then
                        Exit For
                    End If
                Case 10, 11, 12, 13, 20, 21, 22, 23, 30, 31, 32, 33, 40, 41, 42, 43, 50, 51, 52, 53, 82, 83, 84, 85, 86
                    If Me.Controls.Item(n - 1).Tag = "YLabel 0" Then
                        Exit For
                    End If
            End Select
        Next

        pnlGraph.Left = pnlYLabel.Left + pnlYLabel.Width + nMaxYLabelWidth + 20
        pnlGraph.Width = Me.Width - pnlGraph.Left - 10

    End Sub

    Private Sub subSetYMaximumValues(ByVal nGraph As Integer, ByVal nMaxValue As Double)
        'This sub takes the Y maximum values and resets it so that the number is easily divisible and the gridlines easy to read.
        'Pass the graph number and also the nMaxValue value that is currently in the arMaximumYValues list or from the rescale
        Dim nUnitsForEachMajorGridline_Y As Double
        Dim nDiv, nNum, nRemainder As Integer

        Select Case nGraph
            Case 1, 2, 3
                If bEnglishOrMetricUnits = False Then
                    nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue
                Else
                    nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue * Lbs_To_Kgs
                End If
                If nUnitsForEachMajorGridline_Y <= 1 Then
                    nUnitsForEachMajorGridline_Y = 1 'The minimum that you can record is 10 lbs.
                ElseIf nUnitsForEachMajorGridline_Y <= 5 Then 'If the maximum value is between 10 -50 lbs.
                    nNum = Int(nUnitsForEachMajorGridline_Y + 1) 'The maximum increases by 10 lb increments.
                    nDiv = Math.DivRem(nNum, 1, nRemainder)
                    If nRemainder <> 0 Then
                        nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 1)
                    Else
                        nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y)
                    End If
                Else 'If the maximum value is 60 or greater, then increment the maximum value by 20 lb. increments
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.01 Then nNum = nNum + 1
                    nDiv = Math.DivRem(nNum, 2, nRemainder)
                    If nRemainder <> 0 Then
                        nDiv = nDiv + 1
                    End If
                    nUnitsForEachMajorGridline_Y = nDiv * 2
                End If
                If bEnglishOrMetricUnits = False Then
                    arMaximumYValues(nGraph) = 10 * nUnitsForEachMajorGridline_Y
                Else
                    arMaximumYValues(nGraph) = 10 * nUnitsForEachMajorGridline_Y / Lbs_To_Kgs
                End If
            Case 10, 11, 12, 13
                If bEnglishOrMetricUnits = False Then
                    nUnitsForEachMajorGridline_Y = nMaxValue / 5
                ElseIf bEnglishOrMetricUnits = True Then
                    nUnitsForEachMajorGridline_Y = nMaxValue * Lbs_To_Kgs / 5
                End If
                If nGraph = 13 Then nUnitsForEachMajorGridline_Y = nUnitsForEachMajorGridline_Y / 2
                If nUnitsForEachMajorGridline_Y < 0.2 Then
                    nUnitsForEachMajorGridline_Y = 0.2 'The lowest maximum value you can go is 1 lb.
                ElseIf nUnitsForEachMajorGridline_Y <= 1 Then 'Your can set the maximum by 1# units from 1 to 5.  Each gridline is a multiple of 0.2
                    nNum = Int(nUnitsForEachMajorGridline_Y * 5)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.05 Then nNum = nNum + 1
                    nUnitsForEachMajorGridline_Y = 0.2 * nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 10 Then 'You can set the maximum by 5# units , from 5 to 50. Each gridline is a multiple of 1.0
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.05 Then nNum = nNum + 1
                    nUnitsForEachMajorGridline_Y = nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 20 Then 'If the maximum value is between 50-100 then each gridline is a multiple of 2, (i.e. 12,14,16,18,20) so that maximum values increase by 60,70,80,90,100
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 2, nRemainder)
                    If nRemainder <> 0 Then
                        nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 1)
                    Else
                        If nUnitsForEachMajorGridline_Y - Int(nUnitsForEachMajorGridline_Y) <= 0.01 Then
                            nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y)
                        Else
                            nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 2)
                        End If
                    End If
                ElseIf nUnitsForEachMajorGridline_Y <= 40 Then 'For values between 100 and 200, increment each gridline by a multipe of 4, so that the maximum increments by 20 (i.e. 120,140,160,180,200, etc.)
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 4, nRemainder)
                    Select Case nRemainder
                        Case 0
                            If nUnitsForEachMajorGridline_Y - Int(nUnitsForEachMajorGridline_Y) <= 0.01 Then
                                nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y)
                            Else
                                nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 4)
                            End If
                        Case 1
                            nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 3)
                        Case 2
                            nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 2)
                        Case 3
                            nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 1)
                    End Select
                Else
                End If
                If bEnglishOrMetricUnits = False Then
                    arMaximumYValues(nGraph) = nUnitsForEachMajorGridline_Y * 5
                Else
                    arMaximumYValues(nGraph) = nUnitsForEachMajorGridline_Y * 5 / Lbs_To_Kgs
                End If
                If nGraph = 13 Then arMaximumYValues(nGraph) = arMaximumYValues(nGraph) * 2
            Case 20, 21, 22, 23, 30, 31, 32, 33
                If bEnglishOrMetricUnits = False Then
                    nUnitsForEachMajorGridline_Y = nMaxValue * Feet_To_In / 5
                ElseIf bEnglishOrMetricUnits = True Then
                    nUnitsForEachMajorGridline_Y = nMaxValue * Feet_To_cm / 5
                End If
                If nGraph = 23 Or nGraph = 33 Then
                    nUnitsForEachMajorGridline_Y = 0.5 * nUnitsForEachMajorGridline_Y
                End If
                If nUnitsForEachMajorGridline_Y <= 0.1 Then 'The smallest maximum value is 0.5".   
                    nUnitsForEachMajorGridline_Y = 0.1
                ElseIf nUnitsForEachMajorGridline_Y <= 0.15 Then 'The next maximum value is .75"
                    nUnitsForEachMajorGridline_Y = 0.15
                ElseIf nUnitsForEachMajorGridline_Y <= 0.2 Then 'The next maximum value is 1"
                    nUnitsForEachMajorGridline_Y = 0.2
                ElseIf nUnitsForEachMajorGridline_Y <= 0.25 Then 'The next maximum value is 1.25"
                    nUnitsForEachMajorGridline_Y = 0.25
                ElseIf nUnitsForEachMajorGridline_Y <= 1 Then 'From 1.25 to 5, increase the maximum by .5", i.e. 1.5, 2, 2.5, etc.
                    If nUnitsForEachMajorGridline_Y - 0.1 * Int(10 * nUnitsForEachMajorGridline_Y) < 0.005 Then
                        nUnitsForEachMajorGridline_Y = 0.1 * Int(10 * nUnitsForEachMajorGridline_Y)
                    Else
                        nUnitsForEachMajorGridline_Y = 0.1 * (Int(10 * nUnitsForEachMajorGridline_Y + 1))
                    End If
                ElseIf nUnitsForEachMajorGridline_Y <= 1.25 Then 'The next maximum value is 6.25"
                    nUnitsForEachMajorGridline_Y = 1.25
                ElseIf nUnitsForEachMajorGridline_Y <= 1.5 Then 'The next maximum value is 7.5"
                    nUnitsForEachMajorGridline_Y = 1.5
                ElseIf nUnitsForEachMajorGridline_Y <= 2 Then 'The next maximum value is 10"
                    nUnitsForEachMajorGridline_Y = 2
                ElseIf nUnitsForEachMajorGridline_Y <= 2.5 Then 'The next maximum value is 12.5"
                    nUnitsForEachMajorGridline_Y = 2.5
                ElseIf nUnitsForEachMajorGridline_Y <= 10 Then 'The next maximum value is 15, then it increases in increments by 5, i.e. 15,20, etc, up to 50.
                    If nUnitsForEachMajorGridline_Y - Int(nUnitsForEachMajorGridline_Y) < 0.005 Then
                        nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y)
                    Else
                        nUnitsForEachMajorGridline_Y = Int(nUnitsForEachMajorGridline_Y + 1)
                    End If
                ElseIf nUnitsForEachMajorGridline_Y <= 20 Then 'For the maximum values of 50-100, increase the maximum value by 10, i.e. 60,70,80,90,100
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 2, nRemainder)
                    If nRemainder <> 0 Then
                        nUnitsForEachMajorGridline_Y = nNum + 1
                    Else
                        If nUnitsForEachMajorGridline_Y - nNum < 0.005 Then
                            nUnitsForEachMajorGridline_Y = nNum
                        Else
                            nUnitsForEachMajorGridline_Y = nNum + 2
                        End If
                    End If
                End If
                If bEnglishOrMetricUnits = False Then
                    arMaximumYValues(nGraph) = nUnitsForEachMajorGridline_Y * 5 / Feet_To_In
                Else
                    arMaximumYValues(nGraph) = nUnitsForEachMajorGridline_Y * 5 / Feet_To_cm
                End If
                If nGraph = 23 Or nGraph = 33 Then arMaximumYValues(nGraph) = arMaximumYValues(nGraph) * 2
            Case 40, 41, 42, 43, 50, 51, 52, 53, 60
                If bEnglishOrMetricUnits = False Then
                    If nGraph = 60 Then
                        nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue
                    Else
                        nUnitsForEachMajorGridline_Y = 0.2 * nMaxValue
                    End If
                Else
                    If nGraph = 60 Then
                        nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue * FtLbs_To_NewtonM
                    Else
                        nUnitsForEachMajorGridline_Y = 0.2 * nMaxValue * FtLbs_To_NewtonM
                    End If
                End If
                If nGraph = 43 Or nGraph = 53 Then
                    nUnitsForEachMajorGridline_Y = nUnitsForEachMajorGridline_Y / 2
                End If
                If nUnitsForEachMajorGridline_Y <= 0.05 Then 'If the maximum value is less than 0.25 then
                    nNum = Int(100 * nUnitsForEachMajorGridline_Y)
                    If 100 * nUnitsForEachMajorGridline_Y - nNum > 0.05 Then 'Increase the maximum value by .05, ie. .05, .10, .15, .20 and .25
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 0.01 * nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 1.0 Then 'If the maximum value is less than 5.0 then increase the maximum value by .25, i.e. 0.25, .5, .75, 1.0, 1.25, .... up to 5.0
                    nNum = Int(100 * nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 5, nRemainder)
                    If nRemainder <> 0 Then
                        nDiv = nDiv + 1
                    Else
                        If nUnitsForEachMajorGridline_Y - 0.01 * nNum > 0.001 Then
                            nDiv = nDiv + 1
                        End If
                    End If
                    nUnitsForEachMajorGridline_Y = 0.05 * nDiv
                ElseIf nUnitsForEachMajorGridline_Y <= 5 Then 'If the maximum value is between 5 and 25 then increase the maximum by 2.5 (i.e. 5, 7.5, 10, 12.5...., 25)
                    nNum = Int(10 * nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 5, nRemainder)
                    If nRemainder <> 0 Then
                        nDiv = nDiv + 1
                    Else
                        If nUnitsForEachMajorGridline_Y - 0.1 * nNum >= 0.005 Then
                            nDiv = nDiv + 1
                        Else
                            nUnitsForEachMajorGridline_Y = nDiv
                        End If
                    End If
                    nUnitsForEachMajorGridline_Y = 0.5 * nDiv
                ElseIf nUnitsForEachMajorGridline_Y <= 20 Then 'If the maximum value is between 25-100 then increase the maximum value by 5 (i.e. 25, 30, 35, 40, ..., 100)
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.005 Then
                        nUnitsForEachMajorGridline_Y = nNum + 1
                    Else
                        nUnitsForEachMajorGridline_Y = nNum
                    End If
                ElseIf nUnitsForEachMajorGridline_Y <= 50 Then 'If the maximum value is between 100-250 then increase the max value by 10 (i.e. 100, 110, 120, 130, ..., 250)
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 5, nRemainder)
                    If nUnitsForEachMajorGridline_Y - nNum >= 0.1 Then
                        nDiv = nDiv + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 5 * nDiv
                Else
                    nUnitsForEachMajorGridline_Y = 5 * Int(nUnitsForEachMajorGridline_Y + 1)
                End If
                If bEnglishOrMetricUnits = True Then
                    nUnitsForEachMajorGridline_Y = nUnitsForEachMajorGridline_Y / FtLbs_To_NewtonM
                End If
                If nGraph = 60 Then
                    arMaximumYValues(nGraph) = 10 * nUnitsForEachMajorGridline_Y
                Else
                    arMaximumYValues(nGraph) = 5 * nUnitsForEachMajorGridline_Y
                End If
                If nGraph = 43 Or nGraph = 53 Then
                    arMaximumYValues(nGraph) = 2 * arMaximumYValues(nGraph)
                End If
            Case 70
                If bEnglishOrMetricUnits = False Then
                    nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue / Feet_To_In
                Else
                    nUnitsForEachMajorGridline_Y = 0.1 * nMaxValue * Lbs_To_Newtons / Feet_To_cm
                End If
                If nUnitsForEachMajorGridline_Y <= 1 Then
                    nUnitsForEachMajorGridline_Y = 1
                ElseIf nUnitsForEachMajorGridline_Y <= 10 Then
                    nNum = Int(2 * nUnitsForEachMajorGridline_Y)
                    If 2 * nUnitsForEachMajorGridline_Y - nNum > 0.005 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 0.5 * nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 30 Then
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.01 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = nNum
                End If
                If bEnglishOrMetricUnits = False Then
                    arMaximumYValues(nGraph) = 10 * nUnitsForEachMajorGridline_Y * Feet_To_In
                Else
                    arMaximumYValues(nGraph) = 10 * nUnitsForEachMajorGridline_Y * Feet_To_cm / Lbs_To_Newtons
                End If
            Case 83, 84, 85, 86
                If bEnglishOrMetricUnits = False Then
                    nUnitsForEachMajorGridline_Y = 0.2 * nMaxValue * Sensels_To_Inches
                Else
                    nUnitsForEachMajorGridline_Y = 0.2 * nMaxValue * Sensels_To_Cm
                End If
                If nUnitsForEachMajorGridline_Y <= 1 Then 'The smallest maximum value is 0.5.  It can be increased by values of 0.5 up to a maximum value of 5.
                    nNum = Int(10 * nUnitsForEachMajorGridline_Y)
                    If 10 * nUnitsForEachMajorGridline_Y - nNum >= 0.01 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 0.1 * nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 5 Then 'The If the maximum value is 5 - 25 then increase the maximum values by 2.5, i.e. 5, 7.5, 10, 12.5 ...
                    nNum = Int(10 * nUnitsForEachMajorGridline_Y)
                    nDiv = Math.DivRem(nNum, 5, nRemainder)
                    If nRemainder <> 0 Then
                        nDiv = nDiv + 1
                    Else
                        If nUnitsForEachMajorGridline_Y - 0.1 * nNum >= 0.005 Then
                            nDiv = nDiv + 1
                        Else
                        End If
                    End If
                    nUnitsForEachMajorGridline_Y = 0.5 * nDiv
                ElseIf nUnitsForEachMajorGridline_Y <= 25 Then 'If the maximum value is 25 - 125 then increase the maximum values by 5 (i.e. 25, 30, 35, ... , 125)
                    nNum = Int(nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - nNum > 0.01 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 100 Then 'If the maiximum value is 125 - 500 then increase the maximum values by 25 (i.e. 125, 150, 175, ... , 500)
                    nNum = Int(nUnitsForEachMajorGridline_Y / 5)
                    If nUnitsForEachMajorGridline_Y - 5 * nNum > 0.05 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 5 * nNum
                ElseIf nUnitsForEachMajorGridline_Y <= 250 Then 'If the maximum value is 500 - 1250 then increase the maximum values by 50 (i.e. 500, 550, 600, ..., 1250)
                    nNum = Int(0.1 * nUnitsForEachMajorGridline_Y)
                    If nUnitsForEachMajorGridline_Y - 10 * nNum > 0.05 Then
                        nNum = nNum + 1
                    End If
                    nUnitsForEachMajorGridline_Y = 10 * nNum
                End If
                If bEnglishOrMetricUnits = False Then
                    arMaximumYValues(nGraph) = 5 * nUnitsForEachMajorGridline_Y / Sensels_To_Inches
                Else
                    arMaximumYValues(nGraph) = 5 * nUnitsForEachMajorGridline_Y / Sensels_To_Cm
                End If
        End Select

    End Sub

    '  Private Sub pnlGraph_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlGraph.MouseEnter
    '     ToolTip1.Active = True
    'End Sub

    'Private Sub pnlGraph_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlGraph.MouseLeave
    '   ToolTip1.Active = False
    'End Sub

    Private Sub pnlGraph_MouseHover(sender As Object, e As System.EventArgs) Handles pnlGraph.MouseHover

    End Sub


    Private Sub pnlGraph_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlGraph.MouseDown
        ToolTip1.AutoPopDelay = 1700
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim structCursorPosition As POINTAPI
            Dim pointColor As Color
            'Dim numhdc As Integer
            Dim nPctGait As Integer
            Dim nData As Double
            Dim nValue As Single
            Dim sTip As String

            Select Case lblWhichGraph.Text 'Set the Tool Tip Text Title
                Case 1, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55, 80
                Case Else
                    nPctGait = e.X / pnlGraph.Width * lblMaximumXValue.Text
                    If nPctGait > 100 Then
                        Dim nn As Integer
                        nn = Int(nPctGait / 100) * 100
                        nPctGait = nPctGait - nn
                    End If
                    ToolTip1.ToolTipTitle = nPctGait & "% of Gait Cycle"
            End Select

            Select Case lblWhichGraph.Text
                Case 2, 3, 10, 11, 12
                    ' ToolTip1.SetToolTip(pnlGraph, "Force: ")
                    If lblWhichGraph.Text = conForce_Avg Then 'This means that we are showing the average force for both feet
                        If bEnglishOrMetricUnits = False Then
                            nValue = arTotalForce(nPctGait)
                            sTip = "Total Force = " & nValue.ToString("0.0") & "Lbs"
                            If arLeftForce(nPctGait) <> 0 And arRightForce(nPctGait) <> 0 Then
                                nValue = arLeftForce(nPctGait)
                                sTip = sTip & Chr(13) & "Left Foot = " & nValue.ToString("0.0") & " Lbs"
                                nValue = arLeftForce(nPctGait) / arTotalForce(nPctGait)
                                sTip = sTip & " - " & 100 * nValue.ToString("0.000") & "%" & Chr(13)
                                nValue = arRightForce(nPctGait)
                                sTip = sTip & "Right Foot = " & nValue.ToString("0.0") & "Lbs"
                                nValue = arRightForce(nPctGait) / arTotalForce(nPctGait)
                                sTip = sTip & " - " & 100 * nValue.ToString("0.000") & "%"
                            End If
                        End If
                    ElseIf lblWhichGraph.Text = conForce_As_BW Then 'this is number 10
                        If bEnglishOrMetricUnits = False Then
                            nValue = arBodyWeight(nPctGait)
                            sTip = nValue.ToString("0.0") & " Lbs"
                        End If
                    End If
                    ToolTip1.SetToolTip(pnlGraph, sTip)
                Case 20, 21, 22
                        nData = pnlGraph.Height / 2 - e.Y
                        If bEnglishOrMetricUnits = 0 Then
                            nValue = arDisplacement(nPctGait) * Feet_To_In
                            sTip = nValue.ToString("0.00") & " in."
                        Else
                            nValue = arDisplacement(nPctGait) * Feet_To_cm
                            sTip = nValue.ToString("0.0") & " cm."
                        End If
                        ToolTip1.SetToolTip(pnlGraph, "Displacement: " & sTip)
                Case 30, 31, 32
                        ToolTip1.SetToolTip(pnlGraph, "Velocity: " & arCoMVelocity(nPctGait))
                        If bEnglishOrMetricUnits = 0 Then
                            nValue = arCoMVelocity(nPctGait) * Feet_To_In
                            sTip = nValue.ToString("0.0") & " in./sec."
                        Else
                            nValue = arCoMVelocity(nPctGait) * Feet_To_cm
                            sTip = nValue.ToString("0") & " cm./sec."
                        End If
                Case 40, 41, 42
                        ToolTip1.SetToolTip(pnlGraph, "Power: " & arPower(nPctGait))
                Case 50, 51, 52
                        ToolTip1.SetToolTip(pnlGraph, "Work: " & arWork(nPctGait))
                Case 60
                        ToolTip1.SetToolTip(pnlGraph, "Total Energy: " & arEnergy_Total(nPctGait))
                Case 70
                        ToolTip1.SetToolTip(pnlGraph, "Spring Constant: " & Format(arSpringConstants(nPctGait) / 12, "0.0") & " lbs./in")
                Case 81
            End Select
            Call GetCursorPos(structCursorPosition)
            Dim lColor As Integer = GetPixel(GetDC(0), structCursorPosition.x, structCursorPosition.y)
            pointColor = System.Drawing.ColorTranslator.FromOle(lColor)
            Dim strRgb As String = Microsoft.VisualBasic.Right("000000" & Hex(lColor), 6)
            'Text = "R:" & Microsoft.VisualBasic.Right(strRgb, 2) & " G:" & Mid(strRgb, 3, 2) & " B:" & Microsoft.VisualBasic.Left(strRgb, 2)
            If pointColor <> colorBackground And pointColor <> colorGrid Then
                If pointColor = colorLeft Then
                ElseIf pointColor = colorRight Then
                ElseIf pointColor = colorBoth Then
                End If
            End If
        End If
    End Sub

    Private Sub pnlGraph_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlGraph.SizeChanged

        If bFormLoading = False Then
            lblGraphTitle.AutoSize = True
            subChangePanelFontSize()
            lblPatientName.Left = pnlGraph.Left
            lblExamDate.Left = pnlGraph.Left + pnlGraph.Width - lblExamDate.Width
            lblGraphTitle.Left = pnlGraph.Left + 0.5 * (pnlGraph.Width - lblGraphTitle.Width)
        End If

    End Sub

    Private Sub OnPrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim hwndForm As IntPtr
        hwndForm = Me.Handle

        Dim hdcDIBSection As IntPtr
        Dim hdcRef As IntPtr
        Dim hbmDIBSection As IntPtr
        Dim hbmDIBSectionOld As IntPtr
        Dim BMPheader As Win32APICall.BITMAPINFOHEADER

        hdcRef = Win32APICall.GetDC(IntPtr.Zero)
        hdcDIBSection = Win32APICall.CreateCompatibleDC(hdcRef)
        Win32APICall.ReleaseDC(IntPtr.Zero, hdcRef)

        BMPheader.biBitCount = 24
        BMPheader.biClrImportant = 0
        BMPheader.biClrUsed = 0
        BMPheader.biCompression = Win32APICall.BI_RGB
        BMPheader.biSize = 40
        BMPheader.biHeight = Me.Height
        BMPheader.biPlanes = 1
        BMPheader.biSizeImage = 0
        BMPheader.biWidth = Me.Width
        BMPheader.biXPelsPerMeter = 0
        BMPheader.biYPelsPerMeter = 0

        hbmDIBSection = Win32APICall.CreateDIBSection(hdcDIBSection, BMPheader, Win32APICall.DIB_RGB_COLORS, _
        IntPtr.Zero, IntPtr.Zero, 0)

        hbmDIBSectionOld = Win32APICall.SelectObject(hdcDIBSection, hbmDIBSection)
        Win32APICall.PatBlt(hdcDIBSection, 0, 0, Me.Width, Me.Height, Win32APICall.WHITENESS)
        Win32APICall.PrintWindow(hwndForm, hdcDIBSection, 0)
        Win32APICall.SelectObject(hdcDIBSection, hbmDIBSectionOld)

        Dim imageFrm As Bitmap
        imageFrm = Image.FromHbitmap(hbmDIBSection)
        e.Graphics.DrawImage(imageFrm, 0, 0)

        Win32APICall.DeleteDC(hdcDIBSection)
        Win32APICall.DeleteObject(hbmDIBSection)
    End Sub

    Private Sub pnlYLabel_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlYLabel.Paint
        Dim gL As Graphics = e.Graphics
        Dim gLTextBox As SizeF
        Dim sAxisLabel As String = "Y Axis Label"
        gL.Clear(colorBackground)

        'Identify the Label that will be put into the text box
        Select Case lblWhichGraph.Text
            Case conForce_AllSteps, conForce_Avg, conForce_As_BW, conForce_Harm_Sum, conForce_Harm_Diff
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Force (lbs.)"
                Else
                    sAxisLabel = "Force (kgs.)"
                End If
            Case conDisplacement_Vert, conDisp_Harm_Sum, conDisp_Harm_Diff, conCoP_AP, conCoP_AP_ML
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Displacement (in.)"
                Else
                    sAxisLabel = "Displacement (cm.)"
                End If
            Case conVelocity_Vert, conVel_Harm_Sum, conVel_Harm_Diff, conCoP_AP_Vel, conCoP_ML_Vel
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Velocity (in.sec.)"
                Else
                    sAxisLabel = "Velocity (cm./sec.)"
                End If
            Case conPower_Vert, conPower_Harm_Sum, conPower_Harm_Diff
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Power (Ft.-Lbs. / Sec)"
                Else
                    sAxisLabel = "Power (Newton-M / Sec.)"
                End If
            Case conWork_Vert, conWork_Harm_Sum, conWork_Harm_Diff
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Work (Ft.-Lbs.)"
                Else
                    sAxisLabel = "Work (Newton-M)"
                End If
            Case conEnergy
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Energy (Ft.-Lbs.)"
                Else
                    sAxisLabel = "Energy (Newton-M)"
                End If
            Case conCoP_AP_Acc, conCoP_ML_Acc
                If bEnglishOrMetricUnits = False Then
                    sAxisLabel = "Acceleration (in./sec./sec.)"
                Else
                    sAxisLabel = "Acceleration (cm./sec./sec.)"
                End If
        End Select

        pnlYLabel.Left = 5
        pnlYLabel.Width = 0.31 * pnlGraph.Left
        gLTextBox = gL.MeasureString(sAxisLabel, Me.Font)
        gL.RotateTransform(-90)
        gL.DrawString(sAxisLabel, Me.Font, Brushes.Chocolate, -0.5 * (pnlYLabel.Height + gLTextBox.Width), 1)

        Select Case lblWhichGraph.Text
            Case conForce_As_BW, conForce_Harm_Sum, conForce_Harm_Diff
                sAxisLabel = vbCrLf & "<--- Less than body weight   |   More than body weight --->"
                gLTextBox = gL.MeasureString(sAxisLabel, Me.Font)
                gL.DrawString(sAxisLabel, Me.Font, Brushes.Chocolate, -0.5 * (pnlYLabel.Height + gLTextBox.Width), 2)
            Case conCoP_AP, conCoP_AP_Vel, conCoP_AP_Acc
                sAxisLabel = vbCrLf & "<--- Posterior   |   Anterior --->"
                gLTextBox = gL.MeasureString(sAxisLabel, Me.Font)
                gL.DrawString(sAxisLabel, Me.Font, Brushes.Chocolate, -0.5 * (pnlYLabel.Height + gLTextBox.Width), 2)
            Case conCoP_ML, conCoP_ML_Vel, conCoP_ML_Acc
                sAxisLabel = vbCrLf & "<--- Lateral   |   Medial --->"
                gLTextBox = gL.MeasureString(sAxisLabel, Me.Font)
                gL.DrawString(sAxisLabel, Me.Font, Brushes.Chocolate, -0.5 * (pnlYLabel.Height + gLTextBox.Width), 2)
        End Select
    End Sub

    Private Sub pnlGraph_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlGraph.Paint

        Dim gP As Graphics = e.Graphics
        Dim nGraphNum As Integer
        Dim nBorder As Integer
        Dim nValue As Single

        Dim nPixelsPerPercent_X, nPixelsPerUnit_Y, nPixelsPerUnit_X As Double
        Dim nNumberOfXMajorGridLines As Short 'This is to tell you how many X gridlines to draw.
        Dim Point1, Point2 As Point

        nGraphNum = Val(lblWhichGraph.Text)
        pnlGraph.BackColor = colorBackground
        nBorder = 0.5 * (pnlGraph.Width - pnlGraph.ClientRectangle.Width)
        If nGraphNum = 81 Or nGraphNum = 83 Then
            butSuperimpose.Visible = True
            butSuperimpose.Left = pnlGraph.Width - butSuperimpose.Width - 2
            butSuperimpose.Top = 2
        Else
            butSuperimpose.Text = "Compare Left and Right"
            butSuperimpose.FlatStyle = FlatStyle.Standard
            butSuperimpose.Visible = False
        End If

100:    'STEP 1:  Find out how many pixels make up each percent of the gait cycle along the X axis 
        Select Case nGraphNum
            Case 1
                nPixelsPerPercent_X = pnlGraph.ClientRectangle.Width / (100 * nNumberOfStrides)
            Case 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 81, 82, 83, 84, 85, 86
                nPixelsPerPercent_X = (pnlGraph.ClientRectangle.Width) / arMaximumXValues(nGraphNum)
                If butSuperimpose.Visible = True And butSuperimpose.Text = "Show full gait cycle" Then
                    nPixelsPerPercent_X = pnlGraph.ClientRectangle.Width / 100
                End If
                Select Case nGraphNum 'Find out how many pixels are needed for each unit of Y
                    Case conForce_Avg, conEnergy, conSpringConstants, conCoP_AP
                        nPixelsPerUnit_Y = pnlGraph.Height / arMaximumYValues(nGraphNum)
                    Case Else
                        nPixelsPerUnit_Y = pnlGraph.Height / (2 * arMaximumYValues(nGraphNum))
                End Select
            Case conForce_AllSteps 'This is also case 1
            Case conForce_Radial 'This is also case 3
                If pnlGraph.Width <= pnlGraph.Height Then
                    nPixelsPerPercent_X = 0.009 * pnlGraph.Width
                Else
                    nPixelsPerPercent_X = 0.009 * pnlGraph.Height
                End If
            Case 13, 23, 33, 43, 53
                nPixelsPerUnit_X = pnlGraph.ClientRectangle.Width / arMaximumYValues(nGraphNum - 3)
            Case conForce_Harm_Eq, conForce_Harm_Eq_Angle
            Case conDisp_Harm_Eq, conDisp_Harm_Eq_Angle
            Case conVel_Harm_Eq, conVel_Harm_Eq_Angle
            Case conPower_Harm_Eq, conPower_Harm_Eq_Angle
            Case conWork_Harm_Eq, conWork_Harm_Eq_Angle
            Case conCoP_AP_ML
        End Select

200:    'STEP 2:  Draw the X Axis
        Select Case nGraphNum
            Case Is = 1, 2, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55
            Case 3
                penPix.Width = 2
                penPix.DashStyle = Drawing2D.DashStyle.Dash
            Case Else
                penPix.Width = 4
                penPix.Color = colorGrid
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point2.X = pnlGraph.Width
                Select Case nGraphNum
                    Case conForce_Avg, conEnergy, conSpringConstants, conCoP_AP 'These graphs draw the X axis across the bottom of the graph
                        Point1.Y = pnlGraph.Height - 4
                        Point2.Y = Point1.Y
                    Case Else
                        Point1.Y = (pnlGraph.Height - 4) / 2
                        Point2.Y = Point1.Y
                End Select
                gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
        End Select

300:    'STEP 3:  Draw the Y axis along the left side of the bitmap
        Select Case nGraphNum
            Case Is = 3, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55
            Case Else
                gP.DrawLine(penPix, nBorder, 0, nBorder, pnlGraph.Height)
        End Select


400:    'STEP 4:   Draw the Major gridlines along the X axis with a dashed line.
        penPix.Width = 2
        penPix.DashStyle = Drawing2D.DashStyle.Dash
        penPix.Color = colorGrid
        Select Case nGraphNum
            Case 1 'This is the all steps graph
                nNumberOfXMajorGridLines = 10 * nNumberOfStrides
                For Me.i = 1 To nNumberOfXMajorGridLines
                    g = Int(i * 10 * nPixelsPerPercent_X)
                    gP.DrawLine(penPix, nBorder + g, 0, nBorder + g, pnlGraph.Height)
                Next i
            Case 3 'This is the radial graph
            Case 13, 23, 33, 43, 53 'These are the 5 harmonic amplitude graphs
                For Me.i = 1 To 10
                    g = pnlGraph.ClientRectangle.Width * i / 10
                    gP.DrawLine(penPix, nBorder + g, 0, nBorder + g, pnlGraph.Height)
                Next i
            Case 14, 15, 24, 25, 34, 35, 44, 45, 54, 55 'These are the equation graphs
            Case Else
                nNumberOfXMajorGridLines = Int(0.1 * arMaximumXValues(nGraphNum))
                For Me.i = 1 To nNumberOfXMajorGridLines
                    g = Int(i * 10 * nPixelsPerPercent_X) ' - 1)
                    gP.DrawLine(penPix, nBorder + g, 0, nBorder + g, pnlGraph.Height)
                Next i
        End Select

500:    'STEP5: Draw the little tickmarks for the X axis. 4 small ones a then the 5th one just a little longer.
        penPix.DashStyle = Drawing2D.DashStyle.Dot
        penPix.Width = 1
        Select Case nGraphNum
            Case Is = 3, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55
            Case 1
                For Me.i = 1 To 10 * nNumberOfXMajorGridLines
                    Point1.Y = 0.96 * pnlGraph.Height
                    Point2.Y = pnlGraph.Height
                    For Me.j = 1 To 4
                        Point1.X = nBorder + Int(i * nPixelsPerPercent_X)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point1.X, Point2.Y)
                        i = i + 1
                    Next j
                    Point1.Y = 0.92 * pnlGraph.Height
                    Point1.X = nBorder + Int(i * nPixelsPerPercent_X)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point1.X, Point2.Y)
                Next i
            Case Else
                If nGraphNum = 60 Or nGraphNum = 70 Then
                    Point1.Y = 0.96 * pnlGraph.Height
                    Point2.Y = pnlGraph.Height
                Else
                    Point1.Y = 0.48 * pnlGraph.Height
                    Point2.Y = 0.52 * pnlGraph.Height
                End If
                For Me.i = 1 To Val(lblMaximumXValue.Text)
                    For Me.j = 1 To 4 'the four short vertical lines
                        Point1.X = nBorder + Int(i * nPixelsPerPercent_X)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point1.X, Point2.Y)
                        i = i + 1
                    Next j
                    If nGraphNum = 60 Or nGraphNum = 70 Then
                        Point1.Y = 0.92 * pnlGraph.Height
                        Point2.Y = pnlGraph.Height
                    Else
                        Point1.Y = 0.46 * pnlGraph.Height
                        Point2.Y = 0.54 * pnlGraph.Height
                    End If
                    Point1.X = Int(i * nPixelsPerPercent_X)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point1.X, Point2.Y)
                    If nGraphNum = 60 Or nGraphNum = 70 Then
                        Point1.Y = 0.96 * pnlGraph.Height
                        Point2.Y = pnlGraph.Height
                    Else
                        Point1.Y = 0.48 * pnlGraph.Height
                        Point2.Y = 0.52 * pnlGraph.Height
                    End If
                Next i
        End Select

600:    'STEP 6: Put the labels for the X axis below the graph.
        Select Case nGraphNum
            Case 1
            Case 3
            Case 13, 23, 33, 43, 53
                For Me.i = 0 To Me.Controls.Count - 1
                    If Mid(Me.Controls(i).Tag, 1, 6) = "XLabel" Then
                        Me.Controls(i).Top = pnlGraph.Top + pnlGraph.Height + 10
                        n = InStr(Me.Controls(i).Tag, " ")
                        j = Mid(Me.Controls(i).Tag, n + 1)
                        Me.Controls(i).Left = pnlGraph.Left + j * 0.1 * pnlGraph.ClientRectangle.Width - 0.5 * Me.Controls(i).Width
                    End If
                Next i
            Case 14, 15, 24, 25, 34, 35, 44, 45
            Case Else
                If (nGraphNum = 81 Or nGraphNum = 82 Or nGraphNum = 83) And butSuperimpose.FlatStyle = FlatStyle.Flat Then
                    For Me.i = 0 To Me.Controls.Count - 1
                        If Mid(Me.Controls(i).Tag, 1, 6) = "XLabel" Then
                            n = InStr(Me.Controls(i).Tag, " ")
                            j = Mid(Me.Controls(i).Tag, n + 1)
                            Me.Controls(i).Left = pnlGraph.Left + j * 10 * pnlGraph.ClientRectangle.Width / 100 - 0.5 * Me.Controls(i).Width + nBorder
                            Me.Controls(i).Visible = True
                        End If
                    Next i
                Else
                    If Val(lblMaximumXValue.Text) <> 0 Then
                        For Me.i = 0 To Me.Controls.Count - 1
                            If Mid(Me.Controls(i).Tag, 1, 6) = "XLabel" Then
                                Me.Controls(i).Top = pnlGraph.Top + pnlGraph.Height + 10
                                n = InStr(Me.Controls(i).Tag, " ")
                                j = Mid(Me.Controls(i).Tag, n + 1)
                                Me.Controls(i).Left = pnlGraph.Left + j * 10 * pnlGraph.ClientRectangle.Width / Val(lblMaximumXValue.Text) - 0.5 * Me.Controls(i).Width + nBorder
                                Me.Controls(i).Visible = True
                            End If
                        Next i
                    End If
                End If
        End Select

700:    'STEP 7:   Draw the Major Gridlines for the Y axis with a dashed line.
        penPix.Width = 2
        Select Case nGraphNum
            Case Is = 3, 13, 14, 15, 23, 24, 25, 33, 34, 35, 43, 44, 45, 53, 54, 55
            Case Else
                Point1.X = 0
                Point2.X = pnlGraph.Width
                For Me.i = -5 To 5
                    If i <> 0 Then
                        Point1.Y = pnlGraph.Height / 2 - (i * 0.1 * pnlGraph.Height)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point1.Y)
                    End If
                Next i
        End Select

800:    'STEP 8: Put the labels in their right location.
        Select Case nGraphNum
            Case 1, 2, 3, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 81, 82, 83, 84, 85, 86
                For Me.i = 0 To 10
                    For Me.n = (Me.Controls.Count - 1) To 0 Step -1
                        If Me.Controls.Item(n).Tag = "YLabel " & i Then
                            Me.Controls.Item(n).Top = pnlGraph.Top + (1 - 0.1 * i) * pnlGraph.Height - 0.5 * Me.Controls.Item(n).Height
                            Me.Controls.Item(n).Left = pnlGraph.Left - 5 - Me.Controls.Item(n).Width
                            Exit For
                        End If
                    Next n
                Next i
            Case Else

        End Select

900:    'STEP 9: 'Figure out how many pixels one unit of Y is equal to.
        Select Case nGraphNum
            Case 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52
                nPixelsPerUnit_Y = 0.5 * pnlGraph.Height / arMaximumYValues(nGraphNum)
            Case 1, 2, 60, 70, 81
                nPixelsPerUnit_Y = pnlGraph.Height / arMaximumYValues(nGraphNum)
        End Select

1000:   'STEP 10: Shade the Double Support Phases of Gait (or the Float Phases of Gait)
        Select Case nGraphNum
            Case 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 70, 81, 82, 83, 84, 85, 86
                If butSuperimpose.Visible = False Or butSuperimpose.Text = "Compare Left and Right" Then
                    Dim brushDouble As New SolidBrush(Color.FromArgb(25, colorBoth.R, colorBoth.G, colorBoth.B))
                    Dim rectPnl As New Rectangle
                    Dim gPath As New GraphicsPath
                    Point1.X = nBorder
                    For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual displacement graph.
                        If f < 101 Then
                            j = f
                        Else : j = f - 100 * Int(f / 100)
                        End If
                        penPix.Color = graphFunctionSelectGraphingColor(j)
                        penPix.Width = 0
                        Point2.X = nBorder + f * nPixelsPerPercent_X
                        If penPix.Color = colorBoth Then 'Change the background in this part of the graph if you are in double support.
                            rectPnl.X = Point1.X
                            rectPnl.Width = Point2.X - Point1.X
                            rectPnl.Y = 0
                            rectPnl.Height = pnlGraph.Height
                            gP.FillRectangle(brushDouble, rectPnl)
                        End If
                        Point1.X = Point2.X
                    Next f
                End If
        End Select

1100:   'STEP 11: Draw the Actual Graph
        Dim sLine As String
        Select Case nGraphNum
            Case 1
                penPix.Width = 3
                penPix.DashStyle = DashStyle.Solid
                penPix.Color = colorBoth
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllTotalForces(1, 0)
                For f = 1 To nNumberOfStrides
                    For Me.g = 1 To 100
                        Point2.X = ((f - 1) * 100 + g) * nPixelsPerPercent_X
                        Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllTotalForces(f, g)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next g
                Next f
                penPix.Color = colorLeft
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllLeftForces(1, 0)
                For f = 1 To nNumberOfStrides
                    For Me.g = 1 To 100
                        Point2.X = ((f - 1) * 100 + g) * nPixelsPerPercent_X
                        Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllLeftForces(f, g)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next g
                Next f
                penPix.Color = colorRight
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllRightForces(1, 0)
                For f = 1 To nNumberOfStrides
                    For Me.g = 1 To 100
                        Point2.X = ((f - 1) * 100 + g) * nPixelsPerPercent_X
                        Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arAllRightForces(f, g)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next g
                Next f
1102:
            Case 2
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arTotalForce(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual force graph for Both Feet
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = colorBoth
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arTotalForce(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arLeftForce(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual force graph for the Left Foot.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = colorLeft
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arLeftForce(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arRightForce(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual Force graph for the Right Foot.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = colorRight
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arRightForce(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
1103:
            Case 3 'This is for a radial graph 
1110:
            Case 10, 11, 12 'These graphs are for the force vs. time in terms of Body Weight.
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arBodyWeight(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the body graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j)
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arBodyWeight(j)
                    If bWalkingOrRunning = False Then
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Else 'for running.
                        If arBodyWeight(f - 1) <> arBodyWeight(0) Then
                            If arBodyWeight(f) <> arBodyWeight(0) Then
                                gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                            End If
                        End If
                    End If
                    'gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                If nGraphNum = 11 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesForce(0)
                    For f = 1 To arMaximumXValues(11)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesForce(j)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                ElseIf nGraphNum = 12 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arDisplacement(0) - arHarmonicsGraphingValuesForce(0))
                    For f = 1 To arMaximumXValues(12)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arDisplacement(j) - arHarmonicsGraphingValuesForce(j))
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                End If

            Case 13
                Dim brushP As New Drawing.SolidBrush(colorHarm)
                Dim rectPnl As New Rectangle
                penPix.Color = colorHarm
                For Me.i = 1 To 12
                    rectPnl.X = 0
                    rectPnl.Y = pnlGraph.ClientRectangle.Height * (i / 26 + (i - 1) / 24)
                    rectPnl.Height = pnlGraph.ClientRectangle.Height / 24
                    rectPnl.Width = nPixelsPerUnit_X * arHarmonicValuesForce(i, 2)
                    Point1.Y = (i * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point2.Y = (i * 2 + 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point1.X = 0
                    Point2.X = nPixelsPerUnit_X * arHarmonicValuesForce(i, 2)
                    gP.FillRectangle(brushP, rectPnl)
                Next i
                brushP.Dispose()

            Case 14, 15
                Dim brushP As New Drawing.SolidBrush(colorPicText)
                Dim pointP As New Point
                Dim sStr(4) As String 'This is to break up the string into varus parts
                Dim rectStr(4) As SizeF 'These rectangles hold the four parts of the string equation.
                gP.Clear(colorBackground)
                rectText = gP.MeasureString("Fourier Equation for Vertical Force", pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectText.Width) / 2
                Point1.Y = 20
                gP.DrawString("Fourier Equation for Vertical Force", pnlGraph.Font, brushP, Point1.X, Point1.Y)
                For Me.i = 1 To 12
                    If nGraphNum = 14 Then
                        If bEnglishOrMetricUnits = False Then
                            If i <> 1 Then
                                nValue = Math.Abs(arHarmonicValuesForce(i, 0))
                                sStr(2) = nValue.ToString("0.00##") & " cos(" & i & Chr(183) & "x) "
                                nValue = Math.Abs(arHarmonicValuesForce(i, 1))
                                sStr(4) = nValue.ToString("0.00##") & " sin(" & i & Chr(183) & "x)"
                            ElseIf i = 1 Then
                                nValue = Math.Abs(arHarmonicValuesForce(i, 0))
                                sStr(2) = nValue.ToString("0.00##") & " cos(x) "
                                nValue = Math.Abs(arHarmonicValuesForce(i, 1))
                                sStr(4) = nValue.ToString("0.00##") & " sin(x)"
                            End If
                        ElseIf bEnglishOrMetricUnits = True Then
                            If i <> 1 Then
                                nValue = Math.Abs(arHarmonicValuesForce(i, 0) * Lbs_To_Kgs)
                                sStr(2) = nValue.ToString("0.00") & " cos(" & i & Chr(183) & "x) "
                                nValue = Math.Abs(arHarmonicValuesForce(i, 1) * Lbs_To_Kgs)
                                sStr(4) = nValue.ToString("0.00") & " sin(" & i & " x)"
                            ElseIf i = 1 Then
                                nValue = arHarmonicValuesDisplacement(i, 0) * Lbs_To_Kgs
                                sStr(2) = nValue.ToString("0.00") & " cos(x) "
                                nValue = arHarmonicValuesDisplacement(i, 1) * Lbs_To_Kgs
                                sStr(4) = nValue.ToString("0.00") & " sin(x)"
                            End If
                        End If
                        If arHarmonicValuesForce(i, 0) < 0 Then
                            If i <> 1 Then
                                sStr(1) = "-" & Space(1)
                            ElseIf i = 1 Then
                                sStr(1) = Space(2)
                            End If
                        Else
                            sStr(1) = "+ "
                        End If
                        If arHarmonicValuesForce(i, 1) < 0 Then
                            sStr(3) = "-" & Space(1)
                        Else
                            sStr(3) = "+ "
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        rectStr(2) = gP.MeasureString(sStr(2), pnlGraph.Font)
                        rectStr(3) = gP.MeasureString(sStr(3), pnlGraph.Font)
                        rectStr(4) = gP.MeasureString(sStr(4), pnlGraph.Font)
                        Point1.X = pnlGraph.ClientRectangle.Width / 2
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X - rectStr(2).Width - rectStr(1).Width, Point1.Y)
                    ElseIf nGraphNum = 15 Then
                        If bEnglishOrMetricUnits = False Then
                            nValue = Math.Abs(arHarmonicValuesForce(i, 2))
                            sStr(2) = nValue.ToString("0.00##") & " cos(" & i & Chr(183) & "x "
                        Else
                            nValue = Math.Abs(arHarmonicValuesDisplacement(i, 2) * Feet_To_cm)
                            sStr(2) = nValue.ToString("0.00##") & " cos(" & i & Chr(183) & "x "
                        End If
                        nValue = Math.Abs(Math.Atan2(arHarmonicValuesForce(i, 1), arHarmonicValuesForce(i, 0)) * 180 / Math.PI)
                        sStr(4) = nValue.ToString("0.##" & Chr(186) & ")")
                        If Val(sStr(2)) < 0 Then
                            sStr(1) = "- "
                        Else
                            sStr(1) = "+ "
                        End If
                        If (arHarmonicValuesForce(i, 1) / arHarmonicValuesForce(i, 0)) < 0 Then
                            sStr(3) = "- "
                        Else
                            sStr(3) = "+"
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X, Point1.Y)
                    End If
                Next i
                nValue = (Cadence * 8 * System.Math.Atan(1)) / 120
                sStr(0) = "Where x = " & nValue.ToString("0.###") & Chr(183) & " time (in seconds)"
                rectStr(0) = gP.MeasureString(sStr(0), pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectStr(0).Width) / 2
                Point1.Y = 20 + 14 * rectText.Height
                gP.DrawString(sStr(0), pnlGraph.Font, brushP, Point1.X, Point1.Y)

1120:
            Case 20, 21, 22 'For the Displacement Graphs, including the addition and subtraction graphs
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arDisplacement(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual displacement graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j) 'Finds the COLOR of the Line Segment
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arDisplacement(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                If nGraphNum = 21 Then 'If you are drawing the Sum of the Harmonics Graph
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesDisplacement(0)
                    For f = 1 To arMaximumXValues(21)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesDisplacement(j)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                ElseIf nGraphNum = 22 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arDisplacement(0) - arHarmonicsGraphingValuesDisplacement(0))
                    For f = 1 To arMaximumXValues(22)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arDisplacement(j) - arHarmonicsGraphingValuesDisplacement(j))
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                End If

            Case 23
                Dim brushP As New Drawing.SolidBrush(colorHarm)
                Dim rectPnl As New Rectangle
                penPix.Color = colorHarm
                For Me.i = 1 To 12
                    rectPnl.X = 0
                    rectPnl.Y = pnlGraph.ClientRectangle.Height * (i / 26 + (i - 1) / 24)
                    rectPnl.Height = pnlGraph.ClientRectangle.Height / 24
                    rectPnl.Width = nPixelsPerUnit_X * arHarmonicValuesDisplacement(i, 2)
                    Point1.Y = (i * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point2.Y = (i * 2 + 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point1.X = 0
                    Point2.X = nPixelsPerUnit_X * arHarmonicValuesDisplacement(i, 2)
                    gP.FillRectangle(brushP, rectPnl)
                Next i
                'Put the labels along the bars
                Dim sTemp() As String
                For Me.i = 0 To Me.Controls.Count - 1
                    If TypeOf Me.Controls.Item(i) Is Label Then
                        If Mid(Me.Controls(i).Tag, 1, 6) = "YLabel" Then
                            sTemp = Me.Controls(i).Tag.split(" ")
                            k = sTemp(1)
                            Me.Controls(i).BackColor = Color.FromArgb(0, 255, 255, 255)
                            Me.Controls(i).Left = pnlGraph.Left + nPixelsPerUnit_X * arHarmonicValuesDisplacement(k, 2)
                            Me.Controls(i).Top = pnlGraph.Top + (k * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24) - 2
                            Me.Controls(i).BringToFront()
                        End If
                    End If
                Next
                'Use the X labels to identify the 1st through the 12th harmonic
                For Me.i = 0 To Me.Controls.Count - 1
                    If TypeOf Me.Controls.Item(i) Is Label Then
                        If Mid(Me.Controls(i).Tag, 1, 6) = "XLabel" Then
                            sTemp = Me.Controls(i).Tag.split(" ")
                            k = sTemp(1)
                            If k <> 0 Then
                                Me.Controls(i).BackColor = Me.BackColor
                                Me.Controls(i).Left = pnlGraph.Left - 5 - Me.Controls(i).Width
                                Me.Controls(i).Top = pnlGraph.Top + (k * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24) - 2
                                Me.Controls(i).BringToFront()
                            End If
                        End If
                    End If
                Next
                brushP.Dispose()
1125:
            Case 24, 25
                Dim brushP As New Drawing.SolidBrush(colorPicText)
                Dim pointP As New Point
                Dim sStr(4) As String 'This is to break up the string into varus parts
                Dim rectStr(4) As SizeF 'These rectangles hold the four parts of the string equation.
                gP.Clear(colorBackground)
                rectText = gP.MeasureString("Fourier Equation for Vertical Displacement", pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectText.Width) / 2
                Point1.Y = 20
                gP.DrawString("Fourier Equation for Vertical Displacement", pnlGraph.Font, brushP, Point1.X, Point1.Y)
                For Me.i = 1 To 12
                    If nGraphNum = 24 Then
                        If bEnglishOrMetricUnits = False Then
                            If i <> 1 Then
                                nValue = Math.Abs(arHarmonicValuesDisplacement(i, 0)) * Feet_To_In
                                sStr(2) = nValue.ToString("0.00##") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 1) * Feet_To_In), "0.00##") & " sin(" & i & Chr(183) & "x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesDisplacement(i, 0) * Feet_To_In, "0.00##") & " cos(x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 1) * Feet_To_In), "0.00##") & " sin(x)"
                            End If
                        ElseIf bEnglishOrMetricUnits = True Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 0) * Feet_To_cm), "0.00") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 1) * Feet_To_cm), "0.00") & " sin(" & i & " x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesDisplacement(i, 0) * Feet_To_cm, "0.00") & " cos(x) "
                                sStr(4) = VB6.Format(arHarmonicValuesDisplacement(i, 1) * Feet_To_cm, "0.00") & " sin(x)"
                            End If
                        End If
                        If arHarmonicValuesDisplacement(i, 0) < 0 Then
                            If i <> 1 Then
                                sStr(1) = "-" & Space(1)
                            ElseIf i = 1 Then
                                sStr(1) = Space(2)
                            End If
                        Else
                            sStr(1) = "+ "
                        End If
                        If arHarmonicValuesDisplacement(i, 1) < 0 Then
                            sStr(3) = "-" & Space(1)
                        Else
                            sStr(3) = "+ "
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        rectStr(2) = gP.MeasureString(sStr(2), pnlGraph.Font)
                        rectStr(3) = gP.MeasureString(sStr(3), pnlGraph.Font)
                        rectStr(4) = gP.MeasureString(sStr(4), pnlGraph.Font)
                        Point1.X = pnlGraph.ClientRectangle.Width / 2
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X - rectStr(2).Width - rectStr(1).Width, Point1.Y)
                    ElseIf nGraphNum = 25 Then
                        If bEnglishOrMetricUnits = False Then
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 2) * Feet_To_In), "0.00##") & " cos(" & i & Chr(183) & "x "
                        Else
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesDisplacement(i, 2) * Feet_To_cm), "0.00##") & " cos(" & i & Chr(183) & "x "
                        End If
                        sStr(4) = VB6.Format(Math.Abs(Math.Atan2(arHarmonicValuesDisplacement(i, 1), arHarmonicValuesDisplacement(i, 0)) * 180 / Math.PI), "0.##" & Chr(176) & ")")
                        If Val(sStr(2)) < 0 Then
                            sStr(1) = "- "
                        Else
                            sStr(1) = "+ "
                        End If
                        If (arHarmonicValuesDisplacement(i, 1) / arHarmonicValuesDisplacement(i, 0)) < 0 Then
                            sStr(3) = "- "
                        Else
                            sStr(3) = "+"
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X, Point1.Y)
                    End If
                Next i
                sStr(0) = "Where x = " & VB6.Format((Cadence * 8 * System.Math.Atan(1)) / 120, "0.###") & Chr(183) & " time (in seconds)"
                rectStr(0) = gP.MeasureString(sStr(0), pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectStr(0).Width) / 2
                Point1.Y = 20 + 14 * rectText.Height
                gP.DrawString(sStr(0), pnlGraph.Font, brushP, Point1.X, Point1.Y)

1130:
            Case 30, 31, 32 'Velocity Graphs
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoMVelocity(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual VELOCITY graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j)
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoMVelocity(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                If nGraphNum = 31 Then 'If you are drawing the Sum of the Harmonics Graph
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesVelocity(0)
                    For f = 1 To arMaximumXValues(31)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesVelocity(j)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                ElseIf nGraphNum = 32 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arCoMVelocity(0) - arHarmonicsGraphingValuesVelocity(0))
                    For f = 1 To arMaximumXValues(32)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arCoMVelocity(j) - arHarmonicsGraphingValuesVelocity(j))
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                End If
1133:
            Case 33
                Dim brushP As New Drawing.SolidBrush(colorHarm)
                Dim rectPnl As New Rectangle
                penPix.Color = colorHarm
                For Me.i = 1 To 12
                    rectPnl.X = 0
                    rectPnl.Y = pnlGraph.ClientRectangle.Height * (i / 26 + (i - 1) / 24)
                    rectPnl.Height = pnlGraph.ClientRectangle.Height / 24
                    rectPnl.Width = nPixelsPerUnit_X * arHarmonicValuesVelocity(i, 2)
                    Point1.Y = (i * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point2.Y = (i * 2 + 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point1.X = 0
                    Point2.X = nPixelsPerUnit_X * arHarmonicValuesVelocity(i, 2)
                    gP.FillRectangle(brushP, rectPnl)
                Next i
                brushP.Dispose()
1135:
            Case 34, 35 'Fourier Equation for Vertical Velocity
                Dim brushP As New Drawing.SolidBrush(colorPicText)
                Dim pointP As New Point
                Dim sStr(4) As String 'This is to break up the string into varus parts
                Dim rectStr(4) As SizeF 'These rectangles hold the four parts of the string equation.
                gP.Clear(colorBackground)
                rectText = gP.MeasureString("Fourier Equation for Vertical Velocity", pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectText.Width) / 2
                Point1.Y = 20
                gP.DrawString("Fourier Equation for Vertical Velocity", pnlGraph.Font, brushP, Point1.X, Point1.Y)
                For Me.i = 1 To 12
                    If nGraphNum = 34 Then
                        If bEnglishOrMetricUnits = False Then
                            If i <> 1 Then
                                nValue = Math.Abs(arHarmonicValuesVelocity(i, 0)) * Feet_To_In
                                sStr(2) = nValue.ToString("0.00##") & " cos(" & i & Chr(183) & "x) "
                                nValue = Math.Abs(arHarmonicValuesVelocity(i, 1)) * Feet_To_In
                                sStr(4) = nValue.ToString("0.00##") & " sin(" & i & Chr(183) & "x)"
                            ElseIf i = 1 Then
                                nValue = arHarmonicValuesVelocity(i, 0) * Feet_To_In
                                sStr(2) = nValue.ToString("0.00##") & " cos(x) "
                                nValue = Math.Abs(arHarmonicValuesVelocity(i, 1)) * Feet_To_In
                                sStr(4) = nValue.ToString("0.00##") & " sin(x)"
                            End If
                        ElseIf bEnglishOrMetricUnits = True Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesVelocity(i, 0) * Feet_To_cm), "0.00") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesVelocity(i, 1) * Feet_To_cm), "0.00") & " sin(" & i & " x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesVelocity(i, 0) * Feet_To_cm, "0.00") & " cos(x) "
                                sStr(4) = VB6.Format(arHarmonicValuesVelocity(i, 1) * Feet_To_cm, "0.00") & " sin(x)"
                            End If
                        End If
                        If arHarmonicValuesVelocity(i, 0) < 0 Then
                            If i <> 1 Then
                                sStr(1) = "-" & Space(1)
                            ElseIf i = 1 Then
                                sStr(1) = Space(2)
                            End If
                        Else
                            sStr(1) = "+ "
                        End If
                        If arHarmonicValuesVelocity(i, 1) < 0 Then
                            sStr(3) = "-" & Space(1)
                        Else
                            sStr(3) = "+ "
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        rectStr(2) = gP.MeasureString(sStr(2), pnlGraph.Font)
                        rectStr(3) = gP.MeasureString(sStr(3), pnlGraph.Font)
                        rectStr(4) = gP.MeasureString(sStr(4), pnlGraph.Font)
                        Point1.X = pnlGraph.ClientRectangle.Width / 2
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X - rectStr(2).Width - rectStr(1).Width, Point1.Y)
                    ElseIf nGraphNum = 35 Then
                        If bEnglishOrMetricUnits = False Then
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesVelocity(i, 2) * Feet_To_In), "0.00##") & " cos(" & i & Chr(183) & "x "
                        Else
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesVelocity(i, 2) * Feet_To_cm), "0.00##") & " cos(" & i & Chr(183) & "x "
                        End If
                        sStr(4) = VB6.Format(Math.Abs(Math.Atan2(arHarmonicValuesVelocity(i, 1), arHarmonicValuesVelocity(i, 0)) * 180 / Math.PI), "0.##" & Chr(176) & ")")
                        If Val(sStr(2)) < 0 Then
                            sStr(1) = "- "
                        Else
                            sStr(1) = "+ "
                        End If
                        If (arHarmonicValuesVelocity(i, 1) / arHarmonicValuesVelocity(i, 0)) < 0 Then
                            sStr(3) = "- "
                        Else
                            sStr(3) = "+"
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X, Point1.Y)
                    End If
                Next i
                nValue = Cadence * 8 * System.Math.Atan(1) / 120
                sStr(0) = "Where x = " & nValue.ToString("0.###") & Chr(183) & " time (in seconds)"
                rectStr(0) = gP.MeasureString(sStr(0), pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectStr(0).Width) / 2
                Point1.Y = 20 + 14 * rectText.Height
                gP.DrawString(sStr(0), pnlGraph.Font, brushP, Point1.X, Point1.Y)
1140:
            Case 40, 41, 42 'POWER GRAPHS
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arPower(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual POWER graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j)
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arPower(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                If nGraphNum = 41 Then 'If you are drawing the Sum of the Harmonics Graph
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesPower(0)
                    For f = 1 To arMaximumXValues(41)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesPower(j)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                ElseIf nGraphNum = 42 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arPower(0) - arHarmonicsGraphingValuesPower(0))
                    For f = 1 To arMaximumXValues(42)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arPower(j) - arHarmonicsGraphingValuesPower(j))
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                End If
1143:
            Case 43
                Dim brushP As New Drawing.SolidBrush(colorHarm)
                Dim rectPnl As New Rectangle
                penPix.Color = colorHarm
                For Me.i = 1 To 12
                    rectPnl.X = 0
                    rectPnl.Y = pnlGraph.ClientRectangle.Height * (i / 26 + (i - 1) / 24)
                    rectPnl.Height = pnlGraph.ClientRectangle.Height / 24
                    rectPnl.Width = nPixelsPerUnit_X * arHarmonicValuesPower(i, 2)
                    Point1.Y = (i * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point2.Y = (i * 2 + 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point1.X = 0
                    Point2.X = nPixelsPerUnit_X * arHarmonicValuesPower(i, 2)
                    gP.FillRectangle(brushP, rectPnl)
                Next i
                brushP.Dispose()

            Case 44, 45
                Dim brushP As New Drawing.SolidBrush(colorPicText)
                Dim pointP As New Point
                Dim sStr(4) As String 'This is to break up the string into varus parts
                Dim rectStr(4) As SizeF 'These rectangles hold the four parts of the string equation.
                gP.Clear(colorBackground)
                rectText = gP.MeasureString("Fourier Equation for Power", pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectText.Width) / 2
                Point1.Y = 20
                gP.DrawString("Fourier Equation for Power", pnlGraph.Font, brushP, Point1.X, Point1.Y)
                For Me.i = 1 To 12
                    If nGraphNum = 44 Then
                        If bEnglishOrMetricUnits = False Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 0)), "0.00##") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 1)), "0.00##") & " sin(" & i & Chr(183) & "x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesPower(i, 0), "0.00##") & " cos(x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 1)), "0.00##") & " sin(x)"
                            End If
                        ElseIf bEnglishOrMetricUnits = True Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 0) * FtLbs_To_NewtonM), "0.00") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 1) * FtLbs_To_NewtonM), "0.00") & " sin(" & i & " x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesPower(i, 0) * FtLbs_To_NewtonM, "0.00") & " cos(x) "
                                sStr(4) = VB6.Format(arHarmonicValuesPower(i, 1) * FtLbs_To_NewtonM, "0.00") & " sin(x)"
                            End If
                        End If
                        If arHarmonicValuesPower(i, 0) < 0 Then
                            If i <> 1 Then
                                sStr(1) = "-" & Space(1)
                            ElseIf i = 1 Then
                                sStr(1) = Space(2)
                            End If
                        Else
                            sStr(1) = "+ "
                        End If
                        If arHarmonicValuesPower(i, 1) < 0 Then
                            sStr(3) = "-" & Space(1)
                        Else
                            sStr(3) = "+ "
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        rectStr(2) = gP.MeasureString(sStr(2), pnlGraph.Font)
                        rectStr(3) = gP.MeasureString(sStr(3), pnlGraph.Font)
                        rectStr(4) = gP.MeasureString(sStr(4), pnlGraph.Font)
                        Point1.X = pnlGraph.ClientRectangle.Width / 2
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X - rectStr(2).Width - rectStr(1).Width, Point1.Y)
                    ElseIf nGraphNum = 45 Then
                        If bEnglishOrMetricUnits = False Then
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 2)), "0.00##") & " cos(" & i & Chr(183) & "x "
                        Else
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 2) * FtLbs_To_NewtonM), "0.00##") & " cos(" & i & Chr(183) & "x "
                        End If
                        sStr(4) = VB6.Format(Math.Abs(Math.Atan2(arHarmonicValuesPower(i, 1), arHarmonicValuesPower(i, 0)) * 180 / Math.PI), "0.##" & Chr(176) & ")")
                        If Val(sStr(2)) < 0 Then
                            sStr(1) = "- "
                        Else
                            sStr(1) = "+ "
                        End If
                        If (arHarmonicValuesPower(i, 1) / arHarmonicValuesPower(i, 0)) < 0 Then
                            sStr(3) = "- "
                        Else
                            sStr(3) = "+"
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X, Point1.Y)
                    End If
                Next i
                sStr(0) = "Where x = " & VB6.Format((Cadence * 8 * System.Math.Atan(1)) / 120, "0.###") & Chr(183) & " time (in seconds)"
                rectStr(0) = gP.MeasureString(sStr(0), pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectStr(0).Width) / 2
                Point1.Y = 20 + 14 * rectText.Height
                gP.DrawString(sStr(0), pnlGraph.Font, brushP, Point1.X, Point1.Y)
1150:
            Case 50, 51, 52 'WORK graphs
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arWork(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual WORK graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j)
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arWork(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                If nGraphNum = 51 Then 'If you are drawing the Sum of the Harmonics Graph
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesWork(0)
                    For f = 1 To arMaximumXValues(51)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arHarmonicsGraphingValuesWork(j)
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                ElseIf nGraphNum = 52 Then
                    penPix.Color = colorHarm
                    penPix.DashStyle = Drawing2D.DashStyle.DashDot
                    Point1.X = 0
                    Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arWork(0) - arHarmonicsGraphingValuesWork(0))
                    For f = 1 To arMaximumXValues(52)
                        If f < 101 Then
                            j = f
                        Else
                            j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * (arPower(j) - arHarmonicsGraphingValuesWork(j))
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                End If
1153:
            Case 53
                Dim brushP As New Drawing.SolidBrush(colorHarm)
                Dim rectPnl As New Rectangle
                penPix.Color = colorHarm
                For Me.i = 1 To 12
                    rectPnl.X = 0
                    rectPnl.Y = pnlGraph.ClientRectangle.Height * (i / 26 + (i - 1) / 24)
                    rectPnl.Height = pnlGraph.ClientRectangle.Height / 24
                    rectPnl.Width = nPixelsPerUnit_X * arHarmonicValuesWork(i, 2)
                    Point1.Y = (i * 2 - 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point2.Y = (i * 2 + 1) * Int(pnlGraph.ClientRectangle.Height / 24)
                    Point1.X = 0
                    Point2.X = nPixelsPerUnit_X * arHarmonicValuesWork(i, 2)
                    gP.FillRectangle(brushP, rectPnl)
                Next i
                brushP.Dispose()
1155:
            Case 54, 55
                Dim brushP As New Drawing.SolidBrush(colorPicText)
                Dim pointP As New Point
                Dim sStr(4) As String 'This is to break up the string into varus parts
                Dim rectStr(4) As SizeF 'These rectangles hold the four parts of the string equation.
                gP.Clear(colorBackground)
                rectText = gP.MeasureString("Fourier Equation for Power", pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectText.Width) / 2
                Point1.Y = 20
                gP.DrawString("Fourier Equation for Work", pnlGraph.Font, brushP, Point1.X, Point1.Y)
                For Me.i = 1 To 12
                    If nGraphNum = 54 Then
                        If bEnglishOrMetricUnits = False Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesWork(i, 0)), "0.00##") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesWork(i, 1)), "0.00##") & " sin(" & i & Chr(183) & "x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesWork(i, 0), "0.00##") & " cos(x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesWork(i, 1)), "0.00##") & " sin(x)"
                            End If
                        ElseIf bEnglishOrMetricUnits = True Then
                            If i <> 1 Then
                                sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesWork(i, 0) * FtLbs_To_NewtonM), "0.00") & " cos(" & i & Chr(183) & "x) "
                                sStr(4) = VB6.Format(Math.Abs(arHarmonicValuesWork(i, 1) * FtLbs_To_NewtonM), "0.00") & " sin(" & i & " x)"
                            ElseIf i = 1 Then
                                sStr(2) = VB6.Format(arHarmonicValuesWork(i, 0) * FtLbs_To_NewtonM, "0.00") & " cos(x) "
                                sStr(4) = VB6.Format(arHarmonicValuesWork(i, 1) * FtLbs_To_NewtonM, "0.00") & " sin(x)"
                            End If
                        End If
                        If arHarmonicValuesWork(i, 0) < 0 Then
                            If i <> 1 Then
                                sStr(1) = "-" & Space(1)
                            ElseIf i = 1 Then
                                sStr(1) = Space(2)
                            End If
                        Else
                            sStr(1) = "+ "
                        End If
                        If arHarmonicValuesWork(i, 1) < 0 Then
                            sStr(3) = "-" & Space(1)
                        Else
                            sStr(3) = "+ "
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        rectStr(2) = gP.MeasureString(sStr(2), pnlGraph.Font)
                        rectStr(3) = gP.MeasureString(sStr(3), pnlGraph.Font)
                        rectStr(4) = gP.MeasureString(sStr(4), pnlGraph.Font)
                        Point1.X = pnlGraph.ClientRectangle.Width / 2
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X - rectStr(2).Width - rectStr(1).Width, Point1.Y)
                    ElseIf nGraphNum = 55 Then
                        If bEnglishOrMetricUnits = False Then
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 2)), "0.00##") & " cos(" & i & Chr(183) & "x "
                        Else
                            sStr(2) = VB6.Format(Math.Abs(arHarmonicValuesPower(i, 2) * FtLbs_To_NewtonM), "0.00##") & " cos(" & i & Chr(183) & "x "
                        End If
                        sStr(4) = VB6.Format(Math.Abs(Math.Atan2(arHarmonicValuesPower(i, 1), arHarmonicValuesPower(i, 0)) * 180 / Math.PI), "0.##" & Chr(176) & ")")
                        If Val(sStr(2)) < 0 Then
                            sStr(1) = "- "
                        Else
                            sStr(1) = "+ "
                        End If
                        If (arHarmonicValuesPower(i, 1) / arHarmonicValuesPower(i, 0)) < 0 Then
                            sStr(3) = "- "
                        Else
                            sStr(3) = "+"
                        End If
                        rectStr(1) = gP.MeasureString(sStr(1), pnlGraph.Font)
                        Point1.Y = 20 + (i + 1) * rectText.Height
                        gP.DrawString(sStr(1) & sStr(2) & sStr(3) & sStr(4), pnlGraph.Font, brushP, Point1.X, Point1.Y)
                    End If
                Next i
                sStr(0) = "Where x = " & VB6.Format((Cadence * 8 * System.Math.Atan(1)) / 120, "0.###") & Chr(183) & " time (in seconds)"
                rectStr(0) = gP.MeasureString(sStr(0), pnlGraph.Font)
                Point1.X = (pnlGraph.ClientRectangle.Width - rectStr(0).Width) / 2
                Point1.Y = 20 + 14 * rectText.Height
                gP.DrawString(sStr(0), pnlGraph.Font, brushP, Point1.X, Point1.Y)
1160:
            Case 60 'Energy Graph
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                penPix.Color = colorLeft
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Potential(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Potential energy.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Potential(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                penPix.Color = colorRight
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Kinetic(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Kinetic Energy Graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Kinetic(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                penPix.Color = colorBoth
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Total(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Total Energy graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arEnergy_Total(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                Dim sEnergyIndex As String
                Dim brushP As New System.Drawing.SolidBrush(colorBoth)
                Dim rectEnergy As SizeF
                sEnergyIndex = FormatNumber(100 * GI.Energy, "00.0")
                sEnergyIndex = "Energy Conservation Index = " & sEnergyIndex & "%"
                pnlGraph.ForeColor = colorBoth
                rectEnergy = gP.MeasureString(sEnergyIndex, pnlGraph.Font)
                gP.DrawString(sEnergyIndex, pnlGraph.Font, brushP, 0.5 * pnlGraph.Width - 0.5 * rectEnergy.Width, 5)
1170:
            Case 70 'Graph the Spring Constant
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Point1.X = 0
                Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arSpringConstants(0)
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the actual displacement graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    penPix.Color = graphFunctionSelectGraphingColor(j)
                    Point2.X = f * nPixelsPerPercent_X
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arSpringConstants(j)
                    gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                Dim sSpringIndex As String
                Dim brushP As New System.Drawing.SolidBrush(colorBoth)
                Dim rectSpringIndex As SizeF
                sSpringIndex = FormatNumber(100 * SpringConsistencyIndex, "00.0")
                sSpringIndex = "Spring Conservation Index = " & sSpringIndex & "%"
                pnlGraph.ForeColor = colorBoth
                rectSpringIndex = gP.MeasureString(sSpringIndex, pnlGraph.Font)
                gP.DrawString(sSpringIndex, pnlGraph.Font, brushP, 0.5 * pnlGraph.Width - 0.5 * rectSpringIndex.Width, pnlGraph.Height - rectSpringIndex.Height - 9)

            Case 80
1181:
            Case 81, 83 'This is the graph for CoP position in the AP direction in terms of the full gait cycle
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                If butSuperimpose.FlatStyle = FlatStyle.Standard Then 'If you are looking at the full gait cycle
                    penPix.Color = colorLeft
                    Point1.X = 0
                    If nGraphNum = 81 Then
                        Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_AP_L(0) 'location of the CoP
                    Else
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_L(0) ' velocity of the CoP
                    End If
                    For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Left CoP in the AP direction.
                        If f < 101 Then
                            j = f
                        Else : j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        If nGraphNum = 81 Then
                            Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_AP_L(j) 'position of the CoP
                        Else
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_L(j) 'velocity of the coP
                        End If
                        If arCoPLoc_AP_L(j - 1) <> 0 And arCoPLoc_AP_L(j) <> 0 Then gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y) 'draws the actual line for the left foot..
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                    penPix.Color = colorRight
                    Point1.X = 0
                    If nGraphNum = 81 Then
                        Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_AP_R(0) 'Location of the CoP
                    Else
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_R(0) 'velocity of the CoP right foot
                    End If
                    For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the CoP in the AP direction for the RIGHT foot.
                        If f < 101 Then
                            j = f
                        Else : j = f - 100 * Int(f / 100)
                        End If
                        Point2.X = f * nPixelsPerPercent_X
                        If nGraphNum = 81 Then
                            Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_AP_R(j) 'the location of the CoP
                        ElseIf nGraphNum = 83 Then
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_R(j) 'the velocity of the CoP
                        End If
                        If arCoPLoc_AP_R(j - 1) <> 0 And arCoPLoc_AP_R(j) <> 0 Then gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y) 'draws the actual line.
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                Else 'This is if the superimposition button is depressed
                    penPix.Color = colorLeft
                    Point1.X = 0
                    If nGraphNum = 81 Then
                        Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_Stance_AP_L(0) 'this is the location normalized to stance
                    Else
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_Stance_AP_L(0) 'this is the velocity normalized in stance
                    End If
                     For f = 1 To 100 'This FOR block draws the Left CoP in the AP direction.
                        Point2.X = f * nPixelsPerPercent_X
                        If nGraphNum = 81 Then
                            Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_Stance_AP_L(f) 'this is the location normalized in stance
                        Else
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_Stance_AP_L(f) 'this is the velocity normalized in stance
                        End If
                        If Point1.Y <> pnlGraph.Height And Point2.Y <> pnlGraph.Height Then
                            gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        End If
                        Point1.X = Point2.X
                        Point1.Y = Point2.Y
                    Next f
                    penPix.Color = colorRight
                    Point1.X = 0
                    If nGraphNum = 81 Then
                        Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_Stance_AP_R(0) 'this the location CoP right
                    Else
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_Stance_AP_R(0) 'this is the velocity CoP right
                    End If
                     For f = 1 To 100 'This FOR block draws the CoP in the AP direction for the RIGHT foot.
                        Point2.X = f * nPixelsPerPercent_X
                        If nGraphNum = 81 Then
                            Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_Stance_AP_R(f) 'this is the location of the CoP Right foot
                        Else
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_Stance_AP_R(f) 'this is the velocity of the CoP Right foot
                        End If
                        Select Case nGraphNum
                            Case 81
                                If Point1.Y <> pnlGraph.Height And Point2.Y <> pnlGraph.Height Then gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                            Case 83
                                If Point1.Y <> 0 And Point2.Y <> 0 Then gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                        End Select
                Point1.X = Point2.X
                Point1.Y = Point2.Y
                    Next f
                    'Now put a small perfect line
                    Point1.Y = arCoPLoc_Stance_AP_L(0)
                    Point2.Y = arCoPLoc_Stance_AP_L(0)
                    For Me.i = 0 To 100
                        If Point1.Y > arCoPLoc_Stance_AP_L(i) Then Point1.Y = arCoPLoc_Stance_AP_L(i)
                        If Point1.Y > arCoPLoc_Stance_AP_R(i) Then Point1.Y = arCoPLoc_Stance_AP_R(i)
                        If Point2.Y < arCoPLoc_Stance_AP_L(i) Then Point2.Y = arCoPLoc_Stance_AP_L(i)
                        If Point2.Y < arCoPLoc_Stance_AP_R(i) Then Point2.Y = arCoPLoc_Stance_AP_R(i)
                    Next i
                    penPix.Width = 1
                    penPix.Color = colorBoth
                    Point1.Y = pnlGraph.Height - nPixelsPerUnit_Y * Point1.Y
                    Point2.Y = pnlGraph.Height - nPixelsPerUnit_Y * Point2.Y
                    gP.DrawLine(penPix, 0, Point1.Y, pnlGraph.Width, Point2.Y)
                End If
                'Put the Purity and Symmetry Index on the graph.
                If nGraphNum = 81 Then
                    Dim sSymmetryIndex As String
                    Dim brushP As New System.Drawing.SolidBrush(colorBoth)
                    Dim rectSymmetry As SizeF
                    sSymmetryIndex = FormatNumber(100 * CoPSymmetryIndex, "00.0")
                    sSymmetryIndex = "Symmetry of CoP = " & sSymmetryIndex & "%"
                    pnlGraph.ForeColor = colorBoth
                    rectSymmetry = gP.MeasureString(sSymmetryIndex, pnlGraph.Font)
                    gP.DrawString(sSymmetryIndex, pnlGraph.Font, brushP, 0.25 * pnlGraph.Width - 0.5 * rectSymmetry.Width, pnlGraph.Height - 1.5 * rectSymmetry.Height)
                    sSymmetryIndex = "Left Purity CoP = "
                    sSymmetryIndex = sSymmetryIndex & FormatNumber(100 * CoPPurityIndex_L, "00.0")
                    sSymmetryIndex = sSymmetryIndex & "%" & vbCrLf
                    sSymmetryIndex = sSymmetryIndex & "Right Purity CoP = "
                    sSymmetryIndex = sSymmetryIndex & FormatNumber(100 * CoPPurityIndex_R, "00.0") & "%"
                    rectSymmetry = gP.MeasureString(sSymmetryIndex, pnlGraph.Font)
                    gP.DrawString(sSymmetryIndex, pnlGraph.Font, brushP, 0.75 * pnlGraph.Width - 0.5 * rectSymmetry.Width, pnlGraph.Height - rectSymmetry.Height - 8)
                End If

            Case 82, 84, 85, 86
                penPix.Width = 3
                penPix.DashStyle = Drawing2D.DashStyle.Solid
                Dim nHalf As Double
                nHalf = 0.5 * arMaximumYValues(nGraphNum)
                penPix.Color = colorLeft
                Point1.X = 0
                Select Case nGraphNum
                    Case 82
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_ML_L(0) '(nHalf - arCoPLoc_ML_L(0))
                    Case 83
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_L(0)
                    Case 84
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_ML_L(0)
                    Case 85
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_AP_L(0)
                    Case 86
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_ML_L(0)
                End Select
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Potential energy.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    Point2.X = f * nPixelsPerPercent_X
                    Select Case nGraphNum
                        Case 82
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_ML_L(j) '(nHalf - arCoPLoc_ML_L(j))
                        Case 83
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_L(j)
                        Case 84
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_ML_L(j)
                        Case 85
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_AP_L(j)
                        Case 86
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_ML_L(j)
                    End Select
                    If arGaitPhase(j - 1) <> 4 And arGaitPhase(j) <> 4 Then
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    End If
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
                penPix.Color = colorRight
                Point1.X = 0
                Select Case nGraphNum
                    Case 82
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_ML_R(0) '(nHalf - arCoPLoc_ML_R(0))
                    Case 83
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_R(0)
                    Case 84
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_ML_R(0)
                    Case 85
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_AP_R(0)
                    Case 86
                        Point1.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_ML_R(0)
                End Select
                For f = 1 To arMaximumXValues(nGraphNum) 'This FOR block draws the Kinetic Energy Graph.
                    If f < 101 Then
                        j = f
                    Else : j = f - 100 * Int(f / 100)
                    End If
                    Point2.X = f * nPixelsPerPercent_X
                    Select Case nGraphNum
                        Case 82
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPLoc_ML_R(j) '(nHalf - arCoPLoc_ML_R(j))
                        Case 83
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_AP_R(j)
                        Case 84
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPVel_ML_R(j)
                        Case 85
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_AP_R(j)
                        Case 86
                            Point2.Y = 0.5 * pnlGraph.Height - nPixelsPerUnit_Y * arCoPAcc_ML_R(j)
                    End Select
                    If arGaitPhase(j - 1) <> con_L_Single_Support And arGaitPhase(j) <> con_L_Single_Support Then
                        gP.DrawLine(penPix, Point1.X, Point1.Y, Point2.X, Point2.Y)
                    End If
                    Point1.X = Point2.X
                    Point1.Y = Point2.Y
                Next f
        End Select

        'STEP 12 - Put the Legend on the graph
        gbLegend.BackColor = Color.FromArgb(25, pnlGraph.BackColor.R, pnlGraph.BackColor.G, pnlGraph.BackColor.B)
        gbLegend.Visible = True
        gbLegend.ForeColor = Color.Black
        lblLegendLeft.Left = 2
        lblLegendRight.Left = 2
        lblDoubleSupport.Left = 2
        lblLegendLeft.Top = 15
        lblLegendRight.Top = lblLegendLeft.Top + lblLegendLeft.Height + 4
        lblDoubleSupport.Top = lblLegendRight.Top + lblLegendRight.Height + 4
        lblLegendLeft.BackColor = Color.FromArgb(25, gbLegend.BackColor.R, gbLegend.BackColor.G, gbLegend.BackColor.B)
        lblLegendLeft.ForeColor = colorLeft
        lblLegendRight.BackColor = Color.FromArgb(25, gbLegend.BackColor.R, gbLegend.BackColor.G, gbLegend.BackColor.B)
        lblLegendRight.ForeColor = colorRight
        lblDoubleSupport.BackColor = Color.FromArgb(25, gbLegend.BackColor.R, gbLegend.BackColor.G, gbLegend.BackColor.B)
        lblDoubleSupport.ForeColor = colorBoth
        Dim nMaxLegendWidth As Integer
        Select Case nGraphNum
            Case 1, 2, 10, conForce_Harm_Sum, conForce_Harm_Diff, 20, conDisp_Harm_Sum, conDisp_Harm_Diff, 30, conVel_Harm_Sum, conVel_Harm_Diff, 40, conPower_Harm_Sum, conPower_Harm_Diff, 50, conWork_Harm_Sum, conWork_Harm_Diff, 70
                lblDoubleSupport.Visible = True
                lblLegendLeft.Text = "Left Single Support"
                lblLegendRight.Text = "Right Single Support"
                If bWalkingOrRunning = False Then
                    lblDoubleSupport.Text = "Double Support"
                ElseIf bWalkingOrRunning = True Then
                    lblDoubleSupport.Text = "Float"
                End If
                gbLegend.Height = lblDoubleSupport.Top + lblDoubleSupport.Height + 6
                If nGraphNum <> 70 Then
                    gbLegend.Top = 0
                Else
                    gbLegend.Top = pnlGraph.Height - gbLegend.Height - 10
                End If
                nMaxLegendWidth = lblLegendLeft.Left + lblLegendLeft.Width + 3
                If nMaxLegendWidth < lblLegendRight.Left + lblLegendRight.Width + 5 Then nMaxLegendWidth = lblLegendRight.Left + lblLegendRight.Width + 3
                If nMaxLegendWidth < lblDoubleSupport.Left + lblDoubleSupport.Width + 8 Then nMaxLegendWidth = lblDoubleSupport.Left + lblDoubleSupport.Width + 3
                gbLegend.Width = nMaxLegendWidth
            Case 60
                lblDoubleSupport.Visible = True
                lblLegendLeft.Text = "Potential Energy"
                lblLegendRight.Text = "Kinetic Energy"
                lblDoubleSupport.Text = "Total Energy"
                lblDoubleSupport.Top = lblLegendRight.Top + lblLegendRight.Height + 5
                gbLegend.Height = lblDoubleSupport.Top + lblDoubleSupport.Height + 6
                nMaxLegendWidth = lblLegendLeft.Left + lblLegendLeft.Width + 3
                If nMaxLegendWidth < lblLegendRight.Left + lblLegendRight.Width + 5 Then nMaxLegendWidth = lblLegendRight.Left + lblLegendRight.Width + 3
                If nMaxLegendWidth < lblDoubleSupport.Left + lblDoubleSupport.Width + 8 Then nMaxLegendWidth = lblDoubleSupport.Left + lblDoubleSupport.Width + 3
                gbLegend.Width = nMaxLegendWidth
            Case 81, 82, 83, 84, 85, 86
                lblDoubleSupport.Visible = False
                gbLegend.Height = lblLegendRight.Top + lblLegendRight.Height + 3
                gbLegend.Left = 1
                gbLegend.Top = 2
                lblLegendLeft.Text = "Left Support"
                lblLegendRight.Text = "Right Support"
                nMaxLegendWidth = lblLegendLeft.Left + lblLegendLeft.Width + 3
                If nMaxLegendWidth < lblLegendRight.Left + lblLegendRight.Width + 5 Then nMaxLegendWidth = lblLegendRight.Left + lblLegendRight.Width + 3
            Case Else
                gbLegend.Visible = False
        End Select

        pnlYLabel.Visible = False
        Select Case nGraphNum
            Case 1, 2, 10, 11, 12, 20, 21, 22, 30, 31, 32, 40, 41, 42, 50, 51, 52, 60, 81, 82, 83, 84, 85
                pnlYLabel.Visible = True
        End Select

        If bCopyScreen = True Then
            SendKeys.Send("%{PRTSC}")
            bCopyScreen = False
        End If

    End Sub

    Private Sub mnuPrintForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintForm.Click

    End Sub

    Private Function VB6() As Object
        '   Throw New NotImplementedException
    End Function


    Private Sub OpenFScanFileDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFScanFileDialog.FileOk
        sPatientFileName = OpenFScanFileDialog.FileName
        If sPatientFileName = "" Then
            OpenFScanFileDialog.Dispose()
        End If
        sPatientFileName = Microsoft.VisualBasic.Left(sPatientFileName, Len(sPatientFileName) - 5)
        sLeftFileName = sPatientFileName + "L.fsx"
        sRightFileName = sPatientFileName + "R.fsx"
        lblFullFileNameL.Text = sLeftFileName
        lblFullFileNameR.Text = sRightFileName

    End Sub

    Private Sub openfscanfiledialog_Cancel()

    End Sub

   
End Class

Public Class Win32APICall

    Public Const DIB_RGB_COLORS = 0
    Public Const BI_RGB = 0
    Public Const WHITENESS = 16711778

    <DllImport("user32.dll", EntryPoint:="PrintWindow", _
                                SetLastError:=True, CharSet:=CharSet.Unicode, _
                                ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function PrintWindow(ByVal hWnd As IntPtr, ByVal hDC As IntPtr, ByVal dwFlags As Integer) As UInt32
    End Function

    <StructLayout(LayoutKind.Sequential, pack:=8, CharSet:=CharSet.Auto)> _
    Structure BITMAPINFOHEADER
        Dim biSize As Int32
        Dim biWidth As Int32
        Dim biHeight As Int32
        Dim biPlanes As Int16
        Dim biBitCount As Int16
        Dim biCompression As Int32
        Dim biSizeImage As Int32
        Dim biXPelsPerMeter As Int32
        Dim biYPelsPerMeter As Int32
        Dim biClrUsed As Int32
        Dim biClrImportant As Int32
    End Structure

    <DllImport("gdi32.dll", EntryPoint:="CreateDIBSection", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function CreateDIBSection(ByVal hdc As IntPtr, ByRef pbmi As BITMAPINFOHEADER, _
    ByVal iUsage As Int32, ByVal ppvBits As IntPtr, ByVal hSection As IntPtr, _
    ByVal dwOffset As Int32) As IntPtr
    End Function

    <DllImport("gdi32.dll", EntryPoint:="PatBlt", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function PatBlt(ByVal hDC As IntPtr, ByVal nXLeft As Int32, _
        ByVal nYLeft As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, _
        ByVal dwRop As Int32) As Boolean
    End Function

    <DllImport("gdi32.dll", EntryPoint:="SelectObject", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObj As IntPtr) As IntPtr
    End Function

    <DllImport("GDI32.dll", EntryPoint:="CreateCompatibleDC", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function CreateCompatibleDC(ByVal hRefDC As IntPtr) As IntPtr
    End Function

    <DllImport("GDI32.dll", EntryPoint:="DeleteDC", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function DeleteDC(ByVal hDC As IntPtr) As Boolean
    End Function

    <DllImport("GDI32.dll", EntryPoint:="DeleteObject", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function DeleteObject(ByVal hObj As IntPtr) As Boolean
    End Function

    <DllImport("User32.dll", EntryPoint:="ReleaseDC", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Boolean
    End Function

    <DllImport("User32.dll", EntryPoint:="GetDC", _
               SetLastError:=True, CharSet:=CharSet.Unicode, _
               ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function GetDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

End Class