using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.HandleRequest.Response.Item;
namespace TradingAPI.HandleRequest.Request.User
{
    public class RequestVerifyTFA : IRequest<ResponseUserDetail>
    {
        public string UserEmail { get; set; }

        public string TFACode { get; set; }

       
    }
}
