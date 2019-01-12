
namespace Zafaran.Charity.Models
{
    public class SupportMessage:BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}