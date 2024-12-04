using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Proyecto_Semestral_DS_IV.Models.Modelos;


namespace Proyecto_Semestral_DS_IV.Models.DAO
{
    public class clsRoles
    {
        string connectionString;

        public clsRoles()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public void CrearRol(string nombreRol, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearRol", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreRol", nombreRol);

                    var outputParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 50)
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

        public void ActualizarRol(int idRol, string nombreRol, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarRol", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDRol", idRol);
                    command.Parameters.AddWithValue("@NombreRol", nombreRol);

                    var outputParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
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

        public void EliminarRol(int idRol, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarRol", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDRol", idRol);

                    var outputParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
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

        public List<Rol> MostrarRoles()
        {
            var roles = new List<Rol>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rol = new Rol
                            {
                                IDRol = (int)reader["IDRol"],
                                NombreRol = reader["NombreRol"].ToString()
                            };
                            roles.Add(rol);
                        }
                    }
                }
            }

            return roles;
        }
    }
}