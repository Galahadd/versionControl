<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="versionControl.aspx.cs" Inherits="versionUpdate1.versionControl" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProgramConnectionString %>" SelectCommand="SELECT * FROM [Version]"></asp:SqlDataSource>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="listele" meta:resourcekey="Button1Resource1" />
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" meta:resourcekey="GridView1Resource1">
            <Columns>
                <asp:BoundField DataField="Versions" HeaderText="Versions" SortExpression="Versions" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="Tarih" HeaderText="Tarih" SortExpression="Tarih" meta:resourcekey="BoundFieldResource2" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="310px" meta:resourcekey="GridView2Resource1">
            <Columns>
                <asp:BoundField DataField="Ad" HeaderText="Ad" SortExpression="Ad" meta:resourcekey="BoundFieldResource3" />
                <asp:BoundField DataField="Soyad" HeaderText="Soyad" SortExpression="Soyad" meta:resourcekey="BoundFieldResource4" />
                <asp:BoundField DataField="Yas" HeaderText="Yas" SortExpression="Yas" meta:resourcekey="BoundFieldResource5" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProgramConnectionString %>" SelectCommand="SELECT * FROM [Uyeler]"></asp:SqlDataSource>
        <asp:ListBox ID="ListBox1" runat="server" meta:resourcekey="ListBox1Resource1"></asp:ListBox>
    </form>
</body>
</html>
