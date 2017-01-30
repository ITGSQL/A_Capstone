Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        litHdrImage.Text = "<img src=""images/Larrys.jpg"" />"

        If Not IsNothing("SESSION") Then
            litMessage.Text = "Your session has timed out."
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If validateUser(txtUserID.Text.Replace("'", "''"), txtPassword.Text.Replace("'", "''")) Then
            Response.Redirect("Default.aspx")
        Else
            litMessage.Text = "Login Failed."
        End If
    End Sub
End Class