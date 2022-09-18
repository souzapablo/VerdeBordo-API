using System.ComponentModel;

namespace VerdeBordo.Core.Enums
{
    public enum OrderStatus
    {
        [Description("Criado")]
        Created,
        [Description("Esboço")]
        Drafting,
        [Description("Aguardando aprovação")]
        AwaitingDraftApproval,
        [Description("Bordando")]
        Emobridering,
        [Description("Acabamentos")]
        Finishing,
        [Description("Pronto para entrega")]
        Delivering,
        [Description("Entregue")]
        Delivered
    }
}