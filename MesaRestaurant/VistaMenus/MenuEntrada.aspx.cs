using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MesaRestaurant.VistaMenus
{
    public partial class MenuEntrada : System.Web.UI.Page
    {
        DataTable dtb;
        DataTable carrito = new DataTable();
        string nummesa;

        public void CargarDetalle()
        {
            if (Session["pedido"] == null)
            {
                dtb = new DataTable("Carrito");
                dtb.Columns.Add("codproducto", System.Type.GetType("System.String"));
                dtb.Columns.Add("NombreProd", System.Type.GetType("System.String"));
                dtb.Columns.Add("CatProducto", System.Type.GetType("System.String"));
                dtb.Columns.Add("canproducto", System.Type.GetType("System.Int32"));
                dtb.Columns.Add("preproducto", System.Type.GetType("System.Int32"));
                dtb.Columns.Add("subtotal", System.Type.GetType("System.Double"));

                Session["pedido"] = dtb;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (Page.IsPostBack == false)
            {
                CargarDetalle();
            }
        }
        public string GetImage(object img)
        {
            if (img != System.DBNull.Value)
            {
                return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);

            }
            return null;

        }

        protected void clickagregar(object sender, EventArgs e)
        {

        }
        protected void platocommand(object source, ListViewCommandEventArgs e)
        {
            string cod = "";
            string nom = null;
            int precio = 0;
            if (e.CommandName == "Seleccionar")
            {

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    var varnombre = e.Item.FindControl("NOMBRE_PLATOLabel") as Label;
                    var varprecio = e.Item.FindControl("lbPrecio") as Label;
                    var varcod = e.Item.FindControl("lbCodigo") as HiddenField;
                    cod = varcod.Value;
                    nom = varnombre.Text;
                    precio = Int32.Parse(varprecio.Text);
                }

                agregaralcarro(cod, nom, "entrada", 1, precio);

                lbPlatoAgregado.Text = "Producto: " + nom + " Agregado al Carro!";

            }
        }

        protected void agregaralcarro(string cod, string nombre, string cat, int cantidad, int precio)
        {
            double total;
            cantidad = 1;
            total = precio * cantidad;
            carrito = (DataTable)Session["pedido"];
            bool coincide = false;
            foreach (DataRow row in carrito.Rows)
            {
                if (row[0].ToString() == cod && row[2].ToString() == cat)
                {
                    row[3] = Int32.Parse(row[3].ToString()) + 1;
                    row[5] = Int32.Parse(row[3].ToString()) * Int32.Parse(row[4].ToString());
                    coincide = true;
                    break;
                }
            }
            if (!coincide)
            {
                DataRow fila = carrito.NewRow();
                fila[0] = cod;
                fila[1] = nombre;
                fila[2] = cat;
                fila[3] = (int)cantidad;
                fila[4] = precio;
                fila[5] = total;
                carrito.Rows.Add(fila);
                Session["pedido"] = carrito;
            }
            else
            {
                Session["pedido"] = carrito;
            }
        }

        
    }
}