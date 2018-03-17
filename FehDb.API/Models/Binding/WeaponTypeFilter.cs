using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Binding
{
    public class WeaponTypeFilter
    {
        public Color? WeaponColor { get; set; }
        public Arm? WeaponArm { get; set; }

        public bool HaveFilter()
        {
            return WeaponColor != null ||
                   WeaponArm != null;
        }
    }
}
