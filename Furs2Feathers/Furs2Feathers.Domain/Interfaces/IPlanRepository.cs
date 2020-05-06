using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IPlanRepository
    {
        void Add(Plan entity);
        void AddRange(IEnumerable<Plan> entities);
        bool Any(Expression<Func<Plan, bool>> predicate);
        IEnumerable<Plan> FindAsync(Expression<Func<Plan, bool>> predicate);
        Task<Plan> FindAsync(int id);
        Task<Plan> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Plan>> GetAll();
        Task<Plan> GetAsync(int id);
        Task<bool> ModifyStateAsync(Plan plan, int id);
        void Remove(Plan entity);
        void RemoveRange(IEnumerable<Plan> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Plan>> ToListAsync();
    }
}