<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateInvestor.aspx.cs" Inherits="ProudSource.CreateInvestor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CreateInvestor</title>
	<link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/CreateUser.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
    <script type="text/javascript">
        function InvestotrCreated(arg, context) {
            if (arg == 'Investor Profile Created') {
                document.getElementById('lbl-profilestatus').innerHTML = arg;
                // wait one second and then
                window.location.replace("UserDashboard.aspx");
            } else {
                document.getElementById('lbl-profilestatus').innerHTML = arg;
            }
        }
    </script>
</head>
<body>
    <div>
        <div id="navbar-container-div" class="navbar-container-div">
			<nav class="navbar navbar-default">
		        <div class="container-fluid">
		        <!-- Brand and toggle get grouped for better mobile display -->
		        <div class="navbar-header">
		          <a class="navbar-brand" href="#">
		      	    <img alt="Brand" src="images/ProudSourceLogoB.jpg" style="height:50px; width:50px; position:relative; top:-15px;">
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
		        	    <a href="UserLogin.aspx">
		        		    <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
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
                    <h2><span class="glyphicon glyphicon-plus"></span> Create Investor</h2>
				    <h4>
					    <span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>
					    Investor Profile Name
				    </h4>
                    <input id="in-investorName" type="text" />
                    <br />
                    <input id="btn-createinvestor" value="Create" type="button" onclick="CreateInvestor(JSON.stringify({ 'action': 'CreateInvestor', 'InvestorProfile': document.getElementById('in-investorName').value }));" />
				    <br />
				    <label id="lbl-profilestatus" ></label>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
