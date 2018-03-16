using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.API.Models.Resource
{
    public class MovementTypeResource : BaseResource
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Movement Movement { get; set; }
    }
}
