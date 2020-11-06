using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transbank.Webpay;

namespace MesaRestaurant
{
    public partial class PagoWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            var monto = 3500;
            var ordenid = "123";
            var sessionid = "idsession";

            string returnurl = "http://localhost:5656/webpay/return";
            string finalurl = "http://localhost:5656/webpay/final";

            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);

            var initresult = transaction.initTransaction(monto, ordenid, sessionid, returnurl, finalurl);

            var tokenWs = initresult.token;
            var formaction = initresult.url;


            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", formaction);
            sb.AppendFormat("<input type='hidden' name='token_ws' value='{0}'>", tokenWs);
            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();

        }
    }
}