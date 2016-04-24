<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePROC.aspx.cs" Inherits="ProudSource.CreatePROC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create PROC</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/CreatePROC.css" />
</head>
<body>
    <div id="body-container-div" class="body-container-div">
        <div id="navbar-container-div" class="navbar-container-div">
			<nav class="navbar navbar-default">
		  <div class="container-fluid">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-header">
		      
		      <a class="navbar-brand" href="#">
		      	<img alt="Brand" src="/images/ProudSourceLogoB.jpg" style="height:50px; width:50px; position:relative; top:-15px;" />
		      </a>
		    </div>

		    <!-- Collect the nav links, forms, and other content for toggling -->
		    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
		      <ul class="nav navbar-nav">
		        <li class="li-navbar-item"><a href="#">About</a></li>
		        <li class="li-navbar-item"><a href="Contact.aspx">Contact</a></li>
		        <li class="li-navbar-item"><a href="#">Careers</a></li>
		      </ul>
		      <div class="navbar-form navbar-left" role="search">
		        <div class="form-group">
		          <input type="text" class="form-control" placeholder="Search" id="in-search-arg">
		        </div>
		        <button type="submit" class="btn btn-default" id="btn_search_arg" onclick="window.location.replace('Search/' + document.getElementById('in-search-arg').value);">Submit</button>
		      </div>
		      <ul class="nav navbar-nav navbar-right">
		        <li>
		        	<a href="UserLogout.aspx">
                        <span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>
                    </a> 
		        </li>
		      </ul>
		    </div><!-- /.navbar-collapse -->
		  </div><!-- /.container-fluid -->
		</nav>
	  </div>
        <form id="form1" runat="server">
            <div id="form1-container" class="container">
                <asp:Label ID="Label4" runat="server" Text="All elements must be filled or no PROC will be created on submition."></asp:Label>
                <ul class="list-group">
                    <li class="list-group-item">
                        <asp:Label ID="Label1" runat="server" Text="Net % Requested"></asp:Label>
                        <br />
                        <asp:TextBox ID="Tbox_PROC_Percent" runat="server" TextMode="Number"></asp:TextBox>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label5" runat="server" Text="Amount you wish to invest"></asp:Label>
                        <br />
                        <asp:TextBox ID="Tbox_PROC_investment" runat="server" TextMode="Number"></asp:TextBox>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label2" runat="server" Text="Begin Date"></asp:Label>
                        <br />
                        <asp:Calendar ID="Cal_BeginDate" runat="server"></asp:Calendar>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                        <br />
                        <asp:Calendar ID="Cal_EndDate" runat="server"></asp:Calendar>
                    </li>
                </ul>
                <asp:Button ID="Btn_CreatePROC" runat="server" Text="Create PROC" OnClick="Btn_CreatePROC_Click" />
            </div>
        </form>
    </div>
</body>
</html>
