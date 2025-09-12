using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public abstract class User
    {
        //Public fileds for common suer properties 
        public string Role, Name, Id, Password;

        // An array to hold the menu options specifci to the usesr' role
        public string[] options = Array.Empty<string>();




        public User(string Id, string Password, string Name, string Role)
        {
            this.Id = Id;
            this.Password = Password;
            this.Name = Name;
            this.Role = Role;


            // Initialize the menu options based on the user's role 

            switch (Role)
            {
                case "Patients":
                    options = new string[] {
                        "1. List patient Details",
                        "2. List my doctor details",
                        "3. List all appointments",
                        "4. Book appointment",
                        "5. Exit to login",
                        "6: Exit System"
                    };
                    break;

                case "Doctors":
                    options = new string[]
                    {
                        "1. List doctor Details",
                        "2. List patients",
                        "3. List appointments",
                        "4. Check particular patient",
                        "5. List appointments with patient",
                        "6. Logout",
                        "7. Exit"

                    };

                    break;


                case "Administrator":
                    {
                        options = new string[] {

                        "1. List all doctor ",
                        "2. Check doctor details",
                        "3. List all patients ",
                        "4. Check patient details",
                        "5. Add doctor",
                        "6. Add patient",
                        "7. Logout",
                        "8. Exit"
                        };
                    }

                    break;
                default:
                    throw new Exception("No such Role has been found");

            }

        }
        //Use abstract keyword to force all derived class to have its own specifci menu logic 
        public abstract void Menu();





        ~User()
        {
            Console.WriteLine("User object destroyed and clearing memory");
            GC.Collect();

        }





    }
    }

