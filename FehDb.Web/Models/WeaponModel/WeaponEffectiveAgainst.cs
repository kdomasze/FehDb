using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.Web.Models.WeaponModel
{
    public class WeaponEffectiveAgainst : BaseEntity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<WeaponEffectiveAgainstWeaponType> WeaponTypes { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<WeaponEffectiveAgainstMovementType> MovementTypes { get; set; }
    }
}
