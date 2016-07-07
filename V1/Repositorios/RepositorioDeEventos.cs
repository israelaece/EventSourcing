using System;
using System.Collections.Generic;
using System.Linq;
using V1.Eventos;
using V1.Snapshots;

namespace V1.Repositorios
{
    public class RepositorioDeEventos : IRepositorioDeEventos
    {
        private readonly Dictionary<Guid, List<DadosDoEvento>> dados = new Dictionary<Guid, List<DadosDoEvento>>();
        private readonly Dictionary<Guid, List<Snapshot>> snapshots = new Dictionary<Guid, List<Snapshot>>();

        public void Salvar(Guid entidadeId, IEnumerable<Evento> eventos, int versaoEsperada)
        {
            List<DadosDoEvento> eventosArmazenados = null;

            if (!dados.TryGetValue(entidadeId, out eventosArmazenados))
            {
                eventosArmazenados = new List<DadosDoEvento>();
                this.dados.Add(entidadeId, eventosArmazenados);
            }
            else if (eventosArmazenados[eventosArmazenados.Count - 1].Versao != versaoEsperada)
            {
                throw new VersaoConflitante();
            }

            foreach (var e in eventos)
            {
                eventosArmazenados.Add(new DadosDoEvento(entidadeId, e, ++versaoEsperada));
            }
        }

        public IEnumerable<Evento> BuscarEventosPor(Guid entidadeId, int inicio)
        {
            return dados[entidadeId].Select(d => d.Evento).Skip(inicio);
        }

        private class DadosDoEvento
        {
            public DadosDoEvento(Guid entidadeId, Evento evento, int versao)
            {
                this.EntidadeId = entidadeId;
                this.Evento = evento;
                this.Versao = versao;
            }

            public Guid EntidadeId { get; private set; }

            public int Versao { get; private set; }

            public Evento Evento { get; private set; }
        }

        #region Snapshots

        public void Salvar(Guid entidadeId, Snapshot snapshot)
        {
            List<Snapshot> sps = null;

            if (!this.snapshots.TryGetValue(entidadeId, out sps))
            {
                sps = new List<Snapshot>();
                this.snapshots.Add(entidadeId, sps);
            }

            sps.Add(snapshot);
        }

        public T BuscarSnapshotPor<T>(Guid entidadeId) where T : Snapshot
        {
            List<Snapshot> sps = null;

            if (this.snapshots.TryGetValue(entidadeId, out sps))
                return sps.OrderByDescending(s => s.DataDeCriacao).FirstOrDefault() as T;

            return null;
        }

        #endregion
    }

    public class VersaoConflitante : Exception { }
}