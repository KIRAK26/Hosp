using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Data
{
    public class Appointment
    {
        private string doctorID, patientID, description;


        public Appointment(string doctorID, string patientID, string description)
        {
            this.doctorID = doctorID;
            this.patientID = patientID;
            this.description = description;
        }


        public string DoctorID
        {
            get { return doctorID; }
            set { doctorID = value; }
        }

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
        {
            Console.WriteLine("Destoryed Appointment object and clearing memory");
            GC.Collect();

        }


    }
}
