using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IPetRepository
    {
        
        void Add(Pet entity);
        void AddRange(IEnumerable<Pet> entities);
        bool Any(Expression<Func<Pet, bool>> predicate);
        IEnumerable<Pet> FindAsync(Expression<Func<Pet, bool>> predicate);
        Task<Pet> FindAsync(int id);
        Task<Pet> FindAsyncAsNoTracking(int id);
        IEnumerable<Pet> FindByCustId(int custId);
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet> GetAsync(int id);
        Task<bool> ModifyStateAsync(Pet pet, int id);
        void Remove(Pet entity);
        void RemoveRange(IEnumerable<Pet> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Pet>> ToListAsync();
    }
}