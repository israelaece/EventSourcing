using System;

namespace V1.Eventos
{
    public class ContaCorrenteCriada : Evento
    {
        public ContaCorrenteCriada(Guid id, string nome, string numero)
        {
            this.EntidadeId = id;
            this.Nome = nome;
            this.Numero = numero;
        }

        public Guid EntidadeId { get; private set; }

        public string Nome { get; private set; }

        public string Numero { get; private set; }
    }
}