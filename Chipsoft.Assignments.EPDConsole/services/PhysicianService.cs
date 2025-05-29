using Chipsoft.Assignments.EPDConsole.Forms;
using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    public class PhysicianService : PersonService<Physician>
    {
        private readonly IPhysicianRepository _physicianRepository;
        public PhysicianService(IPhysicianRepository physicianRepository) : base(physicianRepository)
        {
            _physicianRepository = physicianRepository;
        }
        public Physician CreatePhysician(Person person, string rizivNbr)
        {
            var physician = new Physician(
                person.FirstName,
                person.LastName,
                person.DateOfBirth,
                person.Email,
                person.PhoneNbr,
                rizivNbr
            );

            _physicianRepository.Add(physician);

            physician.EmployeeId = "EMP" + physician.Id.ToString("D5");
            _physicianRepository.Update(physician);

            return physician;
        }

        public void DeletePhysician()
        {
            Physician? physician = GetByEmployeeId();

            if (physician == null)
                return;

            DeleteEntity(physician);
        }

        public List<Physician> GetForAppointment(List<int> ids)
        {
            return GetByIds(ids);
        }

        public Physician? GetByEmployeeId()
        {
            List<Physician> physicians = _physicianRepository.GetAll().ToList();
            if (!physicians.Any())
            {
                Console.WriteLine("Geen artsen beschikbaar.\n");
                return null;
            }

            Physician? physician = null;
            while (physician == null)
            {
                string employeeId = ConsoleUtil.ReadNonEmptyString("Employee ID van Arts: ");
                physician = _physicianRepository.FindByEmployeeId(employeeId);

                if (physician == null)
                    Console.WriteLine("Arts niet gevonden. Probeer opnieuw.\n");
            }

            return physician;
        }

        public List<Physician> GetAll()
        {
            return _physicianRepository.GetAll();
        }
    }
}