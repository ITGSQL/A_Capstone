<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmployeeManagement.aspx.vb" Inherits="LSS.EmployeeManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
    <asp:Panel ID="pnlEmployee_Listing" runat="server">
        <table class="table table-bordered table-responsive">
            <thead>
                <tr>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>Login ID</th>
                    <th>Position</th>
                    <th>Rate</th>
                    <th><asp:Button ID="btnAdd" runat="server" Text="Add" /></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptEmployees" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("Last_Name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("First_Name") %>' />
                            </td>

                            <td>
                                <asp:Label ID="lblLoginID" runat="server" Text='<%# Eval("Login_Id") %>' />
                            </td>
                            
                            <td>
                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblHourly" runat="server" Text='<%# Eval("Hourly") %>' />
                            </td>
                            <td>
                                <a href="EmployeeManagement.aspx?id=<%# Eval("Sys_Users_ID") %>">Update</a>
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

    <asp:Panel ID="pnlEmployee_Action" runat="server">
        <h3>Modify Employee</h3>
        <table class="table table-responsive table-bordered">
            <tr><td>Employee ID</td><td><asp:TextBox ID="txtEmployeeID" runat="server" Enabled="false"></asp:TextBox></td></tr>
            <tr><td>First Name</td><td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td></tr>
            <tr><td>Last Name</td><td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td></tr>
            <tr><td>LoginID</td><td><asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox></td></tr>
            <tr><td>Password</td><td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox></td></tr>
            <tr><td>Vacation Days</td><td><asp:TextBox ID="txtVacationDays" runat="server"></asp:TextBox></td></tr>
            <tr><td>Tax_Witholding</td><td><asp:TextBox ID="txtTaxWithholding" runat="server"></asp:TextBox></td></tr>
            <tr><td>Position</td><td><asp:DropDownList ID="ddlPosition" runat="server"></asp:DropDownList></td></tr>
            <tr><td>Hourly Pay</td><td><asp:TextBox ID="txtHourly" runat="server"></asp:TextBox></td></tr>
            <tr><td>&nbsp;</td><td><asp:Button ID="btnEmployee_Action" runat="server" Text="Action" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnEmployee_Cancel" runat="server" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnEmployee_Delete" runat="server" Text="Delete" /></td></tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="pnlDelete_Verify" runat="server">
        <h3>Are you sure you want to delete this Employee? </h3>
        <br /><br />


        <asp:Button ID="btnDelete_Yes" runat="server" Text="Confirm" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete_Cancel" runat="server" Text="Cancel" />
    </asp:Panel>
</asp:Content>
