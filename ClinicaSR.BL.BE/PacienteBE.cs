using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicaSR.BL.BE
{
    public class PacienteBE
    {
        public int ID_Paciente {  get; set; }
        
        public string Nombres {  get; set; }
        public string Apellidos { get; set; }       
        public string DNI { get; set; }
	    public DateTime Fecha_nacimiento {  get; set; }
        public string Telefono { get; set; }



    }
}
