using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaInventario
{
    public partial class Ejemplo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (!string.IsNullOrEmpty(eventTarget) && eventTarget == UpdatePanel1.ClientID)
                {
                    string[] args = eventArgument.Split(':');
                    if (args.Length == 2)
                    {
                        string action = args[0];
                        string productId = args[1];

                        if (action == "SingleClick")
                        {
                            HandleSingleClick(productId);
                        }
                        else if (action == "DoubleClick")
                        {
                            HandleDoubleClick(productId);
                        }
                    }
                }
            }
        }

        private void HandleSingleClick(string productId)
        {
            // Lógica para manejar un solo clic
            lblMessage.Text = "Producto seleccionado con un solo clic: " + productId;
        }

        private void HandleDoubleClick(string productId)
        {
            // Lógica para manejar doble clic
            lblMessage.Text = "Producto seleccionado con doble clic: " + productId;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string productId = DataBinder.Eval(e.Row.DataItem, "IDToner").ToString();
                e.Row.Attributes["onclick"] = $"rowClick(this, '{productId}');";
                e.Row.Attributes["ondblclick"] = $"rowDoubleClick(this, '{productId}');";
                e.Row.Style["cursor"] = "pointer"; // Cambiar el cursor a pointer para indicar que la fila es seleccionable
            }
        }
    }

}
