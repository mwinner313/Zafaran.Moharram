using Core;

namespace Payment.Refah
{
    public class RefahPaymentRequestResult:OrderPayRequestResponse
    {
        public string MID { get; set; }
        public long Amount { get; set; }
        public string ReturnRedirectUrl { get; set; }
        public long ResNumOrOrderId { get; set; }
    }
}