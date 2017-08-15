<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="AdmOTS.aspx.cs" Inherits="materialDesing.AdmOTS" %>
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
            $("#<%= fechaIni.ClientID %>").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
    <div class="container">
        <div class="col s12 card-panel grey lighten-4 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Administracion de OTS:</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>   
            <div class="row">
                <div class="col s12">
                    <div class="row" style="display:none">
                        <div class="input-field col s6">
                            <label for="ss">Sub Sistema:</label><br /><br />
                            <asp:dropdownlist id ="ss" runat ="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:dropdownlist>                            
                        </div>
                        <div class="input-field col s6">
                            <p>
                              <input class="with-gap" name="tipo" type="radio" id="pendiente" value="PEN" <% Response.Write(Session["checkPen"]); %>/>
                              <label for="pendiente">Pendiente</label>
                            </p>
                            <p>
                              <input class="with-gap" name="tipo" type="radio" id="soporte" value="SOP" <% Response.Write(Session["checkSop"]); %>/>
                              <label for="soporte">Soporte</label>
                            </p>
                            <label for="tipo">Tipo:</label>
                        </div>
                    </div>
                    <div class="row" style="display:none">
                        <div class="input-field col s6">
                            <asp:TextBox ID="nomOTS" runat="server" CssClass="validate" ></asp:TextBox>
                            <label for="nomOTS">Nombre del nuevo OTS:</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="responsable">Programador Responsable:</label><br /><br />
                            <asp:dropdownlist id ="prg_res" runat ="server" DataTextField="nombre"  DataValueField="user_cve" ></asp:dropdownlist>                            
                        </div>
                        <div class="input-field col s6" style="display:none">
                            <br />
                            <input class="with-gap" name="status" type="radio" id="desactivado" value="0" <% Response.Write(Session["checkDes"]); %> />
                            <label for="desactivado">Desactivado</label>
                            
                            <input class="with-gap" name="status" type="radio" id="activo" value="1" <% Response.Write(Session["checkAct"]); %>/>
                            <label for="activo">Activo</label>
                            
                            <label for="su">Status del OTS:</label>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="input-field col s12">
                            <textarea id="descripcion" name="descripcion" class="materialize-textarea" length="150"><asp:Literal ID="txtDescrip" runat="server"></asp:Literal></textarea>
                            <label for="descripcion">Descripcion</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <asp:TextBox ID="fechaIni" runat="server" name="fechaIni" CssClass="validate" required=""></asp:TextBox>
                            <label for="fechaIni">Fecha Estimada de Fin:</label>
                        </div>
                        <div class="input-field col s6" <% Response.Write(Session["visibleImagenes"]); %>>
                            <div class="file-field input-field">
                                <div class="btn teal darken-4">
                                    <span>Imagenes</span>
                                    <asp:FileUpload ID="uploadFile1" runat="server" class="multi" maxlength="3" accept=".gif,.jpg,.png" AllowMultiple="true" />
                                </div>
                                <div class="file-path-wrapper">
                                    <input class="file-path validate" type="text" placeholder="Imagenes acerca del Pendiente/Soporte"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6">
                            <label for="aplica">Aplica Para:</label><br /><br />
                            <asp:CheckBoxList ID="apl_para2" runat="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s4">
                            <asp:Button ID="btnGuardarOTS" runat="server" Text="Guardar" class="waves-effect green darken-4 btn" OnClick="btnGuardarOTS_Click"/>
                            <a class="waves-effect red darken-4 btn" <asp:Literal ID="href" runat="server"></asp:Literal>>Cancelar</a>
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
            yearSuffix: '',
            minDate: 0
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $(function () {
            $("#<%= fechaIni.ClientID %>").datepicker();
        });
    </script>
</asp:Content>
