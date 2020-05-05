using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee entity);
        void AddRange(IEnumerable<Employee> entities);
        bool Any(Expression<Func<Employee, bool>> predicate);
        IEnumerable<Employee> FindAsync(Expression<Func<Employee, bool>> predicate);
        Task<Employee> FindAsync(int id);
        Task<Employee> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetAsync(int id);
        Task<bool> ModifyStateAsync(Employee address, int id);
        void Remove(Employee entity);
        void RemoveRange(IEnumerable<Employee> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Employee>> ToListAsync();
    }
}