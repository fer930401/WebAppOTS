<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="materialDesing.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h5 class="center grey-text">Reportes de Desempeño</h5>
              <div class="col s12 m3">
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <h5 class="center grey-text"><strong><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></strong>, tu desempeño ha sido el siguiente:</h5>
            <div class="row">
                <br />
                <div class="col s3"> .</div>
                <div class="col s8 center">
                    <div class="row">
                        <div class="col s4">
                            <p><i class="material-icons">business</i> Pendientes terminados:</p>
                        </div>
                        <div class="col s4">
                            <p><strong><asp:Label ID="pendientesT" runat="server" Text="Label"></asp:Label></strong> pendientes terminados</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s4">
                            <p><i class="material-icons">trending_up</i> Soportes terminados:</p>
                        </div>
                        <div class="col s4">
                            <p><strong><asp:Label ID="soportesT" runat="server" Text="Label"></asp:Label></strong> soportes terminados</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s4">
                            <p><i class="material-icons">query_builder</i> Tu tiempo muerto es de:</p>
                        </div>
                        <div class="col s4">
                            <p><strong><asp:Label ID="minParos" runat="server" Text="Label"></asp:Label></strong> minutos</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s4">
                            <p><i class="material-icons">equalizer</i> Dificultad promedio:</p>
                        </div>
                        <div class="col s4 ">
                            <p class="text-left">
                                <asp:GridView ID="GridView1" runat="server" ></asp:GridView>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s4">
                            <p></p>
                        </div>
                        <div class="col s7 ">
                            <a href="ReporteActividadesOTS.aspx" target="_blank" class="btn teal darken-4 modal-trigger"><i class="material-icons left">import_export</i> Exporta tus resultados </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
