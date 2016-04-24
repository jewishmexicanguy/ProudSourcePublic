<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPROC.aspx.cs" Inherits="ProudSource.ViewPROC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
    <link rel="Stylesheet" href="styles/ViewPROC.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
</head>
<body>
    <div id="body-container-div" class="body-container-div">
        <div id="navbar-container-div" class="navbar-container-div">
			<nav class="navbar navbar-default">
		      <div class="container-fluid">
		        <!-- Brand and toggle get grouped for better mobile display -->
		        <div class="navbar-header">
		      
		          <a class="navbar-brand" href="#">
		      	    <img alt="Brand" src="/ProudSource/images/ProudSourceLogoB.jpg" style="height:50px; width:50px; position:relative; top:-15px;" />
		          </a>
		        </div>

		        <!-- Collect the nav links, forms, and other content for toggling -->
		        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
		          <ul class="nav navbar-nav">
		            <li class="li-navbar-item"><a href="#">About</a></li>
		            <li class="li-navbar-item"><a href="/ProudSource/Contact.aspx">Contact</a></li>
		            <li class="li-navbar-item"><a href="#">Careers</a></li>
		          </ul>
		          <div class="navbar-form navbar-left" role="search">
		            <div class="form-group">
		              <input type="text" class="form-control" placeholder="Search" id="in-search-arg">
		            </div>
		            <button type="submit" class="btn btn-default" id="btn_search_arg" onclick="window.location.replace('/ProudSource/Search/' + document.getElementById('in-search-arg').value);">Submit</button>
		          </div>
		          <ul class="nav navbar-nav navbar-right">
                    <li>
                        <a href="/ProudSource/UserLogout.aspx">
                            <span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>
                        </a> 
                    </li>
		            <li>
		        	    <a href="/ProudSource/UserLogin.aspx">
		        		    <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
		        	    </a>
		            </li>
		          </ul>
		        </div><!-- /.navbar-collapse -->
		      </div><!-- /.container-fluid -->
		    </nav>
	      </div>
        <form id="form1" runat="server">
            <div id="PROC-details" class="container">
                <ul class="list-group">
                    <li class="list-group-item">
                        <asp:Label ID="Label1" runat="server" Text="Begin Date"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_BeginDate" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_EndDate" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label3" runat="server" Text="Profit Margin %"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_PROC_Percentage" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label4" runat="server" Text="Amount Invested"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_AmountInvested" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item-danger">
                        <asp:Label ID="Label5" runat="server" Text="Active"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_PROC_Active" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>
            </div>
            <div id="Investor-details" class="container">
                <ul class="list-group">
                    <li class="list-group-item">
                        <asp:Label ID="Label7" runat="server" Text="Investor Name"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_InvestorName" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label6" runat="server" Text="Investor"></asp:Label>
                        <br />
                        <asp:Image ID="img_Investor" runat="server" />
                    </li>
                    <li class="list-group-item-danger">
                        <asp:Label ID="Label8" runat="server" Text="Investor Accepted"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_InvestorAccepted" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>
            </div>
            <div id="Project-details" class="container">
                <ul class="list-group-item">
                    <li class="list-group-item">
                        <asp:Label ID="Label9" runat="server" Text="Project Description"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_ProjectDescription" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item-danger">
                        <asp:Label ID="Label10" runat="server" Text="Project Accepted PROC"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_ProjectAccepted" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>
                <asp:Label ID="lbl_Project_AcceptPROC" runat="server" Text="Accept Proc" Visible="False"></asp:Label>
                <br />
                <asp:Button ID="Btn_Project_AcceptPROC" runat="server" Text="Accept" Visible="False" OnClick="Btn_Project_AcceptPROC_Click" />
            </div>
        </form>
    </div>
</body>
</html>
