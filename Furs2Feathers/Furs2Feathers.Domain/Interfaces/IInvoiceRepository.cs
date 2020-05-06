using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        void Add(Invoice entity);
        void AddRange(IEnumerable<Invoice> entities);
        bool Any(Expression<Func<Invoice, bool>> predicate);
        IEnumerable<Invoice> FindAsync(Expression<Func<Invoice, bool>> predicate);
        Task<Invoice> FindAsync(int id);
        Task<Invoice> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<Invoice>> GetAll();
        Task<Invoice> GetAsync(int id);
        Task<bool> ModifyStateAsync(Invoice invoice, int id);
        void Remove(Invoice entity);
        void RemoveRange(IEnumerable<Invoice> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<Invoice>> ToListAsync();
    }
}