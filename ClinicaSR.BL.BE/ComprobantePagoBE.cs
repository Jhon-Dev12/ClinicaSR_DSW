using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicaSR.BL.BE
{
    public class ComprobantePagoBE
    {
        public long ID_Comprobante { get; set; }
        public CitaBE CitaBE { get; set; }
        public long Id_Usuario { get; set; }
        public string Nombre_Pagador { get; set; }
        public string Apellidos_Pagador { get; set; }
        public string DNI_Pagador { get; set; }
        public string Contacto_Pagador { get; set; }
        public DateTime Fecha_Emision { get; set; }
        public Decimal Monto { get; set; }
        public MetodoPago Metodo_Pago { get; set; }
        public EstadoComprobante Estado { get; set; }
    }

    public enum MetodoPago
    {
        EFECTIVO,
        TARJETA,
        TRANSFERENCIA
    }

    public enum EstadoComprobante
    {
        EMITIDO,
        ANULADO
    }
}
