using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal static class Utils
    {
        
        public class FormField
        {
            public string Label { get; set; }   // Label of the field like "ID" 
            public string Value { get; set; }   // Value entered by user 
            public bool IsPassword { get; set; } // Indicate the filed is a password 
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

            
            Console.WriteLine(subtitle);
            Console.WriteLine();
        }


        //This is an overload of Displayheader that includes an additional line for extra infromation 
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

            //Print the descriptivie instructions 
            Console.WriteLine(instruction);

            //An additional string for status messages or errors.
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
                //Format the person's data to align with the header 
                Console.WriteLine($"{person.Name,-20} | {person.Email,-25} | {person.Phone,-15} | {person.Address,-30}");
            }
            else
            {
                
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
                    Console.Write("\b \b"); // Erase the last * .
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return Password;
        }



        // A method that find and create a user object by its ID from a text file.
        public static T FindUserById<T>(string userId) where T : User
        {
            //Automatically determine the data folder based on the user type
            string roleFolder = typeof(T).Name + "s";
            string filePath = Path.Combine(AppContext.BaseDirectory, "Data", roleFolder, $"{userId}.txt");

            if (!File.Exists(filePath))
            {
                return null; //Return null if the user file doesn't exist.
            }

            try
            {
                string[] data = File.ReadAllLines(filePath)[0].Split('|');
                if (data.Length >= 6)
                {
                    // Dynamically create an instance of the user type (T) using its constructor.
                    object user = Activator.CreateInstance(typeof(T), data[0], data[1], data[2], data[3], data[4], data[5], roleFolder);
                    return (T)user;
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($" {userId} :  {ex.Message}");
            }

            return null; //Return null if file data is invalid or an exception occurs.
        }








        // A method displays a list of appointments in a formatted table
        public static void PrintAppointmentsTable(List<Appointment> appointments)
        {

            Console.WriteLine($"{"Doctor",-25} | {"Patient",-25} | {"Description",-40}");
            Console.WriteLine(new string('-', 95));

            if (appointments == null || appointments.Count == 0)
            {
                Console.WriteLine("You have no appointments booked.");
                return;
            }

            // Loop through all the Appointment objects 
            foreach (Appointment appt in appointments)
            {
                
                // Get the values from appt.DoctorName, appt.PatientName, appt.Description 
                Console.WriteLine($"{appt.DoctorID,-25} | {appt.PatientID,-25} | {appt.Description,-40}");
            }
        }



        public static void PrintPatientDetails(Patient patient, Doctor doctor)
        {
            
            Console.WriteLine($"{"Patient",-15} | {"Doctor",-15} | {"Email Address",-25} | {"Phone",-12} | {"Address",-40}");
            Console.WriteLine(new string('-', 115));

            
            if (patient is null)
            {
                

               
                Console.WriteLine("Patient details could not be displayed.");
                return;

            }

            Console.WriteLine($"{patient.Name,-15} | {(doctor?.Name?? "Unassigned"),-15} | {patient.Email,-25} | {patient.Phone,-12} | {patient.Address,-40}");

        }
    }
    }
    

