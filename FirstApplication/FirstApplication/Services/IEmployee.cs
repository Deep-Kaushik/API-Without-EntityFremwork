using FirstApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Services
{
   public interface IEmployee
    {

        SaveEmployee SaveEmployees(Employee request);

    }
}
