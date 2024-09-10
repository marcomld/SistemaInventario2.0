<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmToner.aspx.cs" Inherits="SistemaInventario.FrmToners" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            /*            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            width:100%;
            height:100%;*/
        }

        .responsive-textbox {
            width: 100%;
            box-sizing: border-box;
            min-width: 150px; /* Ajusta según sea necesario */
            max-width: 100%; /* Para asegurarte que no sobrepase el contenedor */
        }

        .centered-gridview {
            width: 100%;
            margin: 0 auto;
            table-layout: auto;
        }

        .modal-header {
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .modal-title {
            flex: 1;
            font-size: 1.5rem; /* Aumentar el tamaño del título */
            font-weight: bold;
        }

        .close {
            position: absolute;
            right: 15px;
        }

        .scrollable-gridviewModal {
            height: 300px; /* Altura fija del contenedor */
            overflow-y: scroll; /* Habilitar desplazamiento vertical */
            overflow-x: auto; /* Habilitar desplazamiento horizontal si es necesario */
        }

        .gridview-header {
            position: sticky;
            top: 0;
            z-index: 1; /* Asegura que el encabezado esté encima de las filas */
        }

        .scrollable-gridview {
            height: 200px; /* Altura fija del contenedor */
            overflow-y: scroll; /* Habilitar desplazamiento vertical */
            overflow-x: auto; /* Habilitar desplazamiento horizontal si es necesario */
        }

        .selectedRow {
            background-color: #C5BBAF !important; /* Color de fondo cuando está seleccionado */
            font-weight: bold; /* Texto en negrita */
            color: #333333; /* Color de texto */
            cursor: pointer; /* Cambiar el cursor a pointer para indicar que la fila es seleccionable */
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


    <script>
        function confirmSelection(row) {
            var cells = row.getElementsByTagName("td");
            var codigoActivo = cells[0].innerText;
            var marca = cells[2].innerText;
            var modelo = cells[3].innerText;
            var idequipo = cells[4].innerText;

            var message = "¿Está seguro que desea añadir este equipo?\n\n" +
                "Codigo Activo: " + codigoActivo + "\n" +
                "Marca: " + marca + "\n" +
                "Modelo: " + modelo + "\n" +
                "IDEquipo: " + idequipo;

            return confirm(message);
        }

        var selectedIdEquipo = null;  // Variable para almacenar el idEquipo seleccionado

        function rowDoubleClick(row, idEquipo) {
            // Llamar a la función de doble clic
            __doPostBack('<%= UpdatePanel1.ClientID %>', 'DoubleClick:' + idEquipo);
        }

        function confirmarEliminar() {
            if (selectedIdEquipo) {
                return confirm("¿Está seguro de que desea eliminar este equipo ID: " + selectedIdEquipo + "?");
            } else {
                alert("Seleccione un equipo primero.");
                return false;
            }
        }

        // Función para resetear la selección del equipo
        function resetSelectedIdEquipo() {
            selectedIdEquipo = null;
        }
    </script>

    <div id="divEditar" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; width: 500px; display: flex; justify-content: center; background-color: #F0F8FF;">

        <!-- Botón para Editar -->
        <asp:LinkButton ID="btnEditar" runat="server" CssClass="icon-button" ToolTip="Editar" OnClick="btnEditar_Click">
    <i class="fa-regular fa-pen-to-square"></i>
        </asp:LinkButton>

        <!-- Botón para Cancelar -->
        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="icon-button" ToolTip="Cancelar" OnClick="btnCancelar_Click">
    <i class="fa-solid fa-x"></i>
        </asp:LinkButton>

        <!-- Botón para Actualizar -->
        <asp:LinkButton ID="btnActualizar" runat="server" CssClass="icon-button" ToolTip="Actualizar" OnClick="btnActualizar_Click">
    <i class="fa-regular fa-floppy-disk"></i>
        </asp:LinkButton>

        <!-- Botón para Logs del Toner -->
        <asp:LinkButton ID="btnLogs" runat="server" CssClass="icon-button" ToolTip="Logs del Toner" OnClick="btnLogs_Click">
    <i class="fa-regular fa-file-alt"></i>
        </asp:LinkButton>

        <!-- Botón para Añadir Equipo -->
        <asp:LinkButton ID="btnAñadirEquipo" runat="server" CssClass="icon-button" ToolTip="Añadir Equipo" OnClick="btnAñadirEquipo_Click">
    <i class="fa-solid fa-plus"></i>
        </asp:LinkButton>

    </div>


    <div id="divFmrToner" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; width: 500px; background-color: #F0F8FF;">
        <table style="border-collapse: separate; border-spacing: 2px; width: 100%">
            <tr>
                <td colspan="2" style="color: white; text-align: center; background-color: #00008B; width: 100%">
                    <strong>
                        <asp:Label ID="lblToner" runat="server" Text="TONER"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoToner" runat="server" Text="Tipo Toner"></asp:Label>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="txtTipoToner" runat="server" TextMode="MultiLine" CssClass="responsive-textbox"></asp:TextBox>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlTipoToner" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoToner_SelectedIndexChanged" DataSourceID="SqlDataSourceTipoToner" DataTextField="TipoToner" DataValueField="TipoToner"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceTipoToner" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                            SelectCommand="SELECT [TipoToner] FROM [IT_TonersTipo]"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCPC" runat="server" Text="CPC"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCPC" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblRepuesto" runat="server" Text="Repuesto"></asp:Label>
                </td>
                <td class="auto-style1">
                    <div>
                        <asp:CheckBox ID="chkRepuesto" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStock" runat="server" Text="Stock"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAComprar" runat="server" Text="A Comprar"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAComprar" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValorUnitario" runat="server" Text="Valor Unitario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtValorUnitario" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodSuministro" runat="server" Text="CodSuministro"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodSuministro" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBodega" runat="server" Text="Bodega"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBodega" runat="server"></asp:TextBox>
                    <br />
                    <asp:DropDownList ID="ddlBodegaOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBodegaOptions_SelectedIndexChanged">
                        <asp:ListItem Value="CONSUMO">CONSUMO</asp:ListItem>
                        <asp:ListItem Value="INVERSION">INVERSION</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblCodigoYupak" runat="server" Text="Codigo Yupak"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoYupak" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNotas" TextMode="MultiLine" runat="server" CssClass="responsive-textbox"></asp:TextBox>
                </td>
            </tr>
        </table>

    </div>



    <table>
        <tr>
            <td colspan="2">
                <div runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; width: 800px; background-color: #F0F8FF;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="display: flex; justify-content: space-between; align-items: center;">
                                <div>
                                    <asp:Label ID="lblTEquipos" runat="server" Text="Total de Equipos:  "></asp:Label>
                                    <asp:Label ID="lblTotalEquipos" runat="server" Text="..."></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="lblMensaje" runat="server" Text="IDEquipo Seleccionado: "></asp:Label>
                                    <asp:Label ID="lblEquipo" runat="server" Text="..."></asp:Label>
                                    <%--                                    <asp:Button ID="btnEliminarEquipo" runat="server" Text="Eliminar" class="btnEquipo" OnClick="btnEliminarEquipo_Click" OnClientClick="return confirmarEliminar();" />--%>
                                    <asp:LinkButton ID="btnEliminarEquipo" runat="server" CssClass="icon-button" ToolTip="Eliminar" OnClick="btnEliminarEquipo_Click" OnClientClick="return confirmarEliminar();">
                                    <i class="fa-regular fa-trash-can" style="margin-left: 220px;"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="scrollable-gridview">
<asp:GridView ID="GridViewEquiposToner" runat="server" AutoGenerateColumns="False" DataKeyNames="IDEquipo" DataSourceID="SqlDataSourceEquiposToner" BorderStyle="Double" GridLines="None" OnRowCommand="GridViewEquiposToner_RowCommand" OnRowDataBound="GridViewEquiposToner_RowDataBound" OnSelectedIndexChanged="GridViewEquiposToner_SelectedIndexChanged" CssClass="centered-gridview">
    <Columns>
        <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
        <asp:BoundField DataField="EquipoTipo" HeaderText="EquipoTipo" SortExpression="EquipoTipo" />
        <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
        <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
        <asp:BoundField DataField="IDEquipo" HeaderText="IDEquipo" InsertVisible="False" ReadOnly="True" SortExpression="IDEquipo" />
        <asp:CheckBoxField DataField="Imagenes" HeaderText="Imagenes" SortExpression="Imagenes" />
        <asp:CommandField SelectText="" ShowSelectButton="True" />
<asp:TemplateField HeaderText="Acción">
    <ItemTemplate>
        <asp:ImageButton 
            ID="btnDetalles" 
            runat="server" 
            CssClass="icon-button"
            ToolTip="Ver Detalles"
            CommandName="Detalles" 
            CommandArgument='<%# Container.DataItemIndex %>' 
            ImageUrl="img/detalles.png" 
            AlternateText="Detalles" 
            Width="30px" 
            Height="34px" 
        />
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
    <EditRowStyle BackColor="#7C6F57" />
    <RowStyle BackColor="LightGray" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:SqlDataSource ID="SqlDataSourceEquiposToner" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT 
                                                                                                                                                                                            eq.CodigoActivo,
	                                                                                                                                                                                        eq.EquipoTipo,
	                                                                                                                                                                                        eq.Marca,
	                                                                                                                                                                                        eq.Modelo,
	                                                                                                                                                                                        eq.IDEquipo,
	                                                                                                                                                                                        eq.Imagenes
                                                                                                                                                                                        FROM 
                                                                                                                                                                                            IT_Equipos eq
                                                                                                                                                                                        INNER JOIN 
                                                                                                                                                                                            IT_EquiposToners et ON eq.IDEquipo = et.IDEquipo
                                                                                                                                                                                        INNER JOIN 
                                                                                                                                                                                            IT_Toners t ON et.IDToner = t.IDToner
                                                                                                                                                                                        WHERE 
                                                                                                                                                                                            t.IDToner = @IDToner;">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="IDToner" QueryStringField="IDToner" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
    </table>


    <!-- Modal Añadir Toner-->
    <div class="modal fade" id="modalAñadirEquipo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">AÑADIR EQUIPO</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: red">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <h4>Seleccione un Equipo</h4>
                    </div>
                    <div class="scrollable-gridviewModal">
                        <asp:SqlDataSource ID="SqlDataSourceEquiposImpresoras" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [CodigoActivo], [EquipoTipo], [Marca], [Modelo], [IDEquipo], [Imagenes] FROM [IT_Equipos] WHERE ([EquipoTipo] = @EquipoTipo)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Impresora" Name="EquipoTipo" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div>
                            <asp:GridView ID="GridViewAñadirEquipos" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridViewAñadirEquipos_RowDataBound" BorderStyle="Double" DataKeyNames="IDEquipo" DataSourceID="SqlDataSourceEquiposImpresoras" GridLines="None" OnSelectedIndexChanged="GridViewAñadirEquipos_SelectedIndexChanged" Width="100%">
                                <RowStyle BackColor="LightGray" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
                                    <asp:BoundField DataField="EquipoTipo" HeaderText="EquipoTipo" SortExpression="EquipoTipo" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                                    <asp:BoundField DataField="IDEquipo" HeaderText="IDEquipo" InsertVisible="False" ReadOnly="True" SortExpression="IDEquipo" />
                                    <asp:CheckBoxField DataField="Imagenes" HeaderText="Imagenes" SortExpression="Imagenes" />
                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <%--<SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />--%>
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <RowStyle BackColor="LightGray" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
