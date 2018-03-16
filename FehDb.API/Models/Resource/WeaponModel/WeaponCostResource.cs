using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponCostResource : BaseResource
    {
        [Required]
        public int SpCost { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Medals { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Stones { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Dew { get; set; }
    }
}
