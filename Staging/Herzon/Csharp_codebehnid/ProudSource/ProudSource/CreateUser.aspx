<%@ Page Language="C#" Inherits="ProudSource.CreateUser" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>CreateUser</title>
	<link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/CreateUser.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
	<script type="text/javascript">
	    function UserCreated(arg, context) {
	        if (arg == 'Your User account has been created') {
	            document.getElementById('lbl-Register-Status').innerHTML = arg;
	            window.location.replace("UserLogin.aspx");
	        } else {
	            document.getElementById('lbl-Register-Status').innerHTML = arg;
	        }
		}
	</script>
</head>
<body>
	<div id="body-container-div" class="body-container-div">
		<div id="create-user-container-div" class="container">
			<form id="form1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Email, Username and Password must all be larger than 10 charcters long and shorter than 255 characters long."></asp:Label>
				<h2><span class="glyphicon glyphicon-plus"></span> Create User</h2>
				<h4>
					<span class="glyphicon glyphicon-envelope" aria-hidden="true"></span>
					Email
				</h4>
				<input id="in-email" type="text" />
				<br />
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
				<input id="btn-createuser" value="Create" type="button" onclick="SubmitUser(JSON.stringify({ 'action': 'CreateUser', 'UserName': document.getElementById('in-username').value, 'E-mail': document.getElementById('in-email').value, 'Password': document.getElementById('in-password').value}));" />
				<br />
				<label id="lbl-Register-Status" ></label>
				<br />
				<p>Have an account already <a href="UserLogin.aspx">login here</a>.</p>
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

