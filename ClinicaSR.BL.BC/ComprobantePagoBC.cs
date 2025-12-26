using ClinicaSR.BL.BE;
using ClinicaSR.DL.DALC;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicaSR.BL.BC
{
    public class ComprobantePagoBC
    {
        private readonly ComprobantePagoDALC _dalc;
        private readonly string _connectionString;

        // Constructor vacío: inicializa DALC y connection string
        public ComprobantePagoBC()
        {
            _dalc = new ComprobantePagoDALC();

            // Carga la conexión directamente desde appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = config.GetConnectionString("BDHospital");
        }

        // Constructor con IConfiguration (opcional)
        public ComprobantePagoBC(IConfiguration config)
        {
            _dalc = new ComprobantePagoDALC();
            _connectionString = config.GetConnectionString("BDHospital");
        }

        // Resto de métodos sin tocar
        public List<ComprobantePagoBE> ListarComprobantes() => _dalc.ListarComprobantes();

        public bool RegistrarComprobante(ComprobantePagoBE obj)
        {
            if (obj == null)
                throw new Exception("El objeto ComprobantePago no puede ser nulo.");

            if (obj.CitaBE == null || obj.CitaBE.ID_Cita <= 0)
                throw new Exception("Debe asignar una cita válida al comprobante.");

            if (string.IsNullOrWhiteSpace(obj.Nombre_Pagador))
                throw new Exception("El nombre del pagador es obligatorio.");

            if (string.IsNullOrWhiteSpace(obj.Apellidos_Pagador))
                throw new Exception("Los apellidos del pagador son obligatorios.");

            if (obj.Monto <= 0)
                throw new Exception("El monto del comprobante debe ser mayor a cero.");

            bool ok = _dalc.Registrar(obj);

            if (!ok)
                throw new Exception("No se pudo registrar el comprobante. La cita ya tiene un comprobante registrado.");

            return ok;
        }

        public bool AnularComprobante(long idComprobante)
        {
            if (idComprobante <= 0)
                throw new Exception("El ID de comprobante no es válido.");

            bool ok = _dalc.Anular(idComprobante);

            if (!ok)
                throw new Exception("No se pudo anular el comprobante. Ya estaba anulado o no existe.");

            return ok;
        }

        public List<CitaBE> ObtenerCitasPendientes()
        {
            var lista = new List<CitaBE>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_Listar_Citas_Pendientes", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new CitaBE
                        {
                            ID_Cita = dr.GetInt64(dr.GetOrdinal("ID_Cita")),
                            PacienteBE = new PacienteBE
                            {
                                Nombres = dr.GetString(dr.GetOrdinal("Paciente"))
                            },
                            MedicoBE = new MedicoBE
                            {
                                Nombres = dr.GetString(dr.GetOrdinal("Medico"))
                            },
                            Fecha_Cita = dr.GetDateTime(dr.GetOrdinal("Fecha_Cita")),
                            Hora_Cita = dr.GetTimeSpan(dr.GetOrdinal("Hora_Cita")),
                            Motivo = dr.GetString(dr.GetOrdinal("Motivo")),
                            Estado = EstadoCita.PENDIENTE
                        });
                    }
                }
            }

            return lista;
        }
    }
}
