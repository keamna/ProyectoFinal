using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;
using TallerElectronicos.CapaVista;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_Asignacion
    {
        #region Asignaciones

        public static List<Cls_Asignacion> ObtenerAsignacion()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Asignacion> Asignado = new List<Cls_Asignacion>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarAsignacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Asignacion Asignaciones = new Cls_Asignacion();
                            Asignaciones.AsignacionId = reader.GetInt32(0);
                            Asignaciones.ReparacionId = reader.GetInt32(1);
                            Asignaciones.TecnicoId = reader.GetInt32(2);
                            Asignaciones.FechaAsignacion = reader.GetDateTime(reader.GetOrdinal("fechaAsignacion")).ToString("yyyy/MM/dd");
                            Asignaciones.Estado = reader.GetString(4);

                            Asignado.Add(Asignaciones);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Asignado;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Asignado;
        }
        #endregion

        #region Agregar Asignacion
        public static int AgregarAsignacion(int ReparacionId, int TecnicoId, string FechaAsignacion, string Estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarAsignacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", ReparacionId));
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoId));
                    cmd.Parameters.Add(new SqlParameter("@FechaAsignacion", FechaAsignacion));
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


        #region Listado con Filtro de Asignacion
        public static List<Cls_Asignacion> ObtenerAsignacionFiltro(int asignacionID)
        {
            List<Cls_Asignacion> asignaciones = new List<Cls_Asignacion>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarAsignacionFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Asignacion asignacion = new Cls_Asignacion
                                {
                                    AsignacionId = reader.GetInt32(reader.GetOrdinal("AsignacionID")),
                                    ReparacionId = reader.GetInt32(reader.GetOrdinal("ReparacionID")),
                                    TecnicoId = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                                    FechaAsignacion = reader.GetString(reader.GetOrdinal("FechaAsignacion")),
                                    Estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                asignaciones.Add(asignacion);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener asignacion por código: " + ex.Message);
            }

            return asignaciones;
        }

        #endregion

        #region Borrar Asignacion

        public static bool BorrarAsignacion(int AsignacionId)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el Asignacion existe
                string queryExistencia = "SELECT COUNT(*) FROM Asignaciones WHERE AsignacionID = @AsignacionID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@AsignacionID", AsignacionId));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE Asignaciones SET Estado = 'Inactivo' WHERE AsignacionID = @AsignacionID";

                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@AsignacionID", AsignacionId));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar asignacion: " + ex.Message);
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
        public static bool ModificarAsignacion(int AsignacionId, int ReparacionId, int TecnicoId, string FechaAsignacion, string Estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM Asignacions WHERE AsignacionID = @AsignacionID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@AsignacionID", AsignacionId));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarAsignacion";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@AsignacionID", AsignacionId));
                        cmd.Parameters.Add(new SqlParameter("@ReparacionID", ReparacionId));
                        cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoId));
                        cmd.Parameters.Add(new SqlParameter("@FechaAsignacion", FechaAsignacion));
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
                Console.WriteLine("Error al modificar la asignacion: " + ex.Message);
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

    
