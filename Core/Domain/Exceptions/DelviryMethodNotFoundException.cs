using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DelviryMethodNotFoundException(int id)
        :NotFoundException($"No Delviry with id {id} was found !")
    {
    }
}
