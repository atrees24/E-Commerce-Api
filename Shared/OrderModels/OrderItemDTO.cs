﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderItemDTO
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
