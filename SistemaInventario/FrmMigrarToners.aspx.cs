using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SistemaInventario
{
    public class Toner
    {
        public string Bodega { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Q { get; set; }
        public decimal Unitario { get; set; }
        public string ID { get; set; }
        public int Stock { get; set; }
    }

    public partial class FrmMigrarToners : System.Web.UI.Page
    {
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;
        static string connectionStringBDDYupak = "Data Source=172.16.3.49\\SQL2019;Initial Catalog=GADMAA_INV_NEW;Persist Security Info=True;User ID=sa;Password=Yup@k_SqlServer*2023";

        static List<Toner> listaToner = new List<Toner>();
        static List<Toner> listaTonerNuevos = new List<Toner>();
        static List<Toner> listaStock = new List<Toner>();
        protected void Page_Load(object sender, EventArgs e)
        {

            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Migrar Toners"; // Asigna el título visible en el <div>
            }

            if (!IsPostBack)
            {
                // Si es la primera vez que se carga la página, desactivar los botones
                btnNuevosToners.Enabled = false;
                btnMigrar.Enabled = false;
                btnActualizar.Enabled = false;
                legendContainer.Visible = false;
                CargarDatos();

            }
        }

        public static void ConsultaTonerYupak(string tipoBodega)
        {
            string bodega = "BODEGA DE " + tipoBodega.ToUpper();
            int tipoMovimiento = tipoBodega.Equals("CONSUMO", StringComparison.OrdinalIgnoreCase) ? 0 : 2;

            string query = "exec YP_Inv_blnRepSTDisponible @strSelecciona='Familia',@strBodega=@Bodega,@strDesde='MATERIALES DE OFICINA',@strHasta='MATERIALES DE OFICINA',@intTipoMovimiento=@TipoMovimiento,@intCero=0,@intSerie=0,@intMoneda=0,@intOrdenaProducto=0,@intEmpresa=4,@intYp_Sucursal=1,@intSinLote=1";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDYupak))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros dinámicos
                    command.Parameters.AddWithValue("@Bodega", bodega);
                    command.Parameters.AddWithValue("@TipoMovimiento", tipoMovimiento);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string descripcion = reader["DESCRIPCION"].ToString();

                                if (descripcion.IndexOf("TONER", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("CARTUCHO", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                   (descripcion.IndexOf("TINTA", StringComparison.OrdinalIgnoreCase) >= 0 &&
                                    descripcion.IndexOf("TINTA CORRECTORA", StringComparison.OrdinalIgnoreCase) < 0) ||
                                    descripcion.IndexOf("DRUM", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("UNIDAD DE IMAGEN", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("CINTA", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("KIT DE CABEZAL", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("EPSON", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    descripcion.IndexOf("TANQUE DE MANTENIMIENTO", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    // Crear una nueva instancia de Toner
                                    Toner toner = new Toner
                                    {
                                        Bodega = tipoBodega,
                                        Codigo = reader["Codigo"].ToString(),
                                        Descripcion = reader["Descripcion"].ToString(),
                                        Q = Convert.ToInt32(reader["Q"]),
                                        Unitario = Convert.ToDecimal(reader["Unitario"])
                                    };

                                    // Añadir el objeto Toner a la lista
                                    listaToner.Add(toner);


                                    // Segunda consulta para obtener información de la base de datos Sistemas
                                    string selectQueryYP = "SELECT IDToner, Stock FROM IT_Toners WHERE CodigoYupak = '" + toner.Codigo+ "' AND Bodega = '" + tipoBodega + "';";
                                    using (SqlConnection connectionYP = new SqlConnection(connectionStringBDDSistemas))
                                    {
                                        connectionYP.Open();
                                        using (SqlCommand commandYP = new SqlCommand(selectQueryYP, connectionYP))
                                        {
                                            using (SqlDataReader readerYP = commandYP.ExecuteReader())
                                            {
                                                while (readerYP.Read())
                                                {
                                                    toner.ID = readerYP["IDToner"].ToString();
                                                    toner.Stock = Convert.ToInt32(readerYP["Stock"]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        Console.WriteLine("Consulta ejecutada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                    }
                }
            }
        }

        public void MostarDatos()
        {
            Tabla.Controls.Clear();
            Tabla.BorderWidth = Unit.Pixel(1);
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
            cellNew.Text = "ID";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "TIPO TONER";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "BODEGA";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CODIGO YUPAK";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "STCOK SISTEMAS";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "STOCK YUPAK";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            bool nuevo;
            bool stock;

            for (int j = 0; j < listaToner.Count; j++)
            {
                rowNew = new TableRow();
                Tabla.Controls.Add(rowNew);

                nuevo = true;
                stock = true;
                for (int k = 0; k < 7; k++)
                {

                    cellNew = new TableCell();
                    cellNew.BorderWidth = Unit.Pixel(1);

                    // Obtener el objeto DatosEquipo de la lista
                    Toner toner = listaToner[j];

                    // Asignar los valores de la celda según la propiedad correspondiente del objeto DatosEquipo
                    switch (k)
                    {
                        case 0:
                            cellNew.Text = (j + 1).ToString();
                            break;
                        case 1:
                            cellNew.Text = toner.ID;
                            break;
                        case 2:
                            cellNew.Text = toner.Descripcion;
                            break;
                        case 3:
                            cellNew.Text = toner.Bodega;
                            break;
                        case 4:
                            cellNew.Text = toner.Codigo;
                            break;
                        case 5:
                            cellNew.Text = toner.Stock.ToString();
                            break;
                        case 6:
                            cellNew.Text = toner.Q.ToString();
                            break;
                        default:
                            cellNew.Text = ""; // Otras columnas si es necesario
                            break;
                    }

                    //Lógica de color de celda según el valor de la celda
                    if (string.IsNullOrEmpty(toner.ID))
                    {
                        cellNew.BackColor = System.Drawing.Color.LightGreen;
                        if (nuevo == true)
                        {
                            listaTonerNuevos.Add(toner);
                            nuevo = false;
                            btnNuevosToners.Enabled = true;
                        }
                    }
                    if ((toner.Stock != toner.Q && !string.IsNullOrEmpty(toner.ID)))
                    {
                        cellNew.BackColor = System.Drawing.Color.FromArgb(255, 206, 67);
                        if (stock == true)
                        {
                            listaStock.Add(toner);
                            stock = false;
                            btnActualizar.Enabled = true;
                        }
                    }
                    rowNew.Controls.Add(cellNew);
                }
            }
        }

        public void CargarDatos()
        {
            listaToner.Clear();
            listaTonerNuevos.Clear();
            listaStock.Clear();
            ConsultaTonerYupak("CONSUMO");
            ConsultaTonerYupak("INVERSION");
            MostarDatos();
            btnMigrar.Enabled = false;
            legendContainer.Visible = true;
        }
        protected void btnNuevosToners_Click(object sender, EventArgs e)
        {
            Tabla.Controls.Clear();
            Tabla.BorderWidth = Unit.Pixel(1);
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
            cellNew.Text = "TIPO TONER";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "BODEGA";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CODIGO YUPAK";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "STOCK YUPAK";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;


            int num = 1;
            foreach (var nuevoToner in listaTonerNuevos)
            {
                rowNew = new TableRow();
                Tabla.Controls.Add(rowNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = num.ToString();
                cellNew.BackColor = System.Drawing.Color.LightGreen;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoToner.Descripcion;
                cellNew.BackColor = System.Drawing.Color.LightGreen;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoToner.Bodega;
                cellNew.BackColor = System.Drawing.Color.LightGreen;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoToner.Codigo;
                cellNew.BackColor = System.Drawing.Color.LightGreen;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoToner.Q.ToString();
                cellNew.BackColor = System.Drawing.Color.LightGreen;
                rowNew.Controls.Add(cellNew);
                num++;
            }           
            btnMigrar.Enabled = true;
            legendContainer.Visible = false;
        }

        private static void VerificarTipoToner(string tipoToner, SqlConnection connection, SqlTransaction transaction)
        {
            string checkQuery = "SELECT COUNT(*) FROM IT_TonersTipo WHERE TipoToner = @TipoToner";
            string insertQuery = "INSERT INTO IT_TonersTipo (TipoToner) VALUES (@TipoToner)";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
            {
                checkCommand.Parameters.AddWithValue("@TipoToner", tipoToner);

                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@TipoToner", tipoToner);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void InsertarToners(List<Toner> tonersNuevos)
        {
            string insertQuery = "INSERT INTO IT_Toners (TipoToner, Stock, ValorUnitario, Bodega, CodigoYupak) VALUES (@TipoToner, @Stock, @ValorUnitario, @Bodega, @CodigoYupak)";

            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var toner in tonersNuevos)
                    {
                        // Verificar y insertar en IT_TonersTipo si no existe
                        VerificarTipoToner(toner.Descripcion, connection, transaction);

                        // Insertar en IT_Toners
                        using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@TipoToner", toner.Descripcion);
                            command.Parameters.AddWithValue("@Stock", toner.Q);
                            command.Parameters.AddWithValue("@ValorUnitario", toner.Unitario);
                            command.Parameters.AddWithValue("@Bodega", toner.Bodega);
                            command.Parameters.AddWithValue("@CodigoYupak", toner.Codigo);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    // Muestra un mensaje de éxito
                    string successMessage = "Migración realizada correctamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Muestra un mensaje de error
                    string errorMessage = $"Ocurrió un error al realizar la Migración";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
                }
            }
        }

        protected void btnMigrar_Click(object sender, EventArgs e)
        {
            InsertarToners(listaTonerNuevos);
            btnNuevosToners.Enabled = false;
            btnMigrar.Enabled = false;
            CargarDatos();
        }

        public void ActualizarStock(List<Toner> toners)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    foreach (var toner in toners)
                    {
                        using (SqlCommand command = new SqlCommand(
                            "UPDATE IT_Toners SET Stock = @stock WHERE IDToner = @IDToner", connection))
                        {
                            command.Parameters.AddWithValue("@stock", toner.Q);
                            command.Parameters.AddWithValue("@IDToner", toner.ID);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                // Muestra un mensaje de éxito después de actualizar el stock
                string successMessage = "El stock ha sido actualizado correctamente.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre alguna excepción
                string errorMessage = $"Ocurrió un error al actualizar el stock";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarStock(listaStock);
            btnActualizar.Enabled = false;
            CargarDatos();
        }
    }
}