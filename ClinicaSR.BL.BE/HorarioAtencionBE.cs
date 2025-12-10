using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicaSR.BL.BE
{
    public class HorarioAtencionBE
    {
        public int ID_Horario {  get; set; } 
        public MedicoBE MedicoBE { get; set; }  
        public Dias Dia_Semana { get;set;  }
        public DateTime Horario_Entrada {  get; set; }
        public DateTime Horario_Salida {  get; set; }

    }

    public enum Dias
    {
        LUNES, 
        MARTES,
        MIERCOLES, 
        JUEVES, 
        VIERNES,
        SABADO,
        DOMINGO
    }
}
