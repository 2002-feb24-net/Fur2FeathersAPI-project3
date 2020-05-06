using Furs2Feathers.DataAccess.Models;
using Furs2Feathers.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furs2Feathers.DataAccess.Repositories
{
    /// <summary>
    /// This class wraps calls to EF Core's methods to ensure separation of concerns. A domain class making calls to this repository does not have to worry about EntityFramework Core being used. This class can later be modified to make calls to some other version of EF Core or another ORM (Object Relational Mapper). This class wraps CRUD calls to a database using EF Core.
    /// </summary>
    public class PlanRepository : IPlanRepository
    {
        private readonly f2fdbContext _context;

        /// <summary>
        /// This class uses depedency injection in order to avoid sync errors with multiple repositories being called at the same time
        /// </summary>
        /// <param name="context"></param>
        public PlanRepository(f2fdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core Add. The call is made with a mapped entity (DataAccess.Models.Plan) instead of the domain model passed as a parameter. The DataAccess.Model is used to communicate with EF Core.
        /// 
        /// EF Core Add:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        public void Add(Domain.Models.Plan entity)
        {
            var mappedEntity = Mapper.MapPlan(entity);
            _context.Set<Plan>().Add(mappedEntity);
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core Add. The list is added one by one through an iteration over an IEnumerable. The call is made with a mapped entity (DataAccess.Models.Plan) instead of the domain model passed as a parameter. The DataAccess.Model is used to communicate with EF Core.
        /// 
        /// EF Core Add:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        public void AddRange(IEnumerable<Domain.Models.Plan> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }
        /// <summary>
        /// Wraps a call to EntityFramework Core FindAsync and returns a mapped entity instead of a DataAccess.Model used to communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Domain.Models.Plan</returns>
        public async Task<Domain.Models.Plan> FindAsync(int id)
        {
            var data = await _context.Set<Plan>().FindAsync(id);

            return Mapper.MapPlan(data);
        }

        /// <summary>
        /// Wraps a call to EF Core FindAsync using a predicate (for example: a => a). This method returns a mapped entity instead of a DataAccess.Model used to communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Domain.Models.Plan</returns>
        public IEnumerable<Domain.Models.Plan> FindAsync(Expression<Func<Domain.Models.Plan, bool>> predicate)
        {
            return _context.Set<Domain.Models.Plan>().Where(predicate);
        }

        /// <summary>
        /// Wraps a call to EF Core FindAsync and returns a mapped entity instead of a DataAccess.Model used to communicate with EF Core. This method has no tracking because a bug with PostgreSQL does not allow deletions that have tracking associated with them.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Domain.Models.Plan</returns>
        public async Task<Domain.Models.Plan> FindAsyncAsNoTracking(int id)
        {
            var data = await _context.Set<Plan>().AsNoTracking().Where(a => a.PlanId == id).FirstOrDefaultAsync();

            return Mapper.MapPlan(data);
        }

        /// <summary>
        /// Wraps a call to EF Core FindAsync and returns a mapped entity instead of a DataAccess.Model used to communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Domain.Models.Plan</returns>
        public async Task<Domain.Models.Plan> GetAsync(int id)
        {
            var entity = await _context.Set<Plan>().FindAsync(id); //return single object of class

            return Mapper.MapPlan(entity);

        }

        /// <summary>
        /// Wraps a call to EntityFramework Core ToListAsync (generating a list of Plans) and returns a mapped entity list instead of a DataAccess.Model used to communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <returns>Domain.Models.Plan</returns>
        public async Task<IEnumerable<Domain.Models.Plan>> GetAll()
        {
            var entities = await _context.Set<Plan>().ToListAsync();
            var mappedEntities = new List<Domain.Models.Plan>();
            foreach (var entity in entities)
            {
                mappedEntities.Add(Mapper.MapPlan(entity));
            }
            return mappedEntities;

        }

        /// <summary>
        /// Wraps a call to EntityFramework Core ToListAsync (generating a list of Plans) and returns a mapped entity list instead of a DataAccess.Model used to communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <returns>Domain.Models.Plan</returns>
        public async Task<IEnumerable<Domain.Models.Plan>> ToListAsync()
        {
            return await GetAll();
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core Remove. This method maps a domain model into a DataAccess model that can communicate with EF Core.
        /// 
        /// EF Core Remove:
        /// Begins tracking the given entity in the Deleted state such that it will be removed from the database when SaveChanges() is called.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        public void Remove(Domain.Models.Plan entity)
        {
            var mappedEntity = Mapper.MapPlan(entity);
            _context.Set<Plan>().Remove(mappedEntity);
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core Remove. This method maps a domain model list into a DataAccess model objects that can communicate with EF Core.
        /// 
        /// EF Core Remove:
        /// Begins tracking the given entity in the Deleted state such that it will be removed from the database when SaveChanges() is called.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        public void RemoveRange(IEnumerable<Domain.Models.Plan> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core FindAsync. If nothing is found then this method returns false. However if some object is found matching the condition in the predicate then true is returned by this method. This method maps a domain model into a DataAccess model that can communicate with EF Core.
        /// 
        /// EF Core FindAsync:
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        public bool Any(Expression<Func<Domain.Models.Plan, bool>> predicate)
        {
            var entity = FindAsync(predicate);
            if (entity == null)
            {
                return false;
            }
            else
                return true;    // there is something
        }

        /// <summary>
        /// This method tries to update an entity in the database through setting EntityFramework Core's Entry property to EntityState.Modified. If the update fails an exception is thrown. If the update succeeds then the plan parameter object passed in is saved to the database.
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ModifyStateAsync(Domain.Models.Plan plan, int id)
        {
            var mappedPlan = Mapper.MapPlan(plan);
            /*_context.Entry(plan).State = EntityState.Modified;*/
            _context.Entry(mappedPlan).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
                {
                    return false;
                    // plan not found
                }
                else
                {
                    throw;
                }
            }
            return true;
            // it worked, so return true
        }

        /// <summary>
        /// Wraps a call to EntityFramework Core Any. If nothing is found then this method returns false. However if some object is found matching the condition (it matches the id of the parameter) then true is returned by this method. 
        /// 
        /// EF Core Any:
        /// Asynchronously determines whether a sequence contains any elements matching the predicate conditions.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Domain.Models.Plan</returns>
        private bool PlanExists(int id)
        {
            return Any(e => e.PlanId == id);
        }

        /// <summary>
        /// This method wraps a call to EF Core SaveChanges
        /// EF Core SaveChanges
        ///  Saves all changes made in this context to the database.
        /// This method will automatically call EF Core's DetectChanges() to discover any changes to entity instances before saving to the underlying database.This can be disabled via AutoDetectChangesEnabled.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Implementation (wraps SaveChangesAsync from Entity Framework in case their methods change. Centralized place here to update their saveasync for example or any other implementation)
        /// 
        /// EF Core SaveChangesAsync:
        /// This method will automatically call EF Core's DetectChanges() to discover any changes to entity instances before saving to the underlying database. This can be disabled via AutoDetectChangesEnabled. 
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }

}