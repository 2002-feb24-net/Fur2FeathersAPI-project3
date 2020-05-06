using Furs2Feathers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.Domain.Interfaces
{
    public interface IPlanReviewsRepository
    {
        void Add(PlanReviews entity);
        void AddRange(IEnumerable<PlanReviews> entities);
        bool Any(Expression<Func<PlanReviews, bool>> predicate);
        IEnumerable<PlanReviews> FindAsync(Expression<Func<PlanReviews, bool>> predicate);
        Task<PlanReviews> FindAsync(int id);
        Task<PlanReviews> FindAsyncAsNoTracking(int id);
        Task<IEnumerable<PlanReviews>> GetAll();
        Task<PlanReviews> GetAsync(int id);
        Task<bool> ModifyStateAsync(PlanReviews planReviews, int id);
        void Remove(PlanReviews entity);
        void RemoveRange(IEnumerable<PlanReviews> entities);
        void Save();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<PlanReviews>> ToListAsync();
    }
}