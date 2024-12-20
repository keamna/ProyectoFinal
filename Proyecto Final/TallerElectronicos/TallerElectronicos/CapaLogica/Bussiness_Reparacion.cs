using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_Reparacion
    {
        #region Listado de Reparaciones

        public static List<Cls_Reparacion> ObtenerReparacion()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Reparacion> reparaciones = new List<Cls_Reparacion>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarReparacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Reparacion reparacion = new Cls_Reparacion();
                            reparacion.reparacionId = reader.GetInt32(0);
                            reparacion.equipoId = reader.GetInt32(1);
                            reparacion.fechaSolicitud = reader.GetDateTime(reader.GetOrdinal("fechaSolicitud")).ToString("yyyy/MM/dd");
                            reparacion.estado = reader.GetString(3);

                            reparaciones.Add(reparacion);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return reparaciones;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return reparaciones;
        }
        #endregion

        #region Agregar Reparacion
        public static int AgregarReparacion(int equipoID, string fechaSolicitud, string estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarReparacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaSolicitud", fechaSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

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


        #region Listado con Filtro
        public static List<Cls_Reparacion> ObtenerReparacionFiltro(int reparacionID)
        {
            List<Cls_Reparacion> reparaciones = new List<Cls_Reparacion>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarReparacionFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Reparacion reparacion = new Cls_Reparacion
                                {
                                    reparacionId = reader.GetInt32(reader.GetOrdinal("ReparacionID")),
                                    equipoId = reader.GetInt32(reader.GetOrdinal("EquipoID")),
                                    fechaSolicitud = reader.GetString(reader.GetOrdinal("FechaSolicitud")),
                                    estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                reparaciones.Add(reparacion);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener reparacion por código: " + ex.Message);
            }

            return reparaciones;
        }

        #endregion

        #region Borrar Reparacion

        public static bool BorrarReparacion(int reparacionID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si la reparacion existe
                string queryExistencia = "SELECT COUNT(*) FROM Reparaciones WHERE ReparacionID = @ReparacionID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }
                string actualizarEstado = "UPDATE Reparaciones SET Estado = 'Inactivo' WHERE ReparacionID = @ReparacionID";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar reparacion: " + ex.Message);
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

        #region ModificarReparaciones
        public static bool ModificarReparacion(int reparacionID, int equipoID, string fechaSolicitud, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                // Verifica si la reparacion existe
                string queryExistencia = "SELECT COUNT(*) FROM Reparaciones WHERE ReparacionID = @ReparacionID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarReparacion";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                        cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                        cmd.Parameters.Add(new SqlParameter("@FechaSolicitud", fechaSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@Estado", estado));

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
                Console.WriteLine("Error al modificar reparacion: " + ex.Message);
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