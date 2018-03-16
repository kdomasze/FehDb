using FehDb.API.Contexts;
using FehDb.API.Models;
using FehDb.API.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Repositories
{
    public class WeaponTypeRepository : BaseRepository<WeaponType>
    {
        public WeaponTypeRepository(FehContext context) : base(context) {}

        public async Task<WeaponType> GetByWeaponType(Color color, Arm arm)
        {
            return await _entities.Where(wt => wt.Color == color && wt.Arm == arm).SingleOrDefaultAsync();
        }
    }
}
