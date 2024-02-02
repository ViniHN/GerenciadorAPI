using GerenciadorDeTarefa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciadorDeTarefa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public TarefaController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string queryCreate = "INSERT INTO exemplo (titulo, descricao, DataCriacao, StatusTarefa) VALUES (@titulo, @descricao, @DataCriacao, @StatusTarefa)";
            var cmd = new SqlCommand(queryCreate, conexao);

            cmd.Parameters.AddWithValue("@titulo", tarefa.Titulo);
            cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            cmd.Parameters.AddWithValue("@DataCriacao", tarefa.Data);
            cmd.Parameters.AddWithValue("@StatusTarefa", tarefa.Status);

            cmd.ExecuteNonQuery();

            return Ok();
            //return CreatedAtAction(nameof(ObterPorId), new {id = tarefa.Id}, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Tarefa tarefabanco)
        {
            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string queryCreate = "UPDATE exemplo SET titulo = @titulo, descricao = @descricao, DataCriacao = @DataCriacao, StatusTarefa = @StatusTarefa WHERE id = @id";
            var cmd = new SqlCommand(queryCreate, conexao);

            cmd.Parameters.AddWithValue("@id", tarefabanco.Id);
            cmd.Parameters.AddWithValue("@titulo", tarefabanco.Titulo);
            cmd.Parameters.AddWithValue("@descricao", tarefabanco.Descricao);
            cmd.Parameters.AddWithValue("@DataCriacao", tarefabanco.Data);
            cmd.Parameters.AddWithValue("@StatusTarefa", tarefabanco.Status);

            cmd.ExecuteNonQuery();

            conexao.Close();

            return Ok(tarefabanco);

        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(Tarefa tarefa)
        {
            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string queryCreate = "DELETE FROM exemplo WHERE id = @id";
            var cmd = new SqlCommand(queryCreate, conexao);

            cmd.Parameters.AddWithValue("@id", tarefa.Id);

            cmd.ExecuteNonQuery();
            conexao.Close();
            return NoContent();
        }

        [HttpGet("obter-por-id")]
        public IActionResult Get(int id)
        {
            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string queryCreate = "SELECT * FROM exemplo WHERE id = @id";
            var cmd = new SqlCommand(queryCreate, conexao);

            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dados = cmd.ExecuteReader();

            Tarefa tarefa = new Tarefa();

            while (dados.Read())
            {
                for (int i = 0;i< dados.FieldCount;i++)
                {
                    if(dados.GetName(i) == "Id")
                    {
                        tarefa.Id = dados.GetInt32(i);
                    }

                    else if (dados.GetName(i) == "Titulo")
                    {
                        tarefa.Titulo = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "Descricao")
                    {
                        tarefa.Descricao = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "DataCriacao")
                    {
                        tarefa.Data = dados.GetDateTime(i);
                    }

                    else if (dados.GetName(i) == "StatusTarefa")
                    {
                        tarefa.Status = (EnumStatusTarefa)dados.GetValue(i);
                    }
                }
            }

            conexao.Close();
            return Ok(tarefa);
        }

        [HttpGet("obter-por-titulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {

            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string querySelect = "SELECT * FROM exemplo WHERE titulo = @titulo";
            var cmd = new SqlCommand(querySelect, conexao);

            cmd.Parameters.AddWithValue("@titulo", titulo);
            SqlDataReader dados = cmd.ExecuteReader();

            Tarefa tarefa = new Tarefa();

            while (dados.Read())
            {

                for (int i = 0; i < dados.FieldCount; i++)
                {

                    if (dados.GetName(i) == "Id")
                    {
                        tarefa.Id = dados.GetInt32(i);
                    }

                    else if (dados.GetName(i) == "Titulo")
                    {
                        tarefa.Titulo = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "Descricao")
                    {
                        tarefa.Descricao = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "DataCriacao")
                    {
                        tarefa.Data = dados.GetDateTime(i);
                    }

                    else if (dados.GetName(i) == "StatusTarefa")
                    {
                        tarefa.Status = (EnumStatusTarefa)dados.GetValue(i);
                    }
                }

            }

            conexao.Close();

            return Ok(tarefa);
        }

        [HttpGet("obter-por-data")]
        public IActionResult ObterPorData(DateTime data)
        {
            Tarefa tarefa = new Tarefa();

            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string querySelect = "SELECT * FROM exemplo WHERE DataCriacao = @DataCriacao";
            var cmd = new SqlCommand(querySelect, conexao);

            cmd.Parameters.AddWithValue("@DataCriacao", tarefa.Data);
            SqlDataReader dados = cmd.ExecuteReader();

            while (dados.Read())
            {
                for (int i = 0; i < dados.FieldCount; i++)
                {

                    if (dados.GetName(i) == "Id")
                    {
                        tarefa.Id = dados.GetInt32(i);
                    }

                    else if (dados.GetName(i) == "Titulo")
                    {
                        tarefa.Titulo = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "Descricao")
                    {
                        tarefa.Descricao = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "DataCriacao")
                    {
                        tarefa.Data = dados.GetDateTime(i);
                    }

                    else if (dados.GetName(i) == "StatusTarefa")
                    {
                        tarefa.Status = (EnumStatusTarefa)dados.GetValue(i);
                    }
                }
            }

            conexao.Close();

            return Ok(tarefa);
        }


        [HttpGet("obter-por-status")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            Tarefa tarefa = new Tarefa();

            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string querySelect = "SELECT * FROM exemplo WHERE StatusTarefa = @StatusTarefa";
            var cmd = new SqlCommand(querySelect, conexao);

            cmd.Parameters.AddWithValue("@StatusTarefa", status);
            SqlDataReader dados = cmd.ExecuteReader();

            while (dados.Read())
            {


                for (int i = 0; i < dados.FieldCount; i++)
                {

                    if (dados.GetName(i) == "Id")
                    {
                        tarefa.Id = dados.GetInt32(i);
                    }

                    else if (dados.GetName(i) == "Titulo")
                    {
                        tarefa.Titulo = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "Descricao")
                    {
                        tarefa.Descricao = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "DataCriacao")
                    {
                        tarefa.Data = dados.GetDateTime(i);
                    }

                    else if (dados.GetName(i) == "StatusTarefa")
                    {
                        tarefa.Status = (EnumStatusTarefa)dados.GetValue(i);
                    }
                }
            }
            conexao.Close();


            return Ok(tarefa);
        }

        [HttpGet("obter-todos")]
        public IActionResult ObterTodos()
        {
            using SqlConnection conexao = _databaseService.GetOpenConnection();

            string queryCreate = "SELECT * FROM dbo.exemplo";
            var cmd = new SqlCommand(queryCreate, conexao);

            SqlDataReader dados = cmd.ExecuteReader();
            List<Tarefa> list = new List<Tarefa>();

            while(dados.Read())
            {
                Tarefa tarefa = new Tarefa();

                for (int i = 0; i<dados.FieldCount; i++)
                {
                    if(dados.GetName(i) == "Id")
                    {
                        tarefa.Id = dados.GetInt32(i);
                    }

                    else if(dados.GetName(i) == "Titulo")
                    {
                        tarefa.Titulo = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "Descricao")
                    {
                        tarefa.Descricao = dados.GetString(i);
                    }

                    else if (dados.GetName(i) == "DataCriacao")
                    {
                        tarefa.Data = dados.GetDateTime(i);
                    }

                    else if (dados.GetName(i) == "StatusTarefa")
                    {
                        tarefa.Status = (EnumStatusTarefa)dados.GetValue(i);
                    }
                }

                list.Add(tarefa);
            }
            conexao.Close();

            return Ok(list);

        }
    }
}
