using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(EPDDbContext context) : base(context)
        {
        }

        public Patient FindBySocialSecurityNbr(string rrn)
        {
            return _dbSet.FirstOrDefault(p => p.SocialSecurityNbr == rrn);
        }
    }
}