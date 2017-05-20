<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="materialDesing.materialDesing1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <br />
    <script>
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(this).scrollTop() >= 0) {
                    //$('.flotante').animate({ marginBottom: 50 }, 100);
                    $('.flotante2').slideDown(300);
                } else {
                    //$('.flotante').animate({ marginBottom: 0 }, 100);
                    $('.flotante2').slideUp(300);
                }
            });

        });
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo OTS5.png") %>" width="350" height="100"/>
              <br />
              <h5 class="center grey-text">Bienvenido: <strong><asp:Label ID="usuario" runat="server" Text="Label"></asp:Label></strong></h5>
              <div class="col s12 m3">
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#container').highcharts({
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Resumen de resultados:'
                        },
                        xAxis: {
                            categories: [
                                'Enero',
                                'Febrero',
                                'Marzo',
                                'Abril',
                                'Mayo',
                                'Junio',
                                'Julio',
                                'Agosto',
                                'Septiembre',
                                'Octubre',
                                'Noviembre',
                                'Diciembre'
                            ]
                        },
                        yAxis: {

                            min: 0,
                            title: {
                                text: 'Cantidad de OTS'
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:20px">{point.key}</span><table>',
                            pointFormat: '<tr><td style="color:{series.color};padding:0;font-size:15px">{series.name}: </td>' +
                                '<td style="padding:0"><b>{point.y} OTS</b></td></tr>',
                            footerFormat: '</table>',
                            shared: true,
                            useHTML: true
                        },
                        plotOptions: {
                            column: {
                                pointPadding: 0.2,
                                borderWidth: 0
                            }
                        },
                        series: [{
                            borderRadius: 5,
                            name: 'Soportes sin cumplir',
                            data: [<% OTSGraf("SOP", 1, user_cve.Text, "01"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "02"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "03"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "04"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "05"); %>,<% OTSGraf("SOP", 1, user_cve.Text, "06"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "07"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "08"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "09"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "10"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "11"); %>, <% OTSGraf("SOP", 1, user_cve.Text, "12"); %>],
                            }, {
                                name: 'Soportes Resueltos',
                                data: [<% OTSGraf("SOP", 3, user_cve.Text, "01"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "02"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "03"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "04"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "05"); %>,<% OTSGraf("SOP", 3, user_cve.Text, "06"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "07"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "08"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "09"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "10"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "11"); %>, <% OTSGraf("SOP", 3, user_cve.Text, "12"); %>],
                            }, {
                                name: 'Pendientes sin cumplir',
                                data: [<% OTSGraf("PEN", 1, user_cve.Text, "01"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "02"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "03"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "04"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "05"); %>,<% OTSGraf("PEN", 1, user_cve.Text, "06"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "07"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "08"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "09"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "10"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "11"); %>, <% OTSGraf("PEN", 1, user_cve.Text, "12"); %>],
                            }, {
                                name: 'Pendientes cumplidos',
                                data: [<% OTSGraf("PEN", 3, user_cve.Text, "01"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "02"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "03"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "04"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "05"); %>,<% OTSGraf("PEN", 3, user_cve.Text, "06"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "07"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "08"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "09"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "10"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "11"); %>, <% OTSGraf("PEN", 3, user_cve.Text, "12"); %>],
                            }]
                        });
                    });
            </script>
            <div class="row">
                <div id="container" height: 250px; margin: 0 auto"></div>
            </div>
            <div class="row">
                <div class="col s12 m3 center">
                    <div class="icon-block card-panel hoverable">
                        <h2 class="center red-text"><i class="material-icons">report_problem</i></h2>
                        <h5 class="center">Soportes sin cumplir</h5>
                        Tienes un total de 
                            <h5>
                                <asp:Label ID="soportesNoIniciados" runat="server" Text=""></asp:Label>
                            </h5> soportes sin cumplir
                    </div>
                </div>

                <div class="col s12 m3 center">
                  <div class="icon-block card-panel hoverable">
                    <h2 class="center blue-text"><i class="material-icons">trending_up</i></h2>
                    <h5 class="center">Soportes Resueltos</h5>

                    Tienes un total de 
                            <h5>
                                <asp:Label ID="soportesTerminados" runat="server" Text=""></asp:Label>
                            </h5> soportes cumplidos
                  </div>
                </div>

                <div class="col s12 m3 center">
                  <div class="icon-block card-panel hoverable">
                    <h2 class="center orange-text"><i class="material-icons">new_releases</i></h2>
                    <h5 class="center">Pendientes sin cumplir</h5>

                    Tienes un total de 
                            <h5>
                                <asp:Label ID="pendientesNoIniciados" runat="server" Text=""></asp:Label>
                            </h5> pendientes sin cumplir
                  </div>
                </div>

                <div class="col s12 m3 center">
                  <div class="icon-block card-panel hoverable">
                    <h2 class="center green-text"><i class="material-icons">business</i></h2>
                    <h5 class="center">Pendientes Cumplidos</h5>

                    Tienes un total de 
                            <h5>
                                <asp:Label ID="pendientesTerminados" runat="server" Text=""></asp:Label>
                            </h5> pendientes cumplidos
                  </div>
                </div>
            </div>
            
            <!--<div class="row center">
            <div class="fixed-action-btn horizontal" style="bottom: 45px; right: 24px;">
                <a class="btn-floating btn-large red accent-4 btn tooltipped" data-position="top" data-delay="50" data-tooltip="Opciones">
                  <i class="large material-icons">dashboard</i>
                </a>
                <ul>
                  <li><a class="btn-floating red darken-4 btn tooltipped" href="Inicio.aspx" data-position="top" data-delay="50" data-tooltip="Inicio"><i class="material-icons">store</i></a></li>
                  <li><a class="btn-floating amber darken-3 btn tooltipped" href="A_Encabezado.aspx" data-position="top" data-delay="50" data-tooltip="Alta de Pendientes/Soportes"><i class="material-icons">info_outline</i></a></li>
                  <li><a class="btn-floating light-blue darken-4 btn tooltipped" href="C_Soportes.aspx" data-position="top" data-delay="50" data-tooltip="Consulta de Soporte"><i class="material-icons">schedule</i></a></li>
                  <li><a class="btn-floating green darken-4 btn tooltipped" href="Reportes.aspx" data-position="top" data-delay="50" data-tooltip="Reportes"><i class="material-icons">receipt</i></a></li>
                </ul>
            </div>
            </div>-->
            <!--
            <div class="flotante2" id="flota">
                <strong>Revisa tus resultados</strong>
                <a href="Reportes.aspx" class="waves-effect waves-light btn teal darken-4 modal-trigger" data-tooltip="Reporte de actividades"><i class="material-icons left">cloud</i>Actividades</a>
            </div>-->
        </div>
    </div>
</asp:Content>

