using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer entity);
        void AddRange(IEnumerable<Customer> entities);
        bool Any(Expression<Func<Customer, bool>> predicate);
        IEnumerable<Customer> FindAsync(Expression<Func<Customer, bool>> predicate);
        Customer FindbyEmail(string email);
        Task<Customer> FindAsync(int id);
        Task<Customer> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetAsync(int id);
        Task<bool> ModifyStateAsync(Customer address, int id);
        void Remove(Customer entity);
        void RemoveRange(IEnumerable<Customer> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Customer>> ToListAsync();
        int HighestID();
    }
}