<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmMigrarToners.aspx.cs" Inherits="SistemaInventario.FrmMigrarToners" %>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Migrar Toners</title>
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

    <script>
        function confirmarActualizacion() {
            return confirm("¿Está seguro de que desea actualizar el stock?");
        }
        function confirmarMigracio() {
            return confirm("¿Está seguro de que desea realizar la migración?");
        }
    </script>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnNuevosToners" runat="server" Text="Nuevos Toners" CssClass="button-margin" OnClick="btnNuevosToners_Click" />
                </td>
                <td>                    
                    <asp:Button ID="btnMigrar" runat="server" Text="Migrar Toners" CssClass="button-margin" OnClick="btnMigrar_Click" OnClientClick="return confirmarMigracio()" />
                </td>
                 <td>
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Stock" CssClass="button-margin" OnClick="btnActualizar_Click" OnClientClick="return confirmarActualizacion();" />                    
                </td>
            </tr>
        </table>
    </div>
    <div class="legend-container" id="legendContainer" runat="server" >
        <div class="legend">
            <table>
                <tr>
                    <td style="background-color: lightgreen;"></td>
                    <td>Toners nuevos</td>
                    <td></td>
                    <td style="background-color: #ffce43;"></td>
                    <td>Stock no coincide</td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Table ID="Tabla" runat="server" Width="100%">
    </asp:Table>
</asp:Content>
