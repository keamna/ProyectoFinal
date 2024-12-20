using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;
using TallerElectronicos.CapaVista;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_DetalleReparacion
    {
        #region Detalles de Reparacion

        public static List<Cls_DetalleReparacion> ObtenerDetalleReparacion()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_DetalleReparacion> detalles = new List<Cls_DetalleReparacion>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarDetalleReparacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_DetalleReparacion detalle = new Cls_DetalleReparacion();
                            detalle.DetalleId = reader.GetInt32(0);
                            detalle.ReparacionId = reader.GetInt32(1);
                            detalle.Descripcion = reader.GetString(2);
                            detalle.FechaInicio = reader.GetDateTime(reader.GetOrdinal("fechaInicio")).ToString("yyyy/MM/dd");
                            detalle.FechaFin = reader.GetDateTime(reader.GetOrdinal("fechaFin")).ToString("yyyy/MM/dd");
                            detalle.Estado = reader.GetString(5);

                            detalles.Add(detalle);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return detalles;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return detalles;
        }
#endregion

        #region Agregar detalles de reparacion
        public static int AgregarDetallesReparacion(int ReparacionId, string Descripcion, string FechaInicio, string FechaFin, string Estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("IngresarDetalleReparacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", ReparacionId));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                    cmd.Parameters.Add(new SqlParameter("@Estado", Estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
        #endregion


        #region Listado con Filtro de Detalle
        public static List<Cls_DetalleReparacion> ObtenerDetalleFiltro(int DetalleId)
        {
            List<Cls_DetalleReparacion> detalles = new List<Cls_DetalleReparacion>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarDetalleReparacionFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@DetalleID", DetalleId));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_DetalleReparacion detalle = new Cls_DetalleReparacion
                                {
                                    DetalleId = reader.GetInt32(reader.GetOrdinal("DetalleID")),
                                    ReparacionId = reader.GetInt32(reader.GetOrdinal("ReparacionID")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    FechaInicio = reader.GetString(reader.GetOrdinal("FechaInicio")),
                                    FechaFin = reader.GetString(reader.GetOrdinal("FechaFin")),
                                    Estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                detalles.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener detalle de reparacion por código: " + ex.Message);
            }

            return detalles;
        }

        #endregion

        #region Borrar DetalleReparacion

        public static bool BorrarDetalle(int DetalleId)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el detalle existe
                string queryExistencia = "SELECT COUNT(*) FROM DetalleReparacion WHERE DetalleID = @DetalleID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@DetalleID", DetalleId));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE DetalleReparacion SET Estado = 'Inactivo' WHERE DetalleID = @DeatlleID";

                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@DetalleID", DetalleId));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar detalle: " + ex.Message);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region Modificar DetalleReparacion
        public static bool ModificarDetalle(int DetalleId, int ReparacionId, string Descripcion, string FechaInicio, string FechaFin, string Estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM DetalleReparacion WHERE DetalleID = @DetalleID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@DetalleID", DetalleId));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarDetalleReparacion";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@DetalleID", DetalleId));
                        cmd.Parameters.Add(new SqlParameter("@ReparacionID", ReparacionId));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                        cmd.Parameters.Add(new SqlParameter("@Estado", Estado));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al modificar el detalle: " + ex.Message);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();

                    #endregion

                }
            }
        }
    }

}
