using System;

namespace V1.Snapshots
{
    public abstract class Snapshot
    {
        public Snapshot()
        {
            this.DataDeCriacao = DateTime.Now;
        }

        public DateTime DataDeCriacao { get; private set; }

        public Guid EntidadeId { get; internal set; }

        public int Versao { get; internal set; }

        public int VersaoCorrente { get; internal set; }
    }
}