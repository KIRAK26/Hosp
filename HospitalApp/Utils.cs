using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal static class Utils
    {




        public static void PrintDoctorDetails(Doctor doctor)
        {
            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));

            if (doctor != null )
            {
                // 用同样的方式格式化医生的数据，确保对齐
                Console.WriteLine($"{doctor.name,-20} | {doctor.email,-25} | {doctor.phone,-15} | {doctor.address,-30}");
            }
            else
            {
                // 如果病人没有注册医生，也给出提示
                Console.WriteLine("You are not currently registered with a doctor.");
            }


        }




        public static void PrintPatientDetails(Patient patient)
        {
            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));

            if (patient != null)
            {
                // 用同样的方式格式化医生的数据，确保对齐
                Console.WriteLine($"{patient.name,-20} | {patient.email,-25} | {patient.phone,-15} | {patient.address,-30}");
            }
            else
            {
                // 如果病人没有注册医生，也给出提示
                Console.WriteLine("You are not currently registered with a patient.");
            }


        }
    }
}
