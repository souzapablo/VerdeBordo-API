namespace VerdeBordo.Application.Features.Clients.ViewModels
{
    public class ClientListVm
    {
        public int ClientId { get; set; }   
        public string Name { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}