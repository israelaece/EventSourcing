using System;
using V1.Eventos;
using V1.Snapshots;

namespace V1.Dominio
{
    public class ContaCorrente : Entidade
    {
        public ContaCorrente(string nome, string numero)
        {
            DispararEvento(new ContaCorrenteCriada(Guid.NewGuid(), nome, numero));
        }

        public ContaCorrente() { }

        public string Nome { get; private set; }

        public string Numero { get; private set; }

        public decimal Saldo { get; private set; }

        public bool? Encerrada { get; private set; }

        public void Encerrar()
        {
            if (this.Encerrada.Value) 
                throw new InvalidOperationException("A conta já está encerrada.");

            DispararEvento((Evento)new ContaEncerrada());
        }

        public void Lancar(decimal valor)
        {
            if (valor == 0M) 
                throw new ArgumentException("O valor não pode ser zero.", "valor");

            DispararEvento(valor < 0 ? (Evento)new DebitoRealizado(valor * -1) : new CreditoRealizado(valor));
        }

        public void Aplicar(ContaCorrenteCriada evento)
        {
            this.Id = evento.EntidadeId;
            this.Nome = evento.Nome;
            this.Numero = evento.Numero;
            this.Encerrada = false;
        }

        public void Aplicar(CreditoRealizado evento)
        {
            this.Saldo += evento.Valor;
        }

        public void Aplicar(DebitoRealizado evento)
        {
            this.Saldo -= evento.Valor;
        }

        public void Aplicar(ContaEncerrada evento)
        {
            this.Encerrada = true;
        }

        #region Snapshot

        public PosicaoAtualDaConta GerarPosicaoAtual()
        {
            return new PosicaoAtualDaConta()
            {
                Encerrada = this.Encerrada.Value,
                EntidadeId = this.Id,
                Nome = this.Nome,
                Numero = this.Numero,
                Saldo = this.Saldo,
                Versao = this.Versao,
                VersaoCorrente = this.VersaoCorrente
            };
        }

        internal ContaCorrente(PosicaoAtualDaConta snapshot)
        {
            this.Encerrada = snapshot.Encerrada;
            this.Id = snapshot.EntidadeId;
            this.Nome = snapshot.Nome;
            this.Numero = snapshot.Numero;
            this.Saldo = snapshot.Saldo;
            this.Versao = snapshot.Versao;
            this.VersaoCorrente = snapshot.VersaoCorrente;
        }

        #endregion
    }
}