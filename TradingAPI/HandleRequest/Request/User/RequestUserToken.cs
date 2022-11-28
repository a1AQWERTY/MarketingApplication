using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Request.User
{
    public class RequestUserToken : IRequest<ResponseUserDetail>
    {
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
