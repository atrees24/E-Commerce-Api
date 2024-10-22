﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; set; }  
        public string UserEmail { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
        public string PaymentStatus { get; set; } 
        public string DelveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
