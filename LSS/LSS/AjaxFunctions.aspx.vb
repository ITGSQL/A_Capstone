Public Class AjaxFunctions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If Not IsNothing(Request.QueryString("action")) Then
                Dim action As String = Request.QueryString("Action").ToString.ToUpper

                'sessionTimeRemaining'
                If action = "SESSIONTIMEOUT" Then
                    Session.Clear()
                    Response.Redirect("Login.aspx?session=TIMEOUT")
                End If

            End If





        End If
    End Sub

End Class