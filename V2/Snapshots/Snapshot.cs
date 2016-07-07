using System;

namespace V1.Snapshots
{
    public abstract class Snapshot
    {
        public Snapshot()
        {
            this.DataDeCriacao = DateTime.Now;
        }

        public DateTime DataDeCriacao { get; set; }

        public Guid EntidadeId { get; set; }

        public int Versao { get; set; }

        public int VersaoCorrente { get; set; }
    }
}