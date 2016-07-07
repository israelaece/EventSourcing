using System;
using V1.Dominio;
using V1.Snapshots;

namespace V1.Repositorios
{
    public class RepositorioDeContasCorrentes : IRepositorio<ContaCorrente>
    {
        private readonly IRepositorioDeEventos eventos;

        public RepositorioDeContasCorrentes(IRepositorioDeEventos eventos)
        {
            this.eventos = eventos;
        }

        public void Salvar(Entidade entidade)
        {
            eventos.Salvar(entidade.Id, entidade.Eventos, entidade.Versao - 1);
        }

        public ContaCorrente BuscarPor(Guid id)
        {
            var snapshot = this.eventos.BuscarSnapshotPor<PosicaoAtualDaConta>(id);

            var cc = snapshot != null ? new ContaCorrente(snapshot) : new ContaCorrente();
            cc.CarregarEventos(this.eventos.BuscarEventosPor(id, snapshot == null ? 0 : snapshot.VersaoCorrente));

            return cc;
        }
    }
}