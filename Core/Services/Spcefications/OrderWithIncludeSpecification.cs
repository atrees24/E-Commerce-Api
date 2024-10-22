using Domain.Interfaces;
using Domain.Models.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Spcefications
{
    internal class OrderWithIncludeSpecification :Specifications<Order>
    {
        public OrderWithIncludeSpecification(Guid id ) 
            : base(order => order.Id == id )
        {
            AddInclude(order => order.DelveryMethod);
            AddInclude(order => order.OrderItems);
        }

        public OrderWithIncludeSpecification(string email)
           : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.DelveryMethod);
            AddInclude(order => order.OrderItems);

            SetOrderBy(o => o.OrderDate);
        }
    }
}
