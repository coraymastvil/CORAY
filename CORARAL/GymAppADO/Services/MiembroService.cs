using System.Collections.Generic;
using GymAppADO.Models;
using GymAppADO.Repository;

namespace GymAppADO.Services
{
    public class MiembroService : IMiembroService
    {
        private readonly IMiembroRepository _repository;

        public MiembroService(IMiembroRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarMiembro(string nombre_completo, string cedula, string telefono)
        {
            var miembro = new Miembro
            {
                NombreCompleto = nombre_completo,
                Cedula = cedula,
                Telefono = telefono
            };
            _repository.Add(miembro);
        }

        public List<Miembro> ListarMiembros()
        {
            return _repository.GetAll();
        }

        public Miembro BuscarPorCedula(string cedula)
        {
            return _repository.GetByCedula(cedula);
        }

        public bool ActualizarTelefono(string cedula, string nuevo_telefono)
        {
            var miembro = _repository.GetByCedula(cedula);
            if (miembro != null)
            {
                _repository.UpdateTelefono(cedula, nuevo_telefono);
                return true;
            }
            return false;
        }

        public bool EliminarMiembro(string cedula)
        {
            var miembro = _repository.GetByCedula(cedula);
            if (miembro != null)
            {
                _repository.Delete(cedula);
                return true;
            }
            return false;
        }
    }
}
