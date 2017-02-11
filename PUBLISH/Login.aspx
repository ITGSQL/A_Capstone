<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="LSS.Login" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="ITG Brands - PLOT Portal">
    <meta name="author" content="Averitt, Brian K (MLMX22)">
    <title>ITG BRANDS - PLOT PORTAL</title>
    <script src="Scripts/jQuery-2.2.4.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/common.js"></script>
    <script src="Scripts/jquery.timepicker.min.js"></script>
    <script src="Content/jquery.contextMenu.js"></script>
    <link href="Scripts/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.structure.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Dashboard.css" rel="stylesheet" />
    <link href="Scripts/font-awesome.min.css" rel="stylesheet" />
    <link href="Scripts/jquery.timepicker.css" rel="stylesheet" />
    <link href="Content/jquery.contextMenu.css" rel="stylesheet" />
    <script>jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
    </script>

    <style>
        .container {
            margin-left: auto;
            margin-right: auto;
            margin-top: 15px;
            padding-top: 30px;
            padding-left: 3%;
            padding-right: 3%;
            color: #952526;
            background-color: #F5F5F5;
            border: solid thin #909090;
            min-height: 500px;
            width: 90%;
            min-width:330px;
        }
        .containerFoot {
            margin-left: auto;
            margin-right: auto;
            background-color: #F5F5F5;
            border: solid thin #909090;
        }
        .links {
            padding-top: 0px;
            text-decoration : none;
            padding : 4px;
            margin-left: 10px;
            color: #ffffff !important;                    
        }
        .links:hover {
            text-decoration: underline;
            color: #0094ff;              
        }
         .portalControl {
            margin-left: auto !important;
            margin-right: auto !important;
            padding-left: 10% !important;
        }
         .portalControl2 {
            margin-left: auto !important;
            margin-right: auto !important;
            padding-left: 6% !important;
        }
        .messageSuccess {
        color: green;
        font-weight: bold;
        }
        a:link {
            text-decoration: none;
            color: #808080;
        }

        a:visited {
            text-decoration: none;
            color: #808080;
        }

        a:hover {
            text-decoration: underline;
            color: #808080;
        }

        a:active {
            text-decoration: underline;
            color: #808080;
        }

        .divLogon {
            height: 600px;
            width: 400px;
            display: block;
            border: none;
            margin: 0 auto;
        }

    </style>

    <style type="text/css">
       .navRightTop {
           padding-top: 8px;
           text-align: right;
           padding-left: 20px;
           padding-right: 5px;
           padding-top: 87px;
           width: 100%;
           height: 70px;
           color: white;
           background-color: #C02526;
           border-bottom: white thin solid;
           font-size: 18px;
           /*background-image: linear-gradient(to right,Transparent,#C02526);*/
       }
</style>
</head>
<body>
    <form runat="server">
    <!--  Begin Navigation  -->
     <nav class="navbar navbar-inverse navbar-fixed-top" style=" padding-top: 0px; height: 130px; background-color: #333333; ">
      <div class="container-fluid" style="padding-left: 0px;padding-right: 0px; height: 86px; ">
          
        <div class="navbar-header" style="height: 85px; background-color:#C02526; padding: 0px 0px 0px 0px;">
            <center><asp:Literal ID="litHdrImage" runat="server"></asp:Literal></center>
        </div>
          

        <div id="navbar" class="navbar-collapse collapse"  style="background-repeat: no-repeat; width: 100%;height: auto;" >
            
          
               <div class="navRightTop">
            <p><asp:Label ID="lblTime" runat="server" Text=""></asp:Label></p>
                   
<%--            <li><a href="NewEntry.aspx">Data Entry</a></li>
            <%--<li><a href="KnowledgeBase.aspx">Knowledge Base</a></li>
            <li><a href="rpt_ReportGen.aspx">Report</a></li>--%>
            <asp:Literal ID="litMenu" runat="server"></asp:Literal>
              </div>
            <!-- <li><a href="#">Help</a></li> -->
        </div>
      </div>
    </nav>
    <!--  End Navigation  -->

    <!--  Begin Dashboard Layout  -->
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 main">

            <%--<p style="color:#808080; font-size:large; margin-top: 70px; padding-left: 3px; margin-left: auto; margin-right:auto; width: 90%;">--%>
                         
            <div class="container" style=" margin-top: 70px; ">
                <div class="divLogon">
                    <asp:Literal ID="litMessage" runat="server"></asp:Literal>

                    <table class="table table-responsive table-bordered">
                        <tr>
                            <td>USER ID</td>
                            <td>
                                <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PASSWORD</td>
                            <td>
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="2">
                            <span style="display: block; height: 55px; width: 95px; margin: 0 auto;"><asp:Button ID="btnLogin" runat="server" Text="Sign On"  Height="50px" Width="90px" BackColor="#CC0000" ForeColor="White"   /></span>
                            </td></tr>
                    </table>
                </div>
            </div>
        </div>
      </div>
    </div>
        <div style="clear: both;">&nbsp;</div>
        <footer class="footer">
            <div class="containerFoot">
                <div style="text-align: center; color:#808080; font-size:small; padding: 10px" ><center>&copy; ITG BRANDS </center> <%--&nbsp;&nbsp;&bull;&nbsp;&nbsp;<a href="VersionNotes.aspx">Version Notes</a> &nbsp;&nbsp;&bull;&nbsp;&nbsp;<a href="ActivityReport.aspx">Activity</a>--%></div> 
                <asp:Literal ID="litFooter" runat="server"></asp:Literal>
            </div>
        </footer>
       

    </form>
    <style type="text/css">
        span.fontRed {
            color: red;
        }
        span.fontGreen {
            color: #2b7d00;
        }
        span.italic {
            font-style: italic;
        }
        span.bold {
            font-weight: bold;
        }
        span.marBot10 {
            margin-bottom: 10px;
        }

    </style>
</body>
</html>