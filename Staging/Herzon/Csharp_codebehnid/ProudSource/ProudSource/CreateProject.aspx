<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="ProudSource.CreateProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Project</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/CreateProject.css" />
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
        <div id="form1-container" class="container">
            <form id="form1" runat="server">
                <div>
                    <h2><span class="glyphicon glyphicon-plus"></span> Create Project</h2>
                    <h4>
				        <span class="glyphicon glyphicon-book" aria-hidden="true"></span>
					    Project Name
				    </h4>
                    <asp:TextBox ID="Tbox_ProjectDescription" runat="server"></asp:TextBox>
                    <br />
                    <h4>Investment goal</h4>
                    <asp:TextBox ID="Tbox_InvestmentGoal" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
                </div>
            </form>
        </div>
     </div>
</body>
</html>
