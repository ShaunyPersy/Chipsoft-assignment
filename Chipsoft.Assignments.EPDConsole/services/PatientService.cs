using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    public class PatientService : PersonService<Patient>
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) : base(patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public void CreatePatient(Person person, string rrn, Address address)
        {
            Patient patient = new Patient
            (
                person.FirstName,
                person.LastName,
                person.DateOfBirth,
                person.Email,
                person.PhoneNbr,
                rrn,
                address
            );

            _patientRepository.Add(patient);
        }
        public Patient? GetBySocialSecurityNbr()
        {
            List<Patient> patients = _patientRepository.GetAll().ToList();
            if (!patients.Any())
            {
                Console.WriteLine("Geen patiënten beschikbaar.\n");
                return null;
            }

            Patient? patient = null;
            while (patient == null)
            {
                string rrn = ConsoleUtil.ReadNonEmptyString("Rijksregisternummer van patiënt: ");
                patient = _patientRepository.FindBySocialSecurityNbr(rrn);

                if (patient == null)
                    Console.WriteLine("Patiënt niet gevonden. Probeer opnieuw.\n");
            }

            return patient;
        }

        public void DeletePatient()
        {
            Patient? patient = GetBySocialSecurityNbr();

            if (patient == null)
                return;

            DeleteEntity(patient);
        }

        public List<Patient> GetForAppointment(List<int> ids)
        {
            return GetByIds(ids);
        }
    }
}