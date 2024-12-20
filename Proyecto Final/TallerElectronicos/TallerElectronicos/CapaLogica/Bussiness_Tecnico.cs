using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_Tecnico
    {
        #region Listado de Tecnicos

        public static List<Cls_Tecnico> ObtenerTecnico()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Tecnico> tecnicos = new List<Cls_Tecnico>(); 
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarTecnicos", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Tecnico tecnico = new Cls_Tecnico();
                            tecnico.id = reader.GetInt32(0);
                            tecnico.nombre = reader.GetString(1);
                            tecnico.especialidad = reader.GetString(2);
                            tecnico.estado = reader.GetString(3);

                            tecnicos.Add(tecnico);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return tecnicos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return tecnicos;
        }
        #endregion

        #region Agregar Tecnicos
        public static int AgregarTecnico(string nombre, string especialidad, string estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarTecnicos", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@Especialidad", especialidad));
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
        public static List<Cls_Tecnico> ObtenerTecnicoFiltro(int tecnicoID)
        {
            List<Cls_Tecnico> tecnicos = new List<Cls_Tecnico>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarTecnicosFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Tecnico tecnico = new Cls_Tecnico
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                                    nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    especialidad = reader.GetString(reader.GetOrdinal("Especialidad")),
                                    estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                tecnicos.Add(tecnico);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener tecnico por código: " + ex.Message);
            }

            return tecnicos;
        }

        #endregion

        #region Borrar Tecnicos

        public static bool BorrarTecnico(int tecnicoID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el tecnico existe
                string queryExistencia = "SELECT COUNT(*) FROM Tecnicos WHERE TecnicoID = @TecnicoID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }
                string actualizarEstado = "UPDATE Tecnicos SET Estado = 'Inactivo' WHERE TecnicoID = @TecnicoID";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar tecnico: " + ex.Message);
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

        #region Modificar Tecnicos
        public static bool ModificarTecnicos(int tecnicoID, string nombre, string especialidad, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                // Verifica si el tecnico existe
                string queryExistencia = "SELECT COUNT(*) FROM Tecnicos WHERE TecnicoID = @TecnicoID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarTecnicos";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@Especialidad", especialidad));
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
                Console.WriteLine("Error al modificar tecnico: " + ex.Message);
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
