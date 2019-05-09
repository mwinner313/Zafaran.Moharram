using Core;

namespace Payment.Refah
{
    public interface IPaymentProvider
    {
        OrderPayRequestResponse CreatePayment(int orderId,int orderTotalPrice, string returnUrl);
        Response Confirm(PaymentConfirmationContext context,int totalPrice);
    }
}