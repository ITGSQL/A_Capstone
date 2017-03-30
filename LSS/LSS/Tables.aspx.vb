Public Class Tables
    Inherits System.Web.UI.Page

    Dim strSQL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            loadTableListing()
        End If
    End Sub

    Private Sub loadTableListing()
        strSQL = "select S.name + '.' + T.Name AS 'TABLE_NAME', '[' + S.NAME + '].[' + T.NAME + ']' AS VALUE from sys.tables T
                    inner join sys.schemas S ON t.schema_id = s.schema_id
                    union
                    select S.name + '.' + T.Name AS 'TABLE_NAME', '[' + S.NAME + '].[' + T.NAME + ']' AS VALUE from sys.views T
                                        inner join sys.schemas S ON t.schema_id = s.schema_id
                    order by 'TABLE_NAME', value"
        Dim tblResults = g_IO_Execute_SQL(strSQL, False)
        If tblResults.Rows.Count > 0 Then
            ddlTableListing.DataTextField = "TABLE_NAME"
            ddlTableListing.DataValueField = "VALUE"
            ddlTableListing.DataSource = tblResults
            ddlTableListing.DataBind()
        Else
            ddlTableListing.Enabled = False
        End If


    End Sub

    Protected Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        strSQL = "Select * from " & ddlTableListing.SelectedValue
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        If tblResults.Rows.Count > 0 Then
            ''Start the table
            litResults.Text = "<table style=""margin-left: 20px;""><thead><tr>"
            For Each col In tblResults.Columns
                litResults.Text &= "<th>" & col.ToString & "</th>"
            Next
            litResults.Text &= "</tr></thead><tbody>"

            For Each row In tblResults.Rows
                litResults.Text &= "<tr>"
                For Each col In tblResults.Columns
                    litResults.Text &= "<td>" & row(col).ToString & "</td>"
                Next
                litResults.Text &= "</tr>"
            Next

            litResults.Text &= "</tbody></table>"

        Else
            litResults.Text = "<h3 style=""margin-left: 20px;"">No Data Found . . .</h3>"
        End If
    End Sub
End Class