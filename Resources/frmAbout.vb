Public Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = System.Windows.Forms.Application.OpenForms.Item(0) 'This sets this as a child form

        lblTekGait.Left = 0.5 * (Me.Width - lblTekGait.Width)
        lblVersion.Left = 0.5 * (Me.Width - lblVersion.Width)
        lblDescription.Left = 0.5 * (Me.Width - lblDescription.Width)
        lblCopyright.Left = 0.5 * (Me.Width - lblCopyright.Width)

    End Sub

    Private Sub butClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub
End Class