using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponCost : BaseEntity
    {
        [ForeignKey("Weapon")]
        public int WeaponID { get; set; }
        public int SpCost { get; set; }
        public int? Medals { get; set; }
        public int? Stones { get; set; }
        public int? Dew { get; set; }

        // Navigation Properties
        public Weapon Weapon { get; set; }
    }
}
