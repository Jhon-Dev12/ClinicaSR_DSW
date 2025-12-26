using ClinicaSR.BL.BC;
using ClinicaSR.BL.BE;
using ClinicaSR.DL.DALC;
using System.Collections.Generic;

namespace ClinicaSR.PL.WebApp.Models
{
    public class EspecialidadModel
    {
        private EspecialidadBC especialidadBC = new EspecialidadBC();

        public List<EspecialidadBE> Listar()
        {
            return especialidadBC.ListarEspecialidades();
        }

        public int Registrar(EspecialidadBE especialidad)
        {
            return especialidadBC.InsertarEspecialidad(especialidad);
        }

        public bool Actualizar(EspecialidadBE especialidad)
        {
            return especialidadBC.ActualizarEspecialidad(especialidad);
        }

        public bool Eliminar(int id)
        {
            return especialidadBC.EliminarEspecialidad(id);
        }
    }
}