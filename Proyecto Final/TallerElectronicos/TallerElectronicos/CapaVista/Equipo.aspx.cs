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
    public partial class Equipo : System.Web.UI.Page
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
            List<Cls_Equipo> equipos = Bussiness_Equipo.ObtenerEquipo();

            if (equipos.Count > 0)
            {
                GridViewEquipos.DataSource = equipos;
                GridViewEquipos.DataBind();


                foreach (GridViewRow row in GridViewEquipos.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListEquipo");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay equipos disponibles");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListEquipo.SelectedItem.Text;
                int usuarioID = int.Parse(TUsuarioID.Text);

                if (Bussiness_Equipo.AgregarEquipo(TtipoEquipo.Text, Tmodelo.Text, usuarioID, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Equipo ingresado correctamente");
                    LlenarGrid();

                    TtipoEquipo.Text = "";
                    Tmodelo.Text = "";
                    TUsuarioID.Text = "";
                    DropDownListEquipo.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar equipo");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar equipo: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el codigo no este vacío
                if (string.IsNullOrWhiteSpace(TequipoID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el codigo sea un int 
                int codigo;
                if (!int.TryParse(TequipoID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Equipo> equipos = Bussiness_Equipo.ObtenerEquipoFiltro(codigo);

                // Verifica si hay resultados
                if (equipos.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewEquipos.DataSource = equipos;
                    GridViewEquipos.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewEquipos.DataSource = null;
                    GridViewEquipos.DataBind();

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
                int equipoID = int.Parse(TequipoID.Text);

                bool isDeleted = Bussiness_Equipo.BorrarEquipo(equipoID);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Equipo borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TequipoID.Text = "";

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
                int equipoID = int.Parse(TequipoID.Text);
                string tipoEquipo = TtipoEquipo.Text;
                string modelo = Tmodelo.Text;
                int usuarioID = int.Parse(TUsuarioID.Text);
                string estado = DropDownListEquipo.SelectedItem.Text;

                bool isUpdated = Bussiness_Equipo.ModificarEquipo(equipoID, tipoEquipo, modelo, usuarioID, estado);

                if (isUpdated)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Equipo modificado correctamente");

                    // Recarga los datos actualizados en el grid view
                    LlenarGrid();

                    TequipoID.Text = "";
                    TtipoEquipo.Text = "";
                    Tmodelo.Text = "";
                    TUsuarioID.Text = "";
                    DropDownListEquipo.SelectedIndex = 0;
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