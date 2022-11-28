using MediatR;
using System;
using System.Text.Json.Serialization;
using Trading.Data.Common;


namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestItemUpdate : RequestBase, IRequest<bool>
    {
        /// <summary>
        /// Unique Id of Item Master
        /// </summary>
        [JsonIgnore]
        public Guid ItemMasterId { get; set; }

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
