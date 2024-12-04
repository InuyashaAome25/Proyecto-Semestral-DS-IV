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
    public class clsAutor
    {
        string connectionString;

        public clsAutor()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }
        public void CrearAutor(string nombreAutor, string apellidoAutor, string biografia, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearAutor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreAutor", nombreAutor);
                    command.Parameters.AddWithValue("@ApellidoAutor", apellidoAutor);
                    command.Parameters.AddWithValue("@Biografia", biografia);

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

        public void ActualizarAutor(int idAutor, string nombreAutor, string apellidoAutor, string biografia, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarAutor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDAutor", idAutor);
                    command.Parameters.AddWithValue("@NombreAutor", nombreAutor);
                    command.Parameters.AddWithValue("@ApellidoAutor", apellidoAutor);
                    command.Parameters.AddWithValue("@Biografia", biografia);

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

        public void EliminarAutor(int idAutor, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarAutor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDAutor", idAutor);

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

        public List<Autor> MostrarAutores()
        {
            var autores = new List<Autor>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarAutores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var autor = new Autor
                            {
                                IDAutor = (int)reader["IDAutor"],
                                NombreAutor = reader["NombreAutor"].ToString(),
                                ApellidoAutor = reader["ApellidoAutor"].ToString(),
                                Biografia = reader["Biografia"].ToString()
                            };
                            autores.Add(autor);
                        }
                    }
                }
            }

            return autores;
        }
    }
}