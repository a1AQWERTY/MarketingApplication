using System;

namespace TradingAPI.HandleRequest.Response.Users
{
    public class ResponseGetUserDetailById
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Int64 ContactNo { get; set; }
    }
}
