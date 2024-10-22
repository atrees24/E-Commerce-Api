using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int quantity, decimal price, ProductInOrderItem product)
        {
            Quantity = quantity;
            Price = price;
            Product = product;
        }

        public  int  Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductInOrderItem Product {  get; set; }
    }
}
