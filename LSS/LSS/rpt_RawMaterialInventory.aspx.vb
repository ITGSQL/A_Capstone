Public Class rpt_RawMaterialInventory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

            Dim strSQL As String = "Select * from [INVENTORY].[vw_Raw_Materials] order by category_name, name"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        End If
    End Sub

End Class