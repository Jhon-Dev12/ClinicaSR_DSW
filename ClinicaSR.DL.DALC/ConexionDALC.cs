using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace ClinicaSR.DL.DALC
{
    public class ConexionDALC
    {
        private static String cadenaBDHospital;
        private static String cadenaBDSeg;

        static ConexionDALC()
        {

            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).
                AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            cadenaBDSeg = config.GetConnectionString("BDSeguridad");
            cadenaBDHospital = config.GetConnectionString("BDTiendaCarro");
        }




        public static SqlConnection GetConnectionBDSeg()
        {
            return new SqlConnection(cadenaBDSeg);
        }

        public static SqlConnection GetConnectionBDTiendaCarros()
        {
            return new SqlConnection(cadenaBDHospital);
        }

    }
}
