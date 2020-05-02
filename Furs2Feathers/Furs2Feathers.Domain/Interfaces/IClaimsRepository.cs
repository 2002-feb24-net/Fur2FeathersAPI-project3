using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IClaimsRepository
    {
        void Add(Claims entity);
        void AddRange(IEnumerable<Claims> entities);
        bool Any(Expression<Func<Claims, bool>> predicate);
        IEnumerable<Claims> FindAsync(Expression<Func<Claims, bool>> predicate);
        Task<Claims> FindAsync(int id);
        Task<Claims> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Claims>> GetAll();
        Task<Claims> GetAsync(int id);
        Task<bool> ModifyStateAsync(Claims claim, int id);
        void Remove(Claims entity);
        void RemoveRange(IEnumerable<Claims> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Claims>> ToListAsync();
    }
}