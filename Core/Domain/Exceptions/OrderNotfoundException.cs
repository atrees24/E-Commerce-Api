using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotfoundException(Guid id)
        : NotFoundException($"No Order with id {id} was found !")
    {
    }
}
