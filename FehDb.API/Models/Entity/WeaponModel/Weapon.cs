using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class Weapon : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Might { get; set; }
        [Required]
        public int Range { get; set; }
        public string Effect { get; set; }
        [Required]
        public int WeaponTypeID { get; set; }
        [Required]
        public bool Exclusive { get; set; }
        [Required]
        public bool Refined { get; set; }

        //Navigation Properties
        public WeaponType WeaponType { get; set; }
        [Required]
        public WeaponCost WeaponCost { get; set; }
        public WeaponEffectiveAgainst WeaponEffectiveAgainst { get; set; }
        public WeaponStatChange WeaponStatChange { get; set; }
    }
}
