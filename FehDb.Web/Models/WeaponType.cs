using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models
{
    public class WeaponType : BaseEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Arm Arm { get; set; }

        public override string ToString()
        {
            return Color.ToString() + " " + Arm.ToString();
        }
    }
}
