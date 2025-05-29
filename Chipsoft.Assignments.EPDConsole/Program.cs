using Chipsoft.Assignments.EPDConsole.Forms;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Repositories;
using Chipsoft.Assignments.EPDConsole.Services;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole
{

    public class Program
    {
        //Don't create EF migrations, use the reset db option
        //This deletes and recreates the db, this makes sure all tables exist
        private static readonly EPDDbContext _context = new EPDDbContext();
        private static readonly PatientService _patientService = new PatientService(new PatientRepository(_context));
        private static readonly PhysicianService _physicianService = new PhysicianService(new PhysicianRepository(_context));
        private static readonly AppointmentService _appointmentService = new AppointmentService(new AppointmentRepository(_context), _physicianService, _patientService);


        private static void AddPatient()
        {
            Console.WriteLine("Patient toevoegen");

            try
            {
                Person person = PersonForm.CreateForm();

                string rrn = ConsoleUtil.ReadNonEmptyString("Rijksregisternummer (RRN): ");

                Address address = AddressForm.CreateForm();

                _patientService.CreatePatient(person, rrn, address);

                Console.WriteLine("Patient toegevoegd!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error toevoegen patient: " + ex.Message);
            }
        }

        private static void ShowAppointment()
        {
            Console.WriteLine("Afspraak bekijken - Kies een filteroptie:");
            Console.WriteLine("1. Toon alle afspraken");
            Console.WriteLine("2. Toon afspraken voor een specifieke patiënt (op basis van RRN)");
            Console.WriteLine("3. Toon afspraken voor een specifieke arts (op basis van Employee ID)");

            int choice = ConsoleUtil.ReadChoice("Maak uw keuze (1-3): ", 1, 3);

            string? filterValue = null;
            if (choice == 2)
            {
                Patient? patient = _patientService.GetBySocialSecurityNbr();

                if (patient == null)
                    return;

                filterValue = patient.SocialSecurityNbr;
            }
            else if (choice == 3)
            {
                Physician? physician = _physicianService.GetByEmployeeId();

                if (physician == null)
                    return;

                filterValue = physician.EmployeeId;
            }

            var (appointments, physicians, patients) = _appointmentService.GetFilteredAppointments(choice, filterValue);

            if (!appointments.Any())
            {
                Console.WriteLine("\nGeen afspraken gevonden voor de opgegeven criteria.\n");
            }
            else
            {
                Console.WriteLine();
                foreach (var app in appointments)
                {
                    app.Print(physicians, patients);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        private static void AddAppointment()
        {
            Console.WriteLine("Afspraak toevoegen");

            try
            {
                Physician? physician = SelectPhysician();

                if (physician == null)
                {
                    Console.WriteLine("Geen arts beschikbaar.");
                    return;
                }

                Patient? patient = _patientService.GetBySocialSecurityNbr();

                if (patient == null)
                    return;

                DateTime date = ConsoleUtil.ReadDateAndTime("Datum en tijd afspraak (dd/MM/yyyy HH:mm): ");

                string reason = ConsoleUtil.ReadNonEmptyString("Reden afspraak: ");

                Console.Write("Opmerkingen (optioneel): ");
                string? notes = Console.ReadLine()?.Trim();

                _appointmentService.AddAppointment(physician, patient, date, reason, notes);

                Console.WriteLine("Afspraak succesvol toegevoegd!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error toevoegen afspraak: " + ex.Message);
            }
        }

        private static Physician? SelectPhysician()
        {
            List<Physician> physicians = _physicianService.GetAll();
            if (!physicians.Any())
                return null;

            Console.WriteLine("Beschikbare artsen:");

            for (int i = 0; i < physicians.Count; i++)
                Console.WriteLine($"{i + 1}. {physicians[i].FirstName} {physicians[i].LastName}");

            return physicians[ConsoleUtil.ReadChoice("Kies een arts (nummer): ", 1, physicians.Count) - 1];
        }

        private static void DeletePhysician()
        {
            Console.WriteLine("Arts verwijderen op basis van Employee ID");

            _physicianService.DeletePhysician();
        }

        private static void AddPhysician()
        {
            Console.WriteLine("Arts toevoegen");

            try
            {
                Person person = PersonForm.CreateForm();
                string rizivNbr = ConsoleUtil.ReadNonEmptyString("RIZIV nummer: ");
                Physician physician = _physicianService.CreatePhysician(person, rizivNbr);

                Console.WriteLine($"Arts toegevoegd! Employee ID: {physician.EmployeeId}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error toevoegen arts: " + ex.Message);
            }
        }

        private static void DeletePatient()
        {
            Console.WriteLine("Patiënt verwijderen op basis van Rijksregisternummer (RRN)");

            _patientService.DeletePatient();
        }

        #region FreeCodeForAssignment
        static void Main(string[] args)
        {
            while (ShowMenu())
            {
                //Continue
            }
        }

        public static bool ShowMenu()
        {
            //Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddPatient();
                        return true;
                    case 2:
                        DeletePatient();
                        return true;
                    case 3:
                        AddPhysician();
                        return true;
                    case 4:
                        DeletePhysician();
                        return true;
                    case 5:
                        AddAppointment();
                        return true;
                    case 6:
                        ShowAppointment();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        EPDDbContext dbContext = new EPDDbContext();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }

        #endregion
    }
}