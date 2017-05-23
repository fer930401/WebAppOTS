<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="A_Usuarios.aspx.cs" Inherits="materialDesing.A_Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function user_cve() {
            var primerNombre = document.getElementById("nu").value.charAt(0);
            var PartenoNombre = document.getElementById("pu").value.charAt(0);
            var MaternoNombre = document.getElementById("mu").value.charAt(0);
            document.getElementById("cu").value = primerNombre + PartenoNombre + MaternoNombre;
        }
    </script>
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <div class="container">
        
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Alta de Usuarios</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <div class="row">
                <div class="col s1">
                    .
                </div>
                <div class="col s10">
                    <div class="row">
                        <div class="input-field col s11">
                            <span class="grey-text">Clave del Usuario (User_cve)</span>
                            <input id="cu" name="cu" type="text" class="validate mayusculas" readonly="true"/>
                        </div>
                    </div>
                    <div class="row">
                        <label>Nombre del Usuario</label>
                        <div class="input-field col s11">
                            <input id="nu" name="nu" type="text" class="validate" onchange="user_cve()" required=""/>
                            <label for="nu">Nombre:</label>
                        </div>
                        <div class="input-field col s11">
                            <input id="pu" name="pu" type="text" class="validate" onchange="user_cve()" required=""/>
                            <label for="nu">Apellido Paterno:</label>
                        </div>
                        <div class="input-field col s11">
                            <input id="mu" name="mu" type="text" class="validate" onchange="user_cve()" required=""/>
                            <label for="nu">Apellido Materno:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <input id="co" name="co" type="password" class="validate" required=""/>
                            <label for="co">Contraseña</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <br />
                            <p>
                              <input class="with-gap" name="status" type="radio" id="desactivado" value="0" checked />
                              <label for="desactivado">Desactivado</label>
                            </p>
                            <p>
                              <input class="with-gap" name="status" type="radio" id="activo" value="1"/>
                              <label for="activo">Activo</label>
                            </p>
                            <label for="su">Status del Usuario:</label>
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <input id="eu" name="eu" class="validate" type="email" required=""/>
                            <label for="eu">Email Usuario:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <label for="roles">Rol del Usuario:</label><br /><br />
                            <asp:dropdownlist id ="roles" runat ="server" DataTextField="nombre" DataValueField="elm_cve"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s5 right">
                            <a class="waves-effect red darken-4 btn right" href="C_Usuarios.aspx">Cancelar</a>
                            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" class="waves-effect green darken-4 btn" OnClick="btnGuardarUsuario_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            </div>                          
        </div>
    <script type="text/javascript" src="<%= ResolveUrl("js/pickadate.legacy.js") %>"></script>
</asp:Content>
