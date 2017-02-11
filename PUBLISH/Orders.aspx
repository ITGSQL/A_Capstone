<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Orders.aspx.vb" Inherits="LSS.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel ID="pnlOrderDetails" runat="server">
        <h3>Order Details</h3>
        <div style="width: 90%; border: thin solid black; margin: 0 auto;">

        </div>
    </asp:Panel>

    <asp:Panel ID="pnlCustomerSearch" runat="server">
        <h3>Customer Search</h3>

    </asp:Panel>

    <asp:Panel ID="pnlAddItem" runat="server">
        <h3>Add Item</h3>

    </asp:Panel>

    <asp:Panel ID="pnlCustomer" runat="server">
        <h3>New Customer</h3>
        <table class="table table-responsive table-bordered">
            <tr><td>First Name</td><td><asp:TextBox ID="txtCustomerFirstName" runat="server"></asp:TextBox></td></tr>
        </table>
    </asp:Panel>

</asp:Content>
