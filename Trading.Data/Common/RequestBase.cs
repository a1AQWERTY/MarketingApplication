using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Trading.Data.Common
{
    public class RequestBase
    {
        [JsonIgnore]
        public string requestUserEmail { get; set; }

        [JsonIgnore]
        public Guid requestUserId { get; set; }
    }
}
