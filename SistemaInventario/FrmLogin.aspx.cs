using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SistemaInventario
{
    public partial class FormLogin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Value;
            string clave = txtClave.Value;

            // Acceder a la cadena de conexión desde web.config
            string connectionString = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            // Consulta SQL para validar las credenciales
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario AND Clave = @Clave";

            // Define una variable para almacenar el número de filas que coinciden con las credenciales proporcionadas
            int count = 0;

            // Establecer la conexión con la base de datos y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros para evitar la inyección SQL
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Clave", clave);

                    try
                    {
                        // Abrir la conexión
                        connection.Open();

                        // Ejecutar la consulta y obtener el resultado
                        count = (int)command.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        // Manejar errores de SQL
                        lblMensaje.InnerText = "Error al conectar con la base de datos: " + ex.Message;
                        return;
                    }
                }
            }

            // Verificar si se encontraron coincidencias
            if (count > 0)
            {
                // Credenciales válidas, redirigir al usuario a la página de inicio
                Response.Redirect("FrmPrincipal.aspx");
            }
            else
            {
                // Credenciales inválidas, mostrar un mensaje de error
                lblMensaje.InnerText = "Usuario o contraseña incorrectos. Por favor, inténtalo de nuevo.";
            }
        }
    }


}



