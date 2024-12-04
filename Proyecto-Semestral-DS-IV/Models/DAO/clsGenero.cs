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
    public class clsGenero
    {
        string connectionString;

        public clsGenero()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public void CrearGenero(string nombreGenero, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearGenero", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreGenero", nombreGenero);

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

        public void ActualizarGenero(int idGenero, string nombreGenero, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarGenero", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDGenero", idGenero);
                    command.Parameters.AddWithValue("@NombreGenero", nombreGenero);

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

        public void EliminarGenero(int idGenero, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarGenero", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDGenero", idGenero);

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

        public List<Genero> MostrarGeneros()
        {
            var generos = new List<Genero>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarGeneros", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var genero = new Genero
                            {
                                IDGenero = (int)reader["IDGenero"],
                                NombreGenero = reader["NombreGenero"].ToString()
                            };
                            generos.Add(genero);
                        }
                    }
                }
            }

            return generos;
        }
    }
}