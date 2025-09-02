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

        private string address, email, phone;

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

            Console.WriteLine($"Doctor name: {name}");


            Console.WriteLine($"{"Patient",-20} | {"Doctor",-20} | {"Email Address",-25} | {"Phone", -15} | {"Address",-30} ");

            Console.WriteLine(new string('-',115));


            Console.ReadLine();
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
