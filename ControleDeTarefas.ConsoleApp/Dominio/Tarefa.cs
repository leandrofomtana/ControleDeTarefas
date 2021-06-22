using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public class Tarefa : Entidade
    {
        private string titulo;
        private DateTime dataCriacao;
        private DateTime dataConclusao;
        private decimal percentualConcluido;
        private int prioridade;

        public Tarefa(string titulo, DateTime dataCriacao, decimal percentualConcluido, int prioridade)
        {
            this.Titulo = titulo;
            this.DataCriacao = dataCriacao;
            this.dataConclusao = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            this.PercentualConcluido = percentualConcluido;
            this.Prioridade = prioridade;
            Concluir();

        }

        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime DataCriacao { get => dataCriacao; set => dataCriacao = value; }
        public DateTime DataConclusao { get => dataConclusao; set => dataConclusao = value; }
        public decimal PercentualConcluido { get => percentualConcluido; set => percentualConcluido = value; }
        public int Prioridade { get => prioridade; set => prioridade=value; }


        public string PrioridadeString()
        {
            if (Prioridade == 1)
                return "Alta";
            else if (Prioridade == 2)
                return "Normal";
            else
                return "Baixa";
        }

        public void Concluir()
        {
            if (percentualConcluido == 100 &&
                DataConclusao == (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                dataConclusao = System.DateTime.Now;

        }

        public bool Concluido()
        {
            if (percentualConcluido == 100 &&
                DataConclusao != (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return $"ID: {id} | Titulo: {Titulo} | DataCriacao: {DataCriacao} " +
                $"| DataConclusao: {DataConclusao} | Percentual Concluido: {PercentualConcluido}" +
                $"| Prioridade: {PrioridadeString()}";
        }
    }
}
