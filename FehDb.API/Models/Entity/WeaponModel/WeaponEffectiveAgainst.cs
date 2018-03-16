using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponEffectiveAgainst : BaseEntity
    {
        [ForeignKey("Weapon")]
        public int WeaponID { get; set; }
        public IList<WeaponEffectiveAgainstWeaponType> WeaponTypes { get; set; }
        public IList<WeaponEffectiveAgainstMovementType> MovementTypes { get; set; }

        //Navigaton Properties
        public Weapon Weapon { get; set; }
    }
}
