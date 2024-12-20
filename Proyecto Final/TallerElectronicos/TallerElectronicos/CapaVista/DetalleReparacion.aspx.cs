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
    public partial class DetalleReparacion : System.Web.UI.Page
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
            List<Cls_DetalleReparacion> detalleReparaciones = Bussiness_DetalleReparacion.ObtenerDetalleReparacion();

            if (detalleReparaciones.Count > 0)
            {
                GridViewDetalleReparacion.DataSource = detalleReparaciones;
                GridViewDetalleReparacion.DataBind();


                foreach (GridViewRow row in GridViewDetalleReparacion.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListDetalleReparacion");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay detalles de reparacion disponibles.");
            }

        }



        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListDetalleReparacion.SelectedItem.Text;

                int reparacionID = int.Parse(TReparacionID.Text);

                if (Bussiness_DetalleReparacion.AgregarDetallesReparacion(reparacionID, TDescripcion.Text, TFechaInicio.Text,TfechaFin.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Detalle de reparacion ingresada correctamente");
                    LlenarGrid();

                    TReparacionID.Text = "";
                    TDescripcion.Text = "";
                    TFechaInicio.Text = "";
                    TfechaFin.Text = "";
                    DropDownListDetalleReparacion.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar detalle");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar detalle: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TDetalleID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TDetalleID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_DetalleReparacion> detalles = Bussiness_DetalleReparacion.ObtenerDetalleFiltro(codigo);

                // Verifica si hay resultados
                if (detalles.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewDetalleReparacion.DataSource = detalles;
                    GridViewDetalleReparacion.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewDetalleReparacion.DataSource = null;
                    GridViewDetalleReparacion.DataBind();

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
                int codigo = int.Parse(TDetalleID.Text);

                bool isDeleted = Bussiness_DetalleReparacion.BorrarDetalle(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Detalle borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TDetalleID.Text = "";

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
                int detalleID = int.Parse(TDetalleID.Text);
                int reparacionID = int.Parse(TReparacionID.Text);
                string descripcion = TDescripcion.Text;
                string fechaInicio = TFechaInicio.Text;
                string fechaFinal = TfechaFin.Text;
                string estado = DropDownListDetalleReparacion.SelectedItem.Text;

                bool isUpdated = Bussiness_DetalleReparacion.ModificarDetalle(detalleID, reparacionID, descripcion, fechaInicio,fechaFinal, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Detalle modificado correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TDetalleID.Text = "";
                    TReparacionID.Text = "";
                    TDescripcion.Text = "";
                    TFechaInicio.Text = "";
                    TfechaFin.Text = "";
                    DropDownListDetalleReparacion.SelectedIndex = 0;
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