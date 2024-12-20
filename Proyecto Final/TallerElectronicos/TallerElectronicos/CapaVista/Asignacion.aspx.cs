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
    public partial class Asignacion : System.Web.UI.Page
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
            List<Cls_Asignacion> asignaciones = Bussiness_Asignacion.ObtenerAsignacion();

            if (asignaciones.Count > 0)
            {
                GridViewAsignacion.DataSource = asignaciones;
                GridViewAsignacion.DataBind();


                foreach (GridViewRow row in GridViewAsignacion.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListAsignacion");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay asignaciones disponibles.");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListAsignacion.SelectedItem.Text;

                int reparacionID = int.Parse(TReparacionID.Text);
                int tecnicoID = int.Parse(TTecnico.Text);

                if (Bussiness_Asignacion.AgregarAsignacion(reparacionID, tecnicoID, TFechaAsignacion.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Asignacion ingresada correctamente");
                    LlenarGrid();

                    TReparacionID.Text = "";
                    TFechaAsignacion.Text = "";
                    DropDownListAsignacion.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar asignacion");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar asignacion: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TAsignacionID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TAsignacionID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Asignacion> asignaciones = Bussiness_Asignacion.ObtenerAsignacionFiltro(codigo);

                // Verifica si hay resultados
                if (asignaciones.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewAsignacion.DataSource = asignaciones;
                    GridViewAsignacion.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewAsignacion.DataSource = null;
                    GridViewAsignacion.DataBind();

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
                int codigo = int.Parse(TAsignacionID.Text);

                bool isDeleted = Bussiness_Asignacion.BorrarAsignacion(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Asignacion borrada correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TAsignacionID.Text = "";

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
                int asignacionID = int.Parse(TAsignacionID.Text);
                int reparacionID = int.Parse(TReparacionID.Text);
                int tecnicoID = int.Parse(TTecnico.Text);
                string fechaAsignacion = TFechaAsignacion.Text;
                string estado = DropDownListAsignacion.SelectedItem.Text;

                bool isUpdated = Bussiness_Asignacion.ModificarAsignacion(asignacionID, reparacionID, tecnicoID, fechaAsignacion, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Asignacion modificada correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TAsignacionID.Text = "";
                    TReparacionID.Text = "";
                    TTecnico.Text = "";
                    TFechaAsignacion.Text = "";
                    DropDownListAsignacion.SelectedIndex = 0;
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