using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.API.Models.Entity
{
    public class WeaponType : BaseEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Arm Arm { get; set; }
    }
}
