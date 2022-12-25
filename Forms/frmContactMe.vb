Public Class frmContactMe

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub lnkSocial_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkSocial.LinkClicked
        Process.Start("https://facebook.com/actavista.org")
    End Sub

    Private Sub lnkEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkEmail.LinkClicked
        Process.Start("mailto:support@actavista.org")
    End Sub

    Private Sub lnkWebsite_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebsite.LinkClicked
        Process.Start("https://actavista.org")
    End Sub
End Class