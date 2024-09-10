<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEquipos.aspx.cs" Inherits="SistemaInventario.FrmEquipos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .mi-tabla-css {
            width: 100%;
            border-collapse: collapse;
            border: 2px solid #ddd; /* Borde de la tabla */
        }

            .mi-tabla-css th, .mi-tabla-css td {
                padding: 8px;
                text-align: left;
                border-bottom: 1px solid #ddd; /* Borde inferior de las celdas */
                border-right: 1px solid #ddd; /* Borde derecho para separar las columnas */
            }

            .mi-tabla-css th {
                background-color: #f2f2f2; /* Color de fondo de los encabezados de columna */
            }

            .mi-tabla-css tbody tr {
                background-color: #fff; /* Color de fondo de todas las filas */
            }

            /* Estilo para la última columna sin borde derecho */
            .mi-tabla-css th:last-child, .mi-tabla-css td:last-child {
                border-right: none; /* Elimina el borde derecho para la última columna */
            }


        .gridview-row,
        .gridview-header {
            padding: 20px; /* Puedes ajustar el valor según sea necesario */
        }

        .centered-gridview {
            width: 100%;
            margin: 0 auto;
            table-layout: auto;
        }

        .modal-body {
            overflow-x: auto;
            max-width: 100%;
        }

        .d-none {
            display: none;
        }
    </style>
    <script>
        function openSecondForm() {
            var screenWidth = window.screen.availWidth;
            var screenHeight = window.screen.availHeight;
            var url = 'FrmYupak.aspx?CodigoActivo=' + codigoActivo;
            window.open(url, '_blank', 'width=' + (screenWidth / 2) + ',height=' + screenHeight + ',top=0,left=' + (screenWidth / 2));
        }

        function confirmDelete() {
            var ddl = document.getElementById('<%= ddlSoftware.ClientID %>');
            var selectedOption = ddl.options[ddl.selectedIndex];
            var message = "¿Seguro desea eliminar el software?\n" +
                "ID: " + selectedOption.value + "\n" +
                "Descripción: " + selectedOption.text;

            // Mostrar la confirmación y ejecutar el evento click del botón oculto si se confirma
            if (confirm(message)) {
                document.getElementById('<%= btnDelSoft.ClientID %>').click();
            }
            return false; // Detener el postback del botón visible
        }
    </script>



    <div>
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <div id="divCustodio" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" style="color: white; text-align: center; background-color: #00008B;">
                                    <strong>
                                        <asp:Label ID="lblC0" runat="server" Text="CUSTODIO"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblc1" runat="server" Text="Cedula"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblCedula" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblC2" runat="server" Text="Nombres"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombres" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblC3" runat="server" Text="Cargo"></asp:Label>
                                    </strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblCargo" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:DropDownList ID="ddlCustodio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustodio_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceCustodios" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>"
                                SelectCommand="SELECT Cedula, Nombres, Cargo FROM IT_Custodios ORDER BY Nombres"></asp:SqlDataSource>
                        </div>
                    </div>
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 400px">
                                <div id="divEditar" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnEquiposA" runat="server" Text="Equipos Adicionales" OnClick="btnEquiposA_Click" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" Width="100px" />

                                            </td>
                                            <td>
                                                <asp:Button ID="btnCustodiosAnt" runat="server" Text="Custodios Anteriores" OnClick="btnCustodiosAnt_Click" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnYupak" runat="server" Text="Ver Yupak" Width="150px" OnClick="btnYupak_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </td>
                    </table>
                </td>
                <td style="width: 195px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 400px;">
                    <div id="divFmrEquipo" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                        <table class="nav-justified">
                            <tr>
                                <td class="auto-style1" colspan="2" style="color: white; text-align: center; background-color: #00008B;">
                                    <strong>
                                        <asp:Label ID="lblEqui" runat="server" Text="EQUIPO"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td id="lblCodigo" class="auto-style28">CodigoActivo</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtCodigoActivo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style29">
                                    <asp:Label ID="lblequipoTipo" runat="server" Text="Equipo Tipo"></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="txtEquipoTipo" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblFechaAdqui" runat="server" Text="Fecha de Adquisición"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <div>
                                        <!-- TextBox para mostrar la fecha -->
                                        <asp:TextBox ID="txtFechaAdqui" runat="server"></asp:TextBox>
                                        <!-- Botón para mostrar el calendario -->
                                        <asp:Button ID="btnMostrarCalendario" runat="server" Text="..." OnClick="btnMostrarCalendario_Click" />
                                    </div>
                                    <!-- Panel para contener el calendario, inicialmente oculto -->
                                    <asp:Panel ID="calendarContainer" runat="server" Style="display: none;">
                                        <!-- Calendar para seleccionar la fecha -->
                                        <asp:Calendar ID="CalendarFechaAdquisicion" runat="server" OnSelectionChanged="CalendarFechaAdquisicion_SelectionChanged"></asp:Calendar>
                                    </asp:Panel>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblMarca" runat="server" Text="Marca"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblModelo" runat="server" Text="Modelo"></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="txtModelo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblSerie" runat="server" Text="Serie"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtSerie" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td id="lblUbicacion" class="auto-style28">Ubicación</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtUbicacion" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblIP" runat="server" Text="IP"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <div>
                                        <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceEstados" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                            ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>"
                                            SelectCommand="SELECT [Estado] FROM [IT_EstadosBuenoMalo]"></asp:SqlDataSource>

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtObservaciones" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblGarantia" runat="server" Text="Garantía hasta Cuando"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <div>
                                        <!-- TextBox para mostrar la fecha -->
                                        <asp:TextBox ID="txtGarantia" runat="server"></asp:TextBox>
                                        <!-- Botón para mostrar el calendario -->
                                        <asp:Button ID="btnMostrarCalendarioGarantia" runat="server" Text="..." OnClick="btnMostrarCalendarioGarantia_Click" />
                                    </div>
                                    <!-- Panel para contener el calendario, inicialmente oculto -->
                                    <asp:Panel ID="calendarContainerGarantia" runat="server" Style="display: none;">
                                        <!-- Calendar para seleccionar la fecha -->
                                        <asp:Calendar ID="CalendarGarantia" runat="server" OnSelectionChanged="CalendarGarantia_SelectionChanged"></asp:Calendar>
                                    </asp:Panel>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblFechaAct" runat="server" Text="Fecha Actualización Datos"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtFechaAtcDatos" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa Proveedora"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblNumRJ" runat="server" Text="Número de Puertos RJ45"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtNumRJ45" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotas" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td id="idContrato" class="auto-style28">Contrato</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtContrato" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceAreas" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                            ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>"
                                            SelectCommand="SELECT [Nombre] FROM [IT_Areas]"></asp:SqlDataSource>

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="En Uso"></asp:Label>
                                </td>
                                <td>
                                    <div>
                                        <asp:CheckBox ID="chkEnUso" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td id="idImagen" class="auto-style28">
                                    <asp:Label ID="lblImagen" runat="server" Text="Imagenes"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:Button ID="btnVer" runat="server" Text="Ver" OnClick="btnVer_Click" />
                                </td>
                            </tr>
                        </table>

                    </div>
                </td>
                <td style="height: 20px; width: 375px; vertical-align: top;">
                    <asp:Panel ID="panelComputador" runat="server" Visible="false">
                        <!-- Contenido específico para el tipo de equipo "Computador" -->
                        <!-- Puedes incluir aquí los elementos adicionales que pertenecen al tipo de equipo "Computador" -->
                        <div id="divFmrCpu" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style1" colspan="2" style="color: white; text-align: center; background-color: #00008B;">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="CPU"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNombreEquipo" runat="server" Text="NombreDelEquipo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNombreEquipo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style29">
                                        <asp:Label ID="lblProcesador" runat="server" Text="Procesador"></asp:Label>
                                    </td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtProcesador" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblProcesadorVelo" runat="server" Text="ProcesadorVelocidad"></asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtProcesadorVelo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblRam" runat="server" Text="RAM"></asp:Label>
                                    </td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtRam" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblNumDiscos" runat="server" Text="NúmeroDeDiscos"></asp:Label>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:TextBox ID="txtNumDiscos" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblAlmacenamiento" runat="server" Text="Almacenamiento"></asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtAlmacenamiento" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblTipoLector" runat="server" Text="TipoLectorCD"></asp:Label>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:TextBox ID="txtTipoLector" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblWiewless" runat="server" Text="Wireless"></asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:CheckBox ID="cbWireless" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">
                                        <asp:Label ID="lblMac" runat="server" Text="MAC"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMac" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin: 10px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="color: white; text-align: center; background-color: #00008B;">
                                        <strong>
                                            <asp:Label ID="lblsoft" runat="server" Text="SOFTWARE"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                            <table id="tablaSoft" runat="server" class="mi-tabla-css" enableviewstate="true">

                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Descripción</th>
                                        <th>Licencia</th>
                                        <th>Legal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <!-- Aquí se cargarán los datos -->
                                </tbody>
                            </table>
                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAñadirSoft" runat="server" Text="Añadir Soft" OnClick="btnAñadirSoft_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEliminarSoft" runat="server" Text="Eliminar Soft" OnClick="btnEliminarSoft_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAñadirTipoSoft" runat="server" Text="Añadir Tipo Soft" OnClick="btnAñadirTipoSoft_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelImpresora" runat="server" Visible="false">
                        <!-- Contenido específico para el tipo de equipo "Impresora" -->
                        <!-- Puedes incluir aquí los elementos adicionales que pertenecen al tipo de equipo "Impresora" -->
                        <div id="divFmrImpresora" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="color: white; text-align: center; background-color: #00008B;">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="TONERS"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                            <table id="tablaToner" runat="server" class="mi-tabla-css" enableviewstate="true">

                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>TipoToner</th>
                                        <th>Stock</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <!-- Aquí se cargarán los datos -->
                                </tbody>
                            </table>
                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnVerMas" runat="server" Text="Ver mas" OnClick="btnVerMas_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
                <td style="height: 20px; width: 195px;"></td>
            </tr>
            <tr>

                <td>
                    <div id="modales">
                        <!-- Modal Custodios Anteriores-->
                        <div class="modal fade" id="miModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog <%--modal-lg--%>" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">CUSTODIOS ANTERIORES</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <asp:SqlDataSource ID="sdsCustodioAnt" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT Cedula, Nombre, Cargo, FechaActualizacion FROM IT_CustodiosAnteriores WHERE (IDEquipo = @IDEquipo)">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter DefaultValue="IDEquipo" Name="IDEquipo" QueryStringField="IDEquipo" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="sdsCustodioAnt" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                                <Columns>
                                                    <asp:BoundField DataField="Cedula" HeaderText="Cedula" SortExpression="Cedula" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                                    <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                                                    <asp:BoundField DataField="FechaActualizacion" HeaderText="FechaActualizacion" SortExpression="FechaActualizacion" DataFormatString="{0:dd/MM/yyyy}" />
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-row" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" ForeColor="Black" CssClass="gridview-row" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal Equipos Adicionales-->
                        <div class="modal fade" id="modalEquiposAdi" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog <%--modal-lg--%>" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalEqui">EQUIPOS ADICIONALES</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>

                                            <asp:Label ID="lblTEquipos" runat="server" Text="Total de Equipos:  "></asp:Label>
                                            <asp:Label ID="lblTotalEquipos" runat="server" Text="---"></asp:Label>
                                            <asp:GridView ID="GridViewEquiposAdi" runat="server" AutoGenerateColumns="False" DataKeyNames="IDEquipo" DataSourceID="sdsEquiposAdi" OnSelectedIndexChanged="GridViewEquiposAdi_SelectedIndexChanged" OnRowDataBound="GridViewEquiposAdi_RowDataBound"  >
                                                <Columns>
                                                    <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
                                                    <asp:BoundField DataField="EquipoTipo" HeaderText="EquipoTipo" SortExpression="EquipoTipo" />
                                                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                                                    <asp:BoundField DataField="IDEquipo" HeaderText="IDEquipo" InsertVisible="False" ReadOnly="True" SortExpression="IDEquipo" />
                                                    <asp:CheckBoxField DataField="Imagenes" HeaderText="Imagenes" SortExpression="Imagenes" />
                                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-row" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" ForeColor="Black" CssClass="gridview-row" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="sdsEquiposAdi" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT CodigoActivo, EquipoTipo, Marca, Modelo, IDEquipo, Imagenes FROM IT_Equipos WHERE (CedulaCustodio = @CedulaCustodio)">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="CedulaCustodio" QueryStringField="CedulaCustodio" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal Ver Mas - Torner-->
                        <div class="modal fade" id="modalVerMas" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">EQUIPOS TONERS</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <asp:SqlDataSource ID="SqlDataSourceTipoEquipo" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT CodigoActivo, EquipoTipo, Marca, Modelo, Serie FROM IT_Equipos WHERE IDEquipo = @IDEquipo;">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="IDEquipo" QueryStringField="IDEquipo" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>

                                            <div style="text-align: center; margin: 10px">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceTipoEquipo" CssClass="centered-gridview">
                                                    <Columns>
                                                        <asp:BoundField DataField="CodigoActivo" HeaderText="CodigoActivo" SortExpression="CodigoActivo" />
                                                        <asp:BoundField DataField="EquipoTipo" HeaderText="EquipoTipo" SortExpression="EquipoTipo" />
                                                        <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                                                        <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                    <RowStyle BackColor="White" ForeColor="Black" CssClass="gridview-row" />
                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                        </div>
                                        <div style="text-align: center; margin: 10px">
                                            <asp:SqlDataSource ID="SqlDataSourceToner" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT t.IDToner, t.TipoToner, t.CPC, t.Repuesto, t.Stock, t.AComprar, t.ValorUnitario, t.CodSuministro, t.Bodega FROM IT_EquiposToners et INNER JOIN IT_Toners t ON et.IDToner = t.IDToner WHERE et.IDEquipo = @IDEquipo;">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="IDEquipo" QueryStringField="IDEquipo" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="IDToner" DataSourceID="SqlDataSourceToner" CssClass="centered-gridview">
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
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" ForeColor="Black" CssClass="gridview-row" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal Añadir Software-->
                        <div class="modal fade" id="modalAddSoft" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog <%--modal-lg--%>" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">AÑADIR SOFTWARE</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:Label ID="lblDes" runat="server" Text="Descripción: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDescripcion" runat="server" DataSourceID="SqlDataSourceTipoSoft" DataTextField="Descripcion" DataValueField="Descripcion"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSourceTipoSoft" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT Descripcion FROM IT_SoftwareTipo"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        <asp:Label ID="lblLicen" runat="server" Text="Licencia: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLicencia" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        <asp:Label ID="lblLegal" runat="server" Text="Legal: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkLegal" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnAddSoft" runat="server" OnClick="btnAddSoft_Click" Text="Añadir Software" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal Eliminar Software-->
                        <div class="modal fade" id="modalDelSoft" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog <%--modal-lg--%>" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">ELIMINAR SOFTWARE</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:Label ID="lblSele" runat="server" Text="Seleccione el Software: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSoftware" runat="server" DataSourceID="SqlDataSourceSoft" DataTextField="Descripcion" DataValueField="IDSoftware">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSourceSoft" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [IDSoftware], [Descripcion] FROM [IT_Software] WHERE ([IDEquipo] = @IDEquipo)">
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter Name="IDEquipo" QueryStringField="IDEquipo" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <!-- Botón visible para mostrar el mensaje de confirmación -->
                                                        <asp:Button ID="btnShowConfirm" runat="server" Text="Eliminar Software" OnClientClick="return confirmDelete();" />
                                                        <!-- Botón oculto para realizar la eliminación si se confirma -->
                                                        <asp:Button ID="btnDelSoft" runat="server" Text="Eliminar Software" OnClick="btnDelSoft_Click" CssClass="d-none" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal Añadir Tipo de Software-->
                        <div class="modal fade" id="modalAddTipoSoft" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog <%--modal-lg--%>" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">ELIMINAR SOFTWARE</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:Label ID="lblTipoSoft" runat="server" Text="Tipo de Software: "></asp:Label>
                                                    </td>
                                                    <td class="auto-style2">
                                                        <asp:TextBox ID="txtTipoSoft" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnAddTipoSoft" runat="server" Text="Añadir" OnClick="btnAddTipoSoft_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>




                    </div>



                </td>
                <td style="width: 195px">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td style="width: 195px">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
