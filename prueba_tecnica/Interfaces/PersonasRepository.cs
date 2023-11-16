
using Dapper;
using MySql.Data.MySqlClient;
using prueba_tecnica.Models;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace prueba_tecnica.Interfaces
{
    public class PersonasRepository : IPersonasRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public PersonasRepository(MySQLConfiguration connectionString )
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeletePersona(Personas personas)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM personas WHERE identificador = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = personas.Identificador });

            return result > 0;

        }

        public async Task<IEnumerable<Personas>> GetAllPersonas()
        {
            var db = dbConnection();

            var sql = @"CALL getallpersonas";

            return await db.QueryAsync<Personas>(sql, new { });
        }

        public async Task<Personas> GetPersona(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT identificador, nombre, apellido, numero_identificacion, email, tipo_identificacion, fecha_creacion, cnombre, cidentificacion FROM personas 
                        WHERE identificador = @Id";

            return await db.QueryFirstOrDefaultAsync<Personas>(sql, new { Id = id });
        }

        public async Task<bool> InsertPersona(Personas personas)
        {
            var db = dbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@id", personas.Identificador);
            parameters.Add("@nombre", personas.Nombre);
            parameters.Add("@apellido", personas.Apellido);
            parameters.Add("@numero", personas.Numero_Identificacion);
            parameters.Add("@email", personas.Email);
            parameters.Add("@tipo", personas.Tipo_Identificacion);
            parameters.Add("@fecha", personas.Fecha_Creacion);

            var result = await db.ExecuteAsync("insertpersonas", parameters, commandType: CommandType.StoredProcedure);

            return result > 0;

        }

        public async Task<bool> UpdatePersona(Personas personas)
        {
            var db = dbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@id", personas.Identificador);
            parameters.Add("@nombre", personas.Nombre);
            parameters.Add("@apellido", personas.Apellido);
            parameters.Add("@numero", personas.Numero_Identificacion);
            parameters.Add("@email", personas.Email);
            parameters.Add("@tipo", personas.Tipo_Identificacion);
            parameters.Add("@fecha", personas.Fecha_Creacion);

            var result = await db.ExecuteAsync("updatepersonas", parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }
}
