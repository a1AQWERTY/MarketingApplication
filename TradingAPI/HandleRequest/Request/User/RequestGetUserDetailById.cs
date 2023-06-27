using MediatR;
using TradingAPI.HandleRequest.Response.Users;

namespace TradingAPI.HandleRequest.Request.User
{
    /// <summary>
    /// RequestGetUserDetailById 
    /// </summary>
    public class RequestGetUserDetailById : IRequest<ResponseGetUserDetailById>
    {
        /// <summary>
        /// Get or Set User Id
        /// </summary>
        public int UsertId { get; set; }
    }
}
