<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InvRawMaterials.aspx.vb" Inherits="LSS.InvRawMaterials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <span class="formTitle">Raw Material Management</span>

    <table>
        <tr>
            <td>
                Name
            </td>
            <td></td>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </tr>
        <tr>
            <td>
                Category
            </td>
            <td></td>
            <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
        </tr>
        <tr>
            <td>
                Category
            </td>
            <td></td>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        </tr>
    </table>
</asp:Content>
