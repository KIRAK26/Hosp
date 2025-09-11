using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Admin : User
    {




        public Admin(string Id, string Password, string Name, string Role) : base(Id, Password, Name, Role)
        {

        }

        public void Details()
        {
            Console.WriteLine($"I am the admin {Name}");

            Menu();
        }


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
                    {
                        string[] doctorData = File.ReadAllLines(filePath)[0].Split('|');
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



        private void CheckDoctorDetails()
        {
            string errorMessage = "";
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
                
                    Doctor doctor = Utils.FindUserById<Doctor>(doctorId);


                    if (doctor != null)
                    {
                       
                   
                       

                        Utils.DisplayHeader($"Doctor Details", $"Details for {doctor.Name} ", "");
                        Utils.PrintPersonDetails(doctor);

                        
                        break;
                    }
                    else
                    {
                    
                        errorMessage = $"Error: No doctor found with ID '{doctorId}'. Please try again.";
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










        private void ListAllPatients()
        {
            

            

            String instructions = "All patients registered to the DOTNET Hospital Management System";
            Utils.DisplayHeader("All Patients", instructions);


            Console.WriteLine("This is to check Patients' details ");


            var allPatients = new List<Patient>();
            try
            {
                string patientsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients");
                if (Directory.Exists(patientsDirectoryPath))
                {
                    string[] patientFiles = Directory.GetFiles(patientsDirectoryPath, "*.txt");
                    foreach (string filePath in patientFiles)
                    {
                        string[] patientData = File.ReadAllLines(filePath)[0].Split('|');
                        //To prevent broken data 
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

          
            Console.WriteLine($"{"Patient",-15} | {"Doctor",-15} | {"Email Address",-25} | {"Phone",-12} | {"Address",-30}");
            Console.WriteLine(new string('-', 120));


            if (allPatients.Count == 0)
            {
                Console.WriteLine("No patients found in the system.");
            }
            else
            {
              
                foreach (Patient pat in allPatients)
                {
                   
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

                   
                    Console.WriteLine($"{pat.Name,-15} | {doctorName,-15} | {pat.Email,-25} | {pat.Phone,-12} | {pat.Address,-30}");
                }
            }

            Console.WriteLine("\nPress <Enter> to return to the menu.");
            Console.ReadLine();
        }



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
                   
                    Patient patient = Utils.FindUserById<Patient>(patientId);

                    if (patient != null)
                    {
                       
                     
                        Doctor assignedDoctor = null; 

                        string registeredDoctorPath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", "RegisteredDoctors", $"{patient.Id}.txt");
                        if (File.Exists(registeredDoctorPath))
                        {
                            string doctorId = File.ReadAllText(registeredDoctorPath).Trim();
                            
                            assignedDoctor = Utils.FindUserById<Doctor>(doctorId);
                        }

                        Utils.DisplayHeader("Patient Details", $"Details for {patient.Name} ({patient.Id})", "");
                        Utils.PrintPatientDetails(patient, assignedDoctor);
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





        private void AddDoctor()
        {
            
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

           
            while (true)
            {
            


                string whoMenu = "Add Doctor";
                String insructions = "Use UP/DOWN arrows to switch. Press ENTER to submit when done ";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);

               

                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);
                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }

                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
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
                else if (keyInfo.Key == ConsoleKey.UpArrow) currentField = (currentField == 0) ? labels.Length - 1 : currentField - 1;
                else if (keyInfo.Key == ConsoleKey.DownArrow) currentField = (currentField == labels.Length - 1) ? 0 : currentField + 1;
                else if (keyInfo.Key == ConsoleKey.Backspace && values[currentField].Length > 0) values[currentField] = values[currentField].Substring(0, values[currentField].Length - 1);
                else if (!char.IsControl(keyInfo.KeyChar)) values[currentField] += keyInfo.KeyChar;
            } 
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            try
            {
               
                string firstName = values[0].Capitalize();
                string lastName = values[1].Capitalize();
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7].Capitalize();
                string state = values[8].ToUpper();

               
                string newDoctorId;
                string doctorFilePath;
                Random rand = new Random();
                do
                {
                    newDoctorId = rand.Next(10000, 99999999).ToString();
                    
                    doctorFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Doctors", $"{newDoctorId}.txt");
                } while (File.Exists(doctorFilePath));

            
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string doctorData = $"{newDoctorId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

              
                File.WriteAllText(doctorFilePath, doctorData);

              
                Console.ForegroundColor = ConsoleColor.Green;
              
                Console.WriteLine($"\nDoctor {fullName} added to the system!");
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






        private void AddPatient()
        {
         
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

           
            while (true)
            {
               
              

                string whoMenu = "Add Doctor";
                String insructions = "Use UP/DOWN arrows to switch. Press ENTER to submit when done ";

                Utils.DisplayHeader(whoMenu, insructions, errorMessage);


              

                int formTop = Console.CursorTop;
                int labelWidth = 15;

                for (int i = 0; i < labels.Length; i++)
                {
                    Console.SetCursorPosition(0, formTop + i);

                 

                    string displayValue = (i == 2) ? new string('*', values[i].Length) : values[i];
                    Console.Write($"{labels[i].PadRight(labelWidth)}: {displayValue}");
                }

             
                Console.SetCursorPosition(labelWidth + 2 + values[currentField].Length, formTop + currentField);

           
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
           
                    bool allFieldsFilled = true;
                    foreach (string value in values)
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            allFieldsFilled = false; 
                            break;
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
            {
              
                string firstName = values[0].Capitalize();
                string lastName = values[1].Capitalize();
                string password = values[2];
                string email = values[3];
                string phone = values[4];
                string streetNumber = values[5];
                string street = values[6];
                string city = values[7].Capitalize();
                string state = values[8].ToUpper();

               
                string newPatientId;
                string patientFilePath;
                Random rand = new Random();
                do
                {
                    newPatientId = rand.Next(10000, 99999999).ToString();
                    patientFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Patients", $"{newPatientId}.txt");
                } while (File.Exists(patientFilePath));

             
                string fullName = $"{firstName} {lastName}";
                string fullAddress = $"{streetNumber} {street}, {city}, {state}";
                string patientData = $"{newPatientId}|{password}|{fullName}|{fullAddress}|{email}|{phone}";

                
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


        ~Admin()
        {
            Console.WriteLine("Administrator object destroyed and clearing memory");
            GC.Collect();
        }






    }
}
