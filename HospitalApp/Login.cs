using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace HospitalApp
{
    internal class Login
    {

        private string? id, password;


        public void LoginMenu()
        { //String myID = "12356";
          //String myPassword = "Luo2241";
          //string masked = new string('*', myPassword.Length);

            //This is login page 
            // alt + 196 to draw the heavier solid line 

            
            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|                 Login                  | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("BaseDir = " + AppContext.BaseDirectory);

            try
            {
                Console.Write("ID: ");
                id = Console.ReadLine(); 
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new Exception("The ID cannot be empty!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press <Enter> to try again!");
                Console.ReadKey();
                Console.Clear();
                LoginMenu();
                return; 
            }

            Console.Write("Password: ");
            password = PasswordHandle();

            HandleUserLogin();

        }



        //它的作用就是拼接出某个角色的用户文件的绝对路径。

        //这样你不用每次都手写 Path.Combine(AppContext.BaseDirectory, "Data", "Patients", fileName)，只要调用 DataPathOf(...) 就行。
       private static string DataPathOf(string roleFolder, string fileName)
    => Path.Combine(AppContext.BaseDirectory, "Data", roleFolder, fileName);



        private static string PasswordHandle()
        //why is private static ? private means it only can be used in the Login.cs, static means it doesn't need to use the field in the Login.cs
        {
            string password = "";
            //Enum ConsoleKey
            ConsoleKeyInfo key;

            while (true)
            {   //read user input,also block it 
                key = Console.ReadKey(intercept: true);

                //Identify special input from user,it is to identifie the type of the input, such as ConsoleKey.backspace,ConsoleKey.Keychar

                //When user have input, both the Key,and the KeyChar generate something
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }


                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                //Check if the KeyChar contain the control string 
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }



            return password;
        }


        private void HandleUserLogin()
        {
            try

            {   // user entered their id to find the file 
                string fileName = $"{id}.txt";

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




                    case "Invalid password, press <Enter> to try again":
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


            
            if (id == details[0] && password == details[1])
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
                throw new Exception("Invalid password, press <Enter> to try again");
            }

        }


    }










        

    }

