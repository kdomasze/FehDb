using FehDb.API.Business;
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
    public class WeaponUpgradeRepository : BaseRepository<WeaponUpgrade>
    {
        public WeaponUpgradeRepository(FehContext context) : base(context) {}

        public IQueryable<WeaponUpgrade> GetUpgradesForWeaponOfId(int id)
        {
            var upgrades = _entities.Where(wu => wu.PreviousWeaponID == id);

            return upgrades;
        }

        public async Task<WeaponUpgrade> GetUpgradeForWeaponPair(int baseWeaponId, int upgradeWeaponId)
        {
            var upgrade = await _entities.SingleOrDefaultAsync(wu => wu.PreviousWeaponID == baseWeaponId && wu.NextWeaponID == upgradeWeaponId);

            return upgrade;
        }
    }
}
