<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpenFile
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
        Me.OpenFScanFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'OpenFScanFileDialog
        '
        Me.OpenFScanFileDialog.InitialDirectory = """C:\PROGRAM FILES (X86)\TEKSCAN\RESEARCH\DATABASE"""
        Me.OpenFScanFileDialog.Title = "Open an Fscan File"
        '
        'frmOpenFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(214, 43)
        Me.Name = "frmOpenFile"
        Me.Text = "Open an Fscan File"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFScanFileDialog As System.Windows.Forms.OpenFileDialog
End Class
