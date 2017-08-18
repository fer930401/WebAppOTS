<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerminaOTS.aspx.cs" Inherits="materialDesing.TerminaOTS" %>

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
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="input-field col s12">
                    <label for="solucion">Solucion:</label>
                    <textarea id="solucion" name="solucion" class="materialize-textarea" length="254" required> </textarea>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <asp:dropdownlist ID="cls" runat ="server" DataTextField="nombre" DataValueField="elm_cve" CssClass="browser-default" ></asp:dropdownlist>  
                    <label for="clas">Clasificacion:</label>                  
                </div>
            </div>
            <div class="row">
                <div class="col s6">
                    <asp:Button ID="btnTerminar" runat="server" Text="Terminar" CssClass="btn green darken-4" OnClick="btnTerminar_Click" />
                    <a class="btn red darken-4 white-text" href="C_subOTS.aspx">Cancelar</a>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>
</body>
</html>
