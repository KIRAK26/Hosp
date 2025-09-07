using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Doctor : User, PersonalDetails
    {
        public string name => base.name;
        public string address { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }





        public Doctor(string id, string password, string name, string address, string email, string phone, string role) : base(id, password, name, role)
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

            Console.WriteLine($"Doctor name: {name}");

            Console.ReadKey();
            Menu();



        }


        private void ListMyDetails()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();

            Console.WriteLine($"Doctor name: {name}");




            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));
            Console.WriteLine($"{name,-20} | {email,-25} | {phone,-15} | {address,-30}");

            Console.ReadLine();


        }

        private void AssignedPatients()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();




            Console.WriteLine($"Patients Assigned to {name}");






            try
            {
                string RegisteredPatient = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", "RegisteredPatient", $"{this.id}.txt"); ;




                if (File.Exists(RegisteredPatient))
                {
                    string patientId = File.ReadAllText(RegisteredPatient).Trim();
                    string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                    if (File.Exists(patientPath))
                    {
                        string[] Data = File.ReadAllLines(patientPath)[0].Split('|');

                        Patient patient = new Patient(Data[0], Data[1], Data[2], Data[3], Data[4], Data[5], "Patients");
                        Utils.PrintPersonDetails(patient);
                    }
                    else
                    {
                        Console.WriteLine("You don't have any registered Patient (First else)!!!! ");
                    }




                }
                else
                {
                    Console.WriteLine("You don't have any registered Patient (you triggered the second else) ");
                }




            }

            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }

            Console.ReadKey();
        }









        private void ListAllAppointments()
        {

            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();

            Console.WriteLine($"Doctor name: {name}");
            Console.WriteLine("This is a Appointments listing details ");

            var appointments = new List<Appointment>();

            try
            {
                // 【不同点 1】现在我们在 Doctors 文件夹里，用【医生自己的ID】来查找预约文件
                string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Doctors", $"{this.id}.txt");

                if (File.Exists(appointmentFilePath))
                {
                    string[] lines = File.ReadAllLines(appointmentFilePath);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] data = line.Split('|');
                        if (data.Length >= 3)
                        {
                            // 数据格式依然是: PatientID|DoctorID|Description
                            string patientId = data[0].Trim(); // 这次我们需要的是病人ID
                            string description = data[2].Trim();
                            string patientName = "Unknown Patient"; // 默认值

                            // 【不同点 2】现在需要根据 Patient ID 去查找【病人】的名字
                            string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");
                            if (File.Exists(patientPath))
                            {
                                string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                                patientName = patientData[2]; // 假设名字在第3个位置
                            }

                            // 【不同点 3】创建对象时，医生的名字就是当前登录的医生自己 (this.Name)
                            appointments.Add(new Appointment(this.name, patientName, description));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching appointments: {e.Message}");
            }

            // 【完全相同】最后，调用完全一样的 Utils 方法来显示结果
            Utils.PrintAppointmentsTable(appointments);

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }








        private void ListsAppointmentsWithPatients()
        {

            while (true) { 
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();

            Console.WriteLine($"Doctor name: {name}");
            

           
            string errorMessage = "";
            
            Console.WriteLine("Enter the ID of the patient you would like to view appointments for.");

            // 如果有错误信息，就显示它
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }

            // 2. 获取用户输入
            Console.Write("> ");
            string patientId = Console.ReadLine();

            if (patientId.ToLower() == "exit")
            {
                break; // 退出循环
            }

            if (string.IsNullOrWhiteSpace(patientId))
            {
                errorMessage = "Patient ID cannot be empty. Please try again.";
                continue; // 重新循环
            }

            try
            {
                // 3. 核心逻辑：验证病人是否存在
                string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                if (File.Exists(patientPath))
                {
                    // 病人存在，现在查找并显示他们的预约
                    var appointments = new List<Appointment>();
                    string patientName = File.ReadAllLines(patientPath)[0].Split('|')[2]; // 从病人文件获取名字

                    string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{patientId}.txt");

                    // 检查病人是否有预约文件
                    if (File.Exists(appointmentFilePath))
                    {
                        string[] lines = File.ReadAllLines(appointmentFilePath);
                        foreach (string line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            string[] data = line.Split('|');
                            if (data.Length >= 3)
                            {
                                // 文件内容: PatientID|DoctorID|Description
                                // 我们只关心和当前医生相关的预约
                                if (data[1].Trim() == this.id)
                                {
                                    string description = data[2].Trim();
                                    // 创建 Appointment 对象, 医生的名字就是当前医生自己 (this.Name)
                                    appointments.Add(new Appointment(this.name, patientName, description));
                                }
                            }
                        }
                    }

                    // 4. 调用 Utils 方法显示结果（即使没有预约，也会显示一个空表格和提示）
                    Utils.PrintAppointmentsTable(appointments);

                    // 任务完成，退出循环
                    break;
                }
                else
                {
                    // 病人ID不存在，设置错误信息并重新循环
                    errorMessage = $"Error: No patient found with ID '{patientId}'. Please try again.";
                    continue;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
                continue;
            }
        } // while 循环结束

        // 5. 结束交互
        Console.WriteLine("\nPress <Enter> to return to the menu.");
        Console.ReadLine();
    }











        private void CheckPatients()
        {

            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();

            Console.WriteLine($"Doctor name: {name}");
            Console.WriteLine("This is for checking patients  ");


            
                Console.Write("Enter the ID of the patient to check: ");
                string patientId = Console.ReadLine();

                // 步骤2：验证用户输入
                if (string.IsNullOrWhiteSpace(patientId))
                {
                    Console.WriteLine("\nPatient ID cannot be empty. Please try again.");
                    Console.ReadLine();
                    return;
                }

                try
                {
                    // 步骤3：根据用户输入的 ID 查找病人文件
                    string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                    if (File.Exists(patientPath))
                    {
                        // 步骤4：如果病人存在，创建 Patient 对象
                        string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                        // 假设病人文件格式: id|password|name|address|email|phone
                        Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");

                        // 步骤5：根据病人的 ID，查找他/她关联的医生
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");

                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                Doctor doctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");

                                // 步骤6：调用 Utils 方法，传入两个对象进行打印
                                Utils.PrintPatientDetails(patient, doctor);
                            }
                        }
                        else
                        {
                            // 如果病人存在但没有关联医生，也应该能显示（医生部分为空）
                            // 为了简化，我们这里暂时认为每个病人都有医生
                            Console.WriteLine($"\nCould not find the assigned doctor for patient {patient.name}.");
                        }
                    }
                    else
                    {
                        // 步骤7：如果病人文件不存在，给出错误提示 
                        Console.WriteLine($"\nError: No patient found with ID '{patientId}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nAn error occurred: {ex.Message}");
                }

                Console.WriteLine("\nPress <Enter> to return to the menu.");
                Console.ReadLine();
            }







        public override string ToString()
        {
            // 这个方法会返回一个我们精心格式化好的字符串，而不是默认的类名。
            // 请确保你的 Doctor 类拥有 Name, Email, Phone, Address 这些公共属性。
            return $"{this.name,-20} | {this.email,-25} | {this.phone,-15} | {this.address,-30}";
        }



        public override void Menu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|             Doctor  Menu               | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();


                Console.WriteLine($"Welcome to DOTNET Hospital Management System {name}");


                foreach (string option in options)
                {
                    Console.WriteLine(option);
                }

                string input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        ListMyDetails();
                        break;


                    case "2":
                        AssignedPatients();
                        break;

                    case "3":
                        ListAllAppointments();
                        break;

                    case "4":
                        CheckPatients();
                        break;

                    case "5":
                        ListsAppointmentsWithPatients();
                        break;

                    case "6":
                        Console.Clear();
                        Program.Main([]);
                        break;

                    case "7":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input, try again !");
                        continue;









                }

            }

        }

    }
}
