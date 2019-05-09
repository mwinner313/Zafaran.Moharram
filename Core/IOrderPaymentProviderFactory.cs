namespace Payment.Refah
{
    public interface IOrderPaymentProviderFactory
    {
        IPaymentProvider GetProvider(string paymentStrategy);
    }
}