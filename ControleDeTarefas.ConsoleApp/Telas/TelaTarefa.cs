using ControleDeTarefas.ConsoleApp.Controladores;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaTarefa : Tela
    {
        private readonly ControladorTarefa controladorTarefa;

        public TelaTarefa(ControladorTarefa controladorTarefa) : base("Cadastro de Tarefas")
        {
            this.controladorTarefa = controladorTarefa;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova tarefa");
            Console.WriteLine("Digite 2 para visualizar tarefas finalizadas");
            Console.WriteLine("Digite 3 para visualizar tarefas pendentes");
            Console.WriteLine("Digite 4 para editar uma tarefa");
            Console.WriteLine("Digite 5 para excluir uma tarefa");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            controladorTarefa.Inserir(RegistroValido());
        }

        public void Excluir()
        {
            int id;
            List<Tarefa> tarefas = controladorTarefa.SelecionarTodos();
            foreach (var tarefa in tarefas)
                Console.WriteLine(tarefa);
            while (true)
            {
                Console.WriteLine("Digite o ID da tarefa que deseja excluir");
                if (int.TryParse(Console.ReadLine(), out id))
                    if (tarefas.Exists(t => t.id == id))
                        break;
                Console.WriteLine("Digite um ID válido");
            }
            controladorTarefa.Excluir(id);
        }
        public void Atualizar()
        {
            int id;
            string titulo;
            int prioridade;
            decimal percentual;
            List<Tarefa> tarefas = controladorTarefa.SelecionarTodos();
            foreach (var tarefa in tarefas)
                Console.WriteLine(tarefa);
            while (true)
            {
                Console.WriteLine("Digite o ID da tarefa que deseja atualizar");
                if (int.TryParse(Console.ReadLine(), out id))
                    if (tarefas.Exists(t => t.id == id))
                        break;
                Console.WriteLine("Digite um ID válido");
            }
           Tarefa t = controladorTarefa.SelecionarPorId(id);
            while (true)
            {
                Console.WriteLine($"Digite o titulo da tarefa (atual: {t.Titulo}");
                titulo = Console.ReadLine();
                if (titulo.Length > 0)
                    break;
                Console.WriteLine("O título não pode estar vazio");
            }
            t.Titulo = titulo;
            while (true)
            {
                Console.WriteLine($"Digite a prioridade da tarefa" +
                    $"(1: alta, 2: normal, 3: baixa) (atual: {t.PercentualConcluido}");
                if (int.TryParse(Console.ReadLine(), out prioridade))
                    if (prioridade == 1 || prioridade == 2 || prioridade == 3)
                        break;
                    else
                        Console.WriteLine("O número para a prioridade deve ser de 1 a 3");
                else
                    Console.WriteLine("Um número deve ser inserido.");
            }
            t.Prioridade = prioridade;
            while (true)
            {
                Console.WriteLine($"Digite o percentual de conclusão da tarefa(exemplo: 99,99)" +
                    $"(atual: {t.PercentualConcluido}");
                if (decimal.TryParse(Console.ReadLine(), out percentual))
                    break;
                Console.WriteLine("Percentual inválido.");
            }
            t.PercentualConcluido = percentual;
            t.Concluir();
            controladorTarefa.Atualizar(id, t);
        }

        public void VisualizarTarefasFinalizadas()
        {
            List<Tarefa> tarefas = controladorTarefa.SelecionarTodos();
            var tarefasFinalizadas = tarefas.Where(x => x.Concluido());
            tarefas = tarefasFinalizadas.OrderBy(x => x.Prioridade).ToList();
            foreach (var t in tarefas)
                Console.WriteLine(t);
            Console.ReadLine();
        }
        public void VisualizarTarefasPendentes()
        {
            List<Tarefa> tarefas = controladorTarefa.SelecionarTodos();
            var tarefasPendentes = tarefas.Where(x => !x.Concluido());
            tarefas = tarefasPendentes.OrderBy(x => x.Prioridade).ToList();
            foreach (var t in tarefas)
                    Console.WriteLine(t);
            Console.ReadLine();

        }

        private Tarefa RegistroValido()
        {
            string titulo;
            int prioridade;
            decimal percentual;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Digite o titulo da tarefa");
                titulo = Console.ReadLine();
                if (titulo.Length > 0)
                    break;
                Console.WriteLine("O título não pode estar vazio");
            }
            while (true)
            {
                Console.WriteLine("Digite a prioridade da tarefa(1: alta, 2: normal, 3: baixa)");
                if (int.TryParse(Console.ReadLine(), out prioridade))
                    if (prioridade == 1 || prioridade == 2 || prioridade == 3)
                        break;
                    else
                        Console.WriteLine("O número para a prioridade deve ser de 1 a 3");
                else
                    Console.WriteLine("Um número deve ser inserido.");
            }
            while (true)
            {
                Console.WriteLine("Digite o percentual de conclusão da tarefa(exemplo: 99,99)");
                if (decimal.TryParse(Console.ReadLine(), out percentual))
                    break;
                Console.WriteLine("Percentual inválido.");
            }
                return new Tarefa(titulo, DateTime.Now, percentual, prioridade);
        }
    }
}
