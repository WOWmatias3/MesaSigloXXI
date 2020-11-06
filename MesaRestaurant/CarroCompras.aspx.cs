using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transbank.Webpay;
using BLL;


namespace MesaRestaurant
{
    public partial class CarroCompras : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                asignanumBoleta();
                cargarcarrito();
                cargarDetalle();

            }

        }

        public void asignanumBoleta()
        {
            string nummesa;
            HttpCookie myCookie = Request.Cookies["mesa"];
            if (myCookie == null)
            {
                Response.Redirect("/");
            }
            if (!string.IsNullOrEmpty(myCookie.Values["num_mesa"]))
            {
                nummesa = myCookie.Values["num_mesa"].ToString();
                //Yes userId is found. Mission accomplished.
                ComunGUI common = new ComunGUI();
                bool estadoasignacion = common.verificaAsignacion(int.Parse(nummesa));
                if (!estadoasignacion)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('No se Ha asignado ningun Cliente a esta mesa, Favor de acercarce a recepcion');window.location ='/VistaMenus/MenuMain.aspx';", true);

                }
                else
                {
                    Session["numero_boleta"] = common.get_numbol(int.Parse(nummesa));
                }
            }
        }
        public void cargarDetalle()
        {
            OrdenBLL ordBLL = new OrdenBLL();
            if (Session["numero_boleta"] != null)
            {
                int numeroBoleta = Int32.Parse( Session["numero_boleta"].ToString());
                DataTable dtTOTAL = ordBLL.GetDetalleByBoleta(numeroBoleta);
                if (dtTOTAL.Rows.Count > 0)
                {
                    GridView2.DataSource = ordBLL.GetDetalleByBoleta(numeroBoleta);
                    GridView2.DataBind();
                    Button5.Visible = true;
                    
                }
                else
                {
                    Label2.Visible = false;
                    Label3.Visible = false;
                    RadioButton1.Visible = false;
                    RadioButton2.Visible = false;
                    Label4.Visible = false;
                }
                GetTotal(numeroBoleta);

            }

        }

        public void cargarcarrito()
        {

            GridView1.DataSource = Session["pedido"];
            GridView1.DataBind();
            if (Session["pedido"] == null || ((DataTable)Session["pedido"]).Rows.Count == 0)
            {
                Label1.Text = "No ha agregado ningun item Aun, favor dirigase al menú";
                Button1.Visible = false;
                Button5.Visible = false;
            }
            //Button1_Click(Button1, null);
        }

        protected void ActualizarCarro()
        {
            int i;
            double total = 0, precio, subtotal = 0;
            string cod, nombre;
            int cantidad;

            var items = (DataTable)Session["pedido"];
            //DataRow fila = items.NewRow();
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                cod = (GridView1.Rows[i].Cells[1].Text);
                nombre = (GridView1.Rows[i].Cells[2].Text);
                precio = System.Convert.ToDouble(GridView1.Rows[i].Cells[3].Text);
                cantidad = System.Convert.ToInt16(((TextBox)this.GridView1.Rows[i].Cells[4].FindControl("TextBox1")).Text);
                double prec1 = System.Convert.ToDouble(precio);
                subtotal = precio * cantidad;
                GridView1.Rows[i].Cells[5].Text = subtotal.ToString();
                foreach (DataRow dr in items.Rows)
                {
                    if (dr["codproducto"].ToString() == cod.ToString())
                    {
                        dr["canproducto"] = cantidad;
                        dr["subtotal"] = subtotal;
                    }
                }

                total = total + subtotal;
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable ocar = new DataTable();
                ocar = (DataTable)Session["pedido"];
                ocar.Rows[index].Delete();
                //lblTotal.Text = TotalCarrito(ocar).ToString();
                GridView1.DataSource = ocar;
                GridView1.DataBind();
                cargarcarrito();
            }
            if (e.CommandName == "subir")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable ocar = new DataTable();
                ocar = (DataTable)Session["pedido"];
                ocar.Rows[index]["canproducto"] =  Int32.Parse( ocar.Rows[index]["canproducto"].ToString()) + 1;
                ocar.Rows[index]["subtotal"] = Int32.Parse(ocar.Rows[index]["canproducto"].ToString()) * Int32.Parse(ocar.Rows[index]["preproducto"].ToString());
                Session["pedido"] = ocar;
                GridView1.DataSource = ocar;
                GridView1.DataBind();
            }
            if (e.CommandName == "bajar")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable ocar = new DataTable();
                ocar = (DataTable)Session["pedido"];
                if (Int32.Parse(ocar.Rows[index]["canproducto"].ToString()) == 1)
                {/*
                    ocar.Rows[index].Delete();
                    //lblTotal.Text = TotalCarrito(ocar).ToString();
                    GridView1.DataSource = ocar;
                    GridView1.DataBind();
                    cargarcarrito();*/
                }
                else
                {
                    ocar.Rows[index]["canproducto"] = Int32.Parse(ocar.Rows[index]["canproducto"].ToString()) - 1;
                    ocar.Rows[index]["subtotal"] = Int32.Parse(ocar.Rows[index]["canproducto"].ToString()) * Int32.Parse(ocar.Rows[index]["preproducto"].ToString());
                    Session["pedido"] = ocar;
                    GridView1.DataSource = ocar;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["pedido"];
                dt1.Rows[index].Delete();
                //lblTotal.Text = TotalCarrito(dt1).ToString();
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                Session["pedido"] = dt1;
                //Button1_Click(Button1, null);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OrdenBLL ordBLL = new OrdenBLL();
            //ordBLL.creaOrden();
            if (Session["numero_boleta"] == null)
            {

            }
            else if (Session["numero_boleta"] != null && GridView1.Rows.Count >0)
            {
                string faltaStock = "";
                foreach (GridViewRow row in GridView1.Rows)
                {
                    int id = int.Parse(row.Cells[1].Text);
                    string cat = row.Cells[3].Text;
                    if (cat != "bebible")
                    {
                        PlatoBLL plaBLL = new PlatoBLL();
                        int stockactual = plaBLL.verificaStock(id);
                        int stockSolicitado = int.Parse(((TextBox)row.Cells[5].FindControl("TextBox1")).Text);
                        if (stockactual < stockSolicitado)
                        {
                            faltaStock = faltaStock + row.Cells[2].Text + " Stock Actual: " + stockactual +"\\n ";
                        }
                         
                    }
                    else if (cat == "bebible")
                    {
                        BebestibleBLL bebesBLL = new BebestibleBLL();
                        int stockactual = bebesBLL.verificaStock(id);
                        int stockSolicitado = int.Parse(((TextBox)row.Cells[5].FindControl("TextBox1")).Text);
                        if (stockactual < stockSolicitado)
                        {
                            faltaStock = faltaStock + row.Cells[2].Text + " Stock Actual: " + stockactual + "\\n ";
                        }
                    }
                }



                if (faltaStock.Length <= 0)
                {

                    bool check = ordBLL.creaOrden(int.Parse(Session["numero_boleta"].ToString()));
                    if(check)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            int id = int.Parse( row.Cells[1].Text);
                            int cantidad = int.Parse(((TextBox)row.Cells[5].FindControl("TextBox1")).Text);

                            if (row.Cells[3].Text != "bebible")
                            {
                                PlatoBLL plBLL = new PlatoBLL();
                                plBLL.ingresaRelacion(id,cantidad);
                                plBLL.reduceStock(id,cantidad);
                            }
                            else if (row.Cells[3].Text == "bebible")
                            {
                                BebestibleBLL bebBLL = new BebestibleBLL();
                                bebBLL.agregarRelacion(id,cantidad);
                                bebBLL.reduceStock(id, cantidad);

                            }
                        }
                        Session["pedido"] = null;
                        Response.Redirect("/CarroCompras.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo Generar la orden: \\n Insuficiente Stock de: \\n"+faltaStock+" ')", true);

                }
            }


            
        }

        private void GetTotal(int numeroBoleta)
        {
            BoletaBLL bolBLL = new BoletaBLL();
            int total = bolBLL.GetTotalBoleta(numeroBoleta);
            if (total != 0 && total != -1)
            {
                lblTotal.Text = "Total: $" + total;
                lblDetalle.Text = "Detalle Pedidos";
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {/*
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;



            //var ordenid = "" + numboleta;
            //var sessionid = "" + numboleta;

            string returnurl = "http://localhost:52091/Webpay/Pagosuccess";
            string finalurl = "http://localhost:52091/Webpay/FinalPagado";

            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);

            var initresult = transaction.initTransaction(500, "500", "500", returnurl, finalurl);

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

            Response.End();*/
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
            {
                try
                {

                    if (Session["numero_boleta"] != null)
                    {
                        int numboleta = Int32.Parse(Session["numero_boleta"].ToString());

                        BoletaBLL bolBLL = new BoletaBLL();

                        int total = bolBLL.GetTotalBoleta(numboleta);
                        var monto = total;

                        bolBLL.actualizarBoleta(numboleta, total, total, "Transbank", "pagando");

                        var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

                        var ordenid = "" + numboleta;
                        var sessionid = "" + numboleta;

                        string returnurl = "http://localhost:52091/Webpay/Pagosuccess";
                        string finalurl = "http://localhost:52091/Webpay/FinalPagado";

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else if (RadioButton1.Checked == true)
            {
                if (Session["numero_boleta"] != null)
                {
                    int numboleta = Int32.Parse(Session["numero_boleta"].ToString());

                    BoletaBLL bolBLL = new BoletaBLL();

                    int total = bolBLL.GetTotalBoleta(numboleta);
                    var monto = total;

                    bolBLL.actualizarBoleta(numboleta, total, total, "Efectivo", "pagando");
                    Response.Redirect("/CarroCompras.aspx");
                }
            }
            else
            {
                string script = "alert('Porfavor Seleccione un Metodo de Pago');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }

        }
    }
}