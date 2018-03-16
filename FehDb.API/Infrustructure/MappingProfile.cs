using AutoMapper;
using FehDb.API.Models.Entity;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Models.Resource;
using FehDb.API.Models.Resource.WeaponModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Weapon, WeaponResource>();
            CreateMap<WeaponResource, Weapon>();

            CreateMap<WeaponCost, WeaponCostResource>();
            CreateMap<WeaponCostResource, WeaponCost>();

            CreateMap<WeaponEffectiveAgainst, WeaponEffectiveAgainstResource>();
            CreateMap<WeaponEffectiveAgainstResource, WeaponEffectiveAgainst>();

            CreateMap<WeaponEffectiveAgainstMovementType, WeaponEffectiveAgainstMovementTypeResource>();
            CreateMap<WeaponEffectiveAgainstMovementTypeResource, WeaponEffectiveAgainstMovementType>();

            CreateMap<WeaponEffectiveAgainstWeaponType, WeaponEffectiveAgainstWeaponTypeResource>();
            CreateMap<WeaponEffectiveAgainstWeaponTypeResource, WeaponEffectiveAgainstWeaponType>();

            CreateMap<WeaponStatChange, WeaponStatChangeResource>();
            CreateMap<WeaponStatChangeResource, WeaponStatChange>();

            CreateMap<MovementType, MovementTypeResource>();
            CreateMap<MovementTypeResource, MovementType>();

            CreateMap<WeaponType, WeaponTypeResource>();
            CreateMap<WeaponTypeResource, WeaponType>();
        }
    }
}
