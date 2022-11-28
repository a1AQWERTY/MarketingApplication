using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.HandleRequest.Response.Item
{
    public class ResponseUserDetail
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public string ContactNo { get; set; }

        public string Token { get; set; }

        public string ManualEntryKey { get; set; }

        public string QrCodeSetupImageUrl { get; set; }

        public bool IsTFARequired { get; set; }

    }
}
