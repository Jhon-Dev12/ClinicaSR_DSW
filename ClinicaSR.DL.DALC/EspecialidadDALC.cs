using ClinicaSR.BL.BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClinicaSR.DL.DALC
{
    public class EspecialidadDALC
    {
        public List<EspecialidadBE> ListarEspecialidades()
        {
            List<EspecialidadBE> lista = new List<EspecialidadBE>();

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Listar_Especialidades", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EspecialidadBE esp = new EspecialidadBE
                            {
                                ID_Especialidad = Convert.ToInt32(dr["ID_Especialidad"]),
                                Nombre = dr["Nombre"].ToString()
                            };

                            lista.Add(esp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error DALC - ListarEspecialidades", ex);
                }
            }

            return lista;
        }

        // 2. INSERTAR
        public int Insertar(EspecialidadBE especialidadBE)
        {
            int idInsertado = 0;

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Insertar_Especialidad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100)
                               .Value = especialidadBE.Nombre;

                SqlParameter salida = new SqlParameter("@ID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(salida);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (salida.Value != DBNull.Value)
                        idInsertado = Convert.ToInt32(salida.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error DALC - InsertarEspecialidad", ex);
                }
            }

            return idInsertado;
        }

        // 3. ELIMINAR
        public bool Eliminar(int id)
        {
            bool eliminado = false;

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Eliminar_Especialidad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                SqlParameter result = new SqlParameter("@Result", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(result);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (result.Value != DBNull.Value)
                        eliminado = Convert.ToBoolean(result.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error DALC - EliminarEspecialidad", ex);
                }
            }

            return eliminado;
        }

        // 4. ACTUALIZAR
        public bool Actualizar(EspecialidadBE especialidadBE)
        {
            bool actualizado = false;

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Actualizar_Especialidad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int)
                               .Value = especialidadBE.ID_Especialidad;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100)
                               .Value = especialidadBE.Nombre;

                SqlParameter result = new SqlParameter("@Result", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(result);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (result.Value != DBNull.Value)
                        actualizado = Convert.ToBoolean(result.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error DALC - ActualizarEspecialidad", ex);
                }
            }

            return actualizado;
        }

    }
}