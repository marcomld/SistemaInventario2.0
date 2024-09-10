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
    public partial class FrmGridToners : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Lista de Toners"; // Asigna el título visible en el <div>
            }


            lblRegistros.Text = GridToners.Rows.Count.ToString();

            if (!IsPostBack)
            {
                // Obtener el código único de la sesión
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
        }


        private void RestaurarConsultaOriginal()
        {
            // Restaurar la consulta original
            SqlDataSourceToners.SelectCommand = "SELECT * FROM [IT_Toners]";
            SqlDataSourceToners.SelectParameters.Clear();
        }

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
                string consultaSQL = "SELECT [IDToner], [TipoToner], [CPC], [Repuesto], [Stock], [AComprar], [ValorUnitario], [CodSuministro], [Bodega], [CodigoYupak] " +
                                    "FROM [IT_Toners] " +
                                    "WHERE ([TipoToner] LIKE '%' + @filtro + '%') OR ([Stock] LIKE '%' + @filtro + '%')";

                SqlDataSourceToners.SelectCommand = consultaSQL;
                SqlDataSourceToners.SelectParameters.Clear();
                SqlDataSourceToners.SelectParameters.Add("filtro", filtro);
            }

            GridToners.DataBind();
        }

        protected void GridViewEquipos_DataBound(object sender, EventArgs e)
        {
            // Total de registros
            lblRegistros.Text = GridToners.Rows.Count.ToString();
        }


        protected void GridViewEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow fila = this.GridToners.SelectedRow;
            Response.Redirect("FrmToner.aspx?IDToner=" + fila.Cells[0].Text);

        }

        protected void GridViewEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Agregar atributo onclick a cada fila
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridToners, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }
    }
}