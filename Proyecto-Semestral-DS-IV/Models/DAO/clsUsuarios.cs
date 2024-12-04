using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Proyecto_Semestral_DS_IV.Models.Modelos;

namespace Proyecto_Semestral_DS_IV.Models.DAO
{
    public class clsUsuarios
    {
        string connectionString;

        public clsUsuarios()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }
        public bool CrearUsuario(string nombre, string apellido, string correo, string telefono, string direccion, string userName, string password, out string mensaje)
        {
            bool success = false;
            int rolID = 1;
            mensaje = "";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@PasswordUser", password);
                    command.Parameters.AddWithValue("@RolID", rolID);
                    command.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        mensaje = command.Parameters["@Mensaje"].Value.ToString();
                        success = mensaje == "Usuario creado exitosamente.";
                    }
                    catch (Exception ex)
                    {
                        mensaje = $"Error: {ex.Message}";
                    }
                }
            }

            return success;
        }

        public void ActualizarUsuario(int idUsuario, string nombre, string apellido, string correo, string telefono, string direccion, string userName, string passwordUser, int rolID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDUsuario", idUsuario);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@PasswordUser", passwordUser);
                    command.Parameters.AddWithValue("@RolID", rolID);

                    var outputParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();
                    mensaje = outputParam.Value.ToString();
                }
            }
        }

        public void EliminarUsuario(int idUsuario, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDUsuario", idUsuario);

                    var outputParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();
                    mensaje = outputParam.Value.ToString();
                }
            }
        }

        public List<Usuario> MostrarUsuarios()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarUsuarios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IDUsuario = (int)reader["IDUsuario"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                PasswordUser = reader["PasswordUser"].ToString(),
                                RolID = (int)reader["RolID"]
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
        public Usuario ValidarUsuario(string userName, string password)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ValidarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@PasswordUser", password);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IDUsuario = (int)reader["IDUsuario"],
                                UserName = reader["UserName"].ToString(),
                                RolID = (int)reader["RolID"]
                            };
                        }
                    }
                }
            }

            return usuario;
        }
    }
}