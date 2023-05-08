using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class OrderService : NumericService<OrderItem>
    {
        public OrderService(string filepath) : base(filepath) { }
    }
}
