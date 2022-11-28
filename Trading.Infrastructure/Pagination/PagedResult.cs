using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Infrastructure.Pagination
{
    public class PagedResult<T> where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
