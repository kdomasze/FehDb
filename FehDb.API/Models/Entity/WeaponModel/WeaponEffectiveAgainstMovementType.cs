using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponEffectiveAgainstMovementType : BaseEntity
    {
        [ForeignKey("WeaponEffectiveAgainst")]
        public int WeaponEffectiveAgainstID { get; set; }
        [ForeignKey("MovementType")]
        public int MovementTypeID { get; set; }

        public virtual WeaponEffectiveAgainst WeaponEffectiveAgainst { get; set; }
        public virtual MovementType MovementType { get; set; }
    }
}
