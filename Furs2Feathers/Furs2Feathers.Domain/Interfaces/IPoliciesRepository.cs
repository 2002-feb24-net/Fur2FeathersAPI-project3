using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.DataAccess.Repositories
{
    public interface IPoliciesRepository
    {
        void Add(Policies entity);
        void AddRange(IEnumerable<Policies> entities);
        bool Any(Expression<Func<Policies, bool>> predicate);
        IEnumerable<Policies> FindAsync(Expression<Func<Policies, bool>> predicate);
        Task<Policies> FindAsync(int id);
        Task<Policies> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Policies>> GetAll();
        Task<Policies> GetAsync(int id);
        Task<bool> ModifyStateAsync(Policies policies, int id);
        void Remove(Policies entity);
        void RemoveRange(IEnumerable<Policies> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Policies>> ToListAsync();
    }
}