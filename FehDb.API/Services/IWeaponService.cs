using FehDb.API.Models;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Models.Resource;
using FehDb.API.Models.Resource.WeaponModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public interface IWeaponService
    {
        IList<WeaponTypeResource> GetWeaponTypes();

        PagedResult<WeaponResource> GetWeapons(Query query, WeaponFilter filter);
        WeaponResource GetWeaponByID(int ID);
        Task<WeaponResource> Create(WeaponResource entity);
        Task CreateFromList(IEnumerable<WeaponResource> entity);
        Task Update(int ID, WeaponResource resource);
        Task Delete(int ID);

        List<WeaponResource> GetWeaponUpgrades(int weaponID);
        Task<WeaponUpgradeResource> CreateWeaponUpgrades(WeaponUpgradeResource entity);
        Task UpdateWeaponUpgrades(int ID, WeaponUpgradeResource resource);
        Task DeleteWeaponUpgrades(int ID);
        Task DeleteWeaponUpgradesByWeapon(WeaponUpgradeResource weaponUpgradeResource);
    }
}
