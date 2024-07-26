using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> expression, int count, int page, bool tracking = true);
        Task<bool> isExsist(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(string id, Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes);
    }
}
