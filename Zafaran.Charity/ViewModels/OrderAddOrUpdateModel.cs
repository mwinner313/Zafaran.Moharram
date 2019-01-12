using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zafaran.Charity.ViewModels
{
    public class OrderAddOrUpdateModel
    {
        public List<OrderItemAddOrUpdateModel> OrderItems { get; set; }

        [Required]  public int? CharityId { get; set; }
        [Required]  public string UserIdentifier { get; set; }
        [Required]  public string PhoneNumber { get; set; }
        public string Description { get; set; }
        [Required]  public string StaticPhoneNumber { get; set; }
        [Required]  public string UserName { get; set; }
    }
}