using prueba_tecnica.Models;

namespace prueba_tecnica.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuario();

        Task<Usuario> GetUsuario(int Id);

        Task<bool> InsertUsuario(Usuario usuario);

        Task<bool> UpdateUsuario(Usuario usuario);

        Task<bool> DeleteUsuario(Usuario usuario);
    }
}
