
using Dapper;
using MySql.Data.MySqlClient;
using prueba_tecnica.Models;
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

            var result = await db.ExecuteAsync(sql, new { Id = personas.Id });

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

            var sql = @"CALL insertpersonas( @Id, @Nombre, @Apellido, @Numero, @Email, @Tipo, @Fecha)";

            var parameters = new
            {
                Id = personas.Id,
                Nombre = personas.Nombre,
                Apellido = personas.Apellido,
                Numero = personas.Numero_Identificacion,
                Email = personas.Email,
                Tipo = personas.Tipo_Identificacion,
                Fecha = personas.Fecha_Creacion
            };

            var result = await db.ExecuteAsync(sql, parameters);

            return result > 0;

        }

        public async Task<bool> UpdatePersona(Personas personas)
        {
            var db = dbConnection();

            var sql = @" UPDATE personas ( nombre = @Nombre, 
                                           apellido = @Apellido, 
                                           numero_identificacion = @Numero_identificacion, 
                                           email = @Email, 
                                           tipo_identificacion = @Tipo_identificacion, 
                                           fecha_creacion = @Fecha_creacion) WHERE identificador = @Id";
    
            var result = await db.ExecuteAsync(sql, new {   personas.Nombre, 
                                                            personas.Apellido, 
                                                            personas.Numero_Identificacion, 
                                                            personas.Email, 
                                                            personas.Tipo_Identificacion, 
                                                            personas.Fecha_Creacion,
                                                            personas.Id});

            return result > 0;
        }
    }
}
