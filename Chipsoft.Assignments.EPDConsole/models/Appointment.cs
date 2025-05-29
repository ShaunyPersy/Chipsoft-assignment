using Chipsoft.Assignments.EPDConsole.Enums;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Appointment : BaseEntity
    {
        public Physician Physician { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }

        public Appointment() { }

        public Appointment(Physician physician, Patient patient, DateTime date, string reason, string notes = null, AppointmentStatus status = AppointmentStatus.Scheduled)
        {
            Physician = physician;
            Patient = patient;
            Date = date;
            Reason = reason;
            Notes = notes;
            Status = status;
        }

        public void Print(List<Physician> physicians, List<Patient> patients)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Datum & Tijd: {Date:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Reden: {Reason}");
            Console.WriteLine($"Notities: {Notes}");

            Console.WriteLine("\n--- Arts ---");
            if (Physician != null)
            {
                var physician = physicians.FirstOrDefault(p => p.Id == Physician.Id);
                if (physician != null)
                    physician.Print();
                else
                    Console.WriteLine("Arts niet gevonden.");
            }
            else
            {
                Console.WriteLine("Arts niet gevonden.");
            }

            Console.WriteLine("\n--- Patiënt ---");
            if (Patient != null)
            {
                var patient = patients.FirstOrDefault(p => p.Id == Patient.Id);
                if (patient != null)
                    patient.Print();
                else
                    Console.WriteLine("Patiënt niet gevonden.");
            }
            else
            {
                Console.WriteLine("Patiënt niet gevonden.");
            }

            Console.WriteLine("--------------------------------------------------\n");
        }
    }
}