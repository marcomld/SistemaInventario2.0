<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmMigrarCustodios.aspx.cs" Inherits="SistemaInventario.FrmMigrarCustodios" Async="true" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Migrar Custodios</title>
    <style>
        /* Estilos del overlay de carga */
        .loading-overlay {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 1000;
            text-align: center;
        }

        .loading-container {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            text-align: center;
            color: white;
            animation: fadeIn 0.3s ease-out;
        }

        .loading-circle {
            border: 8px solid #f3f3f3;
            border-top: 8px solid #3498db;
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 2s linear infinite;
            margin: 0 auto;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        @keyframes fadeIn {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        .loading-text {
            margin-top: 10px;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }

        .loading-message {
            font-size: 16px;
            color: #ddd;
            margin-top: 20px;
            animation: fadeIn 0.5s ease-out;
        }


    </style>


    <script>
        function showLoading() {
            document.getElementById("loadingOverlay").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loadingOverlay").style.display = "none";
        }

        function confirmarMigracio() {
            return confirm("¿Está seguro de que desea realizar la migración?");
        }

        function showLoadingMessage() {
            document.getElementById("loadingMessage").style.display = "block";
        }

        function hideLoadingMessage() {
            document.getElementById("loadingMessage").style.display = "none";
        }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        .button-margin {
            width: 100%; /* Ajusta el ancho del botón al 100% del ancho de la celda */
        }

        td {
            padding: 5px; /* Crea un espaciado entre botnes */
        }

        .legend-container {
            position: fixed;
            top: 100px; /* Ajusta la distancia desde la parte superior */
            right: 20px; /* Ajusta la distancia desde la derecha */
        }

        .legend {
            background-color: #fff;
            padding: 3px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
            /*font-weight: bold;*/ /* Texto en negrita */
        }
    </style>

    <div id="loadingOverlay" class="loading-overlay">
        <div class="loading-container">
            <div class="loading-circle"></div>
            <div class="loading-text">Cargando...</div>
            <div id="loadingMessage" class="loading-message">La carga está en curso. Por favor, espere...</div>
        </div>
    </div>

    <div class="legend-container" id="legendContainer" runat="server">
        <div class="legend">
            <table>
                <tr>
                    <td style="background-color: coral;"></td>
                    <td>Nuevo Custodio</td>
                </tr>
            </table>
        </div>
    </div>

    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Text="Cargar Datos" CssClass="btnEquipo" OnClientClick="showLoading(); showLoadingMessage();" />
                </td>
                <td>
                    <asp:Button ID="btnCargarMatrizNuevos" runat="server" Text="Nuevos Custodios" OnClick="btnCargarMatrizNuevos_Click" CssClass="btnEquipo" />
                </td>
                <td>                    
                    <asp:Button ID="btnMigrar" runat="server" Text="Migrar Custodios" OnClick="btnMigrar_Click" CssClass="btnEquipo" OnClientClick="return confirmarMigracio()" />
                </td>
            </tr>
        </table>
    </div>

    <asp:Table ID="Tabla" runat="server" Width="100%">
    </asp:Table>

</asp:Content>
