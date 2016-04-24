<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ProudSource.UserLogin" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>UserLogin</title>
	<link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/UserLogin.css" />
	<script type="text/javascript">
	    function HandleLogin(arg, context) {
	        if (arg == 'Authentication Succesfull') {
	            document.getElementById('lbl-Login-Status').innerHTML = arg;
	            window.location.replace("UserDashboard.aspx");
	        }
			document.getElementById('lbl-Login-Status').innerHTML = arg;
		}
	</script>
</head>
<body>
	<div id="body-container-div" class="body-container-div">
		<div id="login-container-div" class="container">
			<form id="form1" runat="server">
				<h2><span class="glyphicon glyphicon-log-in"></span> Login</h2>
				<h4>
					<span class="glyphicon glyphicon-user" aria-hidden="true"></span> 
					User Name
				</h4>
				<input id="in-username" type="text"/>
				<br />
				<h4>
					<span class="entypo-key inputUserIcon" aria-hidden="true"></span> 
					Password
				</h4>
				<input id="in-password" type="text"/>
				<br />
				<input id="btn-login" value="Login" type="button" onclick="UserLogin(JSON.stringify({ 'action': 'login', 'UserName': document.getElementById('in-username').value, 'Password': document.getElementById('in-password').value}));" />
				<br />
				<label id="lbl-Login-Status" ></label>
				<br />
				<p>Don't have an account <a href="CreateUser.aspx">create a user here</a>.</p>
			</form>
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