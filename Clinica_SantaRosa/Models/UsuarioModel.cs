using ClinicaSR.BL.BC;
using ClinicaSR.BL.BE;

namespace Clinica_SantaRosa.PL.WebApp.Models
{
    public class UsuarioModel
    {
        private UsuarioBC usu = new UsuarioBC();

        public List<UsuarioBE> listaUsuarios()
        {
            return usu.ListarUsuarios();
        }

    }
}
