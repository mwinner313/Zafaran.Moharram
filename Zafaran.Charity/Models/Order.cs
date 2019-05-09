using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Zafaran.Charity.Models
{
    public class Order : BaseEntity
    {
        public ICollection<OrderItem> OrderItems { get; set; }
        public string UserIdentifier { get; set; }
        public Charity Charity { get; set; }
        public int CharityId { get; set; }
        public DateTime CreatedDateTime { get; set;} = DateTime.Now;
        public PaymentStatus PaymentStatus { get; set; }
        public OrderState State { get; set; }
        public string PaymentId { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string StaticPhoneNumber { get; set; }
        public string UserName { get; set; }
        public int TotalPriceSofre { get; set; }
        public int TotalPrice { get; set; }
        public int OffPrecentage { get; set; }
    }

    public enum OrderState
    {
        UnKnown,
        Accepted,
        DeliveiriedToCharity
    }
}