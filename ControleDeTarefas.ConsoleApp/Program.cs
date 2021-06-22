using ControleDeTarefas.ConsoleApp.Telas;
using System;

namespace ControleDeTarefas.ConsoleApp
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();

        static void Main(string[] args)
        {
            while (true)
            {
                Tela telaSelecionada = telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is Tela)
                    Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (telaSelecionada is TelaTarefa)
                {
                    TelaTarefa telaTarefa = (TelaTarefa)telaSelecionada;
                    if (opcao == "1")
                        telaTarefa.Inserir();
                    else if (opcao == "2")
                    {
                        telaTarefa.VisualizarTarefasFinalizadas();
                    }
                    else if (opcao == "3")
                        telaTarefa.VisualizarTarefasPendentes();
                    else if (opcao == "4")
                        telaTarefa.Atualizar();
                    else if (opcao == "5")
                        telaTarefa.Excluir();
                }
                if (telaSelecionada is TelaContato)
                {
                    TelaContato telaContato = (TelaContato)telaSelecionada;
                    if (opcao == "1")
                        telaContato.Inserir();
                    else if (opcao == "2")
                    {
                        telaContato.VisualizarPorGrupos();
                    }
                    else if (opcao == "3")
                        telaContato.Atualizar();
                    else if (opcao == "4")
                        telaContato.Excluir();
                }
            }
            Console.Clear();
        }
    }
}

