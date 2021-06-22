using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controladores
{
    abstract public class Controlador<T> where T : Entidade
    {
        protected abstract string Nometabela { get; }

        public abstract void Inserir(T item);

        public abstract T SelecionarPorId(int id);
        public abstract List<T> SelecionarTodos();
        public abstract void Atualizar(int id, T item);
        public void Excluir(int id)
        {
            SqlConnection conexaoComBanco = AbrirConexao();
            SqlCommand comandoExclusao = new();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                $@"DELETE FROM {Nometabela} 	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", id);

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
 
        protected static SqlConnection AbrirConexao()
        {
            string enderecoDBTarefa = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
            SqlConnection conexaoComBanco = new SqlConnection(enderecoDBTarefa);
            conexaoComBanco.Open();
            return conexaoComBanco;
        }
    }
}
