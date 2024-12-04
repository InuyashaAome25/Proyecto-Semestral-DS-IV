using Proyecto_Semestral_DS_IV.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.DAO
{
    public class clsPopularidad
    {
        string connectionString;

        public clsPopularidad()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }
        public void CrearPopularidad(int popularidad, int libroID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearPopularidad", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Popularidad", popularidad);
                    command.Parameters.AddWithValue("@LibroID", libroID);

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

        public void ActualizarPopularidad(int idPopularidad, int popularidad, int vecesPrestado, int vecesBuscado, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarPopularidad", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPopularidad", idPopularidad);
                    command.Parameters.AddWithValue("@Popularidad", popularidad);
                    command.Parameters.AddWithValue("@VecesPrestado", vecesPrestado);
                    command.Parameters.AddWithValue("@VecesBuscado", vecesBuscado);

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

        public void IncrementarPrestamo(int libroID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("IncrementarPrestamo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LibroID", libroID);

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

        public void IncrementarBusqueda(int libroID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("IncrementarBusqueda", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LibroID", libroID);

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

        public void EliminarPopularidad(int idPopularidad, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarPopularidad", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPopularidad", idPopularidad);

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

        public List<Popularidad> MostrarPopularidad()
        {
            var popularidades = new List<Popularidad>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarPopularidad", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var popularidad = new Popularidad
                            {
                                IDPopularidad = (int)reader["IDPopularidad"],
                                Popularidades = (int)reader["Popularidad"],
                                VecesPrestado = (int)reader["VecesPrestado"],
                                VecesBuscado = (int)reader["VecesBuscado"],
                                LibroTitulo = reader["LibroTitulo"].ToString(),
                                ISBN = reader["ISBN"].ToString()
                            };
                            popularidades.Add(popularidad);
                        }
                    }
                }
            }

            return popularidades;
        }
    }
}