using VerdeBordo.Core.Enums;

namespace VerdeBordo.Core.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException(OrderStatus status) : base($"Status {status} inválido.")
        {
            
        }
    }
}