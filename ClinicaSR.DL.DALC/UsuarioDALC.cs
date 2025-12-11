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
                con.Open(); 
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {


   
                    UsuarioBE usuario = new UsuarioBE
                    {
                        Username = dr.GetString(0),
                        Nombres = dr.GetString (1),
                        Apellidos  = dr.GetString(2),   
                    };


                    listaUsuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }


            return listaUsuarios;
        }




    }
}
