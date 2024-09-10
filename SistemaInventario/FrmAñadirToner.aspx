<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmAñadirToner.aspx.cs" Inherits="SistemaInventario.FrmAñadirToner" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Añadir Toners</title>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%;">
        <h1>Añadir Toners</h1>
        <div>
            <!-- SqlDataSource para la búsqueda -->
            <asp:SqlDataSource ID="SqlDataSourceTonerBuscar" runat="server"
                ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                SelectCommand="SELECT [IDToner], [TipoToner], [CPC], [Repuesto], [Stock], [AComprar], [ValorUnitario], [CodSuministro], [Bodega], [CodigoYupak] FROM [IT_Toners] WHERE [TipoToner] LIKE '%' + @TipoToner + '%'">
                <SelectParameters>
                    <asp:Parameter Name="TipoToner" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

            <div class="scrollable-gridviewModal">
                <asp:Label ID="BuscarToner" runat="server" Text="Buscar Toner:"></asp:Label>
                <asp:TextBox ID="TextBuscarToner" runat="server"></asp:TextBox>
                <asp:Button ID="BtnBuscarToner" runat="server" Text="Button" OnClick="BtnBuscarToner_Click" />



                <!-- TABLA-->

                <asp:TextBox ID="TextAreaIDToners" runat="server" TextMode="MultiLine" Rows="5" Columns="30" />

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT * FROM [IT_Toners]"></asp:SqlDataSource>

                <asp:GridView ID="GridViewTonerModal" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="IDToner" DataSourceID="SqlDataSource1" GridLines="None" AllowPaging="True" AutoPostBack="True" OnRowCommand="GridViewTonerModal_RowCommand">
                    <RowStyle BackColor="#E3EAEB" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IDToner" HeaderText="IDToner" SortExpression="IDToner" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="TipoToner" HeaderText="TipoToner" SortExpression="TipoToner" />
                        <asp:BoundField DataField="CPC" HeaderText="CPC" SortExpression="CPC" />
                        <asp:CheckBoxField DataField="Repuesto" HeaderText="Repuesto" SortExpression="Repuesto" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                        <asp:BoundField DataField="AComprar" HeaderText="AComprar" SortExpression="AComprar" />
                        <asp:BoundField DataField="ValorUnitario" HeaderText="ValorUnitario" SortExpression="ValorUnitario" />
                        <asp:BoundField DataField="CodSuministro" HeaderText="CodSuministro" SortExpression="CodSuministro" />
                        <asp:BoundField DataField="Bodega" HeaderText="Bodega" SortExpression="Bodega" />
                        <asp:BoundField DataField="CodigoYupak" HeaderText="CodigoYupak" SortExpression="CodigoYupak" />

                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <RowStyle BackColor="LightGray" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>

            </div>
        </div>

    </form>
</body>
</html>


