using FehDb.API.Contexts;
using FehDb.API.Models;
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

        public override async Task<IEnumerable<Weapon>> GetAllAsync()
        {
            IQueryable<Weapon> weaponSet = _entities.Include(w => w.WeaponType).Include(w => w.WeaponCost).Include(w => w.WeaponStatChange).Include(w => w.WeaponEffectiveAgainst);

            return await weaponSet.ToListAsync();
        }

        public override async Task<Weapon> GetByIdAsync(int id)
        {
            IQueryable<Weapon> weaponSet = _entities.Include(w => w.WeaponType).Include(w => w.WeaponCost).Include(w => w.WeaponStatChange).Include(w => w.WeaponEffectiveAgainst);

            return await weaponSet.SingleOrDefaultAsync(s => s.ID == id);
        }
    }
}
