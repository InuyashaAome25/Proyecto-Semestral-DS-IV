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

        public void CrearLibro(string titulo, string isbn, string editorial, DateTime fechaPublicacion, string descripcionLibro, int stock, int genero, int autor, out string mensaje)
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
                    command.Parameters.AddWithValue("@Stock", stock);  // Añadir el parámetro @Stock
                    command.Parameters.AddWithValue("@GeneroID", genero);
                    command.Parameters.AddWithValue("@AutorID", autor);

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
                                Stock = (int)reader["Stock"], // Conversión corregida
                                Genero = (int)reader["Genero"],
                                Autor = (int)reader["Autor"],
                            };
                            libros.Add(libro);
                        }
                    }
                }
            }

            return libros;
        }
        public List<Genero> ObtenerGeneros()
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
                                IDGenero = (int)reader["Id"],
                                NombreGenero = reader["NombreGenero"].ToString()
                            };
                            generos.Add(genero);
                        }
                    }
                }
            }

            return generos;
        }

        public List<Autor> ObtenerAutores()
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
                                IDAutor = (int)reader["Id"],
                                NombreAutor = reader["NombreAutor"].ToString()
                            };
                            autores.Add(autor);
                        }
                    }
                }
            }

            return autores;
        }

        public Libro ObtenerLibroPorId(int id)
        {
            Libro libro = null;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ObtenerLibroPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDLibros", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            libro = new Libro
                            {
                                IDLibros = (int)reader["IDLibros"],
                                Titulo = reader["Titulo"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                Editorial = reader["Editorial"].ToString(),
                                FechaPublicacion = (DateTime)reader["FechaPublicacion"],
                                DescripcionLibro = reader["DescripcionLibro"].ToString(),
                                Stock = (int)reader["Stock"],
                                Genero = (int)reader["Genero"],
                                Autor = (int)reader["Autor"]
                            };
                        }
                    }
                }
            }

            return libro;
        }
    }
}