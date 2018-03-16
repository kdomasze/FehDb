using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponResource : BaseResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Might { get; set; }
        [Required]
        public int Range { get; set; }
        public string Effect { get; set; }
        [Required]
        public bool Exclusive { get; set; }
        [Required]
        public bool Refined { get; set; }

        //Navigation Properties
        [Required]
        public WeaponTypeResource WeaponType { get; set; }
        [Required]
        public WeaponCostResource WeaponCost { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponEffectiveAgainstResource WeaponEffectiveAgainst { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponStatChangeResource WeaponStatChange { get; set; }
    }
}
