Public Class Customers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Session("USER_ID")) Then
                Response.Redirect("Login.aspx")
            End If
        End If

    End Sub

End Class