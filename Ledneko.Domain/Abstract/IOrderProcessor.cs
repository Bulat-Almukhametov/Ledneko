using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledneko.Domain.Entities;

namespace Ledneko.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Solution solution, ContactDetails contacts);
    }
}
