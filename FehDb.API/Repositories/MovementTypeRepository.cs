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
    public class MovementTypeRepository : BaseRepository<MovementType>
    {
        public MovementTypeRepository(FehContext context) : base(context) {}

        public async Task<MovementType> GetByMovementType(Movement movement)
        {
            return await _entities.Where(mt => mt.Movement == movement).SingleOrDefaultAsync();
        }
    }
}
