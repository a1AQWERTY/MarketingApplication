using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.Unit
{
    public class RequestAddUnitConversion : RequestBase, IRequest<bool>
    {
        public Guid FromUnitMasterId { get; set; }
        public Guid ToUnitMasterId { get; set; }

        public decimal ConversionValue { get; set; }
    }
}
