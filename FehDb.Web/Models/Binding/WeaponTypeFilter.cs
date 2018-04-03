using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models.Binding
{
    public class WeaponTypeFilter
    {
        public Color? WeaponColor { get; set; }
        public Arm? WeaponArm { get; set; }

        public override string ToString()
        {
            return WeaponColor.ToString() + " " + WeaponArm.ToString();
        }

        public bool HaveFilter()
        {
            return WeaponColor != null ||
                   WeaponArm != null;
        }
    }
}
