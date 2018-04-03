using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models.Binding
{
    public class WeaponCostFilter
    {
        public int? SpCostFrom { get; set; }
        public int? SpCostTo { get; set; }

        public int? MedalsFrom { get; set; }
        public int? MedalsTo { get; set; }

        public int? StonesFrom { get; set; }
        public int? StonesTo { get; set; }

        public int? DewFrom { get; set; }
        public int? DewTo { get; set; }

        public bool HaveFilter()
        {
            return SpCostFrom != null ||
                   SpCostTo != null ||
                   MedalsFrom != null ||
                   MedalsTo != null ||
                   StonesFrom != null ||
                   StonesTo != null ||
                   DewFrom != null ||
                   DewTo != null;
        }
    }
}
