using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Admin : User
    {
        // Constructor for creating an Admin object, calling the base User constructor.
        public Admin(string Id, string Password, string Name, string Role) : base(Id, Password, Name, Role)
        {

        }



        // Initial landing method after login for the admin.
        public void Details()
        {
            Console.WriteLine($"I am the admin {Name}");

            Menu();
        }


        // Retrieves and displays a list of all doctors in the system.
        private void ListsAllDoctors()
        {

            String instructions = "All doctors registered to the DOTNET Hospital Management System";
            Utils.DisplayHeader("All Doctors", instructions);

            var allDoctors = new List<Doctor>();

            try
            {
           
                string doctorsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors");

               
                if (Directory.Exists(doctorsDirectoryPath))
                {
                 
                    string[] doctorFiles = Directory.GetFiles(doctorsDirectoryPath, "*.txt");

              
                    foreach (string filePath in doctorFiles)
                    { // Parses each file to create Doctor objects.
                        string[] doctorData = File.ReadAllLines(filePath)[0].Split('|');
                       // To ensure that the Doctor data are all have complete data 
                        if (doctorData.Length >= 6) 
                        {
                            Doctor doctor = new Doctor(doctorData[0], doctorData[1], doctorData[2], doctorData[3], doctorData[4], doctorData[5], "Doctors");
                            allDoctors.Add(doctor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading doctor files: {ex.Message}");
            }

            // Prints the formatted table of doctors.
            Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-15} | {"Address",-30}");
            Console.WriteLine(new string('-', 95));

            if (allDoctors.Count == 0)
            {
                Console.WriteLine("No doctors found in the system.");
            }
            else
            {
                
                foreach (Doctor doc in allDoctors)
                {
                   
                    Console.WriteLine(doc);
                }
            }

           
            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }


        // Check for a doctor's ID and displays their details.
        private void CheckDoctorDetails()
        {
            string errorMessage = "";

            // Loop indefinitely until a valid doctor is found and details are displayed
            while (true) {
       
                string whoMenu = "Administrator Menu";
                String insructions = "Enter the ID of the doctor to check";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);
     
                Console.Write("> ");
                string doctorId = Console.ReadLine();

          
                if (string.IsNullOrWhiteSpace(doctorId))
                {
                    errorMessage = "Doctor ID cannot be empty. Please try again.";
                    continue;
                }

                try
                {
                    // Use a generic utility method to find the user by ID, specifying the type as Doctor.
                    Doctor doctor = Utils.FindUserById<Doctor>(doctorId);


                    if (doctor != null)
                    {
                        // Display the doctor's details.
                        Utils.DisplayHeader($"Doctor Details", $"Details for {doctor.Name} ", "");
                        Utils.PrintPersonDetails(doctor);

                        
                        break;
                    }
                    else
                    {
                    // If no doctor was found, set an error message and restart the loop.
                        errorMessage = $"Error: No doctor found with ID '{doctorId}'. Please try again.";
                        continue;
                    }
                }
                catch (Exception ex)
                {// Handle any potential exceptions during the search.
                    errorMessage = $"An error occurred: {ex.Message}";
                    continue;
                }
            }


            // Pause for user to press Enter before returning to the menu.
            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }
        // Retrieves and displays a formatted list of all patients and their assigned doctors.
        private void ListAllPatients()
        {
            
            String instructions = "All patients registered to the DOTNET Hospital Management System";
            Utils.DisplayHeader("All Patients", instructions);

            Console.WriteLine("This is to check Patients' details ");

            var allPatients = new List<Patient>();
            try
            {// Define the path to the directory containing patient data files
                string patientsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients");
                if (Directory.Exists(patientsDirectoryPath))
                {// Get all patient files.
                    string[] patientFiles = Directory.GetFiles(patientsDirectoryPath, "*.txt");
                    foreach (string filePath in patientFiles)
                    {
                        string[] patientData = File.ReadAllLines(filePath)[0].Split('|');
                        // Check for data integrity to prevent broken data
                        if (patientData.Length >= 6)
                        {
                            Patient patient = new Patient(patientData[0], patientData[1], patientData[2], patientData[3], patientData[4], patientData[5], "Patients");
                            allPatients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading patient files: {ex.Message}");
            }

            // Print table headers.
            Console.WriteLine($"{"Patient",-15} | {"Doctor",-15} | {"Email Address",-25} | {"Phone",-12} | {"Address",-30}");
            Console.WriteLine(new string('-', 120));


            if (allPatients.Count == 0)
            {
                Console.WriteLine("No patients found in the system.");
            }
            else
            {
                // Iterate through each patient to display their details.
                foreach (Patient pat in allPatients)
                {
                    // Check for an associated doctor file.
                    string doctorName = "Not Assigned"; 
                    string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{pat.Id}.txt");
                    if (File.Exists(registeredDoctorPath))
                    {
                        string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                        string doctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{doctorId}.txt");
                        if (File.Exists(doctorPath))
                        {
                            string[] doctorData = File.ReadAllLines(doctorPath)[0].Split('|');
                            doctorName = doctorData[2];
                        }
                    }

                    // Print the formatted line with patient and doctor info.
                    Console.WriteLine($"{pat.Name,-15} | {doctorName,-15} | {pat.Email,-25} | {pat.Phone,-12} | {pat.Address,-30}");
                }
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }


        //Enter a patient's ID, then finds and displays that patient's details,
        private void CheckPatientsDetails()
        {
            string errorMessage = "";


            while (true)
            {

                string whoMenu = "Administrator Menu";
                String insructions = "Enter the ID of the patient to check";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);




                Console.Write("> ");
                string patientId = Console.ReadLine();



                if (string.IsNullOrWhiteSpace(patientId))
                {
                    errorMessage = "Patient ID cannot be empty. Please try again.";
                    continue;
                }

                try
                {
                    // Find the patient by their ID.
                    Patient patient = Utils.FindUserById<Patient>(patientId);

                    if (patient != null)
                    {

                        // Assume no doctor is assigned initially.
                        Doctor assignedDoctor = null;
                        // Check for the file linking the patient to a doctor.
                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.Id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            // Find the assigned doctor's object.
                            assignedDoctor = Utils.FindUserById<Doctor>(doctorId);
                        }
                        // Display the patient and their doctor's details.
                        Utils.DisplayHeader("Patient Details", $"Details for {patient.Name} ({patient.Id})", "");
                        Utils.PrintPatientDetails(patient, assignedDoctor);
                         Console.ReadLine();

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
            Console.WriteLine("\nPress <Enter> to return to the me add a new doctor to the system.");

                }
        private void AddDoctor()
        {
            // Define the form fields and an array to store their values.
            var labels = new string[]
            {
        "First Name", "Last Name", "Password", "Email", "Phone",
        "Street Number", "Street", "City", "State"
            };
            var values = new string[labels.Length];
            for (int i = 0; i < values.Length; i++) values[i] = "";

            int currentField = 0;
            string errorMessage = "";
            ConsoleKeyInfo keyInfo;

            // Main loop to handle form input.
            while (true)
            {
          
                string whoMenu = "Add Doctor";
                String insructions = "Use UP/DOWN arrows to switch. Press ENTER to submit when done ";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);


                //Form Rendering Logic
                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);
                    // Mask the password field with asterisks.
                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }
                // Position cursor at the end of the current field for typing.
                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {  // On Enter, validate if all fields are filled.
                    bool allFieldsFilled = !values.Any(string.IsNullOrWhiteSpace);
                    if (allFieldsFilled)
                    {
                        break;
                    }
                    else
                    {
                        errorMessage = "All fields are required. Please fill in the missing information.";
                        continue;
                    }
                }
                // Handle navigation and text editing.
                else if (keyInfo.Key == ConsoleKey.UpArrow) currentField = (currentField == 0) ? labels.Length - 1 : currentField - 1;
                else if (keyInfo.Key == ConsoleKey.DownArrow) currentField = (currentField == labels.Length - 1) ? 0 : currentField + 1;
                else if (keyInfo.Key == ConsoleKey.Backspace && values[currentField].Length > 0) values[currentField] = values[currentField].Substring(0, values[currentField].Length - 1);
                else if (!char.IsControl(keyInfo.KeyChar)) values[currentField] += keyInfo.KeyChar;
            } 
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            try
            {
                // Process the submitted form data.
                string firstName = values[0].Capitalize();
                string lastName = values[1].Capitalize();
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7].Capitalize();
                string state = values[8].ToUpper();

                // Generate a new, unique ID for the doctor.
                string newDoctorId;
                string doctorFilePath;
                Random rand = new Random();
                do
                {
                    newDoctorId = rand.Next(10000, 99999999).ToString();
                    
                    doctorFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{newDoctorId}.txt");
                } while (File.Exists(doctorFilePath));

                // Combine data into a single string for file storage.
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string doctorData = $"{newDoctorId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

                // Write the doctor's data to a new file.

                File.WriteAllText(doctorFilePath, doctorData);

                // Display a success message.
                Console.ForegroundColor = ConsoleColor.Green;
              
                Console.WriteLine($"\nDoctor {fullName} added to the system!");
                Console.ResetColor();
            }

            // Display an error message if something goes wrong.
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }


       // Allow admin to addPatient from the console 
        private void AddPatient()
        {
            //  The implementation is very similar to AddDoctor

            // Define the form fields and an array to store their values.
            var labels = new string[]
            {
        "First Name", "Last Name", "Password", "Email", "Phone",
        "Street Number", "Street", "City", "State"
            };
            var values = new string[labels.Length];
            for (int i = 0; i < values.Length; i++) values[i] = "";

            int currentField = 0;
            string errorMessage = "";
            ConsoleKeyInfo keyInfo;

           // Main loop to handle form input.
            while (true)
            {
              
                string whoMenu = "Add Doctor";
                String insructions = "Use UP/DOWN arrows to switch. Press ENTER to submit when done ";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);

                // --- Form Rendering Logic ---
                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);

                    // Mask the password field with asterisks.
                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }
                // Position cursor at the end of the current field for typing.
                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);

                // Read key without displaying it.
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
           
                    bool allFieldsFilled = true;
                    foreach (string value in values)
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            allFieldsFilled = false; 
                            break;// Exit the form loop if validation passes
                        }
                    }

                    if (allFieldsFilled)
                    {
                        break; 
                    }
                    else
                    {
                        
                        errorMessage = "All fields are required. Please fill in the missing information.";
                        continue;
                    }
                }
                // Handle navigation and text editing.
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField == 0) ? labels.Length - 1 : currentField - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField == labels.Length - 1) ? 0 : currentField + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (values[currentField].Length > 0)
                    {
                        values[currentField] = values[currentField].Substring(0, values[currentField].Length - 1);
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    values[currentField] += keyInfo.KeyChar;
                }

                
            } 
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            try
            {// Process the submitted form data.

                string firstName = values[0].Capitalize();
                string lastName = values[1].Capitalize();
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7].Capitalize();
                string state = values[8].ToUpper();

                // Generate a new, unique ID for the doctor.
                string newPatientId;
                string patientFilePath;
                Random rand = new Random();
                do
                {
                    newPatientId = rand.Next(10000, 99999999).ToString();
                    patientFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{newPatientId}.txt");
                } while (File.Exists(patientFilePath));

                // Combine data into a single string for file storage.
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string patientData = $"{newPatientId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

                // Write the doctor's data to a new file.
                File.WriteAllText(patientFilePath, patientData);

               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPatient {fullName} added to the system!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }


        //Overrides the base Menu method to provide admin-specific options.
        public override void Menu()
        {

            while (true)
            {
          
                String instructions = $"Welcome to DOTNET Hospital Management System {Name}";
                Utils.DisplayHeader("Administrator", instructions);


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

        //Desctuctor for Admin 
        ~Admin()
        {
            Console.WriteLine("Administrator object destroyed and clearing memory");
            GC.Collect();
        }

    }
}
