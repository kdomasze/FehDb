using FehDb.API.Business;
using FehDb.API.Contexts;
using FehDb.API.Models;
using FehDb.API.Models.Entity;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Models.Resource.UserModel;
using FehDb.API.Models.Resource.WeaponModel;
using FehDb.API.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FehDb.DAL.Contexts
{
    public static class DbInitializer
    {
        public static async Task Initialize(FehContext context, IWeaponService weaponService, IAuthService authService, IConfiguration configuration)
        {
            context.Database.EnsureCreated();

            if (!context.WeaponTypes.Any())
            {
                try
                {
                    var weaponTypes = JsonConvert.DeserializeObject<List<WeaponType>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + GetFileName("WeaponType")));

                    foreach (var wt in weaponTypes)
                    {
                        context.WeaponTypes.Add(wt);
                    }

                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

            if (!context.MovementTypes.Any())
            {
                try
                {
                    var movementTypes = JsonConvert.DeserializeObject<List<MovementType>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + GetFileName("MovementType")));

                    foreach (var mt in movementTypes)
                    {
                        context.MovementTypes.Add(mt);
                    }

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            if(!context.Weapons.Any())
            {
                try
                {
                    var weapons = JsonConvert.DeserializeObject<List<WeaponResource>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + GetFileName("Weapons")));
                    var refinedWeapons = JsonConvert.DeserializeObject<List<WeaponResource>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + GetFileName("WeaponsRefined")));

                    // gets images for refined weapons (since they have the same image as the unrefined versions)
                    for(int i = 0; i < refinedWeapons.Count(); i++)
                    {
                        if(string.IsNullOrEmpty(refinedWeapons[i].ImageUri))
                        {
                            var split = refinedWeapons[i].Name.Split('(')[0];
                            var fixedLength = split.Substring(0, split.Length - 1);
                            var weaponWhere = weapons.Where(w => w.Name.Contains(fixedLength));

                            refinedWeapons[i].ImageUri = weaponWhere.First().ImageUri;
                        }
                    }

                    await weaponService.CreateFromList(weapons);
                    await weaponService.CreateFromList(refinedWeapons);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            if (!context.Users.Any())
            {
                try
                {
                    UserResource userEntry = new UserResource()
                    {
                        Username = "Admin",
                        Password = "default"
                    };

                    await authService.CreateAccount(userEntry);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the latest version of the specified json file from the Seed directory for seeding the database
        /// </summary>
        /// <param name="fileNamePartial">The file name partial for the file (full file format: "[partial].[date].json)</param>
        /// <returns>The full file name</returns>
        private static string GetFileName(string fileNamePartial)
        {
            DirectoryInfo directory = new DirectoryInfo(@"./Seed");
            FileInfo[] filesInDir = directory.GetFiles(fileNamePartial + ".*.json");

            if (filesInDir.Count() == 0)
                throw new FileNotFoundException("The specified file name partial (" + fileNamePartial + ") does not exist in the \"Seed\" directory.");

            DateTime currentDateTime = DateTime.MinValue;
            string output = "";
            foreach (var file in filesInDir)
            {
                string name = file.Name;
                string[] splitName = name.Split('.');

                if(splitName.Count() != 3)
                    throw new FormatException("The format of the specified file (" + name + ") is not correct. Correct format: [partial].[date].json");

                if (!DateTime.TryParse(name.Split('.')[1], out DateTime fileTime))
                    throw new FormatException("The format of the specified file (" + name + ") is not correct. Correct format: [partial].[date].json");

                if (fileTime > currentDateTime)
                    output = name;
            }

            return output;
        }
    }
}
