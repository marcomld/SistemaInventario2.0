<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmNuevoEquipo.aspx.cs" Inherits="SistemaInventario.FrmNuevoEquipo" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="btn-container">

        <h3>Busqueda:
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </h3>
    </div>
    <br />

    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
        AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" Visible="false" DataKeyNames="Cedula"
        CssClass="custom-gridview">
        <Columns>
            <asp:BoundField DataField="Cedula" HeaderText="Cedula" SortExpression="Cedula" />
            <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" />
            <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
        </Columns>
    </asp:GridView>


    <div class="btn-container">
        <div id="divCustodio" runat="server" style="padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;"
            visible="false" class="auto-style9">
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
                            <asp:Label ID="lblC1" runat="server" Text="Cedula"></asp:Label>
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
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>"
            SelectCommand="SELECT [Cedula], [Nombres], [Cargo] FROM [IT_Custodios] WHERE ([Nombres] LIKE '%' + @searchTerm + '%' OR [Cedula] LIKE '%' + @searchTerm + '%')">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox1" Name="searchTerm" PropertyName="Text" Type="String" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>




        <table style="width: 100%;">
            <tr>
                <td style="vertical-align: top; width: 30%;">
                    <div id="divFmrEquipo" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                        <table style="border-collapse: separate; border-spacing: 2px;">
                            <tr>
                                <td class="auto-style3" colspan="2" style="color: white; text-align: center; background-color: #00008B;">
                                    <strong>
                                        <asp:Label ID="lblEqui" runat="server" Text="EQUIPO"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td id="lblCodigo" class="auto-style28">CodigoActivo</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtCodigoActivo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style29">
                                    <asp:Label ID="lblequipoTipo" runat="server" Text="Equipo Tipo"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" DataSourceID="SqlEquipoTipo" DataTextField="EquipoTipo" DataValueField="EquipoTipo">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlEquipoTipo" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [EquipoTipo], [Grupo] FROM [IT_EquiposTipos]"></asp:SqlDataSource>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblFechaAdqui" runat="server" Text="Fecha de Adquisición"></asp:Label>
                                </td>
                                <td class="auto-style6">
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
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblModelo" runat="server" Text="Modelo"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtModelo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblSerie" runat="server" Text="Serie"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtSerie" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <div>
                                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlITAreas" DataTextField="Nombre" DataValueField="Nombre">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlITAreas" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT * FROM [IT_Areas]"></asp:SqlDataSource>
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td id="lblUbicacion" class="auto-style28">Ubicación</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtUbicacion" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="lblIP" runat="server" Text="IP"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <div>
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlEstados" DataTextField="Estado" DataValueField="Estado">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlEstados" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" OnSelecting="SqlDataSource1_Selecting" SelectCommand="SELECT * FROM [IT_EstadosBuenoMalo]"></asp:SqlDataSource>
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtObservaciones" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblGarantia" runat="server" Text="Garantía hasta Cuando"></asp:Label>
                                </td>
                                <td class="auto-style6">
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
                                <td class="auto-style12">
                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa Proveedora"></asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblNumRJ" runat="server" Text="Número de Puertos RJ45"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtNumRJ45" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28">
                                    <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtNotas" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td id="idContrato" class="auto-style28">Contrato</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtContrato" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblEnUso" runat="server" Text="En Uso"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <div>
                                        <asp:CheckBox ID="chkEnUso" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td id="idImagen" class="auto-style28">
                                    <asp:Label ID="lblImagen" runat="server" Text="Imagenes"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:Button ID="btnVer" runat="server" Text="Buscar Imagenes" OnClick="btnVer_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>


                <td style="height: 20px; width: 400px; vertical-align: top;">
                    <asp:Panel ID="panelComputador" runat="server" Visible="false" Height="484px" Width="400px">
                        <div id="divFmrCpu" runat="server" style="margin: 0px 10px 0px 10px; padding: 10px; border: 2px solid #00008B; background-color: #F0F8FF;">
                            <table class="nav-justified">
                                <tr>
                                    <td class="auto-style3" colspan="2" style="color: white; text-align: center; background-color: #00008B;">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Text="CPU"></asp:Label>
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
                            <table class="nav-justified">
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
                                    <div style="display: flex; justify-content: space-between; align-items: center;" id="divImpresora" runat="server">
                                        <div style="margin-top: 5px;">
                                            <asp:Label ID="totalToners" runat="server" Text="Total de Toners: "></asp:Label>
                                            <asp:Label ID="lblTotalToners" runat="server" Text="..."></asp:Label>
                                        </div>
                                        <div style="margin-top: 5px;">
                                            <asp:Label ID="lblMensaje" runat="server" Text="Toner Seleccionado: "></asp:Label>
                                            <asp:Label ID="lblTonerS" runat="server" Text="..."></asp:Label>
                                            <asp:Button ID="btnEliminarToner" runat="server" Text="Eliminar" OnClientClick="return confirmarEliminar();"
                                                OnClick="btnEliminarToner_Click" />
                                        </div>
                                    </div>

                                    <div class="scrollable-gridview" style="margin: 5px 0px 5px 0px;">
                                        <asp:GridView ID="GridViewTonersEquipo" runat="server" AutoGenerateColumns="False" DataKeyNames="IDToner"
                                            DataSourceID="SqlDataSourceTonersEquipo" BorderStyle="Double" GridLines="None"
                                            OnSelectedIndexChanged="GridViewTonersEquipo_SelectedIndexChanged"
                                            OnRowDataBound="GridViewTonersEquipo_RowDataBound" CssClass="centered-gridview">
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:SqlDataSource ID="SqlDataSourceTonersEquipo" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT t.IDToner, t.TipoToner, t.Stock, t.Bodega FROM IT_EquiposToners et INNER JOIN IT_Toners t ON et.IDToner = t.IDToner WHERE et.IDEquipo = @IDEquipo;">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="IDEquipo" QueryStringField="IDEquipo" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            
                            <div>
                                <asp:Button ID="btnAñadirToner" runat="server" Text="Añadir Toner"
                                    OnClientClick="abrirNuevaVentana(); return false;" />

                                <script type="text/javascript">
                                    function abrirNuevaVentana() {
                                        window.open('FrmAñadirToner.aspx', '_blank', 'toolbar=0,location=0,menubar=0,width=800,height=600');
                                    }
                                </script>
                            </div>

                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>



    <div class="btn-container">
        <asp:Button ID="btnCrearEquipo" runat="server" Text="Crear Equipo"
            CssClass="btn-crear-equipo" OnClick="btnCrearEquipo_Click" />
    </div>
    <br />
    <br />



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
                                        <asp:Button ID="btnDelSoft" runat="server" Text="Eliminar Software" OnClick="btnDelSoft_Click" OnClientClick="return confirmarEliminar()" />
                                    </td>
                                </tr>
                            </table>
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
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 244px;
            width: 242px;
        }

        .auto-style2 {
            height: 241px;
            width: 242px;
        }

        /*      .auto-style3 {
            height: 50px;
            width: 242px;
        }*/

        .auto-style4 {
            width: 242px;
        }

        .auto-style5 {
            width: 249px;
        }

        .auto-style6 {
            width: 356px;
        }

        .auto-style9 {
            width: 586px;
        }

        .custom-gridview {
            width: 70%; /* Ajusta el ancho según tus necesidades */
            margin: 0 auto; /* Centra la tabla horizontalmente */
            border: 1px solid #00008b; /* Borde sólido azul para la tabla */
            border-collapse: collapse; /* Colapsar bordes para evitar duplicados */
        }

            .custom-gridview th, .custom-gridview td {
                border: 1px solid #00008b; /* Borde sólido azul para las celdas */
                padding: 8px; /* Espaciado interno de las celdas */
                text-align: center; /* Alineación del texto centrada */
            }

            .custom-gridview th {
                background-color: #00008b; /* Fondo azul para los encabezados de columna */
                color: white; /* Texto blanco para los encabezados */
            }

        .auto-style10 {
            height: 42px;
        }

        .auto-style11 {
            width: 356px;
            height: 37px;
        }

        .auto-style12 {
            height: 37px;
        }
        /* Contenedor centrado */
        .btn-container {
            display: flex; /* Usar flexbox */
            justify-content: center; /* Centrar horizontalmente */
            width: 100%; /* O ajusta a un tamaño específico */
            /*max-width: 800px;*/ /* Ancho máximo opcional */
            margin: 10px; /* Centrar el contenedor */
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
    </style>
</asp:Content>


