using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models.Binding
{
    public class WeaponStatChangeFilter
    {
        public int? HPFrom { get; set; }
        public int? HPTo { get; set; }

        public int? MightFrom { get; set; }
        public int? MightTo { get; set; }

        public int? SpeedFrom { get; set; }
        public int? SpeedTo { get; set; }

        public int? DefenseFrom { get; set; }
        public int? DefenseTo { get; set; }

        public int? ResistanceFrom { get; set; }
        public int? ResistanceTo { get; set; }

        public bool HaveFilter()
        {
            return HPFrom != null ||
                   HPTo != null ||
                   MightFrom != null ||
                   MightTo != null ||
                   SpeedFrom != null ||
                   SpeedTo != null ||
                   DefenseFrom != null ||
                   DefenseTo != null ||
                   ResistanceFrom != null ||
                   ResistanceTo != null;
        }
    }
}
