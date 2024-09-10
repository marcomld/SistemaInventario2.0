<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmReportes.aspx.cs" Inherits="SistemaInventario.FrmReportes" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Reportes</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .button-margin {
            width: 100%; /* Ajusta el ancho del botón al 100% del ancho de la celda */
        }

        td {
            padding: 5px; /* Crea un espaciado entre botnes */
        }

        .btnEquipo {
            background-color: #02569e; /* Color de fondo verde */
            color: white; /* Color del texto */
            border: none; /* Sin borde */
            border-radius: 5px; /* Bordes redondeados */
            padding: 7px 14px; /* Espaciado interno */
            text-align: center; /* Alineación del texto */
            text-decoration: none; /* Sin subrayado */
            display: inline-block; /* Mostrar como un bloque en línea */
            font-size: 14px; /* Tamaño de la fuente */
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

    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnEliminados" runat="server" OnClick="btnEliminados_Click" Text="Equipos Eliminados" class="btnEquipo" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <asp:Table ID="Tabla" runat="server" Width="91%">
    </asp:Table>

    <asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar">
        <asp:Label ID="Label1" runat="server" Text="Buscar"></asp:Label>
        <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btnEquipo" ToolTip="Buscar" OnClick="btnBuscar_Click">
        <i class="fa-solid fa-magnifying-glass"></i>
        </asp:LinkButton>
    </asp:Panel>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Usuarios" HeaderText="Usuarios" SortExpression="Usuarios" />
            <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
            <asp:BoundField DataField="Accion" HeaderText="Accion" SortExpression="Accion" />
            <asp:BoundField DataField="Tabla" HeaderText="Tabla" SortExpression="Tabla" />
            <asp:BoundField DataField="Detalle" HeaderText="Detalle" SortExpression="Detalle" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
        <EditRowStyle BackColor="#7C6F57" />
        <RowStyle BackColor="LightGray" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT * FROM [IT_Logs] ORDER BY [ID] DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceLogs" runat="server"
        ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
        SelectCommand="SELECT [ID], [Usuarios], [Fecha], [Accion], [Tabla], [Detalle], [CodigoActivo]  
                    FROM [IT_Logs] 
                    WHERE (LOWER([ID]) LIKE LOWER('%' + @SearchText + '%') 
                        OR LOWER([Usuarios]) LIKE LOWER('%' + @SearchText + '%') 
                        OR CONVERT(VARCHAR, [Fecha], 103) LIKE '%' + @SearchText + '%' 
                        OR LOWER([Accion]) LIKE LOWER('%' + @SearchText + '%') 
                        OR LOWER([Tabla]) LIKE LOWER('%' + @SearchText + '%')
                        OR LOWER([Detalle]) LIKE LOWER('%' + @SearchText + '%')
                        OR LOWER([CodigoActivo]) LIKE LOWER('%' + @SearchText + '%'))
                        ORDER BY [ID] DESC">
        <SelectParameters>
            <asp:Parameter Name="SearchText" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>



    <asp:GridView ID="GridViewEquiposNoUsados" runat="server" DataSourceID="SqlDataSourceEquiposNoUsados">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
        <EditRowStyle BackColor="#7C6F57" />
        <RowStyle BackColor="LightGray" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

    <asp:SqlDataSource
        ID="SqlDataSourceEquiposNoUsados"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
        SelectCommand="SELECT [IDEquipo], [EquipoTipo], [CodigoActivo], [CedulaCustodio], [Marca], [Modelo], [Estado], [EnUso], [Area] FROM [IT_Equipos] WHERE [EnUso] = 0;"></asp:SqlDataSource>

    <br />




</asp:Content>
