<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="C_Usuarios.aspx.cs" Inherits="materialDesing.C_Usuarios" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        usuario.Text = Session["nombre"].ToString();
        user_cve.Text = Session["user_cve"].ToString().ToUpper();
    %>
    <script type="text/javascript">
        $(document).ready(function () {
            // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
            $('.modal-trigger').leanModal();
        });
        function confirmar() {
            if (confirm('Desea eliminar este soporte?')) {
                alert("Soporte Eliminado");
                window.location.href = 'C_Usuarios.aspx';
            } else {
                return false;
            }
        }
        function asignarValores() {
            document.getElementById("<%= Ncve.ClientID %>").value = document.getElementById("cve_usr").value;
            document.getElementById("<%= Nrol.ClientID %>").value = document.getElementById("<%= rol_cve.ClientID %>").value;
            e = jQuery.Event("keypress")
            e.keyCode = 13 //choose the one you want
            $("#cve_usr").keypress(function () {
                //alert('keypress triggered')
            }).trigger(e);
            $("#rol_cve").keypress(function () {
                //alert('keypress triggered')
            }).trigger(e);
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
              <img class="right responsive-img" src="<%=ResolveUrl("~/Media/logo OTS5.png") %>" width="350" height="100"/>
              <br />
              <h4 class="center grey-text">Consulta de Usuarios</h4>
              <div class="col s12 m3">
                <asp:TextBox ID="usuario" runat="server" ReadOnly="true" hidden></asp:TextBox>
                <asp:TextBox ID="user_cve" runat="server" ReadOnly="true" hidden></asp:TextBox>
              </div>
            </div>
            <a href="A_Usuarios.aspx" class="btn-floating btn-large waves-effect waves-light amber darken-4 tooltipped" data-position="top" data-delay="50" data-tooltip="Agrega un nuevo usuario"><i class="material-icons">add</i></a>
            <%-- <a class="waves-effect waves-light btn-floating btn-large green darken-2 modal-trigger tooltipped" href="#modal1" data-tooltip="Agrega un nuevo rol a un usuario"><i class="large material-icons">mode_edit</i></a>--%>

            <!-- Modal Structure -->
            <div id="modal1" class="modal">
                <div class="modal-content">
                    <h5>Agrega Un Nuevo Rol</h5>
                    <div class="row">
                        <div class="input-field col s11">
                            <asp:TextBox ID="cve_usr" runat="server" CssClass="validate mayusculas" required></asp:TextBox><br />
                            <label for="cve_usr">Clave del Usuario (User_cve)</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s11">
                            <label for="rol_cve">Rol del Usuario:</label><br /><br />
                            <asp:dropdownlist id ="rol_cve" runat ="server" DataTextField="nombre"  DataValueField="elm_cve" ></asp:dropdownlist>
                        </div>
                    </div>
                
                    <div class="input-field col s4 right">
                        <asp:Button ID="btnRol" runat="server" Text="Agregar Rol" OnClick="btnRol_Click" CssClass="modal-action modal-close waves-effect waves-green btn green darken-4"/>
                        <asp:Button ID="btnERol" runat="server" Text="Eliminar Rol" OnClick="btnERol_Click" CssClass="modal-action modal-close waves-effect waves-green btn red darken-4"/>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <asp:TextBox ID="Ncve" runat="server" CssClass="mayusculas" Style="display:none;"></asp:TextBox>
            <asp:TextBox ID="Nrol" runat="server" AutoPostBack="True" CssClass="mayusculas" OnTextChanged="Nrol_TextChanged" Style="display:none;"></asp:TextBox>
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
                            
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:LinkButton Text="Actualizar" runat="server" OnClick = "OnUpdate"/>
                                    <asp:LinkButton Text="Eliminar" runat="server" OnClick = "OnDelete"/>
                                    <asp:LinkButton Text="Cancelar" runat="server" OnClick = "OnCancel"/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
