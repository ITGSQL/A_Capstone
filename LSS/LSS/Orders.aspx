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

        .btnPaymentType {
           height: 100px;
           width: 200px;
           margin: 20px;
        }

        .ui-autocomplete {
            max-height: 100px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
          }
        h3.error {
            color: red;
            font-style: italic;
            display: block;
        }
    </style>
    <asp:Literal ID="litHeaderCode" runat="server"></asp:Literal>
    <script>
        $(function () {
            $("#<%= txtProductCode_Add.ClientID %>").autocomplete({
                source: productTags
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Literal ID="litErrorMessage" runat="server"></asp:Literal>
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
                <tr><td>Product: <asp:TextBox ID="txtProductCode_Add" runat="server" Width="300px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;QTY: <asp:TextBox ID="txtQty" Text="1" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddProduct" runat="server" Text="Add" /></td>
                </tr>
            </table>
        </div>

        <div style="width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <h4>Current Order</h4>
            <asp:Literal ID="litCurOrderListing" runat="server"></asp:Literal>
            <div style="float: right; margin-bottom: 20px;">
                <asp:Button ID="btnOrderDetails_Clear" runat="server" Text="Clear" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOrderDetails_Save" runat="server" Text="Pay" />
            </div>
        </div>
        <div style="visibility: hidden; width: 90%; border: thin solid #e0e0e0; margin: 0 auto;">
            <asp:Literal ID="litOrderID" runat="server"></asp:Literal>
            <asp:Literal ID="litCustID" runat="server"></asp:Literal>
            <asp:Literal ID="litSubTotal" runat="server"></asp:Literal>
            <asp:Literal ID="litSalesTax" runat="server"></asp:Literal>
            <asp:Literal ID="litGrandTotal" runat="server"></asp:Literal>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirmVoid" runat="server">
        <asp:Literal ID="litMessage_Void" runat="server"></asp:Literal>
        <asp:Button runat="server" ID="btnVoid_Confirm" Text="Confirm" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnVoid_Cancel" Text="Cancel" />
    </asp:Panel>
    <asp:Panel ID="pnlCustomerSearch" runat="server">
        <h3>Customer Search</h3>
        <table class="table table-responsive table-bordered">
            <tr><td>Search by:</td><td>
                <asp:DropDownList ID="ddlSearchOptions" runat="server">
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


    <asp:Panel ID="pnlPaymentTypes" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h3>Pay</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <asp:Button ID="btnPaymentType_Cash" runat="server" Text="Cash" CssClass="btnPaymentType" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnPaymentType_Credit" runat="server" Text="Credit Card" CssClass="btnPaymentType" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnPaymentType_Check" runat="server" Text="Check" CssClass="btnPaymentType" />
                </div>
            </div>
            <div class="row" style="margin-top: 200px;">
                <asp:Button ID="btnPaymentTypes_Cancel" runat="server" Text="Cancel Payment" />
            </div>
        </div>
        
        
    </asp:Panel>

    <asp:Panel ID="pnlPayment_Cash" runat="server">
        <h3>Cash Payment</h3>
        <table class="table table-responsive">
            <tr>
                <td>Total Received</td><td><asp:Literal ID="litPayment_Cash_TotalReceived" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Total Due</td><td><asp:Literal ID="litPayment_Cash_TotalDue" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Cash Received</td><td><asp:TextBox ID="txtPayment_CashReceived" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Literal ID="litPayment_Cash_Message" runat="server"></asp:Literal>
        <asp:Button ID="btnPayCancel_Cash" runat="server" Text="Cancel Payment" /><asp:Button ID="btnPayContinue_Cash" runat="server" Text="Continue" />
    </asp:Panel>

    <asp:Panel ID="pnlPayment_Credit" runat="server">
        <h3>Credit Card Payment</h3>

        *For capstone testing purposes, a list of credit cards have been populated for your convenience.<br />

        <table class="table table-bordered table-responsive">
            <tr><td>Credit Card:</td><td>
                <asp:DropDownList ID="ddlCreditCardList" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr><td>Card Type</td><td><asp:Literal ID="litCCCardType" runat="server"></asp:Literal></td></tr>
            <tr><td>Card #</td><td><asp:Literal ID="litCCCardNumber" runat="server"></asp:Literal></td></tr>
            <tr><td>Card Expiration</td><td><asp:Literal ID="litCCExp" runat="server"></asp:Literal></td></tr>
            <tr><td>Card CVV</td><td><asp:Literal ID="litCCCVV" runat="server"></asp:Literal></td></tr>
        </table>
        <asp:Literal ID="litMessage_Credit" runat="server"></asp:Literal>
        <asp:Button ID="btnPayContinue_Credit" runat="server" Text="ConfirmCharge" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPayCancel_Credit" runat="server" Text="Cancel Payment" />
    </asp:Panel>

    <asp:Panel ID="pnlPayment_Check" runat="server">
        <h3>Check Payment</h3>
            <table class="table table-bordered table-responsive">
                <tr><td>Check Number</td><td><asp:TextBox ID="txtPaymentCheck_CheckNumber" runat="server"></asp:TextBox></td></tr>
                <tr><td>DL State</td><td><asp:DropDownList ID="ddlPaymentCheck_DLState" runat="server"></asp:DropDownList> </td></tr>
                <tr><td>DL Number</td><td><asp:TextBox ID="txtPaymentCheck_DLNumber" runat="server"></asp:TextBox></td></tr>
                <tr><td>Amount</td><td><asp:TextBox ID="txtPaymentCheck_Amount" runat="server"></asp:TextBox></td></tr>
            </table>
        <asp:Literal ID="litMessate_Check" runat="server"></asp:Literal>
        <asp:Button ID="btnPayContine_Check" runat="server" Text="Accept Check" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPayCancel_Check" runat="server" Text="Cancel Payment" />
    </asp:Panel>

    </asp:Content>