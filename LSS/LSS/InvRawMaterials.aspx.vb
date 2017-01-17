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
                    pnlRawMaterialUpdateDetails.Visible = True


                End If

            Else
                loadRawMaterialListing()
                pnlRawMaterialListing.Visible = True
            End If
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

        pnlNewBrand.Visible = False
        pnlNewCategory.Visible = False
        pnlNewProperty.Visible = False
        pnlNewUOM.Visible = False

        pnlRawMaterialListing.Visible = False
        pnlRawMaterialProperties.Visible = False
        pnlRawMaterialUpdateDetails.Visible = False

    End Sub

    Protected Sub btnCancelNewRawMaterial_Click(sender As Object, e As EventArgs) Handles btnCancelNewRawMaterial.Click
        Response.Redirect("InvRawMaterials.aspx")
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
End Class