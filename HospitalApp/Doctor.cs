using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Doctor : User, PersonalDetails
    {
        public string Name => base.Name;
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }





        public Doctor(string Id, string Password, string Name, string Address, string Email, string Phone, string Role) : base(Id, Password, Name, Role)
        {
            this.Address = Address;
            this.Email = Email;
            this.Phone = Phone;

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

            Console.WriteLine($"Doctor Name: {Name}");

            Console.ReadKey();
            Menu();



        }


        private void ListMyDetails()
        {
            //Console.Clear();

            //Console.WriteLine("┌────────────────────────────────────────┐");
            //Console.WriteLine("|                                        |");
            //Console.WriteLine("|   DOTNET Hospital Management System    |");
            //Console.WriteLine("|--------------------------------------- |");
            //Console.WriteLine("|               My Details               | ");
            //Console.WriteLine("└────────────────────────────────────────┘ ");
            //Console.WriteLine();

            String instructions = $" {Name} 's Details ";
            Utils.DisplayHeader("My Details", instructions);




            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));
            Console.WriteLine($"{Name,-20} | {Email,-25} | {Phone,-15} | {Address,-30}");

            Console.ReadLine();


        }

        private void AssignedPatients()
        {
          



            String instructions = $"Patients assigned to {Name} ";
            Utils.DisplayHeader("My Patients", instructions);






            try
            {
                string RegisteredPatient = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", "RegisteredPatient", $"{this.Id}.txt"); ;




                if (File.Exists(RegisteredPatient))
                {
                    string patientId = File.ReadAllText(RegisteredPatient).Trim();
                    Patient patient = Utils.FindUserById<Patient>(patientId);

                   

             if(patient !=null) { 
                        Utils.PrintPersonDetails(patient);


                    }
                    else
                    {
                        Console.WriteLine($"Could not find the patient id {patientId} (First else)!!!! ");
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

           


            String instructions = " ";
            Utils.DisplayHeader("All Appointments", instructions);


            Console.WriteLine("This is a Appointments listing details ");

            var appointments = new List<Appointment>();

            try
            {
                
                string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Doctors", $"{this.Id}.txt");

                if (File.Exists(appointmentFilePath))
                {
                    string[] lines = File.ReadAllLines(appointmentFilePath);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] data = line.Split('|');
                        if (data.Length >= 3)
                        {
                            
                            string patientId = data[0].Trim(); 
                            string description = data[2].Trim();
                            string patientName = "Unknown Patient"; 

                         
                            string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");
                            if (File.Exists(patientPath))
                            {
                                string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                                patientName = patientData[2]; 
                            }

                           
                            appointments.Add(new Appointment(this.Name, patientName, description));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching appointments: {e.Message}");
            }

           
            Utils.PrintAppointmentsTable(appointments);

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }








        private void ListsAppointmentsWithPatients()
        {

            while (true) {
               


                string instructions = "Enter the ID of the patient you would like to view appointments for: ";
                Utils.DisplayHeader("Appointments with", instructions);
                
            

           
            string errorMessage = "";
            
            

           
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }

           
            Console.Write("> ");
            string patientId = Console.ReadLine();

            if (patientId.ToLower() == "exit")
            {
                break; 
            }

            if (string.IsNullOrWhiteSpace(patientId))
            {
                errorMessage = "Patient ID cannot be empty. Please try again.";
                continue;
            }

            try
            {
               
                string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                if (File.Exists(patientPath))
                {
                    
                    var appointments = new List<Appointment>();
                    string patientName = File.ReadAllLines(patientPath)[0].Split('|')[2]; // 从病人文件获取名字

                    string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{patientId}.txt");

                
                    if (File.Exists(appointmentFilePath))
                    {
                        string[] lines = File.ReadAllLines(appointmentFilePath);
                        foreach (string line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            string[] data = line.Split('|');
                            if (data.Length >= 3)
                            {
                               
                                if (data[1].Trim() == this.Id)
                                {
                                    string description = data[2].Trim();
                                   
                                    appointments.Add(new Appointment(this.Name, patientName, description));
                                }
                            }
                        }
                    }

                   
                    Utils.PrintAppointmentsTable(appointments);

                  
                    break;
                }
                else
                {
                    
                    errorMessage = $"Error: No patient found with ID '{patientId}'. Please try again.";
                    continue;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
                continue;
            }
        } 
        Console.WriteLine("\nPress <Enter> to return to the menu.");
        Console.ReadLine();
    }











        private void CheckPatients()
        {

            

            String instructions = "Enter the ID of the patient to check: ";
            Utils.DisplayHeader("Check Patient Details", instructions);

            


            
                
                string patientId = Console.ReadLine();

             
                if (string.IsNullOrWhiteSpace(patientId))
                {
                    Console.WriteLine("\nPatient ID cannot be empty. Please try again.");
                    Console.ReadLine();
                    return;
                }

                try
                {
                    
                    string patientPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{patientId}.txt");

                    if (File.Exists(patientPath))
                    {
                      
                        string[] patientData = File.ReadAllLines(patientPath)[0].Split('|');
                        
                        Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");

                        
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.Id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");

                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                Doctor doctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");

                              
                                Utils.PrintPatientDetails(patient, doctor);
                            }
                        }
                        else
                        {
                            
                            Console.WriteLine($"\nCould not find the assigned doctor for patient {patient.Name}.");
                        }
                    }
                    else
                    {
                        
                        Console.WriteLine($"\nError: No patient found with ID '{patientId}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nAn error occurred: {ex.Message}");
                }

                Console.WriteLine("\nPress <Enter> to return to the menu.");
                Console.ReadLine();
            }







        public override string ToString()
        {
           
            return $"{this.Name,-20} | {this.Email,-25} | {this.Phone,-15} | {this.Address,-30}";
        }



        public override void Menu()
        {
            while (true)
            {
                


                String instructions = $"Welcome to DOTNET Hospital Management System {Name}";
                Utils.DisplayHeader("Doctor Menu", instructions);


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
        ~Doctor()
        {
            Console.WriteLine("Doctor object destroyed and clearing memory");
            GC.Collect();
        }

    }
}
