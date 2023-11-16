
using Dapper;
using MySql.Data.MySqlClient;
using prueba_tecnica.Models;

namespace prueba_tecnica.Interfaces
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly MySQLConfiguration _connectionString;

        public UsuarioRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM usuario WHERE identificador = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = usuario.Id });

            return result > 0;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuario()
        {
            var db = dbConnection();

            var sql = @"SELECT usuario, fecha_creacion FROM usuario";

            return await db.QueryAsync<Usuario>(sql, new { });
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT identificador, usuario, fecha_creacion FROM usuario 
                        WHERE identificador = @Id";

            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }

        public async Task<bool> InsertUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO usuario ( identificador, usuario, pass, fecha_creacion) VALUES ( @Id , @User, @Pass, @Fecha_creacion) ";

            var result = await db.ExecuteAsync(sql, new { usuario.Id, usuario.User, usuario.Pass, usuario.Fecha_Creacion});

            return result > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @" UPDATE usuario ( usuario = @User, pass = @pass , fecha_creacion = @Fecha_creacion) WHERE identificador = @Id";

            var result = await db.ExecuteAsync(sql, new { usuario.User, usuario.Pass, usuario.Fecha_Creacion, usuario.Id });

            return result > 0;
        }
    }
}
