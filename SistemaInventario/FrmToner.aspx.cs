using System;
using System.Text;
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
    public partial class FrmToners : System.Web.UI.Page
    {

        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        static int  IDToner;

        static string idToner;

        // Variables estáticas para almacenar los valores
        public static string TipoToner { get; private set; }
        public static string CPC { get; private set; }
        public static string Repuesto { get; private set; }
        public static string Stock { get; private set; }
        public static string AComprar { get; private set; }
        public static string ValorUnitario { get; private set; }
        public static string CodSuministro { get; private set; }
        public static string Bodega { get; private set; }
        public static string CodigoYupak { get; private set; }
        public static string Notas { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Detalles Toner"; // Asigna el título visible en el <div>
            }

            IDToner = int.TryParse(Request.QueryString["IDToner"], out var tempIDToner) ? tempIDToner : 0;
            idToner = (Request.QueryString["IDToner"]);
            lblTotalEquipos.Text = GridViewEquiposToner.Rows.Count.ToString();

            if (!IsPostBack)
            {
                // Obtener el código único de la sesión
                string codigoUnico = Session["CodigoUnico"] as string;
                if (string.IsNullOrEmpty(codigoUnico))
                {
                    // Redirige a la página de error si no hay un código único en la sesión
                    Response.Redirect("~/FrmError.aspx");
                    return;
                }

                // Código válido, continúa con la lógica de la página
                LlenarTextBoxsToner(IDToner);

                // Deshabilitar campos de texto y configurar visibilidad de botones
                DeshabilitarCamposTexto(divFmrToner);
                ddlTipoToner.Visible = false;
                btnCancelar.Visible = false;
                btnActualizar.Visible = false;
                btnLogs.Visible = true;
                ddlBodegaOptions.Visible = false;

                // Extraer y almacenar valores en variables estáticas
                TipoToner = txtTipoToner.Text.Trim();
                CPC = txtCPC.Text.Trim();
                Repuesto = chkRepuesto.Checked.ToString();

                Stock = int.TryParse(txtStock.Text.Trim(), out int stockValue) ? stockValue.ToString() : "0";
                AComprar = int.TryParse(txtAComprar.Text.Trim(), out int aComprarValue) ? aComprarValue.ToString() : "0";
                ValorUnitario = decimal.TryParse(txtValorUnitario.Text.Trim(), out decimal valorUnitarioValue) ? valorUnitarioValue.ToString("F2") : "0.00";

                CodSuministro = txtCodSuministro.Text.Trim();
                Bodega = txtBodega.Text.Trim();
                CodigoYupak = txtCodigoYupak.Text.Trim();
                Notas = txtNotas.Text.Trim();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Solo llamar a SelectDropdownItem después de que el DropDownList haya sido cargado
            if (IsPostBack)
            {
                SelectDropdownItem();
            }
        }

        private void SelectDropdownItem()
        {
            // Obtén el valor del TextBox
            string tipoToner = txtTipoToner.Text.Trim();

            // Verifica que el DropDownList tenga elementos
            if (ddlTipoToner.Items.Count > 0)
            {
                // Busca en la lista del DropDownList el valor que coincide
                foreach (ListItem item in ddlTipoToner.Items)
                {
                    if (item.Value.Equals(tipoToner, StringComparison.OrdinalIgnoreCase))
                    {
                        // Selecciona el ítem que coincide
                        ddlTipoToner.SelectedValue = tipoToner;
                        break;
                    }
                }
            }
        }

        protected void ddlBodegaOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del DropDownList
            string selectedValue = ddlBodegaOptions.SelectedValue;

            // Llena el TextBox con el valor seleccionado
            txtBodega.Text = selectedValue;
        }

        private int GetIntValue(string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        private decimal GetDecimalValue(string value)
        {
            decimal result;
            return decimal.TryParse(value, out result) ? result : 0;
        }

        protected void ddlTipoToner_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del DropDownList y asignarlo al TextBox
            this.txtTipoToner.Text = ddlTipoToner.SelectedValue;

            btnEditar.Enabled = false;
            btnCancelar.Visible = true;
            btnActualizar.Visible = true;
            btnLogs.Visible = false;
            ddlTipoToner.Visible = true;
        }

        protected void btnLogs_Click(object sender, EventArgs e)
        {
            // Asignar el valor de la variable estática a un parámetro de consulta
            //string codigoActivo = ;  Reemplaza con el valor que desees
            string url = $"FrmReportes.aspx?codigoActivo=IDToner: {IDToner}";

            // Redirige a la página de reportes con el parámetro de búsqueda
            Response.Redirect(url);
        }


        // Método para llenar los TextBox con los datos del toner
        private void LlenarTextBoxsToner(int IDToner)
        {
            // Consulta SQL para obtener los datos del equipo
            string sqlQuery = "SELECT IDToner, TipoToner, CPC, Repuesto, Stock, AComprar, ValorUnitario, CodSuministro, Bodega, CodigoYupak, Notas FROM IT_Toners WHERE (IDToner = '" + IDToner + "')";

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
                            this.txtTipoToner.Text = reader["TipoToner"].ToString();
                            this.txtCPC.Text = reader["CPC"].ToString();
                            // Asignar valor al CheckBox
                            if (bool.TryParse(reader["Repuesto"].ToString(), out bool repuestoValue))
                            {
                                this.chkRepuesto.Checked = repuestoValue;
                            }
                            else
                            {
                                // Manejar el caso en el que la conversión no sea válida
                                // Puedes establecer un valor predeterminado o manejar el error de otra manera
                            }
                            this.txtStock.Text = reader["Stock"].ToString();
                            this.txtAComprar.Text = reader["AComprar"].ToString();
                            this.txtValorUnitario.Text = Convert.ToDouble(reader["ValorUnitario"]).ToString("F2");
                            this.txtCodSuministro.Text = reader["CodSuministro"].ToString();
                            this.txtBodega.Text = reader["Bodega"].ToString();
                            this.txtCodigoYupak.Text = reader["CodigoYupak"].ToString();
                            this.txtNotas.Text = reader["Notas"].ToString();
                        }
                        else
                        {
                            // No se encontraron datos para el IDEquipo proporcionado
                        }
                    }
                }
            }
        }

        private void HabilitarEdicionCamposTexto(bool habilitar, HtmlGenericControl divFmr)
        {
            if (divFmr != null)
            {
                // Iterar a través de los controles dentro del div
                foreach (Control control in divFmr.Controls)
                {
                    // Verificar si el control es un TextBox y no es el que queremos omitir
                    if (control is TextBox && control.ID != "txtCodigoYupak")
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarEdicionCamposTexto(true, this.divFmrToner); // Habilitar edición de campos de texto

            btnEditar.Visible = false;
            btnCancelar.Visible = true;
            btnActualizar.Visible = true;
            btnLogs.Visible = false;



            ddlTipoToner.Visible = true;
            txtBodega.Visible = false;
            ddlBodegaOptions.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Deshabilitar edición de campos de texto
            HabilitarEdicionCamposTexto(false, this.divFmrToner);

            btnEditar.Visible = true;
            ddlTipoToner.Visible = false;
            btnCancelar.Visible = false;
            btnActualizar.Visible = false;
            btnLogs.Visible = true;
            btnEditar.Enabled = true;
            ddlBodegaOptions.Visible = false;
            txtBodega.Visible = true;

            LlenarTextBoxsToner(IDToner);
        }

        public void ActualizarToner()
        {
            // Consulta SQL para actualizar los datos del equipo
            string sqlQuery = @"
                                UPDATE IT_Toners
                                SET 
                                    TipoToner = @TipoToner,
                                    CPC = @CPC,
                                    Repuesto = @Repuesto,
                                    Stock = @Stock,
                                    AComprar = @AComprar,
                                    ValorUnitario = @ValorUnitario,
                                    CodSuministro = @CodSuministro,
                                    Bodega = @Bodega,
                                    CodigoYupak = @CodigoYupak,
                                    Notas = @Notas
                                WHERE IDToner = @IDToner";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Agregar los parámetros y sus valores
                    command.Parameters.AddWithValue("@IDToner", IDToner);
                    command.Parameters.AddWithValue("@TipoToner", (object)this.txtTipoToner.Text ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CPC", (object)txtCPC.Text ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Repuesto", this.chkRepuesto.Checked);
                    command.Parameters.AddWithValue("@Stock", (object)(!string.IsNullOrEmpty(this.txtStock.Text) ? Convert.ToInt32(this.txtStock.Text) : (int?)null) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AComprar", (object)(!string.IsNullOrEmpty(this.txtAComprar.Text) ? Convert.ToInt32(this.txtAComprar.Text) : (int?)null) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ValorUnitario", (object)(!string.IsNullOrEmpty(this.txtValorUnitario.Text) ? Convert.ToDecimal(this.txtValorUnitario.Text) : (decimal?)null) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CodSuministro", (object)this.txtCodSuministro.Text ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Bodega", (object)this.txtBodega.Text ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CodigoYupak", (object)this.txtCodigoYupak.Text ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Notas", (object)this.txtNotas.Text ?? DBNull.Value);


                    // Abrir conexión
                    connection.Open();

                    // Ejecutar la consulta
                    int rowsAffected = command.ExecuteNonQuery();

                    //LOG
                    // Comparar los valores originales con los valores actualizados
                    StringBuilder detalle = new StringBuilder();

                    // Agregar la descripción inicial al StringBuilder
                    detalle.AppendLine($"Actualizó el registro del Toner de IDToner: {IDToner} ,realizó los siguientes cambios: ");

                    // Comparar cada parámetro y construir el detalle del log
                    if (TipoToner != txtTipoToner.Text)
                        detalle.AppendLine($"Tipo Toner: {TipoToner } -> {txtTipoToner.Text} ");
                    if (CPC != txtCPC.Text)
                        detalle.AppendLine($"CPC: {CPC } -> {txtCPC.Text} ");
                    if (Repuesto != chkRepuesto.Checked.ToString())
                        detalle.AppendLine($"Repuesto: {Repuesto } -> {chkRepuesto.Checked.ToString()} ");
                    if (Stock != txtStock.Text)
                        detalle.AppendLine($"Stock: {Stock } -> {txtStock.Text} ");
                    if (AComprar != txtAComprar.Text)
                        detalle.AppendLine($"A Comprar: {AComprar } -> {txtAComprar.Text} ");
                    if (ValorUnitario != txtValorUnitario.Text)
                        detalle.AppendLine($"Valor Unitario: {ValorUnitario } -> {txtValorUnitario.Text} ");
                    if (CodSuministro != txtCodSuministro.Text)
                        detalle.AppendLine($"Cod Suministro: {CodSuministro } -> {txtCodSuministro.Text} ");
                    if (Bodega != txtBodega.Text)
                        detalle.AppendLine($"Bodega: {Bodega  } -> {txtBodega.Text} ");
                    if (CodigoYupak != txtCodigoYupak.Text)
                        detalle.AppendLine($"Codigo Yupak: {CodigoYupak} -> {txtCodigoYupak.Text} ");
                    if (Notas != txtNotas.Text)
                        detalle.AppendLine($"Notas: {Notas} -> {txtNotas.Text} ");

                    // Crear una instancia de Logger
                    Logger logger = new Logger();
                    string usuario = Session["Usuario"] as string;



                    // Convertir el StringBuilder a string
                    string detalleString = detalle.ToString();

                    // Registrar la acción en el log
                    logger.RegistrarLog(usuario, "Actualizar", "IT_Toners", detalleString, idToner);

                    // Verificar si la actualización fue exitosa
                    if (rowsAffected > 0)
                    {
                        // La actualización fue exitosa
                    }
                    else
                    {
                        // No se encontró el registro a actualizar o no se realizaron cambios
                    }
                }
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarToner();
            // Deshabilitar edición de campos de texto
            HabilitarEdicionCamposTexto(false, this.divFmrToner);

            this.ddlTipoToner.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnActualizar.Visible = false;
            this.btnLogs.Visible = true;
            btnEditar.Visible = true;

            this.ddlTipoToner.Visible = false;
            this.ddlBodegaOptions.Visible = false;
            txtBodega.Visible = true;

            LlenarTextBoxsToner(IDToner);
        }

        //GRIDVIEW EquiposToner ----------------------------

        private void ManejarDobleClic(string idEquipo)
        {
            // Lógica para manejar doble clic
            string grupo;
            //Consulta para obtener el grupo del equipo
            string consultaSQL = "SELECT iet.Grupo FROM IT_Equipos ie JOIN IT_EquiposTipos iet ON ie.EquipoTipo = iet.EquipoTipo WHERE ie.IDEquipo ='" + idEquipo + "'";
            using (SqlConnection conexion = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    conexion.Open();
                    grupo = (string)comando.ExecuteScalar();
                }
            }

            // Verificar si grupo no está vacío antes de redirigir
            if (!string.IsNullOrEmpty(grupo))
            {
                Response.Redirect("FrmEquipo.aspx?IDEquipo=" + idEquipo + "&Grupo=" + grupo);
            }
            else
            {
                // Manejar el caso en que grupo esté vacío (puedes mostrar un mensaje o realizar otra acción)
            }
        }
        protected void GridViewEquiposToner_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ID del equipo desde DataKeys
            string idEquipo = GridViewEquiposToner.SelectedDataKey.Value.ToString();

            // Asignar el ID del equipo al label (para verificar visualmente)
            lblEquipo.Text = idEquipo;

            // Asignar el ID del equipo a la variable de JavaScript
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetSelectedIdEquipo", $"selectedIdEquipo = '{idEquipo}';", true);
        }
        protected void GridViewEquiposToner_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idEquipo = DataBinder.Eval(e.Row.DataItem, "IDEquipo").ToString();
                //e.Row.Attributes["onclick"] = $"rowClick(this, '{idEquipo}');";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewEquiposToner, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["ondblclick"] = $"rowDoubleClick(this, '{idEquipo}');";
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }
        protected void btnEliminarEquipo_Click(object sender, EventArgs e)
        {
            string equipo = lblEquipo.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(
                        "DELETE FROM IT_EquiposToners WHERE IDToner = @IDToner AND IDEquipo = @IDEquipo", connection))
                    {
                        command.Parameters.AddWithValue("@IDToner", IDToner);
                        command.Parameters.AddWithValue("@IDEquipo", equipo);

                        command.ExecuteNonQuery();
                    }
                }

                // Muestra un mensaje de éxito después de eliminar la relación
                string successMessage = "El equipo ha sido eliminado correctamente." /*+ IDToner + " " + equipo*/;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);

                // Deseleccionar la fila seleccionada y limpiar las variables
                GridViewEquiposToner.SelectedIndex = -1;
                lblEquipo.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "resetSelectedIdEquipo", "selectedIdEquipo = null;", true);

                this.GridViewEquiposToner.DataBind();
                this.lblTotalEquipos.Text = GridViewEquiposToner.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre alguna excepción
                string errorMessage = $"Ocurrió un error al eliminar el equipo." /*+ IDToner + " " + equipo*/;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }

        }
        //------------------ GRIDVIEW EquiposToner

        //Modal AñadirEquipo --------------------------------------
        public void InsertarEquiposToners(string IDEquipo, int IDToner)
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

                        //LOG
                        // Crear una instancia de Logger
                        Logger logger = new Logger();
                        string usuario = Session["Usuario"] as string;
                        string detalle = "Agrego el equipo con ID: " + IDEquipo + " al toner con ID: " + IDToner;

                        // Registrar la acción en el log
                        logger.RegistrarLog(usuario, "Crear", "IT_Toners", detalle, idToner);
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
        protected void GridViewAñadirEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = this.GridViewAñadirEquipos.SelectedRow;
            string idEquipo = fila.Cells[4].Text;
            InsertarEquiposToners(idEquipo, IDToner);
            this.GridViewEquiposToner.DataBind();
            this.lblTotalEquipos.Text = GridViewEquiposToner.Rows.Count.ToString();
        }
        protected void GridViewAñadirEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = "if (!confirmSelection(this)) return; " + Page.ClientScript.GetPostBackClientHyperlink(GridViewAñadirEquipos, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }
        protected void btnAñadirEquipo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAñadirEquipo", "$('#modalAñadirEquipo').modal('show');", true);
        }


        protected void GridViewEquiposToner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                // Obtén el índice de la fila que contiene el botón que fue clickeado
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtén la fila correspondiente
                GridViewRow row = GridViewEquiposToner.Rows[index];

                // Obtén el ID del equipo desde el DataKeys
                int idEquipo = Convert.ToInt32(GridViewEquiposToner.DataKeys[index].Value);

                // Obtén el equipo tipo (que está en la columna "EquipoTipo")
                string equipoTipo = row.Cells[1].Text;  // Ajusta el índice según la columna que contiene el "EquipoTipo"

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
        }



        //------------------------- Modal AñadirEquipo
    }
}