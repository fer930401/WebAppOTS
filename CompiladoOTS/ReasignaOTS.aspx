<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReasignaOTS.aspx.cs" Inherits="materialDesing.ReasignaOTS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
    <link href="<%=ResolveUrl("css/materialize.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="<%=ResolveUrl("css/style.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.1.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/materialize.js") %>"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <label>Num. de OTS:</label>
                <asp:TextBox ID="txtNumOTS" runat="server" CssClass="validate" ReadOnly="true" required></asp:TextBox>
            </div>
            <div class="row">
                <label>Tipo OTS:</label>
                <asp:TextBox ID="txtTipoOTS" runat="server" CssClass="validate" ReadOnly="true" required></asp:TextBox>
            </div>
            <div class="row">
                <label>Responsable:</label>
                <asp:TextBox ID="txtResponsable" runat="server" CssClass="validate" ReadOnly="true" required></asp:TextBox>
            </div>
            <div class="row">
                <label>Nuevo Responsable:</label>
                <asp:DropDownList ID="cmbResponsable" runat="server" CssClass="browser-default" required></asp:DropDownList>
            </div>
            <div class="row">
                <asp:Button ID="btnReasignar" runat="server" Text="Reasignar" CssClass="green darken-4 btn" OnClick="btnReasignar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
