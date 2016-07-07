namespace V1.Snapshots
{
    public class PosicaoAtualDaConta : Snapshot 
    {
        public string Nome { get; set; }

        public string Numero { get; set; }

        public bool Encerrada { get; set; }

        public decimal Saldo { get; set; }
    }
}