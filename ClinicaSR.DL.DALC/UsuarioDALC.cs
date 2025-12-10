using ClinicaSR.BL.BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClinicaSR.DL.DALC
{
    public class UsuarioDALC
    {
        public List<UsuarioBE> listaUsuarios()
        {

            SqlConnection con = new SqlConnection();
            con = ConexionDALC.GetConnectionBDSeg();
            List<UsuarioBE> listaUsuarios = new List<UsuarioBE>();
            // Inicializamos la conexi  ón SQL
            //SqlConnection con = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("ObtenerUsuariosResumen", con);
            SqlDataReader dr = null;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Abrir la conexión antes de ejecutar el SqlDataReader
                con.Open();

                // Ejecutar el comando para obtener el SqlDataReader
                dr = cmd.ExecuteReader();

                // Leer los datos
                while (dr.Read())
                {


                    // Asegurarnos de que las propiedades de EstadoBE y RolBE no sean null
                    UsuarioBE usuario = new UsuarioBE
                    {
                        Username = dr.GetString(0),
                        Nombres = dr.GetString (1),
                        Apellidos  = dr.GetString(2),   
                    };

                    // Agregar el usuario a la lista
                    listaUsuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                // Aquí capturamos el error y lo imprimimos
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                // Asegúrate de cerrar el SqlDataReader si fue abierto
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }


            return listaUsuarios;
        }




    }
}
