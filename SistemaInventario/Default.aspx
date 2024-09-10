<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="SistemaInventario.FrmIniciarSesion" %>

<asp:Content ID="ContenidoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Estilo para el fondo de toda la página */
        body {
            background-color: #f0f4f8; /* Gris muy claro */
            margin: 0;
            padding: 0;
            font-family: 'Arial', sans-serif;
        }

        /* Estilos para centrar el panel del login */
        .login-container {
            width: 100%;
            max-width: 400px;
            margin: 0 auto;
            margin-top: 10%;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #ffffff; /* Color de fondo del panel de login */
            border-radius: 8px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            animation: fadeIn 1s ease-out;
        }

        /* Estilos de tipografía y botones */
        h2 {
            font-family: 'Nunito', sans-serif;
            font-weight: 600;
            text-align: center;
            color: #131d69;
            width: 100%;
            margin-bottom: 10px;
        }

        p {
            font-family: 'Nunito', sans-serif;
            color: #666666;
            margin-bottom: 20px;
            text-align: center;
        }

        .form-group {
            width: 100%;
            margin-bottom: 15px;
            text-align:center;
        }

        .form-group label {
            font-family: 'Nunito', sans-serif;
            font-weight: 600;
            color: #555555;
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            font-family: 'Open Sans', sans-serif;
            padding: 10px;
            border-radius: 4px;
            border: 1px solid #dddddd;
            width: 100%;
            box-sizing: border-box;
        }

        .btn-primary {
            width: 100%;
            font-family: 'Poppins', sans-serif;
            background-color: #0044cc; /* Color de botón coherente con la página de inicio */
            border: none;
            padding: 10px;
            border-radius: 4px;
            font-weight: 600;
            color: #ffffff;
            transition: background-color 0.3s ease;
        }

        .btn-primary:hover {
            background-color: #0033aa;
        }

        .text-danger {
            color: #dc3545;
            font-family: 'Nunito', sans-serif;
            margin-top: 10px;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>

    <div class="login-container">
        <h2>Inicia sesión en tu cuenta</h2>
        <p>Ingresa tu código de acceso</p>

        <asp:Panel ID="pnlInicioSesion" runat="server">
            <div class="form-group">
                <!-- Otros elementos, si es necesario -->
            </div>
            <div class="form-group">
                <asp:Label ID="lblContraseña" runat="server" Text="Código de acceso:" />
                <asp:TextBox ID="txtCodigo" runat="server" TextMode="Password" CssClass="form-control" onkeypress="handleEnterKey(event)"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-danger"></asp:Label>
        <div class="form-group">
            <asp:Button ID="btnIngresar" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
        </div>
    </div>
    <script type="text/javascript">
        function handleEnterKey(event) {
            if (event.key === 'Enter') {
                event.preventDefault(); // Previene el comportamiento por defecto de Enter
                document.getElementById('<%= btnIngresar.ClientID %>').click(); // Simula el clic en el botón
            }
        }
    </script>
</asp:Content>
