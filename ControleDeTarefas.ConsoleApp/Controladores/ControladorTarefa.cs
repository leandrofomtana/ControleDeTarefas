using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controladores
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        protected override string Nometabela => "TBTAREFAS";

        public override void Atualizar(int id, Tarefa item)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @"UPDATE Tarefas 
	                SET	
		                [TITULO] = @TITULO, 
		                [DATACRIACAO]=@DATACRIACAO, 
		                [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO,
                        [PRIORIDADE] = @PRIORIDADE
	                WHERE 
		                [ID] = @ID";
            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", item.id);
            comandoAtualizacao.Parameters.AddWithValue("TITULO", item.Titulo);
            comandoAtualizacao.Parameters.AddWithValue("DATACRIACAO", item.DataCriacao);
            comandoAtualizacao.Parameters.AddWithValue("DATACONCLUSAO", item.DataConclusao);
            comandoAtualizacao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", item.PercentualConcluido);
            comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", item.PrioridadeString());
            comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void Inserir(Tarefa item)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @"INSERT INTO TBTAREFAS 
	                (
		                [TITULO], 
		                [DATACRIACAO], 
		                [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO],
                        [PRIORIDADE]
                    )
                    VALUES
                    (
                        @TITULO, 
		                @DATACRIACAO, 
		                @DATACONCLUSAO,
                        @PERCENTUALCONCLUIDO,
                        @PRIORIDADE
                    );
                    SELECT SCOPE_IDENTITY();";
            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("TITULO", item.Titulo);
            comandoInsercao.Parameters.AddWithValue("DATACRIACAO", item.DataCriacao);
            comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", item.DataConclusao);
            comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", item.PercentualConcluido);
            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", item.PrioridadeString());

            object id = comandoInsercao.ExecuteScalar();

            item.id = Convert.ToInt32(id);
            conexaoComBanco.Close();
        }

        public override Tarefa SelecionarPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        *
                    FROM 
                        TBTAREFAS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            if (leitor.Read() == false)
                return null;

            int id = Convert.ToInt32(leitor["ID"]);
            string titulo = Convert.ToString(leitor["TITULO"]);
            DateTime datacriacao = Convert.ToDateTime(leitor["DATACRIACAO"]);
            DateTime dataconclusao = Convert.ToDateTime(leitor["DATACONCLUSAO"]);
            decimal percentual = Convert.ToDecimal(leitor["PERCENTUALCONCLUIDO"]);
            int prioridade = Convert.ToInt32(leitor["PRIORIDADE"]);

            Tarefa t = new(titulo, datacriacao, percentual, prioridade);
            t.DataConclusao = dataconclusao;
            t.id = id;

            conexaoComBanco.Close();

            return t;
        }


        public override List<Tarefa> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        *
                    FROM 
                        TBTAREFAS";
            comandoSelecao.CommandText = sqlSelecao;
            SqlDataReader leitor = comandoSelecao.ExecuteReader();
            List<Tarefa> tarefas = new();
            
            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string titulo = Convert.ToString(leitor["TITULO"]);
                DateTime datacriacao = Convert.ToDateTime(leitor["DATACRIACAO"]);
                DateTime dataconclusao = Convert.ToDateTime(leitor["DATACONCLUSAO"]);
                decimal percentual = Convert.ToDecimal(leitor["PERCENTUALCONCLUIDO"]);
                int prioridade = Convert.ToInt32(leitor["PRIORIDADE"]);
                Tarefa t = new(titulo, datacriacao, percentual, prioridade);
                t.DataConclusao = dataconclusao;
                t.id = id;
                tarefas.Add(t);
            }
            conexaoComBanco.Close();
            return tarefas;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
