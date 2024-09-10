<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmDefault.aspx.cs" Inherits="SistemaInventario.FrmDefault" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <style>
        body, html {
            height: 100%;
            margin: 0;
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }

        .navbar {
            margin-bottom: 20px; /* Ajusta el espacio debajo del navbar */
        }

        .navbar-brand img {
            width: 250px;
            height: 45px;
        }

        .container-fluid {
            display: flex;
            justify-content: center; /* Centra el contenedor principal horizontalmente */
            padding-top: 20px; /* Espacio entre el navbar y el contenido */
        }

        .content-wrapper {
            display: flex;
            width: 100%;
            max-width: 1200px; /* Ajusta el ancho máximo del contenedor si es necesario */
            gap: 20px; /* Espacio entre los paneles */
        }

        .left-panel, .right-panel {
            flex: 1;
            display: flex;
            justify-content: center; /* Centra horizontalmente el contenido en cada panel */
            align-items: center; /* Centra verticalmente el contenido en cada panel */
            padding: 20px;
            border-radius: 8px;
            background-color: #ffffff; /* Color de fondo blanco para los paneles */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Sombra sutil para los paneles */
        }

        .right-panel {
            flex-direction: column;
        }

            .right-panel > * {
                margin-bottom: 15px; /* Espaciado entre el TextBox y el Button */
            }

            .right-panel h5 {
                margin-bottom: 15px;
            }

        .navbar {
            background-color: #ff0000;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
        }

            .btn-primary:hover {
                background-color: #0056b3;
            }

        .table td, .table th {
            text-align: center; /* Centra el texto en las celdas de la tabla */
        }

        .form-control::placeholder {
            color: #6c757d; /* Color del texto del placeholder */
            opacity: 1; /* Asegura que el color sea visible */
        }

        .auto-style1 {
            display: block;
            font-size: 1rem;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light">
            <div class="container">
                <a class="navbar-brand" href="#">
                    <img src="<%= ResolveUrl("~/Img/logo.png") %>" alt="Logo" />
                </a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
            </div>
        </nav>

        <div class="container-fluid">
            <div class="content-wrapper">
                <!-- Panel Izquierdo -->
                <div class="left-panel">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceUsuarios" CssClass="table table-striped" Width="316px">
                        <Columns>
                            <asp:BoundField DataField="Usuario" HeaderText="Usuarios" SortExpression="Usuario" />
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Panel Derecho -->
                <div class="right-panel">
                    <h5>Código De Acceso</h5>
                    <asp:TextBox ID="TextCodigo" runat="server" CssClass="form-control" Placeholder="Digite su código" Width="219px" />
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                </div>
            </div>
        </div>

        <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:BDDSistemasConnectionString %>" SelectCommand="SELECT [Usuario] FROM [IT_Usuarios]"></asp:SqlDataSource>
    </form>
</body>
</html>
