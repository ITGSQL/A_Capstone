Public Class EmployeeManagement
    Inherits System.Web.UI.Page

    Private PageSize As Integer = 25


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Session("USER_ID")) Then
                Response.Redirect("Login.aspx")
            End If

            hidepanels()
            ''Are we looking at a employee?
            If IsNothing(Request.QueryString("ID")) Then
                GetemployeesPageWise(1)
                pnlEmployee_Listing.Visible = True
            Else
                loadDropDown_Positions()
                loadEmployeeDetails()
                pnlEmployee_Action.Visible = True
            End If
        End If
    End Sub

    Private Sub loadDropDown_Positions()
        Dim strSQL As String = "Select * from EMPLOYEE.POSITION order by POSITION"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        ddlPosition.DataTextField = "POSITION"
        ddlPosition.DataValueField = "Position_ID"
        ddlPosition.DataSource = tblResults
        ddlPosition.DataBind()
    End Sub

    Private Sub clearForm()
        txtEmployeeID.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtHourly.Text = ""
        txtLoginID.Text = ""
        txtPassword.Text = ""
        txtTaxWithholding.Text = ""
        txtVacationDays.Text = ""
    End Sub

    Private Sub loadEmployeeDetails()
        ''Set as session to prevent user error in the querystring
        Session("CurEmployeeUpdate") = Request.QueryString("ID")

        ''Pull Data
        Dim strSQL As String = "Select * from dbo.vw_employee_details where sys_users_ID = " & Session("CurEmployeeUpdate")
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        ''Check and load results
        If tblResults.Rows.Count > 0 Then
            txtEmployeeID.Text = tblResults.Rows(0)("Sys_Users_ID").ToString
            txtFirstName.Text = tblResults.Rows(0)("First_Name").ToString
            txtLastName.Text = tblResults.Rows(0)("Last_Name").ToString
            txtLoginID.Text = tblResults.Rows(0)("Login_ID").ToString
            txtPassword.Text = ""
            txtVacationDays.Text = tblResults.Rows(0)("Vacation_Days").ToString
            txtTaxWithholding.Text = tblResults.Rows(0)("Tax_Witholding").ToString
            ddlPosition.SelectedValue = tblResults.Rows(0)("position_id")
            txtHourly.Text = tblResults.Rows(0)("Hourly").ToString

            btnEmployee_Action.Text = "Update"
        Else
            litMessage.Text = "<span class=""red"">Employee Not found</span>"
        End If
    End Sub

    Private Sub hidepanels()
        pnlEmployee_Action.Visible = False
        pnlemployee_Listing.Visible = False
        pnlDelete_Verify.Visible = False
    End Sub


    Protected Sub Page_Changed(sender As Object, e As EventArgs)
        Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        GetemployeesPageWise(pageIndex)
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

    Private Sub GetemployeesPageWise(pageIndex As Integer)

        Dim strSQL As String = "Exec GetEmployeeListing_Paged @pageIndex = " & pageIndex & ", @PageSize =" & PageSize
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        rptEmployees.DataSource = tblResults
        rptEmployees.DataBind()

        Me.PopulatePager(g_IO_Execute_SQL("Select count(*) From dbo.vw_employee_details", False).Rows(0)(0), pageIndex)

    End Sub

    Protected Sub btnemployee_Action_Click(sender As Object, e As EventArgs) Handles btnEmployee_Action.Click
        If btnEmployee_Action.Text.ToUpper = "UPDATE" Then
            ''Update Existing
            Dim strSQL As String = "EXECUTE [dbo].[udp_employeeUpdate] 
                           @FN = '" & txtFirstName.Text.Replace("'", "''") & "'
                          ,@LN = '" & txtLastName.Text.Replace("'", "''") & "'
                          ,@PW = '" & txtPassword.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "") & "'
                          ,@LI = '" & txtLoginID.Text.Replace("'", "''") & "'
                          ,@HP = " & txtHourly.Text.Replace("'", "''").Replace("$", "").Replace(",", "") & "
                          ,@TW = " & txtTaxWithholding.Text.Replace("'", "''").Replace("$", "").Replace(",", "") & "
                          ,@VD = " & txtVacationDays.Text.Replace("'", "''") & "
                          ,@PI = '" & ddlPosition.SelectedValue & "'
                          ,@UID = '" & txtEmployeeID.Text & "'"
            g_IO_Execute_SQL(strSQL, False)

            Response.Redirect("employeeManagement.aspx")
        ElseIf btnEmployee_Action.Text.ToUpper = "ADD ANYWAY" Then
            CreateNewemployee()
            Response.Redirect("employeeManagement.aspx")
        Else
            ''Check if employee Exists
            Dim strSQL As String = "Select * from dbo.vw_employee_details where first_name = '" & txtFirstName.Text.Replace("'", "''") & "'
                           and last_name = '" & txtLastName.Text.Replace("'", "''") & "' and login_id = '" & txtLoginID.Text.Replace("'", "''") & "'"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

            If tblResults.Rows.Count > 0 Then
                litMessage.Text = "<span>An employee already exists with this information combination. <a href=""employeeManagement.aspx?ID=" & tblResults.Rows(0)("sys_users_id") & """>View</a></span>"
                btnEmployee_Action.Text = "Add Anyway"
            Else
                CreateNewemployee()
                Response.Redirect("employeeManagement.aspx")
            End If
        End If


    End Sub

    Private Sub CreateNewemployee()

        '       [dbo].[udp_EmployeeInsert]
        '-- Add the parameters for the stored procedure here
        '@FN Varchar(255), @LN varchar(255), @PW varchar(255), @LI varchar(255), @PI INT,
        '@VD INT, @TW INT, @HP DECIMAL(6,2)


        ''New employee
        Dim strSQL As String = "EXECUTE [dbo].[udp_EmployeeInsert] 
                           @FN = '" & txtFirstName.Text.Replace("'", "''") & "'
                          ,@LN = '" & txtLastName.Text.Replace("'", "''") & "'
        ,@PW = '" & txtPassword.Text.Replace("'", "''") & "'
        ,@LI = '" & txtLoginID.Text.Replace("'", "''") & "'
        ,@PI = " & ddlPosition.SelectedValue &
        ",@VD = " & txtVacationDays.Text.Replace("'", "''") &
        ",@TW = " & txtTaxWithholding.Text.Replace("'", "''") &
        ",@HP = " & txtHourly.Text.Replace("$", "").Replace("'", "''")
        g_IO_Execute_SQL(strSQL, False)

        ''get the user id that was just created
        strSQL = "Select sys_users_id from dbo.vw_employee_details where login_id = '" & txtLoginID.Text.Trim & "'"
    End Sub

    Protected Sub btnemployee_Cancel_Click(sender As Object, e As EventArgs) Handles btnEmployee_Cancel.Click
        Response.Redirect("employeeManagement.aspx")
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        hidepanels()
        clearForm()
        loadDropDown_Positions()
        btnEmployee_Action.Text = "Save"
        pnlEmployee_Action.Visible = True
    End Sub

    Protected Sub btnemployee_Delete_Click(sender As Object, e As EventArgs) Handles btnEmployee_Delete.Click
        hidepanels()
        pnlDelete_Verify.Visible = True
    End Sub

    Protected Sub btnDelete_Cancel_Click(sender As Object, e As EventArgs) Handles btnDelete_Cancel.Click
        hidepanels()
        pnlEmployee_Action.Visible = True
    End Sub

    Protected Sub btnDelete_Yes_Click(sender As Object, e As EventArgs) Handles btnDelete_Yes.Click
        hidepanels()
        Dim strSQL As String = "Update employee.SYS_USERS set IS_ACTIVE = 0 where SYS_USERS_ID = " & txtEmployeeID.Text
        If g_auditQuery Then
            g_IO_Execute_SQL(strSQL, False)
        End If
        g_IO_Execute_SQL(strSQL, False)

        Response.Redirect("employeeManagement.aspx")
    End Sub
End Class