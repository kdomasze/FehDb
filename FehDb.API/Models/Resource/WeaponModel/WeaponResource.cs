using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponResource : BaseResource
    {
        public string Name { get; set; }
        public int Might { get; set; }
        public int Range { get; set; }
        public string Effect { get; set; }
        public bool Exclusive { get; set; }
        public bool Refined { get; set; }

        //Navigation Properties
        public WeaponTypeResource WeaponType { get; set; }
        public WeaponCostResource WeaponCost { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponEffectiveAgainstResource WeaponEffectiveAgainst { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponStatChangeResource WeaponStatChange { get; set; }
    }
}
