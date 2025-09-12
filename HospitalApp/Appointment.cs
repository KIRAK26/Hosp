using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Data
{


    // Single appointment record, linking a doctor and a patient.
    public class Appointment
    {
        private string doctorID, patientID, description;


        public Appointment(string doctorID, string patientID, string description)
        {
            this.doctorID = doctorID;
            this.patientID = patientID;
            this.description = description;
        }

        // public property to safaely access and modify the doctor's ID 
        public string DoctorID
        {
            get { return doctorID; }
            set { doctorID = value; }
        }
        //Same as the doctor's ID 
        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }







        ~Appointment()
        {  // Manually calling GC.Collect() 
            Console.WriteLine("Destoryed Appointment object and clearing memory");
            GC.Collect();

        }


    }
}
