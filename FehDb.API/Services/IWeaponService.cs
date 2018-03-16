﻿using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Models.Resource.WeaponModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public interface IWeaponService
    {
        Task<IList<WeaponResource>> GetWeapons();
        Task<WeaponResource> GetWeaponByID(int ID);
        Task Create(WeaponResource entity);
        Task Update(int ID, WeaponResource resource);
        Task Delete(int ID);
    }
}
