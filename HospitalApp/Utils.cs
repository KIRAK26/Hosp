using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal static class Utils
    {







        public static void PrintPersonDetails(PersonalDetails person)
        {
            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));

            if (person != null)
            {
                // 用同样的方式格式化医生的数据，确保对齐
                Console.WriteLine($"{person.Name,-20} | {person.Email,-25} | {person.Phone,-15} | {person.Address,-30}");
            }
            else
            {
                // 如果病人没有注册医生，也给出提示
                Console.WriteLine("You are not currently registered with a doctor.");
            }


        }



        public static string GetMaskedPasswordInput()
        {
            string Password = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && Password.Length > 0)
                {
                    Password = Password.Substring(0, Password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return Password;
        }




        public static void PrintAppointmentsTable(List<Appointment> appointments)
        {

            Console.WriteLine($"{"Doctor",-25} | {"Patient",-25} | {"Description",-40}");
            Console.WriteLine(new string('-', 95));

            if (appointments == null || appointments.Count == 0)
            {
                Console.WriteLine("You have no appointments booked.");
                return;
            }

            // 遍历列表中的每一个 appointment 对象
            foreach (Appointment appt in appointments)
            {
                // ⭐ 关键在这里：直接使用你定义的公共属性 ⭐
                // 通过 appt.DoctorName, appt.PatientName, appt.Description 来获取值
                Console.WriteLine($"{appt.DoctorID,-25} | {appt.PatientID,-25} | {appt.Description,-40}");
            }
        }





        //public static void PrintPatientDetails(Patient patient)
        //{
        //    Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
        //    Console.WriteLine(new string('-', 95));

        //    if (patient != null)
        //    {
        //        // 用同样的方式格式化医生的数据，确保对齐
        //        Console.WriteLine($"{patient.Name,-20} | {patient.Email,-25} | {patient.Phone,-15} | {patient.Address,-30}");
        //    }
        //    else
        //    {
        //        // 如果病人没有注册医生，也给出提示
        //        Console.WriteLine("You are not currently registered with a patient.");
        //    }


        //}

        public static void PrintPatientDetails(Patient patient, Doctor doctor)
        {
            // 打印表头，根据截图调整格式
            Console.WriteLine($"{"Patient",-15} | {"Doctor",-15} | {"Email Address",-25} | {"Phone",-12} | {"Address",-40}");
            Console.WriteLine(new string('-', 115));

            // 检查传入的 patient 对象是否有效
            if (patient is null)
            {
                

                // 使用我们之前定义好的公共属性来获取并打印信息
                Console.WriteLine("Patient details could not be displayed.");
                return;

            }

            Console.WriteLine($"{patient.Name,-15} | {(doctor?.Name?? "Unassigned"),-15} | {patient.Email,-25} | {patient.Phone,-12} | {patient.Address,-40}");

        }
    }
    }
    

