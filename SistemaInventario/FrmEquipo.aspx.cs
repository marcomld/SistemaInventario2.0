using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public partial class FrmEquipos : System.Web.UI.Page
    {
        public class Custodio
        {
            public string Cedula { get; set; }
            public string Nombres { get; set; }
            public string Cargo { get; set; }
        }

        //Variable del IDEquipo
        static string grupoE;
        static string cedulaE;
        public static string IdEquipoE { get; set; }
        public static string CodigoActivo { get; set; }
        public static string EquipoTipo { get; set; }
        public static string CedulaCustodio { get; set; }
        public static string FechaAdquisicion { get; set; }
        public static string Marca { get; set; }
        public static string Modelo { get; set; }
        public static string Serie { get; set; }
        public static string Ubicacion { get; set; }
        public static string Ip { get; set; }
        public static string Estado { get; set; }
        public static string Observaciones { get; set; }
        public static string Garantia { get; set; }
        public static string EmpresaProveedora { get; set; }
        public static string NumRJ45 { get; set; }
        public static string Notas { get; set; }
        public static string Contrato { get; set; }
        public static string Area { get; set; }
        public static string Enuso { get; set; }

        public static string NombreEquipo { get; set; } = "";
        public static string Procesador { get; set; } = "";
        public static string ProcesadorVelocidad { get; set; } = "";
        public static string RAM { get; set; } = "";
        public static string NumDiscos { get; set; } = "";
        public static string Almacenamiento { get; set; } = "";
        public static string TipoLectorCD { get; set; } = "";
        public static string MAC { get; set; } = "";
        public static string Wireless { get; set; } = "";

        // Variable para verificar si la página se ha cargado por primera vez
        private bool isPageLoadComplete = false;

        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            string nuevoEquipo = Request.QueryString["nuevoEquipo"];

            if (nuevoEquipo == "SI")
            {
                Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
                if (literalPageTitle != null)
                {
                    literalPageTitle.Text = "Nuevo Equipo"; // Asigna el título visible en el <div>
                }
                //Recoger datos
                IdEquipoE = Request.QueryString["IDEquipo"];
                grupoE = Request.QueryString["Grupo"];
                

                this.ddlEquipoTipo.Visible = true;


                // Ocultar los iconos no deseados
                btnLogsIcon.Attributes["class"] = "icon-button hidden"; 
                btnEditarIcon.Attributes["class"] = "icon-button hidden";
                btnActualizarIcon.Attributes["class"] = "icon-button hidden";
                btnCancelarIcon.Attributes["class"] = "icon-button hidden";
                btnEquiposAIcon.Attributes["class"] = "icon-button hidden";
                btnCustodiosAntIcon.Attributes["class"] = "icon-button hidden";
                btnNuevoEquipoIcon.Attributes["class"] = "icon-button hidden";
                btnYupakIcon.Attributes["class"] = "icon-button hidden";
                btnAtras.Attributes["class"] = "icon-button";

                // Asegurarse de que el icono de "Crear Equipo" permanezca visible
                btnCrearEquipoIcon.Attributes["class"] = "icon-button";

                btnVer.Visible = false;
                btnImagen.Visible = false;

                if (!IsPostBack)
                {
                    // Bindear el DropDownList al SqlDataSource solo en la primera carga
                    ddlCustodio.DataSource = SqlDataSourceCustodios;
                    ddlCustodio.DataTextField = "Nombres";
                    ddlCustodio.DataValueField = "Cedula";
                    ddlCustodio.DataBind();

                    // Llenar el DropDownList con datos desde la base de datos
                    ddlArea.DataSource = SqlDataSourceAreas;
                    ddlArea.DataTextField = "Nombre";
                    ddlArea.DataValueField = "Nombre";
                    ddlArea.DataBind();

                    ddlEstado.DataSource = SqlDataSourceEstados;
                    ddlEstado.DataTextField = "Estado";
                    ddlEstado.DataValueField = "Estado"; // Asegúrate de que el campo ValueField sea correcto
                    ddlEstado.DataBind();
                }


            }
            else
            {
                Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
                if (literalPageTitle != null)
                {
                    literalPageTitle.Text = "Detalles Equipo"; // Asigna el título visible en el <div>
                }

                btnCrearEquipoIcon.Attributes["class"] = "icon-button hidden";
                btnActualizarIcon.Attributes["class"] = "icon-button hidden";
                btnCancelarIcon.Attributes["class"] = "icon-button hidden";
                btnAtras.Attributes["class"] = "icon-button hidden";

                btnLogsIcon.Attributes["class"] = "icon-button";
                btnNuevoEquipoIcon.Attributes["class"] = "icon-button";

                this.ddlEquipoTipo.Visible = true;
                this.ddlArea.Visible = true;
                this.ddlArea.Enabled = false;
                this.ddlCustodio.Enabled = false;
                this.ddlCustodio.Visible = true;

                this.ddlEquipoTipo.Enabled = false;
                this.ddlEstado.Enabled = false;
                this.ddlEstado.Visible = true;


                //Recoger datos
                IdEquipoE = Request.QueryString["IDEquipo"];
                grupoE = Request.QueryString["Grupo"];


                // Establecer la cadena de conexión
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    // Consulta para obtener el registro correspondiente al ID del equipo
                    string query = @"
            SELECT [EquipoTipo], [CedulaCustodio], [Marca], [Modelo], [Serie], [Ubicacion]
            FROM [IT_Equipos]
            WHERE [IDEquipo] = @IDEquipo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDEquipo", IdEquipoE);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Asignar valores a las variables estáticas
                                EquipoTipo = reader["EquipoTipo"].ToString();
                                CedulaCustodio = reader["CedulaCustodio"].ToString();
                                Marca = reader["Marca"].ToString();
                                Modelo = reader["Modelo"].ToString();
                                Serie = reader["Serie"].ToString();
                                Ubicacion = reader["Ubicacion"].ToString();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('No se encontró el equipo con el ID proporcionado.');", true);
                            }
                        }
                    }
                }


                if (Request.QueryString["Grupo"] == null)
                {
                    Response.Redirect("FrmPrincipal.aspx");
                }


                //Verificar el grupo del equipo
                if (grupoE.Equals("Computador"))
                {
                    cargarTablaSoft();
                }
                else
                {
                    if (grupoE.Equals("Impresora"))
                    {
                        cargarTablaToners();
                    }
                }

                if (!IsPostBack)
                {
                    // Bindear el DropDownList al SqlDataSource solo en la primera carga
                    ddlCustodio.DataSource = SqlDataSourceCustodios;
                    ddlCustodio.DataTextField = "Nombres";
                    ddlCustodio.DataValueField = "Cedula";
                    ddlCustodio.DataBind();

                    // Llenar el DropDownList con datos desde la base de datos
                    ddlEstado.DataSource = SqlDataSourceEstados;
                    ddlEstado.DataTextField = "Estado";
                    ddlEstado.DataValueField = "Estado";
                    ddlEstado.DataBind();

                    // Llenar el DropDownList con datos desde la base de datos
                    ddlArea.DataSource = SqlDataSourceAreas;
                    ddlArea.DataTextField = "Nombre";
                    ddlArea.DataValueField = "Nombre";
                    ddlArea.DataBind();

                    //LLenar tabla Custodio
                    ObtenerCustodio(IdEquipoE);

                    // Llenar los TextBox con los datos del equipo
                    LlenarTextBoxsEquipo(IdEquipoE);

                    // Seleccionar el valor adecuado en el DropDownList basado en lblNombres.Text
                    string nombreBuscado = lblNombres.Text;
                    if (!string.IsNullOrEmpty(nombreBuscado))
                    {
                        SetDropDownListValueNombre(nombreBuscado);
                    }


                    // Si es la primera vez que se carga la página, desactivar los botones
                    this.btnMostrarCalendario.Visible = false;
                    this.btnMostrarCalendarioGarantia.Visible = false;
                    this.ddlCustodio.Visible = true;
                    this.ddlEstado.Enabled = false;
                    this.ddlEstado.Visible = true;
                    this.ddlArea.Visible = true;

                    //this.btnCancelar.Enabled = false;
                    //this.btnActualizar.Enabled = false;

                    btnActualizarIcon.Attributes["class"] = "icon-button hidden";
                    btnCancelarIcon.Attributes["class"] = "icon-button hidden";
                    btnAtras.Attributes["class"] = "icon-button hidden";


                    //Deshanilitar campos de texto
                    DeshabilitarCamposTexto(this.divFmrEquipo);

                    //Verificar el grupo del equipo
                    if (grupoE.Equals("Computador"))
                    {
                        this.panelComputador.Visible = true;
                        DeshabilitarCamposTexto(this.divFmrCpu);
                        LlenarTextBoxsCpu(IdEquipoE);
                    }
                    else
                    {
                        if (grupoE.Equals("Impresora"))
                        {
                            this.panelImpresora.Visible = true;
                            DeshabilitarCamposTexto(this.divFmrImpresora);
                        }
                    }
                }
                else
                {
                    if (ViewState["CodigoActivo"] != null)
                    {
                        CodigoActivo = (ViewState["CodigoActivo"].ToString());
                    }
                }

                if (IsPostBack)
                {
                    string eventTarget = Request["__EVENTTARGET"];
                    string eventArgument = Request["__EVENTARGUMENT"];

                    if (!string.IsNullOrEmpty(eventTarget) && eventTarget == UpdatePanel1.ClientID)
                    {
                        string[] args = eventArgument.Split(':');
                        if (args[0] == "DoubleClick")
                        {
                            ManejarDobleClic(args[1]);
                        }
                    }
                }
                this.lblTotalToners.Text = GridViewTonersEquipo.Rows.Count.ToString();
            }
            // Marca que la carga de la página se ha completado
            isPageLoadComplete = true;
        }

        public void ObtenerCustodio(string IDEquipo)
        {
            string sqlQuery = "SELECT TOP 1 Cedula, Nombres, Cargo FROM ViewCustodios WHERE IDEquipo = @IDEquipo ORDER BY Nombres";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@IDEquipo", IDEquipo);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar directamente a los labels sin crear una clase Custodio
                            this.lblCedula.Text = reader["Cedula"].ToString();
                            cedulaE = reader["Cedula"].ToString();
                            this.lblNombres.Text = reader["Nombres"].ToString();
                            this.lblCargo.Text = reader["Cargo"].ToString();

                        }
                        else
                        {
                            // Manejo de caso cuando no se encuentra ningún custodio
                            this.lblCedula.Text = "";
                            this.lblNombres.Text = "";
                            this.lblCargo.Text = "";
                        }
                    }
                }
            }
        }


        protected void ddlCustodio_SelectedIndexChanged(object sender, EventArgs e)
        {


            // Obtener los valores seleccionados del DropDownList
            string cedula = ddlCustodio.SelectedValue;
            string nombres = ddlCustodio.SelectedItem.Text;

            // Buscar el cargo del custodio desde el SqlDataSource
            DataTable dt = ((DataView)SqlDataSourceCustodios.Select(DataSourceSelectArguments.Empty)).ToTable();
            DataRow[] foundRows = dt.Select($"Cedula = '{cedula}'");

            if (foundRows.Length > 0)
            {
                // Asignar los valores a las etiquetas
                lblCedula.Text = cedula;
                lblNombres.Text = nombres;
                lblCargo.Text = foundRows[0]["Cargo"].ToString();
            }
            else
            {
                // Manejar el caso en que no se encuentre la cédula seleccionada
                lblCedula.Text = "Cédula no encontrada";
                lblNombres.Text = "";
                lblCargo.Text = "";
            }

            VisibilidadControlesEditar();

            AsignarCedulaCustodio();
        }



        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del DropDownList y asignarlo al TextBox
            VisibilidadControlesEditar();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisibilidadControlesEditar();
        }



        // Método para llenar los TextBox con los datos del equipo
        private void LlenarTextBoxsEquipo(string IDEquipo)
        {
            // Consulta SQL para obtener los datos del equipo
            string sqlQuery = "SELECT CodigoActivo, EquipoTipo, FechaAdquisicion, Marca, Modelo, Serie, Ubicacion, IP, Estado, Observaciones, GarantiaHastaCuando, FechaActualizacionDatos, EmpresaProveedora, NumeroDePuertosRJ45, Notas, Contrato, Imagenes, Campo2, Area, EnUso FROM IT_Equipos WHERE (IDEquipo = '" + IDEquipo + "')";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Abrir conexión
                    connection.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Verificar si se encontraron datos
                        {

                            // Guardar el valor de codigoActivo
                            CodigoActivo = reader["CodigoActivo"].ToString();
                            ViewState["CodigoActivo"] = CodigoActivo;

                            // Asignar los valores de los campos a los TextBox correspondientes
                            this.txtCodigoActivo.Text = reader["CodigoActivo"].ToString();

                            //Asignar a valor estaticos
                            EquipoTipo = reader["EquipoTipo"].ToString();
                            SetDropDownListValue(EquipoTipo);

                            object fechaAdquiValue = reader["FechaAdquisicion"];
                            if (fechaAdquiValue != DBNull.Value)
                            {
                                this.txtFechaAdqui.Text = ((DateTime)fechaAdquiValue).ToString("yyyy-MM-dd");
                                FechaAdquisicion= this.txtFechaAdqui.Text = ((DateTime)fechaAdquiValue).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                this.txtFechaAdqui.Text = string.Empty; // O cualquier valor predeterminado que desees asignar si el campo es nulo
                                FechaAdquisicion = string.Empty;
                            }
                            this.txtMarca.Text = reader["Marca"].ToString();
                            this.txtModelo.Text = reader["Modelo"].ToString();
                            this.txtSerie.Text = reader["Serie"].ToString();
                            this.txtUbicacion.Text = reader["Ubicacion"].ToString();
                            this.txtIP.Text = reader["IP"].ToString();
                            this.txtObservaciones.Text = reader["Observaciones"].ToString();
                            //Asignar a valor estaticos
                            Marca = reader["Marca"].ToString();
                            Modelo = reader["Modelo"].ToString();
                            Serie= reader["Serie"].ToString();
                            Ubicacion = reader["Ubicacion"].ToString();
                            Ip= reader["IP"].ToString();
                            Estado= reader["Estado"].ToString();
                            Observaciones = reader["Observaciones"].ToString();

                            // Asignar valor al DropDownList para el estado
                            if (ddlEstado.Items.FindByValue(Estado) != null)
                            {
                                ddlEstado.SelectedValue = Estado;
                            }

                            object garantiaValue = reader["GarantiaHastaCuando"];
                            if (garantiaValue != DBNull.Value)
                            {
                                this.txtGarantia.Text = ((DateTime)garantiaValue).ToString("yyyy-MM-dd");
                                Garantia= ((DateTime)garantiaValue).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                this.txtGarantia.Text = string.Empty; // O cualquier valor predeterminado que desees asignar si el campo es nulo
                                Garantia = string.Empty;
                            }


                            this.txtEmpresa.Text = reader["EmpresaProveedora"].ToString();
                            this.txtNumRJ45.Text = reader["NumeroDePuertosRJ45"].ToString();
                            this.txtNotas.Text = reader["Notas"].ToString();
                            this.txtContrato.Text = reader["Contrato"].ToString();
                            //Asignar a valor estaticos
                            EmpresaProveedora = reader["EmpresaProveedora"].ToString();
                            NumRJ45 = reader["NumeroDePuertosRJ45"].ToString();
                            Notas = reader["Notas"].ToString();
                            Contrato = reader["Contrato"].ToString();
                            Area = reader["Area"].ToString();

                            // Asignar valor al DropDownList para el área
                            if (ddlArea.Items.FindByValue(Area) != null)
                            {
                                ddlArea.SelectedValue = Area;
                            }
                            else
                            {
                                // Opcional: Manejar el caso donde el área no esté en el DropDownList
                                // Por ejemplo, podrías agregar un mensaje de error o seleccionar un valor predeterminado
                            }


                            // Asignar valor al CheckBox
                            if (bool.TryParse(reader["EnUso"].ToString(), out bool enusoValue))
                            {
                                this.chkEnUso.Checked = enusoValue;
                                Enuso = enusoValue.ToString();
                            }
                            else
                            {
                                // Manejar el caso en el que la conversión no sea válida
                                // Puedes establecer un valor predeterminado o manejar el error de otra manera
                            }

                        }
                        else
                        {
                            // No se encontraron datos para el IDEquipo proporcionado
                        }
                    }
                }
            }
        }

        private void SetDropDownListValue(string value)
        {
            // Asegúrate de que el DropDownList está cargado
            if (ddlEquipoTipo.Items.Count == 0)
            {
                // Puedes forzar la carga del DropDownList si es necesario
                ddlEquipoTipo.DataBind();
            }

            // Buscar el item en el DropDownList que coincide con el valor
            ListItem item = ddlEquipoTipo.Items.FindByValue(value);
            if (item != null)
            {
                // Establecer el valor seleccionado
                ddlEquipoTipo.SelectedValue = value;

            }
            else
            {
                // Opcional: manejar el caso en que el valor no se encuentra en el DropDownList
                // Por ejemplo, podrías agregar el valor como una opción nueva o mostrar un mensaje.
            }
        }

        private void SetDropDownListValueNombre(string text)
        {
            // Asegúrate de que el DropDownList está cargado
            if (ddlCustodio.Items.Count == 0)
            {
                // Puedes forzar la carga del DropDownList si es necesario
                ObtenerCustodio(IdEquipoE); // Si este método llena el DropDownList también, asegúrate de que se llame antes de intentar seleccionar el valor.
            }

            // Buscar el item en el DropDownList que coincide con el texto
            ListItem item = ddlCustodio.Items.FindByText(text);
            if (item != null)
            {
                // Establecer el valor seleccionado
                ddlCustodio.SelectedValue = item.Value;
                // Solo invoca el evento de cambio de selección si no es la primera carga de la página
                if (isPageLoadComplete)
                {
                    ddlCustodio_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            else
            {
                // Opcional: manejar el caso en que el valor no se encuentra en el DropDownList
                // Por ejemplo, podrías agregar el valor como una opción nueva o mostrar un mensaje.
            }
        }

        private void LlenarTextBoxsCpu(string IDEquipo)
        {
            // Consulta SQL para obtener los datos del equipo
            string sqlQuery = "SELECT [NombreDelEquipo], [Procesador], [ProcesadorVelocidad], [RAM], [NumDiscos], [Almacenamiento], [TipoLectorCD], [Wireless], [MAC] FROM [IT_Computadoras] WHERE IDEquipo = '" + IDEquipo + "'";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Abrir conexión
                    connection.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Verificar si se encontraron datos
                        {
                            // Asignar los valores de los campos a los TextBox correspondientes
                            this.txtNombreEquipo.Text = reader["NombreDelEquipo"].ToString();
                            this.txtProcesador.Text = reader["Procesador"].ToString();
                            this.txtProcesadorVelo.Text = reader["ProcesadorVelocidad"].ToString();
                            this.txtRam.Text = reader["RAM"].ToString();
                            this.txtNumDiscos.Text = reader["NumDiscos"].ToString();
                            this.txtAlmacenamiento.Text = reader["Almacenamiento"].ToString();
                            this.txtTipoLector.Text = reader["TipoLectorCD"].ToString();
                            this.txtMac.Text = reader["MAC"].ToString();

                            // Recuperar los datos de los campos de texto y pasar a los valores estaticos
                            NombreEquipo = txtNombreEquipo.Text;
                            Procesador = txtProcesador.Text;
                            ProcesadorVelocidad = txtProcesadorVelo.Text;
                            RAM = txtRam.Text;
                            NumDiscos = txtNumDiscos.Text;
                            Almacenamiento = txtAlmacenamiento.Text;
                            TipoLectorCD = txtTipoLector.Text;
                            MAC = txtMac.Text;
                            Wireless = cbWireless.Checked.ToString();

                            // Asignar valor al CheckBox
                            if (bool.TryParse(reader["Wireless"].ToString(), out bool wirelessValue))
                            {
                                this.cbWireless.Checked = wirelessValue;
                            }
                            else
                            {
                                // Manejar el caso en el que la conversión no sea válida
                                // Puedes establecer un valor predeterminado o manejar el error de otra manera
                            }
                        }
                        else
                        {
                            // No se encontraron datos para el IDEquipo proporcionado
                        }
                    }
                }
            }
        }

        // Método que actualiza la visibilidad de los controles de la página en modo edicion
        private void VisibilidadControlesEditar()
        {
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
            btnActualizar.Enabled = true;


            btnActualizarIcon.Attributes["class"] = "icon-button";
            btnCancelarIcon.Attributes["class"] = "icon-button";
            btnLogsIcon.Attributes["class"] = "icon-button hidden";
            btnNuevoEquipoIcon.Attributes["class"] = "icon-button hidden";
            btnEditarIcon.Attributes["class"] = "icon-button hidden";
            btnAtras.Attributes["class"] = "icon-button hidden";
            btnEquiposAIcon.Attributes["class"] = "icon-button hidden";
            btnCustodiosAntIcon.Attributes["class"] = "icon-button hidden";
            btnYupakIcon.Attributes["class"] = "icon-button hidden";

            this.ddlEquipoTipo.Visible = true;
            this.ddlEquipoTipo.Enabled = true;
            this.btnMostrarCalendario.Visible = true;
            this.btnMostrarCalendarioGarantia.Visible = true;
            this.ddlCustodio.Visible = true;
            this.ddlCustodio.Enabled = true;
            this.ddlEstado.Visible = true;
            this.ddlArea.Visible = true;
            this.ddlArea.Enabled = true;
            this.ddlEstado.Enabled = true;
            this.ddlEstado.Visible = true;
        }

        protected void btnMostrarCalendario_Click(object sender, EventArgs e)
        {
            if (calendarContainer.Style["display"] == "none")
            {
                // Si el calendario está oculto, mostrarlo
                calendarContainer.Style["display"] = "block";
                VisibilidadControlesEditar();
            }
            else
            {
                // Si el calendario está visible, ocultarlo
                calendarContainer.Style["display"] = "none";
                VisibilidadControlesEditar();
            }
        }
        protected void CalendarFechaAdquisicion_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha en el calendario,
            // asignarla al TextBox y ocultar el panel que contiene el calendario
            txtFechaAdqui.Text = CalendarFechaAdquisicion.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainer.Style["display"] = "none";
            VisibilidadControlesEditar();
        }


        protected void btnMostrarCalendarioGarantia_Click(object sender, EventArgs e)
        {
            if (calendarContainerGarantia.Style["display"] == "none")
            {
                // Si el calendario está oculto, mostrarlo
                calendarContainerGarantia.Style["display"] = "block";
                VisibilidadControlesEditar();
            }
            else
            {
                // Si el calendario está visible, ocultarlo
                calendarContainerGarantia.Style["display"] = "none";
                VisibilidadControlesEditar();
            }
        }
        protected void CalendarGarantia_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha en el calendario,
            // asignarla al TextBox y ocultar el panel que contiene el calendario
            txtGarantia.Text = CalendarGarantia.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainerGarantia.Style["display"] = "none";
            VisibilidadControlesEditar();
        }

        //Controles de los botones necesarios para la edicion
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarEdicionCamposTexto(true, this.divFmrEquipo); // Habilitar edición de campos de texto
            //Verificar el grupo del equipo
            if (grupoE.Equals("Computador"))
            {
                HabilitarEdicionCamposTexto(true, this.divFmrCpu);
            }
            else
            {
                if (grupoE.Equals("Impresora"))
                {
                    HabilitarEdicionCamposTexto(true, this.divFmrImpresora);
                }
            }

            VisibilidadControlesEditar();

        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Restaurar los valores originales de los campos

            // Deshabilitar edición de campos de texto
            HabilitarEdicionCamposTexto(false, this.divFmrEquipo);
            //Verificar el grupo del equipo
            //Verificar el grupo del equipo
            if (grupoE.Equals("Computador"))
            {
                HabilitarEdicionCamposTexto(false, this.divFmrCpu);
                LlenarTextBoxsCpu(IdEquipoE);
            }
            else
            {
                if (grupoE.Equals("Impresora"))
                {
                    HabilitarEdicionCamposTexto(false, this.divFmrImpresora);
                }
            }

            // Desactivar botones "Cancelar" y "Actualizar" y mostrar botón "Editar"
            //btnEditar.Enabled = true;
            //btnCancelar.Enabled = false;
            //btnActualizar.Enabled = false;

            btnEditarIcon.Attributes["class"] = "icon-button";
            btnActualizarIcon.Attributes["class"] = "icon-button hidden";
            btnCancelarIcon.Attributes["class"] = "icon-button hidden";
            btnAtras.Attributes["class"] = "icon-button hidden";
            btnEquiposAIcon.Attributes["class"] = "icon-button ";
            btnCustodiosAntIcon.Attributes["class"] = "icon-button ";
            btnYupakIcon.Attributes["class"] = "icon-button ";

            // Ocultar los controles adicionales
            btnMostrarCalendario.Visible = false;
            btnMostrarCalendarioGarantia.Visible = false;
            ddlEquipoTipo.Enabled = false;
            ddlEquipoTipo.Visible = true;
            ddlCustodio.Visible = true;
            ddlCustodio.Enabled = false;
            ddlEstado.Visible = true;
            ddlArea.Visible = true;
            ddlArea.Enabled = false;

            LlenarTextBoxsEquipo(IdEquipoE);
            //LLenar tabla Custodio
            ObtenerCustodio(IdEquipoE);
        }

        public void actualizarEquipoCustodio()
        {
            // Obtener los valores actualizados de los campos de texto
            string codigoActivo = txtCodigoActivo.Text;
            string equipoTipo = ddlEquipoTipo.Text;
            string cedulaCustodio = ddlCustodio.SelectedValue.ToString();
            string fechaAdquisicion = string.IsNullOrEmpty(txtFechaAdqui.Text) ? null : txtFechaAdqui.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            string ubicacion = txtUbicacion.Text;
            string ip = txtIP.Text;
            string estado = ddlEstado.SelectedValue.ToString();
            string observaciones = txtObservaciones.Text;
            string garantia = string.IsNullOrEmpty(txtGarantia.Text) ? null : txtGarantia.Text;
            string empresaProveedora = txtEmpresa.Text;
            string numRJ45 = txtNumRJ45.Text;
            string notas = txtNotas.Text;
            string contrato = txtContrato.Text;
            string area = ddlArea.SelectedValue.ToString();
            bool enuso = chkEnUso.Checked;

            // Ejecutar la consulta de actualización
            string query = "UPDATE [IT_Equipos] SET " +
                           "[CodigoActivo] = @CodigoActivo, " +
                           "[EquipoTipo] = @EquipoTipo, " +
                           "[CedulaCustodio] = @CedulaCustodio, " +
                           "[FechaAdquisicion] = @FechaAdquisicion, " +
                           "[Marca] = @Marca, " +
                           "[Modelo] = @Modelo, " +
                           "[Serie] = @Serie, " +
                           "[Ubicacion] = @Ubicacion, " +
                           "[IP] = @IP, " +
                           "[Estado] = @Estado, " +
                           "[Observaciones] = @Observaciones, " +
                           "[GarantiaHastaCuando] = @GarantiaHastaCuando, " +
                           "[EmpresaProveedora] = @EmpresaProveedora, " +
                           "[NumeroDePuertosRJ45] = @NumeroDePuertosRJ45, " +
                           "[Notas] = @Notas, " +
                           "[Contrato] = @Contrato, " +
                           "[Area] = @Area, " +
                           "[EnUso] = @EnUso " +
                           "WHERE [IDEquipo] = @IDEquipo";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Asignar valores a los parámetros, manejando DBNull para valores nulos
                command.Parameters.AddWithValue("@CodigoActivo", string.IsNullOrEmpty(codigoActivo) ? (object)DBNull.Value : codigoActivo);
                command.Parameters.AddWithValue("@EquipoTipo", string.IsNullOrEmpty(equipoTipo) ? (object)DBNull.Value : equipoTipo);
                command.Parameters.AddWithValue("@CedulaCustodio", string.IsNullOrEmpty(cedulaCustodio) ? (object)DBNull.Value : cedulaCustodio);
                command.Parameters.AddWithValue("@FechaAdquisicion", string.IsNullOrEmpty(fechaAdquisicion) ? (object)DBNull.Value : fechaAdquisicion);
                command.Parameters.AddWithValue("@Marca", string.IsNullOrEmpty(marca) ? (object)DBNull.Value : marca);
                command.Parameters.AddWithValue("@Modelo", string.IsNullOrEmpty(modelo) ? (object)DBNull.Value : modelo);
                command.Parameters.AddWithValue("@Serie", string.IsNullOrEmpty(serie) ? (object)DBNull.Value : serie);
                command.Parameters.AddWithValue("@Ubicacion", string.IsNullOrEmpty(ubicacion) ? (object)DBNull.Value : ubicacion);
                command.Parameters.AddWithValue("@IP", string.IsNullOrEmpty(ip) ? (object)DBNull.Value : ip);
                command.Parameters.AddWithValue("@Estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);
                command.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(observaciones) ? (object)DBNull.Value : observaciones);
                command.Parameters.AddWithValue("@GarantiaHastaCuando", string.IsNullOrEmpty(garantia) ? (object)DBNull.Value : garantia);
                command.Parameters.AddWithValue("@EmpresaProveedora", string.IsNullOrEmpty(empresaProveedora) ? (object)DBNull.Value : empresaProveedora);
                command.Parameters.AddWithValue("@NumeroDePuertosRJ45", string.IsNullOrEmpty(numRJ45) ? (object)DBNull.Value : numRJ45);
                command.Parameters.AddWithValue("@Notas", string.IsNullOrEmpty(notas) ? (object)DBNull.Value : notas);
                command.Parameters.AddWithValue("@Contrato", string.IsNullOrEmpty(contrato) ? (object)DBNull.Value : contrato);
                command.Parameters.AddWithValue("@Area", string.IsNullOrEmpty(area) ? (object)DBNull.Value : area);
                command.Parameters.AddWithValue("@EnUso", enuso);
                command.Parameters.AddWithValue("@IDEquipo", IdEquipoE);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    // Si rowsAffected es mayor que 0, significa que al menos una fila fue actualizada correctamente
                    if (rowsAffected > 0)
                    {
                        // Actualización exitosa
                    }
                    else
                    {
                        // No se actualizó ninguna fila
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir durante la ejecución de la consulta
                }
            }
        }



        private void ActualizarDatosEquipo()
        {
            string iDEquipo = IdEquipoE; // Debes obtener el ID del equipo de alguna manera, ya sea desde un TextBox u otro control en tu formulario

            // Recuperar los datos de los campos de texto
            string nombreEquipo = txtNombreEquipo.Text;
            string procesador = txtProcesador.Text;
            string procesadorVelocidad = txtProcesadorVelo.Text;
            string ram = txtRam.Text;
            string numDiscos = txtNumDiscos.Text;
            string almacenamiento = txtAlmacenamiento.Text;
            string tipoLectorCD = txtTipoLector.Text;
            string mac = txtMac.Text;
            bool wireless = cbWireless.Checked;


            // Consulta SQL para actualizar los datos del equipo
            string sqlQuery = "UPDATE [IT_Computadoras] SET [NombreDelEquipo] = @NombreEquipo, [Procesador] = @Procesador, [ProcesadorVelocidad] = @ProcesadorVelocidad, [RAM] = @ram, [NumDiscos] = @NumDiscos, [Almacenamiento] = @Almacenamiento, [TipoLectorCD] = @TipoLectorCD, [MAC] = @mac, [Wireless] = @Wireless WHERE IDEquipo = @IDEquipo";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Asignar valores a los parámetros
                    command.Parameters.AddWithValue("@IDEquipo", iDEquipo);
                    command.Parameters.AddWithValue("@NombreEquipo", nombreEquipo);
                    command.Parameters.AddWithValue("@Procesador", procesador);
                    command.Parameters.AddWithValue("@ProcesadorVelocidad", procesadorVelocidad);
                    command.Parameters.AddWithValue("@ram", ram);
                    command.Parameters.AddWithValue("@NumDiscos", numDiscos);
                    command.Parameters.AddWithValue("@Almacenamiento", almacenamiento);
                    command.Parameters.AddWithValue("@TipoLectorCD", tipoLectorCD);
                    command.Parameters.AddWithValue("@mac", mac);
                    command.Parameters.AddWithValue("@Wireless", wireless);

                    // Abrir conexión
                    connection.Open();

                    // Ejecutar la consulta de actualización
                    int rowsAffected = command.ExecuteNonQuery();

                }
            }

        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarEquipoCustodio();

            btnEditarIcon.Attributes["class"] = "icon-button";
            btnActualizarIcon.Attributes["class"] = "icon-button hidden";
            btnCancelarIcon.Attributes["class"] = "icon-button hidden";
            btnAtras.Attributes["class"] = "icon-button hidden";
            btnEquiposAIcon.Attributes["class"] = "icon-button";
            btnCustodiosAntIcon.Attributes["class"] = "icon-button";
            btnYupakIcon.Attributes["class"] = "icon-button";

            // Ocultar los controles adicionales
            btnMostrarCalendario.Visible = false;
            btnMostrarCalendarioGarantia.Visible = false;
            ddlCustodio.Visible = true;
            ddlEstado.Visible = false;
            ddlArea.Visible = true;

            // Deshabilitar edición de campos de texto
            HabilitarEdicionCamposTexto(false, this.divFmrEquipo);
            //Verificar el grupo del equipo
            if (grupoE.Equals("Computador"))
            {
                HabilitarEdicionCamposTexto(false, this.divFmrCpu);
                ActualizarDatosEquipo();
            }
            else
            {
                if (grupoE.Equals("Impresora"))
                {
                    HabilitarEdicionCamposTexto(false, this.divFmrImpresora);
                }
            }

            //LOG
            // Comparar los valores originales con los valores actualizados
            StringBuilder detalle = new StringBuilder();

            // Comparar cada parámetro y construir el detalle del log
            if (CodigoActivo != txtCodigoActivo.Text)
                detalle.AppendLine($"Código Activo: {CodigoActivo} -> {txtCodigoActivo.Text} ");
            if (EquipoTipo != ddlEquipoTipo.Text)
                detalle.AppendLine($"Equipo Tipo: {EquipoTipo} -> {ddlEquipoTipo.Text} ");
            if (CedulaCustodio != ddlCustodio.SelectedValue.ToString())
                detalle.AppendLine($"Cédula Custodio: {CedulaCustodio} -> {ddlCustodio.SelectedValue.ToString()} ");
            if (Marca != txtMarca.Text)
                detalle.AppendLine($"Marca: {Marca} -> {txtMarca.Text} ");
            if (Modelo != txtModelo.Text)
                detalle.AppendLine($"Modelo: {Modelo} -> {txtModelo.Text} ");
            if (Serie != txtSerie.Text)
                detalle.AppendLine($"Serie: {Serie} -> {txtSerie.Text} ");
            if (Ubicacion != txtUbicacion.Text)
                detalle.AppendLine($"Ubicación: {Ubicacion} -> {txtUbicacion.Text} ");
            if (Ip != txtIP.Text)
                detalle.AppendLine($"Ip: {Ip } -> {txtIP.Text} ");
            if (Estado != ddlEstado.SelectedValue.ToString())
                detalle.AppendLine($"Estado: {Estado} -> {ddlEstado.SelectedValue.ToString()} ");
            if (Observaciones != txtObservaciones.Text)
                detalle.AppendLine($"Observaciones: {Observaciones} -> {txtObservaciones.Text} ");
            if (Garantia != txtGarantia.Text)
                detalle.AppendLine($"Garantia: {Garantia} -> {txtGarantia.Text} ");
            if (EmpresaProveedora != txtEmpresa.Text)
                detalle.AppendLine($"EmpresaProveedora: {EmpresaProveedora} -> {txtEmpresa.Text} ");
            if (NumRJ45 != txtNumRJ45.Text)
                detalle.AppendLine($"NumRJ45: {NumRJ45} -> {txtNumRJ45.Text} ");
            if (Notas != txtNotas.Text)
                detalle.AppendLine($"Notas: {Notas } -> {txtNotas.Text} ");
            if (Contrato != txtContrato.Text)
                detalle.AppendLine($"Contrato: {Contrato} -> {txtContrato.Text} ");
            if (Area != ddlArea.SelectedValue.ToString())
                detalle.AppendLine($"Area: {Area} -> {ddlArea.SelectedValue.ToString()} ");
            if (Enuso != chkEnUso.Checked.ToString())
                detalle.AppendLine($"Enuso: {Enuso} -> {chkEnUso.Checked.ToString()} ");

            if (NombreEquipo != txtNombreEquipo.Text)
                detalle.AppendLine($"NombreEquipo: {NombreEquipo } -> {txtNombreEquipo.Text} ");
            if (Procesador != txtProcesador.Text)
                detalle.AppendLine($"Procesador: {Procesador} -> {txtProcesador.Text} ");
            if (ProcesadorVelocidad != txtProcesadorVelo.Text)
                detalle.AppendLine($"ProcesadorVelocidad: {ProcesadorVelocidad } -> {txtProcesadorVelo.Text} ");
            if (RAM != txtRam.Text)
                detalle.AppendLine($"Serie: {RAM } -> {txtRam.Text} ");
            if (NumDiscos != txtNumDiscos.Text)
                detalle.AppendLine($"NumDiscos: {NumDiscos } -> {txtNumDiscos.Text} ");
            if (Almacenamiento != txtAlmacenamiento.Text)
                detalle.AppendLine($"Almacenamiento: {Almacenamiento } -> {txtAlmacenamiento.Text} ");
            if (TipoLectorCD != txtTipoLector.Text)
                detalle.AppendLine($"TipoLectorCD: {TipoLectorCD } -> {txtTipoLector.Text} ");
            if (MAC != txtMac.Text)
                detalle.AppendLine($"MAC: {MAC  } -> {txtMac.Text} ");
            if (Wireless != cbWireless.Checked.ToString())
                detalle.AppendLine($"Wireless: {Wireless } -> {cbWireless.Checked.ToString()} ");

            // Crear una instancia de Logger
            Logger logger = new Logger();
            string nombre = Session["Usuario"] as string;

            // Convertir el StringBuilder a string
            string detalleString = detalle.ToString();

            // Registrar la acción en el log
            logger.RegistrarLog(nombre, "Actualizar", "IT_Equipos", detalleString, CodigoActivo);

            // Llenar los TextBox con los datos del equipo
            LlenarTextBoxsEquipo(IdEquipoE);
            //LLenar tabla Custodio
            ObtenerCustodio(IdEquipoE);
        }

        //protected void btnLogs_Click(object sender, EventArgs e)
        //{
        //    // Asignar el valor de la variable estática a un parámetro de consulta
        //    string url = $"FrmReportes.aspx?codigoActivo={CodigoActivo}";

        //    // Redirige a la página de reportes con el parámetro de búsqueda
        //    Response.Redirect(url);
        //}

        protected void btnLogs_Click(object sender, EventArgs e)
        {
            // Asignar el valor de CodigoActivo al parámetro del SqlDataSource
            SqlDataSourceLogs.SelectParameters["CodigoActivo"].DefaultValue = CodigoActivo.ToString();

            // Forzar la actualización del SqlDataSource
            SqlDataSourceLogs.DataBind();

            // Abre el modal mediante script
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#logsModal').modal('show');", true);
        }


        private void HabilitarEdicionCamposTexto(bool habilitar, HtmlGenericControl divFmr)
        {
            if (divFmr != null)
            {
                // Iterar a través de los controles dentro del div
                foreach (Control control in divFmr.Controls)
                {
                    // Verificar si el control es un TextBox y no es el que queremos omitir
                    if (control is TextBox && control.ID != "txtIDEquipo")
                    {
                        ((TextBox)control).Enabled = habilitar;
                    }
                    // Verificar si el control es un CheckBox
                    else if (control is CheckBox)
                    {
                        ((CheckBox)control).Enabled = habilitar;
                    }
                }
            }
        }




        private void DeshabilitarCamposTexto(HtmlGenericControl divFmr)
        {
            if (divFmr != null)
            {
                // Iterar a través de los controles dentro del div
                foreach (Control control in divFmr.Controls)
                {
                    if (control is TextBox)
                    {
                        // Deshabilitar el control visualmente
                        ((TextBox)control).Enabled = false;
                    }
                    // Verificar si el control es un CheckBox
                    else if (control is CheckBox)
                    {
                        // Deshabilitar el control visualmente
                        ((CheckBox)control).Enabled = false;
                    }
                }
            }
        }

        private void cargarTablaSoft()
        {
            string consultaSQL = "SELECT * FROM [dbo].[IT_Software] WHERE IDEquipo = '" + IdEquipoE + "'";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                SqlCommand command = new SqlCommand(consultaSQL, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Encuentra el tbody de la tabla. En ASP.NET, el tbody es parte de la colección Rows del HtmlTable.
                // Así que necesitamos limpiar las filas pero mantener el encabezado
                while (this.tablaSoft.Rows.Count > 1)
                {
                    this.tablaSoft.Rows.RemoveAt(1); // Borra todas las filas excepto la primera, que es el encabezado
                }

                // Iteramos sobre los datos y creamos filas y celdas para cada elemento
                while (reader.Read())
                {
                    HtmlTableRow row = new HtmlTableRow(); // Usa HtmlTableRow en lugar de TableRow

                    HtmlTableCell idCell = new HtmlTableCell(); // Usa HtmlTableCell en lugar de TableCell
                    idCell.InnerText = reader["IDSoftware"].ToString();
                    row.Cells.Add(idCell);

                    HtmlTableCell descripcionCell = new HtmlTableCell(); // Usa HtmlTableCell en lugar de TableCell
                    descripcionCell.InnerText = reader["Descripcion"].ToString();
                    row.Cells.Add(descripcionCell);

                    HtmlTableCell licenciaCell = new HtmlTableCell(); // Usa HtmlTableCell en lugar de TableCell
                    licenciaCell.InnerText = reader["Licencia"].ToString();
                    row.Cells.Add(licenciaCell);

                    HtmlTableCell legalCell = new HtmlTableCell();
                    bool isChecked = reader["Legal"] != DBNull.Value && Convert.ToBoolean(reader["Legal"]);
                    CheckBox legalCheckBox = new CheckBox
                    {
                        Checked = isChecked, // Establecer el estado del checkbox basado en el valor de la base de datos
                        Enabled = false      // Hacer el checkbox de solo lectura si no quieres que el usuario lo cambie
                    };
                    legalCell.Controls.Add(legalCheckBox);
                    row.Cells.Add(legalCell);

                    this.tablaSoft.Rows.Add(row); // Añade la fila a la tabla
                }

                reader.Close();
            }
        }


        private void cargarTablaToners()
        {
            //string consultaSQL = @"SELECT 
            //                        t.IDToner,
            //                        t.TipoToner,
            //                        t.Stock,
            //                        t.Bodega
            //                    FROM 
            //                        IT_Equipos eq
            //                    INNER JOIN 
            //                        IT_EquiposToners et ON eq.IDEquipo = et.IDEquipo
            //                    INNER JOIN 
            //                        IT_Toners t ON et.IDToner = t.IDToner
            //                    WHERE eq.IDEquipo = '"+idEquipoE+"';";

            //using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            //{
            //    SqlCommand command = new SqlCommand(consultaSQL, connection);
            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    // Encuentra el tbody de la tabla. En ASP.NET, el tbody es parte de la colección Rows del HtmlTable.
            //    // Así que necesitamos limpiar las filas pero mantener el encabezado
            //    while (this.tablaToner.Rows.Count > 1)
            //    {
            //        this.tablaToner.Rows.RemoveAt(1); // Borra todas las filas excepto la primera, que es el encabezado
            //    }

            //    // Iteramos sobre los datos y creamos filas y celdas para cada elemento
            //    while (reader.Read())
            //    {
            //        HtmlTableRow row = new HtmlTableRow();

            //        HtmlTableCell idTonerCell = new HtmlTableCell();
            //        idTonerCell.InnerText = reader["IDToner"].ToString();
            //        row.Cells.Add(idTonerCell);

            //        HtmlTableCell tipoTonerCell = new HtmlTableCell();
            //        tipoTonerCell.InnerText = reader["TipoToner"].ToString();
            //        row.Cells.Add(tipoTonerCell);

            //        HtmlTableCell stockCell = new HtmlTableCell();
            //        stockCell.InnerText = reader["Stock"].ToString();
            //        row.Cells.Add(stockCell);

            //        HtmlTableCell bodegaCell = new HtmlTableCell();
            //        bodegaCell.InnerText = reader["Bodega"].ToString();
            //        row.Cells.Add(bodegaCell);

            //        tablaToner.Rows.Add(row);
            //    }

            //    reader.Close();
            //}
        }



        protected void btnCustodiosAnt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "$('#miModal').modal('show');", true);
        }

        private void AsignarCedulaCustodio(SqlDataSourceSelectingEventArgs e)
        {
            // Establece el valor del parámetro
            e.Command.Parameters["@CedulaCustodio"].Value = this.lblCedula.Text;

        }

        private void AsignarCedulaCustodio()
        {
            // Esto se puede usar para establecer el parámetro antes de la selección
            sdsEquiposAdi.SelectParameters["CedulaCustodio"].DefaultValue = this.lblCedula.Text;

        }


        protected void sdsEquiposAdi_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            AsignarCedulaCustodio(e);

        }



        protected void btnEquiposA_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEquiposAdi", "$('#modalEquiposAdi').modal('show');", true);
            this.lblTotalEquipos.Text = GridViewEquiposAdi.Rows.Count.ToString();
        }


        protected void GridViewEquiposAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grupoG;

            GridViewRow fila = this.GridViewEquiposAdi.SelectedRow;

            //Consulta para obtener el grupo del equipo
            string consultaSQL = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = '" + fila.Cells[1].Text + "'";
            using (SqlConnection conexion = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    conexion.Open();
                    grupoG = (string)comando.ExecuteScalar();
                }
            }

            //Se redireciona los datos obtenidos a la nueva página
            Response.Redirect("FrmEquipo.aspx?IDEquipo=" + fila.Cells[4].Text + "&CedulaCustodio=" + cedulaE + "&Grupo=" + grupoG);

        }

        protected void GridViewEquiposAdi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Agregar atributo onclick a cada fila
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewEquiposAdi, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }

        protected void btnYupak_Click1(object sender, EventArgs e)
        {
            // Llama a la función de JavaScript para redirigir con el parámetro
            ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "redirectToFrmYupak('" + CodigoActivo + "');", true);
        }


        protected void GridViewToner_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = this.GridViewToner.SelectedRow;
            //Se redireciona los datos obtenidos a la nueva página
            Response.Redirect("FrmToner.aspx?IDToner=" + fila.Cells[0].Text);

        }

        protected void GridViewToner_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Agregar atributo onclick a cada fila
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewToner, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable                
            }
        }

        protected void btnVerMas_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalVerMas", "$('#modalVerMas').modal('show');", true);
        }


        protected void btnAñadirSoft_Click(object sender, EventArgs e)
        {
            // Verificar si IDEquipo es 0
            int idequipo = GetIDEquipo(); // Método para obtener el IDEquipo actual

            if (idequipo == 0)
            {
                // Mostrar un mensaje de alerta indicando que primero se debe crear el equipo
                string errorMessage = "Primero debe crear el equipo antes de añadir software.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
            else
            {
                // Abrir el modal si IDEquipo es diferente de 0
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAddSoft", "$('#modalAddSoft').modal('show');", true);
            }
        }

        // Método para obtener el IDEquipo actual
        private int GetIDEquipo()
        {
            // Obtener el ID del equipo de la URL
            string idEquipoStr = Request.QueryString["IDEquipo"];
            if (int.TryParse(idEquipoStr, out int idEquipo))
            {
                return idEquipo; // Retorna el ID del equipo si se ha parseado correctamente
            }
            return 0; // Retorna 0 si no se puede parsear
        }

        protected void btnNuevoEquipo_Click(object sender, EventArgs e)
        {
            string nuevoEquipo = "SI";
            //Response.Redirect("FrmNuevoEquipo.aspx");
            Response.Redirect("FrmEquipo.aspx?IDEquipo=" + "0" + "&Grupo=" + "" + "&nuevoEquipo=" + nuevoEquipo);
        }

        protected void btnAddSoft_Click(object sender, EventArgs e)
        {
            // Recoger los valores de los controles
            string idequipo = IdEquipoE; // Este valor lo obtienes desde el servidor según tu lógica
            string descripcion = ddlDescripcion.SelectedValue;
            string licencia = txtLicencia.Text;
            bool legal = chkLegal.Checked;

            // Cadena de conexión desde el Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            // Consulta SQL para insertar el nuevo registro
            string query = "INSERT INTO IT_Software (IDEquipo, Descripcion, Licencia, Legal) VALUES (@IDEquipo, @Descripcion, @Licencia, @Legal)";

            // Insertar el nuevo software en la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Añadir parámetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@IDEquipo", idequipo);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Licencia", licencia);
                    cmd.Parameters.AddWithValue("@Legal", legal);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        // Mostrar mensaje de éxito, redirigir o actualizar UI según sea necesario
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones
                    }
                }
            }
            cargarTablaSoft();
            txtLicencia.Text = "";
            chkLegal.Checked = false;
            ddlDescripcion.DataBind();
            ddlSoftware.DataBind();
        }

        protected void btnEliminarSoft_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalDelSoft", "$('#modalDelSoft').modal('show');", true);
        }
        protected void btnDelSoft_Click(object sender, EventArgs e)
        {
            // Recoger el ID del software seleccionado
            string idSoftware = ddlSoftware.SelectedValue.ToString();

            // Cadena de conexión desde el Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            // Consulta SQL para eliminar el registro
            string query = "DELETE FROM IT_Software WHERE IDSoftware = @IDSoftware";

            // Eliminar el software de la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Añadir el parámetro
                    cmd.Parameters.AddWithValue("@IDSoftware", idSoftware);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        // Mostrar mensaje de éxito, redirigir o actualizar UI según sea necesario
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones
                    }
                }
            }
            cargarTablaSoft();
            ddlSoftware.DataBind();
        }

        protected void btnAñadirTipoSoft_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAddTipoSoft", "$('#modalAddTipoSoft').modal('show');", true);
        }
        protected void btnAddTipoSoft_Click(object sender, EventArgs e)
        {
            // Recoger el tipo de software desde el TextBox
            string tipoSoftware = txtTipoSoft.Text.Trim();

            // Verificar que el campo no esté vacío
            if (!string.IsNullOrEmpty(tipoSoftware))
            {
                // Cadena de conexión desde el Web.config
                string connectionString = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

                // Consulta SQL para insertar un nuevo tipo de software
                string query = "INSERT INTO IT_SoftwareTipo (Descripcion) VALUES (@Descripcion)";

                // Insertar el nuevo tipo de software en la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Añadir el parámetro
                        cmd.Parameters.AddWithValue("@Descripcion", tipoSoftware);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            // Mostrar mensaje de éxito, redirigir o actualizar UI según sea necesario
                            Response.Write("Tipo de software añadido correctamente.");
                        }
                        catch (Exception ex)
                        {
                            // Manejar excepciones
                            Response.Write("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // Manejar el caso en que el campo de texto esté vacío
                Response.Write("Por favor, introduzca un tipo de software.");
            }
            ddlDescripcion.DataBind();
            txtTipoSoft.Text = "";
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {

            btnEditarIcon.Attributes["class"] = "icon-button";
            btnActualizarIcon.Attributes["class"] = "icon-button hidden";
            btnCancelarIcon.Attributes["class"] = "icon-button hidden";
            btnAtras.Attributes["class"] = "icon-button hidden";

            // Ocultar los controles adicionales
            btnMostrarCalendario.Visible = false;
            btnMostrarCalendarioGarantia.Visible = false;
            ddlCustodio.Visible = true;
            ddlEstado.Visible = true;
            ddlArea.Visible = true;

            string url = "FrmVisorImagenes.aspx?CodigoActivo=" + txtCodigoActivo.Text.ToString();

            // Script para abrir una ventana centrada
            string strScript = "var width = 1200;" +
                               "var height = 650;" +
                               "var left = (window.screen.availWidth / 2) - (width / 2);" +
                               "var top = (window.screen.availHeight / 2) - (height / 2);" +
                               "window.open('" + url + "', '_blank', 'width=' + width + ',height=' + height + ',top=' + top + ',left=' + left);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "btnVer_Click", strScript, true);
        }

        protected void btnYupak_Click(object sender, EventArgs e)
        {
            string url = "FrmYupak.aspx?CodigoActivo=" + CodigoActivo;

            string script = "var screenWidth = window.screen.availWidth;" +
                            "var screenHeight = window.screen.availHeight;" +
                            "window.open('" + url + "', '_blank', 'width=' + (screenWidth / 2) + ',height=' + screenHeight + ',top=0,left=' + (screenWidth / 2));";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "btnYupak_Click", script, true);
        }


        //Funciones Modal Añadir Toner -----------
        public void InsertarEquiposToners(string IDEquipo, string IDToner)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(
                            "INSERT INTO IT_EquiposToners (IDEquipo, IDToner) VALUES (@IDEquipo, @IDToner)", connection))
                    {
                        command.Parameters.AddWithValue("@IDEquipo", IDEquipo);
                        command.Parameters.AddWithValue("@IDToner", IDToner);

                        command.ExecuteNonQuery();
                    }
                }

                // Muestra un mensaje de éxito después de insertar los datos
                string successMessage = "Los datos han sido ingresados correctamente.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre alguna excepción
                string errorMessage = $"Ocurrió un error al ingresar los datos";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
        }
        protected void GridViewTonerModal_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = this.GridViewTonerModal.SelectedRow;
            string idToner = fila.Cells[0].Text;
            InsertarEquiposToners(IdEquipoE, idToner);
            this.GridViewTonersEquipo.DataBind();
            this.lblTotalToners.Text = GridViewTonersEquipo.Rows.Count.ToString();
        }


        protected void GridViewTonerModal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = "if (!confirmSelection(this)) return; " + Page.ClientScript.GetPostBackClientHyperlink(GridViewTonerModal, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }
        protected void btnAñadirToner_Click(object sender, EventArgs e)
        {
            // Verificar si IDEquipo es 0
            int idequipo = GetIDEquipo(); // Método para obtener el IDEquipo actual

            if (idequipo == 0)
            {
                // Mostrar un mensaje de alerta indicando que primero se debe crear el equipo
                string errorMessage = "Primero debe crear el equipo antes de añadir un toner.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
            else
            {
                // Abrir el modal si IDEquipo es diferente de 0
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAñadirToner", "$('#modalAñadirToner').modal('show');", true);
            }
        }
        //------------ Funciones Modal Añadir Toner


        //------------ Funciones Toners
        private void ManejarDobleClic(string idToner)
        {
            //Se redireciona los datos obtenidos a la nueva página
            Response.Redirect("FrmToner.aspx?IDToner=" + idToner);
        }
        protected void GridViewTonersEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ID del equipo desde DataKeys
            string idToner = GridViewTonersEquipo.SelectedDataKey.Value.ToString();

            // Asignar el ID del equipo al label (para verificar visualmente)
            lblTonerS.Text = idToner;

            // Asignar el ID del equipo a la variable de JavaScript
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetSelectedToner", $"selectedToner = '{idToner}';", true);
        }
        protected void GridViewTonersEquipo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idToner = DataBinder.Eval(e.Row.DataItem, "IDToner").ToString();
                //e.Row.Attributes["onclick"] = $"rowClick(this, '{idEquipo}');";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewTonersEquipo, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["ondblclick"] = $"rowDoubleClick(this, '{idToner}');";
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable              
            }
        }

        protected void btnEliminarToner_Click(object sender, EventArgs e)
        {
            string toner = lblTonerS.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(
                        "DELETE FROM IT_EquiposToners WHERE IDToner = @IDToner AND IDEquipo = @IDEquipo", connection))
                    {
                        command.Parameters.AddWithValue("@IDToner", toner);
                        command.Parameters.AddWithValue("@IDEquipo", IdEquipoE);

                        command.ExecuteNonQuery();
                    }
                }

                // Muestra un mensaje de éxito después de eliminar la relación
                string successMessage = "El tóner ha sido eliminado correctamente.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);

                // Deseleccionar la fila seleccionada y limpiar las variables
                GridViewTonersEquipo.SelectedIndex = -1;
                lblTonerS.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "resetSelectedIdToner", "selectedIdToner = null;", true);

                this.GridViewTonersEquipo.DataBind();
                this.lblTotalToners.Text = GridViewTonersEquipo.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre alguna excepción
                string errorMessage = $"Ocurrió un error al eliminar el tóner.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
        }

        protected void btnCrearEquipo_Click(object sender, EventArgs e)
        {
            btnCrearEquipo.Enabled = false; // Deshabilitar el botón para prevenir múltiples clics

            // Obtener los valores actualizados de los campos de texto
            string codigoActivo = this.txtCodigoActivo.Text;
            string equipoTipo = ddlEquipoTipo.SelectedItem.Text;
            string cedulaCustodio = lblCedula.Text;
            string fechaAdquisicion = string.IsNullOrEmpty(txtFechaAdqui.Text) ? null : txtFechaAdqui.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            string ubicacion = txtUbicacion.Text;
            string ip = txtIP.Text;
            string estado = ddlEstado.SelectedItem.Text;
            string observaciones = txtObservaciones.Text;
            string garantia = string.IsNullOrEmpty(txtGarantia.Text) ? null : txtGarantia.Text;
            string empresaProveedora = txtEmpresa.Text;
            string numRJ45 = txtNumRJ45.Text;
            string notas = txtNotas.Text;
            string contrato = txtContrato.Text;
            string area = ddlArea.SelectedItem.Text;
            bool enuso = chkEnUso.Checked;

            string grupo = ""; // Inicializar el grupo

            // Consulta para obtener el grupo del equipo
            string consultaGrupo = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = @EquipoTipo";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    // Obtener el grupo de la base de datos
                    using (SqlCommand commandGrupo = new SqlCommand(consultaGrupo, connection))
                    {
                        commandGrupo.Parameters.AddWithValue("@EquipoTipo", equipoTipo);
                        grupo = (string)commandGrupo.ExecuteScalar();
                    }

                    // Verificar si el registro ya existe
                    string checkQuery = "SELECT COUNT(*) FROM [IT_Equipos] WHERE [CodigoActivo] = @CodigoActivo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@CodigoActivo", codigoActivo);
                        int existingRecords = (int)checkCommand.ExecuteScalar();
                        if (existingRecords > 0)
                        {
                            string errorMessage = "El código activo ya existe.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
                            return; // Salir del método si ya existe el registro
                        }
                    }

                    // Consulta para insertar en IT_Equipos
                    string queryInsertEquipos = "INSERT INTO [IT_Equipos] (" +
                                                 "[CodigoActivo], " +
                                                 "[EquipoTipo], " +
                                                 "[CedulaCustodio], " +
                                                 "[FechaAdquisicion], " +
                                                 "[Marca], " +
                                                 "[Modelo], " +
                                                 "[Serie], " +
                                                 "[Ubicacion], " +
                                                 "[IP], " +
                                                 "[Estado], " +
                                                 "[Observaciones], " +
                                                 "[GarantiaHastaCuando], " +
                                                 "[EmpresaProveedora], " +
                                                 "[NumeroDePuertosRJ45], " +
                                                 "[Notas], " +
                                                 "[Contrato], " +
                                                 "[Area], " +
                                                 "[EnUso]) " +
                                                 "VALUES (" +
                                                 "@CodigoActivo, " +
                                                 "@EquipoTipo, " +
                                                 "@CedulaCustodio, " +
                                                 "@FechaAdquisicion, " +
                                                 "@Marca, " +
                                                 "@Modelo, " +
                                                 "@Serie, " +
                                                 "@Ubicacion, " +
                                                 "@IP, " +
                                                 "@Estado, " +
                                                 "@Observaciones, " +
                                                 "@GarantiaHastaCuando, " +
                                                 "@EmpresaProveedora, " +
                                                 "@NumeroDePuertosRJ45, " +
                                                 "@Notas, " +
                                                 "@Contrato, " +
                                                 "@Area, " +
                                                 "@EnUso);" +
                                                 "SELECT SCOPE_IDENTITY();"; // Obtener el ID del nuevo registro

                    // Ejecutar la inserción en IT_Equipos
                    SqlCommand command = new SqlCommand(queryInsertEquipos, connection);

                    // Asignar valores a los parámetros, manejando DBNull para valores nulos
                    command.Parameters.AddWithValue("@CodigoActivo", string.IsNullOrEmpty(codigoActivo) ? (object)DBNull.Value : codigoActivo);
                    command.Parameters.AddWithValue("@EquipoTipo", string.IsNullOrEmpty(equipoTipo) ? (object)DBNull.Value : equipoTipo);
                    command.Parameters.AddWithValue("@CedulaCustodio", string.IsNullOrEmpty(cedulaCustodio) ? (object)DBNull.Value : cedulaCustodio);
                    command.Parameters.AddWithValue("@FechaAdquisicion", string.IsNullOrEmpty(fechaAdquisicion) ? (object)DBNull.Value : fechaAdquisicion);
                    command.Parameters.AddWithValue("@Marca", string.IsNullOrEmpty(marca) ? (object)DBNull.Value : marca);
                    command.Parameters.AddWithValue("@Modelo", string.IsNullOrEmpty(modelo) ? (object)DBNull.Value : modelo);
                    command.Parameters.AddWithValue("@Serie", string.IsNullOrEmpty(serie) ? (object)DBNull.Value : serie);
                    command.Parameters.AddWithValue("@Ubicacion", string.IsNullOrEmpty(ubicacion) ? (object)DBNull.Value : ubicacion);
                    command.Parameters.AddWithValue("@IP", string.IsNullOrEmpty(ip) ? (object)DBNull.Value : ip);
                    command.Parameters.AddWithValue("@Estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);
                    command.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(observaciones) ? (object)DBNull.Value : observaciones);
                    command.Parameters.AddWithValue("@GarantiaHastaCuando", string.IsNullOrEmpty(garantia) ? (object)DBNull.Value : garantia);
                    command.Parameters.AddWithValue("@EmpresaProveedora", string.IsNullOrEmpty(empresaProveedora) ? (object)DBNull.Value : empresaProveedora);
                    command.Parameters.AddWithValue("@NumeroDePuertosRJ45", string.IsNullOrEmpty(numRJ45) ? (object)DBNull.Value : numRJ45);
                    command.Parameters.AddWithValue("@Notas", string.IsNullOrEmpty(notas) ? (object)DBNull.Value : notas);
                    command.Parameters.AddWithValue("@Contrato", string.IsNullOrEmpty(contrato) ? (object)DBNull.Value : contrato);
                    command.Parameters.AddWithValue("@Area", string.IsNullOrEmpty(area) ? (object)DBNull.Value : area);
                    command.Parameters.AddWithValue("@EnUso", enuso);

                    // Obtener el IDEquipo
                    int idequipo = Convert.ToInt32(command.ExecuteScalar());

                    // Solo insertar en IT_Computadoras si el grupo es "Computador"
                    if (grupo.Equals("Computador"))
                    {
                        string queryInsertComputadora = "INSERT INTO [IT_Computadoras] (" +
                                                        "[IDEquipo], " +
                                                        "[NombreDelEquipo], " +
                                                        "[Procesador], " +
                                                        "[ProcesadorVelocidad], " +
                                                        "[RAM], " +
                                                        "[NumDiscos], " +
                                                        "[Almacenamiento], " +
                                                        "[TipoLectorCD], " +
                                                        "[Wireless], " +
                                                        "[MAC]) " +
                                                        "VALUES (" +
                                                        "@IDEquipo, " +
                                                        "@NombreDelEquipo, " +
                                                        "@Procesador, " +
                                                        "@ProcesadorVelocidad, " +
                                                        "@RAM, " +
                                                        "@NumDiscos, " +
                                                        "@Almacenamiento, " +
                                                        "@TipoLectorCD, " +
                                                        "@Wireless, " +
                                                        "@MAC)";

                        SqlCommand commandComputadora = new SqlCommand(queryInsertComputadora, connection);
                        commandComputadora.Parameters.AddWithValue("@IDEquipo", idequipo);
                        commandComputadora.Parameters.AddWithValue("@NombreDelEquipo", txtNombreEquipo.Text);
                        commandComputadora.Parameters.AddWithValue("@Procesador", txtProcesador.Text);
                        commandComputadora.Parameters.AddWithValue("@ProcesadorVelocidad", txtProcesadorVelo.Text);
                        commandComputadora.Parameters.AddWithValue("@RAM", txtRam.Text);
                        commandComputadora.Parameters.AddWithValue("@NumDiscos", txtNumDiscos.Text);
                        commandComputadora.Parameters.AddWithValue("@Almacenamiento", txtAlmacenamiento.Text);
                        commandComputadora.Parameters.AddWithValue("@TipoLectorCD", txtTipoLector.Text);
                        commandComputadora.Parameters.AddWithValue("@Wireless", cbWireless.Checked); // Suponiendo que es un CheckBox
                        commandComputadora.Parameters.AddWithValue("@MAC", txtMac.Text);

                        // Ejecutar el segundo insert
                        commandComputadora.ExecuteNonQuery();
                    }

                    string successMessage = "Los datos han sido ingresados correctamente.";

                    //LOG
                    // Crear una instancia de Logger
                    Logger logger = new Logger();
                    string usuario = Session["Usuario"] as string;
                    string detalle = "Creo el nuevo equipo que con los siguientes parámetros: " + "IDEquipo: " + idequipo +
            "  Código Activo: " + codigoActivo + "  Equipo Tipo: " + equipoTipo +
            "  Cédula Custodio: " + cedulaCustodio + "  Marca: " + marca +
            "  Modelo: " + modelo + "  Serie: " + serie + "  Ubicación: " + ubicacion;

                    // Registrar la acción en el log
                    logger.RegistrarLog(usuario, "Eliminar", "IT_Equipos", detalle, CodigoActivo);

                    Session["IDEquipo"] = idequipo;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);
                    // Redirigir a la nueva URL con el IDEquipo y el grupo
                    Response.Redirect("FrmEquipo.aspx?IDEquipo=" + idequipo + "&Grupo=" + grupo);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución de la consulta
                // Muestra un mensaje de error si ocurre alguna excepción
                string errorMessage = $"Error: {ex.Message}";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
        }

        protected void ddlEquipoTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlEquipoTipo.SelectedValue;
            string grupo;

            using (SqlConnection conexion = new SqlConnection(connectionStringBDDSistemas))
            {
                conexion.Open();

                //Consulta para obtener el grupo del equipo
                string consultaSQL = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = '" + selectedValue + "'";

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    grupo = (string)comando.ExecuteScalar();
                }
            }


            ddlEquipoTipo.Visible = true;

            VisibilidadControlesEditar();

            if (grupo.Equals("Computador"))
            {
                panelImpresora.Visible = false; // Ocultar panel de impresora
                panelComputador.Visible = true; // Mostrar panel de computadora (ajusta según tu lógica)
            }
            else
            {
                if (grupo.Equals("Impresora"))
                {
                    panelImpresora.Visible = true; // Mostrar panel de impresora
                    panelComputador.Visible = false; // Ocultar panel de computadora

                }
                else
                {
                    panelImpresora.Visible = false; // Mostrar panel de impresora
                    panelComputador.Visible = false; // Ocultar panel de computadora
                }
            }
        }

        protected void btnImagen_Click(object sender, EventArgs e)
        {
            // Set the IdEquipoE property with the actual ID


            // Retrieve repuestos for the selected equipo
            DataTable repuestosData = GetRepuestos(IdEquipoE);

            // Bind data to GridView
            gvRepuestos.DataSource = repuestosData;
            gvRepuestos.DataBind();

            // Show the modal
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "document.getElementById('modalRepuestos').style.display = 'block';", true);
        }

        protected void GridViewRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Obtener el índice de la fila clickeada
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtener el IDRepuesto usando DataKeys
                int idRepuesto = Convert.ToInt32(gvRepuestos.DataKeys[index].Value);

                // Redirigir a la página FrmRepuestos.aspx con el IDRepuesto en la query string
                Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + idRepuesto);
            }
        }



        private DataTable GetRepuestos(string idEquipo)
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT r.[IDRepuesto], r.[NombreRepuesto], r.[Descripcion], r.[Cantidad], r.[Costo], r.[FechaAdquisicion], r.[Proveedor], r.[Observaciones], r.[EnUso], r.[Dañado]
            FROM [IT_RepuestoEquipo] re
            INNER JOIN [IT_Repuestos] r ON re.[IDRepuesto] = r.[IDRepuesto]
            WHERE re.[IDEquipo] = @IdEquipo";

            using (SqlConnection conn = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEquipo", idEquipo);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


    }
}
