<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="AdmOpciones.aspx.cs" Inherits="materialDesing.AdmOpciones" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            var dWidth = $(window).width() * 0.9;
            var dHeight = $(window).height() * 0.9;
            $("#btnOpc").on("click", function () {
                $('<div>').dialog({
                    modal: true,
                    open: function () {
                        $(this).load('A_Opciones.aspx');
                    },
                    width: dWidth,
                    height: dHeight,
                    draggable: false,
                    responsive: true,
                    close: function (e, ui) {
                        window.location.href = 'AdmOpciones.aspx';
                    }
                }).prev(".ui-dialog-titlebar").css("background", "#042644");
            });
        });
    </script>
    <style>
        .ui-dialog {
            z-index: 1000000000;
            top: 0;
            left: 0;
            margin: auto;
            position: fixed;
            max-width: 100%;
            max-height: 100%;
            display: flex;
            flex-direction: column;
            align-items: stretch;
        }

        .ui-dialog .ui-dialog-content {
            flex: 1;
        }

        .ui-dialog .ui-dialog-buttonpane {
            background: white;
        }
        .ui-dialog .ui-dialog-titlebar {
            background:#042644;
            border-color:#042644;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                            <asp:DropDownList ID="cmbOpciones" runat="server" OnSelectedIndexChanged="cmbOpciones_SelectedIndexChanged" AutoPostBack="true" required></asp:DropDownList>
                            <label for="opciones">Opciones disponibles:</label>
                        </div>
                        <div class="input-field col s6">
                            <a id="btnOpc" class="btn-floating btn-large waves-effect waves-light amber darken-4 tooltipped" data-position="right" data-delay="50" data-tooltip="Agrega una nueva opcion"><i class="material-icons">add</i></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s5">
                            <asp:GridView 
                                ID="gvOpciones" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#042644" 
                                HeaderStyle-ForeColor="White" EmptyDataText="No hay resultados para la busqueda" 
                                OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" datakeynames="elm_cve">
                                <Columns>
                                    <asp:BoundField DataField="elm_cve" HeaderText="Clave de la opcion" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre de la opcion" />
                                    <asp:BoundField DataField="status" HeaderText="Status de la opcion" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
    <br />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" /> 
</asp:Content>
