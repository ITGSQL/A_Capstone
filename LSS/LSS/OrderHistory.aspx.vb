Public Class OrderHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Session("USER_ID")) Then
                Response.Redirect("Login.aspx")
            End If

            If Not IsNothing(Request.QueryString("mid")) Then
                Dim mid As String = Request.QueryString("mid").ToString.ToUpper

                If mid = "VS" Then
                    litMessage.Text = "<span class=""message"">Order has been voided successfully.</span>"
                End If
            End If

                Dim strSQL As String = "Select header_id, c.customer_id as CUST_ID, C.FIRST_NAME + ' ' + C.LAST_NAME AS 'CUSTOMER_NAME', (CONVERT(VARCHAR(2),MONTH(DATE_CREATED)) + '-' + CONVERT(VARCHAR(2),DAY(DATE_CREATED)) + '-' + CONVERT(VARCHAR(4),YEAR(DATE_CREATED))) AS DATE_CREATED, IS_PAID, GRAND_TOTAL, (select count(*) from sales.DETAILS D where d.HEADER_ID = h.HEADER_ID) as totalItems 
                from lss.sales.HEADER H 
                INNER JOIN SALES.CUSTOMER C ON H.CUSTOMERS_ID = C.CUSTOMER_ID
                where IS_PAID = 1"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

            If tblResults.Rows.Count > 0 Then
                rptrOrderHistory.DataSource = tblResults
                rptrOrderHistory.DataBind()
            End If

        End If
    End Sub

End Class