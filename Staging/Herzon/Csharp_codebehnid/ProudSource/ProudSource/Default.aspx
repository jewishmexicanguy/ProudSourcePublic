<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProudSource.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
	<title>ProudSource</title>
	<link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/Default.css" />
</head>
<body>
	<div id="body-container-div" class="body-container-div">
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
		      <div class="navbar-form navbar-left" >
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
		<div id="intro-container-div" class="intro-container-div">
			<div class="container">
				<h3>What is ProudSource?</h3>
				<p class="text-left">
					ProudSource.us is a platform used to connect investors and entrepreneurs with small businesses and startups. Essentially, it's crowdfunding with a financial return. However unlike other financial instruments like traditional loans and equity. We will be using financial instruments based on PROC: Progressive Return on Capital. It is a security that guarantees investors a share of revenue, discounted for costs, for a definite period of time. All trades will eventually be able to be made using all national currencies, cryptocurrencies and eventually precious metals.
				</p>
				<h3>Vision</h3>
				<p class="text-left">
					ProudSource.us aims to take what the internet age has inherited, the cloud and enshrine finance within it. Our team works around the clock to decentralize the most corrupt and inefficient institutions in the modern economy: the banks. By using tools with less transaction costs, we can pass the savings onto our customers, allowing anyone to invest or build a business. In essence, we will take the sharing economy to its radical conclusion, the sharing and accumulation of capital on a global scale.
				</p>
			</div>
		</div>
		<div id="media-container-div" class="media-container-div">
			<p>You can find out more about Proudsource in these places</p>
			<ul class="social">
				<li><a href="https://twitter.com/proudsource" target="=_blank">Twitter</a></li>
				<li><a href="https://www.facebook.com/ProudsourceMe" target="_blank">Facebook</a></li>
			</ul>
		</div>
	</div>
	<div id="footer" class="modal-footer">
		<div class="container">
			<p class="text-muted">ProudSource LLC 2015</p>
			<span class="text-muted">All Rights Reserved</span>
		</div>
	</div>
</body>
</html>