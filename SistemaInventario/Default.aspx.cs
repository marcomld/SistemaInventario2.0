using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public partial class FrmIniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string codigoIngresado = txtCodigo.Text.Trim(); // Obtener el código ingresado y eliminar espacios en blanco

            if (string.IsNullOrEmpty(codigoIngresado))
            {
                // Opcional: Manejar el caso en que el campo está vacío.
                // Puedes mostrar un mensaje de error al usuario aquí.
                return;
            }

            // Definir la cadena de conexión
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            // Consulta SQL para verificar si el código existe
            string query = "SELECT [CodigoUsuario] FROM [IT_Usuarios] WHERE [CodigoUsuario] = @CodigoUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CodigoUsuario", codigoIngresado);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        // Crear una instancia de Logger
                        Logger logger = new Logger();
                        string usuario = logger.ObtenerNombreUsuario(codigoIngresado);

                        Session["Usuario"] = usuario;
                        // Código válido, guarda el código en la sesión
                        Session["CodigoUnico"] = codigoIngresado;
                        Response.Redirect($"FrmPrincipal.aspx");
                    }
                    else
                    {
                        // Muestra un mensaje de error si ocurre alguna excepción
                        string errorMessage = $"El código es incorrecto";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
                    }
                }
                catch (Exception ex)
                {
                    // Opcional: Manejar excepciones, como errores de conexión.
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{ex}');", true);
                }
            }
        }

    }
}