using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Exceptions.Weapons
{
    public class WeaponUpgradeNotFoundException : Exception
    {
        public WeaponUpgradeNotFoundException()
        {
        }

        public WeaponUpgradeNotFoundException(string message) : base(String.Format("Weapon upgrade not found: {0}", message))
        {
        }

        public WeaponUpgradeNotFoundException(string message, Exception innerException) : base(String.Format("Weapon upgrade not found: {0}", message), innerException)
        {
        }
    }
}
