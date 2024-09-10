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
    public partial class FormPrincipal : System.Web.UI.Page
    {

        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;
        static string consultaOriginal = "SELECT[CodigoActivo], [EquipoTipo], [Nombres], [Ubicacion], [Marca], [Modelo], [IDEquipo], [Imagenes] FROM[ViewEquipos] WHERE([CodigoActivo] LIKE '%' + '%') OR([Nombres] LIKE '%' + '%')";

        protected void Page_Load(object sender, EventArgs e)
        {

            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Lista de Equipos"; // Asigna el título visible en el <div>


            }

            lblRegistros.Text = GridViewEquipos.Rows.Count.ToString();

            if (!IsPostBack)
            {
                //Obtener el código único de la sesión
                string codigoUnico = Session["CodigoUnico"] as string;

                if (string.IsNullOrEmpty(codigoUnico))
                {
                    // Si no hay un código único en la sesión, redirige a la página de error
                    Response.Redirect("~/FrmError.aspx");
                }
                else
                {
                    // Código válido, continúa con la lógica de la página
                }
            }
            else
            {
                // Manejar acciones postback si es necesario
                lblRegistros.Text = GridViewEquipos.Rows.Count.ToString();
            }
        }

        //protected bool CodigoUnicoValidoEnBaseDeDatos(string codigo)
        //{
        //    // Aquí deberías consultar tu base de datos para verificar si 'codigo' existe y es válido
        //    bool codigoValido = false;

        //    // Lógica para verificar en la base de datos
        //    using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
        //    {
        //        string query = "SELECT COUNT(*) FROM IT_Usuarios WHERE CodigoUsuario = @Codigo";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@Codigo", codigo);
        //        connection.Open();
        //        int count = (int)command.ExecuteScalar();
        //        codigoValido = (count > 0);
        //    }

        //    return codigoValido;
        //}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Verifica si el cuadro de texto está vacío
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                // Establece la consulta original para seleccionar todos los registros
                RestaurarConsultaOriginal();
            }
            else
            {
                // Realiza la búsqueda utilizando el valor del cuadro de texto
                string filtro = txtBuscar.Text.Trim();
                string consultaSQL = "SELECT [CodigoActivo], [EquipoTipo], [Nombres], [Ubicacion], [Marca], [Modelo], [IDEquipo], [Imagenes] " +
                                    "FROM [ViewEquipos] " +
                                    "WHERE ([CodigoActivo] LIKE '%' + @filtro + '%') OR ([Nombres] LIKE '%' + @filtro + '%') OR ([Ubicacion] LIKE '%' + @filtro + '%') OR ([EquipoTipo] LIKE '%' + @filtro + '%')";

                SqlDataSource1.SelectCommand = consultaSQL;
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("filtro", filtro);
            }

            GridViewEquipos.DataBind();

        }




        protected void GridViewEquipos_DataBound(object sender, EventArgs e)
        {
            // Total de registros
            lblRegistros.Text = GridViewEquipos.Rows.Count.ToString();
        }

        private void RestaurarConsultaOriginal()
        {
            // Restaurar la consulta original
            SqlDataSource1.SelectCommand = consultaOriginal;
            SqlDataSource1.SelectParameters.Clear();
        }


        protected void GridViewEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cedulaCustodio;
            string grupo;

            GridViewRow fila = this.GridViewEquipos.SelectedRow;

            //Consulta para obtener la cedulaE del custodio
            //string consultaSQL = "SELECT CedulaCustodio FROM ViewEquipos WHERE IDEquipo = '" + fila.Cells[6].Text + "'";
            //using (SqlConnection conexion = new SqlConnection(connectionStringBDDSistemas))
            //{
            //    using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
            //    {
            //        conexion.Open();
            //        cedulaCustodio = (string)comando.ExecuteScalar();
            //    }


            //    //Consulta para obtener el grupo del equipo
            //    consultaSQL = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = '" + fila.Cells[1].Text + "'";

            //    using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
            //    {
            //        grupo = (string)comando.ExecuteScalar();
            //    }
            //}

            string consultaSQL = "SELECT Grupo FROM IT_EquiposTipos WHERE EquipoTipo = '" + fila.Cells[1].Text + "'";
            using (SqlConnection conexion = new SqlConnection(connectionStringBDDSistemas))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    grupo = (string)comando.ExecuteScalar();
                }
            }

            //Se redireciona los datos obtenidos a la nueva página
            Response.Redirect("FrmEquipo.aspx?IDEquipo=" + fila.Cells[6].Text  + "&Grupo=" + grupo);
        }

        protected void GridViewEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Agregar atributo onclick a cada fila
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewEquipos, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }

        protected void btnNuevoEquipo_Click(object sender, EventArgs e)
        {
            string nuevoEquipo = "SI";
            //Response.Redirect("FrmNuevoEquipo.aspx");
            Response.Redirect("FrmEquipo.aspx?IDEquipo=" + "0"  +"&Grupo=" + "" + "&nuevoEquipo=" + nuevoEquipo);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmNuevoEquipo.aspx");
        }
    }
}