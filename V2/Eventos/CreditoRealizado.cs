namespace V1.Eventos
{
    public class CreditoRealizado : Evento
    {
        public CreditoRealizado(decimal valor)
        {
            this.Valor = valor;
        }

        public decimal Valor { get; private set; }
    }
}