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
            // Obtener los valores ingresados por el usuario
            string cedulaIngresada = txtCedula.Text.Trim();
            string claveIngresada = txtClave.Text.Trim();

            // Validar que ambos campos estén llenos
            if (string.IsNullOrEmpty(cedulaIngresada) || string.IsNullOrEmpty(claveIngresada))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Por favor, complete ambos campos.');", true);
                return;
            }

            // Cadena de conexión
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            // Primera consulta: verificar cédula y contraseña en la tabla Usuarios
            string queryUsuario = "SELECT IDUsuario, Usuario FROM Usuarios WHERE Cedula = @Cedula AND Clave = @Clave";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand commandUsuario = new SqlCommand(queryUsuario, connection);
                commandUsuario.Parameters.AddWithValue("@Cedula", cedulaIngresada);
                commandUsuario.Parameters.AddWithValue("@Clave", claveIngresada);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = commandUsuario.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idUsuario = reader.GetInt32(0); // Obtener IDUsuario
                            string nombreUsuario = reader.GetString(1); // Obtener Nombre

                            // Segunda consulta: verificar acceso en la tabla AccesoUsuarioSistemas
                            string queryAcceso = "SELECT COUNT(*) FROM AccesoUsuarioSistemas WHERE IDUsuario = @IDUsuario AND IDSistema = @IDSistema";
                            SqlCommand commandAcceso = new SqlCommand(queryAcceso, connection);
                            commandAcceso.Parameters.AddWithValue("@IDUsuario", idUsuario);
                            commandAcceso.Parameters.AddWithValue("@IDSistema", 1); // IDSistema es fijo en este caso

                            reader.Close(); // Cerrar el reader antes de ejecutar otra consulta en la misma conexión

                            int accesoResult = (int)commandAcceso.ExecuteScalar();

                            if (accesoResult > 0)
                            {
                                // Acceso concedido, guardar información en la sesión
                                Session["IDUsuario"] = idUsuario;
                                Session["Usuario"] = nombreUsuario;
                                Session["Autenticado"] = true; // Confirmación de autenticación

                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('¡Autenticación exitosa! Bienvenido, {nombreUsuario}.');", true);
                                Response.Redirect("FrmPrincipal.aspx");
                            }
                            else
                            {
                                // Usuario no tiene acceso al sistema
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('No tiene acceso a este sistema.');", true);
                            }
                        }
                        else
                        {
                            // Cédula o clave incorrecta
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Cédula o contraseña incorrecta.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones, como errores de conexión
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
                }
            }


        }

    }
}