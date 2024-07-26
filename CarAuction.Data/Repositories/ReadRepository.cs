using CarAuction.Core.Models.BaseModels;
using CarAuction.Core.Repositories;
using CarAuction.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AuctionDbContext _context;

        public ReadRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> expression, int count, int page, bool tracking = true)
        {
            var query = Table.Where(expression);

            if (count != 0 && page != 0)
            {
                query = query.Skip((page - 1) * count).Take(count);
            }

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<T> GetByIdAsync(string id, Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes)
        {
            var query = Table.Where(expression);
            if (!tracking)
                query = query.AsNoTracking();

            foreach (string include in includes)
                query = query.Include(include);

            Guid.TryParse(id, out var guid);
            return await query.FirstOrDefaultAsync(e => e.Id == guid);
        }

        public async Task<bool> isExsist(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression).Count() > 0;
        }
    }
}
