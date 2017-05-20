<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_Pendientes.aspx.cs" Inherits="materialDesing.C_Pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function confirmar() {
            if (confirm('Desea eliminar este pendiente?')) {
                alert("Pendiente Eliminado");
                window.location.href = 'C_Pendientes.aspx';
            } else {
                return false;
            }
        }
    </script>

    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <h5 class="center blue-text"><strong>OTS</strong> On Time System</h5>
                <h5 class="center grey-text">Consulta de Pendientes</h5>
            </div>
            <asp:TextBox ID="user_cve" runat="server" hidden></asp:TextBox>
            <div class="row center">
                <div class="fixed-action-btn horizontal" style="bottom: 45px; right: 24px;">
                    <a class="btn-floating btn-large red accent-4 btn tooltipped" data-position="top" data-delay="50" data-tooltip="Opciones">
                    <i class="large material-icons">dashboard</i>
                    </a>
                    <ul>
                        <li><a class="btn-floating red darken-4 btn tooltipped" href="Inicio.aspx" data-position="top" data-delay="50" data-tooltip="Inicio"><i class="material-icons">store</i></a></li>
                        <li><a class="btn-floating amber darken-3 btn tooltipped" href="A_Pendientes.aspx" data-position="top" data-delay="50" data-tooltip="Alta de Pendientes"><i class="material-icons">info_outline</i></a></li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="row">
                    <div class="input-field col s6">
                        <input id="busqueda" type="text" class="validate"/>
                        <label for="busqueda">Busqueda</label>
                    </div>
                    <div class="input-field col s6">
                        <a class="waves-effect green darken-1 btn tooltipped" data-position="top" data-delay="50" data-tooltip="Buscar.."><i class="material-icons">search</i></a>
                    </div>
                </div>
            </div>
            <div class="row">
                <table class="responsive-table striped">
                <thead>
                  <tr>
                      <th data-field="id">Folio</th>
                      <th data-field="importancia">Importancia</th>
                      <th data-field="solicita">Solicitante</th>
                      <th data-field="porblema">Problema presentado</th>
                      <th data-field="correccion">Correccion</th>
                      <th data-field="solucionado">Solucionado</th>
                      <th data-field="price">Opciones</th>
                  </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>123</td>
                        <td>Alta</td>
                        <td>No lo se</td>
                        <td>Asi estaba cuando llegue</td>
                        <td>N/A</td>
                        <td>No</td>
                        <td>
                            <div class="row">
                            <div class="input-field col s6">
                              <a href="U_Pendientes.aspx" class="waves-effect amber darken-1 btn tooltipped" data-position="top" data-delay="50" data-tooltip="Editar"><i class="material-icons">mode_edit</i></a>
                            </div>
                            <div class="input-field col s6">
                              <a class="waves-effect red darken-4 btn tooltipped" data-position="top" data-delay="50" data-tooltip="Eliminar" onclick="confirmar()"><i class="material-icons">shuffle</i></a>
                            </div>
                          </div>
                        </td>
                    </tr>
                </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
