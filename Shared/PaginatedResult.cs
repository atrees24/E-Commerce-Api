using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PaginatedResult<TData>(int pageindex , int pagesize, int TotalCount ,IEnumerable<TData> Data);
    
}
