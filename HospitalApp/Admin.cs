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


        public override void Menu()
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

            foreach(string option in options)
            {
                Console.WriteLine(option );
            }



        }






    }
}
