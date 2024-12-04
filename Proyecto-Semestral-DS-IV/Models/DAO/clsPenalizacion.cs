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
    public class clsPenalizacion
    {
        string connectionString;

        public clsPenalizacion()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public void CrearPenalizacion(int prestamoID, int usuarioID, decimal montoPenalizacion, string descripcionPenalizacion, int administradorID, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CrearPenalizacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PrestamoID", prestamoID);
                    command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    command.Parameters.AddWithValue("@MontoPenalizacion", montoPenalizacion);
                    command.Parameters.AddWithValue("@DescripcionPenalizacion", descripcionPenalizacion);
                    command.Parameters.AddWithValue("@AdministradorID", administradorID);

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

        public void ActualizarPenalizacion(int idPenalizacion, decimal montoPenalizacion, string descripcionPenalizacion, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ActualizarPenalizacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPenalizacion", idPenalizacion);
                    command.Parameters.AddWithValue("@MontoPenalizacion", montoPenalizacion);
                    command.Parameters.AddWithValue("@DescripcionPenalizacion", descripcionPenalizacion);

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

        public void EliminarPenalizacion(int idPenalizacion, out string mensaje)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("EliminarPenalizacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDPenalizacion", idPenalizacion);

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

        public List<Penalizacion> MostrarPenalizaciones()
        {
            var penalizaciones = new List<Penalizacion>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("MostrarPenalizaciones", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var penalizacion = new Penalizacion
                            {
                                IDPenalizacion = (int)reader["IDPenalizacion"],
                                FechaPrestamo = (DateTime)reader["FechaPrestamo"],
                                FechaDevoluvion = (DateTime)reader["FechaDevoluvion"],
                                UsuarioNombre = reader["UsuarioNombre"].ToString(),
                                AdministradorNombre = reader["AdministradorNombre"].ToString(),
                                MontoPenalizacion = (decimal)reader["MontoPenalizacion"],
                                DescripcionPenalizacion = reader["DescripcionPenalizacion"].ToString()
                            };
                            penalizaciones.Add(penalizacion);
                        }
                    }
                }
            }

            return penalizaciones;
        }
    }
}