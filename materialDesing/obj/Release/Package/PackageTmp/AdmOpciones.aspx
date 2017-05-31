<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="AdmOpciones.aspx.cs" Inherits="materialDesing.AdmOpciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col s12 card-panel grey lighten-4 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Administracion de opciones:</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>   
            <div class="row">
                <div class="col s12">
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="cve_catlgo">Busca la Clave de la opcion:</label>
                            <asp:TextBox ID="elm_cve" runat="server" name="elm_cve" MaxLength="3" required></asp:TextBox>
                        </div>
                        <div class="input-field col s6">
                            <asp:Button ID="btnBuscaClave" runat="server" Text="Buscar" CssClass="waves-effect green darken-4 btn" OnClick="btnBuscaClave_Click"/>
                        </div>
                    </div>
                    <div class="row" <asp:Literal ID="Literal1" runat="server"></asp:Literal>>
                        <div class="row">
                            <div class="input-field col s6">
                                <label for="nom_catlgo">Clave de catalogo:</label>
                                <asp:TextBox ID="claveCat" runat="server" name="claveCat" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="input-field col s6">
                                <label for="nom_catlgo">Nombre de la opcion:</label>
                                <asp:TextBox ID="nom_catlgo" runat="server" name="nom_catlgo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s6">
                                <br />
                                <p>
                                  <input class="with-gap" name="status" type="radio" id="desactivado" value="0" <% if (LogicaNegocio.variables.M_StatusOpc.Equals("0") == true) { Response.Write("checked"); }; %> />
                                  <label for="desactivado">Desactivado</label>
                                </p>
                                <p>
                                  <input class="with-gap" name="status" type="radio" id="activo" value="1" <% if (LogicaNegocio.variables.M_StatusOpc.Equals("1") == true) { Response.Write("checked"); }; %>/>
                                  <label for="activo">Activo</label>
                                </p>
                                <label for="su">Status de la opcion:</label>
                            
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s6">
                                <asp:Button ID="btnGuardarOpc" runat="server" Text="Guardar" CssClass="waves-effect green darken-4 btn" OnClick="btnGuardarOpc_Click" />
                                <a class="waves-effect red darken-4 btn " href="Inicio.aspx">Cancelar</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
