using Chipsoft.Assignments.EPDConsole.Models;

namespace Chipsoft.Assignments.EPDConsole.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient FindBySocialSecurityNbr(string rrn);
    }
}
