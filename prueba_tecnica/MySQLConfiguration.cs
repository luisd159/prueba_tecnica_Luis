namespace prueba_tecnica
{
    public class MySQLConfiguration
    {
        public string ConnectionString {  get; set; }

        public MySQLConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
