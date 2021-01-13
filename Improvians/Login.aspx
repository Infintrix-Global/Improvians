<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Improvians.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
        <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <title>Login Page</title>

        <!-- Cross platform favicon setup & app themeing -->
        <link rel="apple-touch-icon" sizes="57x57" href="images/favicon/apple-icon-57x57.png"/>
        <link rel="apple-touch-icon" sizes="60x60" href="images/favicon/apple-icon-60x60.png"/>
        <link rel="apple-touch-icon" sizes="72x72" href="images/favicon/apple-icon-72x72.png"/>
        <link rel="apple-touch-icon" sizes="76x76" href="images/favicon/apple-icon-76x76.png"/>
        <link rel="apple-touch-icon" sizes="114x114" href="images/favicon/apple-icon-114x114.png"/>
        <link rel="apple-touch-icon" sizes="120x120" href="images/favicon/apple-icon-120x120.png"/>
        <link rel="apple-touch-icon" sizes="144x144" href="images/favicon/apple-icon-144x144.png"/>
        <link rel="apple-touch-icon" sizes="152x152" href="images/favicon/apple-icon-152x152.png"/>
        <link rel="apple-touch-icon" sizes="180x180" href="images/favicon/apple-icon-180x180.png"/>
        <link rel="icon" type="image/png" sizes="192x192"  href="images/favicon/android-icon-192x192.png"/>
        <link rel="icon" type="image/png" sizes="32x32" href="images/favicon/favicon-32x32.png"/>
        <link rel="icon" type="image/png" sizes="96x96" href="images/favicon/favicon-96x96.png"/>
        <link rel="icon" type="image/png" sizes="16x16" href="images/favicon/favicon-16x16.png"/>
        <link rel="manifest" href="manifest.json"/>
        <meta name="msapplication-TileColor" content="#ffffff"/>
        <meta name="msapplication-TileImage" content="images/favicon/ms-icon-144x144.png"/>
        <meta name="theme-color" content="#005a01"/>

        <!-- SEO tags -->
        <meta name="description" content="Growers Transplanting, Inc"/>

        <meta property="og:locale" content="en_US" />
        <meta property="og:type" content="website" />
        <meta property="og:title" content="Dashboard - Growers Transplanting, Inc" />
        <meta property="og:description" content="Growers Transplanting, Inc" />
        <meta property="og:url" content="/" />
        <meta property="og:site_name" content="Growers Transplanting" />
        <meta property="og:image" content="growers-transplanting.jpg" />
        <meta property="og:image:secure_url" content="growers-transplanting.jpg" />
        <meta property="og:image:width" content="1104" />
        <meta property="og:image:height" content="736" />

        <meta name="twitter:card" content="summary_large_image" />
        <meta name="twitter:description" content="Growers Transplanting, Inc" />
        <meta name="twitter:title" content="Dashboard - Growers Transplanting, Inc" />
        <meta name="twitter:site" content="@growerstransplanting" />
        <meta name="twitter:image" content="growers-transplanting.jpg" />
        <meta name="twitter:creator" content="@growerstransplanting" />

        <link rel="preload" href="fonts/fa/fa-solid-900.woff2"  type="font/woff2" />
        <link rel="preload" href="fonts/fa/fa-regular-400.woff2"  type="font/woff2" />
        <link rel="preload" href="fonts/fa/fa-brands-400.woff2"  type="font/woff2" />
        <link rel="preload" href="fonts/Roboto-Bold.woff2"  type="font/woff2" />
        <link rel="preload" href="fonts/Roboto-Medium.woff2"  type="font/woff2" />
        <link rel="preload" href="fonts/Roboto-Regular.woff2"  type="font/woff2" />

        <link rel="stylesheet" href="css/all.min.css"/>
        <link rel="stylesheet" href="css/bootstrap.min.css"/>
        <link rel="stylesheet" href="css/custom.css"/>
    </head>
    
<body class="cyan" >
  
  
            <img src="images/logo.png" alt="Growers Transplanting Inc"  align="center" />
        
  
     <div class="wrapper-content-sign-in background">
     <div class="container text-center">
         
  <%--  <div class="form-signin-heading text-white">
    <img src="~/images/login-logo.png" width="200" height="67" alt=""/></div>--%>
   <div id="login-page" class="row"   >
   <%-- <div class="col s4 z-depth-4 card-panel">--%>
       <div class="col s12 z-depth-4 card-panel">
      <form class="login-form" runat="server">
        <div class="row">
          <div class="input-field col s12 center">
              <h2 class="tex-black mb-4">Sign in</h2>
           <%-- <img src="images/login-logo.png" alt="" class="circle responsive-img valign profile-image-login">
            <p class="center login-form-text">Material Design Admin Template</p>--%>
          </div>
        </div>
             <asp:Label ID="lblmsg" runat="server" Font-Bold="True" 
	ForeColor="Red" Text=""></asp:Label>
        <div class="row margin">
          <div class="input-field col s12">
            <i class="mdi-social-person-outline prefix"></i>
            
                        <asp:TextBox ID="txtUserName" runat="server"  placeholder="Username"  class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                            SetFocusOnError="true" ErrorMessage="Please Enter User Name" ForeColor="Red"></asp:RequiredFieldValidator>
            <%--<label for="username" class="center-align">Username</label>--%>
          </div>
        </div>
        <div class="row margin">
          <div class="input-field col s12">
            <i class="mdi-action-lock-outline prefix"></i>
           <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" CssClass="form-control" runat="server"></asp:TextBox>
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                            SetFocusOnError="true" ErrorMessage="Please Enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
              
           <%-- <label for="password">Password</label>--%>
          </div>
        </div>

          
        
           <br />
        <div class="row">          
          <div class="input-field col s12 m12 l12  login-text">
              <input type="checkbox" id="CheckBoxRemember" runat="server"/>
              <label for="CheckBoxRemember">Remember me</label>
          </div>
        </div>
        <div class="row">
          <div class="input-field col s12">
              <asp:Button ID="btnlogin"  class="btn waves-effect waves-light col s12" runat="server" Text="Login" OnClick="btnlogin_Click"  />
           
          </div>
        </div>
           <div class="row">
         
          <div class="input-field col s12 m12 l12">
              <p class="margin right-align medium-small"><a href="ForgotPassword.aspx">Forgot password ?</a></p>
          </div>          
        </div>
        <%--<div class="row">
          <div class="input-field col s6 m6 l6">
            <p class="margin medium-small"><a href="page-register.html">Register Now!</a></p>
          </div>
          <div class="input-field col s6 m6 l6">
              <p class="margin right-align medium-small"><a href="page-forgot-password.html">Forgot password ?</a></p>
          </div>          
        </div>--%>

      </form>
    </div>
  </div>
      
  
 

     <%--  <footer class="footer-content row  justify-content-between align-items-center">
    <div class="col sm8"><div class="text">Designed by <a href="#" target="_blank" class="">Infintrix Global Pvt. Ltd.</a></div></div>
    <div class="col sm8 text-right"><span class="text-white">Copyright © 2019 - 2020 by <a href="">InfintrixGlobal Pvt. Ltd.</a> | All Rights Reserved</span></div>
  </footer>--%>
</div>
  </div>
    
    <!-- ================================================
    Scripts
    ================================================ -->

  
</body>
</html>
