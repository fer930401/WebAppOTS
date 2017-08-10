﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat_OTS.aspx.cs" Inherits="materialDesing.Chat_OTS1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chat OTS</title>
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
    <!-- Cambios SignalR -->
    <link rel="stylesheet" href="<%=ResolveUrl("css/SignalRcss/ChatStyle.css") %>" />
    <link rel="stylesheet" href="<%=ResolveUrl("css/SignalRcss/JQueryUI/themes/base/jquery.ui.all.css") %>" />

      <!--Reference the jQuery library. -->
    

    <script src="<%=ResolveUrl("js/SignalRscripts/ui/jquery.ui.core.js") %>"></script>
    <script src="<%=ResolveUrl("js/SignalRscripts/ui/jquery.ui.widget.js") %>"></script>
    <script src="<%=ResolveUrl("js/SignalRscripts/ui/jquery.ui.mouse.js") %>"></script>
    <script src="<%=ResolveUrl("js/SignalRscripts/ui/jquery.ui.draggable.js") %>"></script>
    <script src="<%=ResolveUrl("js/SignalRscripts/ui/jquery.ui.resizable.js") %>"></script>

    <!--Reference the SignalR library. -->
    <script src="<%=ResolveUrl("js/SignalRscripts/jquery.signalR-2.2.0.min.js") %>"></script>
    
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="<%=ResolveUrl("signalr/hubs") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <script type="text/javascript">
             $(document).ready(function () {
                 $('select').material_select();
             });
             $(document).ready(function () {
                 $(window).scroll(function () {
                     $('.flotante').slideDown(300);
                 });
             });
    </script>
    <script type="text/javascript">
        $(function () {
            clearInterval(refreshId);
            setScreen(false);

            // Declare a proxy to reference the hub.
            var chatHub = $.connection.chatHub;
            $.connection.hub.logging = true;

            registerClientMethods(chatHub);

            // Start Hub
            $.connection.hub.start().done(function () {
                registerEvents(chatHub)
            });
        });

        // ------------------------------------------------------------------Variable ----------------------------------------------------------------------//
        var loadMesgCount = 10;
        var topPosition = 0;
        var refreshId = null;

        function scrollTop(ctrId) {
            var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
            $('#' + ctrId).find('#divMessage').scrollTop(height);
        }

        // ------------------------------------------------------------------Start All Chat ----------------------------------------------------------------------//
        function setScreen(isLogin) {
            if (!isLogin) {
                $("#divChat").hide();
            }
            else {
                $("#divChat").show();
            }
        }

        function registerEvents(chatHub) {
            $("#btnStartChat").click(function () {
                var user_cve = $("#<%= txtUser_cve.ClientID %>").val();
                var user = $("#<%= txtUser.ClientID %>").val();
                var email = $("#<%= txtEmail.ClientID %>").val();
                if (user_cve.length > 0 && user.length > 0 && email.length > 0) {
                    $('#hdUser_cve').val(user_cve);
                    $('#hdUser').val(user);
                    $('#hdEmail').val(email);
                    chatHub.server.connect(user_cve, user, email);
                }
                else {
                    alert("Por favor capture los datos solicitados");
                }
            });

            $("#<%= txtUser_cve.ClientID %>").keypress(function (e) {
                if (e.which == 13) {
                    $("#btnStartChat").click();
                }
            });
            $("#<%= txtUser.ClientID %>").keypress(function (e) {
                if (e.which == 13) {
                    $("#btnStartChat").click();
                }
            });
            $("#<%= txtEmail.ClientID %>").keypress(function (e) {
                if (e.which == 13) {
                    $("#btnStartChat").click();
                }
            });
            /* cuando dan enter en el textbox del chat grupal */
            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    $('#btnSendMsg').click();
                }
            });

            $('#btnSendMsg').click(function () {
                var msg = $("#txtMessage").val().trim();
                /* validacion para que la variable mensaje contenga algun valor */
                if (msg !== "") {
                    var user_cve = $('#hdUser_cve').val();
                    chatHub.server.sendMessageToAll(user_cve, msg);
                    $("#txtMessage").val('');
                }
            });

            $(document).ready(function () {
                $('#txtMessage').keypress(function (e) {
                    if (e.which == 13) {
                        var msg = $("#txtMessage").val().trim();
                        /* validacion para que la variable mensaje contenga algun valor */
                        if (msg !== "") {
                            var user_cve = $('#hdUser_cve').val();
                            chatHub.server.sendMessageToAll(user_cve, msg);
                            $("#txtMessage").val('');
                        }
                    }
                });

            });
        }

        function registerClientMethods(chatHub) {
            // Calls when user successfully logged in
            chatHub.client.onConnected = function (id, user_cve, allUsers, messages) {
                setScreen(true);

                $('#hdId').val(id);
                $('#hdUser_cve').val(user_cve);
                $('#spanUser').html(user_cve);

                // Add All Users
                for (i = 0; i < allUsers.length; i++) {
                    AddUser(chatHub, allUsers[i].connection_id, allUsers[i].user_cve, allUsers[i].user_nom, allUsers[i].mail);
                }

                // Add Existing Messages
                for (i = 0; i < messages.length; i++) {
                    AddMessage(messages[i].user_cve, messages[i].mensaje);
                }

                $('.login').css('display', 'none');
            }

            // On New User Connected
            chatHub.client.onNewUserConnected = function (id, name, email) {
                AddUser(chatHub, id, name, email);
            }

            // On User Disconnected
            chatHub.client.onUserDisconnected = function (id, userName) {
                $('#' + id).remove();

                var ctrId = 'private_' + id;
                $('#' + ctrId).remove();

                var disc = $('<div class="disconnect">"' + userName + '" se desconecto.</div>');

                $(disc).hide();
                $('#divusers').prepend(disc);
                $(disc).fadeIn(200).delay(2000).fadeOut(200);
            }

            // On User Disconnected Existing
            chatHub.client.onUserDisconnectedExisting = function (id, userName) {
                $('#' + id).remove();
                var ctrId = 'private_' + id;
                $('#' + ctrId).remove();
            }

            chatHub.client.messageReceived = function (userName, message) {
                AddMessage(userName, message);
            }

            chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userEmail, email, status, fromUserId) {
                var ctrId = 'private_' + windowId;
                if (status == 'Click') {
                    if ($('#' + ctrId).length == 0) {
                        createPrivateChatWindow(chatHub, windowId, ctrId, fromUserName, userEmail, email);
                        chatHub.server.getPrivateMessage(userEmail, email, loadMesgCount).done(function (msg) {
                            for (i = 0; i < msg.length; i++) {
                                $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');
                                // set scrollbar
                                scrollTop(ctrId);
                            }
                        });
                    }
                    else {
                        $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message + '</div>');
                        // set scrollbar
                        scrollTop(ctrId);
                    }
                }

                if (status == 'Type') {
                    if (fromUserId == windowId)
                        $('#' + ctrId).find('#msgTypeingName').text('escribiendo...');
                }
                else { $('#' + ctrId).find('#msgTypeingName').text(''); }
            }
        }

        // Add User
        function AddUser(chatHub, id, cve, name, email) {
            var user_cve = $('#hdUser_cve').val();
            var userEmail = $('#hdEmail').val();
            var code = "";

            if (userEmail == email && $('.loginUser').length == 0) {
                code = $('<div class="loginUser"><ul id="lista3"><li>' + name + "</li></ul></div>");
            }
            else {
                code = $('<a id="' + id + '" class="user" ><ul id="lista3"><li>' + name + '</li></ul><a>');
                $(code).click(function () {
                    var id = $(this).attr('id');
                    if (userEmail != email) {
                        OpenPrivateChatWindow(chatHub, id, name, userEmail, email);
                    }
                });
            }

            $("#divusers").append(code);
        }

        // Add Message
        function AddMessage(userName, message) {
            $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');

            var height = $('#divChatWindow')[0].scrollHeight;
            $('#divChatWindow').scrollTop(height);
        }
        // ------------------------------------------------------------------End All Chat ----------------------------------------------------------------------//


        // ------------------------------------------------------------------Start Private Chat ----------------------------------------------------------------------//
        function OpenPrivateChatWindow(chatHub, id, userName, userEmail, email) {
            var ctrId = 'private_' + id;
            if ($('#' + ctrId).length > 0) return;

            createPrivateChatWindow(chatHub, id, ctrId, userName, userEmail, email);

            chatHub.server.getPrivateMessage(userEmail, email, loadMesgCount).done(function (msg) {
                for (i = 0; i < msg.length; i++) {
                    $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');
                    // set scrollbar
                    scrollTop(ctrId);
                }
            });
        }

        function createPrivateChatWindow(chatHub, userId, ctrId, userName, userEmail, email) {

            var div = '<div id="' + ctrId + '" class="ui-widget-content draggable z-depth-4" rel="0">' +
                        '<div class="header">' +
                            '<div  style="float:right; padding:2px;">' +
                                '<img id="imgDelete"  style="cursor:pointer;" src="<%=ResolveUrl("~/Media/cerrar.png")%>" />' +
                            '</div>' +

                            '<span class="selText" rel="0">' + userName + '</span>' +
                            '<span class="selText" id="msgTypeingName" rel="0"></span>' +
                        '</div>' +
                        '<div id="divMessage" class="messageArea">' +

                        '</div>' +
                        '<div class="buttonBar">' +
                            '<div class="row">' +
                                '<div class="input-field col s12">' +
                                    '<input id="txtPrivateMessage" class="validate col s8" style="font-size:small;" type="text" />' +
                                    '<input id="btnSendMessage" class="btn col s4" type="button" value="Enviar"   />' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div id="scrollLength"></div>' +
                      '</div>';

            var $div = $(div);

            // ------------------------------------------------------------------ Scroll Load Data ----------------------------------------------------------------------//

            var scrollLength = 2;
            $div.find('.messageArea').scroll(function () {
                if ($(this).scrollTop() == 0) {
                    if ($('#' + ctrId).find('#scrollLength').val() != '') {
                        var c = parseInt($('#' + ctrId).find('#scrollLength').val(), 10);
                        scrollLength = c + 1;
                    }
                    $('#' + ctrId).find('#scrollLength').val(scrollLength);
                    var count = $('#' + ctrId).find('#scrollLength').val();

                    chatHub.server.getScrollingChatData(userEmail, email, loadMesgCount, count).done(function (msg) {
                        for (i = 0; i < msg.length; i++) {
                            var firstMsg = $('#' + ctrId).find('#divMessage').find('.message:first');

                            // Where the page is currently:
                            var curOffset = firstMsg.offset().top - $('#' + ctrId).find('#divMessage').scrollTop();

                            // Prepend
                            $('#' + ctrId).find('#divMessage').prepend('<div class="message" style="padding-top:50px;"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');

                            // Offset to previous first message minus original offset/scroll
                            $('#' + ctrId).find('#divMessage').scrollTop(firstMsg.offset().top - curOffset);
                        }
                    });
                }
            });

            // DELETE BUTTON IMAGE
            $div.find('#imgDelete').click(function () {
                $('#' + ctrId).remove();
            });

            // Send Button event
            $div.find("#btnSendMessage").click(function () {
                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val().trim();
                if (msg !== "") {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Click');
                    $textBox.val('');
                }
            });

            $(document).ready(function () {
                $('#txtPrivateMessage').keypress(function (e) {
                    if (e.which == 13) {
                        $textBox = $div.find("#txtPrivateMessage");
                        var msg = $textBox.val().trim();
                        if (msg !== "") {
                            chatHub.server.sendPrivateMessage(userId, msg, 'Click');
                            $textBox.val('');
                        }
                    }
                });

            });

            // Text Box event
            $div.find("#txtPrivateMessage").keyup(function (e) {
                if (e.which == 13) {
                    $div.find("#btnSendMessage").click();
                }

                // Typing
                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val().trim();
                if (msg !== "") {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Type');
                }
                else {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
                }

                clearInterval(refreshId);
                checkTyping(chatHub, userId, msg, $div, 5000);
            });

            AddDivToContainer($div);
        }

        function checkTyping(chatHub, userId, msg, $div, time) {
            refreshId = setInterval(function () {
                // Typing
                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val().trim();
                if (msg !== "") {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
                }
            }, time);
        }

        function AddDivToContainer($div) {
            $('#divContainer').prepend($div);
            $div.draggable({
                handle: ".header",
                containment: "window",
                stop: function () {
                }
            });
        }
        // ------------------------------------------------------------------End Private Chat ----------------------------------------------------------------------//

    </script>
        <ul id="dropOpciones" class="dropdown-content">
            <li><a href="AdmOpciones.aspx" class="light-blue-text text-darken-4"><i class="material-icons left">new_releases</i> Opciones</a></li>
        </ul>
        <ul id="dropConsulta" class="dropdown-content">
            <li><a class="light-blue-text text-darken-4" href="C_Soportes.aspx" ><i class="material-icons left">report_problem</i>Consulta de soportes</a></li>
            <li><a class="light-blue-text text-darken-4" href="C_Pendientes.aspx" ><i class="material-icons left">new_releases</i>Consulta de pendientes</a></li>
        </ul>
        <div class="navbar-fixed">
            <nav>
                <div class="nav-wrapper container">
                    <a></a>
                    <a id="logo-container" href="#" data-activates="mobile-demo" class="brand-logo "><img src="Media/logoSkytexB.png" width="25" height="30" /></a>
                    
                    <ul class="side-nav" id="mobile-demo">
                        <li>.</li>
                        <li class="light-blue-text text-darken-4" style="font-size:smaller"><i class="material-icons right">perm_identity</i> <asp:Label ID="lblUsuarioOTS" runat="server" Text=""></asp:Label></li>
                        <li><a class="light-blue-text text-darken-4 tooltipped" href="Inicio.aspx" data-position="right" data-delay="50" data-tooltip="Regresa al inicio">Inicio<i class="material-icons right">store</i></a></li>
                        <%  
                            if (validarRol(Session["user_cve"].ToString().ToUpper(), "ADM") != null)
                            {
                        %>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="AdmOpciones.aspx" data-position="right" data-delay="50" data-tooltip="Administra las opciones disponibles">Opciones<i class="material-icons right">new_releases</i></a></li>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="C_Usuarios.aspx" data-position="right" data-delay="50" data-tooltip="Consulta el listado de usuarios">Usuarios<i class="material-icons right">supervisor_account</i></a></li>
                        <% 
                            }
                            if (validarRol(Session["user_cve"].ToString().ToUpper(), "ASG") != null)
                            {
                        %>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="A_Encabezado.aspx" data-position="right" data-delay="50" data-tooltip="Registra un nuevo Pendientes/Soportes">Alta OTS<i class="material-icons right">note_add</i></a></li>
                                <li><a class="dropdown-button light-blue-text text-darken-4 tooltipped" href="#!" data-activates="dropConsulta" data-position="right" data-delay="50" data-tooltip="Consulta las clasificaciones de OTS">OTS<i class="material-icons right">arrow_drop_down</i></a></li>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="Reportes.aspx" data-position="right" data-delay="50" data-tooltip="Genera tu reportes">Reportes<i class="material-icons right">receipt</i></a></li>
                        <%        
                            }
                            if (validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null && validarRol(Session["user_cve"].ToString().ToUpper(), "ASG") == null)
                            {
                        %>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="C_Soportes.aspx" data-position="right" data-delay="50" data-tooltip="Consulta tus tareas asignadas">Consulta OTS<i class="material-icons right">assignment</i></a></li>
                                <li><a class="light-blue-text text-darken-4 tooltipped" href="Reportes.aspx" data-position="right" data-delay="50" data-tooltip="Genera tu reportes">Reportes<i class="material-icons right">receipt</i></a></li>
                        <%        
                            }
                        %>
                        <li><a class="light-blue-text text-darken-4 tooltipped" href="Chat_OTS.aspx" data-position="right" data-delay="50" data-tooltip="Ingresa al Chat">Chat OTS<i class="material-icons right">textsms</i></a></li>
                        <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="CerrarSession" CssClass="tooltipped" data-position="right" data-delay="50" data-tooltip="Cierra sesion">
                                <span class="light-blue-text text-darken-4 " >
                                    <i class="material-icons right">power_settings_new</i>Logout
                                </span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>  
    <div id="divContainer">
        <div id="divLogin" class="login card grey lighten-3 z-depth-4">
          <div class="">
            <div class="card-content ">
              <span class="card-title">Chat de Usuarios</span><br /><br />
              <div>
                <asp:Label ID="lblUser_cve" runat="server" Text="Clave de Usuario:" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtUser_cve" runat="server" CssClass="validate" Readonly="true"></asp:TextBox>
              </div>
              <div>
                <asp:Label ID="lblUser" runat="server" Text="Nombre de Usuario:"  Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="validate" Readonly="true"></asp:TextBox>
              </div>
              <div>
                <asp:Label ID="lblMail" runat="server" Text="Email:"  Font-Bold="true" ></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="validate" Readonly="true"></asp:TextBox>
              </div>
            </div>
            <div class="card-action">
                <div id="divButton">
                    <input id="btnStartChat" type="button" class="waves-effect waves-light btn" value="Entrar" />
                </div>
            </div>
        </div>
            
            
        </div>
        <br />
        <div id="divChat" class="chatRoom  z-depth-4">
            <nav>
                <div class="nav-wrapper">
                    <div class="col s12">
                        Bienvenido al Chat de Usuarios OTS [<asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>]
                    </div>
                </div>
            </nav>
            <div class="content">
                <div id="divChatWindow" class="chatWindow">
                </div>
                <div id="divusers" class="users">
                </div>
            </div>
            <div class="messageBar">
                <div class="row">
                    <div class="input-field col s8">
                        <input class="validate col s9" style="font-size:small;" type="text" id="txtMessage" />
                        <input id="btnSendMsg" type="button" value="Enviar" class="btn" />
                    </div>
                </div>
            </div>
        </div>

        <input id="hdUser_cve" type="text" hidden/>
        <input id="hdUser" type="text" hidden/>
        <input id="hdEmail" type="text" style="color:white; cursor:default; text-shadow:white;" readonly/>
    </div>
    </div>
    </form>
    <div class="navbar" >
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
        </div>
    <script>
        $(document).ready(function () {
            $('select').material_select();
            $(".brand-logo").sideNav();
        });
      </script>
</body>
</html>
