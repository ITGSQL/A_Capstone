Public Class InvRawMaterials
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            hidePanels()





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