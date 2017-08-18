<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="valPas.aspx.cs" Inherits="materialDesing.valPas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validacion de Cambio de Contraseña | OTS</title>
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
    <div class="container">
        <div class="col s12 card-panel grey lighten-4 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Alta de Detalle del OTS</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <div class="row">
                <div class="col s12">
                    contraseña cambiada
                </div>
            </div>   
        </div>
    </div>
    </form>
</body>
</html>
