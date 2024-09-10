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
            if (Session["Usuario"] != null)
            {
                phUser.Visible = false;
                phLoggedUser.Visible = true;
                lblUserName.Text = Session["Usuario"].ToString();
                // Mostrar las pestañas cuando el usuario está autenticado
                Principal.Visible = true; 
                GridToners.Visible = true;
                GridRepuestos.Visible = true;
                MigrarCustodios.Visible = true;
                MigrarToners.Visible = true;
                Reportes.Visible = true;
                Iniciar.Visible = true;
                DatabaseLabel.Visible = true;

                // Obtén el nombre de la base de datos y actualiza el texto de la etiqueta
                DatabaseLabel.Text = "" + DatabaseHelper.GetDatabaseNameFromConnectionString();


            }
            else
            {
                phUser.Visible = true;
                phLoggedUser.Visible = false;
                // Ocultar las pestañas cuando el usuario no está autenticado
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
            Session["Usuario"] = null;
            Session["CodigoUnico"] = null;
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