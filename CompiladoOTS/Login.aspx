<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="materialDesing.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adm. Soportes | Skytex México</title>
    <!-- asignacion de icono de barra de navegador -->
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/skytex.ico") %>" />
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <link href="<%=ResolveUrl("css/materialize.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/style.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/materialize.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/init.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/WebNotifications.js") %>"></script>
    <link href="<%=ResolveUrl("css/new-site-engin.min.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="<%=ResolveUrl("css/font-awesome.min.css") %>" type="text/css" rel="stylesheet" media="screen,projection" />
    <script type="text/javascript" src="<%= ResolveUrl("js/new-site-engine.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/va-3b908fd458038f9c59b8a4a8015fee01.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/opa-faabfb86dfeea8d274c346c2627cf53e.js") %>"></script>
</head>
<script type="text/javascript">
    function showNotification() {
        var notif = showWebNotification('On Time System', 'Tienes una nueva asignacion!\n Por favor revise el listado de tareas asignadas.', '/Media/logoSkytex.png', null, 3000);
        //handle different events
        setTimeout("location.href='Inicio.aspx'", 200);
        //window.location.href = 'Inicio.aspx';
        notif.addEventListener("show", Notification_OnEvent);
        notif.addEventListener("click", Notification_OnEvent);
        notif.addEventListener("close", Notification_OnEvent);
        n++;

    }

    $(function () {
        $("#error").hide();
        var isShiftPressed = false;
        var isCapsOn = null;
        $("#password").bind("keydown", function (e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            if (keyCode == 16) {
                isShiftPressed = true;
            }
        });
        $("#password").bind("keyup", function (e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            if (keyCode == 16) {
                isShiftPressed = false;
            }
            if (keyCode == 20) {
                if (isCapsOn == true) {
                    isCapsOn = false;
                    $("#error").hide();
                } else if (isCapsOn == false) {
                    isCapsOn = true;
                    $("#error").show();
                }
            }
        });
        $("#password").bind("keypress", function (e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            if (keyCode >= 65 && keyCode <= 90 && !isShiftPressed) {
                isCapsOn = true;
                $("#error").show();
            } else {
                $("#error").hide();
            }
        });
    });
</script>
<body>
    <div class="navbar-fixed">
        <nav>
            <div class="nav-wrapper container">
                <a id="logo-container" href="Login.aspx" class="brand-logo">
                    <img src="Media/logoSkytexB.png" width="25" height="30" /></a>
                <ul class="right hide-on-med-and-down">
                    <li class="active"><a href="Login.aspx"><span class="white-text text-darken-2"><i class="material-icons left">perm_identity</i>Login</span></a></li>
                </ul>

                <ul id="nav-mobile" class="side-nav">
                    <li class="active"><a href="Login.aspx"><span class="black-text text-darken-2">Login</span></a></li>
                </ul>
                <a href="#" data-activates="nav-mobile" class="button-collapse"><i class="material-icons">menu</i></a>
            </div>
        </nav>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>
    <style type="text/css">
        .minusculas {
            text-transform: lowercase;
        }

        .mayusculas {
            text-transform: uppercase;
        }
    </style>

    <form id="form1" runat="server">
        <div class="container">
            <div class="section card-panel hoverable">

                <div class="row">
                    <div class="col s12 m4">
                        <img class="left responsive-img" src="<%=ResolveUrl("~/Media/skytex.png") %>" />
                    </div>
                    <div class="col s12 m7">
                        <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="60%" height="10%" />
                    </div>
                </div>
                <div class="row">
                    <div class="row">
                        <div class="col s12 m5">
                            <br />
                        </div>
                        <div class="col s12 m6">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />
                        </div>
                    </div>
                    <div class="col s12 m5">
                        <div class="center">
                            <img class="responsive-img" src="<%=ResolveUrl("~/Media/login1.png") %>" width="200" />
                        </div>
                    </div>
                    <div class="col s12 m6">
                        <div class="row">
                            <div class="input-field col s8">
                                <i class="material-icons prefix">account_circle</i>
                                <label for="claveUser">Clave de usuario:</label>
                                <br /><br />
                                <asp:DropDownList ID="claveUser" runat="server" CssClass="browser-default"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s8">
                                <i class="material-icons prefix">https</i>
                                <input id="password" name="password" type="password" /><div id="error" class="chip amber darken-3 white-text">El bloqueo de mayusculas esta activado. sdfasfdasdf</div>
                                <label for="password">Contraseña:</label>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Button ID="Button1" name="btn_Enviar" OnClick="ValidateUser" runat="server" Text="Entrar" class="btn-large waves-effect waves-light green darken-2" />
                        </div>
                    </div>

                </div>
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
            </div>
        </div>
        <div class="footer-copyright">
            <div class="container">
                © <%= DateTime.Now.ToString("yyyy") %> Copyright
            </div>
        </div>
    </footer>
    <script>
        $(document).ready(function () {
            var toggle = -1;
            $('.slider').slider({ full_width: true, indicators: false, transition: 1300, interval: 5000 });
            $('#sidebar-nav').sidr();
            $('body').on('click', '.all-body-click-region', function () {
                //$('.toc').fadeIn();
                $.sidr('close');
                $('.cover__container').css({ 'display': 'block' })
            });
            $('.side-nav-button').click(function () {
                toggle *= -1;
                if (toggle === 1) {
                    $('.cover__container').css({ 'display': 'none' })
                }
                else {
                    $('.cover__container').css({ 'display': 'block' })
                }
            });

            setTimeout(function () {
                var firstSliderImage = document.getElementById('slide-image-1');
                firstSliderImage.src = "/media/img/main__site_1.jpg";
                firstSliderImage.onload = function () {
                    $('.all-body-click-region.inner__wrapper').css({ 'display': 'block' });
                    $('.loader').css({ 'display': 'none' })
                };
            }, 100);
        });
    </script>
</body>
</html>
