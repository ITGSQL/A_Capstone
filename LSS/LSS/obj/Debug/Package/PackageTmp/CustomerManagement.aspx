<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CustomerManagement.aspx.vb" Inherits="LSS.CustomerManagment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
    <asp:Panel ID="pnlCustomer_Listing" runat="server">
        <table class="table table-bordered table-responsive">
            <thead>
                <tr>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th><asp:Button ID="btnAdd" runat="server" Text="Add" /></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptCustomers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("Last_Name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("First_Name") %>' />
                            </td>
                            
                            <td>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' />
                            </td>
                            <td>
                                <a href="CustomerManagement.aspx?id=<%# Eval("CUSTOMER_ID") %>">Update</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <asp:Repeater ID="rptPager" runat="server">
    <ItemTemplate>
        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
            CssClass='<%# If(Convert.ToBoolean(Eval("Enabled")), "page_enabled", "page_disabled")%>'
            OnClick="Page_Changed" OnClientClick='<%# If(Not Convert.ToBoolean(Eval("Enabled")), "return false;", "") %>'></asp:LinkButton>
    </ItemTemplate>

</asp:Repeater>

    </asp:Panel>

    <asp:Panel ID="pnlCustomer_Action" runat="server">
        <table class="table table-responsive table-bordered">
            <tr><td>Customer ID</td><td><asp:TextBox ID="txtCustomerID" runat="server" Enabled="false"></asp:TextBox></td></tr>
            <tr><td>First Name</td><td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td></tr>
            <tr><td>Last Name</td><td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td></tr>
            <tr><td>Phone</td><td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td></tr>
            <tr><td>Email</td><td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td></tr>
            <tr><td>Address Line 1</td><td><asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox></td></tr>
            <tr><td>Address Line 2</td><td><asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox></td></tr>
            <tr><td>City</td><td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td></tr>
            <tr><td>State</td><td><asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList></td></tr>
            <tr><td>Zip</td><td><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td></tr>
            <tr><td>&nbsp;</td><td><asp:Button ID="btnCustomer_Action" runat="server" Text="Action" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCustomer_Cancel" runat="server" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCustomer_Delete" runat="server" Text="Delete" /></td></tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="pnlDelete_Verify" runat="server">
        <h3>Are you sure you want to delete this user? </h3>
        <br /><br />


        <asp:Button ID="btnDelete_Yes" runat="server" Text="Confirm" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete_Cancel" runat="server" Text="Cancel" />
    </asp:Panel>
</asp:Content>
