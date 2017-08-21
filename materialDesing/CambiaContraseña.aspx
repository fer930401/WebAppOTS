<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiaContraseña.aspx.cs" Inherits="materialDesing.CambiaContraseña" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambia Contraseña | OTS</title>
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/skytex.ico") %>" />
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <link href="<%=ResolveUrl("css/materialize.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/style.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.1.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/materialize.js") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("js/highcharts.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/grid-light.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/WebNotifications.js") %>"></script>
    <link href="<%=ResolveUrl("css/pickadate.01.default.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/rtl.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/default.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/default.date.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.0/animate.min.css" />

    <link rel="stylesheet" href="<%=ResolveUrl("css/normaliz.min.css") %>" />
    <link rel="stylesheet" href="<%=ResolveUrl("css/animate.min.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar-fixed">
            <nav>
                <div class="nav-wrapper container">
                    <a></a>
                    <a id="logo-container" href="#" data-activates="mobile-demo" class="brand-logo "><img src="Media/logoSkytexB.png" width="25" height="30" /></a>
                    
                    <ul class="side-nav" id="mobile-demo">
                        <li>.</li>
                        <li><a class="light-blue-text text-darken-4 tooltipped" href="Inicio.aspx" data-position="right" data-delay="50" data-tooltip="Regresa al inicio"><i class="material-icons right">store</i>Inicio</a></li>
                    </ul>
                </div>
            </nav>
        </div> 
        <div class="container">
            <div class="col s12 card-panel grey lighten-5 z-depth-1">
                <div class="row">
                    <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
                    <br />
                    <h4 class="center grey-text">Cambio de Contraseña</h4>
                </div>
                <div class="row">
                    <div class="col s3">
                        <span class="white-text">.</span>
                    </div>
                    <div class="col s6">
                        <div class="row">
                            <label>Ingresa tu correo:</label>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" required></asp:TextBox>
                        </div>
                        <div class="row">
                            <label>Confirma tu correo:</label>
                            <asp:TextBox ID="txtEmailConfirm" runat="server" TextMode="Email" onpaste="return false" required></asp:TextBox>
                        </div>
                        <div class="row">
                            <label>Nueva Contraseña:</label>
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" onpaste="return false" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s4 right">
                        <asp:Button ID="btnCambiarPass" runat="server" Text="Guardar" class="green darken-4 btn" OnClick="btnCambiarPass_Click"/>
                        <a class="red darken-4 btn" href="Login.aspx">Cancelar</a>
                    </div>
                    <div class="col s3">
                        <span class="white-text">.</span>
                    </div>
                </div>
            </div> 
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $('select').material_select();
            $(".brand-logo").sideNav();
        });
    </script>
</body>
</html>
