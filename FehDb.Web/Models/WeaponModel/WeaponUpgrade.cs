using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models.WeaponModel
{
    public class WeaponUpgrade : BaseEntity
    {
        [Required]
        public int PreviousWeaponID { get; set; }
        [Required]
        public int NextWeaponID { get; set; }
    }
}
