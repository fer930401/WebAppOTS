<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_Pendientes.aspx.cs" Inherits="materialDesing.C_Pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
  <script src="http://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.5/js/materialize.min.js"></script>
  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          $("#<%=GridView1.ClientID%> [id='subReng']").click(function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();
              var id_OTS = $("td:eq(0)", tr).html();
              var tip_OTST = $("td:eq(1)", tr).html();
              //alert(id_OTS);
              window.location.href = "C_subOTS.aspx?num_OTS=" + id_OTS + "&tip_OTS=" + tip_OTST;
          });
      });
      $(document).ready(function () {
          $("#<%=GridView1.ClientID%> [id='imgOTS']").click(function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();
              var id_OTS = $("td:eq(0)", tr).html();
              var tip_OTST = $("td:eq(1)", tr).html();
              //alert(id_OTS);
              window.location.href = "C_imgOTS.aspx?num_OTS=" + id_OTS + "&tip_OTS=" + tip_OTST;
          });
      });
      $(document).ready(function () {
          $("#<%=GridView1.ClientID%> [id='btnAgregar']").click(function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();
              var id_OTS = $("td:eq(0)", tr).html();
              alert(id_OTS);
              window.location.href = "A_Detalle.aspx?num_OTS=" + id_OTS;
          });
      });
      $(function () {
          $("#<%=GridView1.ClientID%> [id='btnReasignar']").click(function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();;
              var id_OTS = $("td:eq(0)", tr).html();
              var tip_OTS = $("td:eq(1)", tr).html();
              var nom_resp = $("td:eq(2)", tr).html();
              var dWidth = $(window).width() * 0.9;
              var dHeight = $(window).height() * 0.9;
              alert(id_OTS);
              $('<div>').dialog({
                  modal: true,
                  open: function () {
                      $(this).load('ReasignaOTS.aspx?num_OTS=' + id_OTS + '&tip_OTS=' + tip_OTS);
                  },
                  width: dWidth,
                  height: dHeight,
                  draggable: false,
                  responsive: true,
                  title: 'reasignar ots'
              });
          });
      });

      $(document).ready(function () {
          // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
          $('.modal-trigger').leanModal();
      });
      $(document).ready(function () {
          $('select').material_select();
      });

    </script>
    <style>
        *{
	        margin: 0;
	        padding: 0;
        }
        #contenedor{
	        margin: 10px auto;
	        width: 540px;
	        height: 115px;
        }
        .reloj{
	        float: left;
	        font-size: 80px;
	        font-family: Courier,sans-serif;
	        color: #363431;
        }
        .pagination-ys {
            /*display: inline-block;*/
                padding-left: 0;
                margin: 20px 0;
                border-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td {
            display: inline;
        }

        .pagination-ys table > tbody > tr > td > a,
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }

        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;    
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }

        .pagination-ys table > tbody > tr > td:first-child > a,
        .pagination-ys table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td:last-child > a,
        .pagination-ys table > tbody > tr > td:last-child > span {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td > a:hover,
        .pagination-ys table > tbody > tr > td > span:hover,
        .pagination-ys table > tbody > tr > td > a:focus,
        .pagination-ys table > tbody > tr > td > span:focus {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
        .ui-dialog {
            z-index:1000000000;
            top: 0; left: 0;
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
            background:white;
        }
    </style>
           
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
        
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo OTS5.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Consulta de Pendientes</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>            
            <% 
                for (int i=1; i <= GridView1.Rows.Count; i++)
                {
                     
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            if (row.Cells[5].Text.Equals("Terminada") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#81c784");
                                row.Cells[8].Text = "";
                            }
                            else if (row.Cells[5].Text.Equals("Desactivada") == true)
                            {
                                row.Cells[4].Text = "";
                            }
                            else if (row.Cells[5].Text.Equals("Pausa") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA024");
                                row.Cells[4].Text = "";
                                int numOTSPausa = Int32.Parse(row.Cells[0].Text);
                            }
                            else if (row.Cells[5].Text.Equals("Iniciada") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe0b2");
                                row.Cells[4].Text = "";
                                int numOTSPausa = Int32.Parse(row.Cells[0].Text);
                            }
                            else if (row.Cells[5].Text.Equals("No Iniciada") == true)
                            {
                                row.Cells[4].Text = "";
                            }
                        }
                }
            %>
            <br />
            <div class="row">
                <blockquote>
                    <h5 class="left-align">Filtrado de Resultados</h5>
                </blockquote>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <asp:label ID="lblRespon" runat="server" Text="Responsable:"></asp:label>
                    <asp:DropDownList ID="cmbProgramador" runat="server" OnSelectedIndexChanged="cmbProgramador_SelectedIndexChanged" CssClass="browser-default"></asp:DropDownList>
                </div>
                <div class="input-field col s6">
                    <asp:label ID="lblDescr" runat="server" Text="Descripcion OTS:" CssClass="active"></asp:label>
                    <asp:TextBox ID="descripcion" runat="server" onkeydown="return tab_btn(event);" AutoPostBack="true" OnTextChanged="descripcion_TextChanged"></asp:TextBox>
                    <asp:HiddenField ID="hfCustomerId" runat="server" />
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" HeaderStyle-BackColor="#042644" HeaderStyle-ForeColor="White" 
                        runat="server" AutoGenerateColumns="false" CssClass="responsive-table"
                        PageSize="5" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No hay resultados" >
                        <HeaderStyle BackColor="#042644" Font-Bold="True" />
                        <pagerstyle CssClass="pagination-ys" />
                        <AlternatingRowStyle BackColor="#E4EDF6" />
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="num_OTS" HeaderText="Folio:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="tipo_OTS" HeaderText="Tipo OTS:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="userResp" HeaderText="Responsable:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="fec_asig" HeaderText="Fecha Asignada:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="fec_fin" HeaderText="Fecha Final:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="sts_prog" HeaderText="Status:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="aplica" HeaderText="Aplica Para:" ItemStyle-Width="150" />
                            <asp:BoundField DataField="fec_prom" HeaderText="Fecha Prometida:" ItemStyle-Width="150" />
                            
                            <asp:TemplateField HeaderText="Detalles OTS:" ItemStyle-Width="900">
                                <ItemTemplate>
                                    <div style="position: relative; height: 70px; text-align:left;">
                                        <div class="fixed-action-btn horizontal click-to-toggle tooltipped" data-position="top" data-delay="50" data-tooltip="Detalles" style="position: relative; display: inline-block; height: 70px;">
                                            <a class="btn-floating btn-large red darken-4"><i class="material-icons">toc</i></a>
                                            <ul>
                                                <li><a class="btn-floating green darken-3 click-to-toggle tooltipped" id="btnAgregar" data-position="top" data-delay="50" data-tooltip="Agrega Sub OTS"><i class="material-icons">add</i></a></li>
                                                <li><a class="btn-floating cyan darken-4 click-to-toggle tooltipped" id="subReng" data-position="top" data-delay="50" data-tooltip="Sub OTS"><i class="material-icons">message</i></a></li>
                                                <li><a class="btn-floating green darken-3 click-to-toggle tooltipped" id="imgOTS" data-position="top" data-delay="50" data-tooltip="Img. OTS"><i class="material-icons">perm_media</i></a></li>
                                                <li><a class="btn-floating red darken-3 click-to-toggle tooltipped" id="btnReasignar" data-position="top" data-delay="50" data-tooltip="Reasigna"><i class="material-icons">replay</i></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                <br />
            </div>
        </div>
    </div>

    <script type="text/javascript" src="<%= ResolveUrl("js/pickadate.legacy.js") %>"></script>
    <script type="text/javascript">
        $('[type=date], .datepicker').pickadate()
    </script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" /> 
    <script type="text/javascript">
        var user_cveF = $('#<%= cmbProgramador.ClientID %>').val();
        if (user_cveF == null) {
            user_cveF = $('#<%= user_cve.ClientID %>').val();
        }
        $(function () {
            $("[id$=descripcion]").autocomplete({
                source: function (request, response) {
                    AjaxCall("C_Pendientes.aspx/GetDescripcion", request.term, 0, response, user_cveF)
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });
        function AjaxCall(url, prefix, parentId, response, user_cveF) {
            $.ajax({
                url: url,
                data: "{ 'prefix': '" + prefix + "', parentId: " + parentId + ", 'user_cveF': '" + user_cveF + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('-')[0],
                            val: item.split('-')[1]
                        }
                    }))
                },
                error: function (response) {
                    alert(r.responseText);
                },
                failure: function (response) {
                    alert(r.responseText);
                }
            });
        }
        function tab_btn() {
            document.getElementById('txtName').focus();
            __doPostBack("txtName", "TextChanged");
            return false;
        }
    </script>
</asp:Content>
