using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public class DatosConsulta
    {
        public string CodigoActivo { get; set; }
        public string Tipo { get; set; }
        public string SubTipo { get; set; }
        public string Clase { get; set; }
        public string Origen { get; set; }
    }
    public partial class FrmYupak : System.Web.UI.Page
    {
        // Cadena de conexión para la base de datos BDDYupak
        static string connectionStringBDDYupak = ConfigurationManager.ConnectionStrings["BDDYupakConnectionString"].ConnectionString;

        // Declarar para almacenar los datos
        static DatosConsulta datosConsulta = new DatosConsulta();

        protected void Page_Load(object sender, EventArgs e)
        {
            datosConsulta.CodigoActivo = Request.QueryString["CodigoActivo"];
            CargarDatos();
        }

        protected void CargarDatos()
        {
            try
            {
                if (Regex.IsMatch(datosConsulta.CodigoActivo, @"^\d+$"))
                {
                    string selectQueryYP = "SELECT Tipo, SubTipo, Clase, Origen FROM YP_GAF_ASSETS WHERE Codigo = @codigoActivo ORDER BY Tipo";
                    using (SqlConnection connection = new SqlConnection(connectionStringBDDYupak))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(selectQueryYP, connection))
                        {
                            command.Parameters.AddWithValue("@codigoActivo", datosConsulta.CodigoActivo);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        datosConsulta.Tipo = reader["Tipo"].ToString();
                                        datosConsulta.SubTipo = reader["SubTipo"].ToString();
                                        datosConsulta.Clase = reader["Clase"].ToString();
                                        datosConsulta.Origen = reader["Origen"].ToString();
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

                    if (Regex.IsMatch(datosConsulta.CodigoActivo, @"^\d+$"))
                    {
                        string codigoActivo = datosConsulta.CodigoActivo;
                        string tipo = datosConsulta.Tipo;
                        string subtipo = datosConsulta.SubTipo;
                        string clase = datosConsulta.Clase;
                        string origen = datosConsulta.Origen;

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
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    // Crear la columna de campos
                                    TableRow headerRow = new TableRow();
                                    TableCell cellCampoHeader = new TableCell();
                                    cellCampoHeader.BorderWidth = Unit.Pixel(1);
                                    cellCampoHeader.Text = "Campo";
                                    cellCampoHeader.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
                                    cellCampoHeader.ForeColor = System.Drawing.Color.White;
                                    cellCampoHeader.HorizontalAlign = HorizontalAlign.Center;
                                    headerRow.Cells.Add(cellCampoHeader);

                                    TableCell cellDatoHeader = new TableCell();
                                    cellDatoHeader.BorderWidth = Unit.Pixel(1);
                                    cellDatoHeader.Text = "Dato";
                                    //cellDatoHeader.BackColor = System.Drawing.Color.LimeGreen;
                                    cellDatoHeader.BackColor = System.Drawing.Color.FromArgb(0, 0, 139); // RGB para #00008B es (0, 0, 139)
                                    cellDatoHeader.ForeColor = System.Drawing.Color.White;
                                    cellDatoHeader.HorizontalAlign = HorizontalAlign.Center;
                                    headerRow.Cells.Add(cellDatoHeader);

                                    TablaDatos.Rows.Add(headerRow);

                                    // Crear columna de datos
                                    string[] campos = { "Código del Activo","Fecha de Nacimiento", "Tipo de Activo", "SubTipo de Activo", "Clase de Activo", "Descripcion", "Observaciones", "Estructura del Bien", "Fecha Adquisicion", "Valor", "Valor en Libros", "Vida Util", "% de Depreciacion", "Dimensiones", "Marca", "Modelo", "Numero de Serie", "Fecha de Garantia", "Custodio", "Nombre", "Numero de Componentes", "Tipo de Transaccion", "DepreCH" };

                                    foreach (string campo in campos)
                                    {
                                        TableRow dataRow = new TableRow();
                                        if (campo.Equals("Fecha de Nacimiento"))
                                        {
                                            TableCell cellCampo = new TableCell();
                                            cellCampo.BorderWidth = Unit.Pixel(1);
                                            cellCampo.Text = "Fecha_TomaFisica";
                                            //cellCampo.HorizontalAlign = HorizontalAlign.Center;
                                            dataRow.Cells.Add(cellCampo);
                                        }
                                        else
                                        {
                                            TableCell cellCampo = new TableCell();
                                            cellCampo.BorderWidth = Unit.Pixel(1);
                                            cellCampo.Text = campo;
                                            //cellCampo.HorizontalAlign = HorizontalAlign.Center;
                                            dataRow.Cells.Add(cellCampo);

                                        }

                                        TableCell cellDato = new TableCell();
                                        cellDato.BorderWidth = Unit.Pixel(1);
                                        // Verificar si el valor es una fecha
                                        if (reader[campo] is DateTime)
                                        {
                                            // Obtener solo la parte de la fecha en formato corto (MM/dd/yyyy)
                                            DateTime fecha = Convert.ToDateTime(reader[campo]);
                                            cellDato.Text = fecha.ToShortDateString();
                                        }
                                        else
                                        {
                                            // Dejar el valor como está si no es una fecha
                                            cellDato.Text = reader[campo].ToString();
                                        }
                                        //cellDato.Text = reader[campo].ToString();
                                        //cellDato.HorizontalAlign = HorizontalAlign.Center;
                                        dataRow.Cells.Add(cellDato);

                                        TablaDatos.Rows.Add(dataRow);
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
        }
    }
}