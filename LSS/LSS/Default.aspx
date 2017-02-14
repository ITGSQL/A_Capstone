<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.vb" Inherits="LSS._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <span class="portalLink" onclick="redirect('InvProducts.aspx')">Product Inventory</span>
    <span class="portalLink" onclick="redirect('InvRawMaterials.aspx')">Raw Material Inventory</span>
    <span class="portalLink" onclick="redirect('OrderHistory.aspx')">Order History</span>
    <span class="portalLink" onclick="redirect('Orders.aspx')">Orders</span>
    <span class="portalLink" onclick="redirect('CustomerManagement.aspx')">Customer Management</span>
</asp:Content>