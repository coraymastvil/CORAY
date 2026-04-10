using System.Collections.Generic;
using GymAppADO.Models;

namespace GymAppADO.Services
{
    public interface IMiembroService
    {
        void RegistrarMiembro(string nombre_completo, string cedula, string telefono);
        List<Miembro> ListarMiembros();
        Miembro BuscarPorCedula(string cedula);
        bool ActualizarTelefono(string cedula, string nuevo_telefono);
        bool EliminarMiembro(string cedula);
    }
}
