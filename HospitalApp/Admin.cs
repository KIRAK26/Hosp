using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Admin : User
    {




        public Admin(string id, string password, string name, string role) : base(id, password, name, role)
        {

        }

        public void Details()
        {
            Console.WriteLine($"I am the admin {name}");

            Menu();
        }


        private void ListsAllDoctors()
        {

            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|              Administrator Menu        | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();



            Console.WriteLine("All doctors registered to the DOTNET Hospital Management System"); // [cite: 269]
            Console.WriteLine();

            // 创建一个列表来存放所有找到的医生对象
            var allDoctors = new List<Doctor>();

            try
            {
                // 2. 定位到存放所有医生文件的文件夹
                string doctorsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors");

                // 检查文件夹是否存在，以防万一
                if (Directory.Exists(doctorsDirectoryPath))
                {
                    // 获取该文件夹下所有的 .txt 文件
                    string[] doctorFiles = Directory.GetFiles(doctorsDirectoryPath, "*.txt");

                    // 3. 遍历每一个文件，读取信息并创建 Doctor 对象
                    foreach (string filePath in doctorFiles)
                    {
                        string[] doctorData = File.ReadAllLines(filePath)[0].Split('|');
                        if (doctorData.Length >= 6) // 确保数据格式正确
                        {
                            Doctor doctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                            allDoctors.Add(doctor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading doctor files: {ex.Message}");
            }

            // 4. 打印结果
            // 打印表头 [cite: 270, 273, 275, 276]
            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));

            if (allDoctors.Count == 0)
            {
                Console.WriteLine("No doctors found in the system.");
            }
            else
            {
                // 遍历医生列表，并打印每一个医生
                foreach (Doctor doc in allDoctors)
                {
                    // ⭐ 关键：直接打印 doctor 对象，C#会自动调用我们之前写的 ToString() 方法来格式化输出！
                    Console.WriteLine(doc);
                }
            }

            // 5. 结束交互
            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }



        private void CheckDoctorDetails()
        {
            string errorMessage = "";
            while (true) {
                Console.Clear();

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|              Administrator Menu        | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();




                Console.WriteLine("This is to check Doctor's details ");


                Console.WriteLine("--- Check Patient Details ---"); // 可以加一个副标题更清晰
                Console.WriteLine("Enter the ID of the patient to check (or type 'exit' to return).");

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
                    // 3. 核心逻辑：这部分和 Doctor.cs 里的版本完全一样
                    string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                    if (File.Exists(patientPath))
                    {
                        // 创建 Patient 对象
                        string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                        Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");

                        Doctor assignedDoctor = null; // 先准备一个空的医生对象

                        // 查找病人关联的医生
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                assignedDoctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                            }
                        }

                        // 4. 调用我们早已写好的、完全可重用的 Utils 方法来打印结果
                        Utils.PrintPatientDetails(patient, assignedDoctor);

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


       

            


        


        private void ListAllPatients()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|              Administrator Menu        | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();




            Console.WriteLine("This is to check Patients' details ");


            var allPatients = new List<Patient>();
            try
            {
                string patientsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients");
                if (Directory.Exists(patientsDirectoryPath))
                {
                    string[] patientFiles = Directory.GetFiles(patientsDirectoryPath, "*.txt");
                    foreach (string filePath in patientFiles)
                    {
                        string[] patientData = File.ReadAllLines(filePath)[0].Split('|');
                        //To prevent broken data 
                        if (patientData.Length >= 6)
                        {
                            Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");
                            allPatients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading patient files: {ex.Message}");
            }

            // 打印表头
            Console.WriteLine($"{"Patient",-15} | {"Doctor",-15} | {"Email Address",-25} | {"Phone",-12} | {"Address",-30}");
            Console.WriteLine(new string('-', 120));


            if (allPatients.Count == 0)
            {
                Console.WriteLine("No patients found in the system.");
            }
            else
            {
                // 【核心改动在这里】
                // 我们不再直接打印 patient 对象，因为它的 ToString() 方法里没有医生名字
                foreach (Patient pat in allPatients)
                {
                    // 1. 为当前循环的这个病人，实时查找他/她医生的名字
                    string doctorName = "Not Assigned"; // 默认值
                    string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{pat.id}.txt");
                    if (File.Exists(registeredDoctorPath))
                    {
                        string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                        string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                        if (File.Exists(doctorPath))
                        {
                            string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                            doctorName = doctorData[2];
                        }
                    }

                    // 2. 手动构建包含所有信息的输出字符串
                    Console.WriteLine($"{pat.name,-15} | {doctorName,-15} | {pat.email,-25} | {pat.phone,-12} | {pat.address,-30}");
                }
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }



        private void CheckPatientsDetails()
        {
            string errorMessage = "";

            while (true)
            {
                Console.Clear();

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|              Administrator Menu        | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();


                Console.WriteLine("Enter the ID of the patient to check (or type 'exit' to return).");

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
                    // 3. 核心逻辑：这部分和 Doctor.cs 里的版本完全一样
                    string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                    if (File.Exists(patientPath))
                    {
                        // 创建 Patient 对象
                        string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                        Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");

                        Doctor assignedDoctor = null; // 先准备一个空的医生对象

                        // 查找病人关联的医生
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                assignedDoctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                            }
                        }

                        // 4. 调用我们早已写好的、完全可重用的 Utils 方法来打印结果
                        // 假设你的Utils方法叫 PrintSinglePatientDetails
                        Utils.PrintPatientDetails(patient, assignedDoctor);

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

        


        private void AddDoctor()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|              Administrator Menu        | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();




            Console.WriteLine("This is to add a new Doctor ");


            Console.ReadLine();

        }


        private void AddPatient()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|              Administrator Menu        | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();




            Console.WriteLine("This is to add a new patient ");


            Console.ReadLine();

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
                Console.WriteLine("|              Administrator Menu        | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();


                Console.WriteLine($"Welcome to DOTNET Hospital Management System {name}");

                foreach (string option in options)
                {
                    Console.WriteLine(option);
                }


                string input = Console.ReadLine();

                //Very useful way to switch to different menu 
                switch (input)
                {
                    case "1":
                        ListsAllDoctors();

                        break;



                    case "2":
                        CheckDoctorDetails();

                        break;


                    case "3":
                        ListAllPatients();

                        break;


                    case "4":
                        CheckPatientsDetails();

                        break;



                    case "5":
                        AddDoctor();

                        break;


                    case "6":
                        AddPatient();

                        break;

                    case "7":
                        Console.Clear();
                        Program.Main([]);


                        break;



                    case "8":

                        Environment.Exit(0);
                        break;


                    default:
                        Console.WriteLine("There is no such case !  ");

                        continue;












                }
            }



        }






    }
}
