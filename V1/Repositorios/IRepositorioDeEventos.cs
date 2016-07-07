using System;
using System.Collections.Generic;
using V1.Eventos;
using V1.Snapshots;

namespace V1.Repositorios
{
    public interface IRepositorioDeEventos
    {
        void Salvar(Guid entidadeId, IEnumerable<Evento> eventos, int versaoEsperada);

        void Salvar(Guid entidadeId, Snapshot snapshot);

        IEnumerable<Evento> BuscarEventosPor(Guid entidadeId, int inicio = 0);

        T BuscarSnapshotPor<T>(Guid entidadeId) where T : Snapshot;
    }
}