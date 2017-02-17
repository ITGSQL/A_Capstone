Public Class Orders
    Inherits System.Web.UI.Page
    Dim strSQL As String
    Dim curCust As Integer
    Dim curOrder As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            hidePanels()

            If IsNothing(Request.QueryString("Cust")) Then
                ''No customer selected
                pnlCustomerSearch.Visible = True
            Else
                curCust = Request.QueryString("Cust")

                ''Load the customer details
                loadCustomerDetails()

                ''Current Order?
                If IsNothing(Request.QueryString("Order")) Then
                    ''createOrder()
                Else
                    curOrder = Request.QueryString("Order")
                End If

                pnlOrderDetails.Visible = True
            End If
        End If

    End Sub

    Private Sub LoadOrderDetails(ByVal Header_ID As Integer, ByVal orderType As String)
        If orderType.ToUpper = "TEMP" Then
            ''Load the current Temp Order
            strSQL = "Select * from SALES.VW_DETAILS_TEMP WHERE HEADER_ID = " & Header_ID
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
            If tblResults.Rows.Count > 0 Then
                loadOrderLines(tblResults)
            Else
                ''Do nothing
                litCurOrderListing.Text = "No items have been added at this time."
            End If
        Else
            ''Load a past order
        End If
    End Sub

    Private Sub loadOrderLines(ByVal tblResults As DataTable)
        Dim SubTotal As Decimal = 0D
        Dim rowTotal As Decimal = 0D
        litCurOrderListing.Text = "<table class=""table table-responsive table-bordered""><thead><tr><th>&nbsp;</th><th>Item ID</th><th>Description</th><th>Price</th><th>Qty</th><th>Total</th></tr></thead>"
        litCurOrderListing.Text &= "<tbody>"

        For Each row In tblResults.Rows
            rowTotal = row("Price") * row("QTY")
            SubTotal += rowTotal
            litCurOrderListing.Text &= "<tr><td><img style=""cursor: pointer;"" src=""images/trash.png"" onclick=""redirect('ajaxfunctions.aspx?action=deletefromtemporder&id=" & row("details_id") & "&cust=" & litCustID.Text & "&prod=" & row("product_id") & "&qty=" & row("qty") & "')"" /></td><td>" & row("PRODUCT_CODE") & "</td><td>" & row("Description") & "<br />" & row("PRODUCT_PROPERTIES") & "</td><td>$" & row("Price") & "</td><td>" & row("Qty") & "</td><td>$" & Format(rowTotal, "0.00") & "</td></tr>"

            rowTotal = 0D
        Next

        Dim SalesTax As Decimal = 0.08 * SubTotal
        Dim GrandTotal As Decimal = SubTotal + SalesTax

        ''Add the Final Totals
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>SubTotal  </td><td>$" & Format(SubTotal, "0.00") & "</td></tr>"
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>Sales Tax  </td><td>$" & Format(SalesTax, "0.00") & "</td></tr>"
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>Grand Total  </td><td>$" & Format(GrandTotal, "0.00") & "</td></tr>"

        litCurOrderListing.Text &= "<tbody></table>"
    End Sub

    Private Sub createOrder(ByVal custID As Integer)
        ''Check to see if an existing order is there
        strSQL = "Select header_id from SALES.HEADER_TEMP where Customers_ID = " & custID
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            litOrderID.Text = tblResults.Rows(0)(0)
            LoadOrderDetails(tblResults.Rows(0)(0), "TEMP")
        Else
            strSQL = "Insert into SALES.HEADER_TEMP (REGISTER_ID, IS_VENDOR, CUSTOMERS_ID, SALESPERSON_ID) VALUES (1,0," & custID & "," & Session("USER_ID") & ")"
            Dim orderInt As Integer = g_IO_Execute_SQL(strSQL, False)(0)(0)
            litOrderID.Text = orderInt
        End If


    End Sub

    Private Sub hidePanels()
        pnlAddItem.Visible = False
        pnlCustomer.Visible = False
        pnlCustomerSearch.Visible = False
        pnlOrderDetails.Visible = False
    End Sub

    Private Sub showOrderDetailsPanel()
        hidePanels()
        pnlOrderDetails.Visible = True
    End Sub

    Private Sub loadCustomerDetails()
        loadCustomerDetails(Request.QueryString("cust"))
    End Sub

    Private Sub loadCustomerDetails(ByVal customer_id As Integer)
        litCustID.Text = customer_id
        strSQL = "Select C.*, S.state_name from Sales.Customer C  inner join dbo.state S on S.state_id = C.state_id where customer_id = " & customer_id & ""
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            lblCustomerName.Text = tblResults(0)("First_Name") & " " & tblResults(0)("Last_Name")
            lblCustomerAddress.Text = tblResults(0)("Address1")
            If tblResults(0)("Last_Name").ToString <> "" Then
                lblCustomerAddress.Text &= ", " & tblResults(0)("Address2")
            End If
            lblCitySateZip.Text = tblResults(0)("CITY") & ", " & tblResults(0)("state_name") & " " & tblResults(0)("ZIP")
            showOrderDetailsPanel()
            createOrder(tblResults(0)("CUSTOMER_ID"))
        Else
            Response.Redirect("Orders.aspx")
        End If
    End Sub

    Protected Sub btnCustomerSearch_Click(sender As Object, e As EventArgs) Handles btnCustomerSearch.Click
        If ddlSearchOptions.SelectedValue = "Name" Then
            strSQL = "Select * from  Sales.Customer where first_name like '%" & txtSearch.Text.Replace("'", "''") & "%' or last_name like '%" & txtSearch.Text.Replace("'", "''") & "%'"
        Else
            strSQL = "Select * from  Sales.Customer where replace(" & ddlSearchOptions.SelectedValue & ", '-','') = '" & txtSearch.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "") & "'"
        End If

        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            If tblResults.Rows.Count = 1 Then
                loadCustomerDetails(tblResults(0)("customer_id"))
            Else
                litSearchResults_Cust.Text = "<table class=""table table-responsive table-bordered"">"
                litSearchResults_Cust.Text &= "<thead><tr><th>Name</th><th>Phone</th><th>Email</th><th>&nbsp;</th></tr></thead><tbody>"
                For Each row In tblResults.Rows
                    litSearchResults_Cust.Text &= "<tr><td>" & row("First_Name") & " " & row("Last_Name") & "</td><td>" & row("Phone") & "</td><td>" & row("Email") & "</td><td><a class=""buttonLink"" href=""Orders.aspx?Type=New&Cust=" & row("Customer_ID") & """>Select</a></td></tr>"

                Next
                litSearchResults_Cust.Text &= "</tbody></table>"
            End If
        Else
            litSearchResults_Cust.Text = "No User Found."
        End If


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
            "@Email = '" & txtEmail.Text.Replace("'", "''") & "', @Phone = '" & txtPhone.Text.Replace("'", "''").Replace("-", "").Replace("(", "").Replace(")", "") & "'"
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

    Private Function validateAddProductForm()
        If IsNothing(txtProductCode_Add.Text) Then
            Return False
        End If
        Try
            Dim test As Integer = txtQty.Text
            Return True
        Catch ex As Exception
            txtQty.Text = 1
            litProductEntryError.Text = "<span class=""error"">Invalid Qty entered</span>"
            Return False
        End Try
    End Function

    Private Sub clearAddProductForm()
        txtProductCode_Add.Text = ""
        txtQty.Text = 1
    End Sub

    'Private Sub decrementProductInventory(ByVal productID As Integer, ByVal qty As Integer)
    '    strSQL = "Update inventory.vw_Products set ONHAND_QTY = (ONHAND_QTY - " & qty & ") where product_id = " & productID
    '    g_IO_Execute_SQL(strSQL, False)
    'End Sub

    Protected Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click

        If validateAddProductForm() Then
            ''Is this a valid Product Code?
            strSQL = "Select * from INVENTORY.VW_PRODUCTS where PRODUCT_CODE = '" & txtProductCode_Add.Text.Replace("'", "''") & "'"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
            If tblResults.Rows.Count > 0 Then
                ''Do we have any on hand?
                If tblResults.Rows(0)("ONHAND_QTY") > 0 Then
                    ''Are there enough? 
                    If tblResults.Rows(0)("ONHAND_QTY") >= txtQty.Text.Replace("'", "''") Then
                        ''DO STUFF HERE!!!
                        strSQL = "Insert into SALES.DETAILS_TEMP (header_id, product_id, qty, price) VALUES (" & litOrderID.Text & "," & tblResults.Rows(0)("PRODUCT_ID") & "," & txtQty.Text & "," & tblResults.Rows(0)("PRICE") & ")"
                        g_IO_Execute_SQL(strSQL, False)
                        ''decrement the original order
                        decrementProductInventory(tblResults.Rows(0)("PRODUCT_ID"), txtQty.Text)
                        strSQL = "Select * from SALES.VW_DETAILS_TEMP where header_id = " & litOrderID.Text & " order by details_id"
                        loadOrderLines(g_IO_Execute_SQL(strSQL, False))
                        txtProductCode_Add.Text = ""
                    Else
                        ''WE HAVE A PROBLEM...
                        ''Is this LSS Produced?
                        If tblResults.Rows(0)("IS_LSS_PRODUCED") = 1 Then
                            litProductEntryError.Text = "<span class=""error"">Not enough inventory available but LSS can make it.</span>"
                        Else
                            litProductEntryError.Text = "<span class=""error"">Not enough inventory available.  We'll have to order it.</span>"
                        End If
                    End If
                Else
                    ''None on hand. Display error
                    litProductEntryError.Text = "<span class=""error"">No inventory available.</span>"
                End If
            Else
                ''No Product exists with that product code
                litProductEntryError.Text = "<span class=""error"">No products with code """ & txtProductCode_Add.Text & """ were found in the system. Please enter or scan the product code again.</span>"
            End If
        End If
















    End Sub
End Class