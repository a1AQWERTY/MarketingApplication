using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trading.Data.Common;


namespace TradingAPI.HandleRequest.Request.UploadMaster
{
    public class RequestUploadMaster : RequestBase, IRequest<bool>
    {
       public IFormFile file { get; set; }

        public string MasterType { get; set; }
    }
}
