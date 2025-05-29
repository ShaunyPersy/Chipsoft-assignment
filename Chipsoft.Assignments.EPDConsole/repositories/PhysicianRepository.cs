using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    public class PhysicianRepository : Repository<Physician>, IPhysicianRepository
    {
        public PhysicianRepository(EPDDbContext context) : base(context)
        {
        }

        public Physician FindByEmployeeId(string employeeId)
        {
            return _dbSet.FirstOrDefault(p => p.EmployeeId == employeeId);
        }
    }
}