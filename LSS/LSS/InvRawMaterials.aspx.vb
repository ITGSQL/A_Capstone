Public Class InvRawMaterials
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            hidePanels()
            loadRawMaterialListing()
            pnlRawMaterialListing.Visible = True



        End If
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
        pnlRawMaterialUpdateDetails.Visible = False
        pnlRawMaterialProperties.Visible = False
    End Sub

End Class