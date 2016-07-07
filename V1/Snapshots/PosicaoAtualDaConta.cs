namespace V1.Snapshots
{
    public class PosicaoAtualDaConta : Snapshot 
    {
        public string Nome { get; internal set; }

        public string Numero { get; internal set; }

        public bool Encerrada { get; internal set; }

        public decimal Saldo { get; internal set; }
    }
}