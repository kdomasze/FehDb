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
                if (filter.IdFrom.HasValue)
                    weapon = weapon.Where(x => x.ID >= filter.IdFrom);
                if (filter.IdTo.HasValue)
                    weapon = weapon.Where(x => x.ID <= filter.IdTo);

                if (!string.IsNullOrEmpty(filter.Name))
                    weapon = weapon.Where(x => x.Name.Contains(filter.Name));

                if (filter.MightFrom.HasValue)
                    weapon = weapon.Where(x => x.Might >= filter.MightFrom);
                if (filter.MightTo.HasValue)
                    weapon = weapon.Where(x => x.Might <= filter.MightTo);

                if (filter.RangeFrom.HasValue)
                    weapon = weapon.Where(x => x.Range >= filter.RangeFrom);
                if (filter.RangeTo.HasValue)
                    weapon = weapon.Where(x => x.Range <= filter.RangeTo);

                if (!string.IsNullOrEmpty(filter.Effect))
                    weapon = weapon.Where(x => x.Effect.Contains(filter.Effect));

                if (filter.Exclusive.HasValue)
                    weapon = weapon.Where(x => x.Exclusive == filter.Exclusive);

                if (filter.Refined.HasValue)
                    weapon = weapon.Where(x => x.Refined == filter.Refined);

                if (filter.WeaponCost.HaveFilter())
                {
                    if (filter.WeaponCost.SpCostFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.SpCost >= filter.WeaponCost.SpCostFrom);
                    if (filter.WeaponCost.SpCostTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.SpCost <= filter.WeaponCost.SpCostTo);

                    if (filter.WeaponCost.MedalsFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Medals >= filter.WeaponCost.MedalsFrom);
                    if (filter.WeaponCost.MedalsTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Medals <= filter.WeaponCost.MedalsTo);

                    if (filter.WeaponCost.StonesFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Stones >= filter.WeaponCost.StonesFrom);
                    if (filter.WeaponCost.StonesTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Stones <= filter.WeaponCost.StonesTo);

                    if (filter.WeaponCost.DewFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Dew >= filter.WeaponCost.DewFrom);
                    if (filter.WeaponCost.DewTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponCost.Dew <= filter.WeaponCost.DewTo);
                }

                if (filter.WeaponStatChange.HaveFilter())
                {
                    if (filter.WeaponStatChange.HPFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.HP >= filter.WeaponStatChange.HPFrom);
                    if (filter.WeaponStatChange.HPTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.HP <= filter.WeaponStatChange.HPTo);

                    if (filter.WeaponStatChange.MightFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Might >= filter.WeaponStatChange.MightFrom);
                    if (filter.WeaponStatChange.MightTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Might <= filter.WeaponStatChange.MightTo);

                    if (filter.WeaponStatChange.SpeedFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Speed >= filter.WeaponStatChange.SpeedFrom);
                    if (filter.WeaponStatChange.SpeedTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Speed <= filter.WeaponStatChange.SpeedTo);

                    if (filter.WeaponStatChange.DefenseFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Defense >= filter.WeaponStatChange.DefenseFrom);
                    if (filter.WeaponStatChange.DefenseTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Defense <= filter.WeaponStatChange.DefenseTo);

                    if (filter.WeaponStatChange.ResistanceFrom.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Resistance >= filter.WeaponStatChange.ResistanceFrom);
                    if (filter.WeaponStatChange.ResistanceTo.HasValue)
                        weapon = weapon.Where(x => x.WeaponStatChange.Resistance <= filter.WeaponStatChange.ResistanceTo);
                }

                if (filter.WeaponType.HaveFilter())
                {
                    if (filter.WeaponType.WeaponColor.HasValue)
                        weapon = weapon.Where(x => x.WeaponType.Color == filter.WeaponType.WeaponColor);
                    if (filter.WeaponType.WeaponArm.HasValue)
                        weapon = weapon.Where(x => x.WeaponType.Arm == filter.WeaponType.WeaponArm);
                }
            }

            return weapon;
        }
    }
}
