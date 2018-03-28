using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Exceptions.Weapons
{
    public class WeaponNotFoundException : Exception
    {
        public WeaponNotFoundException()
        {
        }

        public WeaponNotFoundException(string message) : base(String.Format("Weapon not found: {0}", message))
        {
        }

        public WeaponNotFoundException(string message, Exception innerException) : base(String.Format("Weapon not found: {0}", message), innerException)
        {
        }
    }
}
