<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="C_imgOTS.aspx.cs" Inherits="materialDesing.C_imgOTS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Capturas OTS | Skytex México</title>
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
    <%--<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.0/animate.min.css"/>

    <link rel="stylesheet" href="<=ResolveUrl("css/normaliz.min.css") >"/>
    <link rel="stylesheet" href="<=ResolveUrl("css/animate.min.css") >"/>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.carousel').carousel();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar-fixed">
            <nav>
                <div class="nav-wrapper container">
                    <a id="logo-container" href="Inicio.aspx" class="brand-logo">
                        <img src="Media/logoSkytexB.png" width="25" height="30" /></a>
                    <ul class="right hide-on-med-and-down">
                        <li class="active">
                            <asp:LinkButton ID="cerrarSession1" runat="server" OnClick="cerrarSession1_Click">
                                    <span class="white-text text-darken-2">
                                        <i class="material-icons right">power_settings_new</i>Logout
                                    </span>
                            </asp:LinkButton>
                        </li>
                    </ul>

                </div>
            </nav>
        </div>
        <div class="container">
            <div class="col s12 card-panel grey lighten-5 z-depth-1">
                <div class="row">
                    <h4 class="center grey-text">Capturas del OTS</h4>
                </div>
                <div class="row">
                    <div class="col s12 center">
                        <h5>Descripcion: <strong>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong></h5>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="responsive-table2" EmptyDataText="No hay resultados">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src='<%# ResolveUrl(Eval("ImageUrl").ToString()) %>' class="materialboxed center" width="500" height="500" alt='<%# ResolveUrl(Eval("ImageName").ToString()) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Button ID="soportes" runat="server" Text="Ir a soportes" OnClick="soportes_Click" CssClass="waves-effect waves-light btn  green darken-4" />
            </div>
        </div>
    </form>
    <footer class="page-footer">
        <div class="container">
            <div class="row">
                <div class="col l6 s12">
                    <h5 class="white-text">Skytex México S.A de C.V.</h5>
                    <a href="http://www.skytex.com.mx/">Skytex México</a>
                    <p class="grey-text text-lighten-4">Sistemas - Desarrollo</p>
                </div>
                <!--<input type="button" value="Show sample notification" onclick="showNotification();" />-->
            </div>
        </div>
        <!--<div class="flotante">
            <div id="cronometro">
                <div class="col s12 m3">
                    <div class="">
	                    <div id="vis" style="float: left; font-size:30px">00:00:00:00</div>
                    </div>
                </div>
	            <div id="button_container" style="display: inline; margin-left: 10px;">
		            <button id="avvia" onclick="javascript:avvia();" class="waves-effect waves-light btn  green darken-4">Inicia</button>
		            <button id="stop" onclick="javascript:ferma();" class="waves-effect waves-light btn  red darken-4">Detener</button>
		            <button id="azzera" onclick="javascript:azzera();" class="waves-effect waves-light btn  amber darken-4">Reiniciar</button>
	            </div>
            </div>
        </div>-->
        <div class="footer-copyright">
            <div class="container">
                © <%= DateTime.Now.ToString("yyyy") %> Copyright
            </div>
        </div>
    </footer>
</body>
</html>
