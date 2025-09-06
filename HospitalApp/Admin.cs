using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Admin:User
    {
        



        public Admin(string id, string password, string name, string role) : base(id, password, name,role)
        {
            
        }

        public void Details()
        {
            Console.WriteLine( $"I am the admin {name}");

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




            Console.WriteLine($"This is an Adminstrator menu, welcome, you want to see all the doctors lists here ? {name}");


            Console.ReadLine();



        }


        private void CheckDoctorDetails()
        {
            Console.Clear();

            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|   DOTNET Hospital Management System    |");
            Console.WriteLine("|--------------------------------------- |");
            Console.WriteLine("|              Administrator Menu        | ");
            Console.WriteLine("└────────────────────────────────────────┘ ");
            Console.WriteLine();




            Console.WriteLine("This is to check Doctor's details ");


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


            Console.ReadLine();

        }

        private void CheckPatientsDetails()
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
