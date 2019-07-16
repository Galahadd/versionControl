<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="versionControl.aspx.cs" Inherits="versionUpdate1.versionControl" %>

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
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="listele" />
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Versions" HeaderText="Versions" SortExpression="Versions" />
                <asp:BoundField DataField="Tarih" HeaderText="Tarih" SortExpression="Tarih" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="310px">
            <Columns>
                <asp:BoundField DataField="Ad" HeaderText="Ad" SortExpression="Ad" />
                <asp:BoundField DataField="Soyad" HeaderText="Soyad" SortExpression="Soyad" />
                <asp:BoundField DataField="Yas" HeaderText="Yas" SortExpression="Yas" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProgramConnectionString %>" SelectCommand="SELECT * FROM [Uyeler]"></asp:SqlDataSource>
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    </form>
</body>
</html>
