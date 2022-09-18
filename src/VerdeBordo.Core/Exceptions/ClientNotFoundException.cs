namespace VerdeBordo.Core.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(int id) : base($"Cliente com o Id {id} não encontrado.")
        {
            
        }
    }
}