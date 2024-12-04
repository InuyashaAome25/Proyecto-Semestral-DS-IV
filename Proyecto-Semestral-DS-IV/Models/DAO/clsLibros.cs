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
    public class clsLibros
    {
        string connectionString;

        public clsLibros()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public void CrearLibro(string titulo, string isbn, string editorial, DateTime fechaPublicacion, string descripcionLibro, int stock, int generoID, int autorID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearLibro", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Titulo", titulo);
                    command.Parameters.AddWithValue("@ISBN", isbn);
                    command.Parameters.AddWithValue("@Editorial", editorial);
                    command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
                    command.Parameters.AddWithValue("@DescripcionLibro", descripcionLibro);
                    command.Parameters.AddWithValue("@@Stock", stock);
                    command.Parameters.AddWithValue("@GeneroID", generoID);
                    command.Parameters.AddWithValue("@AutorID", autorID);

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

        public void ActualizarLibro(int idLibros, string titulo, string isbn, string editorial, DateTime fechaPublicacion, string descripcionLibro, int stock, int generoID, int autorID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarLibro", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDLibros", idLibros);
                    command.Parameters.AddWithValue("@Titulo", titulo);
                    command.Parameters.AddWithValue("@ISBN", isbn);
                    command.Parameters.AddWithValue("@Editorial", editorial);
                    command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
                    command.Parameters.AddWithValue("@DescripcionLibro", descripcionLibro);
                    command.Parameters.AddWithValue("@@Stock", stock);
                    command.Parameters.AddWithValue("@GeneroID", generoID);
                    command.Parameters.AddWithValue("@AutorID", autorID);

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

        public void EliminarLibro(int idLibros, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarLibro", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDLibros", idLibros);

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

        public List<Libro> MostrarLibros()
        {
            var libros = new List<Libro>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarLibros", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var libro = new Libro
                            {
                                IDLibros = (int)reader["IDLibros"],
                                Titulo = reader["Titulo"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                Editorial = reader["Editorial"].ToString(),
                                FechaPublicacion = (DateTime)reader["FechaPublicacion"],
                                DescripcionLibro = reader["DescripcionLibro"].ToString(),
                                Stock = reader["Stock"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                Autor = reader["Autor"].ToString()
                            };
                            libros.Add(libro);
                        }
                    }
                }
            }

            return libros;
        }
    }
}