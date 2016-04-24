<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ProudSource.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
	<script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
	<link rel="Stylesheet" href="styles/styles.css" />
	<link rel="Stylesheet" href="styles/Search.css" />
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
            <div>
    
            </div>
        </form>
        <div id="Search-results-div" class="container">
            <div id="Entrepreneur-results">
                <%=EntrepreneurResults %>
            </div>
            <div id="ProjectResults">
                <%=ProjectResults %>
            </div>
        </div>
    </div>
</body>
</html>
