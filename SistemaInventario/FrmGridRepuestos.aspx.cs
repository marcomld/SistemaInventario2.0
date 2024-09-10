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
    public partial class FrmGridRepuestos : System.Web.UI.Page
    {

        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
                if (literalPageTitle != null)
                {
                    literalPageTitle.Text = "LISTA DE REPUESTOS"; // Asigna el título visible en el <div>
                }
                // Al cargar la página por primera vez, se carga todos los repuestos
                GridRepuestos.DataBind(); // Asegurarse de que el GridView se enlaza correctamente
            }
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            // Establecer el valor del parámetro de búsqueda en el SqlDataSource
            IT_Repuestos.SelectCommand = "SELECT * FROM [IT_Repuestos] WHERE " +
                                          "[NombreRepuesto] LIKE '%' + @searchText + '%' OR " +
                                          "[IDRepuesto] LIKE '%' + @searchText + '%' OR " +
                                          "[Descripcion] LIKE '%' + @searchText + '%' OR " +
                                          "[Proveedor] LIKE '%' + @searchText + '%'";

            // Asignar el valor del parámetro de búsqueda
            IT_Repuestos.SelectParameters.Clear(); // Limpiar parámetros existentes
            IT_Repuestos.SelectParameters.Add("searchText", txtBuscar.Text.Trim()); // Agregar el nuevo parámetro

            // Vuelve a enlazar el GridView para que muestre los resultados filtrados
            GridRepuestos.DataBind();
        }

        protected void btnAgregarRepuesto_Click(object sender, EventArgs e)
        {
            // Abrir el modal usando JavaScript
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAgregarRepuesto", "$('#modalAgregarRepuesto').modal('show');", true);
        }


        protected void btnGuardarRepuesto_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos del modal
            string nombreRepuesto = txtNombreRepuesto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            int cantidad;
            decimal costo;
            bool cantidadValida = int.TryParse(txtCantidad.Text.Trim(), out cantidad);
            bool costoValido = decimal.TryParse(txtCosto.Text.Trim(), out costo);

            if (!cantidadValida || !costoValido)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cantidad o Costo inválidos.');", true);
                return;
            }

            string proveedor = txtProveedor.Text.Trim();
            string observaciones = txtObservaciones.Text.Trim();

            // Consulta para insertar un nuevo repuesto en la base de datos
            string query = "INSERT INTO [IT_Repuestos] (NombreRepuesto, Descripcion, Cantidad, Costo, FechaAdquisicion, Proveedor, Observaciones) " +
                           "VALUES ( @NombreRepuesto, @Descripcion, @Cantidad, @Costo, @FechaAdquisicion, @Proveedor, @Observaciones)";



            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas)) // Cambia la cadena de conexión aquí
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Asignar valores a los parámetros
                command.Parameters.AddWithValue("@NombreRepuesto", nombreRepuesto);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@Cantidad", cantidad);
                command.Parameters.AddWithValue("@Costo", costo);
                command.Parameters.AddWithValue("@FechaAdquisicion", DateTime.Now); // Cambia esto si tienes otro valor para la fecha
                command.Parameters.AddWithValue("@Proveedor", proveedor);
                command.Parameters.AddWithValue("@Observaciones", observaciones);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

            // Volver a enlazar el GridView para mostrar el nuevo repuesto
            GridRepuestos.DataBind();
        }

        protected void GridViewRepuestos_DataBound(object sender, EventArgs e)
        {
            // Total de registros
            lblRegistros.Text = GridRepuestos.Rows.Count.ToString();
        }

        protected void GridViewRepuestos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Agregar atributo onclick a cada fila
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridRepuestos, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }

        protected void GridViewRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Obtener el IDRepuesto usando DataKeys
                int index = Convert.ToInt32(e.CommandArgument);
                int idRepuesto = Convert.ToInt32(GridRepuestos.DataKeys[index].Value);

                // Redirigir a la página FrmRepuestos.aspx con el IDRepuesto en la query string
                Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + idRepuesto);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmRepuestos.aspx?IDRepuesto=" + "0" );
        }
    }
}