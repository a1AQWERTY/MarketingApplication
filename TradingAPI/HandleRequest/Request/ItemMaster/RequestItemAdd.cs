using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trading.Data.Common;
using TradingAPI.HandleRequest.Response;

namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestItemAdd : RequestBase, IRequest<Guid>
    {
        /// <summary>
        /// Code of Item
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Name of the Item
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Description of Item
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Unit Master Id
        /// </summary>
        public Guid UnitMasterId { get; set; }

        
    }
}
