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
    internal class Patient: User

    {

        private string address, email, phone;

        public Patient(string id, string password, string name, string address, string email, string phone, string role)
    : base(id, password, name, role)
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
            Console.WriteLine($"{name}'s Details");


            Console.WriteLine();

            Console.ReadKey();

            Menu();

        }

         private void AssignedDoctor()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine("Your doctor:");

            Console.WriteLine( );
            Console.WriteLine("This is just a test case code ");


            Console.ReadLine();
        }
        private void BookAppointments()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();
            Console.WriteLine($"You can try to book appointment here , {name}");

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
            Console.WriteLine($"Appointments for {name}  ");
            Console.WriteLine("This will tell you that this a appointments");

            Console.ReadLine();
        }

        private void ListPaitentsDetails()
        {

            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|               My Details               | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();


            Console.WriteLine($"{name}'s Details");
            Console.WriteLine($"Patient Id: {id} ");
            Console.WriteLine($"Full Name: {name} ");
            Console.WriteLine($"Address: {address} ");
            Console.WriteLine($"Email: {email} ");
            Console.WriteLine($"Phone: {phone} ");
            Console.ReadLine();



        }





        public override void Menu()
        {
            while (true) {
                Console.Clear();

                Console.WriteLine("┌────────────────────────────────────────┐");
                Console.WriteLine("|                                        |");
                Console.WriteLine("|   DOTNET Hospital Management System    |");
                Console.WriteLine("|--------------------------------------- |");
                Console.WriteLine("|              Patient Menu              | ");
                Console.WriteLine("└────────────────────────────────────────┘ ");
                Console.WriteLine();


                Console.WriteLine($"Welcome to DOTNET Hospital Management System {name}");

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



    }
}
