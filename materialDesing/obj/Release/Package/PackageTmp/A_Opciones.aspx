<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="A_Opciones.aspx.cs" Inherits="materialDesing.A_Opciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <asp:DropDownList ID="cmbOpciones" runat="server" OnSelectedIndexChanged="cmbOpciones_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <label for="opciones">Opciones disponibles:</label>
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
                            <asp:Button ID="btnGuardarOpc" runat="server" Text="Guardar" CssClass="waves-effect green darken-4 btn" OnClick="btnGuardarOpc_Click" />
                            <a class="waves-effect red darken-4 btn " href="Inicio.aspx">Cancelar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
