using MediatR;
using System.Collections.Generic;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Data.Common;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Response.Users;

namespace TradingAPI.HandleRequest.Request.User
{
    public class RequestUsers : RequestBase, IRequest<ResponseListClass<List<ResponseGetUserList>>>
    {
        /// <summary>
        /// requestFilter
        /// </summary>
        public RequestFilter requestFilter { get; set; }

    }
}
