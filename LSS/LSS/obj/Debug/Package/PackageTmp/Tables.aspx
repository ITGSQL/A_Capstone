<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Tables.aspx.vb" Inherits="LSS.Tables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jQuery-2.2.4.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/common.js"></script>
    <script src="Scripts/jquery.timepicker.min.js"></script>
    <script src="Content/jquery.contextMenu.js"></script>
    <link href="Scripts/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.structure.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Dashboard.css" rel="stylesheet" />
    <link href="Scripts/font-awesome.min.css" rel="stylesheet" />
    <link href="Scripts/jquery.timepicker.css" rel="stylesheet" />
    <link href="Content/jquery.contextMenu.css" rel="stylesheet" />
    <style type="text/css">
        td, th {
            border: thin solid black; 
            padding: 5px 10px;
        }

        th {
            background-color: #ffc8c8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td>Select a table:</td><td>
                <asp:DropDownList ID="ddlTableListing" runat="server"></asp:DropDownList></td><td>
                    <asp:Button ID="btnLoadData" runat="server" Text="Load" /></td></tr>
        </table>
        <asp:Literal ID="litResults" runat="server"></asp:Literal>
    </form>
</body>
</html>
