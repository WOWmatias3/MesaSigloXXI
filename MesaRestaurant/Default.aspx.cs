using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace MesaRestaurant
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void setmesa()
        {
            //create a cookie
            HttpCookie myCookie = new HttpCookie("mesa");

            //Add key-values in the cookie
            myCookie.Values.Add("num_mesa", DropDownList1.SelectedValue);

            //set cookie expiry date-time. Made it to last for next 12 hours.
            myCookie.Expires = DateTime.Now.AddYears(30);

            MesaBLL mesaBLL = new MesaBLL();
            mesaBLL.SetStatusMesa(Int32.Parse( DropDownList1.SelectedValue), "ocupado");

            //Most important, write the cookie to client.
            Response.Cookies.Add(myCookie);
            Response.Redirect("/VistaMenus/mesamain.aspx");
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "" || txtUsername.Text.Trim() == "")
            {
                lblComprobacion.Text = "Ingrese los Campos antes de continuar";
            }
            else
            {

                UsuarioBLL usrBLL = new UsuarioBLL();
                if (usrBLL.verificaUser(txtUsername.Text.Trim(),txtPassword.Text.Trim()))
                {


                    HttpCookie myCookie = Request.Cookies["mesa"];
                    if (myCookie == null)
                    {
                        setmesa();
                    }
                    if (!string.IsNullOrEmpty(myCookie.Values["num_mesa"]))
                    {
                        string nummesa = myCookie.Values["num_mesa"].ToString();
                        //Yes userId is found. Mission accomplished.
                        MesaBLL mesaBLL = new MesaBLL();
                        mesaBLL.SetStatusMesa(Int32.Parse(nummesa),"vacio");
                        setmesa();
                    }
                }
                else
                {
                    lblComprobacion.Text = "Usuario, contraseña o Rol incorrectos";
                }
            }
        }
    }
}