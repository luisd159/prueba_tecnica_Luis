using prueba_tecnica.Models;

namespace prueba_tecnica.Interfaces
{
    public interface IPersonasRepository
    {
        Task<IEnumerable<Personas>> GetAllPersonas();

        Task<Personas> GetPersona(int Id);

        Task<bool> InsertPersona(Personas personas);

        Task<bool> UpdatePersona(Personas personas);

        Task<bool> DeletePersona(Personas personas);
            

    }
}
