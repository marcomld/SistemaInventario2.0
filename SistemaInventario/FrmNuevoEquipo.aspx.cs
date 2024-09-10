using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace SistemaInventario
{
    public partial class FrmNuevoEquipo : System.Web.UI.Page
    {

        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        //Variable del IDEquipo
        static string idEquipoE;
        static string grupoE;
        static string cedulaE;
        static string codigoActivo;


      
    
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Nuevo Equipo"; // Asigna el título visible en el <div>
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Forzar la actualización del DataSource para reflejar la nueva búsqueda
            GridView2.DataBind();

            // Verificar si se encontraron resultados
            if (GridView2.Rows.Count > 0)
            {
                // Mostrar el GridView y ocultar la tabla de detalles
                GridView2.Visible = true;
                divCustodio.Visible = false;
            }
            else
            {
                // No se encontraron resultados, ocultar el GridView y la tabla de detalles
                GridView2.Visible = false;
                divCustodio.Visible = false;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el índice de la fila seleccionada
            int index = GridView2.SelectedIndex;

            // Verificar si se ha seleccionado alguna fila
            if (index >= 0 && GridView2.DataKeys != null)
            {
                // Obtener los datos de la fila seleccionada usando las claves de datos
                string cedula = GridView2.DataKeys[index]["Cedula"].ToString();
                string nombres = GridView2.SelectedRow.Cells[2].Text; // O usar DataKeys si se prefiere
                string cargo = GridView2.SelectedRow.Cells[3].Text;   // O usar DataKeys si se prefiere

                // Actualizar los labels en la tabla de custodio
                lblCedula.Text = cedula;
                lblNombres.Text = nombres;
                lblCargo.Text = cargo;

                // Mostrar la tabla de detalles
                divCustodio.Visible = true;

                // Ocultar el GridView después de seleccionar
                GridView2.Visible = false;
            }
        }



        protected void CalendarFechaAdquisicion_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha en el calendario,
            // asignarla al TextBox y ocultar el panel que contiene el calendario
            txtFechaAdqui.Text = CalendarFechaAdquisicion.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainer.Style["display"] = "none";
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
            txtGarantia.Text = CalendarGarantia.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainerGarantia.Style["display"] = "none";
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

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
                        command.Parameters.AddWithValue("@IDEquipo", idEquipoE);

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
            InsertarEquiposToners(idEquipoE, idToner);
            this.GridViewTonersEquipo.DataBind();
            this.lblTotalToners.Text = GridViewTonersEquipo.Rows.Count.ToString();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownList3.SelectedValue;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAñadirToner", "$('#modalAñadirToner').modal('show');", true);
        }



        protected void btnCrearEquipo_Click(object sender, EventArgs e)
        {
            // Obtener los valores actualizados de los campos de texto
            string codigoActivo = this.txtCodigoActivo.Text;
            string equipoTipo = DropDownList3.SelectedItem.Text;
            string cedulaCustodio = lblCedula.Text;
            string fechaAdquisicion = string.IsNullOrEmpty(txtFechaAdqui.Text) ? null : txtFechaAdqui.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            string ubicacion = txtUbicacion.Text;
            string ip = txtIP.Text;
            string estado = DropDownList1.SelectedItem.Text;
            string observaciones = txtObservaciones.Text;
            string garantia = string.IsNullOrEmpty(txtGarantia.Text) ? null : txtGarantia.Text;
            string empresaProveedora = txtEmpresa.Text;
            string numRJ45 = txtNumRJ45.Text;
            string notas = txtNotas.Text;
            string contrato = txtContrato.Text;
            string area = DropDownList2.SelectedItem.Text;
            bool enuso = chkEnUso.Checked;

 
            // Ejecutar la consulta de inserción
            string query = "INSERT INTO [BDDSistemasPruebas].[dbo].[IT_Equipos] (" +
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
                           "[EnUso]) " + // Asegúrate de que haya un cierre de paréntesis
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
                           "@EnUso)" + // Asegúrate de que haya un cierre de paréntesis
                           "SELECT SCOPE_IDENTITY();"; // Obtener el ID del nuevo registro
            

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();
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

                    // Obtener el IDEquipo
                    int idequipo = Convert.ToInt32(command.ExecuteScalar());


                    // Ahora insertar en IT_Computadoras
                    string queryInsertComputadora = "INSERT INTO [BDDSistemasPruebas].[dbo].[IT_Computadoras] (" +
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

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Muestra un mensaje de éxito después de insertar los datos
                        string successMessage = "Los datos han sido ingresados correctamente.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);
                    }
                    else
                    {
                        // Muestra un mensaje de advertencia si no se insertó nada
                        string warningMessage = "No se insertó ningún dato. Verifique que los datos sean válidos.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{warningMessage}');", true);
                    }
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



    }
}