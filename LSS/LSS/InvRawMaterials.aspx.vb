Public Class InvRawMaterials
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            hidePanels()

            If Not IsNothing(Request.QueryString("action")) Then
                loadDropDowns()
                Dim action As String = Request.QueryString("action").ToString.ToUpper
                If action = "NEWRAWMATERIAL" Then
                    loadDropDowns()
                    pnlRawMaterialUpdateDetails.Visible = True
                    btnSaveNewRawMaterial.Text = "SAVE"
                ElseIf action = "UPDATE" Then
                    loadRawMaterialDetails()
                    loadRawMaterialProperties()
                    pnlRawMaterialUpdateDetails.Visible = True
                    disableForm()
                ElseIf action = "COPY" Then
                    copyItemToNewItem()
                ElseIf action = "ADDPROPERTY" Then
                    loadMultipleProperties()
                    pnlAddMultiplePropertyToRawMaterial.Visible = True
                ElseIf action = "UPDATEPROPERTY" Then
                    loadPropertyDetails()
                    pnlAddPropertyToRawMaterial.Visible = True
                ElseIf action = "DELETEPROPERTY" Then
                    deleteProperty()
                End If
            Else
                loadRawMaterialListing()
                pnlRawMaterialListing.Visible = True
            End If
        End If
    End Sub

    Private Sub disableForm()
        txtName.Enabled = False
        ddlCategory.Enabled = False
        ddlBrand.Enabled = False
        txtDescription.Enabled = False
        ddlUOM.Enabled = False
        txtOnHandQty.Enabled = False
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
        txtMinOnHandQty.Enabled = True
        txtReorderQty.Enabled = True
        btnSaveNewRawMaterial.Text = "Update"
        pnlRawMaterialProperties.Visible = False
        btnCancelNewRawMaterial.Visible = True
    End Sub

    Private Sub copyItemToNewItem()
        Dim strSQL As String = "Select * from [INVENTORY].[vw_Raw_Materials] WHERE raw_material_id = " & Request.QueryString("ID")
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            strSQL = "INSERT INTO [INVENTORY].[RAW_MATERIAL]
                   ([NAME]
                   ,[CATEGORY_ID]
                   ,[BRAND_ID]
                   ,[DESCRIPTION]
                   ,[UOM_ID]
                   ,[ONHAND_QTY]
                   ,[MINIMUM_ONHAND_QTY]
                   ,[REORDER_QTY])
             SELECT [NAME] + ' - COPY'
                   ,[CATEGORY_ID]
                   ,[BRAND_ID]
                   ,[DESCRIPTION]
                   ,[UOM_ID]
                   ,[ONHAND_QTY]
                   ,[MINIMUM_ONHAND_QTY]
                   ,[REORDER_QTY] 
            FROM [INVENTORY].[vw_Raw_Materials] WHERE raw_material_id = " & Request.QueryString("ID")
            g_IO_Execute_SQL(strSQL, False)
            Dim newID As Integer = g_IO_Execute_SQL("select top 1 raw_material_id from inventory.vw_raw_materials order by raw_material_id desc", False)(0)(0)

            strSQL = "INSERT INTO [INVENTORY].[INVENTORY_PROPERTY]
                   ([PRODUCT_ID]
                   ,[PROPERTY_ID]
                   ,[VALUE]
                   ,[IS_PRODUCT])
                    SELECT " & newID & "
                   ,[PROPERTY_ID]
                   ,[VALUE]
                   ,[IS_PRODUCT]
                    FROM [INVENTORY].[INVENTORY_PROPERTY] WHERE PRODUCT_ID =  " & Request.QueryString("ID") &
                    " and is_product = 0"
            g_IO_Execute_SQL(strSQL, False)
            Response.Redirect("InvRawMaterials.aspx?action=update&id=" & newID)
        Else

        End If
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

    Private Sub deleteProperty()
        Dim strSQL As String = "Delete from [INVENTORY].[INVENTORY_PROPERTY] WHERE INVENTORY_PROPERTY_ID = " & Request.QueryString("IPID")
        g_IO_Execute_SQL(strSQL, False)
        Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
    End Sub

    Private Sub loadPropertyDetails()
        Dim strSQL As String = "Select * from [inventory].[VW_INVENTORY_PROPERTY] where INVENTORY_PROPERTY_ID = " & Request.QueryString("IPID")
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            pnlAddPropertyToRawMaterial.Visible = True
            ddlPropertiesAdd.SelectedValue = tblResults.Rows(0)("PROPERTY_ID")
            txtAddPropertyDetails.Text = tblResults.Rows(0)("VALUE")
            btnSaveNewProperty.Text = "Update"
        Else
            Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
        End If
    End Sub

    Private Sub loadRawMaterialDetails()
        Dim strSQL As String = "Select * from INVENTORY.VW_RAW_MATERIALS WHERE RAW_MATERIAL_ID = " & Request.QueryString("ID")
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        If tblResults.Rows.Count > 0 Then
            txtName.Text = tblResults.Rows(0)("Name")
            ddlCategory.SelectedValue = tblResults.Rows(0)("CATEGORY_ID")
            ddlBrand.SelectedValue = tblResults.Rows(0)("BRAND_ID")
            ddlUOM.SelectedValue = tblResults.Rows(0)("UOM_ID")
            txtDescription.Text = tblResults.Rows(0)("DESCRIPTION")
            txtOnHandQty.Text = tblResults.Rows(0)("ONHAND_QTY")
            txtMinOnHandQty.Text = tblResults.Rows(0)("MINIMUM_ONHAND_QTY")
            txtReorderQty.Text = tblResults.Rows(0)("REORDER_QTY")
            btnSaveNewRawMaterial.Text = "UPDATE"
        End If
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

    Private Sub loadRawMaterialListing()
        Dim strSQL As String = "Select * from [INVENTORY].[vw_Raw_Materials] ORDER BY NAME"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        If tblResults.Rows.Count > 0 Then
            rptrRawMaterialListing.DataSource = tblResults
            rptrRawMaterialListing.DataBind()
        Else

        End If

    End Sub

    Private Sub hidePanels()

        pnlAddPropertyToRawMaterial.Visible = False
        pnlAddMultiplePropertyToRawMaterial.Visible = False

        pnlNewBrand.Visible = False
        pnlNewCategory.Visible = False
        pnlNewProperty.Visible = False
        pnlNewUOM.Visible = False

        pnlRawMaterialListing.Visible = False
        pnlRawMaterialProperties.Visible = False
        pnlRawMaterialUpdateDetails.Visible = False

    End Sub

    Protected Sub btnCancelNewRawMaterial_Click(sender As Object, e As EventArgs) Handles btnCancelNewRawMaterial.Click
        Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
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
        pnlRawMaterialUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewBrand_Click(sender As Object, e As EventArgs) Handles btnCancelNewBrand.Click
        hidePanels()
        txtNewBrand.Text = ""
        pnlRawMaterialUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewUOM_Click(sender As Object, e As EventArgs) Handles btnCancelNewUOM.Click
        hidePanels()
        txtNewUOM.Text = ""
        pnlRawMaterialUpdateDetails.Visible = True
    End Sub

    Protected Sub btnCancelNewProperty_Click(sender As Object, e As EventArgs) Handles btnCancelNewProperty.Click
        hidePanels()
        txtNewProperty.Text = ""
        pnlAddPropertyToRawMaterial.Visible = True
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
                pnlAddPropertyToRawMaterial.Visible = True
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
                pnlRawMaterialUpdateDetails.Visible = True
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
                pnlRawMaterialUpdateDetails.Visible = True
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
                pnlRawMaterialUpdateDetails.Visible = True
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
                    "(" & Request.QueryString("ID") & "," & ddlPropertiesAdd.SelectedValue & ",'" & txtAddPropertyDetails.Text.Replace("'", "''") & "',0)"
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
                    "(" & Request.QueryString("ID") & "," & property_id & ",'" & value.Replace("'", "''") & "',0)"
        g_IO_Execute_SQL(strsQL, False)
    End Sub

    Protected Sub btnSaveNewRawMaterial_Click(sender As Object, e As EventArgs) Handles btnSaveNewRawMaterial.Click
        If btnSaveNewRawMaterial.Text.ToUpper = "UPDATE" Then
            Dim strSQL As String = "UPDATE [INVENTORY].[RAW_MATERIAL]
                   SET [NAME] = '" & txtName.Text.Replace("'", "''") & "'
                      ,[CATEGORY_ID] = " & ddlCategory.SelectedValue & "
                      ,[BRAND_ID] = " & ddlBrand.SelectedValue &
                      ",[DESCRIPTION] = '" & txtDescription.Text.Replace("'", "''") & "'
                      ,[UOM_ID] = " & ddlUOM.SelectedValue & "
                      ,[ONHAND_QTY] = " & txtOnHandQty.Text.Replace("'", "''") & "
                      ,[MINIMUM_ONHAND_QTY] = " & txtMinOnHandQty.Text.Replace("'", "''") & "
                      ,[REORDER_QTY] = " & txtReorderQty.Text.Replace("'", "''") & "
                      WHERE RAW_MATERIAL_ID = " & Request.QueryString("id")
            g_IO_Execute_SQL(strSQL, False)
            Response.Redirect("InvRawMaterials.aspx")

        ElseIf btnSaveNewRawMaterial.Text.ToUpper = "EDIT" Then
            enableForm()

        Else
            If txtName.Text <> "" And txtDescription.Text <> "" And ddlBrand.SelectedValue <> -1 And ddlCategory.SelectedValue <> -1 And ddlUOM.SelectedValue <> -1 Then
                Dim strSQL As String = "INSERT INTO [INVENTORY].[RAW_MATERIAL]
           ([NAME]
           ,[CATEGORY_ID]
           ,[BRAND_ID]
           ,[DESCRIPTION]
           ,[UOM_ID]
           ,[ONHAND_QTY]
           ,[MINIMUM_ONHAND_QTY]
           ,[REORDER_QTY])
     VALUES
           ('" & txtName.Text.Replace("'", "''") & "'," &
               ddlCategory.SelectedValue & "," &
               ddlBrand.SelectedValue & "," &
              "'" & txtDescription.Text.Replace("'", "''") & "'," &
              ddlUOM.SelectedValue & "," &
              txtOnHandQty.Text.Replace("'", "''") & "," &
              txtMinOnHandQty.Text.Replace("'", "''") & "," &
              txtReorderQty.Text.Replace("'", "''") & ")"

                Dim id As Integer = g_IO_Execute_SQL(strSQL, False).Rows(0)(0)
                Response.Redirect("InvRawMaterials.aspx?action=update&id=" & id)
            End If
        End If


    End Sub



    Protected Sub btnSaveAddMultipleProperty_Click(sender As Object, e As EventArgs) Handles btnSaveAddMultipleProperty.Click
        If txtAddPropertyDetails1.Text <> "" And ddlPropertiesAdd1.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd1.SelectedValue, txtAddPropertyDetails1.Text)
        End If

        If txtAddPropertyDetails2.Text <> "" And ddlPropertiesAdd2.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd2.SelectedValue, txtAddPropertyDetails2.Text)
        End If

        If txtAddPropertyDetails3.Text <> "" And ddlPropertiesAdd3.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd3.SelectedValue, txtAddPropertyDetails3.Text)
        End If

        If txtAddPropertyDetails4.Text <> "" And ddlPropertiesAdd4.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd4.SelectedValue, txtAddPropertyDetails4.Text)
        End If

        If txtAddPropertyDetails5.Text <> "" And ddlPropertiesAdd5.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd5.SelectedValue, txtAddPropertyDetails5.Text)
        End If

        If txtAddPropertyDetails6.Text <> "" And ddlPropertiesAdd6.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd6.SelectedValue, txtAddPropertyDetails6.Text)
        End If

        If txtAddPropertyDetails7.Text <> "" And ddlPropertiesAdd7.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd7.SelectedValue, txtAddPropertyDetails7.Text)
        End If

        If txtAddPropertyDetails8.Text <> "" And ddlPropertiesAdd8.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd8.SelectedValue, txtAddPropertyDetails8.Text)
        End If

        If txtAddPropertyDetails9.Text <> "" And ddlPropertiesAdd9.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd9.SelectedValue, txtAddPropertyDetails9.Text)
        End If

        If txtAddPropertyDetails10.Text <> "" And ddlPropertiesAdd10.SelectedValue <> -1 Then
            addPropertyToProduct(ddlPropertiesAdd10.SelectedValue, txtAddPropertyDetails10.Text)
        End If

        Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
    End Sub

    Protected Sub btnCancelAddProperty_Click(sender As Object, e As EventArgs) Handles btnCancelAddProperty.Click, btnCancelAddMultipleProperty.Click
        Response.Redirect("InvRawMaterials.aspx?action=update&id=" & Request.QueryString("ID"))
    End Sub

    Protected Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Response.Redirect("InvRawMaterials.aspx")
    End Sub
End Class