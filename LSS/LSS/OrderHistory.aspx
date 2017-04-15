<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OrderHistory.aspx.vb" Inherits="LSS.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .message {
            color: green;
            font-weight: bold;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <h3>Order History</h3>

    <asp:Literal ID="litMessage" runat="server"></asp:Literal>

    <table class="table table-bordered table-responsive">
        <thead>
            <tr>
                <th>Receipt #</th><th>Date</th><th>Customer Name</th><th># Items</th><th>Total</th><th>&nbsp;</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptrOrderHistory" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#DataBinder.Eval(Container.DataItem, "HEADER_ID") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "DATE_CREATED") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "CUSTOMER_NAME") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "TOTALITEMS") %></td>
                        <td><%#DataBinder.Eval(Container.DataItem, "GRAND_TOTAL") %></td>
                        <td><a href="Orders.aspx?id=<%#DataBinder.Eval(Container.DataItem, "HEADER_id") %>&cid=<%#DataBinder.Eval(Container.DataItem, "CUST_ID") %>&st=p">View</a></td>
<%--                        &nbsp;&nbsp;&nbsp;&nbsp;<a href="Orders.aspx?id=<%#DataBinder.Eval(Container.DataItem, "HEADER_id") %>&V=true&cid=<%#DataBinder.Eval(Container.DataItem, "ID") %>">Void</a>--%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
