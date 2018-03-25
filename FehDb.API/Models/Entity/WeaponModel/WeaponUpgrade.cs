using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Entity.WeaponModel
{
    public class WeaponUpgrade : BaseEntity
    {
        [Required]
        public int PreviousWeaponID { get; set; }
        [Required]
        public int NextWeaponID { get; set; }

        // Navigation properties
        public Weapon PreviousWeapon { get; set; }
        public Weapon NextWeapon { get; set; }
    }
}
