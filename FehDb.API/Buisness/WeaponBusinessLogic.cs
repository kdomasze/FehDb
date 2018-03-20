using FehDb.API.Extensions;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.WeaponModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FehDb.API.Buisness
{
    public class WeaponBusinessLogic
    {
        public static IQueryable<Weapon> Parse(IQueryable<Weapon> weapon, Query query, WeaponFilter filter)
        {
            weapon = Search(weapon, query);
            weapon = Sort(weapon, query);
            weapon = Filter(weapon, filter);

            return weapon;
        }

        private static IQueryable<Weapon> Search(IQueryable<Weapon> weapon, Query query)
        {
            if (query.Search == null) return weapon;
            weapon = weapon.Where(w => w.Name == query.Search || w.Effect == query.Search);

            return weapon;
        }

        private static IQueryable<Weapon> Sort(IQueryable<Weapon> weapon, Query query)
        {
            if (query.SortBy == null) return weapon;
            string[] sortBy = query.SortBy.Split(',');

            PropertyInfo property =  typeof(Weapon).GetProperty(sortBy[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            switch (sortBy[1].ToLower())
            {
                case "dec":
                case "decending":
                case "d":
                    weapon = weapon.OrderByDescending(a => property.GetValue(a, null));
                    break;
                default:
                case "asc":
                case "ascending":
                case "a":
                    weapon = weapon.OrderBy(a => property.GetValue(a, null));
                    break;
            }

            return weapon;
        }

        private static IQueryable<Weapon> Filter(IQueryable<Weapon> weapon, WeaponFilter filter)
        {
            if (filter.HaveFilter())
            {
                weapon = weapon.WhereIf(filter.IdFrom.HasValue, x => x.ID >= filter.IdFrom);
                weapon = weapon.WhereIf(filter.IdTo.HasValue, x => x.ID <= filter.IdTo);

                weapon = weapon.WhereIf(!string.IsNullOrEmpty(filter.Name), x => x.Name.Contains(filter.Name));

                weapon = weapon.WhereIf(filter.MightFrom.HasValue, x => x.Might >= filter.MightFrom);
                weapon = weapon.WhereIf(filter.MightTo.HasValue, x => x.Might <= filter.MightTo);

                weapon = weapon.WhereIf(filter.RangeFrom.HasValue, x => x.Range >= filter.RangeFrom);
                weapon = weapon.WhereIf(filter.RangeTo.HasValue, x => x.Range <= filter.RangeTo);

                weapon = weapon.WhereIf(!string.IsNullOrEmpty(filter.Effect), x => x.Effect.Contains(filter.Effect));

                weapon = weapon.WhereIf(filter.Exclusive.HasValue, x => x.Exclusive == filter.Exclusive);

                weapon = weapon.WhereIf(filter.Refined.HasValue, x => x.Refined == filter.Refined);

                if (filter.WeaponCost.HaveFilter())
                {
                    weapon = weapon.WhereIf(filter.WeaponCost.SpCostFrom.HasValue, x => x.WeaponCost.SpCost >= filter.WeaponCost.SpCostFrom);
                    weapon = weapon.WhereIf(filter.WeaponCost.SpCostTo.HasValue, x => x.WeaponCost.SpCost <= filter.WeaponCost.SpCostTo);

                    weapon = weapon.WhereIf(filter.WeaponCost.MedalsFrom.HasValue, x => x.WeaponCost.Medals >= filter.WeaponCost.MedalsFrom);
                    weapon = weapon.WhereIf(filter.WeaponCost.MedalsTo.HasValue, x => x.WeaponCost.Medals <= filter.WeaponCost.MedalsTo);

                    weapon = weapon.WhereIf(filter.WeaponCost.StonesFrom.HasValue, x => x.WeaponCost.Stones >= filter.WeaponCost.StonesFrom);
                    weapon = weapon.WhereIf(filter.WeaponCost.StonesTo.HasValue, x => x.WeaponCost.Stones <= filter.WeaponCost.StonesTo);

                    weapon = weapon.WhereIf(filter.WeaponCost.DewFrom.HasValue, x => x.WeaponCost.Dew >= filter.WeaponCost.DewFrom);
                    weapon = weapon.WhereIf(filter.WeaponCost.DewTo.HasValue, x => x.WeaponCost.Dew <= filter.WeaponCost.DewTo);
                }

                if (filter.WeaponStatChange.HaveFilter())
                {
                    weapon = weapon.WhereIf(filter.WeaponStatChange.HPFrom.HasValue, x => x.WeaponStatChange.HP >= filter.WeaponStatChange.HPFrom);
                    weapon = weapon.WhereIf(filter.WeaponStatChange.HPTo.HasValue, (x => x.WeaponStatChange.HP <= filter.WeaponStatChange.HPTo);

                    weapon = weapon.WhereIf(filter.WeaponStatChange.MightFrom.HasValue, x => x.WeaponStatChange.Might >= filter.WeaponStatChange.MightFrom);
                    weapon = weapon.WhereIf(filter.WeaponStatChange.MightTo.HasValue, x => x.WeaponStatChange.Might <= filter.WeaponStatChange.MightTo);

                    weapon = weapon.WhereIf(filter.WeaponStatChange.SpeedFrom.HasValue, x => x.WeaponStatChange.Speed >= filter.WeaponStatChange.SpeedFrom);
                    weapon = weapon.WhereIf(filter.WeaponStatChange.SpeedTo.HasValue, x => x.WeaponStatChange.Speed <= filter.WeaponStatChange.SpeedTo);

                    weapon = weapon.WhereIf(filter.WeaponStatChange.DefenseFrom.HasValue, x => x.WeaponStatChange.Defense >= filter.WeaponStatChange.DefenseFrom);
                    weapon = weapon.WhereIf(filter.WeaponStatChange.DefenseTo.HasValue, x => x.WeaponStatChange.Defense <= filter.WeaponStatChange.DefenseTo);

                    weapon = weapon.WhereIf(filter.WeaponStatChange.ResistanceFrom.HasValue, x => x.WeaponStatChange.Resistance >= filter.WeaponStatChange.ResistanceFrom);
                    weapon = weapon.WhereIf(filter.WeaponStatChange.ResistanceTo.HasValue, x => x.WeaponStatChange.Resistance <= filter.WeaponStatChange.ResistanceTo);
                }

                if (filter.WeaponType.HaveFilter())
                {
                    weapon = weapon.WhereIf(filter.WeaponType.WeaponColor.HasValue, x => x.WeaponType.Color == filter.WeaponType.WeaponColor);
                    weapon = weapon.WhereIf(filter.WeaponType.WeaponArm.HasValue, x => x.WeaponType.Arm == filter.WeaponType.WeaponArm);
                }
            }

            return weapon;
        }
    }
}
