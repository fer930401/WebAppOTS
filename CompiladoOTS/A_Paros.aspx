<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="A_Paros.aspx.cs" Inherits="materialDesing.A_Paros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function quitaValidacion() {
            //alert("quito validacion");
            $("#form1").attr("novalidate", "novalidate");
        }
    </script>
    <% int numOTS = LogicaNegocio.variables.Num_OTS; %>
    <div class="container">        
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <div class="container">
                    <h5>Paro de OTS</h5>
                    <asp:TextBox ID="variableSession" runat="server" hidden></asp:TextBox><br />
                    <div class="row">
                        <div class="input-field col s11">
                            <label for="num_otsTB">Id OTS:</label>
                            <asp:TextBox ID="num_otsTB" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <label for="numReng">Renglon OTS:</label>
                            <asp:TextBox ID="num_reng" runat="server" ReadOnly="true" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <label for="tipOTSTB">Tipo OTS:</label>
                            <asp:TextBox ID="tipOTSTB" runat="server"  ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <label>Status OTS:</label>
                            <asp:TextBox ID="statusOTS" runat="server"  ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" <% Response.Write(Session["visibleSD" + numOTS]); %>>
                        <div class="input-field col s11">
                            <asp:TextBox ID="motivo" runat="server" CssClass="validate" length="254" required></asp:TextBox><br />
                            <label >Motivo de Paro:</label>
                        </div>
                    </div>
                    
                    <div class="">
                        <br />
                        <div class="row">
                          <asp:Button ID="btnAltaParo" runat="server" Text="Guardar Paro"  CssClass="waves-effect waves-green btn amber darken-4" OnClick="btnAltaParo_Click" />
                          <asp:Button ID="btnReanudarParo" runat="server" Text="Reanudar Actividad"  CssClass="waves-effect waves-green btn green darken-4" OnClick="btnReanudarParo_Click" OnClientClick="quitaValidacion()"/>
                          <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"  CssClass="waves-effect waves-green btn red darken-4" OnClick="btnCancelar_Click" OnClientClick="quitaValidacion()"/>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
                </div>
            </div>
        </div>
</asp:Content>
