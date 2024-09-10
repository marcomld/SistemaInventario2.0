<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmVisorImagenes.aspx.cs" Inherits="SistemaInventario.FrmVisorImagenes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Visor de Imágenes</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <style>
        /* Estilo general del contenedor principal */
        .container {
            display: flex;
            height: 100vh; /* Ocupa toda la altura de la ventana del navegador */
        }

        /* Estilo para la primera mitad (botones y thumbnails) */
        .left-half {
            flex: 1 1 30%; /* Ocupa aproximadamente el 30% del ancho */
            padding: 20px;
            box-sizing: border-box;
            border-right: 2px solid #ddd; /* Línea divisoria opcional */
        }

        /* Estilo para la segunda mitad (imagen principal y detalles) */
        .right-half {
            flex: 1 1 70%; /* Ocupa aproximadamente el 70% del ancho */
            padding: 20px;
            box-sizing: border-box;
        }

        /* Estilo para los botones de subir y eliminar */
        .btn-upload, .btn-delete {
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            border: none;
            display: block;
        }

        .btn-upload {
            background-color: #007bff;
            color: white;
        }

            .btn-upload:hover {
                background-color: #0056b3;
            }

        .btn-delete {
            background-color: #d9534f;
            color: white;
        }

            .btn-delete:hover {
                background-color: #c9302c;
            }

            .btn-delete:active {
                background-color: #ac2925;
            }

        /* Estilo para los thumbnails */
        .thumbnails {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 10px;
            margin-bottom: 20px;
        }

        .thumbnail {
            border: 1px solid #ddd;
            border-radius: 4px;
            overflow: hidden;
        }

        .thumbnail-image {
            width: 100px;
            height: 100px;
            object-fit: cover;
        }

        /* Estilo para la imagen principal */
        .image-viewer {
            margin-bottom: 20px;
            text-align: center;
        }

        .image-viewer-image {
            max-width: 100%;
            max-height: 500px;
            height: auto;
            object-fit: contain;
        }

        /* Estilo para los detalles de la imagen */
        .lbl-details {
            display: block;
            margin-bottom: 10px;
            padding: 10px;
            background-color: #e0f7fa;
            border: 1px solid #b2ebf2;
            border-radius: 4px;
            text-align: center;
        }

        .file-upload-wrapper {
            position: relative;
            display: inline-block;
        }

            .file-upload-wrapper input[type="file"] {
                position: absolute;
                z-index: 1;
                opacity: 0;
                cursor: pointer;
                width: 100%;
                height: 100%;
            }

        .custom-file-upload {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            font-weight: bold;
            color: white;
            background-color: #007bff; /* Ajusta el color según tus necesidades */
            border: 1px solid #007bff;
            border-radius: 4px;
            cursor: pointer;
        }

            .custom-file-upload:hover {
                background-color: #0056b3;
            }

        .file-upload-input {
            height: 100%;
            width: 100%;
        }

        /* Estilo para el mensaje de no imagen */
        .no-images-message {
            color: #999;
            text-align: center;
            font-size: 14px;
        }

        /* Contenedor para la imagen con capacidad de zoom */
        .zoom {
            position: relative;
            overflow: hidden;
        }

            .zoom img {
                transition: transform 0.25s ease; /* Animación suave para el zoom */
            }
    </style>
    <script type="text/javascript">
        function confirmarEliminar() {
            return confirm("¿Está seguro de que desea eliminar?");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Primera mitad: botones y thumbnails -->
            <div class="left-half">
                <!-- Formulario de subida de imágenes -->
                <div class="upload-form" style="display: flex; align-items: center; gap: 10px;">
                    <label id="fileUploadLabel" for="fileUpload" class="custom-file-upload">Subir Imagen</label>
                    <div class="file-upload-wrapper" style="flex: 1;">
                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="file-upload-input" OnChange="uploadFile()" />
                    </div>
                    <asp:Button ID="btnSubir" runat="server" Text="Subir Imagen" OnClick="btnSubir_Click" CssClass="btn-upload" Style="display: none;" />

                </div>
                <br />
                <!-- Thumbnails -->
                <div class="thumbnails">
                    <asp:Repeater ID="rptThumbnails" runat="server" OnItemCommand="rptThumbnails_ItemCommand">
                        <ItemTemplate>
                            <div class="thumbnail">
                                <asp:ImageButton ID="imgThumbnail" runat="server" ImageUrl='<%# Eval("ThumbnailUrl") %>' CommandArgument='<%# Eval("OriginalUrl") %>' CommandName="SelectImage" CssClass="thumbnail-image" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Segunda mitad: imagen principal y detalles -->
            <div class="right-half">
                <!-- Sección de imagen principal -->
                <div class="image-section">
                    <div class="image-viewer zoom">
                        <div style="display: flex; align-items: flex-start; position: relative; width: 100%;">
                            <asp:Image ID="imgSelected" runat="server" CssClass="image-viewer-image" Style="flex: 1; max-width: 100%; height: auto;" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn-upload" OnClientClick="return confirmarEliminar();"
                                Style="position: absolute; top: 10px; right: 10px; z-index: 10;" />
                        </div>
                    </div>
                    <asp:Label ID="lblImageDetails" runat="server" Text="" CssClass="lbl-details"></asp:Label>

                    <asp:Panel ID="pnlNoImages" runat="server" Visible="false">
                        <asp:Label ID="lblNoImages" runat="server" Text="" CssClass="no-images-message"></asp:Label>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var zoomElement = document.querySelector(".zoom");
            var zoomImg = zoomElement.querySelector("img");
            var scale = 1;
            var isDragging = false;
            var startX, startY, initialX = 0, initialY = 0;

            zoomElement.addEventListener("wheel", function (e) {
                e.preventDefault();
                scale += e.deltaY * -0.01;
                scale = Math.min(Math.max(1, scale), 3); // Limitar el zoom entre 1x y 3x

                // Ajustar la posición y el tamaño de la imagen al hacer zoom
                zoomImg.style.transform = `scale(${scale})`;
            });

            zoomElement.addEventListener("mousedown", function (e) {
                e.preventDefault();
                startX = e.clientX - initialX;
                startY = e.clientY - initialY;
                isDragging = true;

                function onMouseMove(e) {
                    if (!isDragging) return;
                    initialX = e.clientX - startX;
                    initialY = e.clientY - startY;
                    zoomElement.style.transform = `translate(${initialX}px, ${initialY}px) scale(${scale})`;
                }

                function onMouseUp() {
                    isDragging = false;
                    document.removeEventListener("mousemove", onMouseMove);
                    document.removeEventListener("mouseup", onMouseUp);
                }

                document.addEventListener("mousemove", onMouseMove);
                document.addEventListener("mouseup", onMouseUp);
            });

            // Ajustar posición inicial del contenedor al hacer zoom
            zoomElement.addEventListener("transitionend", function () {
                if (scale === 1) {
                    zoomElement.style.transform = `translate(0, 0) scale(1)`;
                }
            });
        });
    </script>
    <script type="text/javascript">
        function uploadFile() {
            // Simula el clic en el botón de subir imagen
            document.getElementById('<%= btnSubir.ClientID %>').click();
        }
    </script>
</body>
</html>
