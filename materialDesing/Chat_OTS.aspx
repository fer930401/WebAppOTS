<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Chat_OTS.aspx.cs" Inherits="materialDesing.Chat_OTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

            $("#txtUser_cve").keypress(function (e) {
                if (e.which == 13) {
                    $("#btnStartChat").click();
                }
            });

            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    $('#btnSendMsg').click();
                }
            });

            $('#btnSendMsg').click(function () {
                var msg = $("#txtMessage").val();
                if (msg.length > 0) {

                    var user_cve = $('#hdUser_cve').val();
                    chatHub.server.sendMessageToAll(user_cve, msg);
                    $("#txtMessage").val('');
                }
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

                var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

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
                        $('#' + ctrId).find('#msgTypeingName').text('typing...');
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
                code = $('<div class="loginUser">' + name + "</div>");
            }
            else {
                code = $('<a id="' + id + '" class="user" >' + name + '<a>');
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

            var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
                        '<div class="header">' +
                            '<div  style="float:right;">' +
                                '<img id="imgDelete"  style="cursor:pointer;" src="/Media/delete.png"/>' +
                            '</div>' +

                            '<span class="selText" rel="0">' + userName + '</span>' +
                            '<span class="selText" id="msgTypeingName" rel="0"></span>' +
                        '</div>' +
                        '<div id="divMessage" class="messageArea">' +

                        '</div>' +
                        '<div class="buttonBar">' +
                            '<input id="txtPrivateMessage" class="msgText" type="text"   />' +
                            '<input id="btnSendMessage" class="submitButton button" type="button" value="Send"   />' +
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
                            $('#' + ctrId).find('#divMessage').prepend('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');

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
                var msg = $textBox.val();
                if (msg.length > 0) {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Click');
                    $textBox.val('');
                }
            });

            // Text Box event
            $div.find("#txtPrivateMessage").keyup(function (e) {
                if (e.which == 13) {
                    $div.find("#btnSendMessage").click();
                }

                // Typing
                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val();
                if (msg.length > 0) {
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
                var msg = $textBox.val();
                if (msg.length == 0) {
                    chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
                }
            }, time);
        }

        function AddDivToContainer($div) {
            $('#divContainer').prepend($div);
            $div.draggable({
                handle: ".header",
                stop: function () {
                }
            });
        }
        // ------------------------------------------------------------------End Private Chat ----------------------------------------------------------------------//

    </script>
    <script language="javascript">
        function checkKeyCode(evt) {

            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116) {
                evt.keyCode = 0;
                return false
            }
        }
        document.onkeydown = checkKeyCode;
    </script>
    <div id="divContainer">
        <div id="divLogin" class="login card blue-grey darken-1">
          <div class="">
            <div class="card-content white-text">
              <span class="card-title">Chat de Usuarios</span><br /><br />
              <div>
                <asp:Label ID="lblUser_cve" runat="server" Text="Clave de Usuario:" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtUser_cve" runat="server" CssClass="validate"></asp:TextBox>
              </div>
              <div>
                <asp:Label ID="lblUser" runat="server" Text="Nombre de Usuario:"  Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="validate"></asp:TextBox>
              </div>
              <div>
                <asp:Label ID="lblMail" runat="server" Text="Email:"  Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="validate"></asp:TextBox>
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
        <div id="divChat" class="chatRoom">
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
                <input class="form-control" type="text" id="txtMessage" />
                <input id="btnSendMsg" type="button" value="Send" class="btn btn-success" />
            </div>
        </div>

        <input id="hdUser_cve" type="text" />
        <input id="hdUser" type="text" />
        <input id="hdEmail" type="text" />
    </div>
</asp:Content>
