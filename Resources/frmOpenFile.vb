Public Class frmOpenFile


    Dim sDriveLetterText As String = ""
    Dim sDriveNameText As String = ""
    Dim sDriveTypeText As String = ""
    Dim i As Integer = 0
    Dim sPath As String
    Dim bform As New OpenFileDialog
    Dim bButton As DialogResult = bform.ShowDialog



    Private Sub frmOpenFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MdiParent = System.Windows.Forms.Application.OpenForms.Item(0)
        mdi_bNewFileSelected = False
        OpenFScanFileDialog.InitialDirectory = mdi_DirectoryPath
        'OpenFScanFileDialog.ShowDialog()
        'OpenFScanFileDialog.Filter = "Tekscan files (*.fsx)|*.fsx"
        Exit Sub

        i = 0 'i is used to count the drives
        For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives
            If drive.IsReady = True Then
                sDriveNameText = drive.VolumeLabel
                Select Case drive.DriveType ' Need to find out what the Drive Type is.
                    Case IO.DriveType.Fixed : sDriveTypeText = "Local Disk"
                    Case IO.DriveType.CDRom : sDriveTypeText = "CD-ROM"
                    Case IO.DriveType.Removable : sDriveTypeText = "Removable"
                    Case IO.DriveType.Network : sDriveTypeText = "Network Drive"
                    Case IO.DriveType.Unknown : sDriveTypeText = "Unknown"
                End Select
                'sDriveLetterText = drive.Name
                'listDrives.Items.Add(sDriveLetterText) 'Put the disk letter in the first column
                'listDrives.Items(i).SubItems.Add(sDriveNameText) 'Put the Drive Name in the 2nd column
                'If listDrives.Columns(1).Width < 10 Then

                'End If
                'listDrives.Items(i).SubItems.Add(sDriveTypeText) 'Put the Drive Type in the Third column
                i = i + 1
            End If
        Next

        'Find the directories 
    End Sub
  
    Private Sub FolderBrowserDialogFscan_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)
        mdi_DirectoryPath = sPath
        Me.Close()
    End Sub


 

    Private Sub OpenFScanFileDialog_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFScanFileDialog.FileOk
        'When you select a file, you want to select both the left and right names.
        Dim sPatientName As String = OpenFScanFileDialog.FileName
        sPatientName = Microsoft.VisualBasic.Left(sPatientName, Len(sPatientName) - 5)

        mdi_sLeftFileName = sPatientName & "L.fsx"
        mdi_sRightFileName = sPatientName & "R.fsx"
        mdi_DirectoryPath = Microsoft.VisualBasic.Left(sPatientName, InStrRev(sPatientName, "\") - 1)

        Me.Close()

    End Sub

    Private Sub frmOpenFile_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        'If you click the cancel button, then the form closes and you go back to the Mdi form.
        If bButton = DialogResult.Cancel Then
            Me.Close()
        End If
    End Sub
End Class