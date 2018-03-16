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

        public WeaponService(FehContext context, IMapper mapper)
        {
            _mapper = mapper;

            _weaponRepository = new WeaponRepository(context);
            _weaponTypeRepository = new WeaponTypeRepository(context);
        }

        public async Task<IList<WeaponResource>> GetWeapons()
        {
            var weaponsEnumerable = await _weaponRepository.GetAllAsync();
            
            var result = _mapper.Map<IList<WeaponResource>>(weaponsEnumerable.ToList());

            return result;
        }

        public async Task<WeaponResource> GetWeaponByID(int ID)
        {
            var weapon = await _weaponRepository.GetByIdAsync(ID);

            if (weapon == null) throw new Exception("Weapon matching ID not found.");

            var result = _mapper.Map<WeaponResource>(weapon);

            return result;
        }

        public async Task Create(WeaponResource entity)
        {
            var weapon = _mapper.Map<Weapon>(entity);

            weapon.DateAdded = DateTime.Now;

            // Ensure WeaponType is supplied
            if (weapon.WeaponType == null) throw new ArgumentNullException("weapon.WeaponType", "The specified WeaponType is null.");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(weapon.WeaponType.Color, weapon.WeaponType.Arm);
            weapon.WeaponTypeID = weaponType.ID;
            weapon.WeaponType = null;
            
            // Ensure WeaponCost is supplied
            if(weapon.WeaponCost == null) throw new ArgumentNullException("weapon.WeaponCost", "The specified WeaponCost is null.");

            weapon.WeaponCost.DateAdded = weapon.DateAdded;

            // Add DateAdded field to WeaponEffectiveAgainst details
            if (weapon.WeaponEffectiveAgainst != null)
            {
                if (weapon.WeaponEffectiveAgainst.WeaponTypes != null)
                {
                    foreach(var wtea in weapon.WeaponEffectiveAgainst.WeaponTypes)
                    {
                        wtea.DateAdded = weapon.DateAdded;
                    }
                }

                if (weapon.WeaponEffectiveAgainst.MovementTypes != null)
                {
                    foreach (var mtea in weapon.WeaponEffectiveAgainst.MovementTypes)
                    {
                        mtea.DateAdded = weapon.DateAdded;
                    }
                }

                weapon.WeaponEffectiveAgainst.DateAdded = weapon.DateAdded;
            }

            // Add DateAdded field to WeaponStatChange details
            if (weapon.WeaponStatChange != null)
            {
                weapon.WeaponStatChange.DateAdded = weapon.DateAdded;
            }

            // Insert and save
            await _weaponRepository.Insert(weapon);
            await _weaponRepository.SaveChanges();
        }

        public async Task Update(int ID, WeaponResource resource)
        {
            var entity = _mapper.Map<Weapon>(resource);
            
            entity.DateModified = DateTime.Now;

            // Ensure WeaponType is supplied
            if (entity.WeaponType == null) throw new ArgumentNullException("weapon.WeaponType", "The specified WeaponType is null.");

            var weaponType = await _weaponTypeRepository.GetByWeaponType(entity.WeaponType.Color, entity.WeaponType.Arm);
            entity.WeaponTypeID = weaponType.ID;
            entity.WeaponType = null;

            // Ensure WeaponCost is supplied
            if (entity.WeaponCost == null) throw new ArgumentNullException("weapon.WeaponCost", "The specified WeaponCost is null.");

            entity.WeaponCost.DateModified = entity.DateModified;

            // Add DateModified field to WeaponEffectiveAgainst details
            if (entity.WeaponEffectiveAgainst != null)
            {
                if (entity.WeaponEffectiveAgainst.WeaponTypes != null)
                {
                    foreach (var wtea in entity.WeaponEffectiveAgainst.WeaponTypes)
                    {
                        wtea.DateModified = entity.DateModified;
                    }
                }

                if (entity.WeaponEffectiveAgainst.MovementTypes != null)
                {
                    foreach (var mtea in entity.WeaponEffectiveAgainst.MovementTypes)
                    {
                        mtea.DateModified = entity.DateModified;
                    }
                }

                entity.WeaponEffectiveAgainst.DateModified = entity.DateModified;
            }

            // Add DateModified field to WeaponStatChange details
            if (entity.WeaponStatChange != null)
            {
                entity.WeaponStatChange.DateModified = entity.DateModified;
            }

            // Update and save
            await _weaponRepository.Update(entity);
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
