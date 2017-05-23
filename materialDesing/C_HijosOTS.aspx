<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_HijosOTS.aspx.cs" Inherits="materialDesing.C_HijosOTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
                 <br />
                <h4 class="center grey-text">Hijos de OTS</h4>
            </div> 
            <div> 
                <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" 
                        runat="server" AutoGenerateColumns="false" CssClass="responsive-table"
                         EmptyDataText="No hay resultados" >
                        <HeaderStyle BackColor="#1565c0" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#E4EDF6" />
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="num_OTS" HeaderText="Folio:"  />
                            <asp:BoundField DataField="tipo_OTS" HeaderText="Tipo OTS:"/>
                            <asp:BoundField DataField="operacion" HeaderText="Operacion:" />
                            <asp:BoundField DataField="userResp" HeaderText="Responsable:"  />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripcion:" />
                            <asp:BoundField DataField="fec_asig" HeaderText="Fecha Inicial:"  />
                            <asp:BoundField DataField="fec_fin" HeaderText="Fecha Final:"  />
                            <asp:BoundField DataField="aplica" HeaderText="Aplica Para:"  />
                            <asp:BoundField DataField="sts_prog" HeaderText="Status:"  />
                            <asp:TemplateField HeaderText="Iniciar:" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Button Text="Iniciar" ID="lnkSelect" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn  green darken-4" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Terminar:" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Button Text="Terminar" ID="lnkSelect2" runat="server" CommandName="Select" CssClass="waves-effect waves-light btn  green darken-4" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paro Proceso:" ItemStyle-Width="350">
                                <ItemTemplate>
                                    <a class="waves-effect waves-light btn red darken-4  tooltipped" id="paroOTS" data-tooltip="Para la actividad">Parar / Continuar</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reanudar Proceso:" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <a class="waves-effect waves-light btn red darken-4 modal-trigger tooltipped" id="paroOTS" href="#modal1" data-tooltip="Continuar la actividad">Continuar</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
            <br />
            </div>
        </div>
    </div>
</asp:Content>
