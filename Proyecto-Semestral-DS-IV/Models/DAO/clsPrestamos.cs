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
    public class clsPrestamos
    {
        string connectionString;

        public clsPrestamos()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public void CrearPrestamo(DateTime fechaDevolucion, int libroID, int usuarioID, int administradorID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearPrestamo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FechaDevoluvion", fechaDevolucion);
                    command.Parameters.AddWithValue("@LibroID", libroID);
                    command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    command.Parameters.AddWithValue("@AdministradorID", administradorID);

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

        public void ActualizarPrestamo(int idPrestamos, DateTime fechaDevolucion, int libroID, int usuarioID, int administradorID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarPrestamo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPrestamos", idPrestamos);
                    command.Parameters.AddWithValue("@FechaDevoluvion", fechaDevolucion);
                    command.Parameters.AddWithValue("@LibroID", libroID);
                    command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    command.Parameters.AddWithValue("@AdministradorID", administradorID);

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

        public void EliminarPrestamo(int idPrestamos, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarPrestamo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPrestamos", idPrestamos);

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

        public List<Prestamo> MostrarPrestamos()
        {
            var prestamos = new List<Prestamo>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarPrestamos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prestamo = new Prestamo
                            {
                                IDPrestamos = (int)reader["IDPrestamos"],
                                FechaPrestamo = (DateTime)reader["FechaPrestamo"],
                                FechaDevoluvion = (DateTime)reader["FechaDevoluvion"],
                                LibroTitulo = reader["LibroTitulo"].ToString(),
                                UsuarioNombre = reader["UsuarioNombre"].ToString(),
                                AdministradorNombre = reader["AdministradorNombre"].ToString()
                            };
                            prestamos.Add(prestamo);
                        }
                    }
                }
            }

            return prestamos;
        }
    }
}