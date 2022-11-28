using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trading.Data.Common;

namespace TradingAPI.HandleRequest.Request.User
{
    public class RequestAddUpdateUser : RequestBase, IRequest<Guid>
    {
        /// <summary>
        /// Gets Sets User Id
        /// </summary>
        [JsonIgnore]
        public Guid UserId { get; set; }

        /// <summary>
        /// Get or sets UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets User Email
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Contact No
        /// </summary>
        public Int64 ContactNo { get; set; }

    }
}
