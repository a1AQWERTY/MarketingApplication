using MediatR;
using System;
using System.Text.Json.Serialization;
using Trading.Data.Common;
namespace TradingAPI.HandleRequest.Request.ItemMaster
{
    public class RequestUpdateItemInventory : RequestBase, IRequest<bool>
    {
        /// <summary>
        /// Unique Id of Item Master
        /// </summary>
        [JsonIgnore]
        public Guid ItemInventoryId { get; set; }

        

        /// <summary>
        /// Unit Master Id
        /// </summary>
        public Guid UnitMasterId { get; set; }

        /// <summary>
        /// Item Rate
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Item Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Item Batch No
        /// </summary>
        public string BatchNo { get; set; }
    }
}
