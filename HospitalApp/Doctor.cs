using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Doctor:User
    {

        public string address { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }





        public Doctor(string id, string password,string name, string address, string email, string phone,string role) : base(id, password, name, role)
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


            


            Console.ReadLine();
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
                        Utils.PrintPatientDetails(patient);
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

            Console.ReadLine();




        }



        private void ListsAppointmentsWithPatients()
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
            Console.WriteLine("Lists appointments with the particular patients  ");

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
