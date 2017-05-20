<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="materialDesing.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 324px">
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Hyperlink" Target="_blank" NavigateUrl="~/Login.aspx">HyperLink</asp:HyperLink>
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="Media/skytex.png"/>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Control Label"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="RadioButton1" runat="server" Text="Opcion 1" GroupName="pruebaRadio"  />
        <asp:RadioButton ID="RadioButton2" runat="server" Text="Opcion 2" GroupName="pruebaRadio"/>
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Text="TextBox" TextMode="Number"></asp:TextBox>
        <br />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" accept=".pdf" AllowMultiple="true"/>
        <br />
        <br />
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <li><%# Eval("nombre") %></li>
            </ItemTemplate>
        </asp:DataList>
        <br />
        <br />
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <li> <%# Eval("user_cve") %> </li>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
        <br />

    </div>
    </form>
</body>
</html>
