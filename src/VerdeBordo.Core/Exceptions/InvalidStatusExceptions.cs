using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Extensions;

namespace VerdeBordo.Core.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException(OrderStatus status) : base($"Status {EnumExtensions<OrderStatus>.GetDescription(status)} inválido.")
        {
            
        }
    }
}