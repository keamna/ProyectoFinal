using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TallerElectronicos.CapaLogica;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaVista
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();

            }
        }

        protected void LlenarGrid()
        {
            List<Cls_Usuario> usuarios = Bussiness_Usuario.ObtenerUsuario();

            if (usuarios.Count > 0)
            {
                GridViewUsuario.DataSource = usuarios;
                GridViewUsuario.DataBind();

                
                foreach (GridViewRow row in GridViewUsuario.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListUsuario");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text; 
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay usuarios disponibles.");
            }

        }
        

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListUsuario.SelectedItem.Text;

                if (Bussiness_Usuario.AgregarUsuario(TnombreUsuario.Text, Tcorreo.Text, Ttelefono.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Usuario ingresado correctamente");
                    LlenarGrid();

                    TnombreUsuario.Text = "";
                    Tcorreo.Text = "";
                    Ttelefono.Text = "";
                    DropDownListUsuario.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar usuario");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar usuario: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TusuarioID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TusuarioID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Usuario> usuarios = Bussiness_Usuario.ObtenerUsuarioFiltro(codigo);

                // Verifica si hay resultados
                if (usuarios.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewUsuario.DataSource = usuarios;
                    GridViewUsuario.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewUsuario.DataSource = null;
                    GridViewUsuario.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no encontrado");
                    LlenarGrid();

                }
            }
            catch (FormatException)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es válido. Por favor ingresa un número.");
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
            }
        }

        protected void Bborrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(TusuarioID.Text);

                bool isDeleted = Bussiness_Usuario.BorrarUsuario(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Usuario borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TusuarioID.Text = "";

            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);

                LlenarGrid();
            }
        }
       
        protected void Bmodificar_Click1(object sender, EventArgs e)
        {
            try
            {
                int codigoUsuario = int.Parse(TusuarioID.Text);
                string nombre = TnombreUsuario.Text;
                string correo = Tcorreo.Text;
                string telefono = Ttelefono.Text;
                string estado = DropDownListUsuario.SelectedItem.Text;  

                bool isUpdated = Bussiness_Usuario.ModificarUsuarios(codigoUsuario, nombre, correo, telefono, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Usuario modificado correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TusuarioID.Text = "";
                    TnombreUsuario.Text = "";
                    Tcorreo.Text = "";
                    Ttelefono.Text = "";
                    DropDownListUsuario.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo modificar");
                }
            }
            catch (Exception ex)
            {
                // Muestra el mensaje del error en especifico
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
            }
        }
    }
}