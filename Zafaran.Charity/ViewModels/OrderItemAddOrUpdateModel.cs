using System.ComponentModel.DataAnnotations;

namespace Zafaran.Charity.ViewModels
{
    public class OrderItemAddOrUpdateModel
    {
        [Required] public int? ProductId { get; set; }

        [Required] public int? Count { get; set; }
        
    }
}