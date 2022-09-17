namespace VerdeBordo.Core.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int orderId) : base($"Pedido com o Id {orderId} não encontrado.")
        {
            
        }
    }
}