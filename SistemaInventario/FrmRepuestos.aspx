<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmRepuestos.aspx.cs" Inherits="SistemaInventario.FrmRepuestos" EnableEventValidation="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelIDRepuesto" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>

    <script type="text/javascript">
        function confirmarEliminacion() {
            return confirm('¿Está seguro de que desea eliminar este repuesto? Esta acción no se puede deshacer.');
        }
    </script>

    <table>
        <tbody>
            <tr>
                <td style="vertical-align: top;">
                    <%--                    COLUMNA 1--%>
                    <div style="margin: 5px 10px; padding: 10px; border: 2px solid #00008B; display: flex; justify-content: center; align-items: center; background-color: #F0F8FF;" class="auto-style3">
                        <div id="divEditar" runat="server" style="display: flex">
                            <asp:LinkButton ID="btnNuevo" runat="server" CssClass="icon-button" ToolTip="Nuevo Repuesto" OnClick="btnNuevo_Click"> <i class="fa-solid fa-plus"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="icon-button" ToolTip="Editar" OnClick="btnEditar_Click"> <i class="fa-regular fa-pen-to-square"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnActualizar" runat="server" CssClass="icon-button" ToolTip="Actualizar" OnClick="btnActualizar_Click"> <i class="fa-regular fa-floppy-disk"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="icon-button" ToolTip="Cancelar" OnClick="btnCancelar_Click"> <i class="fa-solid fa-x"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnLogs" runat="server" CssClass="icon-button" ToolTip="Logs del Equipo" OnClick="btnLogs_Click"> <i class="fa-regular fa-file-alt"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnAñadirEquipo" runat="server" CssClass="icon-button" Visible="false" ToolTip="Añadir Equipo" OnClientClick="$('#modalEquipos').modal('show'); return false;"> <i class="fa-solid fa-arrow-up-from-bracket"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnCrearRepuesto" runat="server" CssClass="icon-button" Visible="false" ToolTip="Crear repuesto" OnClick="btnCrearRepuesto_Click"> <i class="fa-regular fa-floppy-disk"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="icon-button" Visible="false" ToolTip="Eliminar Repuesto" OnClick="btnEliminar_Click" OnClientClick="return confirmarEliminacion();"> <i class="fa-regular fa-trash-can"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnRetroceder" runat="server" CssClass="icon-button" ToolTip="Retroceder" OnClientClick="window.history.back(); return false;"> <i class="fa-solid fa-x"></i></asp:LinkButton>
                        </div>
                    </div>

                    <div style="display: flex;">
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 400px;">
                                        <div id="divFrmRepuesto" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                                            <table style="border-collapse: separate; border-spacing: 2px; width: 100%">
                                                <tr>
                                                    <td colspan="2" style="color: white; text-align: center; background-color: #00008B; width: 100%">
                                                        <strong>
                                                            <asp:Label ID="labelRepuesto" runat="server" Text="Repuesto"></asp:Label>
                                                        </strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblIDRepuesto" runat="server" Text="ID del Repuesto:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIDRepuesto" runat="server" CssClass="responsive-textbox" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNombreRepuesto" runat="server" Text="Nombre Repuesto:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNombreRepuesto" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Columns="40" Width="179px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCosto" runat="server" Text="Valor Unitario"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCosto" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFechaAdquisicion" runat="server" Text="Fecha Adquisicion:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaAdquisicion" runat="server"></asp:TextBox>
                                                        <asp:Button ID="btnMostrarCalendario" runat="server" Text="..." OnClick="btnMostrarCalendario_Click" />
                                                        <!-- Panel para contener el calendario, inicialmente oculto -->
                                                        <asp:Panel ID="calendarContainer" runat="server" Style="display: none;">
                                                            <!-- Calendar para seleccionar la fecha -->
                                                            <asp:Calendar ID="CalendarFechaAdquisicion" runat="server" OnSelectionChanged="CalendarFechaAdquisicion_SelectionChanged"></asp:Calendar>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblProveedor" runat="server" Text="Proveedor:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtProveedor" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" Columns="40" Width="179px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEnUso" runat="server" Text="En Uso:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkEnUso" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDañado" runat="server" Text="Dañado:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkDañado" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>


                </td>
                <%--                COLUMNA 2--%>
                <td style="vertical-align: top;">
                    <div id="ListaEquipos" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" style="text-align: center;">
                            <ContentTemplate>
                                <asp:Panel ID="PanelSearch" runat="server">
                                    <asp:Label ID="Label1" runat="server" Text="Busqueda:"></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    <asp:LinkButton
                                        ID="LinkButton1"
                                        runat="server"
                                        CssClass="btnEquipo"
                                        ToolTip="Buscar"
                                        OnClick="btnBuscar_Click1">
                    <i class="fa-solid fa-magnifying-glass"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table" Style="margin: 0 auto;" OnRowCommand="GridView1_RowCommand" DataKeyNames="IDEquipo">
                                    <Columns>
                                        <asp:BoundField DataField="EquipoTipo" HeaderText="Tipo de Equipo" SortExpression="EquipoTipo" />
                                        <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <div class="action-buttons" style="display: flex;">
                                                <asp:LinkButton
                                                    runat="server"
                                                    CssClass="icon-button"
                                                    ToolTip="Ver Detalles"
                                                    CommandName="Detalles"
                                                    CommandArgument='<%# Eval("IDEquipo") %>'>
                    <img src="img/detalles.png" alt="Detalles" style="width: 30px; height: 34px;" />
                                                </asp:LinkButton>
                                                <asp:LinkButton
                                                    runat="server"
                                                    CssClass="icon-button"
                                                    ToolTip="Eliminar Repuesto"
                                                    CommandName="Eliminar"
                                                    CommandArgument='<%# Eval("IDEquipo") %>'
                                                    OnClientClick="return confirmarEliminacion();">
                    <i class="fa-regular fa-trash-can"></i>
                                                </asp:LinkButton>
                                                    </div>
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




                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                    SelectCommand="SELECT e.[IDEquipo], e.[EquipoTipo], e.[CodigoActivo], e.[CedulaCustodio], 
                              e.[FechaAdquisicion], e.[Marca], e.[Modelo], e.[Observaciones], e.[Notas], 
                              e.[Area], e.[EnUso]
                       FROM [IT_RepuestoEquipo] r
                       JOIN [IT_Equipos] e ON r.[IDEquipo] = e.[IDEquipo]
                       WHERE r.[IDRepuesto] = @IDRepuesto">
                                    <SelectParameters>
                                        <asp:Parameter Name="IDRepuesto" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                                <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                    SelectCommand="SELECT e.[IDEquipo], e.[EquipoTipo], e.[CodigoActivo], e.[CedulaCustodio], 
                  e.[FechaAdquisicion], e.[Marca], e.[Modelo], e.[Serie], 
                  e.[Ubicacion], e.[Estado], e.[Observaciones], 
                  e.[Notas], e.[Imagenes], 
                  e.[Area], e.[EnUso]
           FROM [IT_RepuestoEquipo] r
           JOIN [IT_Equipos] e ON r.[IDEquipo] = e.[IDEquipo]
           WHERE r.[IDRepuesto] = @IDRepuesto 
           AND (e.[Marca] LIKE '%' + @Buscar + '%' 
           OR e.[Modelo] LIKE '%' + @Buscar + '%' 
           OR e.[Serie] LIKE '%' + @Buscar + '%' 
           OR e.[EquipoTipo] LIKE '%' + @Buscar + '%')">
                                    <SelectParameters>
                                        <asp:Parameter Name="IDRepuesto" Type="Int32" />
                                        <asp:Parameter Name="Buscar" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </td>
            </tr>
        </tbody>
    </table>


    <!-- Modal -->
    <div class="modal fade" id="modalEquipos" tabindex="-1" role="dialog" aria-labelledby="modalEquiposLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="modalEquiposLabel" style="text-align: center;">Equipos Disponibles</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="reloadPage()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="panelmodal" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-group" style="display: flex; align-items: center;">
                                <label for="txtBuscar" style="margin-right: 10px;">Buscar:</label>
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre, serie, etc." Style="margin-right: 10px;" />
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                            </div>
                            <asp:GridView ID="GridViewEquipos" runat="server" AutoGenerateColumns="False"
                                DataSourceID="SqlDataSourceEquipos" CssClass="table"
                                OnSelectedIndexChanged="GridViewEquipos_SelectedIndexChanged"
                                OnRowDataBound="GridViewEquipos_RowDataBound"
                                AllowPaging="true"
                                SelectedIndexMode="Single"
                                DataKeyNames="IDEquipo">
                                <Columns>
                                    <asp:BoundField DataField="IDEquipo" HeaderText="ID Equipo" Visible="False" />
                                    <asp:BoundField DataField="EquipoTipo" HeaderText="Tipo de Equipo" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:BoundField DataField="Serie" HeaderText="Serie" />
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <RowStyle BackColor="LightGray" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:SqlDataSource ID="SqlDataSourceEquipos" runat="server"
                    ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                    SelectCommand="SELECT [IDEquipo], [EquipoTipo], [Marca], [Modelo], [Serie] FROM [IT_Equipos]">
                    <SelectParameters>
                        <asp:Parameter Name="IDRepuesto" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn-eliminar" data-dismiss="modal" onclick="reloadPage()">Cerrar</button>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        document.getElementById('<%= txtBuscar.ClientID %>').addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                document.getElementById('<%= btnBuscar.ClientID %>').click();
                e.preventDefault(); // Evita el comportamiento por defecto de enviar el formulario
            }
        });
    </script>
    <script type="text/javascript">
        function reloadPage() {
            location.reload(); // Recarga la página
        }
        // Agregar un evento que se ejecute al ocultar el modal
        $('#modalEquipos').on('hidden.bs.modal', function () {
            reloadPage(); // Recargar la página cuando el modal se oculta
        });
    </script>


