Public Class Orders
    Inherits System.Web.UI.Page
    Dim strSQL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hidePanels()

        If IsNothing(Request.QueryString("Cust")) Then
            ''No customer selected
            pnlCustomerSearch.Visible = True
        Else
            ''Load the customer details
            loadCustomerDetails()
            pnlOrderDetails.Visible = True
        End If
    End Sub

    Private Sub hidePanels()
        pnlAddItem.Visible = False
        pnlCustomer.Visible = False
        pnlCustomerSearch.Visible = False
        pnlOrderDetails.Visible = False
    End Sub

    Private Sub loadCustomerDetails()
        strSQL = "Select C.*, S.state_name from Sales.Customer C  inner join dbo.state S on S.state_id = C.state_id where customer_id = " & Request.QueryString("cust") & ""
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            lblCustomerName.Text = tblResults(0)("First_Name") & " " & tblResults(0)("Last_Name")
        Else
            Response.Redirect("Orders.aspx")
        End If
    End Sub

    Protected Sub btnCustomerSearch_Click(sender As Object, e As EventArgs) Handles btnCustomerSearch.Click

    End Sub

    Protected Sub btnClearNewCustomerForm_Click(sender As Object, e As EventArgs) Handles btnClearNewCustomerForm.Click
        clearNewCustomerForm()
    End Sub

    Private Sub clearNewCustomerForm()
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = ""
        ddlState.SelectedValue = -1
        txtZip.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
    End Sub

    Protected Sub btnSaveNewCustomer_Click(sender As Object, e As EventArgs) Handles btnSaveNewCustomer.Click
        strSQL = "exec [dbo].[udp_mergeCustomer] @FName = '" & txtFirstName.Text.Replace("'", "''") & "'," &
            "@LName = '" & txtLastName.Text.Replace("'", "''") & "', @Add1 = '" & txtAddress1.Text.Replace("'", "''") & "'," &
            "@Add2 = '" & txtAddress2.Text.Replace("'", "''") & "', @City = '" & txtCity.Text.Replace("'", "''") & "'," &
            "@State = " & ddlState.SelectedValue & "," & "@Zip = '" & txtZip.Text.Replace("'", "''") & "'," &
            "@Email = '" & txtEmail.Text.Replace("'", "''") & "', @Phone = '" & txtPhone.Text.Replace("'", "''") & "'"
        g_IO_Execute_SQL(strSQL, False)
        strSQL = "Select * from SALES.CUSTOMER where phone = '" & txtPhone.Text.Replace("'", "''") & "'"
        Response.Redirect("Orders.aspx?Type=New&Cust=" & g_IO_Execute_SQL(strSQL, False)(0)(0))
    End Sub

    Protected Sub btnShowNewCustomerPanel_Click(sender As Object, e As EventArgs) Handles btnShowNewCustomerPanel.Click
        loadDropDown_States()
        pnlCustomerSearch.Visible = False
        pnlCustomer.Visible = True
    End Sub

    Private Sub loadDropDown_States()
        strSQL = "Select state_id, state_name from dbo.State order by State_Name"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--Select --", "state_name", "state_id")
        ddlState.DataValueField = "State_Id"
        ddlState.DataTextField = "State_Name"
        ddlState.DataSource = tblResults
        ddlState.DataBind()
    End Sub
End Class