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


        

        public void LoginMenu()
        {   //values[0] store ID, value [1] store password 

            var values = new string[] { "", "" }; 

            //0 is the field of Id, 1 is the filed of password 
            int currentField = 0; 
            ConsoleKeyInfo keyInfo;
            int formTop;


            String instructions = "Welcome to the Login ,Please Enter your password and ID ";
            Utils.DisplayHeader("Login", instructions);
            
            Console.WriteLine("Use UP/DOWN arrows to switch fields. Press ENTER to login.");
            Console.WriteLine();

            //Record the starting line of the cursor 
            formTop = Console.CursorTop;

            // Track the cursor's horizontal position
            int cursorPosition = 0;
            

            // loop to handle user input 
            while (true)
            {
               



               
                int inputLeftPosition = 11; // Strating colum for user input text 

                // Draw the ID field,re draw the entire menu on each key press  
                Console.SetCursorPosition(0, formTop);
                Console.Write($"ID       : {values[0]}".PadRight(Console.WindowWidth - 1));

                // Draw the password field, use * to mask 
                Console.SetCursorPosition(0, formTop + 1);
                Console.Write($"Password : {new string('*', values[1].Length)}".PadRight(Console.WindowWidth - 1));

                // Place Cusor at the position in the current active field 
                Console.SetCursorPosition(inputLeftPosition + cursorPosition, formTop + currentField);

                // Waiting the next key press from the user 
                keyInfo = Console.ReadKey(true);

                // Handle the user's key press 
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break; 
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = 0;
                    cursorPosition = values[currentField].Length;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = 1;
                    cursorPosition = values[currentField].Length;
                }

                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (cursorPosition > 0)
                    {
                        cursorPosition--; // Move cursor left 
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (cursorPosition < values[currentField].Length)
                    {
                        cursorPosition++; // Move curso right 
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (cursorPosition > 0)
                    {
                        // Remove the character before the cursor 
                        values[currentField] = values[currentField].Remove(cursorPosition - 1, 1);
                        cursorPosition--; // Cusor position move to left 
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Delete)
                {
                    if (cursorPosition < values[currentField].Length)
                    {
                        // Remove the character at the cursors' position 
                        values[currentField] = values[currentField].Remove(cursorPosition, 1);
                        //Cusor position remain unchange
                    }
                }



                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    //Insert the typed Character at the cursor's position 
                    values[currentField] = values[currentField].Insert(cursorPosition, keyInfo.KeyChar.ToString());
                    cursorPosition++; 
                }
            } 

            // assign the values for processing after loop 
            this.Id = values[0];
            this.Password = values[1];

            Console.SetCursorPosition(0, formTop + 4);

            //Basic validation to make sure fields are not empty 
            if (string.IsNullOrWhiteSpace(this.Id) || string.IsNullOrWhiteSpace(this.Password))
            {
                Console.WriteLine("ID or Password cannot be empty. Press <Enter> to try again.");
                Console.ReadLine();
                LoginMenu();
                return;
            }

            HandleUserLogin();
        }


        

       

        //Construct  relative file path within the applicaiton Data path directory 
        private static string DataPathOf(string roleFolder, string fileName)
    => Path.Combine(AppContext.BaseDirectory, "Data", roleFolder, fileName);



        


        private void HandleUserLogin()
        {
            try

            {   // user entered their Id to find the file 
                string fileName = $"{Id}.txt";

                //Construct paths for  possible user roles 
                string adminPath = DataPathOf("Administrators", fileName);
                string doctorPath = DataPathOf("Doctors", fileName);
                string patientPath = DataPathOf("Patients", fileName);


                //Debuggin code to test if the path is exists 
                //Console.WriteLine($"[DEBUG] adminPath   = {adminPath}   Exists={File.Exists(adminPath)}");
                //Console.WriteLine($"[DEBUG] doctorPath  = {doctorPath}  Exists={File.Exists(doctorPath)}");
                //Console.WriteLine($"[DEBUG] patientPath = {patientPath} Exists={File.Exists(patientPath)}");


                //Check the user file to identify the users' role 
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
                    // if no file found , the user ID is invalid 
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
                        LoginMenu();   //Return to the login menu when failed
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

        private void VerfiyCred(string position, string file)
        {
            //read the first line of the user file, which contain id and password 
            string[] lines = File.ReadAllLines(file, Encoding.UTF8);
            string[] details = lines[0].Split("|");


            //check if both Id and password match the  file 
            if (Id == details[0] && Password == details[1])
            {
                Console.WriteLine("User account exists");

                Console.ReadKey();



                // Navigate user to the interface based on their role
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
            {   // If the password doesn't match, throw an exception , it will be caught by the handleuserLogin()
                throw new Exception("Invalid Password, press <Enter> to try again");
            }

        }


    }










        

    }

