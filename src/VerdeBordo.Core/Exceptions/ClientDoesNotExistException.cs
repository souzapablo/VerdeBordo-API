namespace VerdeBordo.Core.Exceptions
{
    public class ClientDoesNotExistException : Exception
    {
        public ClientDoesNotExistException(int id) : base($"Não existe cliente com o Id {id}.")
        {
            
        }
    }
}