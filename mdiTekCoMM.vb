Option Explicit On

Imports System.Runtime.InteropServices
Imports System.IO
Imports Microsoft.VisualBasic

Public Class frmMdiTekGait

    Dim ii, jj, kk As Integer
    Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As Long, ByVal lParam As Long) As Long
    Declare Function EnumChildWindows Lib "user32" (ByVal hWndParent As Long, ByVal lpEnumFunc As Long, ByVal lParam As Long) As Long
    Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (ByVal hwnd As Long, ByVal lpClassName As String, ByVal nMaxCount As Long) As Long
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
    Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As Long) As Long
    Declare Function SendMessageS Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As String) As Long
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As String) As Long

    Public colDrives As New Collection 'This is a list of all the drives on the machine.  
    Public colDirectories As New Collection 'This is the list of the top directories to search on each drive to search
    Public colFSXFolders As New Collection ' This is a list of all the folders tha contain fsx files.

    Private Sub frmMdiTekGait_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim WFD As WIN32_FIND_DATA = Nothing
        Dim sOptionFilePath As String
        Dim hSearch As Integer 'Search Handle
        Dim fileOptions As String
        Dim sOptionFile As String
        Dim nFileNum As Integer
        Dim RetVal As Long
        Dim sFileOpenName As String

        frmSplashScreen.Show() 'Show the splash screen
        Threading.Thread.Sleep(2000) 'Show splash screen for 2 seconds

        Dim drive As IO.DriveInfo
        For Each drive In IO.DriveInfo.GetDrives 'While the splash screen is showing, we want to get all the drives on the machine.
            If drive.IsReady = True Then
                colDrives.Add(drive.Name)  'Put the disk letter in the collection of drives.
            End If
        Next

        
        For i = 1 To colDrives.Count 'This routine identifies the folders on the drives where fsx files might be located.
            Dim path As String = colDrives.Item(i)
            Dim dirInfo As New System.IO.DirectoryInfo(path)
            Dim directories() As IO.DirectoryInfo
            directories = dirInfo.GetDirectories
            For j = 0 To directories.Count - 1 'This finds the directories to search for fsx files
                Select Case directories(j).Name
                    ' The first case are the directories you don't want to search
                    Case "$Recycle.Bin", "$RECYCLE.BIN", "Boot", "Drivers", "Intel", "PerfLogs", "ProgramData", "Recovery", "System Volume Information", "UserGuidePDF", "Windows", "Application", "MSOCache", "Windows.old"
                    Case Else 'These are the directories you do want to add
                        colDirectories.Add(directories(j).FullName)
                End Select
            Next j
        Next i

        For i = 1 To colDirectories.Count 'This FOR block checks if the directories that contain FSX files are in the collection of FSX folders
            Dim di As IO.DirectoryInfo = New IO.DirectoryInfo(colDirectories(i))
            Try
                Dim files() As IO.FileInfo = di.GetFiles("*.fsx", SearchOption.AllDirectories)
                For Each fi In di.GetFiles("*.fsx", SearchOption.AllDirectories)
                    colFSXFileDirectories.Add(fi.DirectoryName)
                Next
                For j = 0 To files.Count - 1
                    Dim sDir As String = files(j).DirectoryName
                    If colFSXFolders.Count <> 0 Then
                        Dim bExists As Boolean = False
                        For k = 1 To colFSXFolders.Count 'Look through the colFSXFolders for the directory name
                            If colFSXFolders(k) = sDir Then
                                bExists = True 'Set the Flag to True
                                k = colFSXFolders.Count 'Increase the value of k to the full count in the Folder collection so that it will exit.
                            End If
                        Next k
                        If bExists = False Then colFSXFolders.Add(sDir) 'If you didn't find the directory, then add it to the collection.
                    Else : colFSXFolders.Add(sDir)
                    End If
                Next j
            Catch ex As UnauthorizedAccessException
                'MsgBox(String.Format("Log Your Exceptions Here:-{0}{0}{1}", Environment.NewLine, ex.Message))
            End Try
            Next i




        frmSplashScreen.Close() 'Close the splash screen

        bEnglishOrMetricUnits = Globalization.RegionInfo.CurrentRegion.IsMetric 'Is the machine naturally a metric or english units.
        cultureInfo = Globalization.CultureInfo.CurrentCulture 'Get information about the country
        sLanguage = cultureInfo.EnglishName 'Get the name of the language
        nLCID = cultureInfo.LCID 'Get the number of the language.

        'Find out if the Fscan program is already running.
        Dim processes() As Process
        Dim instance As Process
        Dim process As New Process()
        processes = process.GetProcesses
        bFscanOpen = False
        For Each instance In processes 'Looks through every instance that is running on the machine
            If instance.ProcessName = "clinres" Then 'If you find the Fscan program is running
                bFscanOpen = True 'This boolen is set to true to tell you that the Fscan program is open.
                sFileOpenName = instance.MainWindowTitle 'The name of the file is at the top of the program.
                sFileOpenName = Strings.Right(sFileOpenName, Len(sFileOpenName) - 18) 'Subtract the first 18 letters from the text title
                sFileOpenName = Strings.Left(sFileOpenName, Microsoft.VisualBasic.InStr(sFileOpenName, ".fsx") + 3) 'These two lines get the name of the file.
                Exit For
            End If
        Next

        If bFscanOpen = True Then 'If the Fscan program is running you need to find the file and read it and transfer it to the new Graph Form.

        Else 'If the Fscan program is not running then you need to open the file dialogue
            ' Dim formFindFile As New frmOpenFile
            'formFindFile.Show()
            'formFindFile.Close()
        End If



        Me.Width = 0.8 * Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = 0.8 * Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height



        fileOptions = My.Application.Info.DirectoryPath & "\TekCoMM Options.rtf"
        hSearch = FindFirstFile(fileOptions, WFD)
        Dim nSt, nEnd As Short
        If hSearch = INVALID_HANDLE_VALUE Then 'if you don't find the file 'TekgaitOptions.rtf' then set the colors.
            sOptionFilePath = My.Application.Info.DirectoryPath
            colorBackground = ColorTranslator.FromOle(RGB(225, 225, 225))
            colorLeft = ColorTranslator.FromOle(RGB(0, 0, 255))
            colorRight = ColorTranslator.FromOle(RGB(0, 150, 60))
            colorBoth = ColorTranslator.FromOle(RGB(255, 0, 0))
            colorGrid = ColorTranslator.FromOle(RGB(192, 192, 192))
            colorHarm = ColorTranslator.FromOle(RGB(180, 150, 100))
            colorPicText = ColorTranslator.FromOle(RGB(50, 50, 50))
            colorComp1 = colorLeft
            colorComp2 = colorRight

            sOptionFile = "Folder:" & My.Application.Info.DirectoryPath & Chr(13) & "ColorBackground:" & ColorTranslator.ToOle(colorBackground) & Chr(13) & "ColorLeft:" & ColorTranslator.ToOle(colorLeft) & Chr(13) & "ColorRight:" & ColorTranslator.ToOle(colorRight) & Chr(13) & "ColorBoth:" & ColorTranslator.ToOle(colorBoth) & Chr(13) & "ColorGrid:" & ColorTranslator.ToOle(colorGrid) & Chr(13) & "ColorHarm:" & ColorTranslator.ToOle(colorHarm) & Chr(13) & "ColorPicText:" & ColorTranslator.ToOle(colorPicText) & Chr(13) & "ColorComp1:" & ColorTranslator.ToOle(colorComp1) & Chr(13) & "ColorComp2:" & ColorTranslator.ToOle(colorComp2) & "EndOfOptionFile"
            nFileNum = FreeFile()
            FileOpen(nFileNum, fileOptions, OpenMode.Output)
            PrintLine(nFileNum, sOptionFile)
            FileClose(nFileNum)

        Else 'read the default options
            nFileNum = FreeFile()
            FileOpen(nFileNum, fileOptions, OpenMode.Input)
            sOptionFile = InputString(nFileNum, LOF(nFileNum))
            FileClose(nFileNum)
            nSt = InStr(sOptionFile, "Folder:")
            nEnd = InStr(sOptionFile, "ColorBackground")
            If nSt <> 0 And nEnd <> 0 Then
                sOptionFilePath = Mid(sOptionFile, nSt + Len("Folder:"), nEnd - nSt - Len("Folder:"))
            Else
                sOptionFilePath = My.Application.Info.DirectoryPath
            End If
            mdi_DirectoryPath = sOptionFilePath
            If Asc(Microsoft.VisualBasic.Right(sOptionFilePath, 1)) = 13 Then sOptionFilePath = Microsoft.VisualBasic.Left(sOptionFilePath, Len(sOptionFilePath) - 1)
            nSt = InStr(sOptionFile, "ColorBackground") 'Find the ColorBackground word
            nEnd = InStr(sOptionFile, "ColorLeft")
            If nSt <> 0 And nEnd <> 0 Then
                colorBackground = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorBackground:"), nEnd - nSt - Len("ColorBackground:"))))
            Else
                colorBackground = ColorTranslator.FromOle(RGB(225, 225, 225))
            End If
            If colorBackground = Color.Black Then colorBackground = ColorTranslator.FromOle(RGB(225, 225, 225))
            nSt = InStr(sOptionFile, "ColorLeft") 'Find the ColorLeft word
            nEnd = InStr(sOptionFile, "ColorRight")
            If nSt <> 0 And nEnd <> 0 Then
                colorLeft = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorLeft:"), nEnd - nSt - Len("ColorLeft:"))))
            Else
                colorLeft = ColorTranslator.FromOle(RGB(0, 0, 255))
            End If
            If colorLeft = Color.Black Then colorLeft = ColorTranslator.FromOle(RGB(0, 0, 225))
            nSt = InStr(sOptionFile, "ColorRight") 'Find the "ColorRight" word and set
            nEnd = InStr(sOptionFile, "ColorBoth")
            If nSt <> 0 And nEnd <> 0 Then
                colorRight = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorRight:"), nEnd - nSt - Len("ColorRight:"))))
            Else
                colorRight = ColorTranslator.FromOle(RGB(0, 255, 0))
            End If
            If colorRight = ColorTranslator.FromOle(0) Then colorRight = ColorTranslator.FromOle(RGB(0, 0, 225))
            nSt = InStr(sOptionFile, "ColorBoth") 'Find the ColorBoth word and set
            nEnd = InStr(sOptionFile, "ColorGrid")
            If nSt <> 0 And nEnd <> 0 Then
                colorBoth = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorBoth:"), nEnd - nSt - Len("ColorBoth:"))))
            Else
                colorBoth = ColorTranslator.FromOle(RGB(255, 0, 0))
            End If
            If colorBoth = ColorTranslator.FromOle(0) Then colorBoth = ColorTranslator.FromOle(RGB(255, 0, 0))
            nSt = InStr(sOptionFile, "ColorGrid") 'Find the ColorGrid word and set
            nEnd = InStr(sOptionFile, "ColorHarm")
            If nSt <> 0 And nEnd <> 0 Then
                colorGrid = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorGrid:"), nEnd - nSt - Len("ColorGrid:"))))
            Else
                colorGrid = ColorTranslator.FromOle(RGB(192, 192, 192))
            End If
            If colorGrid = ColorTranslator.FromOle(0) Then colorGrid = ColorTranslator.FromOle(RGB(192, 192, 192))
            nSt = InStr(sOptionFile, "ColorHarm") 'Find the ColorHarm word and set
            nEnd = InStr(sOptionFile, "ColorPicText")
            If nSt <> 0 And nEnd <> 0 Then
                colorHarm = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorHarm:"), nEnd - nSt - Len("ColorHarm:"))))
            Else
                colorHarm = ColorTranslator.FromOle(RGB(180, 150, 100))
            End If
            If colorHarm = ColorTranslator.FromOle(0) Then colorHarm = ColorTranslator.FromOle(RGB(180, 150, 100))
            nSt = InStr(sOptionFile, "ColorPicText") 'Find the ColorPicText word and set
            nEnd = InStr(sOptionFile, "ColorComp1")
            If nSt <> 0 And nEnd <> 0 Then
                colorPicText = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorPicText:"), nEnd - nSt - Len("ColorPicText:"))))
            Else
                colorPicText = ColorTranslator.FromOle(RGB(50, 50, 50))
            End If
            If colorPicText = ColorTranslator.FromOle(0) Then colorPicText = ColorTranslator.FromOle(RGB(50, 50, 50))
            nSt = InStr(sOptionFile, "ColorComp1") 'Find the ColorComp1 word and set
            nEnd = InStr(sOptionFile, "ColorComp2")
            If nSt <> 0 And nEnd <> 0 Then
                colorComp1 = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorComp1:"), nEnd - nSt - Len("ColorComp1:"))))
            Else
                colorComp1 = colorLeft
            End If
            If colorComp1 = Nothing Then colorComp1 = colorLeft
            nSt = InStr(sOptionFile, "ColorComp2") 'Find the ColorComp1 word and set
            nEnd = InStr(sOptionFile, "EndOfOptionFile")
            If nSt <> 0 And nEnd <> 0 Then 'This means that the word was found
                colorComp2 = ColorTranslator.FromOle(CInt(Mid(sOptionFile, nSt + Len("ColorComp2:"), nEnd - nSt - Len("ColorComp2:"))))
            Else
                colorComp2 = colorRight
            End If
            If colorComp2 = Nothing Then colorComp2 = colorRight

        End If

        Me.Text = "TekCoMM"
        mnuOpen.Text = funTranslateMenuItem("Open")
        mnuHelpContents.Text = funTranslateMenuItem("Help")
        mnuExit.Text = funTranslateMenuItem("Exit")
        mnuAbout.Text = funTranslateMenuItem("About")

        'click on the file MenuOpen
        ' mnuOpen.PerformClick()


    End Sub

    Private Sub mnuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        'Dim formFindFile As New frmOpenFile
        For Me.ii = 1 To System.Windows.Forms.Application.OpenForms.Count
            If System.Windows.Forms.Application.OpenForms(ii - 1).Name = "frmCompareGraphs" Then
                System.Windows.Forms.Application.OpenForms(ii - 1).Close()
            ElseIf System.Windows.Forms.Application.OpenForms(ii - 1).Name = "frmGaitIndexes" Then
                System.Windows.Forms.Application.OpenForms(ii - 1).Close()
            End If
        Next ii

        'Start a new frmGraph form and it will open the dialog box
        Dim formGraph As New frmGraph
        formGraph.Show()

        '       formFindFile.Show()
        '      formFindFile.Close()
        'If mdi_bNewFileSelected = True Then
        'Dim formGraph As New frmGraph
        'formGraph.Show() 'Start the frmGraph for this grial.
        'End If
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

        'Dim sOptionFilePath As String
        Dim fileOptions As String
        Dim sOptionFile As String
        Dim nFileNum As Integer

        sOptionFile = "Folder:" & mdi_DirectoryPath & Chr(13)
        sOptionFile = sOptionFile & "ColorBackground:" & ColorTranslator.ToOle(colorBackground) & Chr(13)
        sOptionFile = sOptionFile & "ColorLeft:" & ColorTranslator.ToOle(colorLeft)
        sOptionFile = sOptionFile & Chr(13) & "ColorRight:" & ColorTranslator.ToOle(colorRight) & Chr(13)
        sOptionFile = sOptionFile & "ColorBoth:" & ColorTranslator.ToOle(colorBoth) & Chr(13)
        sOptionFile = sOptionFile & "ColorGrid:" & ColorTranslator.ToOle(colorGrid) & Chr(13)
        sOptionFile = sOptionFile & "ColorHarm:" & ColorTranslator.ToOle(colorHarm) & Chr(13)
        sOptionFile = sOptionFile & "ColorPicText:" & ColorTranslator.ToOle(colorPicText) & Chr(13)
        sOptionFile = sOptionFile & "ColorComp1:" & ColorTranslator.ToOle(colorComp1) & Chr(13)
        sOptionFile = sOptionFile & "ColorComp2:" & ColorTranslator.ToOle(colorComp2) & "EndOfOptionFile"
        nFileNum = FreeFile()

        fileOptions = My.Application.Info.DirectoryPath & "\CoM'nalysis Options.rtf"
        FileOpen(nFileNum, fileOptions, OpenMode.Output)
        PrintLine(nFileNum, sOptionFile)
        FileClose(nFileNum)

        Me.Close()

    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'formFindFile.Show()
    End Sub


End Class