using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderRequst
    {
        public string BasketId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public int DeliveryMethod { get; set; }
    }
}
