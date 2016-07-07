using System;

namespace V1.Eventos
{
    public class ContaCorrenteCriada : Evento
    {
        public ContaCorrenteCriada(Guid entidadeId, string nome, string numero)
        {
            this.EntidadeId = entidadeId;
            this.Nome = nome;
            this.Numero = numero;
        }

        public Guid EntidadeId { get; private set; }

        public string Nome { get; private set; }

        public string Numero { get; private set; }
    }
}