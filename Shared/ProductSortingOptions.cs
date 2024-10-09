using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpceficationParamters
    {
        private const int MAXPAGESIZE = 10;
        private const int DEFAULTPAGESIZE = 5;
        public int? Typeid { get; set; }
        public int? Brandid { get; set; }

        public ProductSortingOptions? sort { get; set; }

        public int PageIndex { get; set; } = 1;
        private int _Pagesize = DEFAULTPAGESIZE;

        public int PageSize
        {
            get => _Pagesize;
            set => _Pagesize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }
        public string? search { get; set; }

    }

    public enum ProductSortingOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,

    }
}
