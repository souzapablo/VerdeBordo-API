namespace VerdeBordo.Core.Exceptions
{
    public class ClientDoesNotExistException : Exception
    {
        public ClientDoesNotExistException(int id) : base($"Cliente com o Id {id} não encontrado.")
        {
            
        }
    }
}