using System;
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


        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        //Variable del IDEquipo
        static string idEquipoE;
        static string grupoE;
        static string cedulaE;
        static string codigoActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Recoger datos
            idEquipoE = Request.QueryString["IDEquipo"];
            grupoE = Request.QueryString["Grupo"];

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

                //LLenar tabla Custodio
                verCustodio(ObtenerCustodio(idEquipoE));

                // Llenar los TextBox con los datos del equipo
                LlenarTextBoxsEquipo(idEquipoE);

                // Si es la primera vez que se carga la página, desactivar los botones
                this.btnMostrarCalendario.Visible = false;
                this.btnMostrarCalendarioGarantia.Visible = false;
                this.ddlCustodio.Visible = false;
                this.ddlEstado.Visible = false;
                this.ddlArea.Visible = false;
                this.btnCancelar.Enabled = false;
                this.btnActualizar.Enabled = false;

                //Deshanilitar campos de texto
                DeshabilitarCamposTexto(this.divFmrEquipo);

                //Verificar el grupo del equipo
                if (grupoE.Equals("Computador"))
                {
                    this.panelComputador.Visible = true;
                    DeshabilitarCamposTexto(this.divFmrCpu);
                    LlenarTextBoxsCpu(idEquipoE);
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
                    codigoActivo = ViewState["CodigoActivo"].ToString();
                }
            }
        }

        public void verCustodio(Custodio custodio)
        {
            this.lblCedula.Text = custodio.Cedula;
            cedulaE = custodio.Cedula;
            this.lblNombres.Text = custodio.Nombres;
            this.lblCargo.Text = custodio.Cargo;
        }

        public Custodio ObtenerCustodio(string IDEquipo)
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
                            Custodio custodio = new Custodio();
                            custodio.Cedula = reader["Cedula"].ToString();
                            custodio.Nombres = reader["Nombres"].ToString();
                            custodio.Cargo = reader["Cargo"].ToString();
                            return custodio;
                        }
                    }
                }
            }

            return null; // Si no se encuentra ningún custodio
        }

        protected void ddlCustodio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener los valores seleccionados del DropDownList
            string cedula = ddlCustodio.SelectedValue;
            string nombres = ddlCustodio.SelectedItem.Text; // Suponiendo que el texto mostrado es el nombre del custodio

            // Buscar el cargo del custodio en el DataView
            DataView dv = (DataView)SqlDataSourceCustodios.Select(DataSourceSelectArguments.Empty);
            DataRowView cargoRow = null;
            foreach (DataRowView row in dv)
            {
                if (row["Cedula"].ToString() == cedula)
                {
                    cargoRow = row;
                    break;
                }
            }

            // Verificar si se encontró la fila correspondiente a la cédula
            if (cargoRow != null)
            {
                string cargo = cargoRow["Cargo"].ToString();

                // Asignar los valores a las etiquetas
                lblCedula.Text = cedula;
                lblNombres.Text = nombres;
                lblCargo.Text = cargo;
            }
            else
            {
                // Manejar el caso en que no se encuentre la cédula seleccionada
                lblCedula.Text = "Cédula no encontrada";
                lblNombres.Text = "";
                lblCargo.Text = "";
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del DropDownList y asignarlo al TextBox
            txtEstado.Text = ddlEstado.SelectedValue;
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del DropDownList y asignarlo al TextBox
            this.txtArea.Text = ddlArea.SelectedValue;
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
                            codigoActivo = reader["CodigoActivo"].ToString();
                            ViewState["CodigoActivo"] = codigoActivo;

                            // Asignar los valores de los campos a los TextBox correspondientes
                            this.txtCodigoActivo.Text = reader["CodigoActivo"].ToString();
                            //codigoActivo = reader["CodigoActivo"].ToString();
                            //ClientScript.RegisterStartupScript(this.GetType(), "SetCodigoActivo", "var codigoActivo = '" + txtCodigoActivo.Text + "';", true);
                            this.txtEquipoTipo.Text = reader["EquipoTipo"].ToString();
                            object fechaAdquiValue = reader["FechaAdquisicion"];
                            if (fechaAdquiValue != DBNull.Value)
                            {
                                this.txtFechaAdqui.Text = ((DateTime)fechaAdquiValue).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                this.txtFechaAdqui.Text = string.Empty; // O cualquier valor predeterminado que desees asignar si el campo es nulo
                            }
                            this.txtMarca.Text = reader["Marca"].ToString();
                            this.txtModelo.Text = reader["Modelo"].ToString();
                            this.txtSerie.Text = reader["Serie"].ToString();
                            this.txtUbicacion.Text = reader["Ubicacion"].ToString();
                            this.txtIP.Text = reader["IP"].ToString();
                            this.txtEstado.Text = reader["Estado"].ToString();
                            this.txtObservaciones.Text = reader["Observaciones"].ToString();
                            object garantiaValue = reader["GarantiaHastaCuando"];
                            if (garantiaValue != DBNull.Value)
                            {
                                this.txtGarantia.Text = ((DateTime)garantiaValue).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                this.txtGarantia.Text = string.Empty; // O cualquier valor predeterminado que desees asignar si el campo es nulo
                            }
                            object fechaActValue = reader["FechaActualizacionDatos"];
                            if (fechaActValue != DBNull.Value)
                            {
                                this.txtFechaAtcDatos.Text = ((DateTime)fechaActValue).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                this.txtFechaAtcDatos.Text = string.Empty; // O cualquier valor predeterminado que desees asignar si el campo es nulo
                            }

                            this.txtEmpresa.Text = reader["EmpresaProveedora"].ToString();
                            this.txtNumRJ45.Text = reader["NumeroDePuertosRJ45"].ToString();
                            this.txtNotas.Text = reader["Notas"].ToString();
                            //this.txtIDEquipo.Text = IDEquipo;
                            this.txtContrato.Text = reader["Contrato"].ToString();
                            this.txtArea.Text = reader["Area"].ToString();
                            // Asignar valor al CheckBox
                            if (bool.TryParse(reader["EnUso"].ToString(), out bool enusoValue))
                            {
                                this.chkEnUso.Checked = enusoValue;
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

        private void LlenarTextBoxsCpu(string IDEquipo)
        {
            // Consulta SQL para obtener los datos del equipo
            string sqlQuery = "SELECT [NombreDelEquipo], [Procesador], [ProcesadorVelocidad], [RAM], [NumDiscos], [Almacenamiento], [TipoLectorCD], [Wireless], [MAC] FROM[BDDSistemasPruebas].[dbo].[IT_Computadoras] WHERE IDEquipo = '" + IDEquipo + "'";

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

        protected void btnMostrarCalendario_Click(object sender, EventArgs e)
        {
            if (calendarContainer.Style["display"] == "none")
            {
                // Si el calendario está oculto, mostrarlo
                calendarContainer.Style["display"] = "block";
            }
            else
            {
                // Si el calendario está visible, ocultarlo
                calendarContainer.Style["display"] = "none";
            }
        }
        protected void CalendarFechaAdquisicion_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha en el calendario,
            // asignarla al TextBox y ocultar el panel que contiene el calendario
            txtFechaAdqui.Text = CalendarFechaAdquisicion.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainer.Style["display"] = "none";
        }


        protected void btnMostrarCalendarioGarantia_Click(object sender, EventArgs e)
        {
            if (calendarContainerGarantia.Style["display"] == "none")
            {
                // Si el calendario está oculto, mostrarlo
                calendarContainerGarantia.Style["display"] = "block";
            }
            else
            {
                // Si el calendario está visible, ocultarlo
                calendarContainerGarantia.Style["display"] = "none";
            }
        }
        protected void CalendarGarantia_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha en el calendario,
            // asignarla al TextBox y ocultar el panel que contiene el calendario
            txtGarantia.Text = CalendarFechaAdquisicion.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainerGarantia.Style["display"] = "none";
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
            btnEditar.Enabled = false;
            btnCancelar.Enabled = true;
            btnActualizar.Enabled = true;


            this.btnMostrarCalendario.Visible = true;
            this.btnMostrarCalendarioGarantia.Visible = true;
            this.ddlCustodio.Visible = true;
            this.ddlEstado.Visible = true;
            this.ddlArea.Visible = true;


            // Enlazar el SqlDataSource al DropDownList
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

            // Establecer la fecha actual en el TextBox de la fecha de actualización de datos
            txtFechaAtcDatos.Text = DateTime.Now.ToString("yyyy-MM-dd"); // Cambia el formato según tus preferencias

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
                LlenarTextBoxsCpu(idEquipoE);
            }
            else
            {
                if (grupoE.Equals("Impresora"))
                {
                    HabilitarEdicionCamposTexto(false, this.divFmrImpresora);
                }
            }

            // Desactivar botones "Cancelar" y "Actualizar" y mostrar botón "Editar"
            btnEditar.Enabled = true;
            btnCancelar.Enabled = false;
            btnActualizar.Enabled = false;

            // Ocultar los controles adicionales
            btnMostrarCalendario.Visible = false;
            btnMostrarCalendarioGarantia.Visible = false;
            ddlCustodio.Visible = false;
            ddlEstado.Visible = false;
            ddlArea.Visible = false;

            LlenarTextBoxsEquipo(idEquipoE);
            //LLenar tabla Custodio
            verCustodio(ObtenerCustodio(idEquipoE));
        }

        public void actualizarEquipoCustodio()
        {
            // Obtener los valores actualizados de los campos de texto
            string codigoActivo = this.txtCodigoActivo.Text;
            string equipoTipo = txtEquipoTipo.Text;
            string cedulaCustodio = lblCedula.Text;
            string fechaAdquisicion = string.IsNullOrEmpty(txtFechaAdqui.Text) ? null : txtFechaAdqui.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            string ubicacion = txtUbicacion.Text;
            string ip = txtIP.Text;
            string estado = txtEstado.Text;
            string observaciones = txtObservaciones.Text;
            string garantia = string.IsNullOrEmpty(txtGarantia.Text) ? null : txtGarantia.Text;
            string fechaActualizacionDatos = txtFechaAtcDatos.Text;
            string empresaProveedora = txtEmpresa.Text;
            string numRJ45 = txtNumRJ45.Text;
            string notas = txtNotas.Text;
            string contrato = txtContrato.Text;
            string area = txtArea.Text;
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
                           "[FechaActualizacionDatos] = @FechaActualizacionDatos, " +
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
                command.Parameters.AddWithValue("@FechaActualizacionDatos", string.IsNullOrEmpty(fechaActualizacionDatos) ? (object)DBNull.Value : fechaActualizacionDatos);
                command.Parameters.AddWithValue("@EmpresaProveedora", string.IsNullOrEmpty(empresaProveedora) ? (object)DBNull.Value : empresaProveedora);
                command.Parameters.AddWithValue("@NumeroDePuertosRJ45", string.IsNullOrEmpty(numRJ45) ? (object)DBNull.Value : numRJ45);
                command.Parameters.AddWithValue("@Notas", string.IsNullOrEmpty(notas) ? (object)DBNull.Value : notas);
                command.Parameters.AddWithValue("@Contrato", string.IsNullOrEmpty(contrato) ? (object)DBNull.Value : contrato);
                command.Parameters.AddWithValue("@Area", string.IsNullOrEmpty(area) ? (object)DBNull.Value : area);
                command.Parameters.AddWithValue("@EnUso", enuso);
                command.Parameters.AddWithValue("@IDEquipo", idEquipoE);

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
            string IDEquipo = idEquipoE; // Debes obtener el ID del equipo de alguna manera, ya sea desde un TextBox u otro control en tu formulario

            // Recuperar los datos de los campos de texto
            string nombreEquipo = txtNombreEquipo.Text;
            string procesador = txtProcesador.Text;
            string procesadorVelocidad = txtProcesadorVelo.Text;
            string RAM = txtRam.Text;
            string numDiscos = txtNumDiscos.Text;
            string almacenamiento = txtAlmacenamiento.Text;
            string tipoLectorCD = txtTipoLector.Text;
            string MAC = txtMac.Text;
            bool wireless = cbWireless.Checked;

            // Consulta SQL para actualizar los datos del equipo
            string sqlQuery = "UPDATE [BDDSistemasPruebas].[dbo].[IT_Computadoras] SET [NombreDelEquipo] = @NombreEquipo, [Procesador] = @Procesador, [ProcesadorVelocidad] = @ProcesadorVelocidad, [RAM] = @RAM, [NumDiscos] = @NumDiscos, [Almacenamiento] = @Almacenamiento, [TipoLectorCD] = @TipoLectorCD, [MAC] = @MAC, [Wireless] = @Wireless WHERE IDEquipo = @IDEquipo";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Asignar valores a los parámetros
                    command.Parameters.AddWithValue("@IDEquipo", IDEquipo);
                    command.Parameters.AddWithValue("@NombreEquipo", nombreEquipo);
                    command.Parameters.AddWithValue("@Procesador", procesador);
                    command.Parameters.AddWithValue("@ProcesadorVelocidad", procesadorVelocidad);
                    command.Parameters.AddWithValue("@RAM", RAM);
                    command.Parameters.AddWithValue("@NumDiscos", numDiscos);
                    command.Parameters.AddWithValue("@Almacenamiento", almacenamiento);
                    command.Parameters.AddWithValue("@TipoLectorCD", tipoLectorCD);
                    command.Parameters.AddWithValue("@MAC", MAC);
                    command.Parameters.AddWithValue("@Wireless", wireless);

                    // Abrir conexión
                    connection.Open();

                    // Ejecutar la consulta de actualización
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si se realizaron actualizaciones
                    if (rowsAffected > 0)
                    {

                        // La actualización se realizó con éxito
                        // Puedes manejar este caso de acuerdo a tu lógica específica
                    }
                    else
                    {
                        // No se realizaron actualizaciones (puede que el IDEquipo no exista en la base de datos)
                        // Puedes manejar este caso de acuerdo a tu lógica específica
                    }
                }
            }

        }




        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarEquipoCustodio();

            // Ocultar los botones de cancelar y actualizar y mostrar el botón de editar
            btnEditar.Enabled = true;
            btnCancelar.Enabled = false;
            btnActualizar.Enabled = false;

            // Ocultar los controles adicionales
            btnMostrarCalendario.Visible = false;
            btnMostrarCalendarioGarantia.Visible = false;
            ddlCustodio.Visible = false;
            ddlEstado.Visible = false;
            ddlArea.Visible = false;

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

            // Llenar los TextBox con los datos del equipo
            LlenarTextBoxsEquipo(idEquipoE);
            //LLenar tabla Custodio
            verCustodio(ObtenerCustodio(idEquipoE));
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
            string consultaSQL = "SELECT * FROM [dbo].[IT_Software] WHERE IDEquipo = '" + idEquipoE + "'";

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
            string consultaSQL = "SELECT IDToner, TipoToner, Stock FROM IT_TONERS WHERE IDEquipo = '" + idEquipoE + "'";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                SqlCommand command = new SqlCommand(consultaSQL, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Limpiamos cualquier fila existente en la tabla
                //tablaToner.Rows.Clear();

                // Iteramos sobre los datos y creamos filas y celdas para cada elemento
                while (reader.Read())
                {
                    HtmlTableRow row = new HtmlTableRow();

                    HtmlTableCell idTonerCell = new HtmlTableCell();
                    idTonerCell.InnerText = reader["IDToner"].ToString();
                    row.Cells.Add(idTonerCell);

                    HtmlTableCell tipoTonerCell = new HtmlTableCell();
                    tipoTonerCell.InnerText = reader["TipoToner"].ToString();
                    row.Cells.Add(tipoTonerCell);

                    HtmlTableCell stockCell = new HtmlTableCell();
                    stockCell.InnerText = reader["Stock"].ToString();
                    row.Cells.Add(stockCell);

                    tablaToner.Rows.Add(row);
                }

                reader.Close();
            }
        }







        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmPrincipal.aspx");
        }


        protected void btnCustodiosAnt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "$('#miModal').modal('show');", true);
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
            Response.Redirect("FrmEquipos.aspx?IDEquipo=" + fila.Cells[4].Text + "&CedulaCustodio=" + cedulaE + "&Grupo=" + grupoG);

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
            ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "redirectToFrmYupak('" + codigoActivo + "');", true);
        }

        protected void btnVerMas_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalVerMas", "$('#modalVerMas').modal('show');", true);
        }

        protected void btnAñadirSoft_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAddSoft", "$('#modalAddSoft').modal('show');", true);
        }
        protected void btnAddSoft_Click(object sender, EventArgs e)
        {
            // Recoger los valores de los controles
            string idequipo = idEquipoE; // Este valor lo obtienes desde el servidor según tu lógica
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
            string url = "FrmVisorImagenes.aspx?CodigoActivo=" + codigoActivo;

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
            string url = "FrmYupak.aspx?CodigoActivo=" + codigoActivo;

            string script = "var screenWidth = window.screen.availWidth;" +
                            "var screenHeight = window.screen.availHeight;" +
                            "window.open('" + url + "', '_blank', 'width=' + (screenWidth / 2) + ',height=' + screenHeight + ',top=0,left=' + (screenWidth / 2));";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "btnYupak_Click", script, true);
        }
    }
}
