
using Dapper;
using MySql.Data.MySqlClient;
using prueba_tecnica.Models;
using System.Data;

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

            var result = await db.ExecuteAsync(sql, new { Id = usuario.Identificador });

            return result > 0;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuario()
        {
            var db = dbConnection();

            var sql = @"CALL getallusuarios";

            return await db.QueryAsync<Usuario>(sql, new { });
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT identificador, user, fecha_creacion FROM usuario 
                        WHERE identificador = @Id";

            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }

        public async Task<bool> InsertUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@id", usuario.Identificador);
            parameters.Add("@usuario", usuario.User);
            parameters.Add("@pass", usuario.Pass);
            parameters.Add("@fecha", usuario.Fecha_Creacion);

            var result = await db.ExecuteAsync("insertusuario", parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@id", usuario.Identificador);
            parameters.Add("@usuario", usuario.User);
            parameters.Add("@pass", usuario.Pass);
            parameters.Add("@fecha", usuario.Fecha_Creacion);

            var result = await db.ExecuteAsync("updateusuario", parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }
}
