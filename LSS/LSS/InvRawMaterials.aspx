<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InvRawMaterials.aspx.vb" Inherits="LSS.InvRawMaterials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <span class="formTitle">Raw Material Management</span>

    <asp:Panel ID="pnlRawMaterialListing" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td>Name</td><td>Category</td><td>Brand</td><td>Description</td><td>On-Hand</td></tr>
            <asp:Repeater ID="rptrRawMaterialListing" runat="server">
                <ItemTemplate>

                </ItemTemplate>
            </asp:Repeater>

        </table>

    </asp:Panel>

    <asp:Panel ID="pnlRawMaterialUpdateDetails" runat="server">
        <table class="table table-responsive table-bordered">
            <tr>
                <td>Name: </td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Category: 
                </td>
                <td><asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList><asp:ImageButton ID="imgNewCategory" runat="server" Height="16px" ImageUrl="~/images/round_add_red.PNG" Width="16px" /></td>
            
            </tr>
            <tr>
                <td>Brand: </td>
                <td><asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList><asp:ImageButton ID="imgNewBrand" runat="server" Height="16px" ImageUrl="~/images/round_add_red.PNG" Width="16px" /></td>       
            </tr>
            <tr>
                <td>Description: </td>
                <td><asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Unit of Measure:</td>
                <td><asp:DropDownList ID="ddlUOM" runat="server"></asp:DropDownList><asp:ImageButton ID="imgNewUOM" runat="server" Height="16px" ImageUrl="~/images/round_add_red.PNG" Width="16px" /></td>       
            </tr>
            <tr>
                <td>On Hand Qty:</td>
                <td><asp:TextBox ID="txtOnHandQty" runat="server"></asp:TextBox></td>       
            </tr>
            <tr>
                <td>Minimum On Hand Qty:</td>
                <td><asp:TextBox ID="txtMinOnHandQty" runat="server"></asp:TextBox></td>       
            </tr>
            <tr>
                <td>Reorder Qty:</td>
                <td><asp:TextBox ID="txtReorderQty" runat="server"></asp:TextBox></td>       
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveNewRawMaterial" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewRawMaterial" runat="server" Text="Cancel" /></td></tr>

        </table>
    </asp:Panel>

    <asp:Panel ID="pnlRawMaterialProperties" runat="server">
        <asp:Literal ID="litRawMaterialProperties" runat="server"></asp:Literal>

    </asp:Panel>

    <asp:Panel ID="pnlAddPropertyToRawMaterial" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">Add Property</span></td></tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveAddProperty" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelAddProperty" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlNewCategory" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">New Category</span></td></tr>
            <tr>
                <td>Category Name: </td>
                <td><asp:TextBox ID="txtCategory" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnAddNewCategory" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewCategory" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlNewBrand" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">New Brand</span></td></tr>
            <tr>
                <td>Brand Name: </td>
                <td><asp:TextBox ID="txtNewBrand" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveNewBrand" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewBrand" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlNewUOM" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">New Unit Of Measure</span></td></tr>
            <tr>
                <td>Unit Of Measure: </td>
                <td><asp:TextBox ID="txtNewUOM" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveNewUOM" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewUOM" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlNewProperty" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">New Property</span></td></tr>
            <tr>
                <td>Property: </td>
                <td><asp:TextBox ID="txtNewProperty" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveNewProperty" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewProperty" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

</asp:Content>
