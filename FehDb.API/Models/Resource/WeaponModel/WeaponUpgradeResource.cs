using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource.WeaponModel
{
    public class WeaponUpgradeResource : BaseResource
    {
        [Required]
        public int PreviousWeaponID { get; set; }
        [Required]
        public int NextWeaponID { get; set; }
    }
}
