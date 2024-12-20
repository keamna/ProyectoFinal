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
    public partial class Reparacion : System.Web.UI.Page
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
            List<Cls_Reparacion> reparaciones = Bussiness_Reparacion.ObtenerReparacion();

            if (reparaciones.Count > 0)
            {
                GridViewReparacion.DataSource = reparaciones;
                GridViewReparacion.DataBind();


                foreach (GridViewRow row in GridViewReparacion.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListReparacion");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay reparaciones disponibles.");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListReparacion.SelectedItem.Text;

                int equipo = int.Parse(TequipoID.Text);

                if (Bussiness_Reparacion.AgregarReparacion(equipo,TfechaSolicitud.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Reparacion ingresada correctamente");
                    LlenarGrid();

                    TequipoID.Text = "";
                    TfechaSolicitud.Text = "";
                    DropDownListReparacion.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar reparacion");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar reparacion: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TreparacionID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TreparacionID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Reparacion> reparaciones = Bussiness_Reparacion.ObtenerReparacionFiltro(codigo);

                // Verifica si hay resultados
                if (reparaciones.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewReparacion.DataSource = reparaciones;
                    GridViewReparacion.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewReparacion.DataSource = null;
                    GridViewReparacion.DataBind();

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
                int codigo = int.Parse(TreparacionID.Text);

                bool isDeleted = Bussiness_Reparacion.BorrarReparacion(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Reparacion borrada correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TreparacionID.Text = "";

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
                int reparacionID = int.Parse(TreparacionID.Text);
                int equipoID = int.Parse(TequipoID.Text);
                string fechaSolicitud = TfechaSolicitud.Text;
                string estado = DropDownListReparacion.SelectedItem.Text;

                bool isUpdated = Bussiness_Reparacion.ModificarReparacion(reparacionID,equipoID,fechaSolicitud, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Reparacion modificada correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TreparacionID.Text = "";
                    TequipoID.Text = "";
                    TfechaSolicitud.Text = "";
                    DropDownListReparacion.SelectedIndex = 0;
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