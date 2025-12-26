using ClinicaSR.BL.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSR.PL.WebApp.ViewModels
{
    public class ComprobantePagoViewModel
    {
        public long ID_Comprobante { get; set; }

        [Display(Name = "Cita")]
        [Required(ErrorMessage = "Debe seleccionar una cita")]
        public long ID_Cita { get; set; }

        public List<CitaBE> CitasPendientes { get; set; } = new List<CitaBE>();

        public long ID_Usuario { get; set; }

        [Display(Name = "Nombre del Pagador")]
        [Required(ErrorMessage = "Nombre obligatorio")]
        public string Nombre_Pagador { get; set; }

        [Display(Name = "Apellidos del Pagador")]
        [Required(ErrorMessage = "Apellidos obligatorios")]
        public string Apellidos_Pagador { get; set; }

        [Display(Name = "DNI")]
        public string DNI_Pagador { get; set; }

        [Display(Name = "Contacto")]
        public string Contacto_Pagador { get; set; }

        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Monto obligatorio")]
        public decimal Monto { get; set; }

        [Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        public MetodoPago Metodo_Pago { get; set; }

        public EstadoComprobante Estado { get; set; } = EstadoComprobante.EMITIDO;

        public DateTime Fecha_Emision { get; set; } = DateTime.Now;
    }
}