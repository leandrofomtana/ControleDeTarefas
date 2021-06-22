using ControleDeTarefas.ConsoleApp.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaPrincipal : Tela
    {
        private readonly ControladorTarefa controladorTarefa;
        private readonly ControladorContato controladorContato;
        private readonly TelaTarefa telaTarefa;
        private readonly TelaContato telaContato;


        public TelaPrincipal() : base("Tela Principal")
        {
            controladorTarefa = new();
            controladorContato = new();
            telaTarefa = new(controladorTarefa);
            telaContato = new(controladorContato);
        }

        public Tela ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            Tela telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");
                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaTarefa;

                if (opcao == "2")
                    telaSelecionada = telaContato;
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }

}
