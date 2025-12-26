using ClinicaSR.BL.BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicaSR.DL.DALC
{
    public class ComprobantePagoDALC
    {
        // Listar todos los comprobantes
        public List<ComprobantePagoBE> ListarComprobantes()
        {
            List<ComprobantePagoBE> lista = new List<ComprobantePagoBE>();

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Listar_Comprobantes", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ComprobantePagoBE
                            {
                                ID_Comprobante = dr.GetInt64(dr.GetOrdinal("ID_Comprobante")),
                                CitaBE = new CitaBE { ID_Cita = dr.GetInt64(dr.GetOrdinal("ID_Cita")) },
                                Id_Usuario = dr.GetInt64(dr.GetOrdinal("ID_Usuario")),
                                Nombre_Pagador = dr["Nombre_Pagador"].ToString(),
                                Apellidos_Pagador = dr["Apellidos_Pagador"].ToString(),
                                DNI_Pagador = dr["DNI_Pagador"] == DBNull.Value ? null : dr["DNI_Pagador"].ToString(),
                                Contacto_Pagador = dr["Contacto_Pagador"] == DBNull.Value ? null : dr["Contacto_Pagador"].ToString(),
                                Fecha_Emision = Convert.ToDateTime(dr["Fecha_Emision"]),
                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Metodo_Pago = Enum.Parse<MetodoPago>(dr["Metodo_Pago"].ToString()),
                                Estado = Enum.Parse<EstadoComprobante>(dr["Estado"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en ComprobantePagoDALC.ListarComprobantes: " + ex.Message);
                }
            }

            return lista;
        }

        // Registrar un comprobante
        public bool Registrar(ComprobantePagoBE obj)
        {
            bool exito = false;

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Registrar_Comprobante", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ID_Cita", obj.CitaBE.ID_Cita);
                cmd.Parameters.AddWithValue("@ID_Usuario", obj.Id_Usuario);
                cmd.Parameters.AddWithValue("@Nombre_Pagador", obj.Nombre_Pagador);
                cmd.Parameters.AddWithValue("@Apellidos_Pagador", obj.Apellidos_Pagador);
                cmd.Parameters.AddWithValue("@DNI_Pagador", obj.DNI_Pagador ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Contacto_Pagador", obj.Contacto_Pagador ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Monto", obj.Monto);
                cmd.Parameters.AddWithValue("@Metodo_Pago", obj.Metodo_Pago.ToString());

                SqlParameter output = new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(output);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    exito = Convert.ToBoolean(output.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en ComprobantePagoDALC.Registrar: " + ex.Message);
                }
            }

            return exito;
        }

        // Anular comprobante
        public bool Anular(long idComprobante)
        {
            bool exito = false;

            using (SqlConnection con = ConexionDALC.GetConnectionBDHospital())
            {
                SqlCommand cmd = new SqlCommand("USP_Anular_Comprobante", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ID_Comprobante", idComprobante);

                SqlParameter output = new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(output);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    exito = Convert.ToBoolean(output.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en ComprobantePagoDALC.Anular: " + ex.Message);
                }
            }

            return exito;
        }
    }
}