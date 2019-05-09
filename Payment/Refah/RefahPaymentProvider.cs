using System.Net;
using Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RefahVerify;

namespace Payment.Refah
{
    public class RefahPaymentProvider:IPaymentProvider
    {
        private string _mid;
        private string _paymentRedirectUrl;

        public RefahPaymentProvider(IConfiguration configuration)
        {
            _mid = configuration["refahMerchantId"];
            _paymentRedirectUrl =configuration["sepSite"];
        }

        public OrderPayRequestResponse CreatePayment(int orderId,int orderTotalPrice, string returnUrl)
        {
            return new RefahPaymentRequestResult
            {
                MID = _mid,
                Status = ResponseStatus.Success,
                Amount = orderTotalPrice,
                PaymentRedirectUrl = _paymentRedirectUrl,
                ResNumOrOrderId = orderId,
                ReturnRedirectUrl = GetCallBackUrl(returnUrl,orderId)
            };
        }
        private static string GetCallBackUrl(string returnUrl,long orderId)
        {
            var callBackUrl = returnUrl;
            callBackUrl = callBackUrl + ((callBackUrl.Contains("?") ? "&" : "?") + "orderId=" + orderId);
            return callBackUrl;
        }
        public Response Confirm(PaymentConfirmationContext context,int totalPrice)
        {
            var ctx = (RefahConfirmationContext) context;
            if (ctx.State != "OK")
                return Response.Failed();
            var amountIfSuccessErrorCodeIfError = CheckWithBank(ctx);
            return totalPrice == amountIfSuccessErrorCodeIfError
                ? Response.Success(JsonConvert.SerializeObject(ctx))
                : Response.Failed();
        }
        private long CheckWithBank(RefahConfirmationContext ctx)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            var srv = new PaymentIFBindingSoapClient(PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            var result = (long) srv.verifyTransactionAsync(ctx.RefNum, _mid).Result;
            return result;
        }
    }
}