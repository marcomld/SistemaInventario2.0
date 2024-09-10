using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public class DatosEquipo
    {
        public string CedulaS { get; set; }
        public string NombreS { get; set; }
        public string EquipoTipo { get; set; }
        public string IDEquipo { get; set; }
        public string CodigoActivo { get; set; }
        public string NumeroFila { get; set; }
        public string Tipo { get; set; }
        public string SubTipo { get; set; }
        public string Clase { get; set; }
        public string Origen { get; set; }
        public string CedulaY { get; set; }
        public string NombreY { get; set; }

    }



    public partial class FrmMigrarCustodios : System.Web.UI.Page
    {

        // Cadena de conexión para la base de datos BDDYupak
        static string connectionStringBDDYupak = ConfigurationManager.ConnectionStrings["BDDYupakConnectionString"].ConnectionString;

        // Cadena de conexión para la base de datos BDDSistemas
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        // Declarar listas para almacenar los datos
        static List<DatosEquipo> listaEquipos = new List<DatosEquipo>();
        static List<DatosEquipo> nuevosCustodios = new List<DatosEquipo>();


        protected void Page_Load(object sender, EventArgs e)
        {

            Literal literalPageTitle = (Literal)Master.FindControl("LiteralPageTitle");
            if (literalPageTitle != null)
            {
                literalPageTitle.Text = "Migrar Custodios"; // Asigna el título visible en el <div>
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
                    // Código válido, continúa con la lógica de la página
                }

                // Si es la primera vez que se carga la página, desactivar los botones
                btnCargarMatrizNuevos.Enabled = false;
                btnMigrar.Enabled = false;
            }
        }


        public void CargarDatos()
        {
            listaEquipos.Clear();

            try
            {
                string selectQuery = "SELECT Cedula, Nombres, EquipoTipo, IDEquipo, CodigoActivo FROM IT_Custodios RIGHT OUTER JOIN IT_Equipos ON IT_Custodios.Cedula = IT_Equipos.CedulaCustodio WHERE IT_Equipos.CedulaCustodio <> '1000000000' OR IT_Equipos.CedulaCustodio IS NULL";
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterar sobre los resultados y almacenarlos en la lista de objetos DatosEquipo
                            while (reader.Read())
                            {
                                string cedula = reader["Cedula"].ToString();
                                string nombre = reader["Nombres"].ToString();
                                string equipoTipo = reader["EquipoTipo"].ToString();
                                string idEquipo = reader["IDEquipo"].ToString();
                                string codigoActivo = reader["CodigoActivo"].ToString().Trim();


                                DatosEquipo equipo = new DatosEquipo
                                {
                                    CedulaS = cedula,
                                    NombreS = nombre,
                                    EquipoTipo = equipoTipo,
                                    IDEquipo = idEquipo,
                                    CodigoActivo = codigoActivo,
                                    NumeroFila = (listaEquipos.Count + 1).ToString()
                                };
                                listaEquipos.Add(equipo);

                                //Condicion para que el codigo de activo no contenga letras
                                if (Regex.IsMatch(codigoActivo, @"^\d+$"))
                                {
                                    // Segunda consulta para obtener información de la otra base de datos utilizando el código de activo
                                    string selectQueryYP = "SELECT Tipo, SubTipo, Clase, Origen FROM YP_GAF_ASSETS WHERE Codigo = @codigoActivo ORDER BY Tipo";
                                    using (SqlConnection connectionYP = new SqlConnection(connectionStringBDDYupak))
                                    {
                                        connectionYP.Open();
                                        using (SqlCommand commandYP = new SqlCommand(selectQueryYP, connectionYP))
                                        {
                                            commandYP.Parameters.AddWithValue("@codigoActivo", codigoActivo);
                                            using (SqlDataReader readerYP = commandYP.ExecuteReader())
                                            {
                                                while (readerYP.Read())
                                                {
                                                    equipo.Tipo = readerYP["Tipo"].ToString();
                                                    equipo.SubTipo = readerYP["SubTipo"].ToString();
                                                    equipo.Clase = readerYP["Clase"].ToString();
                                                    equipo.Origen = readerYP["Origen"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBDDYupak))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("YP_Act_blnBuscarBien", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var equipo in listaEquipos)
                    {
                        if (Regex.IsMatch(equipo.CodigoActivo, @"^\d+$"))
                        {
                            string codigoActivo = equipo.CodigoActivo;
                            string tipo = equipo.Tipo;
                            string subtipo = equipo.SubTipo;
                            string clase = equipo.Clase;
                            string origen = equipo.Origen;

                            // Limpiar parámetros existentes antes de agregar nuevos
                            command.Parameters.Clear();

                            // Agregar parámetros de entrada
                            command.Parameters.AddWithValue("@intEmpresa", 3);
                            command.Parameters.AddWithValue("@intYp_Sucursal", 1);
                            command.Parameters.AddWithValue("@intSeleccionadoPor", 2);
                            command.Parameters.AddWithValue("@strCondicion", $"AND YP_GAF_ASSETS.Tipo='{tipo}' AND YP_GAF_ASSETS.SubTipo='{subtipo}' AND YP_GAF_ASSETS.Clase='{clase}' AND YP_GAF_ASSETS.Codigo='{codigoActivo}'");
                            command.Parameters.AddWithValue("@dblVResidual", 10);
                            command.Parameters.AddWithValue("@intBajas", 0);


                            // Ejecutar el comando y obtener el SqlDataReader
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Verificar si hay filas en el resultado
                                if (reader.HasRows)
                                {
                                    // Iterar sobre las filas
                                    while (reader.Read())
                                    {
                                        // Obtener el valor de la columna "Custodio y Nombre"
                                        equipo.CedulaY = reader["Custodio"].ToString();
                                        equipo.NombreY = reader["Nombre"].ToString();
                                    }
                                }
                                else
                                {
                                    //Si no se encuentra datos de la BDDYupak el equipo se dio de Baja
                                    if (!string.IsNullOrEmpty(equipo.CedulaS))
                                    {
                                        equipo.CedulaY = "1000000000";
                                        equipo.NombreY = "Equipo dado de Baja";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            // Ocultar el círculo de carga cuando los datos estén cargados
            ClientScript.RegisterStartupScript(this.GetType(), "hideLoading", "hideLoading();", true);
        }
        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MostrarDatos();
        }


        public void MostrarDatos()
        {
            nuevosCustodios.Clear();

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
            cellNew.Text = "CEDULA CUSTODIO ANTERIOR";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "NOMBRE CUSTODIO ANTERIOR";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CEDULA CUSTODIO ACTUAL";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "NOMBRE CUSTODIO ACTUAL";
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
            cellNew.Text = "ID EQUIPO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;


            bool nuevo;
            for (int j = 0; j < listaEquipos.Count; j++)
            {
                rowNew = new TableRow();
                Tabla.Controls.Add(rowNew);
                nuevo = true;
                for (int k = 0; k < 8; k++)
                {

                    cellNew = new TableCell();
                    cellNew.BorderWidth = Unit.Pixel(1);

                    // Obtener el objeto DatosEquipo de la lista
                    DatosEquipo equipo = listaEquipos[j];

                    // Asignar los valores de la celda según la propiedad correspondiente del objeto DatosEquipo
                    switch (k)
                    {
                        case 0:
                            cellNew.Text = equipo.NumeroFila;
                            break;
                        case 1:
                            cellNew.Text = equipo.CedulaS;
                            break;
                        case 2:
                            cellNew.Text = equipo.NombreS;
                            break;
                        case 3:
                            cellNew.Text = equipo.CedulaY;
                            break;
                        case 4:
                            cellNew.Text = equipo.NombreY;
                            break;
                        case 5:
                            cellNew.Text = equipo.EquipoTipo;
                            break;
                        case 6:
                            cellNew.Text = equipo.CodigoActivo;
                            break;
                        case 7:
                            cellNew.Text = equipo.IDEquipo;
                            break;
                        default:
                            cellNew.Text = ""; // Otras columnas si es necesario
                            break;
                    }

                    // Lógica de color de celda según el valor de la celda
                    if (equipo.CedulaS != equipo.CedulaY && !string.IsNullOrEmpty(equipo.CedulaY))
                    {
                        cellNew.BackColor = System.Drawing.Color.Coral;
                        if (nuevo == true)
                        {
                            nuevosCustodios.Add(equipo);
                            nuevo = false;
                            btnCargarMatrizNuevos.Enabled = true;
                        }
                    }
                    rowNew.Controls.Add(cellNew);
                }
            }          
        }

        protected void btnCargarMatrizCompleta_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCargarMatrizNuevos_Click(object sender, EventArgs e)
        {
            Tabla.Controls.Clear();
            Tabla.BorderWidth = Unit.Pixel(2);
            TableRow rowNew = new TableRow();
            Tabla.Controls.Add(rowNew);


            TableCell cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "Nro";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CEDULA CUSTODIO ANTERIOR";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "NOMBRE CUSTODIO ANTERIOR";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "CEDULA CUSTODIO ACTUAL";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;

            cellNew = new TableCell();
            cellNew.BorderWidth = Unit.Pixel(1);
            cellNew.Text = "NOMBRE CUSTODIO ACTUAL";
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
            cellNew.Text = "ID EQUIPO";
            rowNew.Controls.Add(cellNew);
            cellNew.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
            cellNew.ForeColor = System.Drawing.Color.White;
            cellNew.HorizontalAlign = HorizontalAlign.Center;          


            int num = 1;
            foreach (var nuevoCustodio in nuevosCustodios)
            {
                rowNew = new TableRow();
                Tabla.Controls.Add(rowNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = num.ToString();
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);


                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.CedulaS;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.NombreS;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.CedulaY;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.NombreY;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.EquipoTipo;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.CodigoActivo;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);

                cellNew = new TableCell();
                cellNew.BorderWidth = Unit.Pixel(1);
                cellNew.Text = nuevoCustodio.IDEquipo;
                cellNew.BackColor = System.Drawing.Color.Coral;
                rowNew.Controls.Add(cellNew);
                num++;
            }

            btnMigrar.Enabled = true;

        }

        protected void btnMigrar_Click(object sender, EventArgs e)
        {
            SqlConnection connection2 = new SqlConnection(connectionStringBDDSistemas);
            connection2.Open();


            using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
            {
                connection.Open();
                bool migracionExitosa = true;

                foreach (var nuevoCustodio in nuevosCustodios)
                {
                    try
                    {
                        // Verificar si el nuevo custodio existe en la tabla IT_Custodios
                        string selectQueryCustodios = "SELECT COUNT(*) FROM IT_Custodios WHERE Cedula = @Cedula";
                        using (SqlCommand command = new SqlCommand(selectQueryCustodios, connection))
                        {
                            command.Parameters.AddWithValue("@Cedula", nuevoCustodio.CedulaY);
                            int count = (int)command.ExecuteScalar();

                            if (count == 0)
                            {
                                // Si el nuevo custodio no existe, ingresarlo en la tabla IT_Custodios
                                string insertQuery = "INSERT INTO IT_Custodios (Cedula, Nombres, Cargo) VALUES (@Cedula, @Nombre, @Cargo)";
                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@Cedula", nuevoCustodio.CedulaY);
                                    insertCommand.Parameters.AddWithValue("@Nombre", nuevoCustodio.NombreY);
                                    insertCommand.Parameters.AddWithValue("@Cargo", "-");
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        if (nuevoCustodio.CedulaY == "1000000000")
                        {
                            string selectQueryEquipos = "SELECT EquipoTipo, CedulaCustodio, FechaAdquisicion, Marca, Modelo, Serie FROM IT_Equipos WHERE CodigoActivo = @CodigoActivo";
                            using (SqlCommand command2 = new SqlCommand(selectQueryEquipos, connection2))
                            {
                                command2.Parameters.AddWithValue("@CodigoActivo", nuevoCustodio.CodigoActivo);
                                using (SqlDataReader reader = command2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string equipoTipo = reader["EquipoTipo"].ToString();
                                        string ultimoCustodio = reader["CedulaCustodio"].ToString();
                                        string fechaAdquisicion = reader["FechaAdquisicion"].ToString();
                                        string marca = reader["Marca"].ToString();
                                        string modelo = reader["Modelo"].ToString();
                                        string serie = reader["Serie"].ToString();

                                        string insertQuery = @"INSERT INTO [dbo].[IT_EquiposBorrados] ([CodigoActivo], [EquipoTipo], [UltimoCustodio], [FechaAdquisicion], [Marca], [Modelo], [Serie], [FechaBaja]) VALUES (@CodigoActivo,@EquipoTipo ,@UltimoCustodio ,@FechaAdquisicion, @Marca, @Modelo, @Serie, @FechaBaja)";
                                        using (SqlCommand commandIn = new SqlCommand(insertQuery, connection))
                                        {
                                            commandIn.Parameters.AddWithValue("@CodigoActivo", nuevoCustodio.CodigoActivo);
                                            commandIn.Parameters.AddWithValue("@EquipoTipo", equipoTipo);
                                            commandIn.Parameters.AddWithValue("@UltimoCustodio", ultimoCustodio);
                                            commandIn.Parameters.AddWithValue("@FechaAdquisicion", fechaAdquisicion);
                                            commandIn.Parameters.AddWithValue("@Marca", marca);
                                            commandIn.Parameters.AddWithValue("@Modelo", modelo);
                                            commandIn.Parameters.AddWithValue("@Serie", serie);
                                            commandIn.Parameters.AddWithValue("@FechaBaja", DateTime.Today.ToString());

                                            int rowsAffected = commandIn.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            //Actualizar la tabla IT_Equipos con la nueva cédula
                            string updateQuery = "UPDATE IT_Equipos SET CedulaCustodio = @CedulaNuevo WHERE CodigoActivo = @CodigoActivo";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@CedulaNuevo", nuevoCustodio.CedulaY);
                                updateCommand.Parameters.AddWithValue("@CodigoActivo", nuevoCustodio.CodigoActivo);
                                updateCommand.ExecuteNonQuery();
                            }

                            //Registro del CustodioAnterior en la tabla IT_CustodiosAnteriores

                            //Esta inserción en la tabla "it_CustodiosAnteriores" se realiza automáticamente dentro de la base de datos mediante un trigger.
                            //Este trigger se activa cuando se actualiza el custodio en la tabla "it_equipos".
                            //Su función es guardar el antiguo custodio antes de la actualización en la tabla "IT_CustodiosAnteriores" para mantener un registro histórico de los cambios.

                        }
                        else
                        {
                            //Actualizar la tabla IT_Equipos con la nueva cédula
                            string updateQuery = "UPDATE IT_Equipos SET CedulaCustodio = @CedulaNuevo WHERE CodigoActivo = @CodigoActivo";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@CedulaNuevo", nuevoCustodio.CedulaY);
                                updateCommand.Parameters.AddWithValue("@CodigoActivo", nuevoCustodio.CodigoActivo);
                                updateCommand.ExecuteNonQuery();
                            }

                            //Registro del CustodioAnterior en la tabla IT_CustodiosAnteriores

                            //Esta inserción en la tabla "it_CustodiosAnteriores" se realiza automáticamente dentro de la base de datos mediante un trigger.
                            //Este trigger se activa cuando se actualiza el custodio en la tabla "it_equipos".
                            //Su función es guardar el antiguo custodio antes de la actualización en la tabla "IT_CustodiosAnteriores" para mantener un registro histórico de los cambios.
                        }

                    }
                    catch (Exception ex)
                    {
                        migracionExitosa = false;
                    }
                }
                connection2.Close();
                connection.Close();

                if (migracionExitosa)
                {
                    // Muestra un mensaje de éxito
                    string successMessage = "Migración realizada correctamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{successMessage}');", true);
                }
                else
                {
                    // Muestra un mensaje de error
                    string errorMessage = $"Ocurrió un error al realizar la Migración";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{errorMessage}');", true);
                }
                btnMigrar.Enabled = false;
            }
            btnCargarMatrizNuevos_Click(null, EventArgs.Empty);
        }
        
    }
}

