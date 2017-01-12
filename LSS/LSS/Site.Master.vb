Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        litHdrImage.Text = "<img src=""images/itgbrandslogo.png"" />"
        ''litHdrImage.Text = "<span style=""color: white; font-size: 2em; font-weight: bold;>LSS</span>"
    End Sub

End Class