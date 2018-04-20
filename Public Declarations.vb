Module Public_Declarations

    'The following constants are needed for international issues
    Public cultureInfo As Globalization.CultureInfo
    Public sLanguage As String
    Public nLCID As Integer
    Public sLocalDecimalChar As String

    Public bEnglishOrMetricUnits As Boolean 'This is needed for international users.  False = Englisih,  True = Metric
    Public bFscanOpen As Boolean 'This is needed for knowing if the Fscan program is running.

    Public colorBackground As Color
    Public colorLeft As Color
    Public colorRight As Color
    Public colorBoth As Color
    Public colorGrid As Color
    Public colorHarm As Color
    Public colorPicText As Color
    Public colorComp1 As Color 'This is for the first comparison color
    Public colorComp2 As Color 'This is for the second comparison color

    'These variables are used to transfer data from the frmOpenFile to the new frmGraph.
    Public mdi_DirectoryPath As String 'This string keeps track of which folder you were last working in
    Public mdi_sLeftFileName As String = ""
    Public mdi_sRightFileName As String = ""
    Public mdi_bNewFileSelected As Boolean = False 'This flag keeps track if you have a new file to start opening.  It is set to true until both the left and right files are both read.
    Public mdi_sFirstName As String = ""
    Public mdi_sLastName As String = ""
    Public mdi_sChartNumber As String = ""
    Public mdi_DateAndTime As Date = Now
    Public mdi_bWearingCustomShoes As Boolean
    Public mdi_bWearingOrthotics As Boolean
    Public mdi_bHeelLift As Boolean
    Public mdi_sHeelLiftSide As String
    Public mdi_nHeelLiftHeight As String
    Public mdi_bDiabetic As Boolean
    Public mdi_bNeuropathic As Boolean
    Public mdi_bPVD As Boolean
    Public mdi_sComments As String = ""
    Public mdi_sOrthoticRx As String = ""

    'The following are collections that will be needed.
    Public colFSXFileDirectories As New Collection
    Public colFileName As New Collection
    Public colBriefFileName As New Collection
    Public colDisplacement As New Collection
    Public colVelocity As New Collection
    Public colBodyWeight As New Collection
    Public colBodyWeightPct As New Collection
    Public colLeftForce As New Collection
    Public colRightForce As New Collection
    Public colTotalForce As New Collection
    Public colGaitPhase As New Collection
    Public colPower As New Collection
    Public colWork As New Collection
    Public colSpringConstants As New Collection
    Public colCoPLoc_AP_L As New Collection
    Public colCoPLoc_AP_R As New Collection
    Public colCoPLoc_ML_L As New Collection
    Public colCoPLoc_ML_R As New Collection
    Public colCoPVel_AP_L As New Collection
    Public colCoPVel_AP_R As New Collection
    Public colCoPVel_ML_L As New Collection
    Public colCoPVel_ML_R As New Collection
    Public colCoPAcc_AP_L As New Collection
    Public colCoPAcc_AP_R As New Collection
    Public colCoPAcc_ML_L As New Collection
    Public colCoPAcc_ML_R As New Collection
    Public colEnergy_Potential As New Collection
    Public colEnergy_Kinetic As New Collection
    Public colEnergy_Total As New Collection
    Public colGI As New Collection
    Public colCoPSymmetryIndex As New Collection
    Public colCoPPurityIndex_L As New Collection
    Public colCoPPurityIndex_R As New Collection
    Public colCoPPurityIndex_Avg As New Collection

    Public frmActiveForm As frmGraph


    'The following constants are needed for API calls
    Public Const INVALID_HANDLE_VALUE As Short = -1
    Public Const MAX_PATH As Short = 260


    'The following constant conversion values need to be used in the program.
    Public Const Lbs_To_Slugs As Double = 0.03108095017    'to convert 1 lb. to slugs
    Public Const Lbs_To_Newtons As Double = 4.448221615 'to convert 1 lb. to newtons 
    Public Const Lbs_To_Kgs As Double = 0.453592409232511    'to convert 1 lb. to kilograms
    Public Const In_To_Cm As Double = 2.54 'to convert 1" to centimeters
    Public Const MetricGrav As Double = 9.81    'The GravitationalConstant for metric , meters per second^2
    Public Const EnglishGrav As Double = 32.1522    'The gravitational constant for english, feet per second^2
    Public Const Feet_To_In As Short = 12    'to convert ft/sec to in/sec
    Public Const Feet_To_cm As Double = 30.48    'to convert velocity, distance or acceleration ft/sec to cm/sec
    Public Const FtLbs_To_NewtonM As Double = 1.355818    'to convert Power (ft-lbs/sec to watts (joules/sec)) or Work (ft-lbs to joules)
    Public Const Sensels_To_Inches As Double = 0.2    'To convert Sensel Size to Inches
    Public Const Sensels_To_Cm As Double = 0.508 'To convert Sensel Size to Centimeters

    'The following constants identify walking or running state
    Public Const con_Walking As Byte = 0
    Public Const con_Running As Byte = 1

    'The following constants are for identifying the various graphs
    Public Const conForce_AllSteps As Short = 1
    Public Const conForce_Avg As Short = 2
    Public Const conForce_Radial As Short = 3
    Public Const conForce_As_BW As Short = 10
    Public Const conForce_Harm_Sum As Short = 11
    Public Const conForce_Harm_Diff As Short = 12
    Public Const conForce_Harm_Amp As Short = 13
    Public Const conForce_Harm_Eq As Short = 14
    Public Const conForce_Harm_Eq_Angle As Short = 15
    Public Const conDisplacement_Vert As Short = 20
    Public Const conDisp_Harm_Sum As Short = 21
    Public Const conDisp_Harm_Diff As Short = 22
    Public Const conDisp_Harm_Amp As Short = 23
    Public Const conDisp_Harm_Eq As Short = 24
    Public Const conDisp_Harm_Eq_Angle As Short = 25
    Public Const conVelocity_Vert As Short = 30
    Public Const conVel_Harm_Sum As Short = 31
    Public Const conVel_Harm_Diff As Short = 32
    Public Const conVel_Harm_Amp As Short = 33
    Public Const conVel_Harm_Eq As Short = 34
    Public Const conVel_Harm_Eq_Angle As Short = 35
    Public Const conPower_Vert As Short = 40
    Public Const conPower_Harm_Sum As Short = 41
    Public Const conPower_Harm_Diff As Short = 42
    Public Const conPower_Harm_Amp As Short = 43
    Public Const conPower_Harm_Eq As Short = 44
    Public Const conPower_Harm_Eq_Angle As Short = 45
    Public Const conWork_Vert As Short = 50
    Public Const conWork_Harm_Sum As Short = 51
    Public Const conWork_Harm_Diff As Short = 52
    Public Const conWork_Harm_Amp As Short = 53
    Public Const conWork_Harm_Eq As Short = 54
    Public Const conWork_Harm_Eq_Angle = 55
    Public Const conEnergy As Short = 60
    Public Const conSpringConstants As Short = 70
    Public Const conCoP_AP_ML As Short = 80
    Public Const conCoP_AP As Short = 81
    Public Const conCoP_ML As Short = 82
    Public Const conCoP_AP_Vel As Short = 83
    Public Const conCoP_ML_Vel As Short = 84
    Public Const conCoP_AP_Acc As Short = 85
    Public Const conCoP_ML_Acc As Short = 86

    'The following constants identify the phase of gait.
    Public Const con_L_Double_Support As Short = 1
    Public Const con_L_Single_Support As Short = 2
    Public Const con_R_Double_Support As Short = 3
    Public Const con_R_Single_Support As Short = 4
    Public Const con_L_Float As Short = 5
    Public Const con_R_Float As Short = 6

    'The following constants are used in the HarmonicsValues
    Public Const con_Cos As Short = 0
    Public Const con_Sin As Short = 1
    Public Const con_Amp As Short = 2

    'The following constants are used for the beginning and end of any substring
    Public Const conStart As Byte = 0
    Public Const conEnd As Byte = 1

    'This API function looks for a file in the directory.
    Public Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer

    'This function will take a FILETIME function and return a GMT value
    Public Declare Function FileTimeToSystemTime Lib "kernel32" (ByRef lpFileTime As FILETIME, ByRef lpSystemTime As SYSTEMTIME) As Integer

    'This function takes the GMT time to the local time
    Public Declare Function FileTimeToLocalFileTime Lib "kernel32" (ByRef lpFileTime As FILETIME, ByRef lpLocalFileTime As FILETIME) As Integer

    Public Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
    Public Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    Public Declare Function GetPixel Lib "gdi32" (ByVal hdc As Integer, ByVal x As Integer, ByVal y As Integer) As Integer

    Public Structure FILETIME
        Dim dwLowDateTime As UInteger
        Dim dwHighDateTime As UInteger
    End Structure

    Public Structure SYSTEMTIME
        Dim wYear As Integer
        Dim wMonth As Integer
        Dim wDayOfWeek As Integer
        Dim wDay As Integer
        Dim wHour As Integer
        Dim wMinute As Integer
        Dim wSecond As Integer
        Dim wMilliseconds As Integer
    End Structure

    Public Structure WIN32_FIND_DATA
        Dim dwFileAttributes As Integer
        Dim ftCreationTime As FILETIME
        Dim ftLastAccessTime As FILETIME
        Dim ftLastWriteTime As FILETIME
        Dim nFileSizeHigh As Integer
        Dim nFileSizeLow As Integer
        Dim dwReserved0 As Integer
        Dim dwReserved1 As Integer
        <VBFixedString(MAX_PATH), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=(MAX_PATH + 1))> Public cFileName() As Char
        <VBFixedString(14), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=(14 + 1))> Public cAlternate() As Char
    End Structure

    Public Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure

    'The GaitIndices structure contains all 4 name indices.
    Public Structure structGaitIndices
        Dim Purity As Double
        Dim Symmetry As Double
        Dim CoP As Double
        Dim Energy As Double
        Dim Overall As Double
        Dim sName As String
    End Structure

    'For each percent of the gait cycle, we need the following values. _
    'By making this a structure, we reduce from a 3 dimension to a 2 dimensional array.
    Public Structure structPercentData
        Dim nPhase As Byte
        Dim nLForce As Double
        Dim nRForce As Double
        Dim nCoP_ML_L As Double
        Dim nCoP_AP_L As Double
        Dim nCoP_ML_R As Double
        Dim nCoP_AP_R As Double
        Dim nTotalForce As Double
        Dim nTime As Double
    End Structure

    'If we want to make a Spline, we will need the following values _
    'so we will make the structSpline as a type.
    Public Structure structSpline
        Dim a As Double 'This is for the constant in the spline which are the y values at the nodes.
        Dim b As Double 'This is th coefficient for x^2
        Dim c As Double 'This is the coefficient of x^2
        Dim d As Double 'This is the coefficient for x^3
        Dim x As Double 'This is the X values
        Dim h As Double 'These are the differences between the x values
        Dim alpha As Double 'These are the alpha values
        Dim mu As Double
        Dim z As Double
        Dim l As Double
    End Structure

End Module
