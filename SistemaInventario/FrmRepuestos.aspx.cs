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
    public partial class FrmRepuestos : Page
    {

        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        static int IDRepuesto;

        public static string idRepuestoS;

        public static string NombreRepuesto { get; private set; }
        public static string Cantidad { get; private set; }
        public static string Costo { get; private set; }
        public static string FechaAdquisicion { get; private set; }
        public static string Proveedor { get; private set; }
        public static string Observaciones { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si hay un parámetro en la URL
                string idRepuesto = Request.QueryString["IDRepuesto"];

                idRepuestoS = idRepuesto;

                if (string.IsNullOrEmpty(idRepuesto) || idRepuesto == "0")
                {

                    Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
                    if (literalPageTitle != null)
                    {
                        literalPageTitle.Text = "NUEVO REPUESTO"; // Asigna el título visible en el <div>
                    }

                    LabelIDRepuesto.Text = "Detalles del nuevo repuesto";

                    // Aquí puedes decidir si ocultar los controles relacionados con IDRepuesto o mostrar un mensaje alternativo.
                    btnAñadirEquipo.Visible = false;
                    btnCrearRepuesto.Visible = true;
                    btnEditar.Visible = false;
                    btnCancelar.Visible = false;
                    btnActualizar.Visible = false;
                    btnMostrarCalendario.Visible = true;
                    btnEliminar.Visible = false;
                    btnLogs.Visible = false;
                    btnNuevo.Visible = false;

                    CalendarFechaAdquisicion.Visible = true;

                    ListaEquipos.Visible = false;

                    lblIDRepuesto.Visible = false;
                    txtIDRepuesto.Visible = false;

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                else
                {
                    Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
                    if (literalPageTitle != null)
                    {
                        literalPageTitle.Text = "DETALLES REPUESTO"; // Asigna el título visible en el <div>
                    }


                    // Convertir a int solo si el idRepuesto no está vacío y no es 0
                    IDRepuesto = Convert.ToInt32(idRepuesto);

                    btnAñadirEquipo.Visible = true;
                    btnNuevo.Visible = true;
                    LabelIDRepuesto.Visible = false;
                    btnEliminar.Visible = true;
                    btnRetroceder.Visible = false;
                    // Configurar el SqlDataSource para usar el IDRepuesto de la QueryString
                    SqlDataSource1.SelectParameters["IDRepuesto"].DefaultValue = idRepuesto;

                    // Asignar el valor del IDRepuesto a SqlDataSource
                    SqlDataSourceEquipos.SelectParameters["IDRepuesto"].DefaultValue = idRepuesto;

                    // Asigna el ID a la etiqueta
                    LabelIDRepuesto.Text = "ID del Repuesto: " + idRepuesto;

                    // Llama a la función para cargar los detalles del repuesto
                    CargarDetallesRepuesto(idRepuesto);

                    // Asigna los valores de los controles a las variables estáticas
                    NombreRepuesto = txtNombreRepuesto.Text;
                    Cantidad = txtCantidad.Text;
                    Costo = txtCosto.Text;
                    FechaAdquisicion = txtFechaAdquisicion.Text;
                    Proveedor = txtProveedor.Text;
                    Observaciones = txtObservaciones.Text;

                    DeshabilitarCamposTexto(divFrmRepuesto);

                    // Datos de enlace
                    GridView1.DataSourceID = SqlDataSource1.ID;
                    GridView1.DataBind(); // Cargar los datos en el GridView

                    btnEditar.Enabled = true;
                    btnCancelar.Visible = false;
                    btnActualizar.Visible = false;
                    btnMostrarCalendario.Visible = false;
                    btnLogs.Visible = true;



                }
            }
        }



        private void CargarDetallesRepuesto(string idRepuesto)
        {
            string query = "SELECT [IDRepuesto], [NombreRepuesto], [Descripcion], [Cantidad], [Costo], [FechaAdquisicion], [Proveedor], [Observaciones], [EnUso], [Dañado] " +
                           "FROM [IT_Repuestos] " +
                           "WHERE [IDRepuesto] = @IDRepuesto";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetro para la consulta
                    command.Parameters.AddWithValue("@IDRepuesto", idRepuesto);

                    // Crear un DataAdapter para llenar un DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    try
                    {
                        connection.Open();
                        adapter.Fill(dataTable);

                        // Verificar si hay resultados
                        if (dataTable.Rows.Count > 0)
                        {
                            // Asignar los valores a los controles de texto
                            txtIDRepuesto.Text = dataTable.Rows[0]["IDRepuesto"].ToString();
                            txtNombreRepuesto.Text = dataTable.Rows[0]["NombreRepuesto"].ToString();
                            txtDescripcion.Text = dataTable.Rows[0]["Descripcion"].ToString();
                            txtCantidad.Text = dataTable.Rows[0]["Cantidad"].ToString();
                            txtCosto.Text = dataTable.Rows[0]["Costo"].ToString();
                            txtFechaAdquisicion.Text = Convert.ToDateTime(dataTable.Rows[0]["FechaAdquisicion"]).ToString("dd/MM/yyyy");
                            txtProveedor.Text = dataTable.Rows[0]["Proveedor"].ToString();
                            txtObservaciones.Text = dataTable.Rows[0]["Observaciones"].ToString();
                            chkEnUso.Checked = Convert.ToBoolean(dataTable.Rows[0]["EnUso"]);
                            chkDañado.Checked = Convert.ToBoolean(dataTable.Rows[0]["Dañado"]);

                        }
                        else
                        {
                            LabelIDRepuesto.Text = "No se encontraron detalles para el ID de repuesto proporcionado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        LabelIDRepuesto.Text = "Error al cargar los detalles: " + ex.Message;
                    }
                }
            }
        }

        protected void btnLogs_Click(object sender, EventArgs e)
        {
            // Asignar el valor de la variable estática a un parámetro de consulta
            //string codigoActivo = ;  Reemplaza con el valor que desees
            string url = $"FrmReportes.aspx?codigoActivo=IDRepuesto: {IDRepuesto}";

            // Redirige a la página de reportes con el parámetro de búsqueda
            Response.Redirect(url);
        }
        private void DeshabilitarCamposTexto(Control control)
        {
            if (control != null)
            {
                // Iterar a través de los controles dentro del control padre
                foreach (Control childControl in control.Controls)
                {
                    if (childControl is TextBox)
                    {
                        // Deshabilitar el control visualmente
                        ((TextBox)childControl).Enabled = false;
                    }
                    // Verificar si el control es un CheckBox
                    else if (childControl is CheckBox)
                    {
                        // Deshabilitar el control visualmente
                        ((CheckBox)childControl).Enabled = false;
                    }
                    // Llamar recursivamente si el control tiene otros controles dentro
                    if (childControl.HasControls())
                    {
                        DeshabilitarCamposTexto(childControl);
                    }
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarCamposTexto(divFrmRepuesto);

            btnEditar.Visible = false;
            btnCancelar.Visible = true;
            btnActualizar.Visible = true;
            btnLogs.Visible = false;

            CalendarFechaAdquisicion.Visible = true;
            btnMostrarCalendario.Visible = true;
        }


        private void HabilitarCamposTexto(Control control)
        {
            if (control != null)
            {
                // Iterar a través de los controles dentro del control padre
                foreach (Control childControl in control.Controls)
                {
                    if (childControl is TextBox)
                    {
                        // Habilitar el control visualmente
                        ((TextBox)childControl).Enabled = true;
                    }
                    // Verificar si el control es un CheckBox
                    else if (childControl is CheckBox)
                    {
                        // Habilitar el control visualmente
                        ((CheckBox)childControl).Enabled = true;
                    }
                    // Llamar recursivamente si el control tiene otros controles dentro
                    if (childControl.HasControls())
                    {
                        HabilitarCamposTexto(childControl);
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnAñadirEquipo.Visible = true;
            btnCrearRepuesto.Visible = false;
            btnEditar.Visible = true;
            btnEditar.Enabled = true;
            btnCancelar.Visible = false;
            btnActualizar.Visible = false;
            btnMostrarCalendario.Visible = true;
            btnEliminar.Visible = true;
            btnLogs.Visible = true;

            ListaEquipos.Visible = true;

            CalendarFechaAdquisicion.Visible = false;
            btnMostrarCalendario.Visible = false;

            lblIDRepuesto.Visible = false;
            txtIDRepuesto.Visible = false;

            DeshabilitarCamposTexto(divFrmRepuesto);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            // Obtén la ID del repuesto del campo de texto
            int idRepuesto = int.Parse(txtIDRepuesto.Text);

            // Obtén los valores de los campos de texto
            string nombreRepuesto = txtNombreRepuesto.Text;
            string descripcion = txtDescripcion.Text;
            int cantidad = int.Parse(txtCantidad.Text);
            decimal costo = decimal.Parse(txtCosto.Text);
            DateTime fechaAdquisicion = DateTime.Parse(txtFechaAdquisicion.Text);
            string proveedor = txtProveedor.Text;
            string observaciones = txtObservaciones.Text;
            // Obtén los valores de los campos EnUso y Dañado
            bool enUso = chkEnUso.Checked;
            bool dañado = chkDañado.Checked;

            // Crear la consulta de actualización
            string query = "UPDATE [IT_Repuestos] SET " +
                           "[NombreRepuesto] = @NombreRepuesto, " +
                           "[Descripcion] = @Descripcion, " +
                           "[Cantidad] = @Cantidad, " +
                           "[Costo] = @Costo, " +
                           "[FechaAdquisicion] = @FechaAdquisicion, " +
                           "[Proveedor] = @Proveedor, " +
                           "[Observaciones] = @Observaciones, " +
                           "[EnUso] = @EnUso, " +
                           "[Dañado] = @Dañado " +
                           "WHERE [IDRepuesto] = @IDRepuesto";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros
                    command.Parameters.AddWithValue("@NombreRepuesto", nombreRepuesto);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Cantidad", cantidad);
                    command.Parameters.AddWithValue("@Costo", costo);
                    command.Parameters.AddWithValue("@FechaAdquisicion", fechaAdquisicion);
                    command.Parameters.AddWithValue("@Proveedor", proveedor);
                    command.Parameters.AddWithValue("@Observaciones", observaciones);
                    command.Parameters.AddWithValue("@EnUso", enUso);
                    command.Parameters.AddWithValue("@Dañado", dañado);
                    command.Parameters.AddWithValue("@IDRepuesto", idRepuesto);

                    // Abrir conexión y ejecutar la actualización
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    //LOG
                    // Crear una instancia de Logger
                    Logger logger = new Logger();
                    string usuario = Session["Usuario"] as string;
                    string detalle = "Actualizó el registro del repuesto que tenía los siguientes parámetros: " +
            "IDRepuesto: " + IDRepuesto + "  Nombre Repuesto: " + NombreRepuesto +
            "  Cantidad: " + Cantidad + "  Costo: " + Costo +
            "  FechaAdquisicion: " + FechaAdquisicion + "  Proveedor: " + Proveedor;

                    // Registrar la acción en el log
                    logger.RegistrarLog(usuario, "Actualizar", "IT_Repuestos", detalle, idRepuestoS);

                    if (rowsAffected > 0)
                    {
                        // Mostrar mensaje de éxito
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Repuesto actualizado con éxito.');", true);

                    }
                    else
                    {
                        // Mostrar mensaje de error
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No se encontró el repuesto para actualizar.');", true);
                    }
                    Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + idRepuesto);
                }
            }
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                // Cambia al SqlDataSource de búsqueda
                GridView1.DataSourceID = "SqlDataSourceSearch";
                SqlDataSourceSearch.SelectParameters["Buscar"].DefaultValue = TextBox1.Text.Trim();
            }
            else
            {
                // Cambia al SqlDataSource predeterminado si el TextBox está vacío
                GridView1.DataSourceID = "SqlDataSource1";
            }

            // Vuelve a enlazar la GridView para reflejar los cambios
            GridView1.DataBind();
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
            txtFechaAdquisicion.Text = CalendarFechaAdquisicion.SelectedDate.ToString("yyyy-MM-dd");

            calendarContainer.Style["display"] = "none";
        }

        protected void btnCrearRepuesto_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos del modal
            string nombreRepuesto = txtNombreRepuesto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            int cantidad;
            decimal costo;
            DateTime fechaAdquisicion;
            bool cantidadValida = int.TryParse(txtCantidad.Text.Trim(), out cantidad);
            bool costoValido = decimal.TryParse(txtCosto.Text.Trim(), out costo);
            bool fechaValida = DateTime.TryParse(txtFechaAdquisicion.Text.Trim(), out fechaAdquisicion);

            if (!cantidadValida || !costoValido || !fechaValida)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cantidad, Costo o Fecha de Adquisición inválidos.');", true);
                return;
            }

            string proveedor = txtProveedor.Text.Trim();
            string observaciones = txtObservaciones.Text.Trim();

            // Consulta para insertar un nuevo repuesto en la base de datos y recuperar el nuevo ID
            string query = "INSERT INTO [IT_Repuestos] (NombreRepuesto, Descripcion, Cantidad, Costo, FechaAdquisicion, Proveedor, Observaciones) " +
                           "OUTPUT INSERTED.IDRepuesto " +
                           "VALUES (@NombreRepuesto, @Descripcion, @Cantidad, @Costo, @FechaAdquisicion, @Proveedor, @Observaciones)";

            int nuevoIDRepuesto = 0;

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas)) // Cambia la cadena de conexión aquí
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Asignar valores a los parámetros
                command.Parameters.AddWithValue("@NombreRepuesto", nombreRepuesto);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@Cantidad", cantidad);
                command.Parameters.AddWithValue("@Costo", costo);
                command.Parameters.AddWithValue("@FechaAdquisicion", fechaAdquisicion);
                command.Parameters.AddWithValue("@Proveedor", proveedor);
                command.Parameters.AddWithValue("@Observaciones", observaciones);

                try
                {
                    connection.Open();
                    nuevoIDRepuesto = (int)command.ExecuteScalar(); // Recuperar el nuevo ID del repuesto
                                                                    // Mostrar mensaje de éxito
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Repuesto agregado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
                }
                finally
                {
                    connection.Close();
                    // Cerrar el modal después de guardar
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalClose", "$('#modalAgregarRepuesto').modal('hide');", true);
                }
            }

            // Redirigir a la página con el nuevo IDRepuesto
            if (nuevoIDRepuesto > 0)
            {
                //LOG
                // Crear una instancia de Logger
                Logger logger = new Logger();
                string usuario = Session["Usuario"] as string;
                string detalle = "Creo el repuesto que tiene los siguientes parámetros: " +
        " ID Repuesto: " + nuevoIDRepuesto + " Nombre Repuesto : " + nombreRepuesto + " Cantidad: " + cantidad +
        "  Costo: " + costo + "  FechaAdquisicion: " + fechaAdquisicion +
        "  Proveedor: " + proveedor;

                // Registrar la acción en el log
                logger.RegistrarLog(usuario, "Crear", "IT_Repuestos", detalle, idRepuestoS);

                Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + nuevoIDRepuesto);
            }
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el IDRepuesto de la QueryString
            string idRepuestoStr = Request.QueryString["IDRepuesto"];
            if (!int.TryParse(idRepuestoStr, out int idRepuesto))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('ID de Repuesto no válido.');", true);
                return;
            }

            // Establecer la cadena de conexión
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                connection.Open();

                // Iniciar una transacción para asegurar la integridad de las operaciones
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Eliminar todas las relaciones en la tabla IT_RepuestoEquipo
                        string deleteRelationsQuery = "DELETE FROM [IT_RepuestoEquipo] WHERE IDRepuesto = @IDRepuesto";
                        using (SqlCommand command = new SqlCommand(deleteRelationsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@IDRepuesto", idRepuesto);
                            int rowsAffected = command.ExecuteNonQuery();
                            // Mensaje de éxito para la eliminación de relaciones
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Relaciones eliminadas correctamente.');", true);
                        }

                        // Eliminar el repuesto de la tabla IT_Repuestos
                        string deleteRepuestoQuery = "DELETE FROM [IT_Repuestos] WHERE IDRepuesto = @IDRepuesto";
                        using (SqlCommand command = new SqlCommand(deleteRepuestoQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@IDRepuesto", idRepuesto);
                            int rowsAffected = command.ExecuteNonQuery();
                            // Mensaje de éxito para la eliminación del repuesto
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Repuesto eliminado correctamente.');", true);
                        }

                        // Confirmar la transacción
                        transaction.Commit();

                        //LOG
                        // Crear una instancia de Logger
                        Logger logger = new Logger();
                        string usuario = Session["Usuario"] as string;
                        string detalle = "Elimino el repuesto que tenía los siguientes parámetros: " +
                " ID Repuesto: " + IDRepuesto + " Nombre Repuesto: " + NombreRepuesto + " Cantidad: " + Cantidad +
                "  Costo: " + Costo + "  FechaAdquisicion: " + FechaAdquisicion +
                "  Proveedor: " + Proveedor;

                        // Registrar la acción en el log
                        logger.RegistrarLog(usuario, "Eliminar", "IT_Repuestos", detalle, idRepuestoS);


                    }
                    catch (Exception ex)
                    {
                        // Deshacer la transacción en caso de error
                        transaction.Rollback();
                        // Mostrar mensaje de error
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
                    }
                }
            }

            // Opcional: Redirigir a otra página o realizar otras acciones después de eliminar el repuesto
            Response.Redirect("FrmGridRepuestos.aspx");
        }


        // FUNCIONES DEL MODAL

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchQuery = txtBuscar.Text.Trim();

            // Actualizar el SelectCommand del SqlDataSource con las columnas correctas
            SqlDataSourceEquipos.SelectCommand = "SELECT [IDEquipo], [EquipoTipo], [Marca], [Modelo], [Serie] " +
                                                  "FROM [IT_Equipos] " +
                                                  "WHERE LOWER([EquipoTipo]) LIKE '%' + LOWER(@Search) + '%' OR " +
                                                  "LOWER([Marca]) LIKE '%' + LOWER(@Search) + '%' OR " +
                                                  "LOWER([Modelo]) LIKE '%' + LOWER(@Search) + '%' OR " +
                                                  "LOWER([Serie]) LIKE '%' + LOWER(@Search) + '%'";


            SqlDataSourceEquipos.SelectParameters.Clear();
            SqlDataSourceEquipos.SelectParameters.Add("Search", searchQuery);

            // Rebind the GridView
            GridViewEquipos.DataBind();
        }

        protected void GridViewEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el IDEquipo usando DataKeys
            int idEquipo = Convert.ToInt32(GridViewEquipos.DataKeys[GridViewEquipos.SelectedIndex].Value);

            // Verificar que IDRepuesto tenga un valor válido
            if (IDRepuesto > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();
                    string query = "INSERT INTO IT_RepuestoEquipo (IDRepuesto, IDEquipo) VALUES (@IDRepuesto, @IDEquipo)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDRepuesto", IDRepuesto);
                        command.Parameters.AddWithValue("@IDEquipo", idEquipo);

                        try
                        {
                            command.ExecuteNonQuery();

                            //LOG
                            // Crear una instancia de Logger
                            Logger logger = new Logger();
                            string usuario = Session["Usuario"] as string;
                            string detalle = "Relaciono el repuesto de ID:  " +
                     IDRepuesto + "  con el equipo de ID: " + idEquipo;

                            // Registrar la acción en el log
                            logger.RegistrarLog(usuario, "Actualizar", "IT_Repuestos", detalle, idRepuestoS);


                            // Mensaje opcional de éxito
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Inserción exitosa');", true);
                        }
                        catch (Exception ex)
                        {
                            // Manejar el error
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al insertar: {ex.Message}');", true);
                        }
                    }
                }

                // Opcional: volver a enlazar los datos si es necesario
                GridViewEquipos.DataBind();
            }
            else
            {
                // Manejar el caso donde IDRepuesto no es válido
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('IDRepuesto no es válido.');", true);
            }
        }

        protected void GridViewEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Verificar que la fila sea una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Buscar el botón de selección en la fila actual
                Button selectButton = (Button)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]; // El botón de selección está en la última celda
                if (selectButton != null)
                {
                    // Agregar la clase CSS al botón de selección
                    selectButton.CssClass += " btnEquipo"; // Asegúrate de que 'btnEquipo' esté definido en tu CSS
                }
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                // Obtén el ID del equipo desde el CommandArgument
                int idEquipo = Convert.ToInt32(e.CommandArgument);

                // Obtén la fila que contiene el LinkButton
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                // Obtén el equipo tipo (que está en la columna "EquipoTipo")
                string equipoTipo = row.Cells[0].Text;  // Ajusta el índice según la columna que contiene el "EquipoTipo"

                // Inicializa el grupo como una cadena vacía
                string grupo = string.Empty;

                // Consulta la base de datos para obtener el grupo basado en el equipo tipo
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    string query = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = @EquipoTipo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EquipoTipo", equipoTipo);

                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                grupo = result.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Maneja la excepción según sea necesario
                            // Ejemplo: muestra un mensaje de error o escribe en el log
                            Console.WriteLine("Error al obtener el grupo: " + ex.Message);
                        }
                    }
                }

                // Redirige a la página con los parámetros necesarios
                Response.Redirect("FrmEquipo.aspx?IDEquipo=" + idEquipo + "&Grupo=" + Server.UrlEncode(grupo));
            }
            else if (e.CommandName == "Eliminar")
            {
                // Maneja la lógica de eliminación
                int idEquipo = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();
                    string query = "DELETE FROM [IT_RepuestoEquipo] WHERE IDEquipo = @IDEquipo"; // Asegúrate de usar la tabla correcta
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDEquipo", idEquipo);
                        try
                        {
                            command.ExecuteNonQuery();
                            //LOG
                            // Crear una instancia de Logger
                            Logger logger = new Logger();
                            string usuario = Session["Usuario"] as string;
                            string detalle = "Elimino la relación del repuesto de ID:  " +
                     IDRepuesto + "  con el equipo de ID: " + idEquipo;

                            // Registrar la acción en el log
                            logger.RegistrarLog(usuario, "Eliminar", "IT_Repuestos", detalle, idRepuestoS);

                            // Opcional: Mensaje de éxito
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Relación entre el repuesto y el equipo eliminado exitosamente.');", true);
                        }
                        catch (Exception ex)
                        {
                            // Manejar el error
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al eliminar la relación entre el repuesto y el equipo: {ex.Message}');", true);
                        }
                    }
                }

                // Re-bind the GridView to reflect changes
                GridView1.DataBind();
            }

        }






        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + "0");
        }



    }
}
