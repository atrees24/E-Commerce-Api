using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderEntities
{
    public class DelveryMethod : BaseEntity<int>
    {
        public DelveryMethod()
        {
            
        }
        public DelveryMethod(string shortName, string description, string delviryTime, decimal price)
        {
            ShortName = shortName;
            Description = description;
            DelviryTime = delviryTime;
            Price = price;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DelviryTime { get; set; }
        public decimal Price { get; set; }

    }
}
