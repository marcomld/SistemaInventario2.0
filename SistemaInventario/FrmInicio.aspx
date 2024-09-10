<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmInicio.aspx.cs" Inherits="SistemaInventario.FormInicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página de Inicio</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
    <style>
        /* Cambia el color del texto de la barra de navegación a blanco */
        .navbar-light .navbar-nav .nav-link {
            color: #fff; /* Blanco */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Encabezado -->
        <nav class="navbar navbar-expand-lg navbar-light" style="background-color: #ff0000;">
            <div class="container">
                <a class="navbar-brand" href="https://www.antonioante.gob.ec/AntonioAnte/">
                    <img src="<%= ResolveUrl("~/Img/logo.png") %>" width="250" height="45" class="d-inline-block align-top" alt="" href="https://www.antonioante.gob.ec/AntonioAnte/">
                </a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="FrmLogin.aspx">Iniciar Sesión</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Contenido Principal -->
        <div class="container mt-4">
            <div class="row">
                <div class="col-md-6">
                    <h1>Bienvenido a Nuestra Aplicación</h1>
                    <p>Registra aquí tu incidencia.</p>
                </div>
                <div class="col-md-6">
                    <img src="<%= ResolveUrl("~/Img/AA_ADM2327.jpg") %>" class="img-fluid" alt="Imagen Principal">
                </div>
            </div>
        </div>

    </form>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
