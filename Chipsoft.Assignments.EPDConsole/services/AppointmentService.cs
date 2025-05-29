using Chipsoft.Assignments.EPDConsole.Enums;
using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentnRepository _appointmentRepository;
        private readonly PhysicianService _physicianService;
        private readonly PatientService _patientService;

        public AppointmentService(IAppointmentnRepository appointmentRepository,
                                PhysicianService physicianService,
                                PatientService patientService)
        {
            _appointmentRepository = appointmentRepository;
            _physicianService = physicianService;
            _patientService = patientService;
        }

        public (List<Appointment> Appointments, List<Physician> Physicians, List<Patient> Patients)
            GetFilteredAppointments(int filterOption, string? filterValue = null)
        {
            IEnumerable<Appointment> appointments = _appointmentRepository.GetAll();

            foreach (Appointment appointment in appointments)
            {
                Console.WriteLine($"Appointment ID: {appointment.Id}, Date: {appointment.Date}, Physician ID: {appointment.Physician?.Id}, Patient ID: {appointment.Patient?.Id}");
            }

            switch (filterOption)
            {
                case 2:
                    if (!string.IsNullOrEmpty(filterValue))
                        appointments = appointments.Where(a => a.Patient != null && a.Patient.SocialSecurityNbr == filterValue);

                    break;
                case 3:
                    if (!string.IsNullOrEmpty(filterValue))
                        appointments = appointments.Where(a => a.Physician != null && a.Physician.EmployeeId == filterValue);

                    break;
                case 1:
                default:
                    break;
            }

            List<int> physicianIds = appointments
                .Select(a => a.Physician?.Id)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            List<int> patientIds = appointments
                .Select(a => a.Patient?.Id)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            List<Physician> physicians = _physicianService.GetForAppointment(physicianIds);
            List<Patient> patients = _patientService.GetForAppointment(patientIds);

            return (appointments.ToList(), physicians, patients);
        }

        public void AddAppointment(Physician physician, Patient patient, DateTime date, String reason, String? notes)
        {
            var appointment = new Appointment
            (
                physician,
                patient,
                date,
                reason,
                notes,
                AppointmentStatus.Scheduled
            );

            _appointmentRepository.Add(appointment);
        }
    }
}