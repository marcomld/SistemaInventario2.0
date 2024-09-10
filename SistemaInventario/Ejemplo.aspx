<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ejemplo.aspx.cs" Inherits="SistemaInventario.Ejemplo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IDToner" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="IDToner" HeaderText="IDToner" ReadOnly="True" SortExpression="IDToner" InsertVisible="False" />
                            <asp:BoundField DataField="TipoToner" HeaderText="TipoToner" SortExpression="TipoToner" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                            <asp:BoundField DataField="Bodega" HeaderText="Bodega" SortExpression="Bodega" />
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                SelectCommand="SELECT [IDToner], [TipoToner], [Stock], [Bodega] FROM [IT_Toners]"></asp:SqlDataSource>
        </div>

        <script type="text/javascript">
            var singleClickTimer;

            function rowClick(row, productId) {
                clearTimeout(singleClickTimer);
                singleClickTimer = setTimeout(function () {
                    // Llamar a la función de un solo clic
                    __doPostBack('<%= UpdatePanel1.ClientID %>', 'SingleClick:' + productId);
            }, 250);
            }

            function rowDoubleClick(row, productId) {
                clearTimeout(singleClickTimer);
                // Llamar a la función de doble clic
                __doPostBack('<%= UpdatePanel1.ClientID %>', 'DoubleClick:' + productId);
            }
    </script>
    </form>
</body>
</html>
