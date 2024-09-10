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
    public partial class FrmAñadirToner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Lógica de carga de la página
        }


        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        //Variable del IDEquipo
        static string idEquipoE;
        static string grupoE;
        static string cedulaE;
        static string codigoActivo;


        protected void BtnBuscarToner_Click(object sender, EventArgs e)
        {
            // Asignar el valor del TextBox al parámetro del SqlDataSource
            SqlDataSourceTonerBuscar.SelectParameters["TipoToner"].DefaultValue = TextBuscarToner.Text;

            // Cambiar el DataSource del GridView a SqlDataSourceTonerBuscar
            GridViewTonerModal.DataSourceID = "SqlDataSourceTonerBuscar";

            // Vuelve a vincular los datos del GridView para que muestre los resultados de búsqueda
            GridViewTonerModal.DataBind();
        }

        protected void GridViewTonerModal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Verifica si se está seleccionando una fila
            if (e.CommandName == "Select")
            {
                // Obtener el índice de la fila seleccionada
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = GridViewTonerModal.Rows[rowIndex];

                // Obtener el IDToner de la fila seleccionada
                int selectedIDToner = Convert.ToInt32(GridViewTonerModal.DataKeys[rowIndex].Value);

                // Agregar el IDToner al TextArea
                if (!string.IsNullOrEmpty(TextAreaIDToners.Text))
                {
                    TextAreaIDToners.Text += ", "; // Añadir coma si el TextArea ya tiene texto
                }

                TextAreaIDToners.Text += selectedIDToner.ToString(); // Agregar el IDToner seleccionado
            }
        }



    }
}
