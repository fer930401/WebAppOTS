<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="materialDesing.materialDesing1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <%
        //usuario.Text = Session["nombre"].ToString();
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
        $(function () {
            $("#fec_ini").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#fec_fin").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="80" style="padding-right:5px;"/>
                <div class="col s12 m3">
                    <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden="true"></asp:TextBox>
                    <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden="true"></asp:TextBox>
                    <asp:TextBox ID="fec_iniI" runat="server" ReadOnly="true"  hidden="true"></asp:TextBox>
                    <asp:TextBox ID="fec_finI" runat="server" ReadOnly="true"  hidden="true"></asp:TextBox>
                </div>
              
                <div class="col s1">
                    <br />
                    <h6>Filtro de grafica</h6>
                </div>
                <div class="input-field col s2">
                    <label for="num_OTS">Fecha inicio</label>
                    <br />
                    <asp:TextBox ID="fec_ini" runat="server" TextMode="Date" ></asp:TextBox>
                </div>
                <div class="input-field col s2">
                    <label for="num_OTS">Fecha Fin</label>
                    <br />
                    <asp:TextBox ID="fec_fin" runat="server" TextMode="Date" ></asp:TextBox>
                </div>
                <div class="col s1">
                    <br />
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn green darken-4" OnClick="btnFiltrar_Click"/>
                </div>
            </div>
            <script type="text/javascript">
                var SOP_sin_cumplir = parseInt([<% OTSGraf("SOP", 1, user_cve.Text, DateTime.Parse(fec_iniI.Text), DateTime.Parse(fec_finI.Text)); %>]);
                var SOP_resuelos = parseInt([<% OTSGraf("SOP", 3, user_cve.Text, DateTime.Parse(fec_iniI.Text), DateTime.Parse(fec_finI.Text)); %>]);
                var PEN_sin_cumplir = parseInt([<% OTSGraf("PEN", 1, user_cve.Text, DateTime.Parse(fec_iniI.Text), DateTime.Parse(fec_finI.Text)); %>]);
                var PEN_resuelos = parseInt([<% OTSGraf("PEN", 3, user_cve.Text, DateTime.Parse(fec_iniI.Text), DateTime.Parse(fec_finI.Text)); %>]);
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
                                y: SOP_sin_cumplir
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
                <div class="col s10">
                    <div id="container"></div>
                </div>
                <div class="col s2" style="margin-top:-15px;">
                    <div class="center">
                        <div class="icon-block card-panel hoverable center tooltipped" data-tooltip="Consulta de Soportes no iniciados" onclick="location.href='C_Soportes.aspx';" style="cursor:pointer;">
                            <h3><i class="material-icons center red-text">report_problem</i></h3>
                            <h5 style="margin-top:-25px;"><asp:Label ID="soportesNoIniciados" runat="server" Text=""></asp:Label></h5>
                                    
                        </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center tooltipped" data-tooltip="Consulta de Soportes Terminados" onclick="location.href='C_Soportes.aspx';" style="cursor:pointer;">
                        <h3><i class="material-icons center blue-text">trending_up</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="soportesTerminados" runat="server" Text=""></asp:Label></h5>           
                      </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center tooltipped" data-tooltip="Consulta de pendientes no iniciados" onclick="location.href='C_Pendientes.aspx';" style="cursor:pointer;">
                        <h3><i class="material-icons center orange-text">new_releases</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="pendientesNoIniciados" runat="server" Text=""></asp:Label></h5>             
                      </div>
                    </div>

                    <div class="center">
                      <div class="icon-block card-panel hoverable center tooltipped" data-tooltip="Consulta de pendientes terminados" onclick="location.href='C_Pendientes.aspx';" style="cursor:pointer;">
                        <h3><i class="material-icons center green-text">business</i></h3>
                        <h5 style="margin-top:-25px;"><asp:Label ID="pendientesTerminados" runat="server" Text=""></asp:Label></h5>             
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '< Ant',
            nextText: 'Sig >',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: '',
            minDate: new Date()
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $(function () {
            $("#fec_ini").datepicker();
        });
        $(function () {
            $("#fec_fin").datepicker();
        });
        </script>
</asp:Content>

