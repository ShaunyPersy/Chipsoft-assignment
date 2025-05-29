using Chipsoft.Assignments.EPDConsole.Interfaces;
using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Services
{
    public class PersonService<T> where T : Person
    {
        protected readonly IRepository<T> _repository;

        public PersonService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void DeleteEntity(T entity)
        {
            Console.WriteLine($"Persoon gevonden: {entity.FirstName} {entity.LastName}");

            if (ConsoleUtil.Confirm())
            {
                _repository.Delete(entity.Id);
                Console.WriteLine("Persoon succesvol verwijderd.");
            }
        }

        public List<T> GetByIds(List<int> ids)
        {
            return _repository.GetAll().Where(p => ids.Contains(p.Id)).ToList();
        }
    }
}