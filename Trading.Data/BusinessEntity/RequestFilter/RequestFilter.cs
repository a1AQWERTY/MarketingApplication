using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.BusinessEntity.RequestFilter
{
    public class RequestFilter
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public string Search { get; set; }
    }
}
