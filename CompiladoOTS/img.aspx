<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="img.aspx.cs" Inherits="materialDesing.img" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="<%=ResolveUrl("~/Media/skytex.ico") %>" />
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
    <link href="<%=ResolveUrl("css/materialize.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="<%=ResolveUrl("css/style.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.1.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/materialize.js") %>"></script>
    
    <script type="text/javascript" src="<%= ResolveUrl("js/highcharts.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/grid-light.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("js/WebNotifications.js") %>"></script>
    <link href="<%=ResolveUrl("css/pickadate.01.default.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="<%=ResolveUrl("css/rtl.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="<%=ResolveUrl("css/default.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="<%=ResolveUrl("css/default.date.css") %>" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.0/animate.min.css"/>

    <link rel="stylesheet" href="<%=ResolveUrl("css/normaliz.min.css") %>"/>
    <link rel="stylesheet" href="<%=ResolveUrl("css/animate.min.css") %>"/>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.carousel').carousel();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="col s12 card-panel grey lighten-5 z-depth-1">
            <div class="row">
                <h4 class="center grey-text">Imagenes OTS</h4>
            </div>
            <div class="row">
                <div class="col s12 m4 l2"><p>.</p></div>
                <div class="col s12 m4 l8">
                    <p>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader = "false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src='<%# ResolveUrl(Eval("ImageUrl").ToString()) %>' class="materialboxed center" width="500" height="500" alt='<%# ResolveUrl(Eval("ImageName").ToString()) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </p>
                </div>
                <div class="col s12 m4 l2"><p>.</p></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
