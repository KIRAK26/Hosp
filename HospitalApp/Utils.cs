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
        
        public class FormField
        {
            public string Label { get; set; }   // 字段的标签，如 "ID"
            public string Value { get; set; }   // 用户输入的值
            public bool IsPassword { get; set; } // 这个字段是不是密码
        }





        public static void DisplayHeader(string title, string subtitle)
        {
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");

            string content = title.Length > 36 ? title.Substring(0, 36) : title;
            int padding = (38 - content.Length) / 2;
            string formattedTitle = new string(' ', padding) + content;

            Console.WriteLine($"| {formattedTitle.PadRight(38)} |");
            Console.WriteLine("└────────────────────────────────────────┘");
            Console.WriteLine();

            // 打印副标题
            Console.WriteLine(subtitle);
            Console.WriteLine();
        }



        public static void DisplayHeader(string title, string instruction, string extraInfo)
        {
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");

            string content = title.Length > 36 ? title.Substring(0, 36) : title;
            int padding = (38 - content.Length) / 2;
            string formattedTitle = new string(' ', padding) + content;

            Console.WriteLine($"| {formattedTitle.PadRight(38)} |");
            Console.WriteLine("└────────────────────────────────────────┘");
            Console.WriteLine();

            // 打印指令
            Console.WriteLine(instruction);

            // 如果动态信息不为空，才打印
            if (!string.IsNullOrEmpty(extraInfo))
            {
                Console.WriteLine(extraInfo);
            }
            Console.WriteLine();
        }










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


        public static T FindUserById<T>(string userId) where T : User
        {
            // 根据类型 T 的名字，自动决定要去哪个文件夹里找
            // 比如 T 是 "Patient"，roleFolder 就是 "Patients"
            string roleFolder = typeof(T).Name + "s";
            string filePath = Path.Combine(AppContext.BaseDirectory, "Data", roleFolder, $"{userId}.txt");

            if (!File.Exists(filePath))
            {
                return null; // 如果文件不存在，直接返回 null
            }

            try
            {
                string[] data = File.ReadAllLines(filePath)[0].Split('|');
                if (data.Length >= 6)
                {
                    // 这是C#的一个高级功能，可以根据类型和构造函数参数动态创建对象
                    object user = Activator.CreateInstance(typeof(T), data[0], data[1], data[2], data[3], data[4], data[5], roleFolder);
                    return (T)user;
                }
            }
            catch (Exception ex)
            {
                // 打印一个调试信息，方便我们知道哪里出错了
                Console.WriteLine($"[DEBUG] 查找ID为 {userId} 的用户时出错: {ex.Message}");
            }

            return null; // 如果文件数据有问题或者发生异常，也返回 null
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
    

