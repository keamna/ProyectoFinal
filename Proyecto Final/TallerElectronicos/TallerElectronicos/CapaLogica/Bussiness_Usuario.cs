using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TallerElectronicos.CapaModelo;

namespace TallerElectronicos.CapaLogica
{
    public class Bussiness_Usuario
    {
        #region Listado de Usuarios

        public static List<Cls_Usuario> ObtenerUsuario()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Usuario> usuarios = new List<Cls_Usuario>(); 
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarUsuarios", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Usuario usuario = new Cls_Usuario();
                            usuario.id = reader.GetInt32(0);
                            usuario.nombre = reader.GetString(1);
                            usuario.correo = reader.GetString(2);
                            usuario.telefono = reader.GetString(3);
                            usuario.estado = reader.GetString(4);

                            usuarios.Add(usuario);  
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return usuarios;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return usuarios;
        }
        #endregion

        #region AgregarUsuarios
        public static int AgregarUsuario(string nombre, string correo, string telefono, string estado)
        {
            int retorno = 0;
            ;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", correo));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", telefono));
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
        public static List<Cls_Usuario> ObtenerUsuarioFiltro(int usuarioID)
        {
            List<Cls_Usuario> usuarios = new List<Cls_Usuario>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarUsuariosFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Usuario usuario = new Cls_Usuario
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                    nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    correo = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                                    telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    estado = reader.GetString(reader.GetOrdinal("Estado"))
                                };

                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener usuario por código: " + ex.Message);
            }

            return usuarios;
        }




        #endregion

        #region BorrarUsuarios

        public static bool BorrarUsuario(int usuarioID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                // Verifica si el usuario existe
                string queryExistencia = "SELECT COUNT(*) FROM Usuarios WHERE UsuarioID = @UsuarioID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                // Si el usuario existe, procede a eliminarlo
                string actualizarEstado = "UPDATE Usuarios SET Estado = 'Inactivo' WHERE UsuarioID = @UsuarioID";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar usuario: " + ex.Message);
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

        #region ModificarUsuarios
        public static bool ModificarUsuarios(int usuarioID, string nombre, string correo, string telefono, string estado)
        {
            SqlConnection Conn = null;
            try
            {

                Conn = DBConn.obtenerConexion();

                // Verifica si el usuario existe
                string queryExistencia = "SELECT COUNT(*) FROM Usuarios WHERE UsuarioID = @UsuarioID";
                using (SqlCommand cmdExistencia = new SqlCommand(queryExistencia, Conn))
                {
                    cmdExistencia.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));

                    int count = (int)cmdExistencia.ExecuteScalar();

                    if (count == 0)
                    {
                        return false;
                    }

                    string procedimientoAlmacenado = "modificarUsuario";
                    using (SqlCommand cmd = new SqlCommand(procedimientoAlmacenado, Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@UsuarioID", usuarioID));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", correo));
                        cmd.Parameters.Add(new SqlParameter("@Telefono", telefono));
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
                Console.WriteLine("Error al modificar usuario: " + ex.Message);
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

        public static string LoginUsuario(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return "Por favor ingresa un correo electrónico válido";
            }

            return "Login Exitoso"; 
        }
    }
}

