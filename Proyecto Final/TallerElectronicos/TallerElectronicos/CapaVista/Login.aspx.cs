using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TallerElectronicos.CapaLogica;
using static TallerElectronicos.CapaLogica.DBConn;

namespace TallerElectronicos.CapaVista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioID"] != null)
            {
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(correo))
            {
                JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa tu correo");
                return;
            }

            Session["UsuarioID"] = correo; 

            Response.Redirect("Inicio.aspx");
        }
    }
}