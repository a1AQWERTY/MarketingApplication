using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.Infrastructure.Pagination
{
    public static class GetPagedResult
    {

        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
                                         int page, int pageSize, out int Count) where T : class
        {
            var result = new PagedResult<T>();
            page = page <= 0 ? 1 : page;
            Count = query.Count();

            var skip = (page - 1) * pageSize;
            if (pageSize <= 0)
            {
                result.Results = query.ToList();

            }
            else
            {
                result.Results = query.Skip(skip).Take(pageSize).ToList();

            }



            return result;
        }
    }
}
