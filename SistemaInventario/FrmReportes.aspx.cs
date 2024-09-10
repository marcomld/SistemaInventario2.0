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
    public partial class FrmReportes : System.Web.UI.Page
    {
        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");

            GridView1.DataSourceID = "SqlDataSource1";

            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Reportes"; // Asigna el título visible en el <div>
            }

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
                    // GridView1.DataBind(); // Esto puede estar comentado si no necesitas hacer un DataBind en el Page_Load
                }
            }

            // Leer el parámetro de consulta "codigoActivo"
            string codigoActivo = Request.QueryString["codigoActivo"];

            if (!string.IsNullOrEmpty(codigoActivo))
            {
                // Establecer el valor del TextBox
                txtBuscar.Text = codigoActivo;

                // Ejecutar la búsqueda automáticamente
                btnBuscar_Click(null, EventArgs.Empty);

                // Verificar el valor del parámetro de consulta
                if (codigoActivo == "EquiposNoUsados")
                {
                    // Hacer visible el GridViewEquiposNoUsados
                    GridViewEquiposNoUsados.Visible = true;
                }
                else
                {
                    // Hacer invisible el GridViewEquiposNoUsados si el valor no coincide
                    GridViewEquiposNoUsados.Visible = false;
                }
            }
            else
            {
                // Si no hay un valor para "codigoActivo", asegúrate de que el GridView esté oculto
                GridViewEquiposNoUsados.Visible = false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            GridView1.DataSourceID = "SqlDataSourceLogs";
            SqlDataSourceLogs.SelectParameters["SearchText"].DefaultValue = txtBuscar.Text.Trim();
            GridView1.DataBind(); // Para actualizar el GridView con los nuevos resultados
        }


        protected void btnEliminados_Click(object sender, EventArgs e)
        {
            Tabla.Controls.Clear();
            Tabla.BorderWidth = Unit.Pixel(2);
            TableRow rowNew = new TableRow();
            Tabla.Controls.Add(rowNew);

            TableCell cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "N°";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CEDULA ULTIMO CUSTODIO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "NOMBRE ULTIMO CUSTODIO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "EQUIPO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CODIGO ACTIVO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "FECHA DE ADQUISICION";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "MARCA";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "MODELO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "SERIE";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "FECHA DADO DE BAJA";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;



            //Variable para indicar el número de fila
            int fila = 1;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    string selectQuery = "SELECT e.CodigoActivo, e.EquipoTipo, e.UltimoCustodio, c.Nombres, e.FechaAdquisicion, e.Marca, e.Modelo, e.Serie, e.FechaBaja FROM IT_EquiposBorrados e INNER JOIN IT_Custodios c ON e.UltimoCustodio = c.Cedula";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Obtener los valores de cada columna
                                string codigoActivo = reader["CodigoActivo"].ToString();
                                string equipoTipo = reader["EquipoTipo"].ToString();
                                string ultimoCustodio = reader["UltimoCustodio"].ToString();
                                string nombres = reader["Nombres"].ToString();
                                string marca = reader["Marca"].ToString();
                                string modelo = reader["Modelo"].ToString();
                                string serie = reader["Serie"].ToString();
                                string fechaAdquisicion = ((DateTime)reader["FechaAdquisicion"]).ToString("yyyy-MM-dd");
                                string fechaBaja = ((DateTime)reader["FechaBaja"]).ToString("yyyy-MM-dd");

                                rowNew = new TableRow();
                                Tabla.Controls.Add(rowNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = fila.ToString();
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = ultimoCustodio;
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = nombres;
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = equipoTipo;
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = codigoActivo;
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = fechaAdquisicion.ToString();
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = marca.ToString();
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = modelo.ToString();
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = serie.ToString();
                                rowNew.Controls.Add(cellNew);

                                cellNew = new TableCell();
                                cellNew.BorderWidth = Unit.Pixel(1);
                                cellNew.Text = fechaBaja.ToString();
                                rowNew.Controls.Add(cellNew);
                            
                                fila++;

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            GridView1.Visible = false;
            Label1.Visible = false;
            txtBuscar.Visible = false;
            btnBuscar.Visible = false;
        }


        protected void SqlDataSourceEquiposNoUsados_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            // Aquí puedes añadir lógica adicional antes de ejecutar la consulta
        }



    }
}