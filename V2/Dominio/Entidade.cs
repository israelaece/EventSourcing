using System;
using System.Collections.Generic;
using System.Diagnostics;
using V1.Eventos;

namespace V1.Dominio
{
    [DebuggerDisplay("Versao: {Versao} - VersaoCorrente: {VersaoCorrente}")]
    public abstract class Entidade
    {
        private readonly IList<Evento> _eventos = new List<Evento>();

        public Guid Id { get; set; }

        public int Versao { get; protected set; }

        public int VersaoCorrente { get; protected set; }

        internal void CarregarEventos(IEnumerable<Evento> eventos)
        {
            foreach (var e in eventos)
            {
                this.Versao++;
                DispararEvento(e, false);
            }
        }

        protected virtual void DispararEvento(Evento evento, bool novoEvento = true)
        {
            ((dynamic)this).Aplicar((dynamic)evento);
            this.VersaoCorrente++;

            if (novoEvento)
                this._eventos.Add(evento);
        }

        public IEnumerable<Evento> Eventos
        {
            get
            {
                return this._eventos;
            }
        }
    }
}