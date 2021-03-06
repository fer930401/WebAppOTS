﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="AdmUsuarios.aspx.cs" Inherits="materialDesing.AdmUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Modificacion de Usuarios</h4>
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
                            <asp:TextBox ID="txtUser_cve" runat="server" CssClass="validate" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="validate" required></asp:TextBox>
                            <label for="txtNombre">Nombre:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <br />
                            <p>
                              <input class="with-gap" name="status" type="radio" id="desactivado" value="0" <% if (LogicaNegocio.variables.M_Status.Equals("0") == true /*&& !IsPostBack*/) { Response.Write("checked"); }; %> />
                              <label for="desactivado">Desactivado</label>
                            </p>
                            <p>
                              <input class="with-gap" name="status" type="radio" id="activo" value="1" <% if (LogicaNegocio.variables.M_Status.Equals("1") == true /*&& !IsPostBack*/) { Response.Write("checked"); }; %>/>
                              <label for="activo">Activo</label>
                            </p>
                            <label for="su">Status del Usuario:</label>
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <asp:TextBox ID="txtEmail" type="email" runat="server" CssClass="validate" required></asp:TextBox>
                            <label for="txtEmail">Email Usuario:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="aplica">Roles de Usuario:</label><br /><br />
                            <asp:CheckBoxList ID="rol_cve" runat="server"></asp:CheckBoxList>
                        </div>
                    </div> 
                    <div class="row">
                        <div class="input-field col s5">
                            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" class="green darken-4 btn" OnClick="btnGuardarUsuario_Click" />
                            <a class="red darken-4 btn" href="C_Usuarios.aspx">Cancelar</a>
                        </div>
                    </div>
                </div>
            </div>
            </div>                          
        </div>
</asp:Content>
