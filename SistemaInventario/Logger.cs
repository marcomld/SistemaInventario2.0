using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaInventario
{
    public class Logger
    {
        private string connectionStringBDDSistemas = System.Configuration.ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        public void RegistrarLog(string usuario, string accion, string tabla, string detalle, string CodigoActivo)
        {
            // Obtener la fecha actual del sistema
            DateTime fechaActual = DateTime.Now;

            // Consulta para insertar en la tabla de logs
            string query = "INSERT INTO IT_Logs (Usuarios, Fecha, Accion, Tabla, Detalle, CodigoActivo) VALUES (@Usuario, @Fecha, @Accion, @Tabla, @Detalle, @CodigoActivo)";

            using (SqlConnection conn = new SqlConnection(connectionStringBDDSistemas))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Fecha", fechaActual); // Usar la fecha del sistema
                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@Tabla", tabla);
                    cmd.Parameters.AddWithValue("@Detalle", detalle);
                    cmd.Parameters.AddWithValue("@CodigoActivo", CodigoActivo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener el nombre de usuario desde la base de datos
        public string ObtenerNombreUsuario(string codigoUsuario)
        {
            string nombreUsuario = string.Empty;
            string query = "SELECT Usuario FROM IT_Usuarios WHERE CodigoUsuario = @CodigoUsuario";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CodigoUsuario", codigoUsuario);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        nombreUsuario = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error que ocurra
                    // Por ejemplo, puedes registrar el error
                    nombreUsuario = "Error al obtener el nombre"; // Mensaje de error por defecto
                }
            }

            return nombreUsuario;
        }
    }
}