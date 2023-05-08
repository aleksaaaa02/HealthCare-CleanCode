using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Repository
{
    public interface IKey
    {
        object GetKey();
        void SetKey(object key);
    }
}
