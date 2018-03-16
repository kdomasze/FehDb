using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class Weapon : BaseEntity
    {
        public string Name { get; set; }
        public int Might { get; set; }
        public int Range { get; set; }
        public string Effect { get; set; }
        public int WeaponTypeID { get; set; }
        //public int WeaponCostID { get; set; }
        //public int WeaponEffectiveAgainstID { get; set; }
        public bool Exclusive { get; set; }
        public bool Refined { get; set; }
        //public int WeaponStatChangeID { get; set; }

        //Navigation Properties
        public WeaponType WeaponType { get; set; }
        public WeaponCost WeaponCost { get; set; }
        public WeaponEffectiveAgainst WeaponEffectiveAgainst { get; set; }
        public WeaponStatChange WeaponStatChange { get; set; }
    }
}
