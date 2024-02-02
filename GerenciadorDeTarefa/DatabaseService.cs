using System.Data.SqlClient;

namespace GerenciadorDeTarefa
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexaoPadrao");
        }

        public SqlConnection GetOpenConnection()
        {
            SqlConnection conexao = new SqlConnection(_connectionString);
            conexao.Open();
            return conexao;
        }
    }
}
