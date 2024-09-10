<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="SistemaInventario.FormLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
    
    <style>
        /* Cambia el color del texto de la barra de navegación a blanco */
        .navbar-light .navbar-nav .nav-link {
            color: #fff; /* Blanco */
        }
        
        .log {
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f2f2f2;
        }

        .login-card {
            background-color: rgba(0, 128, 0, 0.2); /* Color verde con opacidad */
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1); /* Sombra ligera */
            text-align: center;
        }

        .login-card h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .login-card input[type="text"],
        .login-card input[type="password"],
        .login-card button {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }

        .login-card .error-message {
            color: red;
            text-align: center;
            margin-top: 10px;
        }

        .login-card-button {
           background-color: #008000; /* Color verde */
           color: white;
           font-weight: bold;
           border: none; /* Para eliminar el borde del botón */
           border-radius: 5px; /* Ajusta el radio de borde según sea necesario */
           cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
           transition: background-color 0.3s; /* Efecto de transición suave */
        }

        .login-card-button:hover {
            background-color: #006400; /* Color verde más oscuro al pasar el ratón */
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light" style="background-color: #ff0000;">
            <div class="container">
                <a class="navbar-brand" href="#">
                    <img src="<%= ResolveUrl("~/Img/logo.png") %>" width="250" height="45" class="d-inline-block align-top" alt=""/>
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

        <div class="log">
            <div class="login-card">
                <div>
                    <h2>Iniciar Sesión</h2>
                    <div>
                        <label for="txtUsuario">Usuario:</label>
                        <input type="text" id="txtUsuario" runat="server" />
                    </div>
                    <div>
                        <label for="txtClave">Contraseña:</label>
                        <input type="password" id="txtClave"  runat="server"/>
                    </div>
                    <div>
                        <asp:Button ID="btnIniciarSesion" runat="server" OnClick="Button1_Click" Text="Iniciar Sesión"  CssClass="login-card-button" Height="45px" Width="211px"/>
                        
                    </div>
                    <div class="error-message" runat="server" id="lblMensaje"></div>
                </div>
            </div>
        </div>

        

    </form>
</body>
</html>
