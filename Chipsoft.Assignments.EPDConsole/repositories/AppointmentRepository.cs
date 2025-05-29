using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentnRepository
    {
        public AppointmentRepository(EPDDbContext context) : base(context)
        {
        }

        public new IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                        .Include(a => a.Physician)
                        .Include(a => a.Patient)
                        .ToList();
        }
    }
}