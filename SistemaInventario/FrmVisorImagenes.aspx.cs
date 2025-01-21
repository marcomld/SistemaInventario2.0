using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public partial class FrmVisorImagenes : System.Web.UI.Page
    {
        static string connectionStringBDDSistemas = ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

        static string codigoActivo;
        //public static int CodigoActivo { get; set; }
        static int tamañoMAx = 5 * 1024 * 1024; // 5 MB en bytes


        public static string EquipoTipo { get; set; }
        public static string CedulaCustodio { get; set; }
        public static string Marca { get; set; }
        public static string Modelo { get; set; }
        public static string Serie { get; set; }
        public static string Ubicacion { get; set; }
        public static string NombreArchivo { get; set; }


        private void AsegurarDirectoriosExisten()
        {
            string[] directorios = {
                Server.MapPath("~/ImagenesDeLosEquipos/" + codigoActivo),
                Server.MapPath("~/ImagenesDeLosEquipos/Thumbnail")
            };

            foreach (string directorio in directorios)
            {
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            codigoActivo = Request.QueryString["codigoActivo"];

            //CodigoActivo = int.TryParse(codigoActivo, out var tempCodigoActivo) ? tempCodigoActivo : 0;

            if (!IsPostBack)
            {
                // Establecer la cadena de conexión
                using (SqlConnection connection = new SqlConnection(connectionStringBDDSistemas))
                {
                    connection.Open();

                    // Consulta para obtener el registro correspondiente al código activo
                    string query = @"
                SELECT [EquipoTipo], [CedulaCustodio], [Marca], [Modelo], [Serie], [Ubicacion]
                FROM [IT_Equipos]
                WHERE [CodigoActivo] = @CodigoActivo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CodigoActivo", codigoActivo);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Asignar valores a las variables estáticas
                                EquipoTipo = reader["EquipoTipo"].ToString();
                                CedulaCustodio = reader["CedulaCustodio"].ToString();
                                Marca = reader["Marca"].ToString();
                                Modelo = reader["Modelo"].ToString();
                                Serie = reader["Serie"].ToString();
                                Ubicacion = reader["Ubicacion"].ToString();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('No se encontró el equipo con el código activo proporcionado.');", true);
                            }
                        }
                    }
                }

                AsegurarDirectoriosExisten();
                CargarImagenes();
            }
        }

        private void CargarImagenes()
        {
            string[] imagenes = Directory.GetFiles(Server.MapPath("~/ImagenesDeLosEquipos/" + codigoActivo));
            var listaImagenes = new List<object>();

            foreach (string rutaImagen in imagenes)
            {
                string nombreImagen = Path.GetFileName(rutaImagen);
                string rutaThumbnail = "~/ImagenesDeLosEquipos/Thumbnail/" + nombreImagen;
                string rutaImagenRedimensionada = Server.MapPath(rutaThumbnail);

                // Redimensionar la imagen y guardarla como miniatura
                RedimensionarImagen(rutaImagen, rutaImagenRedimensionada, 100, 100); // Establece el tamaño deseado para las miniaturas

                listaImagenes.Add(new { ThumbnailUrl = rutaThumbnail, OriginalUrl = "~/ImagenesDeLosEquipos/" + codigoActivo + "/" + nombreImagen });
            }

            rptThumbnails.DataSource = listaImagenes;
            rptThumbnails.DataBind();

            if (listaImagenes.Count > 0)
            {
                // Mostrar la primera imagen por defecto
                MostrarImagen(((dynamic)listaImagenes[0]).OriginalUrl);
                pnlNoImages.Visible = false; // Ocultar el mensaje de no hay imágenes
            }
            else
            {
                // No hay imágenes, mostrar mensaje
                pnlNoImages.Visible = true;
                lblNoImages.Text = "No hay imágenes disponibles.";
            }
        }

        private void MostrarImagen(string urlImagen)
        {
            imgSelected.ImageUrl = urlImagen;
            FileInfo infoArchivo = new FileInfo(Server.MapPath(urlImagen));
            lblImageDetails.Text = $"Nombre: {infoArchivo.Name}<br />Tamaño: {infoArchivo.Length / 1024} KB<br />Fecha de Captura: {ObtenerFechaCaptura(infoArchivo.FullName)}";
        }

        protected void rptThumbnails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectImage")
            {

                MostrarImagen(e.CommandArgument.ToString());
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Extraer el nombre del archivo desde la URL de la imagen
            string nombreArchivo = Path.GetFileName(imgSelected.ImageUrl);
            string rutaImagen = Server.MapPath("~/ImagenesDeLosEquipos/" + codigoActivo + "/" + nombreArchivo);
            string rutaThumbnail = Server.MapPath("~/ImagenesDeLosEquipos/Thumbnail/" + nombreArchivo);

            // Eliminar la imagen principal
            if (File.Exists(rutaImagen))
            {
                File.Delete(rutaImagen);
            }

            // Eliminar la miniatura
            if (File.Exists(rutaThumbnail))
            {
                File.Delete(rutaThumbnail);
            }

            // LOG
            Logger logger = new Logger();
            string usuario = Session["Usuario"] as string;
            string detalle = "Eliminó la foto de nombre: " + nombreArchivo + " del equipo con los siguientes parámetros: " +
                "Código Activo: " + codigoActivo + "  Equipo Tipo: " + EquipoTipo +
                "  Cédula Custodio: " + CedulaCustodio + "  Marca: " + Marca +
                "  Modelo: " + Modelo + "  Serie: " + Serie + "  Ubicación: " + Ubicacion;

            // Registrar la acción en el log
            logger.RegistrarLog(usuario, "Eliminar", "IT_Equipos", detalle, codigoActivo);

            // Lógica para determinar si ya no quedan más imágenes
            if (rptThumbnails.Items.Count == 1) // Si este es el último elemento que se va a eliminar
            {
                ActualizarCampoImagenes(codigoActivo, false); // Actualiza el campo imagenes a false
            }

            lblImageDetails.Text = "";
            imgSelected.ImageUrl = "";
            CargarImagenes();
        }


        // Método para actualizar el campo 'Imagenes' en la tabla 'IT_Equipos'
        public void ActualizarCampoImagenes(string codigoActivo, bool hayImagenes)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDDSistemasConnectionString"].ConnectionString;

            string updateQuery = "UPDATE it_equipos SET imagenes = @HayImagenes WHERE codigoActivo = @CodigoActivo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@HayImagenes", hayImagenes);
                command.Parameters.AddWithValue("@CodigoActivo", codigoActivo);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se actualizó al menos una fila, registrar la acción
                    if (rowsAffected > 0)
                    {
                        // Crear una instancia de Logger
                        Logger logger = new Logger();
                        string usuario = Session["Usuario"] as string;
                        string detale = "Agrego una nueva foto de nombre: " + NombreArchivo + " al equipo que tiene los siguiente parametros: " +
                           "Código Activo: " + codigoActivo + "  Equipo Tipo: " + EquipoTipo + "  Cedula Custodio: " + CedulaCustodio + "  Marca : " + Marca + "  Modelo : " + Modelo + "  Serie : " + Serie + "  Ubicacion : " + Ubicacion;

                        // Registrar la acción en el log
                        logger.RegistrarLog(usuario, "Actualizar", "IT_Equipos", detale, codigoActivo);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, puedes loguear el error o lanzar una excepción hacia arriba si es necesario
                    Console.WriteLine("Error al actualizar campo imagenes: " + ex.Message);
                }
            }
        }


        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                // Verificar el tamaño de la imagen
                if (fileUpload.PostedFile.ContentLength > tamañoMAx)
                {
                    // Mostrar mensaje de error en una alerta
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El tamaño de la imagen no debe exceder los 5 MB.');", true);
                    return;
                }

                string nombreArchivo = Path.GetFileName(fileUpload.PostedFile.FileName).ToString();
                string rutaArchivo = Server.MapPath("~/ImagenesDeLosEquipos/" + codigoActivo + "/") + nombreArchivo;

                NombreArchivo = nombreArchivo;

                fileUpload.SaveAs(rutaArchivo);

                // Actualizar el campo imagenes a true en la base de datos
                ActualizarCampoImagenes(codigoActivo, true);

                CargarImagenes();
            }
        }

        private string ObtenerFechaCaptura(string ruta)
        {
            using (var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (var miImagen = System.Drawing.Image.FromStream(fs, false, false))
            {
                try
                {
                    PropertyItem propItem = miImagen.GetPropertyItem(36867);
                    string fechaCaptura = new string(System.Text.Encoding.ASCII.GetChars(propItem.Value));
                    return DateTime.ParseExact(fechaCaptura, "yyyy:MM:dd HH:mm:ss\0", null).ToString();
                }
                catch (Exception)
                {
                    return "No disponible";
                }
            }
        }

        private void RedimensionarImagen(string rutaImagenOriginal, string rutaImagenRedimensionada, int anchoMax, int altoMax)
        {
            using (var imagenOriginal = System.Drawing.Image.FromFile(rutaImagenOriginal))
            {
                int nuevoAncho = imagenOriginal.Width;
                int nuevoAlto = imagenOriginal.Height;

                if (nuevoAncho > anchoMax)
                {
                    nuevoAlto = (int)((double)nuevoAlto * anchoMax / nuevoAncho);
                    nuevoAncho = anchoMax;
                }
                if (nuevoAlto > altoMax)
                {
                    nuevoAncho = (int)((double)nuevoAncho * altoMax / nuevoAlto);
                    nuevoAlto = altoMax;
                }

                using (var imagenRedimensionada = new Bitmap(nuevoAncho, nuevoAlto))
                using (var graficos = Graphics.FromImage(imagenRedimensionada))
                {
                    graficos.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graficos.DrawImage(imagenOriginal, 0, 0, nuevoAncho, nuevoAlto);
                    imagenRedimensionada.Save(rutaImagenRedimensionada, imagenOriginal.RawFormat);
                }
            }
        }
    }
}