using FehDb.API.Buisness;
using FehDb.API.Contexts;
using FehDb.API.Extensions;
using FehDb.API.Models;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.WeaponModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Repositories
{
    public class WeaponRepository : BaseRepository<Weapon>
    {
        public WeaponRepository(FehContext context) : base(context) {}

        public override async Task<PagedResult<Weapon>> GetAllAsync(Query query, BaseFilter filter)
        {
            IQueryable<Weapon> weaponSet = _entities
                .Include(w => w.WeaponType)
                .Include(w => w.WeaponCost)
                .Include(w => w.WeaponStatChange)
                .Include(w => w.WeaponEffectiveAgainst);

            weaponSet = WeaponBusinessLogic.Parse(weaponSet, query, (WeaponFilter)filter);

            return await weaponSet.GetPaged(query.Page, query.PageSize);
        }

        public override async Task<Weapon> GetByIdAsync(int id)
        {
            IQueryable<Weapon> weaponSet = _entities
                .Include(w => w.WeaponType)
                .Include(w => w.WeaponCost)
                .Include(w => w.WeaponStatChange)
                .Include(w => w.WeaponEffectiveAgainst);

            return await weaponSet.SingleOrDefaultAsync(s => s.ID == id);
        }

        public override async Task Update(Weapon entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null.");

            IQueryable<Weapon> weaponSet = _entities
                .Include(w => w.WeaponType)
                .Include(w => w.WeaponCost)
                .Include(w => w.WeaponStatChange)
                .Include(w => w.WeaponEffectiveAgainst);

            var oldEntity = await weaponSet.SingleOrDefaultAsync(w => w.ID == entity.ID);

            entity.DateAdded = oldEntity.DateAdded;
            entity.DateModified = oldEntity.DateModified;

            entity.WeaponCost.ID = oldEntity.WeaponCost.ID;
            entity.WeaponCost.WeaponID = oldEntity.ID;

            if (entity.WeaponEffectiveAgainst != null)
            {
                entity.WeaponEffectiveAgainst.ID = oldEntity.WeaponEffectiveAgainst.ID;
                entity.WeaponEffectiveAgainst.WeaponID = oldEntity.ID;
            }

            if (entity.WeaponStatChange != null)
            {
                entity.WeaponStatChange.ID = oldEntity.WeaponStatChange.ID;
                entity.WeaponStatChange.WeaponID = oldEntity.ID;
            }

            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
    }
}
