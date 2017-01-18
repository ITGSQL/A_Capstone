Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        litHdrImage.Text = "<img src=""images/Larrys.jpg"" />"
        ''litHdrImage.Text = "<span style=""color: white; font-size: 2em; font-weight: bold;>LSS</span>"
    End Sub

    Protected Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Dim strSQL As String = "exec udp_backupLSSDB"
        g_IO_Execute_SQL(strSQL, False)
    End Sub
End Class