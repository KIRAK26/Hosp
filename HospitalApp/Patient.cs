using HospitalApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{

    //  Patient is inheriting from the base USer class and connect to the PersonalDetails interface. 
    internal class Patient: User, PersonalDetails

    {



        //Publicly exposes the Name property from the base User class 
        public string Name => base.Name;
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        // Constructor for creating an Patient object, calling the base User constructor.
        public Patient(string Id, string Password, string Name, string Address, string Email, string Phone, string Role)
    : base(Id, Password, Name, Role)
        {
            this.Address = Address;
            this.Email = Email;
            this.Phone = Phone;
        }



        // This is the represent the user details .
        public void Details()
        {
            

            String instructions = $"{Name}'s Details";
            Utils.DisplayHeader("My Details", instructions);
           


            Console.WriteLine();

            Console.ReadKey();

            Menu();

        }


        
        private void AssignedDoctor()
        {
            

            String instructions = "Your doctor: ";
            Utils.DisplayHeader("My Doctor", instructions);


            try
            {   //Store the file path taht stores the patient-doctor relationship
                string RegisteredDoctor = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{this.Id}.txt"); ;




                if (File.Exists(RegisteredDoctor))
                {    // If the file exists, read the doctor's ID and find the doctor's details.
                    string doctorId = File.ReadAllText(RegisteredDoctor).Trim();
                    Doctor doctor = Utils.FindUserById<Doctor>(doctorId);


                    if (doctor != null)
                    {
                        Utils.PrintPersonDetails(doctor);
                    }





                    else
                    {
                        Console.WriteLine($"Cound not found the ID {doctorId} doctor");
                    }

                }
                else
                {
                    Console.WriteLine("You don't have any registered Doctor (you triggered the second else)");
                }



            }

            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }

            Console.ReadKey();
        }
        private void BookAppointments()
        {

            Doctor assignedDoctor = null;
            string patientId = this.Id;


            


            String instructions = "";
            Utils.DisplayHeader("My Appointments", "" );
            Console.WriteLine();

            string relationshipFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patientId}.txt");

            try
            {
                // Check if the patient is already registered with a doctor.
                if (!File.Exists(relationshipFilePath))
                {
                
                    Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with:"); //

                    // If not registered, load and display a list of all available doctors.
                    var allDoctors = new List<Doctor>();
                    string doctorsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors");
                    string[] doctorFiles = Directory.GetFiles(doctorsDirectoryPath, "*.txt");
                    foreach (string filePath in doctorFiles)
                    {
                        string[] doctorData = File.ReadAllLines(filePath)[0].Split('|');
                        if (doctorData.Length >= 6)
                        {
                            allDoctors.Add(new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors"));
                        }
                    }

                    if (allDoctors.Count == 0)
                    {
                        Console.WriteLine("Sorry, there are no doctors available in the system.");
                        Console.ReadLine();
                        return;
                    }

                    // Display the list for the patient to choose from.
                    for (int i = 0; i < allDoctors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {allDoctors[i]}"); // 这里利用了我们为Doctor写的ToString()方法
                    }

                   
                    Console.Write("Please choose a doctor: "); //
                    int choice = -1;
                    if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= allDoctors.Count)
                    {
                        assignedDoctor = allDoctors[choice - 1];

                        // Assign the chosen doctor and create the relationship files.
                        File.WriteAllText(relationshipFilePath, assignedDoctor.Id);

                       
                        string doctorRelationshipPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", "RegisteredPatient", $"{assignedDoctor.Id}.txt");
                        File.AppendAllText(doctorRelationshipPath, $"{patientId}{Environment.NewLine}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Returning to menu.");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                   // If already registered, load the assigned doctor's details directly.
                    string doctorId = File.ReadAllText(relationshipFilePath).Trim();
                    string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                    if (File.Exists(doctorPath))
                    {
                        string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                        assignedDoctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                    }
                }

                // Proceed with booking if a doctor is assigned.
                if (assignedDoctor != null)
                {
                    Console.WriteLine($"\nYou are booking a new appointment with {assignedDoctor.Name}"); //
                    Console.Write("Description of the appointment: "); //
                    string description = Console.ReadLine();

                    // Format the appointment data and 
                    string appointmentData = $"{patientId}|{assignedDoctor.Id}|{description}{Environment.NewLine}";

                    
                    string patientAppointmentPath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{patientId}.txt");
                    string doctorAppointmentPath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Doctors", $"{assignedDoctor.Id}.txt");
                    //Save it to both patient's and doctor's appointment files.
                    File.AppendAllText(patientAppointmentPath, appointmentData);
                    File.AppendAllText(doctorAppointmentPath, appointmentData);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The appointment has been booked successfully"); //
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Could not find doctor details. Appointment booking failed.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred during booking: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu."); //
            Console.ReadLine();
        }





        

        private void ListAllAppointments()
        {
            


            String instructions = $"Appointments for {Name} ";
            Utils.DisplayHeader("My Appointments", instructions);


            var appointments = new List<Appointment>();

            try
            {
                
                string appointmentFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Appointments", "Patients", $"{this.Id}.txt");

                if (File.Exists(appointmentFilePath))
                {
                   
                    string[] lines = File.ReadAllLines(appointmentFilePath);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue; 

                        string[] data = line.Split('|');
                        if (data.Length >= 3)
                        {
                            // For each appointment, find the doctor's name to display it.
                            string doctorId = data[1].Trim();
                            string description = data[2].Trim();
                            string doctorName = ""; 

                            
                            string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                            if (File.Exists(doctorPath))
                            {
                                string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                                doctorName = doctorData[2]; 
                            }

                            
                            appointments.Add(new Appointment(doctorName, this.Name, description));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching appointments: {e.Message}");
            }

            // Use the utility function to print the formatted table of appointments.
            Utils.PrintAppointmentsTable(appointments);

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            




            Console.ReadLine();
        }

        private void ListPaitentsDetails()
        {

           


            String instructions = $"{Name} 's Details ";
            Utils.DisplayHeader("My Details", instructions);



            Console.WriteLine($"{Name}'s Details");
            Console.WriteLine($"Patient Id: {Id} ");
            Console.WriteLine($"Full Name: {Name} ");
            Console.WriteLine($"Address: {Address} ");
            Console.WriteLine($"Email: {Email} ");
            Console.WriteLine($"Phone: {Phone} ");
            Console.ReadLine();



        }





        // Overrides the abstract Menu method from the User class to provide patient-specific options.
        public override void Menu()
        {
            while (true) {
              
                String instructions = $"Welcome to DOTNET Hospital Management System {Name}";
                Utils.DisplayHeader("Patient Menu ", instructions);


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


        // Destructor for the Patient class 
        ~Patient()
        {
            Console.WriteLine("Patient object destroyed and clearing memory");
            GC.Collect();
        }



    }
}
