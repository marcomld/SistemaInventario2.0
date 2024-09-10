<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmGridRepuestos.aspx.cs" Inherits="SistemaInventario.FrmGridRepuestos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        /* Estilo para el botón Crear Equipo */
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

        .auto-style1 {
            width: 404px;
        }
    </style>

<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function() {
        // Obtén el campo de texto y el botón
        var txtBuscar = document.getElementById('<%= txtBuscar.ClientID %>');
        var btnBuscar = document.getElementById('<%= btnBuscar.ClientID %>');

        // Agrega un listener para el evento keydown en el campo de texto
        txtBuscar.addEventListener('keydown', function(event) {
            if (event.key === 'Enter') {
                // Prevenir el comportamiento por defecto del Enter
                event.preventDefault();
                // Simular un clic en el botón de búsqueda
                btnBuscar.click();
            }
        });
    });
</script>

    <asp:Label ID="Label1" runat="server" Text="Buscar:"></asp:Label>
    <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btnEquipo"  ToolTip="Buscar" OnClick="btnBuscar_Click1">
     <i class="fa-solid fa-magnifying-glass"></i>
    </asp:LinkButton>

     <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btnEquipo" ToolTip="Nuevo Equipo" OnClick="btnNuevo_Click">
     <i class="fa-solid fa-plus"></i>
    </asp:LinkButton>

    <asp:Label ID="Label3" runat="server" Text="Registros:   "></asp:Label>
    <asp:Label ID="lblRegistros" runat="server" Text="____"></asp:Label>


    <br />
    <br />
    <asp:GridView ID="GridRepuestos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        OnRowDataBound="GridViewRepuestos_RowDataBound" CellPadding="4" DataKeyNames="IDRepuesto"
        DataSourceID="IT_Repuestos" Font-Size="11pt" ForeColor="#333333" GridLines="None"
        OnDataBound="GridViewRepuestos_DataBound" OnRowCommand="GridViewRepuestos_RowCommand"
        Width="100%">
        <Columns>
            <asp:BoundField DataField="IDRepuesto" HeaderText="IDRepuesto" InsertVisible="False" ReadOnly="True" SortExpression="IDRepuesto" />
            <asp:BoundField DataField="NombreRepuesto" HeaderText="NombreRepuesto" SortExpression="NombreRepuesto" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
            <asp:BoundField DataField="Costo" HeaderText="Costo" SortExpression="Costo" />
            <asp:BoundField DataField="FechaAdquisicion" HeaderText="FechaAdquisicion" SortExpression="FechaAdquisicion" />
            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="Observaciones" />
            <asp:CheckBoxField DataField="EnUso" HeaderText="EnUso" SortExpression="EnUso" />
            <asp:CheckBoxField DataField="Dañado" HeaderText="Dañado" SortExpression="Dañado" />
            <asp:CommandField SelectText="" ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <RowStyle BackColor="LightGray" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

    <asp:SqlDataSource ID="IT_Repuestos" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
        SelectCommand="SELECT * FROM [IT_Repuestos]"></asp:SqlDataSource>




    <!-- Modal para agregar un nuevo repuesto -->
    <div class="modal fade" id="modalAgregarRepuesto" tabindex="-1" role="dialog" aria-labelledby="modalAgregarRepuestoLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAgregarRepuestoLabel">Agregar Nuevo Repuesto</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblNombreRepuesto" runat="server" Text="Nombre:"></asp:Label>
                    <asp:TextBox ID="txtNombreRepuesto" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label>
                    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblCosto" runat="server" Text="Costo:"></asp:Label>
                    <asp:TextBox ID="txtCosto" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblProveedor" runat="server" Text="Proveedor:"></asp:Label>
                    <asp:TextBox ID="txtProveedor" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones:"></asp:Label>
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarRepuesto" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarRepuesto_Click" />
                </div>
            </div>
        </div>
    </div>




</asp:Content>
