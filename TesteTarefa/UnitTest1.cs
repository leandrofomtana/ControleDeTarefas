using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;

namespace TesteTarefa
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NaoDeveConcluirTarefaSemEstar100Porcento()
        {
            Tarefa teste1 =new("tarefateste", System.DateTime.Today, 99, 3);
            DateTime agora = System.DateTime.Now;
            teste1.Concluir();
            Assert.AreNotEqual(agora, teste1.DataConclusao);  
        }
        [TestMethod]
        public void DeveConcluirTarefasCom100Porcento()
        {
            Tarefa teste1 = new("tarefateste", System.DateTime.Today, 100.00m, 3);
            teste1.Concluir();
            Assert.IsTrue(teste1.Concluido()); 
        }
        [TestMethod]
        public void DeveRetornarPrioridades()
        {
            Tarefa teste1 = new("tarefateste", System.DateTime.Today, 45, 1);
            Assert.AreEqual("Alta", teste1.PrioridadeString());
        }
        

    }
}
