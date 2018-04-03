using AutoMapper;
using FehDb.API.Business;
using FehDb.API.Contexts;
using FehDb.API.Extensions;
using FehDb.API.Infrustructure.Exceptions;
using FehDb.API.Infrustructure.Exceptions.Weapons;
using FehDb.API.Models;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Models.Resource;
using FehDb.API.Models.Resource.WeaponModel;
using FehDb.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;

        private WeaponRepository _weaponRepository;
        private WeaponUpgradeRepository _weaponUpgradeRepository;
        private WeaponTypeRepository _weaponTypeRepository;
        private BaseRepository<WeaponCost> _weaponCostRepository;

        private BaseRepository<WeaponEffectiveAgainst> _WeaponEffectiveAgainstRepository;
        private BaseRepository<WeaponStatChange> _weaponStatChangeRepository;

        public WeaponService(FehContext context, IMapper mapper)
        {
            _mapper = mapper;

            _weaponRepository = new WeaponRepository(context);
            _weaponUpgradeRepository = new WeaponUpgradeRepository(context);
            _weaponTypeRepository = new WeaponTypeRepository(context);
            _weaponCostRepository = new BaseRepository<WeaponCost>(context);

            _WeaponEffectiveAgainstRepository = new BaseRepository<WeaponEffectiveAgainst>(context);
            _weaponStatChangeRepository = new BaseRepository<WeaponStatChange>(context);

        }

        public IList<WeaponTypeResource> GetWeaponTypes()
        {
            var weaponTypes = _weaponTypeRepository.GetAll(new Query(), new BaseFilter()).Results;

            return _mapper.Map<IList<WeaponTypeResource>>(weaponTypes);
        }

        #region Weapons
        public PagedResult<WeaponResource> GetWeapons(Query query, WeaponFilter filter)
        {
            var weaponsEnumerable = _weaponRepository.GetAll(query, filter);
            
            var result = _mapper.Map<PagedResult<WeaponResource>>(weaponsEnumerable);

            return result;
        }

        public WeaponResource GetWeaponByID(int ID)
        {
            var weapon = _weaponRepository.GetById(ID);

            if (weapon == null) throw new WeaponNotFoundException("Weapon matching ID not found.");

            var result = _mapper.Map<WeaponResource>(weapon);

            return result;
        }

        public async Task<WeaponResource> Create(WeaponResource entity)
        {
            var weapon = _mapper.Map<Weapon>(entity);
            
            // Ensure WeaponType is supplied
            if (weapon.WeaponType == null) throw new InvalidModelException("weapon.WeaponType", "Null");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(weapon.WeaponType.Color, weapon.WeaponType.Arm);
            weapon.WeaponTypeID = weaponType.ID;
            weapon.WeaponType = null;
            
            // Ensure WeaponCost is supplied
            if(weapon.WeaponCost == null) throw new InvalidModelException("weapon.WeaponCost", "Null");
            
            // Insert and save
            await _weaponRepository.Insert(weapon);
            await _weaponRepository.SaveChanges();

            return _mapper.Map<WeaponResource>(weapon);
        }

        public async Task CreateFromList(IEnumerable<WeaponResource> entity)
        {
            var weaponList = _mapper.Map<IEnumerable<Weapon>>(entity);

            foreach (var weapon in weaponList)
            {
                // Ensure WeaponType is supplied
                if (weapon.WeaponType == null) throw new InvalidModelException("weapon.WeaponType", "Null");

                var weaponType = await _weaponTypeRepository.GetByWeaponType(weapon.WeaponType.Color, weapon.WeaponType.Arm);
                weapon.WeaponTypeID = weaponType.ID;
                weapon.WeaponType = null;

                // Ensure WeaponCost is supplied
                if (weapon.WeaponCost == null) throw new InvalidModelException("weapon.WeaponCost", "Null");

                // Insert and save
                await _weaponRepository.Insert(weapon);
            }
            await _weaponRepository.SaveChanges();
        }

        public async Task Update(int ID, WeaponResource resource)
        {
            var entity = _mapper.Map<Weapon>(resource);
            
            // Ensure WeaponType is supplied
            if (entity.WeaponType == null) throw new InvalidModelException("weapon.WeaponType", "Null");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(entity.WeaponType.Color, entity.WeaponType.Arm);
            entity.WeaponTypeID = weaponType.ID;
            entity.WeaponType = null;

            // Ensure WeaponCost is supplied
            if (entity.WeaponCost == null) throw new InvalidModelException("weapon.WeaponCost", "Null");

            // Update weapon
            await _weaponRepository.Update(entity);

            // Update weaponCost
            await _weaponCostRepository.Update(entity.WeaponCost);

            // Update WeaponEffectiveAgainst
            if(entity.WeaponEffectiveAgainst != null)
                await _WeaponEffectiveAgainstRepository.Update(entity.WeaponEffectiveAgainst);

            // Update WeaponStatChange
            if (entity.WeaponStatChange != null)
                await _weaponStatChangeRepository.Update(entity.WeaponStatChange);
            
            await _weaponRepository.SaveChanges();
        }

        public async Task Delete(int ID)
        {
            var weapon = _weaponRepository.GetById(ID);

            if (weapon == null) throw new WeaponNotFoundException("Weapon matching ID not found.");

            // Delete and save
            _weaponRepository.Delete(weapon);
            await _weaponRepository.SaveChanges();
        }
        #endregion

        #region Weapon Upgrades
        public List<WeaponResource> GetWeaponUpgrades(int weaponID)
        {
            var weaponIds = _weaponUpgradeRepository.GetUpgradesForWeaponOfId(weaponID).Select(wu => wu.NextWeaponID);

            List<Weapon> weaponList = new List<Weapon>();

            foreach (var id in weaponIds)
            {
                weaponList.Add(_weaponRepository.GetById(id));
            }

            var result = _mapper.Map<List<WeaponResource>>(weaponList);

            return result;
        }

        public async Task<WeaponUpgradeResource> CreateWeaponUpgrades(WeaponUpgradeResource entity)
        {
            var weapon = _mapper.Map<WeaponUpgrade>(entity);

            // Insert and save
            await _weaponUpgradeRepository.Insert(weapon);
            await _weaponUpgradeRepository.SaveChanges();

            return _mapper.Map<WeaponUpgradeResource>(weapon);
        }

        public async Task UpdateWeaponUpgrades(int ID, WeaponUpgradeResource resource)
        {
            var entity = _mapper.Map<WeaponUpgrade>(resource);

            // Update weaponUpgrade and save
            await _weaponUpgradeRepository.Update(entity);
            await _weaponRepository.SaveChanges();
        }

        public async Task DeleteWeaponUpgrades(int ID)
        {
            var weaponUpgrade = _weaponUpgradeRepository.GetById(ID);

            if (weaponUpgrade == null) throw new WeaponUpgradeNotFoundException("Weapon matching ID not found.");

            // Delete and save
            _weaponUpgradeRepository.Delete(weaponUpgrade);
            await _weaponRepository.SaveChanges();
        }

        public async Task DeleteWeaponUpgradesByWeapon(WeaponUpgradeResource weaponUpgradeResource)
        {
            var weaponUpgrade = await _weaponUpgradeRepository.GetUpgradeForWeaponPair(weaponUpgradeResource.PreviousWeaponID, weaponUpgradeResource.NextWeaponID);

            if (weaponUpgrade == null) throw new WeaponUpgradeNotFoundException("Weapon matching ID not found.");

            // Delete and save
            _weaponUpgradeRepository.Delete(weaponUpgrade);
            await _weaponRepository.SaveChanges();
        }
        #endregion
    }
}
