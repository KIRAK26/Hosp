using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public interface PersonalDetails
    {
        string Name { get; }
        string Email { get; }
        string Phone { get; }
        string Address { get; }

    }
}
