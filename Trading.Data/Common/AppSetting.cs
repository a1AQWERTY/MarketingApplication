using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.Common
{
    public class AppSetting
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }

        public string Issuer { get; set; }
    }
}
