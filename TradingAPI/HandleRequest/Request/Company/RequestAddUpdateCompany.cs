using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.Company
{
    public class RequestAddUpdateCompany : RequestBase, IRequest<Guid>
    {
        /// <summary>
        /// Gets or sets Company Master Id
        /// </summary>
        /// <remarks>
        /// Pass company Master Id only when need to update data.. or else pass nothing or Guid.Empty
        /// </remarks>
        [JsonIgnore]
        public Guid CompanyMasterId { get; set; }

        /// <summary>
        /// Gets or sets Company Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Comapny Name
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets contact no
        /// </summary>
        public Int64 ContactNo { get; set; }

        /// <summary>
        /// Gets or sets contact Person
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gtets or set Company Email Id
        /// </summary>
        public string EmailId { get; set; }

    }
}
