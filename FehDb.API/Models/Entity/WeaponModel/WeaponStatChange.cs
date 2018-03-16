using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponStatChange : BaseEntity
    {
        [ForeignKey("Weapon")]
        public int WeaponID { get; set; }
        public int? HP { get; set; }
        public int? Might { get; set; }
        public int? Speed { get; set; }
        public int? Defense { get; set; }
        public int? Resistance { get; set; }

        // Navigation Properties
        public Weapon Weapon { get; set; }
    }
}
