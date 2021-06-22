using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controladores
{
    public class ControladorContato : Controlador<Contato>
    {
        protected override string Nometabela => "TBCONTATOS";

        public override void Atualizar(int id, Contato item)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            string sqlAtualizacao =
                @$"UPDATE {Nometabela} 
	                SET	
		                [NOME] = @NOME, 
		                [EMAIL]=@EMAIL, 
		                [TELEFONE] = @TELEFONE,
                        [EMPRESA] = @EMPRESA,
                        [CARGO] = @CARGO
	                WHERE 
		                [ID] = @ID";
            comandoAtualizacao.CommandText = sqlAtualizacao;

            comandoAtualizacao.Parameters.AddWithValue("ID", item.id);
            comandoAtualizacao.Parameters.AddWithValue("NOME", item.Nome);
            comandoAtualizacao.Parameters.AddWithValue("EMAIL", item.Email);
            comandoAtualizacao.Parameters.AddWithValue("TELEFONE", item.Telefone);
            comandoAtualizacao.Parameters.AddWithValue("EMPRESA", item.Empresa);
            comandoAtualizacao.Parameters.AddWithValue("CARGO", item.Cargo);
            comandoAtualizacao.ExecuteNonQuery();
            conexaoComBanco.Close();

        }

        public override void Inserir(Contato item)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            string sqlInsercao =
                @$"INSERT INTO {Nometabela} 
	                (
		                [NOME], 
		                [EMAIL], 
		                [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    )
                    VALUES
                    (
                        @NOME, 
		                @EMAIL, 
		                @TELEFONE,
                        @EMPRESA,
                        @CARGO
                    );
                    SELECT SCOPE_IDENTITY();";
            comandoInsercao.CommandText = sqlInsercao;

            comandoInsercao.Parameters.AddWithValue("NOME", item.Nome);
            comandoInsercao.Parameters.AddWithValue("EMAIL", item.Email);
            comandoInsercao.Parameters.AddWithValue("TELEFONE", item.Telefone);
            comandoInsercao.Parameters.AddWithValue("EMPRESA", item.Empresa);
            comandoInsercao.Parameters.AddWithValue("CARGO", item.Cargo);

            object id = comandoInsercao.ExecuteScalar();

            item.id = Convert.ToInt32(id);
            conexaoComBanco.Close();
        }

        public override Contato SelecionarPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @$"SELECT 
                        *
                    FROM 
                        {Nometabela}
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            if (leitor.Read() == false)
                return null;

            int id = Convert.ToInt32(leitor["ID"]);
            string nome = Convert.ToString(leitor["NOME"]);
            string email = Convert.ToString(leitor["EMAIL"]);
            string telefone = Convert.ToString(leitor["TELEFONE"]);
            string empresa = Convert.ToString(leitor["EMPRESA"]);
            string cargo = Convert.ToString(leitor["CARGO"]);

            Contato c = new(nome, email, telefone, empresa, cargo);
            c.id = id;
            conexaoComBanco.Close();

            return c;
        }


        public override List<Contato> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @$"SELECT 
                        *
                    FROM 
                        {Nometabela}";
            comandoSelecao.CommandText = sqlSelecao;
            SqlDataReader leitor = comandoSelecao.ExecuteReader();
            List<Contato> contatos = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = Convert.ToString(leitor["NOME"]);
                string email = Convert.ToString(leitor["EMAIL"]);
                string telefone = Convert.ToString(leitor["TELEFONE"]);
                string empresa = Convert.ToString(leitor["EMPRESA"]);
                string cargo = Convert.ToString(leitor["CARGO"]);
                Contato c = new(nome, email, telefone, empresa, cargo);
                c.id = id;
                contatos.Add(c);
            }
            conexaoComBanco.Close();
            return contatos;
        }
    }
}
