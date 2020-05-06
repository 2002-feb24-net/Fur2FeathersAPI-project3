using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.DataAccess.Repositories
{
    public interface IPlanProLabelsRepository
    {
        void Add(PlanProLabels entity);
        void AddRange(IEnumerable<PlanProLabels> entities);
        bool Any(Expression<Func<PlanProLabels, bool>> predicate);
        IEnumerable<PlanProLabels> FindAsync(Expression<Func<PlanProLabels, bool>> predicate);
        Task<PlanProLabels> FindAsync(int id);
        Task<PlanProLabels> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<PlanProLabels>> GetAll();
        Task<PlanProLabels> GetAsync(int id);
        Task<bool> ModifyStateAsync(PlanProLabels planProLabels, int id);
        void Remove(PlanProLabels entity);
        void RemoveRange(IEnumerable<PlanProLabels> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<PlanProLabels>> ToListAsync();
    }
}