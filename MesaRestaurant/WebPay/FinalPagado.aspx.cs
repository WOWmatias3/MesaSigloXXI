using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MesaRestaurant.WebPay
{
    public partial class FinalPagado : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["codigo"] != null)
            {

                // AGREGAR POP UP DE ALERTA Y REDIRECCION
                HttpCookie myCookie = Request.Cookies["codigo"];
                string responsecode = myCookie.Values["responsecode"];
                if (responsecode != "0")
                {
                    Label1.Text = "La transaccion no se a realizado correctamente";
                    Label2.Text = "Sera Redirigido a la pagina automaticamente en 10 segundos para reintentar";
                    Label3.Text = "";

                    //
                    Response.AddHeader("REFRESH", "10;URL=/CarroCompras.aspx");
                }

                else
                {
                    Session["responsecode"] = null;
                    Label1.Text = "La transaccion se ha realizado correctamente!";
                    Label2.Text = "¡Gracias por su Presencia!";
                    Label3.Text = "¡Esperamos que vuelva!";
                    Response.AddHeader("REFRESH", "15;URL=/VistaMenus/MenuMain.aspx");

                }
            }
            else
            {
                Response.Redirect("/VistaMenus/MenuMain");
            }
        }
    }
}