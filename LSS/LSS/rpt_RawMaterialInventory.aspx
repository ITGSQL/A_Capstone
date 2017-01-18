<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="rpt_RawMaterialInventory.aspx.vb" Inherits="LSS.rpt_RawMaterialInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .RED {
            background-color: #ffc2c2;
        }

        .YELLOW {
            background-color: #fff398;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <h3>Raw Material Inventory</h3>
    <asp:Literal ID="litReport" runat="server"></asp:Literal>
</asp:Content>
