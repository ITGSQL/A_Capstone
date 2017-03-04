﻿Public Class Orders
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

    Private Sub LoadOrderDetails(ByVal Header_ID As Integer)
        ''Load the current Temp Order
        strSQL = "Select * from SALES.VW_DETAILS WHERE HEADER_ID = " & Header_ID
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            loadOrderLines(tblResults)
            btnOrderDetails_Save.Visible = True
            btnOrderDetails_Clear.Visible = True
        Else
            ''Do nothing
            litCurOrderListing.Text = "No items have been added at this time."
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

        litSubTotal.Text = Format(SubTotal, "0.00")
        litSalesTax.Text = Format(SalesTax, "0.00")
        litGrandTotal.Text = Format(GrandTotal, "0.00")

        ''Add the Final Totals
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>SubTotal  </td><td>$" & Format(SubTotal, "0.00") & "</td></tr>"
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>Sales Tax  </td><td>$" & Format(SalesTax, "0.00") & "</td></tr>"
        litCurOrderListing.Text &= "<tr><td colspan=""4"">&nbsp;</td><td>Grand Total  </td><td>$" & Format(GrandTotal, "0.00") & "</td></tr>"

        litCurOrderListing.Text &= "<tbody></table>"
    End Sub

    Private Sub createOrder(ByVal custID As Integer)
        ''Check to see if an existing order is there
        strSQL = "Select header_id from Sales.Header where Customers_ID = " & custID & " and is_Paid = 0"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            litOrderID.Text = tblResults.Rows(0)(0)
            LoadOrderDetails(tblResults.Rows(0)(0))
        Else
            strSQL = "Insert into Sales.Header (REGISTER_ID, IS_VENDOR, CUSTOMERS_ID, SALESPERSON_ID) VALUES (1,0," & custID & "," & Session("USER_ID") & ")"
            Dim orderInt As Integer = g_IO_Execute_SQL(strSQL, False)(0)(0)
            litOrderID.Text = orderInt
        End If


    End Sub



    Private Sub hidePanels()
        pnlPaymentTypes.Visible = False
        pnlCustomer.Visible = False
        pnlCustomerSearch.Visible = False
        pnlOrderDetails.Visible = False
        pnlPayment_Cash.Visible = False
        pnlPayment_Check.Visible = False
        pnlPayment_Credit.Visible = False
        ''btnOrderDetails_Save.Visible = False
        ''btnOrderDetails_Clear.Visible = False

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
            lblCustomerName.Text = tblResults(0)("First_Name").ToString & " " & tblResults(0)("Last_Name").ToString
            lblCustomerAddress.Text = tblResults(0)("Address1").ToString
            If tblResults(0)("Address2").ToString <> "" Then
                lblCustomerAddress.Text &= ", " & tblResults(0)("Address2").ToString
            End If
            lblCitySateZip.Text = tblResults(0)("CITY").ToString & ", " & tblResults(0)("state_name").ToString & " " & tblResults(0)("ZIP").ToString
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
                        strSQL = "Insert into SALES.DETAILS (header_id, product_id, qty, price) VALUES (" & litOrderID.Text & "," & tblResults.Rows(0)("PRODUCT_ID") & "," & txtQty.Text & "," & tblResults.Rows(0)("PRICE") & ")"
                        g_IO_Execute_SQL(strSQL, False)
                        ''decrement the original order
                        decrementProductInventory(tblResults.Rows(0)("PRODUCT_ID"), txtQty.Text)
                        strSQL = "Select * from SALES.VW_DETAILS where header_id = " & litOrderID.Text & " order by details_id"
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

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnOrderDetails_Clear.Click
        strSQL = "Select * from Sales.Details where header_id = " & litOrderID.Text
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        ''Add the inventory back into the product inventory
        If tblResults.Rows.Count > 0 Then
            For Each row In tblResults.Rows
                incrementProductInventory(row("Product_id"), row("Qty"))
                strSQL = "Delete from Sales.Details where Details_id = " & row("Details_ID")
                g_IO_Execute_SQL(strSQL, False)
            Next
        End If

        ''Delete the header
        strSQL = "DELETE from SALES.DETAILS where header_id = " & litOrderID.Text
        g_IO_Execute_SQL(strSQL, False)

        Response.Redirect("Orders.aspx?cust=" & Request.QueryString("CUST"))
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnOrderDetails_Save.Click
        hidePanels()
        pnlPaymentTypes.Visible = True
    End Sub

    Protected Sub btnPayCancel_Click(sender As Object, e As EventArgs) Handles btnPaymentTypes_Cancel.Click, btnPayCancel_Cash.Click, btnPayCancel_Check.Click, btnPayCancel_Credit.Click
        clearPaymentForms()
        hidePanels()
        pnlOrderDetails.Visible = True
    End Sub

    Protected Sub btnPaymentType_Cash_Click(sender As Object, e As EventArgs) Handles btnPaymentType_Cash.Click
        hidePanels()
        checkExistingPayments()
        litPayment_Cash_TotalDue.Text = "$" & litGrandTotal.Text
        pnlPayment_Cash.Visible = True
    End Sub

    Protected Sub btnPaymentType_Credit_Click(sender As Object, e As EventArgs) Handles btnPaymentType_Credit.Click
        hidePanels()
        pnlPayment_Credit.Visible = True
    End Sub

    Protected Sub btnPaymentType_Check_Click(sender As Object, e As EventArgs) Handles btnPaymentType_Check.Click
        hidePanels()
        pnlPayment_Check.Visible = True
    End Sub

    Private Sub clearPaymentForms()
        txtPaymentCheck_CheckNumber.Text = ""
        txtPayment_CashReceived.Text = ""
    End Sub

    Private Sub showPanel_OrderDetails()
        pnlOrderDetails.Visible = True
        btnOrderDetails_Clear.Visible = True
        btnOrderDetails_Save.Visible = True
    End Sub

    Protected Sub btnPayContinue_Cash_Click(sender As Object, e As EventArgs) Handles btnPayContinue_Cash.Click
        Try
            Dim blnIsPaid As Boolean = False
            Dim blnChangeRequired As Boolean = False

            Dim decCashReceived As Decimal = txtPayment_CashReceived.Text
            Dim decGrandTotal As Decimal = litGrandTotal.Text

            If (decCashReceived + litPayment_Cash_TotalReceived.Text) <= decGrandTotal Then
                If decCashReceived > 0 Then
                    strSQL = "Insert into SALES.PAYMENTS (HEADER_ID, PAYMENT_TYPE_ID, AMOUNT) VALUES (" & litOrderID.Text & ",1," & decCashReceived & ")"
                    g_IO_Execute_SQL(strSQL, False)
                End If

                If decCashReceived = decGrandTotal Then
                    ''Full amount paid
                    blnIsPaid = True

                    If decCashReceived > decGrandTotal Then
                        blnChangeRequired = True
                    End If
                Else
                    txtPayment_CashReceived.Text = ""
                End If
            Else

                ''Overpaid
                blnChangeRequired = True
                blnIsPaid = True

                If decCashReceived > 0 Then
                    strSQL = "Insert into SALES.PAYMENTS (HEADER_ID, PAYMENT_TYPE_ID, AMOUNT) VALUES (" & litOrderID.Text & ",1," & decCashReceived & ")"
                    g_IO_Execute_SQL(strSQL, False)
                End If

            End If

            checkExistingPayments()

            If blnChangeRequired Then
                strSQL = "Insert into SALES.PAYMENTS (HEADER_ID, PAYMENT_TYPE_ID, AMOUNT) VALUES (" & litOrderID.Text & ",4,-" & (litPayment_Cash_TotalReceived.Text) - decGrandTotal & ")"
                g_IO_Execute_SQL(strSQL, False)
            End If

            If blnIsPaid Then
                strSQL = "update Sales.Header set is_paid = 1, DATE_PAID = getdate(), subtotal = " & litSubTotal.Text & ",sales_tax_total = " & litSalesTax.Text & ",Grand_Total = " & litGrandTotal.Text & " where header_id = " & litOrderID.Text
                g_IO_Execute_SQL(strSQL, False)

                litPayment_Cash_Message.Text = "<span>Transaction Complete.</span><br /><span style=""display: block; margin-bottom: 100px;"">Change: $" & ((litPayment_Cash_TotalReceived.Text) - decGrandTotal).ToString("0.00") & "</span>"

                btnPayCancel_Cash.Visible = False
                txtPayment_CashReceived.Text = ""
                txtPayment_CashReceived.Enabled = False

            End If

            checkExistingPayments()
        Catch ex As Exception
            litErrorMessage.Text = "Please enter a valid cash amount."
        End Try
    End Sub

    Private Sub checkExistingPayments()
        Dim totalReceived As Decimal = 0D
        strSQL = "Select Amount From Sales.PAYMENTS where header_id = " & litOrderID.Text
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            For Each row In tblResults.Rows
                totalReceived += row("Amount")
            Next
        End If
        litPayment_Cash_TotalReceived.Text = "$" & totalReceived.ToString("0.00")
    End Sub
End Class