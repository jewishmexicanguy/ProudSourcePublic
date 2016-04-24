<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProject.aspx.cs" Inherits="ProudSource.ViewProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Account</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
    <link rel="Stylesheet" href="styles/ViewProject.css" />
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
            <div id="form1-contents-investor" class="container">
                <ul class="list-group">
                    <li class="list-group-item">
                        <asp:Label ID="Label3" runat="server" Text="Project Description"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_ProjectDescription" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <asp:Label ID="Label1" runat="server" Text="Investment Goals"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_InvestmentGoal" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>
                <asp:Label ID="lbl_InvestorCreatePROC" runat="server" Text="Create a new PROC" Visible="False"></asp:Label>
                <%=InvestorHTML %>
                <br />
            </div>
            <%=EntrepreneurOpenDiv %>
                <ul class="list-group">
                    <%=EntrepreneurHTML[0] %>
                        <asp:Label ID="lbl_OwnerDescription" runat="server" Text="Project Description" Visible="False"></asp:Label>
                    <%=EntrepreneurHTML[1] %>
                    <%=EntrepreneurHTML[2] %>
                        <asp:TextBox ID="Tbox_ProjectDescription" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
                    <%=EntrepreneurHTML[3] %>
                    <%=EntrepreneurHTML[4] %>
                        <asp:Label ID="lbl_OwnerInvestmentGoal" runat="server" Text="Project Investment Goals" Visible="False"></asp:Label>
                    <%=EntrepreneurHTML[5] %>
                    <%=EntrepreneurHTML[6] %>
                        <asp:TextBox ID="Tbox_ProjectInvestmentGoal" runat="server" Visible="False" TextMode="Number"></asp:TextBox>
                    <%=EntrepreneurHTML[7] %>
                </ul>
                <br />
                <asp:Button ID="Btn_ProjectUpdate" runat="server" Text="Update" Visible="False" OnClick="Btn_ProjectUpdate_Click" />
            <%=EntrepreneurCloseDiv %>
        </form>
    </div>
</body>
</html>
