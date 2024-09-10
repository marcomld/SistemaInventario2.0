<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEquipo.aspx.cs" Inherits="SistemaInventario.FrmEquipos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        body {
            /*            margin: 0;*/
            /*           display: flex;*/
            /*            justify-content: center;
            align-items: center;*/
            /*background-color: #48cacf;*/
        }

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


        .centered-gridview {
            width: 100%;
            margin: 0 auto;
            table-layout: auto;
        }

        .gridview-container {
            display: flex;
            flex-direction: column;
            height: 325px; /* Ajusta la altura según tus necesidades */
            overflow-y: auto;
            margin: 5px 0;
        }

        .modal-header {
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .scrollable-gridviewModal {
            height: 300px; /* Altura fija del contenedor */
            overflow-y: scroll; /* Habilitar desplazamiento vertical */
            overflow-x: auto; /* Habilitar desplazamiento horizontal si es necesario */
        }

        .scrollable-gridview {
            height: 150px; /* Altura fija del contenedor */
            overflow-y: scroll; /* Habilitar desplazamiento vertical */
            overflow-x: auto; /* Habilitar desplazamiento horizontal si es necesario */
        }

        .auto-style1 {
            height: 20px;
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
            font-size: 13px; /* Tamaño de la fuente */
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

        .auto-style2 {
            height: 77px;
        }


        .auto-style3 {
            width: 100%;
            height: 129px;
        }


        .auto-style5 {
            width: 404px;
        }

        .table-container {
            border: 2px solid #00008B;
            background-color: #F0F8FF;
            padding: 10px;
            margin: 10px;
            width: 200px;
            height: 160px;
            max-height: 300px;
            flex: 1; /* Ajusta el tamaño de las tablas según el contenido */
            /*max-width: 100%;*/ /* Evita que las tablas se salgan del contenedor */
            overflow: auto; /* Añadido para permitir el scroll si el contenido excede el tamaño */
            box-sizing: border-box; /* Asegura que padding y border se incluyan en el width y height */
        }

        .auto-style8 {
            height: 19px;
        }

        #div1 {
            margin: 1px 1px 10px 10px;
            padding: 10px;
            border: 2px solid #00008B;
            background-color: #F0F8FF;
            width: 650px;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 10px;
        }


        /* Oculta los botones ASP.NET */
        .asp-button {
            display: none;
        }

        table {
            border-collapse: collapse;
        }

        td {
            vertical-align: top;
        }

        /* Estilo del modal */
        .modal {
            /*display: none;*/ /* Oculto por defecto */
            position: fixed; /* Queda en el mismo lugar durante el scroll */
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto; /* Hacer scroll si el contenido es más grande que la ventana */
            background-color: rgba(0,0,0,0.4); /* Fondo semi-transparente */
        }

        /* Contenido del modal */
        .modal-content {
            background-color: #fefefe;
            margin: 15% auto; /* Centra el modal */
            padding: 20px;
            border: 1px solid #888;
            width: 80%; /* Puedes ajustar el ancho aquí */
            max-width: 900px; /* Ancho máximo */
            border-radius: 10px; /* Esquinas redondeadas */
        }

        /* Botón de cierre */
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        /* Estilo del GridView */
        .gridview {
            width: 100%;
            border-collapse: collapse;
        }

            .gridview th,
            .gridview td {
                padding: 8px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .gridview th {
                background-color: #003399;
                color: white;
            }

            .gridview tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .gridview tr:hover {
                background-color: #ddd;
            }
    </style>
    <script>
        function confirmarEliminarSoft() {
            var ddl = document.getElementById('<%= ddlSoftware.ClientID %>');
            var selectedOption = ddl.options[ddl.selectedIndex];
            var message = "¿Está seguro de que desea eliminar este software?\n" +
                "ID: " + selectedOption.value + "\n" +
                "Descripción: " + selectedOption.text;
            return confirm(message);
        }


        function confirmSelection(row) {
            var cells = row.getElementsByTagName("td");
            var idToner = cells[0].innerText;
            var tipoToner = cells[1].innerText;
            var bodega = cells[8].innerText;

            var message = "¿Está seguro que desea añadir este toner?\n\n" +
                "ID Toner: " + idToner + "\n" +
                "Tipo Toner: " + tipoToner + "\n" +
                "Bodega: " + bodega;

            return confirm(message);
        }

        var selectedToner = null;  // Variable para almacenar el Toner seleccionado

        function rowDoubleClick(row, idToner) {
            // Llamar a la función de doble clic
            __doPostBack('<%= UpdatePanel1.ClientID %>', 'DoubleClick:' + idToner);
        }

        function confirmarEliminar() {
            if (selectedToner) {
                return confirm("¿Está seguro de que desea eliminar este Toner ID : " + selectedToner + "?");
            } else {
                alert("Seleccione un Toner primero.");
                return false;
            }
        }

        function closeModal() {
            document.getElementById('modalRepuestos').style.display = 'none';
        }

        // Cerrar el modal si se hace clic fuera del contenido del modal
        window.onclick = function (event) {
            if (event.target == document.getElementById('modalRepuestos')) {
                closeModal();
            }
        }

        // Función para resetear la selección del equipo
        function resetSelectedTooner() {
            selectedToner = null;
        }

        function goBack() {
            // Retrocede en el historial del navegador
            window.history.back();
        }

        // Aquí defines las funciones de JavaScript para manejar los clics
        function btnEditar_Click() {
            document.getElementById('<%= btnEditar.ClientID %>').click();
        }

        function btnCancelar_Click() {
            document.getElementById('<%= btnCancelar.ClientID %>').click();
        }

        function btnActualizar_Click() {
            document.getElementById('<%= btnActualizar.ClientID %>').click();
        }

        function btnEquiposA_Click() {
            document.getElementById('<%= btnEquiposA.ClientID %>').click();
        }

        function btnCrearEquipo_Click() {
            document.getElementById('<%= btnCrearEquipo.ClientID %>').click();
        }

        function btnLogs_Click() {
            document.getElementById('<%= btnLogs.ClientID %>').click();
        }

        function btnCustodiosAnt_Click() {
            document.getElementById('<%= btnCustodiosAnt.ClientID %>').click();
        }

        function btnNuevoEquipo_Click() {
            document.getElementById('<%= btnNuevoEquipo.ClientID %>').click();
        }

        function btnYupak_Click() {
            document.getElementById('<%= btnYupak.ClientID %>').click();
        }
    </script>


    <table>
        <tr>
            <!-- Column 1: Two containers on the left -->
            <td>

                <div id="div1">
                    <asp:Button ID="btnEditar" runat="server" CssClass="asp-button" Text="Editar" OnClick="btnEditar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="asp-button" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" CssClass="asp-button" Text="Actualizar" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnEquiposA" runat="server" CssClass="asp-button" Text="Equipos Adicionales" OnClick="btnEquiposA_Click" />
                    <asp:Button ID="btnCrearEquipo" runat="server" CssClass="asp-button" Text="Crear Equipo" OnClick="btnCrearEquipo_Click" />
                    <asp:Button ID="btnLogs" runat="server" CssClass="asp-button" Text="Logs del Equipo" OnClick="btnLogs_Click" />
                    <asp:Button ID="btnCustodiosAnt" runat="server" CssClass="asp-button" Text="Custodios Anteriores" OnClick="btnCustodiosAnt_Click" />
                    <asp:Button ID="btnNuevoEquipo" runat="server" CssClass="asp-button" Text="Nuevo Equipo" OnClick="btnNuevoEquipo_Click" />
                    <asp:Button ID="btnYupak" runat="server" CssClass="asp-button" Text="Ver Yupak" OnClick="btnYupak_Click" />

                    <!-- Iconos para el cliente -->
                    <a id="btnNuevoEquipoIcon" runat="server" href="#" class="icon-button" title="Nuevo Equipo" onclick="btnNuevoEquipo_Click()">
                        <i class="fa-solid fa-plus"></i>
                    </a>
                    <a id="btnEditarIcon" runat="server" href="#" class="icon-button" title="Editar" onclick="btnEditar_Click()">
                        <i class="fa-regular fa-pen-to-square"></i>
                    </a>
                    <a id="btnActualizarIcon" runat="server" href="#" class="icon-button" title="Actualizar" onclick="btnActualizar_Click()">
                        <i class="fa-regular fa-floppy-disk"></i>
                    </a>
                    <a id="btnCancelarIcon" runat="server" href="#" class="icon-button" title="Cancelar" onclick="btnCancelar_Click()">
                        <i class="fa-solid fa-x"></i>
                    </a>

                    <a id="btnEquiposAIcon" runat="server" href="#" class="icon-button" title="Equipos Adicionales" onclick="btnEquiposA_Click()">
                        <i class="fa-solid fa-laptop"></i>
                    </a>
                    <a id="btnCrearEquipoIcon" runat="server" href="#" class="icon-button" title="Crear Equipo" onclick="btnCrearEquipo_Click()">
                        <i class="fa-regular fa-floppy-disk"></i>
                    </a>
                    <a id="btnCustodiosAntIcon" runat="server" href="#" class="icon-button" title="Custodios Anteriores" onclick="btnCustodiosAnt_Click()">
                        <i class="fa-solid fa-person"></i>
                    </a>
                    <a id="btnYupakIcon" runat="server" href="#" class="icon-button" title="Ver Yupak" onclick="btnYupak_Click()">
                        <i class="fa-brands fa-y-combinator"></i>
                    </a>
                    <a id="btnAtras" runat="server" href="#" class="icon-button" title="Cancelar" onclick="goBack()">
                        <i class="fa-solid fa-x"></i>
                    </a>
                    <a id="btnLogsIcon" runat="server" href="#" class="icon-button" title="Logs del Equipo" onclick="btnLogs_Click()">
                        <i class="fa-regular fa-file-alt"></i>
                    </a>
                    <a id="btnMantenimiento" runat="server" href="FrmMantenimiento.aspx" class="icon-button" title="Mantenimiento" >
                        <i class="fa-solid fa-screwdriver-wrench"></i>
                    </a>
                </div>

                <div id="divFmrEquipo" runat="server" style="margin: 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF; width: 650px;">
                    <table style="border-collapse: collapse; width: 100%;">
                        <tr>
                            <td colspan="4" style="color: white; text-align: center; background-color: #00008B; padding: 10px;">
                                <strong>
                                    <asp:Label ID="lblEqui" runat="server" Text="EQUIPO"></asp:Label>
                                </strong>
                            </td>
                        </tr>

                        <!-- Primeras dos columnas -->
                        <tr>
                            <td style="padding: 8px;">Código Activo:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtCodigoActivo" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;">Observaciones:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Style="width: 100%; padding: 5px; resize: vertical;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Equipo Tipo:</td>
                            <td style="padding: 8px;">
                                <asp:DropDownList ID="ddlEquipoTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEquipoTipo_SelectedIndexChanged" DataSourceID="SqlDataSEquipoTipo" DataTextField="EquipoTipo" DataValueField="EquipoTipo" Style="width: 100%;"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSEquipoTipo" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [EquipoTipo] FROM [IT_EquiposTipos]"></asp:SqlDataSource>
                            </td>
                            <td style="padding: 8px;">Notas:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtNotas" runat="server" TextMode="MultiLine" Style="width: 100%; padding: 5px; resize: vertical;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Marca:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtMarca" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;">Fecha de Adquisición:</td>
                            <td style="padding: 8px;">
                                <div style="display: flex; align-items: center;">
                                    <asp:TextBox ID="txtFechaAdqui" runat="server" Style="flex: 1; padding: 5px;"></asp:TextBox>
                                    <asp:Button ID="btnMostrarCalendario" runat="server" Text="..." OnClick="btnMostrarCalendario_Click" Style="margin-left: 5px;" />
                                </div>
                                <asp:Panel ID="calendarContainer" runat="server" Style="display: none; margin-top: 5px;">
                                    <asp:Calendar ID="CalendarFechaAdquisicion" runat="server" OnSelectionChanged="CalendarFechaAdquisicion_SelectionChanged"></asp:Calendar>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Modelo:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtModelo" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;">Garantía hasta Cuando:</td>
                            <td style="padding: 8px;">
                                <div style="display: flex; align-items: center;">
                                    <asp:TextBox ID="txtGarantia" runat="server" Style="flex: 1; padding: 5px;"></asp:TextBox>
                                    <asp:Button ID="btnMostrarCalendarioGarantia" runat="server" Text="..." OnClick="btnMostrarCalendarioGarantia_Click" Style="margin-left: 5px;" />
                                </div>
                                <asp:Panel ID="calendarContainerGarantia" runat="server" Style="display: none; margin-top: 5px;">
                                    <asp:Calendar ID="CalendarGarantia" runat="server" OnSelectionChanged="CalendarGarantia_SelectionChanged"></asp:Calendar>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Serie:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtSerie" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;">Empresa Proveedora:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtEmpresa" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Estado:</td>
                            <td style="padding: 8px;">
                                <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Style="width: 100%;"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceEstados" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>" SelectCommand="SELECT [Estado] FROM [IT_EstadosBuenoMalo]"></asp:SqlDataSource>
                            </td>
                            <td style="padding: 8px;">Contrato:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtContrato" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">IP:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtIP" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;">En Uso:</td>
                            <td style="padding: 8px;">
                                <asp:CheckBox ID="chkEnUso" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Puerto RJ45:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtNumRJ45" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                            </td>
                            <td style="padding: 8px;"></td>
                            <td style="padding: 8px;"></td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Área:</td>
                            <td style="padding: 8px;">
                                <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Style="width: 100%;"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceAreas" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>" SelectCommand="SELECT [Nombre] FROM [IT_Areas]"></asp:SqlDataSource>
                            </td>
                            <td style="padding: 8px; padding-left: 120px;">
                                <asp:LinkButton ID="btnVer" runat="server" CssClass="icon-button btn_img" ToolTip="Ver" OnClick="btnVer_Click"> <i class="fa-solid fa-camera" style="font-size: 46px; color: #0454BC;" alt="Descripción del Icono"></i></asp:LinkButton>
                            </td>

                            <td style="padding: 8px; padding-left: 56px;">
                                <asp:LinkButton ID="btnImagen" runat="server" CssClass="icon-button" ToolTip="Repuestos" OnClick="btnImagen_Click"> <img src="img/repuestos.png" alt="Descripción del Icono" style="width: 82px; " /></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px;">Ubicación:</td>
                            <td style="padding: 8px;">
                                <asp:TextBox ID="txtUbicacion" runat="server" Style="width: 100%; padding: 5px;"></asp:TextBox>
                                <%--</td>--%>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>

            </td>
            <!-- Column 2: Four containers on the right -->
            <td>


                <div id="divCustodio" runat="server" style="margin: 1px 1px 10px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF; width: 380px; height: 140px;">
                    <table class="auto-style3">
                        <tr>
                            <td colspan="2" style="color: white; text-align: center; background-color: #00008B;" class="auto-style1">
                                <strong>
                                    <asp:Label ID="lblC0" runat="server" Text="CUSTODIO"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>
                                <asp:Label ID="lblc1" runat="server" Text="Cedula"></asp:Label>
                                :</strong></td>
                            <td>
                                <asp:Label ID="lblCedula" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>
                                <asp:Label ID="lblC2" runat="server" Text="Nombres"></asp:Label>
                                :</strong></td>
                            <td>
                                <asp:Label ID="lblNombres" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCustodio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustodio_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>
                                <asp:Label ID="lblC3" runat="server" Text="Cargo"></asp:Label>
                                :</strong></td>
                            <td>
                                <asp:Label ID="lblCargo" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:SqlDataSource ID="SqlDataSourceCustodios" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" ProviderName="<%$ ConnectionStrings:BDDSistemasConnectionString.ProviderName %>" SelectCommand="SELECT Cedula, Nombres, Cargo FROM IT_Custodios ORDER BY Nombres"></asp:SqlDataSource>
                    </div>
                </div>



                <asp:Panel ID="panelComputador" runat="server" Visible="false">
                    <!-- Contenido específico para el tipo de equipo "Computador" -->
                    <!-- Puedes incluir aquí los elementos adicionales que pertenecen al tipo de equipo "Computador" -->
                    <div id="divFmrCpu" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; width: 380px; background-color: #F0F8FF;">
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
                                    :</td>
                                <td>
                                    <asp:TextBox ID="txtNombreEquipo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style29">
                                    <asp:Label ID="lblProcesador" runat="server" Text="Procesador"></asp:Label>
                                    :</td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="txtProcesador" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblProcesadorVelo" runat="server" Text="ProcesadorVelocidad:"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtProcesadorVelo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblRam" runat="server" Text="RAM"></asp:Label>
                                    :</td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="txtRam" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblNumDiscos" runat="server" Text="NúmeroDeDiscos"></asp:Label>
                                    :</td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtNumDiscos" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblAlmacenamiento" runat="server" Text="Almacenamiento"></asp:Label>
                                    :</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtAlmacenamiento" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblTipoLector" runat="server" Text="TipoLectorCD"></asp:Label>
                                    :</td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtTipoLector" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblWiewless" runat="server" Text="Wireless"></asp:Label>
                                    :</td>
                                <td class="auto-style10">
                                    <asp:CheckBox ID="cbWireless" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">
                                    <asp:Label ID="lblMac" runat="server" Text="MAC"></asp:Label>
                                    :</td>
                                <td class="auto-style13">
                                    <asp:TextBox ID="txtMac" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="margin: 10px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF; width: 380px;">
                        <table style="width: 100%">

                            <tr>
                                <td style="color: white; text-align: center; background-color: #00008B;" class="auto-style8">
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
                                    <asp:Button ID="btnAñadirSoft" runat="server" Text="Añadir Soft" class="btnEquipo" OnClick="btnAñadirSoft_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEliminarSoft" runat="server" Text="Eliminar Soft" class="btnEquipo" OnClick="btnEliminarSoft_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAñadirTipoSoft" runat="server" Text="Añadir Tipo Soft" class="btnEquipo" OnClick="btnAñadirTipoSoft_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <asp:Panel ID="panelImpresora" runat="server" Visible="false">
                    <!-- Contenido específico para el tipo de equipo "Impresora" -->
                    <div id="divFmrImpresora" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF; width: 380px; height: 450px; display: flex; flex-direction: column;">
                        <!-- Encabezado -->
                        <table style="width: 100%">
                            <tr>
                                <td style="color: white; text-align: center; background-color: #00008B;">
                                    <strong>
                                        <asp:Label ID="lbl" runat="server" Text="TONERS"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                        </table>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <!-- Contenido principal con flexbox para distribuir espacio -->
                                <div style="display: flex; flex-direction: column; height: 700px; /* ajusta la altura según tus necesidades */">
                                    <!-- Área de información y botones -->
                                    <div style="margin-top: 5px; display: flex; justify-content: space-between; align-items: center;">
                                        <div>
                                            <!-- Espacio reservado para otros contenidos si es necesario -->
                                        </div>
                                        <div>
                                            <asp:Button ID="btnAñadirToner" runat="server" class="btnEquipo" OnClick="btnAñadirToner_Click" Text="Añadir Toner" />
                                            <asp:Button ID="btnEliminarToner" runat="server" Text="Eliminar" class="btnEquipo" OnClientClick="return confirmarEliminar();" OnClick="btnEliminarToner_Click" />
                                        </div>
                                    </div>

                                    <!-- GridView ajustado para ocupar el espacio restante -->
                                    <div class="gridview-container">
                                        <asp:GridView ID="GridViewTonersEquipo" runat="server" AutoGenerateColumns="False" DataKeyNames="IDToner" DataSourceID="SqlDataSourceTonersEquipo" BorderStyle="Double" GridLines="None" OnSelectedIndexChanged="GridViewTonersEquipo_SelectedIndexChanged" OnRowDataBound="GridViewTonersEquipo_RowDataBound" CssClass="centered-gridview">
                                            <Columns>
                                                <asp:BoundField DataField="IDToner" HeaderText="IDToner" SortExpression="IDToner" InsertVisible="False" ReadOnly="True" />
                                                <asp:BoundField DataField="TipoToner" HeaderText="TipoToner" SortExpression="TipoToner" />
                                                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                                <asp:BoundField DataField="Bodega" HeaderText="Bodega" SortExpression="Bodega" />
                                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                                            </Columns>
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" CssClass="gridview-header" />
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <RowStyle BackColor="LightGray" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <div style="display: flex; align-items: center;">
                                            <asp:Label ID="totalToners" runat="server" Text="Total de Toners: " Style="margin-right: 10px;"></asp:Label>
                                            <asp:Label ID="lblTotalToners" runat="server" Text="" Style="margin-right: 20px;"></asp:Label>
                                            <asp:Label ID="lblTonerS" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                        <asp:SqlDataSource ID="SqlDataSourceTonersEquipo" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT t.IDToner, t.TipoToner, t.Stock, t.Bodega FROM IT_EquiposToners et INNER JOIN IT_Toners t ON et.IDToner = t.IDToner WHERE et.IDEquipo = @IDEquipo;">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="IDEquipo" QueryStringField="IDEquipo" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        <!-- Botón adicional -->
                        <div style="margin-top: 5px;">
                        </div>
                    </div>
                </asp:Panel>

            </td>
        </tr>
    </table>






    <%--        <div id="divEditar" runat="server" style="margin: 10px 10px 10px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF; width: 350px; height: 140px;">
            <table style="width: 300px;">
                <tr>
                    <td>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" class="btnEquipo" Width="120px" />
                    </td>
                    <td>
                        <asp:Button ID="btnEquiposA" runat="server" Text="Equipos Adicionales" class="btnEquipo" OnClick="btnEquiposA_Click" Width="150px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCrearEquipo" runat="server" Text="Crear Equipo" CssClass="btn-crear-equipo" OnClick="btnCrearEquipo_Click" />
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" class="btnEquipo" OnClick="btnActualizar_Click" Width="120px" />
                        <asp:Button ID="btnLogs" runat="server" Text="Logs del Equipo" class="btnEquipo" OnClick="btnLogs_Click" Width="120px" />
                    </td>
                    <td>
                        <asp:Button ID="btnCustodiosAnt" runat="server" Text="Custodios Anteriores" class="btnEquipo" OnClick="btnCustodiosAnt_Click" Width="150px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btnEquipo" OnClick="btnCancelar_Click" Width="120px" />
                        <asp:Button ID="btnNuevoEquipo" runat="server" Text="Nuevo Equipo" CssClass="btnEquipo" OnClick="btnNuevoEquipo_Click" Width="120px" />
                    </td>
                    <td>
                        <asp:Button ID="btnYupak" runat="server" Text="Ver Yupak" Width="150px" class="btnEquipo" OnClick="btnYupak_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>





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
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalEqui">EQUIPOS ADICIONALES</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: red">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div>

                            <asp:Label ID="lblTEquipos" runat="server" Text="Total de Equipos:  "></asp:Label>
                            <asp:Label ID="lblTotalEquipos" runat="server" Text="---"></asp:Label>
                            <asp:GridView ID="GridViewEquiposAdi" runat="server" AutoGenerateColumns="False" DataKeyNames="IDEquipo" DataSourceID="sdsEquiposAdi" OnSelectedIndexChanged="GridViewEquiposAdi_SelectedIndexChanged" OnRowDataBound="GridViewEquiposAdi_RowDataBound">
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
                            <asp:SqlDataSource ID="sdsEquiposAdi" runat="server"
                                ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                                SelectCommand="SELECT CodigoActivo, EquipoTipo, Marca, Modelo, IDEquipo, Imagenes FROM IT_Equipos WHERE (CedulaCustodio = @CedulaCustodio) ORDER BY EquipoTipo"
                                OnSelecting="sdsEquiposAdi_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="CedulaCustodio" Type="String" />
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
                            <asp:GridView ID="GridViewToner" runat="server" AutoGenerateColumns="False" DataKeyNames="IDToner" DataSourceID="SqlDataSourceToner" OnSelectedIndexChanged="GridViewToner_SelectedIndexChanged" OnRowDataBound="GridViewToner_RowDataBound" CssClass="centered-gridview">
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
                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
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
                                        <asp:Button ID="btnDelSoft" runat="server" Text="Eliminar Software" OnClick="btnDelSoft_Click" OnClientClick="return confirmarEliminarSoft()" />
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
                        <h5 class="modal-title">AÑADIR TIPO DE SOFTWARE</h5>
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

        <!-- Modal Repuestos -->
        <div id="modalRepuestos" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <h2>Repuestos</h2>
                <asp:GridView ID="gvRepuestos" runat="server" AutoGenerateColumns="False"
                    CssClass="gridview"
                    OnRowCommand="GridViewRepuestos_RowCommand"
                    DataKeyNames="IDRepuesto"
                    FooterStyle-BackColor="#1C5E55"
                    FooterStyle-Font-Bold="True"
                    FooterStyle-ForeColor="White"
                    PagerStyle-BackColor="#666666"
                    PagerStyle-ForeColor="White"
                    PagerStyle-HorizontalAlign="Center"
                    HeaderStyle-BackColor="#003399"
                    HeaderStyle-Font-Bold="True"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-CssClass="gridview-header"
                    EditRowStyle-BackColor="#7C6F57"
                    RowStyle-BackColor="LightGray"
                    AlternatingRowStyle-BackColor="White">

                    <Columns>
                        <asp:BoundField DataField="IDRepuesto" HeaderText="ID Repuesto" />
                        <asp:BoundField DataField="NombreRepuesto" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CommandName="Select" CssClass="icon-button" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Ver Detalles">
                            <img src="img/detalles.png" alt="Detalles" style="width: 30px; height: 34px;" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>



        <!-- Modal Logs -->
        <div class="modal fade" id="logsModal" tabindex="-1" aria-labelledby="logsModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="logsModalLabel">Logs del Equipo</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: red">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSourceLogs" AllowSorting="True">
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
                        <asp:SqlDataSource ID="SqlDataSourceLogs" runat="server"
                            ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
                            SelectCommand="SELECT [ID], [Usuarios], [Fecha], [Accion], [Tabla], [Detalle], [CodigoActivo] FROM [IT_Logs] WHERE [CodigoActivo] = @CodigoActivo ORDER BY [ID] DESC">
                            <SelectParameters>
                                <asp:Parameter Name="CodigoActivo" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <div class="modal-footer">
                        <!-- Aquí puedes agregar botones u otros elementos si es necesario -->
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Añadir Toner-->
        <div class="modal fade" id="modalAñadirToner" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">AÑADIR TONER</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: red">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div>
                            <h4>Seleccione un Toner</h4>
                        </div>
                        <div class="scrollable-gridviewModal">
                            <asp:SqlDataSource ID="SqlDataSourceTonerModal" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT * FROM [IT_Toners]"></asp:SqlDataSource>
                            <div>
                                <asp:GridView ID="GridViewTonerModal" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridViewTonerModal_RowDataBound" BorderStyle="Double" DataKeyNames="IDToner" DataSourceID="SqlDataSourceTonerModal" GridLines="None" OnSelectedIndexChanged="GridViewTonerModal_SelectedIndexChanged">
                                    <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
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

    </div>


</asp:Content>
