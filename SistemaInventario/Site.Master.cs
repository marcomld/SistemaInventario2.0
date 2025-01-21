using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaInventario
{

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarAutenticacion();
                ManejarVisibilidadNavbar();
            }
        }

        // Validar autenticación del usuario
        private void ValidarAutenticacion()
        {
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            List<string> paginasExcluidas = new List<string> { "Default", "Default.aspx", "FrmError.aspx" };

            if (paginasExcluidas.Contains(currentPage))
            {
                // No realizar validación para páginas excluidas
                return;
            }

            bool? autenticado = Session["Autenticado"] as bool?;

            if (autenticado == null || autenticado == false)
            {
                // Redirigir a la página de error si no está autenticado
                Response.Redirect("~/Default.aspx");
            }
        }

        // Manejar visibilidad de los elementos en la navbar
        private void ManejarVisibilidadNavbar()
        {
            if (Session["Usuario"] != null)
            {
                // Usuario autenticado
                phUser.Visible = false;
                phLoggedUser.Visible = true;

                // Muestra el nombre del usuario
                lblUserName.Text = Session["Usuario"].ToString();

                // Muestra las pestañas relevantes
                Principal.Visible = true;
                GridToners.Visible = true;
                GridRepuestos.Visible = true;
                MigrarCustodios.Visible = true;
                MigrarToners.Visible = true;
                Reportes.Visible = true;
                Iniciar.Visible = true;
                DatabaseLabel.Visible = true;

                // Actualiza el nombre de la base de datos
                DatabaseLabel.Text = DatabaseHelper.GetDatabaseNameFromConnectionString();
            }
            else
            {
                // Usuario no autenticado
                phUser.Visible = true;
                phLoggedUser.Visible = false;

                // Oculta las pestañas relevantes
                Principal.Visible = false;
                GridToners.Visible = false;
                GridRepuestos.Visible = false;
                MigrarCustodios.Visible = false;
                MigrarToners.Visible = false;
                Reportes.Visible = false;
                Iniciar.Visible = false;
                DatabaseLabel.Visible = false;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar variables de sesión y redirigir al inicio
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }

    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            // Aquí obtienes la cadena de conexión del archivo web.config
            return ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;
        }

        public static string GetDatabaseNameFromConnectionString()
        {
            var connectionString = GetConnectionString();
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }
    }

}