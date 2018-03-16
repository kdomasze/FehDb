using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponEffectiveAgainstWeaponTypeResource : BaseResource
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponEffectiveAgainstResource WeaponEffectiveAgainst { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponTypeResource WeaponType { get; set; }
    }
}
