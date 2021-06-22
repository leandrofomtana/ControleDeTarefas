using ControleDeTarefas.ConsoleApp.Controladores;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaContato : Tela
    {
        private readonly ControladorContato controladorContato;

        public TelaContato(ControladorContato controladorContato) : base("Cadastro de Contatos")
        {
            this.controladorContato = controladorContato;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo contato");
            Console.WriteLine("Digite 2 para visualizar a lista ordenada por cargos");
            Console.WriteLine("Digite 3 para editar um contato");
            Console.WriteLine("Digite 4 para excluir um contato");
            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            controladorContato.Inserir(RegistroValido());
        }

        public void Excluir()
        {
            int id;
            List<Contato> contatos = controladorContato.SelecionarTodos();
            foreach (var contato in contatos)
                Console.WriteLine(contato);
            while (true)
            {
                Console.WriteLine("Digite o ID do contato que deseja excluir ou 0 para sair");
                if (int.TryParse(Console.ReadLine(), out id))
                    if (contatos.Exists(t => t.id == id))
                        controladorContato.Excluir(id);
                if (id == 0)
                    break;
                Console.WriteLine("Digite um ID válido");
            }
        }
        public void Atualizar()
        {
            int id;
            List<Contato> lista = controladorContato.SelecionarTodos();
            foreach (var c in lista)
                Console.WriteLine(c);
            while (true)
            {
                Console.WriteLine("Digite o ID do contato a ser atualizado ou 0 para sair");
                if (int.TryParse(Console.ReadLine(), out id))
                    if (lista.Exists(t => t.id == id))
                        controladorContato.Atualizar(id, RegistroValido());
                if (id == 0)
                    break;
                Console.WriteLine("Digite um ID válido");
            }
        }

        public void VisualizarPorGrupos()
        {
            List<Contato> contatos = controladorContato.SelecionarTodos();
            var query = contatos.GroupBy(c => c.Cargo, c => new
            {
                c.Nome,
                c.Email,
                c.Telefone,
                c.Empresa
            });
            foreach (var cargo in query)
            {
                Console.WriteLine($"Cargo {cargo.Key}");
                foreach (var c in cargo)
                {
                    Console.WriteLine($"Nome: {c.Nome} | Email: {c.Email} | Telefone: {c.Telefone}" +
                        $" | Empresa: {c.Empresa}");
                }
            }
            Console.ReadLine();

        }

        private Contato RegistroValido()
        {
            string nome;
            string email;
            string telefone;
            string empresa;
            string cargo;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Digite o nome do contato");
                nome = Console.ReadLine();
                if (nome.Length > 0)
                    break;
                Console.WriteLine("O título não pode estar vazio");
            }
            while (true)
            {
                Console.WriteLine("Digite o email do contato");
                email = Console.ReadLine();
                if (email.Length > 0 && email.Contains(".com") && email.Contains("@"))
                    break;
                Console.WriteLine("O email deve conter @ e .com");
            }
            while (true)
            {
                Console.WriteLine("Digite o telefone do contato");
                telefone = Console.ReadLine();
                if (telefone.Length > 0)
                    break;
                Console.WriteLine("O telefone não pode estar vazio");
            }
            while (true)
            {
                Console.WriteLine("Digite a empresa do contato");
                empresa = Console.ReadLine();
                if (empresa.Length > 0)
                    break;
                Console.WriteLine("A empresa não pode estar vazia");
            }
            while (true)
            {
                Console.WriteLine("Digite o cargo do contato");
                cargo = Console.ReadLine();
                if (cargo.Length > 0)
                    break;
                Console.WriteLine("O cargo não pode estar vazio");
            }
            return new Contato(nome, email, telefone, empresa, cargo);
        }
    }

}
