using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.Web.Models
{
    public class MovementType : BaseEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Movement Movement { get; set; }
    }
}
