using CarAuction.Core.Models.BaseModels;
using CarAuction.Core.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Data.Context;

namespace CarAuction.Data.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly AuctionDbContext _context;

        public WriteRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _context.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await _context.AddRangeAsync(entities);
            return true;
        }

        public async Task<bool> Remove(string id)
        {
            var data = await Table.FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));
            return Remove(data);
        }

        public bool Remove(T entity)
        {
            EntityEntry entityEntry = _context.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> entities)
        {
            _context.RemoveRange(entities);
            return true;
        }

        public Task<int> SaveAsync()
          => _context.SaveChangesAsync();

        public bool Update(T entity)
        {
            EntityEntry entityEntry = _context.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
