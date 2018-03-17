using AutoMapper;
using FehDb.API.Contexts;
using FehDb.API.Models;
using FehDb.API.Models.Entity.WeaponModel;
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
        private WeaponTypeRepository _weaponTypeRepository;
        private BaseRepository<WeaponCost> _weaponCostRepository;

        private BaseRepository<WeaponEffectiveAgainst> _WeaponEffectiveAgainstRepository;
        private BaseRepository<WeaponStatChange> _weaponStatChangeRepository;

        public WeaponService(FehContext context, IMapper mapper)
        {
            _mapper = mapper;

            _weaponRepository = new WeaponRepository(context);
            _weaponTypeRepository = new WeaponTypeRepository(context);
            _weaponCostRepository = new BaseRepository<WeaponCost>(context);

            _WeaponEffectiveAgainstRepository = new BaseRepository<WeaponEffectiveAgainst>(context);
            _weaponStatChangeRepository = new BaseRepository<WeaponStatChange>(context);

        }

    public async Task<PagedResult<WeaponResource>> GetWeapons(int page, int pageSize)
        {
            var weaponsEnumerable = await _weaponRepository.GetAllAsync(page, pageSize);
            
            var result = _mapper.Map<PagedResult<WeaponResource>>(weaponsEnumerable);

            return result;
        }

        public async Task<WeaponResource> GetWeaponByID(int ID)
        {
            var weapon = await _weaponRepository.GetByIdAsync(ID);

            if (weapon == null) throw new Exception("Weapon matching ID not found.");

            var result = _mapper.Map<WeaponResource>(weapon);

            return result;
        }

        public async Task<WeaponResource> Create(WeaponResource entity)
        {
            var weapon = _mapper.Map<Weapon>(entity);
            
            // Ensure WeaponType is supplied
            if (weapon.WeaponType == null) throw new ArgumentNullException("weapon.WeaponType", "The specified WeaponType is null.");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(weapon.WeaponType.Color, weapon.WeaponType.Arm);
            weapon.WeaponTypeID = weaponType.ID;
            weapon.WeaponType = null;
            
            // Ensure WeaponCost is supplied
            if(weapon.WeaponCost == null) throw new ArgumentNullException("weapon.WeaponCost", "The specified WeaponCost is null.");
            
            // Insert and save
            await _weaponRepository.Insert(weapon);
            await _weaponRepository.SaveChanges();

            return _mapper.Map<WeaponResource>(weapon);
        }

        public async Task Update(int ID, WeaponResource resource)
        {
            var entity = _mapper.Map<Weapon>(resource);
            
            // Ensure WeaponType is supplied
            if (entity.WeaponType == null) throw new ArgumentNullException("weapon.WeaponType", "The specified WeaponType is null.");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(entity.WeaponType.Color, entity.WeaponType.Arm);
            entity.WeaponTypeID = weaponType.ID;
            entity.WeaponType = null;

            // Ensure WeaponCost is supplied
            if (entity.WeaponCost == null) throw new ArgumentNullException("weapon.WeaponCost", "The specified WeaponCost is null.");

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
            var weapon = await _weaponRepository.GetByIdAsync(ID);

            if (weapon == null) throw new Exception("Weapon matching ID not found.");

            // Delete and save
            _weaponRepository.Delete(weapon);
            await _weaponRepository.SaveChanges();
        }
    }
}