</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">


        .modal-dialog {
            max-width: 90%; /* Controla el ancho del modal */
            width: 100%; /* Opcional: si deseas que sea aún más ancho */
        }
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
        /* Estilo para el botón Crear Equipo */
        .btn-crear-equipo {
            background-color: #02569e; /* Color de fondo verde */
            color: white; /* Color del texto */
            border: none; /* Sin borde */
            border-radius: 5px; /* Bordes redondeados */
            padding: 15px 32px; /* Espaciado interno */
            text-align: center; /* Alineación del texto */
            text-decoration: none; /* Sin subrayado */
            display: inline-block; /* Mostrar como un bloque en línea */
            font-size: 16px; /* Tamaño de la fuente */
            cursor: pointer; /* Cursor tipo puntero al pasar sobre el botón */
            transition: background-color 0.3s, transform 0.2s; /* Transiciones suaves */
        }

            /* Efecto hover */
            .btn-crear-equipo:hover {
                background-color: #00008b; /* Color más oscuro al pasar el mouse */
                transform: scale(1.05); /* Efecto de agrandamiento */
            }

            /* Efecto activo (al hacer clic) */
            .btn-crear-equipo:active {
                transform: scale(0.95); /* Efecto de disminución al hacer clic */
            }

        .btn-eliminar {
            background-color: #d9534f; /* Color rojo */
            color: white; /* Texto en blanco */
            border: none; /* Sin borde */
            padding: 8px 12px; /* Espaciado interno */
            text-align: center; /* Centrado de texto */
            text-decoration: none; /* Sin subrayado */
            display: inline-block; /* Para permitir el espaciado */
            font-size: 14px; /* Tamaño de fuente */
            border-radius: 4px; /* Bordes redondeados */
            cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
            transition: background-color 0.3s ease; /* Transición suave para el color */
        }

            .btn-eliminar:hover {
                background-color: #c9302c; /* Color rojo más oscuro al pasar el mouse */
                transform: scale(1.05); /* Efecto de agrandamiento */
            }

            .btn-eliminar:active {
                background-color: #ac2925; /* Color rojo aún más oscuro cuando se hace clic */
                transform: scale(0.95); /* Efecto de disminución al hacer clic */
            }


        .btnEliminar {
            background-color: #c9302c; /* Color rojo más fuerte */
            color: white; /* Texto en blanco */
            border: none; /* Sin borde */
            padding: 15px 32px; /* Espaciado interno */
            text-align: center; /* Centrado de texto */
            text-decoration: none; /* Sin subrayado */
            display: inline-block; /* Para permitir el espaciado */
            font-size: 16px; /* Tamaño de fuente */
            border-radius: 4px; /* Bordes redondeados */
            cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
            transition: background-color 0.3s ease, transform 0.3s ease; /* Transiciones suaves para el color y el tamaño */
            margin: 0 auto; /* Centra el botón horizontalmente en su contenedor */
            display: block; /* Asegura que el margin: 0 auto; funcione */
        }

            .btnEliminar:hover {
                background-color: #a94442; /* Color rojo más oscuro al pasar el mouse */
                transform: scale(1.05); /* Efecto de agrandamiento */
            }

            .btnEliminar:active {
                background-color: #8a3f2b; /* Color rojo aún más oscuro cuando se hace clic */
                transform: scale(0.95); /* Efecto de disminución al hacer clic */
            }


        .auto-style3 {
            width: 384px;
        }
    </style>


</asp:Content>


