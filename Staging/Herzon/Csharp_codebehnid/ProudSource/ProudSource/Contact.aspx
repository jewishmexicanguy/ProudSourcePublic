<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ProudSource.Contact" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Contact us</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" />
    <script type="text/javascript" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script>
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
        <div id="intro-container-div" class="intro-container-div">
            <div class="container">
                <h3>Contact Us</h3>
                <p class="text-left">
                    We're happy to assist with any question or concerns you may have. Please fill the below boxes or email Proudsource at proudsource@proudsource.us.
                </p>
            
                <form id="form1" runat="server">
                    <asp:Label ID="Label5" runat="server" Text="Fill out all fields or we wont get your message"></asp:Label>
                    <ul class="list-group">
                        <li class="list-group-item">
                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="Tbox_Name" runat="server" Width="227px"></asp:TextBox>
                        </li>               
                        <li class="list-group-item">       
                            <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                            <br />               
                            <asp:TextBox ID="Tbox_Email" runat="server" Width="226px"></asp:TextBox>
                        </li>
                        <li class="list-group-item">  
                            <asp:Label ID="Label2" runat="server" Text="Subject"></asp:Label>
                            <br />       
                            <asp:TextBox ID="TBox_Subject" runat="server" Width="225px"></asp:TextBox>
                        </li>
                        <li class="list-group-item">
                            <asp:Label ID="Label1" runat="server" Text="Message"></asp:Label>
                            <br />
                            <asp:TextBox ID="TBox_Message" runat="server" Height="143px" TextMode="MultiLine" Width="507px"></asp:TextBox>
                        </li>
                    </ul>
                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click" />
            </form>
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
