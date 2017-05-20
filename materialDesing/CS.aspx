<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CS.aspx.cs" Inherits="materialDesing.CS" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- asignacion de icono de barra de navegador -->
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/skytex.ico") %>" />
    <!-- CSS  -->
    <script type="text/javascript">
        //setInterval
        var ore = 1, minuti = 59, secondi = 45, decimi = 0;
        var vis = "";
        var stop = true;

        function avvia() {
            if (stop == true) {
                stop = false;
                cronometro();
                
            }
        }

        function cronometro() {
            if (stop == false) {
                decimi++;
                if (decimi > 9) {
                    decimi = 0;
                    secondi++;
                }
                if (secondi > 59) {
                    secondi = 0;
                    minuti++;
                }
                if (minuti > 59) {
                    minuti = 0;
                    ore++;
                }
                mostra();
                setTimeout("cronometro()", 100);
            }
        }
        function mostra() {
            if (ore == 0) vis = ""; else vis = ore+":";
            if (minuti < 10) vis = ""+ vis + "0";
            vis = vis + minuti + ":";
            if (secondi < 10) vis = vis + "0";
            vis = vis + secondi + ":" + decimi;
            document.getElementById("vis").innerHTML = vis;
        }
        function ferma() {
            stop = true;
            alert(document.getElementById("vis").innerHTML);
        }
        function azzera() {
            if (stop == false) {
                stop = true;
            }
            ore = minuti = secondi = decimi = 0;
            vis = "";
            mostra();
        }
        $(function () {
            var offset = $("#sidebar").offset();
            var topPadding = 15;
            $(window).scroll(function () {
                if ($("#sidebar").height() < $(window).height() && $(window).scrollTop() > offset.top) { /* LINEA MODIFICADA POR ALEX PARA NO ANIMAR SI EL SIDEBAR ES MAYOR AL TAMANO DE PANTALLA */
                    $("#sidebar").stop().animate({
                        marginTop: $(window).scrollTop() - offset.top + topPadding
                    });
                } else {
                    $("#sidebar").stop().animate({
                        marginTop: 0
                    });
                };
            });
        });
	</script>
</head>
<body>
	<div class="sidebar">
            <div id="cronometro">
	<div id="vis" style="float: left; line-height: 27px;">00:00:00:00</div>
	<div id="button_container" style="display: inline; margin-left: 10px;">
		<button id="avvia" onclick="javascript:avvia();">AVVIA</button>
		<button id="stop" onclick="javascript:ferma();">STOP</button>
		<button id="azzera" onclick="javascript:azzera();">AZZERA</button>
	</div>
        </div>
	
</div>
</body>
</html>
