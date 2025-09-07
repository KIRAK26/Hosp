using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Patient: User, PersonalDetails

    {



        //redelcare since it has a base name filed 
        public string name => base.name;
        public string address { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }


        public Patient(string id, string password, string name, string address, string email, string phone, string role)
    : base(id, password, name, role)
        {
            this.address = address;
            this.email = email;
            this.phone = phone;
        }

        public void Details()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine($"{name}'s Details");


            Console.WriteLine();

            Console.ReadKey();

            Menu();

        }


     
        private void AssignedDoctor()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Doctor                | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine("Your doctor:");

            Console.WriteLine( );
            Console.WriteLine("This is just a test case code ");


            try
            {
                string RegisteredDoctor = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{this.id}.txt"); ;

                


                if (File.Exists(RegisteredDoctor))
                {
                    string doctorId = File.ReadAllText(RegisteredDoctor).Trim();
                    string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
               
                if(File.Exists(doctorPath))
                    {
                        string[] Data = File.ReadAllLines(doctorPath)[0].Split('|');
                        Doctor doctor = new Doctor(Data[0], Data[1], Data[2], Data[3], Data[4], Data[5], "Doctors");
                        Utils.PrintPersonDetails(doctor);
                    }
                    else
                    {
                        Console.WriteLine("You don't have any registered Doctor (First else)!!!! ");
                    }


              
                
                }
                else
                {
                    Console.WriteLine("You don't have any registered Doctor (you triggered the second else) ");
                }
                



            }

            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }

            Console.ReadKey();
        }
        private void BookAppointments()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine($"You can try to book appointment here , {name}");

            Console.ReadLine();





        }

        private void ListAllAppointments()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Appointments          | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine($"Appointments for {name}  ");
            Console.WriteLine("This will tell you that this a appointments");



            var appointments = new List<Appointment>();

            try
            {
                // 步骤1：定位当前病人的预约文件
                string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{this.id}.txt");

                if (File.Exists(appointmentFilePath))
                {
                    // 步骤2：读取文件中的每一行，每一行都是一次预约
                    string[] lines = File.ReadAllLines(appointmentFilePath);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue; // 跳过空行

                        string[] data = line.Split('|');
                        if (data.Length >= 3)
                        {
                            // data[0] 是 Patient ID, data[1] 是 Doctor ID, data[2] 是 Description
                            string doctorId = data[1].Trim();
                            string description = data[2].Trim();
                            string doctorName = "Unknown Doctor"; // 默认值

                            // 步骤3：根据 Doctor ID 找到医生的名字
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                doctorName = doctorData[2]; // 假设名字在第3个位置
                            }

                            // 步骤4：创建 Appointment 对象并添加到列表
                            // 病人的名字就是当前登录用户自己的名字 (this.Name)
                            appointments.Add(new Appointment(doctorName, this.name, description));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching appointments: {e.Message}");
            }

            // 步骤5：调用 Utils 方法来显示整个列表
            Utils.PrintAppointmentsTable(appointments);

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            




            Console.ReadLine();
        }

        private void ListPaitentsDetails()
        {

            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();


            Console.WriteLine($"{name}'s Details");
            Console.WriteLine($"Patient Id: {id} ");
            Console.WriteLine($"Full Name: {name} ");
            Console.WriteLine($"Address: {address} ");
            Console.WriteLine($"Email: {email} ");
            Console.WriteLine($"Phone: {phone} ");
            Console.ReadLine();



        }

        public override string ToString()
        {
            // 根据作业第10页的格式要求输出
            string doctorName = "";

            try
            {
                // 2. 使用 Path.Combine 来安全地构建文件路径
                string relationshipFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{this.id}.txt");

                // 3. 检查关联文件是否存在
                if (File.Exists(relationshipFilePath))
                {
                    string doctorId = File.ReadAllText(relationshipFilePath).Trim();

                    // 确保读取到的 doctorId 不是空的
                    if (!string.IsNullOrEmpty(doctorId))
                    {
                        string doctorFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");

                        // 4.【关键】再次检查医生的详细信息文件是否存在，防止程序崩溃
                        if (File.Exists(doctorFilePath))
                        {
                            string[] doctorData = File.ReadAllLines(doctorFilePath)[0].Split('|');
                            if (doctorData.Length >= 3)
                            {
                                doctorName = doctorData[2]; // 获取医生的名字
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果在查找过程中发生任何预料之外的错误，程序也不会崩溃
                // 而是会安全地使用默认值 "Not Assigned"
                // 我们可以在控制台打印错误信息，方便调试
                Console.WriteLine($"\n[DEBUG] Error in Patient.ToString() for Patient ID {this.id}: {ex.Message}");
            }

            // 5. 使用我们之前优化好的、统一的列宽度来格式化并返回最终的字符串
            return $"{this.name,-15} | {doctorName,-15} | {this.email,-25} | {this.phone,-12} | {this.address,-40}";
        }
     
        



        public override void Menu()
        {
            while (true) {
                Console.Clear();

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|              Patient Menu              | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();


                Console.WriteLine($"Welcome to DOTNET Hospital Management System {name}");

                foreach (string option in options)
                {
                    Console.WriteLine(option);
                }


                string input = Console.ReadLine();

                switch(input)
                    {
                    case "1":
                        ListPaitentsDetails();
                        break;


                    case "2":
                        AssignedDoctor();

                        break;

                    case "3":
                        ListAllAppointments();

                        break;


                    case "4":
                        BookAppointments();

                        break;

                    case "5":
                        Console.Clear();
                        Program.Main([]);
                        break;

                    case "6":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("this is invalid input, Please Enter the right input!")
                            ;

                        continue;







                }

            }

        }



    }
}
