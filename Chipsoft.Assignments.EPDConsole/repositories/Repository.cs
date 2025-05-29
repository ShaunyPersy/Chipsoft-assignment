using Chipsoft.Assignments.EPDConsole.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDConsole.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EPDDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(EPDDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
