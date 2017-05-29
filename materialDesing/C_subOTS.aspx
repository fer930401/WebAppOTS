<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_subOTS.aspx.cs" Inherits="materialDesing.C_subOTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <!--<script src="http://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.5/js/materialize.min.js"></script>-->
    <!--<script src="htp://ajax.aspnetcdn.com/ajax/jQuery/jquer-1.10.0.min.js" type="text/javascript"></script>-->
    <script type="text/javascript">
        $(function () {
            $("#<%=GridView1.ClientID%> [id='hijosReng']").on("click", function () {
                var tr = $(this).parent().parent().parent().parent().parent().parent();
                var id_OTS = $("td:eq(0)", tr).html();
                var tip_OTS = $("td:eq(1)", tr).html();
                var dWidth = $(window).width() * 0.9;
                var dHeight = $(window).height() * 0.9;
                //alert(id_OTS);
                $('<div>').dialog({
                    modal: true,
                    open: function () {
                        $(this).load('C_HijosOTS.aspx?num_OTS=' + id_OTS + '&tip_OTS=' + tip_OTS);
                    },
                    width: dWidth,
                    height: dHeight,
                    draggable: false,
                    responsive: true
                });
            });
        });
        $(function () {
            $("#<%=GridView1.ClientID%> [id='imgOTS']").on("click", function () {
              var tr = $(this).parent().parent().parent().parent().parent().parent();
              var id_OTS = $("td:eq(0)", tr).html();
              var tip_OTS = $("td:eq(1)", tr).html();
              var dWidth = $(window).width() * 0.9;
              var dHeight = $(window).height() * 0.9;
              //alert('C_imgOTS.aspx?num_OTS=' + id_OTS + '&tip_OTS=' + tip_OTS);
              $('<div>').dialog({
                  modal: true,
                  open: function () {
                      $(this).load('C_imgOTS.aspx?num_OTS=' + id_OTS + '&tip_OTS=' + tip_OTS);
                  },
                  width: dWidth,
                  height: dHeight,
                  draggable: false,
                  responsive: true
              });
          });
      });

      $(document).ready(function () {
          // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
          $('.modal-trigger').leanModal();
      });

      function confirmar() {
          if (confirm('Desea eliminar este soporte?')) {
              alert("Soporte Eliminado");
              window.location.href = 'C_Soportes.aspx';
          } else {
              return false;
          }
      }
      /*$(document).ready(function () {
          $("#<=GridView1.ClientID%> [id='paroOTS']").click(function () {
              var tr = $(this).parent().parent();
              var id_OTS = $("td:eq(0)", tr).html();
              var tip_OTST = $("td:eq(1)", tr).html();
              var status_OTST = $("td:eq(4)", tr).html();
              '<Session["variableSession"] = " id_OTS "; %>';
            //alert(status_OTST);
            window.location.href = "A_Paros.aspx?num_OTS=" + id_OTS + "&status=" + status_OTST + "&tipoOTS=" + tip_OTST;
        });*/
      //});
      $(document).ready(function () {
          $('select').material_select();
      });
    </script>
    <style>
        .pagination-ys {
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
    </style>

    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">

        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100" />
                <br />
                <h4 class="center grey-text">Consulta SubOTS</h4>
                <div class="col s12 m3">
                    <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                    <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                    <asp:TextBox ID="rol_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
                    <asp:Button ID="soportes" runat="server" Text="Ir a soportes" OnClick="soportes_Click" CssClass="waves-effect waves-light btn  green darken-4" />
                </div>
            </div>
            <% 
                for (int i = 1; i <= GridView1.Rows.Count; i++)
                {
                    if (rol_cve.Text == "PRG")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            if (row.Cells[5].Text.Equals("Terminada") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#6CBD72");
                                row.Cells[8].Text = "";
                                row.Cells[9].Text = "";
                                row.Cells[10].Text = "";
                                row.Cells[11].Text = "";
                            }
                            else if (row.Cells[5].Text.Equals("Desactivada") == true)
                            {
                                row.Cells[4].Text = "";
                                row.Cells[8].Text = "";
                                row.Cells[9].Text = "";
                                row.Cells[10].Text = "";
                                row.Cells[11].Text = "";
                                /* si hay alguna OTS desactivada los botones de ocultan */
                            }
                            else if (row.Cells[5].Text.Equals("Pausa") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA024");
                                row.Cells[4].Text = "";
                                row.Cells[8].Text = "";
                                row.Cells[9].Text = "";
                                row.Cells[10].Text = "";
                                int numOTSPausa = Int32.Parse(row.Cells[0].Text);
                            }
                            else if (row.Cells[5].Text.Equals("Iniciada") == true)
                            {
                                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe0b2");
                                row.Cells[8].Text = "";
                                row.Cells[11].Text = "";
                                row.Cells[4].Text = "";
                                int numOTSPausa = Int32.Parse(row.Cells[0].Text);
                            }
                            else if (row.Cells[5].Text.Equals("No Iniciada") == true)
                            {
                                row.Cells[4].Text = "";
                                row.Cells[9].Text = "";
                                row.Cells[10].Text = "";
                                row.Cells[11].Text = "";
                            }
                        }
                    }
                    else
                    {

                    }
                }
            %>
            <div class="row">
                <div class="col s8">
                    <table>
                        <thead>
                            <tr>
                                <th>Tipo OTS:</th>
                                <th>Descripcion</th>
                                <th>Aplica para:</th>
                                <th>Status:</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoOts" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAplica" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <br />
            <div class="col s12">
                <asp:GridView ID="GridView1" HeaderStyle-BackColor="#042644" HeaderStyle-ForeColor="White"
                    runat="server" AutoGenerateColumns="false" CssClass="responsive-table"
                    PageSize="5" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No hay resultados">
                    <HeaderStyle BackColor="#042644" Font-Bold="True" />
                    <PagerStyle CssClass="pagination-ys" />
                    <AlternatingRowStyle BackColor="#E4EDF6" />
                    <FooterStyle BackColor="Tan" />
                    <Columns>
                        <asp:BoundField DataField="num_OTS" HeaderText="Folio:" />
                        <asp:BoundField DataField="tipo_OTS" HeaderText="Tipo OTS:" />
                        <asp:BoundField DataField="userResp" HeaderText="Responsable:" />
                        <asp:BoundField DataField="fec_asig" HeaderText="Fecha Asignada:" />
                        <asp:BoundField DataField="fec_fin" HeaderText="Fecha Final:" />
                        <asp:BoundField DataField="sts_prog" HeaderText="Status:" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion:" />
                        <asp:BoundField DataField="operacion" HeaderText="Operacion:" />
                        <asp:TemplateField HeaderText="Iniciar:">
                            <ItemTemplate>
                                <asp:Button Text="Iniciar" ID="iniciar" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn  green darken-4" OnClick="iniciar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Terminar:">
                            <ItemTemplate>
                                <asp:Button Text="Terminar" ID="terminar" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn  green darken-4" OnClick="terminar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paro Proceso:">
                            <ItemTemplate>
                                <asp:Button Text="Paro" ID="paro" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn red darken-4" OnClick="paro_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reanudar Proceso:">
                            <ItemTemplate>
                                <asp:Button Text="Continuar" ID="continuar" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn red darken-4" OnClick="continuar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
