<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_Usuarios.aspx.cs" Inherits="materialDesing.C_Usuarios" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo_OTS.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Consulta de Usuarios</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <a href="A_Usuarios.aspx" class="btn-floating btn-large waves-effect waves-light amber darken-4 tooltipped" data-position="top" data-delay="50" data-tooltip="Agrega un nuevo usuario"><i class="material-icons">add</i></a>
            
            <br />
            <br />
            <div class="row">
                <div class="col s12 floating">
                    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#042644" HeaderStyle-ForeColor="White"
                        runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                        OnRowEditing="OnRowEditing" datakeynames="Clave">
                        <HeaderStyle BackColor="#042644" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#E4EDF6" />
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="Clave" HeaderText="Clave" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Rol" HeaderText="Rol" ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
