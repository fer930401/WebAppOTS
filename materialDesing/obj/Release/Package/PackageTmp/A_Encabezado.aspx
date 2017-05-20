<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="A_Encabezado.aspx.cs" Inherits="materialDesing.A_Pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#fechaIni").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo OTS5.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Alta de Pendientes/Soportes</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <div class="row">
                <div class="col s12">
                    <div class="row">
                        <div class="input-field col s6">
                            <!--<input id="ss" name="ss" type="text" class="validate"/>
                            <label for="ss">Sub-Sistema:</label>-->
                            <label for="ss">Sub Sistema:</label><br /><br />
                            <asp:dropdownlist id ="ss" runat ="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:dropdownlist>                            
                        </div>
                        <div class="input-field col s6">
                            <p>
                              <input class="with-gap" name="tipo" type="radio" id="pendiente" value="PEN" checked />
                              <label for="pendiente">Pendiente</label>
                            </p>
                            <p>
                              <input class="with-gap" name="tipo" type="radio" id="soporte" value="SOP" />
                              <label for="soporte">Soporte</label>
                            </p>
                            <label for="tipo">Tipo:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <input id="nomOTS" name="nomOTS" type="text" class="validate" required=""/>
                            <label for="nomOTS">Nombre del nuevo OTS:</label>
                        </div>
                        <div class="input-field col s6">
                            <!--<input id="otsP" name="otsP" type="hidden" value="1" readonly/>
                            <label for="otsP">OTS padre:</label>-->
                        </div>
                        <div class="input-field col s6">
                            <!--<input id="otsT" name="otsT" type="hidden" value="SOP" readonly/>
                            <label for="otsT">OTS tipo:(tipo del ots padre)</label>-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <%--<!--<textarea id="operacion" name="operacion" class="materialize-textarea" length="120"></textarea>-->
                            <label for="operacion">Operación</label><br /><br />
                            <asp:DropDownList ID="opr" runat="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:DropDownList>--%>
                            <label for="responsable">Programador Responsable:</label><br /><br />
                            <asp:dropdownlist id ="prg_res" runat ="server" DataTextField="nombre"  DataValueField="user_cve" ></asp:dropdownlist>                            
                        </div>
                        <div class="input-field col s6">
                            <br />
                            <input class="with-gap" name="status" type="radio" id="desactivado" value="0" checked />
                            <label for="desactivado">Desactivado</label>
                            
                            <input class="with-gap" name="status" type="radio" id="activo" value="1"/>
                            <label for="activo">Activo</label>
                            
                            <label for="su">Status del OTS:</label>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="input-field col s12">
                            <textarea id="descripcion" name="descripcion" class="materialize-textarea" length="120"></textarea>
                            <label for="descripcion">Descripcion</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <input id="fechaIni" name="fechaIni" class="validate" type="text" required=""/>
                            <label for="fechaIni">Fecha Estimada de Fin:</label>
                        </div>
                        <div class="input-field col s6">
                            <div class="file-field input-field">
                                <div class="btn teal darken-4">
                                    <span>Imagenes</span>
                                    <asp:FileUpload ID="uploadFile1" runat="server" class="multi" maxlength="3" accept=".gif,.jpg,.png" AllowMultiple="true" />
                                </div>
                                <div class="file-path-wrapper">
                                    <input class="file-path validate" type="text" placeholder="Imagenes acerca del Pendiente/Soporte" required="">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="aplica">Aplica Para:</label><br /><br />
                            <asp:CheckBoxList ID="apl_para2" runat="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:CheckBoxList>
                            <!--<asp:dropdownlist id ="apl_para" runat ="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:dropdownlist>-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s4 right">
                            <a class="waves-effect red darken-4 btn right" href="Inicio.aspx">Cancelar</a>
                            <asp:Button ID="btnGuardarOTS" runat="server" Text="Guardar" class="waves-effect green darken-4 btn" OnClick="btnGuardarOTS_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
    
    <script type="text/javascript" src="<%= ResolveUrl("js/pickadate.legacy.js") %>"></script>
    <script type="text/javascript">
        $('[type=date], .datepicker').pickadate()
            
    </script>
    <script>
          $(document).ready(function () {
              $('select').material_select();
          });
      </script>
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
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $(function () {
            $("#fechaIni").datepicker();
            });
        </script>
    <!--<script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>-->
    
</asp:Content>
