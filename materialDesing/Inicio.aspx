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
                    $('.flotante2').slideDown(300);
                } else {
                    $('.flotante2').slideUp(300);
                }
            });

        });
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h5 class="center grey-text">Bienvenido: <strong><asp:Label ID="usuario" runat="server" Text="Label"></asp:Label></strong></h5>
              <div class="col s12 m3">
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <script type="text/javascript">
                var SOP_sin_cumplir = parseInt([<% OTSGraf("SOP", 1, user_cve.Text, "01"); %>]);
                var SOP_resuelos = parseInt([<% OTSGraf("SOP", 3, user_cve.Text, "01"); %>]);
                var PEN_sin_cumplir = parseInt([<% OTSGraf("PEN", 1, user_cve.Text, "01"); %>]);
                var PEN_resuelos = parseInt([<% OTSGraf("PEN", 3, user_cve.Text, "01"); %>]);
                $(function () {
                    $('#container').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: 'Resumen de resultados:'
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y}</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: false
                                },
                                showInLegend: true
                            }
                        },
                        series: [{
                            name: 'Total',
                            colorByPoint: true,
                            data: [{
                                name: 'Soportes sin resolver',
                                y: SOP_sin_cumplir,
                                sliced: true,
                                selected: true
                            }, {
                                name: 'Soportes Resueltos',
                                y: SOP_resuelos                                
                            }, {
                                name: 'Pendientes sin resolver',
                                y: PEN_sin_cumplir
                            }, {
                                name: 'Pendientes Resueltos',
                                y: PEN_resuelos
                            }]
                        }]
                    });
                });
            </script>
            <div class="row">
                <div class="col s12 m11">
                    <div id="container"></div>
                </div>
                <div class="col s12 m1" style="margin-top:-15px;">
                    <div class="center">
                        <div class="icon-block card-panel hoverable center" onclick="location.href='C_Soportes.aspx';" style="cursor:pointer; width:80px; height:80px;">
                            <h3><i class="material-icons center red-text">report_problem</i></h3>
                            <h5 style="margin-top:-25px;"><asp:Label ID="soportesNoIniciados" runat="server" Text=""></asp:Label></h5>
                                    
                        </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center" onclick="location.href='C_Soportes.aspx';" style="cursor:pointer; width:80px; height:80px;">
                        <h3><i class="material-icons center blue-text">trending_up</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="soportesTerminados" runat="server" Text=""></asp:Label></h5>           
                      </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center" onclick="location.href='C_Pendientes.aspx';" style="cursor:pointer; width:80px; height:80px;">
                        <h3><i class="material-icons center orange-text">new_releases</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="pendientesNoIniciados" runat="server" Text=""></asp:Label></h5>             
                      </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center" onclick="location.href='C_Pendientes.aspx';" style="cursor:pointer; width:80px; height:80px;">
                        <h3><i class="material-icons center green-text">business</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="pendientesTerminados" runat="server" Text=""></asp:Label></h5>             
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

