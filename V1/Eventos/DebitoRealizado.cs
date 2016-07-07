namespace V1.Eventos
{
    public class DebitoRealizado : Evento
    {
        public DebitoRealizado(decimal valor)
        {
            this.Valor = valor;
        }

        public decimal Valor { get; private set; }
    }
}