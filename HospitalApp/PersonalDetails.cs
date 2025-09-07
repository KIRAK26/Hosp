using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public interface PersonalDetails
    {
        string name { get; }
        string email { get; }
        string phone { get; }
        string address { get; }

    }
}
