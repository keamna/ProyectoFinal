using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TallerElectronicos.CapaLogica;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaVista
{
    public partial class Tecnico : System.Web.UI.Page
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
            List<Cls_Tecnico> tecnicos = Bussiness_Tecnico.ObtenerTecnico(); 

            if (tecnicos.Count > 0)
            {
                GridViewTecnico.DataSource = tecnicos;
                GridViewTecnico.DataBind(); 

              
                foreach (GridViewRow row in GridViewTecnico.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListTecnico");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text; 
                        ddlEstado.SelectedValue = estado;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay tecnicos disponibles.");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListTecnico.SelectedItem.Text;

                if (Bussiness_Tecnico.AgregarTecnico(TnombreTecnico.Text, Tespecialidad.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Tecnico ingresado correctamente");
                    LlenarGrid();

                    TnombreTecnico.Text = "";
                    Tespecialidad.Text = "";
                    DropDownListTecnico.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar tecnico");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar tecnico: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TtecnicoID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TtecnicoID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Tecnico> tecnicos = Bussiness_Tecnico.ObtenerTecnicoFiltro(codigo);

                // Verifica si hay resultados
                if (tecnicos.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewTecnico.DataSource = tecnicos;
                    GridViewTecnico.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewTecnico.DataSource = null;
                    GridViewTecnico.DataBind();

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
                int codigo = int.Parse(TtecnicoID.Text);

                bool isDeleted = Bussiness_Tecnico.BorrarTecnico(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Tecnico borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TtecnicoID.Text = "";

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
                int codigoTecnico = int.Parse(TtecnicoID.Text);
                string nombre = TnombreTecnico.Text;
                string especialidad = Tespecialidad.Text;
                string estado = DropDownListTecnico.SelectedItem.Text;

                bool isUpdated = Bussiness_Tecnico.ModificarTecnicos(codigoTecnico, nombre, especialidad, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Tecnico modificado correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TtecnicoID.Text = "";
                    TnombreTecnico.Text = "";
                    Tespecialidad.Text = "";
                    DropDownListTecnico.SelectedIndex = 0;
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