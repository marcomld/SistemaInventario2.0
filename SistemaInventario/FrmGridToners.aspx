<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmGridToners.aspx.cs" Inherits="SistemaInventario.FrmGridToners" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function() {
        // Obtén el TextBox y el LinkButton
        var txtBuscar = document.getElementById('<%= txtBuscar.ClientID %>');
        var btnBuscar = document.getElementById('<%= btnBuscar.ClientID %>');

        // Agrega un listener para la tecla Enter en el TextBox
        txtBuscar.addEventListener('keydown', function(event) {
            if (event.key === 'Enter') {
                // Prevenir el comportamiento por defecto del Enter
                event.preventDefault();
                // Simular un clic en el LinkButton
                btnBuscar.click();
            }
        });
    });
</script>

    <div style="height: 500px; width: 100%">
        <table style="width: 100%; height: 54px;">
            <tr>
                <td style="width: 300px;">
                    <div class="form-group" style="display: flex; align-items: center;">
                        <asp:Label ID="Label2" runat="server" Text="Buscar: "></asp:Label>
                        <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btnEquipo" ToolTip="Buscar"  OnClick="btnBuscar_Click">
                        <i class="fa-solid fa-magnifying-glass"></i>
                        </asp:LinkButton>

                    </div>
                </td>
                <td style="width: 100px;">
                    <asp:Label ID="Label3" runat="server" Text="Registros:   "></asp:Label>
                    <asp:Label ID="lblRegistros" runat="server" Text="____"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSourceToners" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT * FROM [IT_Toners]"></asp:SqlDataSource>
        <div>
            <%--Si se modifica el GridView la configuración se va a resetear y va a dejar de funcionar el SelectIndex
                para solucionar esto pegue la siguiente linea dentro de las columnas del GridView
                <asp:CommandField SelectText="" ShowSelectButton="True" />--%>
            <asp:GridView ID="GridToners" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridViewEquipos_RowDataBound" CellPadding="4" DataKeyNames="IDToner" DataSourceID="SqlDataSourceToners" Font-Size="11pt" ForeColor="#333333" GridLines="None" OnDataBound="GridViewEquipos_DataBound" OnSelectedIndexChanged="GridViewEquipos_SelectedIndexChanged" Width="100%">
                <RowStyle BackColor="#E3EAEB" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="IDToner" HeaderText="IDToner" InsertVisible="False" ReadOnly="True" SortExpression="IDToner" />
                    <asp:BoundField DataField="TipoToner" HeaderText="TipoToner" SortExpression="TipoToner" />
                    <asp:BoundField DataField="CPC" HeaderText="CPC" SortExpression="CPC" />
                    <asp:CheckBoxField DataField="Repuesto" HeaderText="Repuesto" SortExpression="Repuesto" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                    <asp:BoundField DataField="AComprar" HeaderText="AComprar" SortExpression="AComprar" />
                    <asp:BoundField DataField="ValorUnitario" HeaderText="ValorUnitario" SortExpression="ValorUnitario" />
                    <asp:BoundField DataField="CodSuministro" HeaderText="CodSuministro" SortExpression="CodSuministro" />
                    <asp:BoundField DataField="Bodega" HeaderText="Bodega" SortExpression="Bodega" />
                    <asp:BoundField DataField="CodigoYupak" HeaderText="CodigoYupak" SortExpression="CodigoYupak" />
                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <RowStyle BackColor="LightGray" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>

    <style type="text/css">
        .btnEquipo {
            background-color: #02569e; /* Color de fondo verde */
            color: white; /* Color del texto */
            border: none; /* Sin borde */
            border-radius: 5px; /* Bordes redondeados */
            padding: 7px 14px; /* Espaciado interno */
            text-align: center; /* Alineación del texto */
            text-decoration: none; /* Sin subrayado */
            display: inline-block; /* Mostrar como un bloque en línea */
            font-size: 12px; /* Tamaño de la fuente */
            cursor: pointer; /* Cursor tipo puntero al pasar sobre el botón */
            transition: background-color 0.3s, transform 0.2s; /* Transiciones suaves */
            margin: 3px; /* Espacio entre botones */
        }

            /* Efecto hover */
            .btnEquipo:hover {
                background-color: #00008b; /* Color más oscuro al pasar el mouse */
                transform: scale(1.05); /* Efecto de agrandamiento */
            }

            /* Efecto activo (al hacer clic) */
            .btnEquipo:active {
                transform: scale(0.95); /* Efecto de disminución al hacer clic */
            }
    </style>

</asp:Content>
