using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public static class Extension
    {

        
            public static string Capitalize(this string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                
                return char.ToUpper(input[0]) + input.Substring(1).ToLower();
            }
        }
    

}
