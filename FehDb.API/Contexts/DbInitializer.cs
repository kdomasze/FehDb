﻿using FehDb.API.Contexts;
using FehDb.API.Models;
using FehDb.API.Models.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FehDb.DAL.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(FehContext context)
        {
            context.Database.EnsureCreated();

            if (!context.WeaponTypes.Any())
            {
                var weaponTypes = JsonConvert.DeserializeObject<List<WeaponType>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "WeaponType.json"));

                foreach (var wt in weaponTypes)
                {
                    wt.DateAdded = DateTime.Now;
                    context.WeaponTypes.Add(wt);
                }

                context.SaveChanges();
            }

            if (!context.MovementTypes.Any())
            {
                var movementTypes = JsonConvert.DeserializeObject<List<MovementType>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "MovementType.json"));

                foreach (var mt in movementTypes)
                {
                    mt.DateAdded = DateTime.Now;
                    context.MovementTypes.Add(mt);
                }

                context.SaveChanges();
            }
        }
    }
}