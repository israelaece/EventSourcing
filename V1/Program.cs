using System;
using V1.Dominio;
using V1.Repositorios;

namespace V1
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventos = new RepositorioDeEventos();
            var contas = new RepositorioDeContasCorrentes(eventos);

            var cc1 = new ContaCorrente("Israel", "1234-5");
            cc1.Lancar(3500M);
            cc1.Lancar(-1236.27M);
            cc1.Lancar(-100M);
            cc1.Lancar(-125.36M);

            contas.Salvar(cc1);

            Console.WriteLine("Id: {0}", cc1.Id);
            Console.WriteLine("Nome: {0}", cc1.Nome);
            Console.WriteLine("Número: {0}", cc1.Numero);
            Console.WriteLine("Encerrada: {0}", cc1.Encerrada);
            Console.WriteLine("Saldo: {0:N2}", cc1.Saldo);

            Console.WriteLine();

            var cc2 = contas.BuscarPor(cc1.Id);

            Console.WriteLine("Id: {0}", cc2.Id);
            Console.WriteLine("Nome: {0}", cc2.Nome);
            Console.WriteLine("Número: {0}", cc2.Numero);
            Console.WriteLine("Encerrada: {0}", cc2.Encerrada);
            Console.WriteLine("Saldo: {0:N2}", cc2.Saldo);

            cc2.Encerrar();

            contas.Salvar(cc2);

            Console.WriteLine();

            var cc3 = contas.BuscarPor(cc1.Id);

            Console.WriteLine("Id: {0}", cc3.Id);
            Console.WriteLine("Nome: {0}", cc3.Nome);
            Console.WriteLine("Número: {0}", cc3.Numero);
            Console.WriteLine("Encerrada: {0}", cc3.Encerrada);
            Console.WriteLine("Saldo: {0:N2}", cc3.Saldo);

            Console.WriteLine();

            var s = cc3.GerarPosicaoAtual();

            eventos.Salvar(cc3.Id, s);

            cc3.Lancar(150);
            cc3.Lancar(-10);
            contas.Salvar(cc3);

            var cc4 = contas.BuscarPor(cc3.Id);

            cc4.Lancar(100);

            contas.Salvar(cc4);

            var cc5 = contas.BuscarPor(cc4.Id);
        }
    }
}