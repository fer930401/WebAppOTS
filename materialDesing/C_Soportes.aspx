<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_Soportes.aspx.cs" Inherits="materialDesing.C_Soportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
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
              //alert(id_OTS);
              window.location.href = "A_Detalle.aspx?num_OTS=" + id_OTS;
          });
      });
      $(function () {
          $("#<%=GridView1.ClientID%> [id='btnReasignar']").click(function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();;
                    var id_OTS = $("td:eq(0)", tr).html();
                    var tip_OTS = $("td:eq(1)", tr).html();
                    var nom_resp = $("td:eq(2)", tr).html();
                    var tipoReasigna = 1;
                    var ots_padre = 0;
                    var dWidth = $(window).width() * 0.9;
                    var dHeight = $(window).height() * 0.9;
                    //alert(id_OTS);
                    $('<div>').dialog({
                        modal: true,
                        open: function () {
                            $(this).load('ReasignaOTS.aspx?num_OTS=' + id_OTS + '&ots_padre=' + ots_padre + '&tip_OTS=' + tip_OTS + '&opc=' + tipoReasigna);
                        },
                        width: dWidth,
                        height: dHeight,
                        draggable: false,
                        responsive: true,
                        title: 'reasignar ots',
                        close: function(e, ui) {
                            window.location.href = 'C_Soportes.aspx';
                        }
                    })
                });
      });
    </script>
    <style>
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
        .ui-dialog .ui-dialog-titlebar {
            background:#042644;
            border-color:#042644;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
        
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Consulta de Soportes</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" ></asp:TextBox>
              </div>
            </div>  
            <script>
                //document.getElementById("btnReasignar").style.display = "none"
            </script>          
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
                            <asp:BoundField DataField="descripcion" HeaderText="Descripcion:" ItemStyle-Width="150" />
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
                                                <li><a class="btn-floating green darken-3 click-to-toggle tooltipped" id="btnAgregar" data-position="top" data-delay="50" data-tooltip="Agrega Sub OTS" <% Response.Write(Session["visibleAgregar"]); %>><i class="material-icons">add</i></a></li>
                                                <li><a class="btn-floating cyan darken-4 click-to-toggle tooltipped" id="subReng" data-position="top" data-delay="50" data-tooltip="Sub OTS"><i class="material-icons">message</i></a></li>
                                                <!--<li><a class="btn-floating green darken-3 click-to-toggle tooltipped" id="imgOTS" data-position="top" data-delay="50" data-tooltip="Img. OTS"><i class="material-icons">perm_media</i></a></li>-->
                                                <li><a class="btn-floating red darken-3 click-to-toggle tooltipped" id="btnReasignar" data-position="top" data-delay="50" data-tooltip="Reasigna" <% Response.Write(Session["visibleReasigna"]); %>><i class="material-icons">replay</i></a></li>
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
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" /> 
</asp:Content>
