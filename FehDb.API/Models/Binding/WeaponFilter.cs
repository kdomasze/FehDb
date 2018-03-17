using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Binding
{
    public class WeaponFilter : BaseFilter
    {
        public string Name { get; set; }

        public int? MightFrom { get; set; }
        public int? MightTo { get; set; }

        public int? RangeFrom { get; set; }
        public int? RangeTo { get; set; }

        public string Effect { get; set; }
        
        public bool? Exclusive { get; set; }

        public bool? Refined { get; set; }

        public WeaponTypeFilter WeaponType { get; set; } 
        public WeaponCostFilter WeaponCost { get; set; }
        public WeaponStatChangeFilter WeaponStatChange { get; set; }

        public bool HaveFilter()
        {
            return WeaponType.HaveFilter() ||
                   WeaponType.HaveFilter() ||
                   WeaponStatChange.HaveFilter() ||
                   !string.IsNullOrEmpty(Name) ||
                   MightFrom != null ||
                   MightTo != null ||
                   RangeFrom != null ||
                   RangeTo != null ||
                   Effect != null ||
                   Exclusive != null ||
                   Refined != null;
        }
    }
}
