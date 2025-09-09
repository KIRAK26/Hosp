using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using static HospitalApp.Utils;

namespace HospitalApp
{
    internal class Login
    {

        private string? Id, Password;


        // 文件: Login.cs

        public void LoginMenu()
        {
            var values = new string[] { "", "" }; // values[0] 存ID, values[1] 存Password
            int currentField = 0; // 0代表ID, 1代表Password
            ConsoleKeyInfo keyInfo;
            int formTop;



            // 2. 主循环
            while (true)
            {
                // a) 绘制界面
                Console.Clear();
                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|    DOTNET Hospital Management System   |");
                Console.WriteLine("|----------------------------------------|");
                Console.WriteLine("|                  Login                 |");
                Console.WriteLine("└────────────────────────────────────────┘");
                Console.WriteLine("\nUse UP/DOWN arrows to switch fields. Press ENTER to login.");

                 formTop = Console.CursorTop;
                int inputLeftPosition = 11; // "Password : ".Length

                // 画ID行
                Console.SetCursorPosition(0, formTop);
                Console.Write($"ID       : {values[0]}");

                // 画Password行
                Console.SetCursorPosition(0, formTop + 1);
                Console.Write($"Password : {new string('*', values[1].Length)}");

                // 【关键改动 2】把光标精确定位到当前输入框的末尾
                Console.SetCursorPosition(inputLeftPosition + values[currentField].Length, formTop + currentField);

                // b) 读取用户按键
                keyInfo = Console.ReadKey(true);

                // c) 处理按键
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break; // 按回车，跳出循环去登录
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = 0;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = 1;
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

            // 3. 循环结束后，处理登录
            this.Id = values[0];
            this.Password = values[1];

            Console.SetCursorPosition(0, formTop + 4);

            // 简单的非空验证
            if (string.IsNullOrWhiteSpace(this.Id) || string.IsNullOrWhiteSpace(this.Password))
            {
                Console.WriteLine("ID or Password cannot be empty. Press <Enter> to try again.");
                Console.ReadLine();
                LoginMenu();
                return;
            }

            HandleUserLogin();
        }


        //它的作用就是拼接出某个角色的用户文件的绝对路径。

        //这样你不用每次都手写 Path.Combine(AppContext.BaseDirectory, "Data", "Patients", fileName)，只要调用 DataPathOf(...) 就行。
        private static string DataPathOf(string roleFolder, string fileName)
    => Path.Combine(AppContext.BaseDirectory, "Data", roleFolder, fileName);



        //private static string PasswordHandle()
        ////why is private static ? private means it only can be used in the Login.cs, static means it doesn't need to use the field in the Login.cs
        //{
        //    string Password = "";
        //    //Enum ConsoleKey
        //    ConsoleKeyInfo key;

        //    while (true)
        //    {   //read user input,also block it 
        //        key = Console.ReadKey(intercept: true);

        //        //Identify special input from user,it is to identifie the type of the input, such as ConsoleKey.backspace,ConsoleKey.Keychar

        //        //When user have input, both the Key,and the KeyChar generate something
        //        if (key.Key == ConsoleKey.Enter)
        //        {
        //            Console.WriteLine();
        //            break;
        //        }


        //        else if (key.Key == ConsoleKey.Backspace && Password.Length > 0)
        //        {
        //            Password = Password.Substring(0, Password.Length - 1);
        //            Console.Write("\b \b");
        //        }
        //        //Check if the KeyChar contain the control string 
        //        else if (!char.IsControl(key.KeyChar))
        //        {
        //            Password += key.KeyChar;
        //            Console.Write("*");
        //        }
        //    }



        //    return Password;
        //}


        private void HandleUserLogin()
        {
            try

            {   // user entered their Id to find the file 
                string fileName = $"{Id}.txt";

                string adminPath = DataPathOf("Administrators", fileName);
                string doctorPath = DataPathOf("Doctors", fileName);
                string patientPath = DataPathOf("Patients", fileName);


                Console.WriteLine($"[DEBUG] adminPath   = {adminPath}   Exists={File.Exists(adminPath)}");
                Console.WriteLine($"[DEBUG] doctorPath  = {doctorPath}  Exists={File.Exists(doctorPath)}");
                Console.WriteLine($"[DEBUG] patientPath = {patientPath} Exists={File.Exists(patientPath)}");



                if (File.Exists(adminPath))

                {
                    VerfiyCred("Administrators", adminPath);

                }
                else if (File.Exists(doctorPath))
                {
                    VerfiyCred("Doctors", doctorPath);

                }
                else if (File.Exists(patientPath))
                {
                    

                    
                    VerfiyCred("Patients", patientPath);


                }
                else
                {
                    throw new Exception("Invalid ID, press <Enter> to try again!");
                }
            }

            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Invalid ID, press <Enter> to try again!":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();
                        break;




                    case "Invalid Password, press <Enter> to try again":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();


                        break;


                    default:
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();
                        break;








                }



            }





        }








        //Console.WriteLine("ID: {0}",myID);
        //Console.WriteLine("Passowrd: {0}",masked);









        private void VerfiyCred(string position, string file)
        {//the file will beconme this values :C:\Users\xluo4\Desktop\Semster 5\.NET Application Development\Assignment1\HospitalApp\HospitalApp\bin\Debug\net8.0\Data\Patients\12345.txt

            string[] lines = File.ReadAllLines(file, Encoding.UTF8);
            string[] details = lines[0].Split("|");


            
            if (Id == details[0] && Password == details[1])
            {
                Console.WriteLine("User account exists");

                Console.ReadKey();




                switch (position)
                {
                    case "Patients":
                        Patient patient = new Patient(details[0], details[1], details[2], details[3], details[4], details[5], "Patients");
                        patient.Details();
                        break;

                    case "Doctors":
                        Doctor doctor = new Doctor(details[0], details[1], details[2], details[3], details[4], details[5],"Doctors");
                        doctor.Details();
                        break;

                    case "Administrators":

                        Admin admin = new Admin(details[0], details[1], details[2], "Administrator");
                        
                        admin.Details();

                        break;



                }

            }

            else
            {
                throw new Exception("Invalid Password, press <Enter> to try again");
            }

        }


    }










        

    }

