using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponEffectiveAgainstResource : BaseResource
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<WeaponEffectiveAgainstWeaponTypeResource> WeaponTypes { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<WeaponEffectiveAgainstMovementTypeResource> MovementTypes { get; set; }
    }
}
