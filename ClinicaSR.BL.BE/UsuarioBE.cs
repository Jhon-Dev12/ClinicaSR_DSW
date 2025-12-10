using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ClinicaSR.BL.BE
{
    public class UsuarioBE
    {
      
        public int Id_Usuario {  get; set; }
        public string Username { get; set; }
        public string Contrasenia { get; set; }                                                                 
        public string Nombres { get; set; }                                                                      
        public string Apellidos {  get; set; }                                                                      
        public string DNI { get; set; }                                                                   
        public string Telefono {  get; set; }                                                                     
        public string Img_Perfil {  get; set; }                                                                    
        public string Correo {  get; set; }     
        //ESTO SALE DEL ENUM
        public Rol rol { get; set; }                                   


    }



    public enum Rol{
        Administrador,
        Recepcionista,
        Cajero
    }
}
