<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmPrincipal.aspx.cs" Inherits="SistemaInventario.FormPrincipal" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function handleEnterKey(event) {
            if (event.key === 'Enter') {
                event.preventDefault(); // Previene el comportamiento por defecto de Enter
                document.getElementById('<%= btnBuscar.ClientID %>').click(); // Simula el clic en el botón
            }
        }
    </script>

    <div style="height: 500px; width: 100%">
        <table class="auto-style3">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Buscar: "></asp:Label>
                    <asp:TextBox ID="txtBuscar" runat="server" onkeydown="handleEnterKey(event)"></asp:TextBox>
                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btnEquipo" ToolTip="Buscar" OnClick="btnBuscar_Click">
                    <i class="fa-solid fa-magnifying-glass"></i>
                    </asp:LinkButton>
                </td>
                <td class="auto-style2">
                    <asp:LinkButton ID="btnNuevoEquipo" runat="server" CssClass="btnEquipo" ToolTip="Nuevo Equipo" OnClick="btnNuevoEquipo_Click">
                    <i class="fa-solid fa-plus"></i>
                    </asp:LinkButton>
                </td>
                <td style="width: 100px;">
                    <asp:Label ID="Label3" runat="server" Text="Registros:   "></asp:Label>
                    <asp:Label ID="lblRegistros" runat="server" Text="____"></asp:Label>
                </td>

            </tr>
        </table>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [CodigoActivo], [EquipoTipo], [Nombres], [Ubicacion], [Marca], [Modelo], [IDEquipo], [Imagenes] FROM [ViewEquipos] WHERE ([CodigoActivo] LIKE '%' + '%') OR ([Nombres] LIKE '%' + '%')"></asp:SqlDataSource>
        <div>
            <asp:GridView ID="GridViewEquipos" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridViewEquipos_RowDataBound" CellPadding="5" BorderStyle="Double" DataKeyNames="IDEquipo" DataSourceID="SqlDataSource1" Font-Size="11pt" ForeColor="#333333" GridLines="None" OnDataBound="GridViewEquipos_DataBound" OnSelectedIndexChanged="GridViewEquipos_SelectedIndexChanged" Width="100%">
                <RowStyle BackColor="#E3EAEB" />
                <Columns>
                    <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
                    <asp:BoundField DataField="EquipoTipo" HeaderText="EquipoTipo" SortExpression="EquipoTipo" />
                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" />
                    <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                    <asp:BoundField DataField="IDEquipo" HeaderText="IDEquipo" SortExpression="IDEquipo" />
                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                    <asp:BoundField DataField="Imagenes" HeaderText="Imagenes" SortExpression="Imagenes" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <RowStyle BackColor="LightGray" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 229px
        }
        .auto-style2 {
            width: 81px;
        }
        .auto-style3 {
            width: 40%;
            height: 54px;
        }
    </style>
</asp:Content>


