<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_Opciones.aspx.cs" Inherits="materialDesing.A_Opciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script type="text/javascript">
        function ShowSelected() {
            var cod = document.getElementById("<%= cmbOpciones.ClientID %>").value;
            if (cod == "APL") {
                document.getElementById('<%= elm_cve.ClientID %>').disabled = true;
            } else {
                document.getElementById('<%= elm_cve.ClientID %>').disabled = false;
            }
        }
    </script>
    <form id="form1" runat="server">
    <div class="container">
        <div class="col s12 card-panel grey lighten-4 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Alta de una nueva opción:</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>   
            <div class="row">
                <div class="col s12">
                    <div class="row">
                        <div class="input-field col s6">
                            <asp:DropDownList ID="cmbOpciones" runat="server" onchange="ShowSelected();" CssClass="browser-default" required></asp:DropDownList>
                        </div>
                        <div class="input-field col s6">
                            <label for="cve_catlgo">Clave de la opcion:</label>
                            <asp:TextBox ID="elm_cve" runat="server" name="elm_cve" MaxLength="3" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="nom_catlgo">Nombre de la opcion:</label>
                            <asp:TextBox ID="nom_catlgo" runat="server" name="nom_catlgo" required></asp:TextBox>
                        </div>
                        <div class="input-field col s6">
                            <br />
                            <p>
                              <input class="with-gap" name="status" type="radio" id="desactivado" value="0" checked />
                              <label for="desactivado">Desactivado</label>
                            </p>
                            <p>
                              <input class="with-gap" name="status" type="radio" id="activo" value="1"/>
                              <label for="activo">Activo</label>
                            </p>
                            <label for="su">Status de la opcion:</label>
                            
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div >
                            <asp:Button ID="btnGuardarOpc" runat="server" Text="Guardar" CssClass="btn green darken-4" OnClick="btnGuardarOpc_Click" OnClientClick="return ValidaCombo();" />
                            <a class="btn red darken-4 white-text" href="AdmOpciones.aspx">Cancelar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    function ValidaCombo() {
        var combo = document.getElementById('<%=cmbOpciones.ClientID%>').selectedIndex;
        if (combo == 0) {
            alert("Selecciona una opcion");
            return false;
        } else {
            return true;
        }
    }
</script>
    </form>
</body>
</html>
