Public Class InvProducts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            hidePanels()

        Else

        End If
    End Sub

    Private Sub disableForm()
        txtName.Enabled = False
        ddlCategory.Enabled = False
        ddlBrand.Enabled = False
        txtDescription.Enabled = False
        ddlUOM.Enabled = False
        txtOnHandQty.Enabled = False
        txtPrice.Enabled = False
        txtMinOnHandQty.Enabled = False
        txtReorderQty.Enabled = False
        btnSaveNewRawMaterial.Text = "Edit"
        pnlRawMaterialProperties.Visible = True
        btnCancelNewRawMaterial.Visible = False
    End Sub

    Private Sub enableForm()
        txtName.Enabled = True
        ddlCategory.Enabled = True
        ddlBrand.Enabled = True
        txtDescription.Enabled = True
        ddlUOM.Enabled = True
        txtOnHandQty.Enabled = True
        txtPrice.Enabled = True
        txtMinOnHandQty.Enabled = True
        txtReorderQty.Enabled = True
        btnSaveNewRawMaterial.Text = "Update"
        pnlRawMaterialProperties.Visible = False
        btnCancelNewRawMaterial.Visible = True
    End Sub

    Private Sub resetAllSubForms()
        ''Properties Add
        txtAddPropertyDetails.Text = ""
        ddlPropertiesAdd.SelectedValue = -1
        btnSaveAddProperty.Text = "Save"

        ''New Category
        txtNewCategory.Text = ""

        ''New Brand
        txtNewBrand.Text = ""

        ''New YOM
        txtNewUOM.Text = ""
    End Sub

    Private Sub hidePanels()

        pnlAddPropertyToProduct.Visible = False
        pnlAddMultiplePropertyToProduct.Visible = False

        pnlNewBrand.Visible = False
        pnlNewCategory.Visible = False
        pnlNewProperty.Visible = False
        pnlNewUOM.Visible = False

        pnlRawMaterialListing.Visible = False
        pnlRawMaterialProperties.Visible = False
        pnlProductsUpdateDetails.Visible = False

    End Sub

    Private Sub loadDropDowns()
        loadBrands()
        loadUOM()
        loadCategories()
        loadProperties()
    End Sub

    Private Sub loadRawMaterialProperties()
        Dim strSQL As String = "Select * from [INVENTORY].[VW_INVENTORY_PROPERTY] WHERE PRODUCT_ID = " & Request.QueryString("id") & " And IS_PRODUCT = 0 ORDER BY NAME"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            rptrProperties.DataSource = tblResults
            rptrProperties.DataBind()
        End If
        litNewPropertyImage.Text = "<a href=""InvRawMaterials.aspx?id=" & Request.QueryString("ID") & "&action=AddProperty""><img src=""images/round_add_red.png"" style=""height: 16px; width: 16px;"" /></a>"
    End Sub

    Private Sub loadBrands()
        Dim strSQL As String = "SELECT brand_id, [brand] FROM [inventory].brand ORDER BY [brand]"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--Brand--", "Brand", "Brand_id")
        ddlBrand.DataValueField = "brand_id"
        ddlBrand.DataTextField = "Brand"
        ddlBrand.DataSource = tblResults
        ddlBrand.DataBind()
    End Sub

    Private Sub loadProperties()
        Dim strSQL As String = "SELECT PROPERTY_ID, [PROPERTY] FROM [inventory].[PROPERTY] ORDER BY [PROPERTY]"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--Property--", "Property", "Property_id")
        ddlPropertiesAdd.DataValueField = "Property_id"
        ddlPropertiesAdd.DataTextField = "Property"
        ddlPropertiesAdd.DataSource = tblResults
        ddlPropertiesAdd.DataBind()
    End Sub

    Private Sub loadMultipleProperties()
        Dim strSQL As String = "SELECT PROPERTY_ID, [PROPERTY] FROM [inventory].[PROPERTY] ORDER BY [PROPERTY]"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--Property--", "Property", "Property_id")

        ddlPropertiesAdd1.DataValueField = "Property_id"
        ddlPropertiesAdd1.DataTextField = "Property"
        ddlPropertiesAdd1.DataSource = tblResults
        ddlPropertiesAdd1.DataBind()

        ddlPropertiesAdd2.DataValueField = "Property_id"
        ddlPropertiesAdd2.DataTextField = "Property"
        ddlPropertiesAdd2.DataSource = tblResults
        ddlPropertiesAdd2.DataBind()

        ddlPropertiesAdd3.DataValueField = "Property_id"
        ddlPropertiesAdd3.DataTextField = "Property"
        ddlPropertiesAdd3.DataSource = tblResults
        ddlPropertiesAdd3.DataBind()

        ddlPropertiesAdd4.DataValueField = "Property_id"
        ddlPropertiesAdd4.DataTextField = "Property"
        ddlPropertiesAdd4.DataSource = tblResults
        ddlPropertiesAdd4.DataBind()

        ddlPropertiesAdd5.DataValueField = "Property_id"
        ddlPropertiesAdd5.DataTextField = "Property"
        ddlPropertiesAdd5.DataSource = tblResults
        ddlPropertiesAdd5.DataBind()

        ddlPropertiesAdd6.DataValueField = "Property_id"
        ddlPropertiesAdd6.DataTextField = "Property"
        ddlPropertiesAdd6.DataSource = tblResults
        ddlPropertiesAdd6.DataBind()

        ddlPropertiesAdd7.DataValueField = "Property_id"
        ddlPropertiesAdd7.DataTextField = "Property"
        ddlPropertiesAdd7.DataSource = tblResults
        ddlPropertiesAdd7.DataBind()

        ddlPropertiesAdd8.DataValueField = "Property_id"
        ddlPropertiesAdd8.DataTextField = "Property"
        ddlPropertiesAdd8.DataSource = tblResults
        ddlPropertiesAdd8.DataBind()

        ddlPropertiesAdd9.DataValueField = "Property_id"
        ddlPropertiesAdd9.DataTextField = "Property"
        ddlPropertiesAdd9.DataSource = tblResults
        ddlPropertiesAdd9.DataBind()

        ddlPropertiesAdd10.DataValueField = "Property_id"
        ddlPropertiesAdd10.DataTextField = "Property"
        ddlPropertiesAdd10.DataSource = tblResults
        ddlPropertiesAdd10.DataBind()

    End Sub

    Private Sub loadUOM()
        Dim strSQL As String = "SELECT UOM_ID, [UOM] FROM [inventory].UOM ORDER BY [UOM]"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--UOM--", "UOM", "UOM_id")
        ddlUOM.DataValueField = "UOM_id"
        ddlUOM.DataTextField = "UOM"
        ddlUOM.DataSource = tblResults
        ddlUOM.DataBind()
    End Sub

    Private Sub loadCategories()
        Dim strSQL As String = "SELECT CATEGORY_ID, [CATEGORY_NAME] FROM [inventory].CATEGORY ORDER BY [CATEGORY_NAME]"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        createDefaultRow(tblResults, "--CATEGORY--", "CATEGORY_NAME", "CATEGORY_ID")
        ddlCategory.DataValueField = "CATEGORY_ID"
        ddlCategory.DataTextField = "CATEGORY_NAME"
        ddlCategory.DataSource = tblResults
        ddlCategory.DataBind()
    End Sub

    Protected Sub btnCancelNewRawMaterial_Click(sender As Object, e As EventArgs) Handles btnCancelNewRawMaterial.Click
        Response.Redirect("InvProducts.aspx?action=update&id=" & Request.QueryString("ID"))
    End Sub

    Protected Sub imgNewCategory_Click(sender As Object, e As ImageClickEventArgs) Handles imgNewCategory.Click
        hidePanels()
        pnlNewCategory.Visible = True
    End Sub

    Protected Sub imgNewBrand_Click(sender As Object, e As ImageClickEventArgs) Handles imgNewBrand.Click
        hidePanels()
        pnlNewBrand.Visible = True
    End Sub

    Protected Sub imgNewUOM_Click(sender As Object, e As ImageClickEventArgs) Handles imgNewUOM.Click
        hidePanels()
        pnlNewUOM.Visible = True
    End Sub

    Protected Sub btnCancelNewCategory_Click(sender As Object, e As EventArgs) Handles btnCancelNewCategory.Click
        hidePanels()
        txtNewCategory.Text = ""
        pnlProductsUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewBrand_Click(sender As Object, e As EventArgs) Handles btnCancelNewBrand.Click
        hidePanels()
        txtNewBrand.Text = ""
        pnlProductsUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewUOM_Click(sender As Object, e As EventArgs) Handles btnCancelNewUOM.Click
        hidePanels()
        txtNewUOM.Text = ""
        pnlProductsUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewProperty_Click(sender As Object, e As EventArgs) Handles btnCancelNewProperty.Click
        hidePanels()
        txtNewProperty.Text = ""
        pnlProductsUpdateDetails.Visible = True
    End Sub

    Protected Sub btnSaveNewProperty_Click(sender As Object, e As EventArgs) Handles btnSaveNewProperty.Click
        If txtNewProperty.Text <> "" Then
            Try
                Dim StrSQL As String = "Insert into [INVENTORY].[PROPERTY] (PROPERTY) VALUES ('" & txtNewProperty.Text.Replace("'", "''") & "')"
                Dim returnInt As Integer = g_IO_Execute_SQL(StrSQL, False).Rows(0)(0)
                loadProperties()
                ddlPropertiesAdd.SelectedValue = returnInt
                hidePanels()
                resetAllSubForms()
                pnlProductsUpdateDetails.Visible = True
            Catch ex As Exception
                txtNewProperty.BorderColor = System.Drawing.Color.Red
                litMessage.Text = "<span class=""fontRed"">Invalid Property Name Provided.</span><br /><br />"
            End Try
        End If

    End Sub

    Protected Sub btnAddNewCategory_Click(sender As Object, e As EventArgs) Handles btnAddNewCategory.Click
        If txtNewCategory.Text <> "" Then
            Try
                Dim StrSQL As String = "Insert into [INVENTORY].[CATEGORY] (CATEGORY_NAME) VALUES ('" & txtNewCategory.Text.Replace("'", "''") & "')"
                Dim returnInt As Integer = g_IO_Execute_SQL(StrSQL, False).Rows(0)(0)
                loadCategories()
                ddlCategory.SelectedValue = returnInt
                hidePanels()
                resetAllSubForms()
                pnlProductsUpdateDetails.Visible = True
            Catch ex As Exception
                txtNewCategory.BorderColor = System.Drawing.Color.Red
                litMessage.Text = "<span class=""fontRed"">Invalid Category Name Provided.</span><br /><br />"
            End Try
        End If
    End Sub

    Protected Sub btnSaveNewBrand_Click(sender As Object, e As EventArgs) Handles btnSaveNewBrand.Click
        If txtNewBrand.Text <> "" Then
            Try
                Dim StrSQL As String = "Insert into [INVENTORY].[BRAND] (BRAND) VALUES ('" & txtNewBrand.Text.Replace("'", "''") & "')"
                Dim returnInt As Integer = g_IO_Execute_SQL(StrSQL, False).Rows(0)(0)
                loadBrands()
                ddlBrand.SelectedValue = returnInt
                hidePanels()
                resetAllSubForms()
                pnlProductsUpdateDetails.Visible = True
            Catch ex As Exception
                txtNewBrand.BorderColor = System.Drawing.Color.Red
                litMessage.Text = "<span class=""fontRed"">Invalid Brand Name Provided.</span><br /><br />"
            End Try
        End If
    End Sub

    Protected Sub btnSaveNewUOM_Click(sender As Object, e As EventArgs) Handles btnSaveNewUOM.Click
        If txtNewUOM.Text <> "" Then
            Try
                Dim StrSQL As String = "Insert into [INVENTORY].[UOM] (UOM) VALUES ('" & txtNewUOM.Text.Replace("'", "''") & "')"
                Dim returnInt As Integer = g_IO_Execute_SQL(StrSQL, False).Rows(0)(0)
                loadUOM()
                ddlUOM.SelectedValue = returnInt
                hidePanels()
                resetAllSubForms()
                pnlProductsUpdateDetails.Visible = True
            Catch ex As Exception
                txtNewUOM.BorderColor = System.Drawing.Color.Red
                litMessage.Text = "<span class=""fontRed"">Invalid Unit Of Measure Provided.</span><br /><br />"
            End Try
        End If
    End Sub

    Protected Sub btnSaveAddProperty_Click(sender As Object, e As EventArgs) Handles btnSaveAddProperty.Click
        If ddlPropertiesAdd.SelectedValue <> -1 And txtAddPropertyDetails.Text <> "" Then
            If btnSaveNewProperty.Text = "Update" Then
                Try
                    Dim strsQL As String = "UPDATE [INVENTORY].[INVENTORY_PROPERTY] SET " &
                        "PRODUCT_ID = " & Request.QueryString("ID") & ", " &
                        "PROPERTY_ID = " & ddlPropertiesAdd.SelectedValue & ", " &
                        "VALUE = '" & txtAddPropertyDetails.Text.Replace("'", "''") & "' " &
                        "WHERE INVENTORY_PROPERTY_ID = " & Request.QueryString("IPID")
                    g_IO_Execute_SQL(strsQL, False)
                Catch ex As Exception
                    litMessage.Text = "<span class=""fontRed"">An error has occurred.</span><br /><br />"
                End Try
            Else
                Try
                    Dim strsQL As String = "INSERT into [INVENTORY].[INVENTORY_PROPERTY] (PRODUCT_ID, PROPERTY_ID, VALUE, IS_PRODUCT) VALUES " &
                    "(" & Request.QueryString("ID") & "," & ddlPropertiesAdd.SelectedValue & ",'" & txtAddPropertyDetails.Text.Replace("'", "''") & "',1)"
                    g_IO_Execute_SQL(strsQL, False)
                Catch ex As Exception
                    litMessage.Text = "<span class=""fontRed"">An error has occurred.</span><br /><br />"
                End Try
            End If
            Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
        End If
    End Sub

    Private Sub addPropertyToProduct(ByVal property_id As Integer, ByVal value As String)
        Dim strsQL As String = "INSERT into [INVENTORY].[INVENTORY_PROPERTY] (PRODUCT_ID, PROPERTY_ID, VALUE, IS_PRODUCT) VALUES " &
                    "(" & Request.QueryString("ID") & "," & property_id & ",'" & value.Replace("'", "''") & "',1)"
        g_IO_Execute_SQL(strsQL, False)
    End Sub
End Class