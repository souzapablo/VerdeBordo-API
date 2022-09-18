using System.ComponentModel;

namespace VerdeBordo.Core.Enums
{
    public enum PaymentMethod
    {
        [Description("Pix")]
        Pix,
        [Description("PicPay")]
        PicPay,
        [Description("PagSeguro")]
        PagSeguro,
        [Description("Transferência bancária")]
        BankTransfer
    }
}