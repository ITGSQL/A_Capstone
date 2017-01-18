<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InvRawMaterials.aspx.vb" Inherits="LSS.InvRawMaterials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <span class="formTitle">Raw Material Management</span>
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
    <asp:Panel ID="pnlRawMaterialListing" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td>Name</td><td>Category</td><td>Brand</td><td>Description</td><td>On-Hand</td><td><a href="InvRawMaterials.aspx?&action=NewRawMaterial"><img src="images/round_add_red.png" style="height: 16px; width: 16px;" /></a></td></tr>
            <asp:Repeater ID="rptrRawMaterialListing" runat="server">
                <ItemTemplate>
                    <tr><td><%#DataBinder.Eval(Container.DataItem, "Name") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "Category_Name") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "Brand") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "Description") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "onhand_qty") %></td>
                        <td><a href="InvRawMaterials.aspx?id=<%#DataBinder.Eval(Container.DataItem, "RAW_MATERIAL_ID") %>&action=Update"><img src="images/edit.png" /></a>
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="InvRawMaterials.aspx?id=<%#DataBinder.Eval(Container.DataItem, "RAW_MATERIAL_ID") %>&action=Delete"><img src="images/trash.png" /></a>
                        </td></tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>

    </asp:Panel>

    <asp:Panel ID="pnlRawMaterialUpdateDetails" runat="server">
        <table class="table table-responsive table-bordered" style="max-width: 600px; float: left;">
            <tr>
                <td>Name: </td>
                <td><asp:TextBox ID="txtName" runat="server" placeholder="Material Name..." Width="100%"></asp:TextBox></td>
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
                <td><asp:TextBox ID="txtDescription" runat="server" placeholder="ex:Button,20mm,Plastic" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Unit of Measure:</td>
                <td><asp:DropDownList ID="ddlUOM" runat="server"></asp:DropDownList><asp:ImageButton ID="imgNewUOM" runat="server" Height="16px" ImageUrl="~/images/round_add_red.PNG" Width="16px" /></td>       
            </tr>
            <tr>
                <td>On Hand Qty:</td>
                <td><asp:TextBox ID="txtOnHandQty" runat="server" placeholder="25" Width="100%"></asp:TextBox></td>       
            </tr>
            <tr>
                <td>Minimum On Hand Qty:</td>
                <td><asp:TextBox ID="txtMinOnHandQty" runat="server" placeholder="5" Width="100%"></asp:TextBox></td>       
            </tr>
            <tr>
                <td>Reorder Qty:</td>
                <td><asp:TextBox ID="txtReorderQty" runat="server" placeholder="20" Width="100%"></asp:TextBox></td>       
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveNewRawMaterial" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelNewRawMaterial" runat="server" Text="Cancel" /></td></tr>

        </table>
    </asp:Panel>

    <asp:Panel ID="pnlRawMaterialProperties" runat="server">
        <table class="table table-bordered table-responsive">
            <tr><td>Property</td><td>Value</td><td><asp:Literal ID="litNewPropertyImage" runat="server"></asp:Literal></td></tr>
            <asp:Repeater ID="rptrProperties" runat="server">
                <ItemTemplate>
                    
                    <tr><td><%#DataBinder.Eval(Container.DataItem, "Property") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "VALUE") %></td>
                        <td><a href="InvRawMaterials.aspx?id=<%#DataBinder.Eval(Container.DataItem, "PRODUCT_ID") %>&action=UpdateProperty&IPID=<%#DataBinder.Eval(Container.DataItem, "INVENTORY_PROPERTY_ID") %>"><img src="images/edit.png" /></a>
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="InvRawMaterials.aspx?id=<%#DataBinder.Eval(Container.DataItem, "PRODUCT_ID") %>&action=DeleteProperty&IPID=<%#DataBinder.Eval(Container.DataItem, "INVENTORY_PROPERTY_ID") %>"><img src="images/trash.png" /></a>
                        </td></tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

    </asp:Panel>

    <asp:Panel ID="pnlAddMultiplePropertyToRawMaterial" runat="server">
        <table class="table table-responsive table-bordered" style="float: left; max-width: 300px;">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">Add Properties</span></td></tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd1" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd2" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd3" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails3" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd4" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails4" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd5" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails5" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd6" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails6" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd7" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails7" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd8" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails8" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd9" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails9" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Property: </td>
                <td><asp:DropDownList ID="ddlPropertiesAdd10" runat="server"></asp:DropDownList></td>
                <td>Property Details: </td>
                <td><asp:TextBox ID="txtAddPropertyDetails10" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="btnSaveAddMultipleProperty" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelAddMultipleProperty" runat="server" Text="Cancel" /></td></tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlAddPropertyToRawMaterial" runat="server">
        <table class="table table-responsive table-bordered" style="float: left; max-width: 300px;">
            <tr><td colspan="2"><span style="font-size: 1.1em; font-weight: bold;">Add Properties</span></td></tr>
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
                <td><asp:TextBox ID="txtNewCategory" runat="server"></asp:TextBox></td>
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
