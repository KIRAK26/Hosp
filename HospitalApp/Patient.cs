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



        //redelcare since it has a base Name filed 
        public string Name => base.Name;
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }


        public Patient(string Id, string Password, string Name, string Address, string Email, string Phone, string Role)
    : base(Id, Password, Name, Role)
        {
            this.Address = Address;
            this.Email = Email;
            this.Phone = Phone;
        }

        public void Details()
        {
            

            String instructions = $"{Name}'s Details";
            Utils.DisplayHeader("My Details", instructions);
           


            Console.WriteLine();

            Console.ReadKey();

            Menu();

        }


     
        private void AssignedDoctor()
        {
            

            String instructions = "Your doctor: ";
            Utils.DisplayHeader("My Doctor", instructions);


            try
            {
                string RegisteredDoctor = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{this.Id}.txt"); ;




                if (File.Exists(RegisteredDoctor))
                {
                    string doctorId = File.ReadAllText(RegisteredDoctor).Trim();
                    Doctor doctor = Utils.FindUserById<Doctor>(doctorId);


                    if (doctor != null)
                    {
                        Utils.PrintPersonDetails(doctor);
                    }





                    else
                    {
                        Console.WriteLine($"Cound not found the ID {doctorId} doctor");
                    }

                }
                else
                {
                    Console.WriteLine("You don't have any registered Doctor (you triggered the second else)");
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

            Doctor assignedDoctor = null;
            string patientId = this.Id;


            


            String instructions = "";
            Utils.DisplayHeader("My Appointments", "" );
            Console.WriteLine();

            string relationshipFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patientId}.txt");

            try
            {
                // 步骤 1: 检查病人是否已经注册了医生
                if (!File.Exists(relationshipFilePath))
                {
                    // ---- 场景一：病人没有注册医生 ----
                    Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with:"); //

                    // 加载所有医生
                    var allDoctors = new List<Doctor>();
                    string doctorsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors");
                    string[] doctorFiles = Directory.GetFiles(doctorsDirectoryPath, "*.txt");
                    foreach (string filePath in doctorFiles)
                    {
                        string[] doctorData = File.ReadAllLines(filePath)[0].Split('|');
                        if (doctorData.Length >= 6)
                        {
                            allDoctors.Add(new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors"));
                        }
                    }

                    if (allDoctors.Count == 0)
                    {
                        Console.WriteLine("Sorry, there are no doctors available in the system.");
                        Console.ReadLine();
                        return;
                    }

                    // 显示医生列表
                    for (int i = 0; i < allDoctors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {allDoctors[i]}"); // 这里利用了我们为Doctor写的ToString()方法
                    }

                    // 获取用户选择
                    Console.Write("Please choose a doctor: "); //
                    int choice = -1;
                    if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= allDoctors.Count)
                    {
                        assignedDoctor = allDoctors[choice - 1];

                        // 【关键文件操作 1】创建病人的医生关联文件
                        File.WriteAllText(relationshipFilePath, assignedDoctor.Id);

                        // 【关键文件操作 2】更新医生的病人关联文件
                        // 注意：一个医生可以有多个病人，所以我们用 Append (追加)
                        string doctorRelationshipPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", "RegisteredPatient", $"{assignedDoctor.Id}.txt");
                        File.AppendAllText(doctorRelationshipPath, $"{patientId}{Environment.NewLine}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Returning to menu.");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                    // ---- 场景二：病人已经有注册医生 ----
                    string doctorId = File.ReadAllText(relationshipFilePath).Trim();
                    string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                    if (File.Exists(doctorPath))
                    {
                        string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                        assignedDoctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                    }
                }

                // ---- 共通流程：获取描述并创建预约记录 ----
                if (assignedDoctor != null)
                {
                    Console.WriteLine($"\nYou are booking a new appointment with {assignedDoctor.Name}"); //
                    Console.Write("Description of the appointment: "); //
                    string description = Console.ReadLine();

                    // 准备预约数据
                    string appointmentData = $"{patientId}|{assignedDoctor.Id}|{description}{Environment.NewLine}";

                    // 【关键文件操作 3】写入两条预约记录
                    // 注意：一个病人/医生可以有多个预约，所以我们用 Append (追加)
                    string patientAppointmentPath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{patientId}.txt");
                    string doctorAppointmentPath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Doctors", $"{assignedDoctor.Id}.txt");

                    File.AppendAllText(patientAppointmentPath, appointmentData);
                    File.AppendAllText(doctorAppointmentPath, appointmentData);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The appointment has been booked successfully"); //
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Could not find doctor details. Appointment booking failed.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred during booking: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu."); //
            Console.ReadLine();
        }





        

        private void ListAllAppointments()
        {
            


            String instructions = $"Appointments for {Name} ";
            Utils.DisplayHeader("My Appointments", instructions);


            var appointments = new List<Appointment>();

            try
            {
                // 步骤1：定位当前病人的预约文件
                string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{this.Id}.txt");

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
                            string doctorName = ""; // 默认值

                            // 步骤3：根据 Doctor ID 找到医生的名字
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                doctorName = doctorData[2]; // 假设名字在第3个位置
                            }

                            // 步骤4：创建 Appointment 对象并添加到列表
                            // 病人的名字就是当前登录用户自己的名字 (this.Name)
                            appointments.Add(new Appointment(doctorName, this.Name, description));
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

           


            String instructions = $"{Name} 's Details ";
            Utils.DisplayHeader("My Details", instructions);



            Console.WriteLine($"{Name}'s Details");
            Console.WriteLine($"Patient Id: {Id} ");
            Console.WriteLine($"Full Name: {Name} ");
            Console.WriteLine($"Address: {Address} ");
            Console.WriteLine($"Email: {Email} ");
            Console.WriteLine($"Phone: {Phone} ");
            Console.ReadLine();



        }

        
        



        public override void Menu()
        {
            while (true) {
              
                String instructions = $"Welcome to DOTNET Hospital Management System {Name}";
                Utils.DisplayHeader("Patient Menu ", instructions);


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

        ~Patient()
        {
            Console.WriteLine("Patient object destroyed and clearing memory");
            GC.Collect();
        }



    }
}
