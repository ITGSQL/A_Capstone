<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Orders.aspx.vb" Inherits="LSS.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 48px;
        }

        a.buttonLink {
            padding: 5px 10px;
            background-color: #c7c7c7;
            border: thin solid #808080;
            color: black;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel ID="pnlOrderDetails" runat="server">
        <h3>Order Details</h3>
        <div style="width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <h4>Customer</h4>
            <table class="table table-responsive table-bordered">
                <tr><td>Name</td><td><asp:Label ID="lblCustomerName" runat="server" Text="lblCustomerName"></asp:Label></td></tr>
                <tr><td>Address</td><td><asp:Label ID="lblCustomerAddress" runat="server" Text="lblCustomerAddress"></asp:Label></td></tr>
                <tr><td>&nbsp;</td><td><asp:Label ID="lblCitySateZip" runat="server" Text="lblCitystateZip"></asp:Label></td></tr>
            </table>
        </div>

        <div style="width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <h4>Enter Product Code</h4>
            <asp:Literal ID="litProductEntryError" runat="server"></asp:Literal>
            <table class="table table-responsive table-bordered">
                <tr><td>Product: <asp:TextBox ID="txtProductCode_Add" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;QTY: <asp:TextBox ID="txtQty" Text="1" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddProduct" runat="server" Text="Add" /></td>
                </tr>
            </table>
        </div>

        <div style="width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <h4>Current Order</h4>
            <asp:Literal ID="litCurOrderListing" runat="server"></asp:Literal>
        </div>
        <div style="visibility: hidden; width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <asp:Literal ID="litOrderID" runat="server"></asp:Literal>
            <asp:Literal ID="litCustID" runat="server"></asp:Literal>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlCustomerSearch" runat="server">
        <h3>Customer Search</h3>
        <table class="table table-responsive table-bordered">
            <tr><td>Search by:</td><td><asp:DropDownList ID="ddlSearchOptions" runat="server">
                    <asp:ListItem Value="Phone" Text="Phone"></asp:ListItem>
                    <asp:ListItem Value="Name" Text="Name"></asp:ListItem>
                    <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
                </asp:DropDownList></td></tr>
            <tr><td>Criteria:</td>
                <td>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </td></tr>
            <tr><td class="auto-style1"></td><td class="auto-style1"><asp:Button ID="btnCustomerSearch" runat="server" Text="Search" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowNewCustomerPanel" runat="server" Text="Add New" /></td></tr>
        </table>
        <asp:Literal ID="litSearchResults_Cust" runat="server"></asp:Literal>
    </asp:Panel>

    <asp:Panel ID="pnlAddItem" runat="server">
        <h3>Add Item</h3>

    </asp:Panel>

    <asp:Panel ID="pnlCustomer" runat="server">
        <h3>New Customer</h3>
        <table class="table table-responsive table-bordered">
            <tr><td>First Name</td><td><asp:TextBox ID="txtFirstName" runat="server" Height="22px"></asp:TextBox></td></tr>
            <tr><td>Last Name</td><td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td></tr>
            <tr><td>Address1</td><td><asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox></td></tr>
            <tr><td>Address2</td><td><asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox></td></tr>
            <tr><td>City</td><td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td></tr>
            <tr><td>State</td><td><asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList> </td></tr>
            <tr><td>Zip</td><td><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr><td>Phone</td><td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td></tr>
            <tr><td>Email</td><td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td></tr>
            <tr><td>&nbsp;</td><td><asp:Button ID="btnSaveNewCustomer" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClearNewCustomerForm" runat="server" Text="Clear" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewCustomer" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

</asp:Content>
