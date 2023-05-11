using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Repository;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCare.Service
{
    public class EquipmentService : NumericService<Equipment>
    {
        public EquipmentService(string filepath) : base(filepath) { }
    }
}
