using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;


namespace MesaRestaurant.WebPay
{
    public class defController : Controller
    {
        // GET: def
        public ActionResult pago()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            var monto = 3500;
            var ordenid = "123";
            var sessionid = "idsession";

            string returnurl = "http://localhost:52091/Webpay/return";
            string finalurl = "http://localhost:52091/webpay/final";

            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);

            var initresult = transaction.initTransaction(monto, ordenid, sessionid, returnurl, finalurl);

            var tokenWs = initresult.token;
            var formaction = initresult.url;
            ViewBag.Amount = monto;
            ViewBag.BuyOrder = ordenid;
            ViewBag.TokenWs = tokenWs;
            ViewBag.FormAction = formaction;

            return View();
        }

        public ActionResult Return()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];

            var result = transaction.getTransactionResult(tokenWs);
            var output = result.detailOutput[0];
            if(output.responseCode == 0)
            {
                ViewBag.URLredirect = result.urlRedirection;
                ViewBag.token = tokenWs;
                ViewBag.ResponseCode = output.responseCode;
                ViewBag.Amount = output.amount;
                ViewBag.AuthCode = output.authorizationCode;

            }

            return View();
        }
    }
}