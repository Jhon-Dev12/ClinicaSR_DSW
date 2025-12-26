using System.ComponentModel.DataAnnotations;

namespace ClinicaSR.PL.WebApp.Models
{
    public class EspecialidadViewModel
    {
        public int ID_Especialidad { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; }
    }
}
