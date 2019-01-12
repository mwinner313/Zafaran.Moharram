using System.Threading.Tasks;


namespace Zafaran.Charity.Services
{
    public class ZarinPalPaymentProvider
    {
        public async Task<(bool succes, long refId)> IsPaymnetValid(int amount, string authority)
        {
            //"1b7662fc-5b33-11e8-9c88-005056a205be"
             var zp = new Zarinpal.Payment("1b7662fc-5b33-11e8-9c88-005056a205be",amount);
    //        var zp = new ZarinpalSandbox.Payment(amount);
            var res = await zp.Verification(authority);
            return (res.Status == 100, res.RefId);
        }
    }
}