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

    public class AddressRepository : IAddressRepository
    {
        private readonly f2fdbContext _context;

        public AddressRepository(f2fdbContext context)
        {
            _context = context;
        }

/*        public EntityState State { get; set; }
*/        public void Add(Domain.Models.Address entity)
        {
            var mappedEntity = Mapper.MapAddress(entity);
            _context.Set<Address>().Add(mappedEntity);
        }

        public void AddRange(IEnumerable<Domain.Models.Address> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public async Task<Domain.Models.Address> FindAsync(int id)
        {
            var data = await _context.Set<Address>().FindAsync(id);

            return Mapper.MapAddress(data);
        }

        public IEnumerable<Domain.Models.Address> FindAsync(Expression<Func<Domain.Models.Address, bool>> predicate)
        {
            return _context.Set<Domain.Models.Address>().Where(predicate);
        }

        public async Task<Domain.Models.Address> FindAsyncAsNoTracking(int id)
        {
            var data = await _context.Set<Address>().AsNoTracking().Where(a => a.AddressId == id).FirstOrDefaultAsync();

            return Mapper.MapAddress(data);
        }

        public async Task<Domain.Models.Address> GetAsync(int id)
        {
            var entity = await _context.Set<Address>().FindAsync(id); //return single object of class

            return Mapper.MapAddress(entity);

        }

        public async Task<IEnumerable<Domain.Models.Address>> GetAll()
        {
            var entities = await _context.Set<Address>().ToListAsync();
            var mappedEntities = new List<Domain.Models.Address>();
            foreach (var entity in entities)
            {
                mappedEntities.Add(Mapper.MapAddress(entity));
            }
            return mappedEntities;

        }

       /* public object Entry(Models.Address address)
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// same as get all. Returns a list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Domain.Models.Address>> ToListAsync()
        {
            return await GetAll();
        }

        public void Remove(Domain.Models.Address entity)
        {
            var mappedEntity = Mapper.MapAddress(entity);
            _context.Set<Address>().Remove(mappedEntity);
        }

        public void RemoveRange(IEnumerable<Domain.Models.Address> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public bool Any(Expression<Func<Domain.Models.Address, bool>> predicate)
        {
            var entity = FindAsync(predicate);
            if (entity == null)
            {
                return false;
            }
            else
                return true;    // there is something
        }

        // need to implement a put method but it requires indepth knowledge of how entity state works

        public async Task<bool> ModifyStateAsync(Domain.Models.Address address, int id)
        {
            var mappedAddress = Mapper.MapAddress(address);
            /*_context.Entry(address).State = EntityState.Modified;*/
            _context.Entry(mappedAddress).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return false;
                    // address not found
                }
                else
                {
                    throw;
                }
            }
            return true;
            // it worked, so return true
        }

        private bool AddressExists(int id)
        {
            return Any(e => e.AddressId == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Implementation (wraps SaveChangesAsync from Entity Framework in case their methods change. Centralized place here to update their saveasync for example)
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }

}