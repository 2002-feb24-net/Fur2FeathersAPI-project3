using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IAddressRepository
    {
       /* EntityState State { get; set; }*/

        void Add(Domain.Models.Address entity);
        void AddRange(IEnumerable<Domain.Models.Address> entities);
        bool Any(Expression<Func<Domain.Models.Address, bool>> predicate);
        IEnumerable<Domain.Models.Address> FindAsync(Expression<Func<Domain.Models.Address, bool>> predicate);
        Task<Domain.Models.Address> FindAsync(int id);
        Task<IEnumerable<Domain.Models.Address>> GetAll();
        Task<Domain.Models.Address> GetAsync(int id);
        void Remove(Domain.Models.Address entity);
        void RemoveRange(IEnumerable<Domain.Models.Address> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Domain.Models.Address>> ToListAsync();
    }
}