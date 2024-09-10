<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmMantenimiento.aspx.cs" Inherits="SistemaInventario.FrmMantenimiento" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .responsive-image {
            width: 100%;
            height: auto;
            max-width: 100%;
        }
    </style>

    <div style="text-align: center; margin-top: 0px;">
	<img src="<%= ResolveUrl("~/Img/construccion.png") %>" alt="En Construcción" class="responsive-image" />
    </div>

</asp:Content>
