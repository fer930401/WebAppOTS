<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_imgOTS.aspx.cs" Inherits="materialDesing.C_imgOTS1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
            <div class="col s12 card-panel grey lighten-5 z-depth-1">
                <div class="row">
                    <h4 class="center grey-text">Capturas del OTS</h4>
                </div>
                <div class="row">
                    <div class="col s12 center">
                        <h5>Descripcion: <strong>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong></h5>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="responsive-table2" EmptyDataText="No hay resultados">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src='<%# ResolveUrl(Eval("ImageUrl").ToString()) %>' class="materialboxed center responsive-img" alt='<%# ResolveUrl(Eval("ImageName").ToString()) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Button ID="soportes" runat="server" Text="Ir a soportes" OnClick="soportes_Click" CssClass="waves-effect waves-light btn  green darken-4" />
            </div>
        </div>
</asp:Content>
