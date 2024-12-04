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
    public class clsAdministrador
    {
        string connectionString;

        public clsAdministrador()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }
        public void CrearAdministrador(string nombre, string apellido, string correo, string direccion, string telefono, string userName, string passwordAdm, int rolID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearAdministrador", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@PasswordAdm", passwordAdm);
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

        public void ActualizarAdministrador(int idAdim, string nombre, string apellido, string correo, string direccion, string telefono, string userName, string passwordAdm, int rolID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarAdministrador", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDAdim", idAdim);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@PasswordAdm", passwordAdm);
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

        public void EliminarAdministrador(int idAdim, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarAdministrador", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDAdim", idAdim);

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

        public List<Administrador> MostrarAdministradores()
        {
            var administradores = new List<Administrador>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarAdministradores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var administrador = new Administrador
                            {
                                IDAdim = (int)reader["IDAdim"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                PasswordAdm = reader["PasswordAdm"].ToString(),
                                RolID = (int)reader["RolID"]
                            };
                            administradores.Add(administrador);
                        }
                    }
                }
            }

            return administradores;
        }
    }
}