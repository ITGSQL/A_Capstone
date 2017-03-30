Public Class CustomerManagment
    Inherits System.Web.UI.Page

    Private PageSize As Integer = 25


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Session("USER_ID")) Then
                Response.Redirect("Login.aspx")
            End If

            hidepanels()
            ''Are we looking at a customer?
            If IsNothing(Request.QueryString("ID")) Then
                GetCustomersPageWise(1)
                pnlCustomer_Listing.Visible = True
            Else
                loadDropDown_States()
                loadCustomerDetails()
                pnlCustomer_Action.Visible = True
            End If
        End If
    End Sub

    Private Sub loadDropDown_States()
        Dim strSQL As String = "Select * from dbo.State order by State_Name"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        ddlState.DataTextField = "State_Name"
        ddlState.DataValueField = "State_ID"
        ddlState.DataSource = tblResults
        ddlState.DataBind()
    End Sub

    Private Sub clearForm()
        txtCustomerID.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = ""
        txtZip.Text = ""
    End Sub

    Private Sub loadCustomerDetails()
        ''Set as session to prevent user error in the querystring
        Session("CurCustomerUpdate") = Request.QueryString("ID")

        ''Pull Data
        Dim strSQL As String = "Select * from SALES.CUSTOMER where Customer_ID = " & Session("CurCustomerUpdate")
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        ''Check and load results
        If tblResults.Rows.Count > 0 Then
            txtCustomerID.Text = tblResults.Rows(0)("Customer_ID").ToString
            txtFirstName.Text = tblResults.Rows(0)("First_Name").ToString
            txtLastName.Text = tblResults.Rows(0)("Last_Name").ToString
            txtPhone.Text = tblResults.Rows(0)("Phone").ToString
            txtEmail.Text = tblResults.Rows(0)("Email").ToString
            txtAddress1.Text = tblResults.Rows(0)("Address1").ToString
            txtAddress2.Text = tblResults.Rows(0)("Address2").ToString
            txtCity.Text = tblResults.Rows(0)("City").ToString
            ddlState.SelectedValue = tblResults.Rows(0)("State_ID")
            txtZip.Text = tblResults.Rows(0)("Zip").ToString

            btnCustomer_Action.Text = "Update"
        Else
            litMessage.Text = "<span class=""red"">Customer Not found</span>"
        End If
    End Sub

    Private Sub hidepanels()
        pnlCustomer_Action.Visible = False
        pnlCustomer_Listing.Visible = False
        pnlDelete_Verify.Visible = False
    End Sub


    Protected Sub Page_Changed(sender As Object, e As EventArgs)
        Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        GetCustomersPageWise(pageIndex)
    End Sub

    Private Sub PopulatePager(recordCount As Integer, currentPage As Integer)
        Dim dblPageCount As Double = CDbl(CDec(recordCount) / Convert.ToDecimal(PageSize))
        Dim pageCount As Integer = CInt(Math.Ceiling(dblPageCount))
        Dim pages As New List(Of ListItem)()
        If pageCount > 0 Then
            For i As Integer = 1 To pageCount
                pages.Add(New ListItem(i.ToString(), i.ToString(), i <> currentPage))
            Next
        End If
        rptPager.DataSource = pages
        rptPager.DataBind()
    End Sub

    Private Sub GetCustomersPageWise(pageIndex As Integer)

        Dim strSQL As String = "Exec GetCustomerListing_Paged @pageIndex = " & pageIndex & ", @PageSize =" & PageSize
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        rptCustomers.DataSource = tblResults
        rptCustomers.DataBind()

        Me.PopulatePager(g_IO_Execute_SQL("Select count(*) From Sales.Customer", False).Rows(0)(0), pageIndex)

    End Sub

    Protected Sub btnCustomer_Action_Click(sender As Object, e As EventArgs) Handles btnCustomer_Action.Click
        If btnCustomer_Action.Text.ToUpper = "UPDATE" Then
            ''Update Existing
            Dim strSQL As String = "EXECUTE [dbo].[udp_CustomerUpdate] 
                           @FN = '" & txtFirstName.Text.Replace("'", "''") & "'
                          ,@LN = '" & txtLastName.Text.Replace("'", "''") & "'
                          ,@PH = '" & txtPhone.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "") & "'
                          ,@EM = '" & txtEmail.Text.Replace("'", "''") & "'
                          ,@AD1 = '" & txtAddress1.Text.Replace("'", "''") & "'
                          ,@AD2 = '" & txtAddress2.Text.Replace("'", "''") & "'
                          ,@CI = '" & txtCity.Text.Replace("'", "''") & "'
                          ,@ST = " & ddlState.SelectedValue & "
                          ,@ZI = '" & txtZip.Text.Replace("'", "''") & "'
                          ,@CID = '" & txtCustomerID.Text & "'"
            g_IO_Execute_SQL(strSQL, False)

            Response.Redirect("CustomerManagement.aspx")
        ElseIf btnCustomer_Action.Text.ToUpper = "ADD ANYWAY" Then
            CreateNewCustomer()
            Response.Redirect("CustomerManagement.aspx")
        Else
            ''Check if customer Exists
            Dim strSQL As String = "Select * from Sales.Customer where phone = '" & txtPhone.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "") & "'
                           and email = '" & txtEmail.Text.Replace("'", "''") & "'"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

            If tblResults.Rows.Count > 0 Then
                litMessage.Text = "<span>A customer already exists with this phone and email combination. <a href=""CustomerManagement.aspx?ID=" & tblResults.Rows(0)("Customer_ID") & """>View</a></span>"
                btnCustomer_Action.Text = "Add Anyway"
            Else
                CreateNewCustomer()
                Response.Redirect("CustomerManagement.aspx")
            End If
        End If


    End Sub

    Private Sub CreateNewCustomer()
        ''New Customer
        Dim strSQL As String = "EXECUTE [dbo].[udp_CustomerInsert] 
                           @FN = '" & txtFirstName.Text.Replace("'", "''") & "'
                          ,@LN = '" & txtLastName.Text.Replace("'", "''") & "'
                          ,@PH = '" & txtPhone.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "") & "'
                          ,@EM = '" & txtEmail.Text.Replace("'", "''") & "'
                          ,@AD1 = '" & txtAddress1.Text.Replace("'", "''") & "'
                          ,@AD2 = '" & txtAddress2.Text.Replace("'", "''") & "'
                          ,@CI = '" & txtCity.Text.Replace("'", "''") & "'
                          ,@ST = " & ddlState.SelectedValue & "
                          ,@ZI = '" & txtZip.Text.Replace("'", "''") & "'"
        g_IO_Execute_SQL(strSQL, False)
    End Sub

    Protected Sub btnCustomer_Cancel_Click(sender As Object, e As EventArgs) Handles btnCustomer_Cancel.Click
        Response.Redirect("CustomerManagement.aspx")
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        hidepanels()
        clearForm()
        loadDropDown_States()
        btnCustomer_Action.Text = "Save"
        pnlCustomer_Action.Visible = True
    End Sub

    Protected Sub btnCustomer_Delete_Click(sender As Object, e As EventArgs) Handles btnCustomer_Delete.Click
        hidepanels()
        pnlDelete_Verify.Visible = True
    End Sub

    Protected Sub btnDelete_Cancel_Click(sender As Object, e As EventArgs) Handles btnDelete_Cancel.Click
        hidepanels()
        pnlCustomer_Action.Visible = True
    End Sub

    Protected Sub btnDelete_Yes_Click(sender As Object, e As EventArgs) Handles btnDelete_Yes.Click
        hidepanels()
        Dim strSQL As String = "Update Sales.Customer set Enabled = 0 where Customer_ID = " & txtCustomerID.Text
        g_IO_Execute_SQL(strSQL, False)

        Response.Redirect("CustomerManagement.aspx")
    End Sub
End Class