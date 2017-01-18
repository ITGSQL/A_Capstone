Public Class rpt_RawMaterialInventory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

            Dim strSQL As String = "Select * from [INVENTORY].[vw_Raw_Materials] order by category_name, name"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
            If tblResults.Rows.Count > 0 Then
                litReport.Text = "<table class=""table table-responsive table-bordered"">" &
                    "<thead><tr><th>Name</th><th>Category</th><th>Brand</th><th>OnHand</th><th>Minimum</th></tr></thead>"
                For Each row In tblResults.Rows
                    Dim onhand As Decimal = row("ONHAND_QTY")
                    Dim minimum As Decimal = row("MINIMUM_ONHAND_QTY")
                    Dim strClass As String = ""

                    If onhand < minimum Then
                        strClass = "RED"
                    ElseIf onhand = minimum Then
                        strClass = "YELLOW"
                    Else
                        strClass = "GREEN"
                    End If

                    litReport.Text &= "<tr class=""" & strClass & """><td>" & row("Name") & "</td><td>" & row("Category_Name") & "</td><td>" & row("Brand") & "</td><td>" & row("ONHAND_QTY").ToString & "</td><td>" & row("MINIMUM_ONHAND_QTY") & "</td></tr>"
                Next
                litReport.Text &= "</table>"


            Else
                litReport.Text = "No Raw Materials found in the database at this time."
            End If

        End If
    End Sub

End Class