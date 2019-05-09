using System;
using System.Collections.Generic;
using Zafaran.Charity.Models;

namespace Zafaran.Charity.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public CharityViewModel Charity { get; set; }
        public int CharityId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderState State { get; set; }
        public string PhoneNumber { get; set; }
        public int OffPrecentage { get; set; }
        public string StaticPhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        /// <summary>
        /// used with off percentage
        /// </summary>
        public int TotalPriceSofre { get; set; }
        public int TotalPrice { get; set; }
    }
}