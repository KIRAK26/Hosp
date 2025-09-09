using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Admin : User
    {




        public Admin(string Id, string Password, string Name, string Role) : base(Id, Password, Name, Role)
        {

        }

        public void Details()
        {
            Console.WriteLine($"I am the admin {Name}");

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


                
                Console.WriteLine("Enter the ID of the Doctor to check (or type 'exit' to return).");

                // 如果有错误信息，就显示它
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    Console.ResetColor();
                }

                Console.Write("> ");
                string doctorId = Console.ReadLine();

                if (doctorId.ToLower() == "exit")
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(doctorId))
                {
                    errorMessage = "Doctor ID cannot be empty. Please try again.";
                    continue;
                }

                try
                {
                    // 【核心改动】这里的逻辑现在是查找【医生】
                    string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");

                    if (File.Exists(doctorPath))
                    {
                        // 1. 读取文件并创建 Doctor 对象
                        string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                        Doctor doctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");

                        // 2. 调用我们为医生专门创建的打印方法
                        Utils.PrintPersonDetails(doctor);

                        // 任务完成，退出循环
                        break;
                    }
                    else
                    {
                        // 医生ID不存在，设置错误信息并重新循环
                        errorMessage = $"Error: No doctor found with ID '{doctorId}'. Please try again.";
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"An error occurred: {ex.Message}";
                    continue;
                }
            } // while 循环结束

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
                    string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{pat.Id}.txt");
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
                    Console.WriteLine($"{pat.Name,-15} | {doctorName,-15} | {pat.Email,-25} | {pat.Phone,-12} | {pat.Address,-30}");
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
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.Id}.txt");
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




        //private void AddDoctor()
        //{
        //    Console.Clear();

        //    Console.WriteLine("┌────────────────────────────────────────┐");
        //    Console.WriteLine("|                                        |");
        //    Console.WriteLine("|   DOTNET Hospital Management System    |");
        //    Console.WriteLine("|--------------------------------------- |");
        //    Console.WriteLine("|              Administrator Menu        | ");
        //    Console.WriteLine("└────────────────────────────────────────┘ ");
        //    Console.WriteLine();




        //    Console.WriteLine("This is to add a new Doctor ");



        //    try
        //    {
        //        // 1. Generate a unique, random Doctor ID
        //        string newDoctorId;
        //        string doctorFilePath; // Changed variable Name for clarity
        //        Random rand = new Random();
        //        do
        //        {
        //            // The ID generation logic is identical
        //            newDoctorId = rand.Next(13339, 20000).ToString();
        //            // 【Key Change】Save to the "Doctors" folder instead of "Patients"
        //            doctorFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{newDoctorId}.txt");
        //        } while (File.Exists(doctorFilePath));

        //        // 2. Gather the doctor's information
        //        Console.Write("First Name: ");
        //        string firstName = Console.ReadLine();
        //        Console.Write("Last Name: ");
        //        string lastName = Console.ReadLine();
        //        Console.Write("Password: ");
        //        string Password = Utils.GetMaskedPasswordInput(); // Reuse the Password utility
        //        Console.Write("Email: ");
        //        string Email = Console.ReadLine();
        //        Console.Write("Phone: ");
        //        string Phone = Console.ReadLine();
        //        Console.Write("Street Number: ");
        //        string streetNumber = Console.ReadLine();
        //        Console.Write("Street: ");
        //        string street = Console.ReadLine();
        //        Console.Write("City: ");
        //        string city = Console.ReadLine();
        //        Console.Write("State: ");
        //        string state = Console.ReadLine();

        //        // 3. Combine the data into the standard format
        //        string fullName = $"{firstName} {lastName}";
        //        string fullAddress = $"{streetNumber} {street}, {city}, {state}";

        //        // The data format is the same for doctors and patients
        //        string doctorData = $"{newDoctorId}|{Password}|{fullName}|{fullAddress}|{Email}|{Phone}";

        //        // 4. Write the data to the new file
        //        File.WriteAllText(doctorFilePath, doctorData);

        //        // 5. Display the success message
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        // 【Key Change】Update the success message
        //        Console.WriteLine($"\nDoctor {fullName} added to the system!");
        //        Console.ResetColor();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine($"\nAn error occurred while adding the doctor: {ex.Message}");
        //        Console.ResetColor();
        //    }

        //    Console.WriteLine("\nPress <Enter> to return to the menu.");
        //    Console.ReadLine();
        //}


        private void AddDoctor()
        {
            // 1. 准备工作：定义表单的标签和值的数组 (和AddPatient完全一样)
            var labels = new string[]
            {
        "First Name", "Last Name", "Password", "Email", "Phone",
        "Street Number", "Street", "City", "State"
            };
            var values = new string[labels.Length];
            for (int i = 0; i < values.Length; i++) values[i] = "";

            int currentField = 0;
            string errorMessage = "";
            ConsoleKeyInfo keyInfo;

            // 2. 主循环：负责界面绘制、光标导航和接收输入
            while (true)
            {
                Console.Clear();
                // 【改动】这里的标题现在是 Add Doctor
                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|    DOTNET Hospital Management System   |");
                Console.WriteLine("|----------------------------------------|");
                Console.WriteLine("|                Add Doctor                |");
                Console.WriteLine("└────────────────────────────────────────┘");
                Console.WriteLine("\nUse UP/DOWN arrows to switch. Press ENTER to submit when done.");

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    Console.ResetColor();
                }

                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);
                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }

                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    bool allFieldsFilled = !values.Any(string.IsNullOrWhiteSpace);
                    if (allFieldsFilled)
                    {
                        break;
                    }
                    else
                    {
                        errorMessage = "All fields are required. Please fill in the missing information.";
                        continue;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow) currentField = (currentField == 0) ? labels.Length - 1 : currentField - 1;
                else if (keyInfo.Key == ConsoleKey.DownArrow) currentField = (currentField == labels.Length - 1) ? 0 : currentField + 1;
                else if (keyInfo.Key == ConsoleKey.Backspace && values[currentField].Length > 0) values[currentField] = values[currentField].Substring(0, values[currentField].Length - 1);
                else if (!char.IsControl(keyInfo.KeyChar)) values[currentField] += keyInfo.KeyChar;
            } // UI循环结束

            // 3. 循环结束后，处理并保存数据
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            try
            {
                // a) 从values数组中提取数据
                string firstName = values[0];
                string lastName = values[1];
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7];
                string state = values[8];

                // b) 随机生成唯一的医生ID
                string newDoctorId;
                string doctorFilePath;
                Random rand = new Random();
                do
                {
                    newDoctorId = rand.Next(10000, 99999999).ToString();
                    // 【改动】确保文件保存在 "Doctors" 文件夹
                    doctorFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{newDoctorId}.txt");
                } while (File.Exists(doctorFilePath));

                // c) 组合数据
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string doctorData = $"{newDoctorId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

                // d) 写入文件
                File.WriteAllText(doctorFilePath, doctorData);

                // e) 显示成功信息
                Console.ForegroundColor = ConsoleColor.Green;
                // 【改动】成功信息的主语是 "Doctor"
                Console.WriteLine($"\nDoctor {fullName} added to the system!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }





        //private void AddPatient()
        //{
        //    Console.Clear();

        //    Console.WriteLine("┌────────────────────────────────────────┐");
        //    Console.WriteLine("|                                        |");
        //    Console.WriteLine("|   DOTNET Hospital Management System    |");
        //    Console.WriteLine("|--------------------------------------- |");
        //    Console.WriteLine("|              Administrator Menu        | ");
        //    Console.WriteLine("└────────────────────────────────────────┘ ");
        //    Console.WriteLine();




        //    try
        //    {
        //        // 1. 自动生成一个唯一的、随机的 Patient ID
        //        string newPatientId;
        //        string patientFilePath;
        //        Random rand = new Random();
        //        do
        //        {
        //            newPatientId = rand.Next(12451, 25000).ToString();
        //            patientFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{newPatientId}.txt");
        //        } while (File.Exists(patientFilePath));

        //        // 2. 依次获取病人信息 (使用基础的 Console.ReadLine)
        //        Console.Write("First Name: ");
        //        string firstName = Console.ReadLine();
        //        Console.Write("Last Name: ");
        //        string lastName = Console.ReadLine();
        //        Console.Write("Password: ");
        //        string Password = Utils.GetMaskedPasswordInput();
        //        Console.Write("Email: ");
        //        string Email = Console.ReadLine();
        //        Console.Write("Phone: ");
        //        string Phone = Console.ReadLine();
        //        Console.Write("Street Number: ");
        //        string streetNumber = Console.ReadLine();
        //        Console.Write("Street: ");
        //        string street = Console.ReadLine();
        //        Console.Write("City: ");
        //        string city = Console.ReadLine();
        //        Console.Write("State: ");
        //        string state = Console.ReadLine();

        //        // 3. 组合数据
        //        string fullName = $"{firstName} {lastName}";
        //        string fullAddress = $"{streetNumber} {street}, {city}, {state}";

        //        // 格式: Id|Password|Name|Address|Email|Phone
        //        string patientData = $"{newPatientId}|{Password}|{fullName}|{fullAddress}|{Email}|{Phone}";

        //        // 4. 写入文件
        //        File.WriteAllText(patientFilePath, patientData);

        //        // 5. 显示成功信息
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine($"\n{fullName} added to the system!"); //
        //        Console.ResetColor();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine($"\nAn error occurred while adding the patient: {ex.Message}");
        //        Console.ResetColor();
        //    }


        //    Console.ReadLine();
        //}



        // 文件: Admin.cs

        private void AddPatient()
        {
            // 1. 准备工作 (这部分不变)
            var labels = new string[]
            {
        "First Name", "Last Name", "Password", "Email", "Phone",
        "Street Number", "Street", "City", "State"
            };
            var values = new string[labels.Length];
            for (int i = 0; i < values.Length; i++) values[i] = "";

            int currentField = 0;
            string errorMessage = "";
            ConsoleKeyInfo keyInfo;

            // 2. 主循环
            while (true)
            {
                // a) 绘制界面
                Console.Clear();
               

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|              Administrator Menu        | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();

                Console.WriteLine("Use UP/DOWN arrows to switch. Press ENTER to submit when done.");

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    Console.ResetColor();
                }

                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);

                    // 【改动1】我们删除了在这里打印 ">" 的代码

                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }

                // b) 定位光标 (不变)
                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);

                // c) 读取按键 (不变)
                keyInfo = Console.ReadKey(true);

                // d) 处理按键
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // 【改动2】在提交前进行验证
                    bool allFieldsFilled = true;
                    foreach (string value in values)
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            allFieldsFilled = false; // 发现一个未填写的字段
                            break;
                        }
                    }

                    if (allFieldsFilled)
                    {
                        break; // 所有字段都已填写，跳出循环，提交表单！
                    }
                    else
                    {
                        // 如果有字段未填写，设置错误信息并继续循环
                        errorMessage = "All fields are required. Please fill in the missing information.";
                        continue;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField == 0) ? labels.Length - 1 : currentField - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField == labels.Length - 1) ? 0 : currentField + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (values[currentField].Length > 0)
                    {
                        values[currentField] = values[currentField].Substring(0, values[currentField].Length - 1);
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    values[currentField] += keyInfo.KeyChar;
                }

                
            } // 循环结束
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            try
            {
                // a) 从values数组中提取数据
                string firstName = values[0];
                string lastName = values[1];
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7];
                string state = values[8];

                // b) 【你的要求】随机生成唯一的病人ID
                string newPatientId;
                string patientFilePath;
                Random rand = new Random();
                do
                {
                    newPatientId = rand.Next(10000, 99999999).ToString();
                    patientFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{newPatientId}.txt");
                } while (File.Exists(patientFilePath));

                // c) 组合数据
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string patientData = $"{newPatientId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

                // d) 写入文件
                File.WriteAllText(patientFilePath, patientData);

                // e) 【你的要求】显示成功信息
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPatient {fullName} added to the system!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }
            // 3. 循环结束后，处理并保存数据 (这部分逻辑不变)
            // ... (代码省略) ...
        


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


                Console.WriteLine($"Welcome to DOTNET Hospital Management System {Name}");

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
