using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_Equipo
    {
        #region Listado de Usuarios

        public static List<Cls_Equipo> ObtenerEquipo()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Equipo> equipos = new List<Cls_Equipo>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarEquipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Equipo equipo = new Cls_Equipo();
                            equipo.equipoID = reader.GetInt32(0);
                            equipo.tipoEquipo = reader.GetString(1);
                            equipo.modelo = reader.GetString(2);
                            equipo.usuarioID = reader.GetInt32(3);
                            equipo.estado = reader.GetString(4);

                            equipos.Add(equipo);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return equipos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return equipos;
        }
        #endregion

        #region AgregarEquipos
        public static int AgregarEquipo(string tipoEquipo, string modelo, int usuarioID, string estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarEquipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@TipoEquipo", tipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@Modelo", modelo));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));
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
        public static List<Cls_Equipo> ObtenerEquipoFiltro(int equipoID)
        {
            List<Cls_Equipo> equipos = new List<Cls_Equipo>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarEquipoFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Equipo equipo = new Cls_Equipo
                                {
                                    equipoID = reader.GetInt32(reader.GetOrdinal("EquipoID")),
                                    tipoEquipo = reader.GetString(reader.GetOrdinal("TipoEquipo")),
                                    modelo = reader.GetString(reader.GetOrdinal("Modelo")),
                                    usuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                    estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                equipos.Add(equipo);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener equipo por código: " + ex.Message);
            }

            return equipos;
        }

        #endregion

        #region BorrarEquipo

        public static bool BorrarEquipo(int equipoID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el equipo existe
                string queryExistencia = "SELECT COUNT(*) FROM Equipos WHERE EquipoID = @EquipoID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@EquipoID", equipoID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE Equipos SET Estado = 'Inactivo' WHERE EquipoID = @EquipoID";

                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar equipo: " + ex.Message);
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

        #region ModificarEquipo
        public static bool ModificarEquipo(int equipoID, string tipoEquipo, string modelo, int usuarioID, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM Equipos WHERE EquipoID = @EquipoID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@EquipoID", equipoID));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarEquipo";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                        cmd.Parameters.Add(new SqlParameter("@TipoEquipo", tipoEquipo));
                        cmd.Parameters.Add(new SqlParameter("@Modelo", modelo));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));
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
                Console.WriteLine("Error al modificar equipo: " + ex.Message);
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
