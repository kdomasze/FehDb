using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.Web.Models.WeaponModel
{
    public class WeaponStatChange : BaseEntity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? HP { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Might { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Speed { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Defense { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Resistance { get; set; }
    }
}
