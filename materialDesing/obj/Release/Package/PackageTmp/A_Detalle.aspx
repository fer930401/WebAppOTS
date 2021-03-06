﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="A_Detalle.aspx.cs" Inherits="materialDesing.A_Soportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        document.getElementById("error").required = true;
        $('#error').prop('required', true);
        function quitaValidacion() {
            $("#form1").attr("novalidate", "novalidate");
        }
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-4 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Alta de Detalle del OTS</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>   
            <div class="row">
                <div class="col s12">
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="num_OTS">Numero de OTS</label>
                            <asp:TextBox ID="num_OTS" runat="server" name="num_OTS" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="input-field col s6">
                            <label for="tipo_OTS">Tipo OTS</label>
                            <asp:TextBox ID="tipo_OTS" runat="server" name="tipo_OTS" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <asp:DropDownList ID="opr" runat="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:DropDownList>
                            <label for="operacion">Proceso a revisar:</label>
                        </div>
                        <div class="input-field col s6">
                            <asp:TextBox ID="nom_proceso" runat="server" name="nom_proceso" required></asp:TextBox>
                            <label for="nom_proceso">Descripcion del Sub OTS:</label>
                        </div>
                    </div>
                    <div class="row" >
                        <div class="input-field col s12">
                            <asp:TextBox ID="descripcion" runat="server" name="descripcion" ReadOnly="true"></asp:TextBox>
                            <label for="descripcion">Descripcion del OTS</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <asp:TextBox ID="fechaIni" runat="server" name="fechaIni" ReadOnly="true"></asp:TextBox>
                            <label for="fechaIni">Fecha inicio:</label>
                        </div>
                        <div class="input-field col s6">
                            <asp:TextBox ID="fechaFin" runat="server" name="fechaFin" ReadOnly="true"></asp:TextBox>
                            <label for="fechaFin">Fecha prometida:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12" >
                            <textarea id="error" name="error" class="materialize-textarea" length="254" required> </textarea>
                            <label for="error">Error:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12">
                            <textarea id="obs" name="obs" class="materialize-textarea" length="254" required> </textarea>
                            <label for="obs">Observaciones:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div >
                            <asp:Button ID="btnGuardarOTSD" runat="server" Text="Guardar" CssClass="green darken-4 btn" OnClick="btnGuardarOTSD_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="red darken-4 btn" OnClick="btnCancelar_Click" OnClientClick="quitaValidacion()"/>
                        </div>
                    </div>
                </div>
            </div>            
        </div>
    </div>
</asp:Content>
