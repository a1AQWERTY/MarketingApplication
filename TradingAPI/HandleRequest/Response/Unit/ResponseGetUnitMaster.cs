using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Unit
{
    public class ResponseGetUnitMaster
    {
        /// <summary>
        /// Gets or Sets Unit Master Id
        /// </summary>
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Gets or Sets Unit Code
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// Gets Or Sets Unit Name
        /// </summary>
        public string UnitName { get; set; }

    }

   
}
