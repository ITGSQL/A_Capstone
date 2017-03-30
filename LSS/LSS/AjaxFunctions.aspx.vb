Public Class AjaxFunctions
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Session("USER_ID")) Then
                Response.Redirect("Login.aspx")
            End If


            If Not IsNothing(Request.QueryString("action")) Then
                Dim action As String = Request.QueryString("Action").ToString.ToUpper

                'sessionTimeRemaining'
                If action = "SESSIONTIMEOUT" Then
                    Session.Clear()
                    Response.Redirect("Login.aspx?session=TIMEOUT")
                ElseIf action = "DELETEFROMTEMPORDER" Then
                    RemoveFromTempOrder()
                End If

            End If





        End If
    End Sub

    Private Sub RemoveFromTempOrder()
        strSQL = "Delete from SALES.DETAILS where DETAILS_ID = " & Request.QueryString("id")
        g_IO_Execute_SQL(strSQL, False)
        incrementProductInventory(Request.QueryString("Prod"), Request.QueryString("qty"))
        Response.Redirect("Orders.aspx?Type=New&cust=" & Request.QueryString("cust"))
    End Sub

End Class