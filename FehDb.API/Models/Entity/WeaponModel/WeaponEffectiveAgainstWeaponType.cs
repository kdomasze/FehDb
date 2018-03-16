using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponEffectiveAgainstWeaponType : BaseEntity
    {
        [ForeignKey("WeaponEffectiveAgainst")]
        public int WeaponEffectiveAgainstID { get; set; }
        [ForeignKey("WeaponType")]
        public int WeaponTypeID { get; set; }

        public virtual WeaponEffectiveAgainst WeaponEffectiveAgainst { get; set; }
        public virtual WeaponType WeaponType { get; set; }
    }
}
