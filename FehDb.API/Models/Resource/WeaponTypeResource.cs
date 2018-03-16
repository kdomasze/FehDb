using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource
{
    public class WeaponTypeResource : BaseResource
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Arm Arm { get; set; }
    }
}
