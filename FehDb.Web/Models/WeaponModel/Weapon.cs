using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FehDb.Web.Models.WeaponModel
{
    public class Weapon : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUri { get; set; }
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
        [Display(Name="Type")]
        [Required]
        public WeaponType WeaponType { get; set; }
        [Required]
        public WeaponCost WeaponCost { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponEffectiveAgainst WeaponEffectiveAgainst { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WeaponStatChange WeaponStatChange { get; set; }
    }
}
