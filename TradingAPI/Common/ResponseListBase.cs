using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Common
{
    public class ResponseListClass<T>
    {
        /// <summary>
        /// Set Count from List
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Set List Data
        /// </summary>
        public T Data { get; set; }
    }
}
