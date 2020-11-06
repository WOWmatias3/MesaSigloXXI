using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transbank.Webpay;
using BLL;

namespace MesaRestaurant.WebPay
{
    public partial class Pagosuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
                string tokenWs = Request.Form["token_ws"];

                if (tokenWs == null)
                {
                    Response.Redirect("/VistaMenus/MenuMain");
                }

                var result = transaction.getTransactionResult(tokenWs);
                var output = result.detailOutput[0];
                if (output.responseCode == 0)
                {
                    var urlredirect = result.urlRedirection;
                    string tokenws = tokenWs;
                    int codigo = output.responseCode;
                    var monto = output.amount;
                    var authCode = output.authorizationCode;
                    int boletaid = Int32.Parse(output.buyOrder);

                    BoletaBLL bolBLL = new BoletaBLL();
                    bolBLL.actualizarEstadoBoleta(boletaid, "pagado");

                    HttpCookie myCookie = new HttpCookie("codigo");

                    //Add key-values in the cookie
                    myCookie.Values.Add("responsecode", "0");
                    //set cookie expiry date-time. Made it to last for next 12 hours.
                    myCookie.Expires = DateTime.Now.AddSeconds(25);
                    //Most important, write the cookie to client.
                    Response.Cookies.Add(myCookie);


                    Response.Clear();

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<html>");
                    sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                    sb.AppendFormat("<form name='form' action='{0}' method='post'>", urlredirect);
                    sb.AppendFormat("<input type='hidden' name='token_ws' value='{0}'>", tokenWs);
                    sb.Append("</form>");
                    sb.Append("</body>");
                    sb.Append("</html>");

                    Response.Write(sb.ToString());

                    Response.End();
                }
                else if (output.responseCode != 0)
                {
                    HttpCookie myCookie = new HttpCookie("codigo");
                    //Add key-values in the cookie
                    myCookie.Values.Add("responsecode", "1");
                    //set cookie expiry date-time. Made it to last for next 12 hours.
                    myCookie.Expires = DateTime.Now.AddSeconds(20);
                    //Most important, write the cookie to client.
                    Response.Cookies.Add(myCookie);
                    Response.Redirect("/WebPay/FinalPagado");
                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}