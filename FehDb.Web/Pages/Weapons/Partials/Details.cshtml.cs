using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FehDb.Web.Models.WeaponModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace FehDb.Web.Pages.Weapons.Partials
{
    public class DetailsModel : PageModel
    {
        public Weapon Weapon { get; set; }
        public int EffectiveLoopLength { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var client = new RestClient("http://localhost:54359/api/v1");

            var request = new RestRequest("Weapons/" + id, Method.GET);

            var response = client.Execute(request);
            if (!response.IsSuccessful) return NotFound();

            Weapon = JsonConvert.DeserializeObject<Weapon>(response.Content);
            if (Weapon == null) return NotFound();
            
            if(Weapon.WeaponEffectiveAgainst != null)
            {
                EffectiveLoopLength = 0;
                if (Weapon.WeaponEffectiveAgainst.WeaponTypes != null)
                {
                    EffectiveLoopLength = Weapon.WeaponEffectiveAgainst.WeaponTypes.Count();
                }

                if(Weapon.WeaponEffectiveAgainst.MovementTypes != null)
                {
                    if(Weapon.WeaponEffectiveAgainst.MovementTypes.Count() > EffectiveLoopLength)
                    {
                        EffectiveLoopLength = Weapon.WeaponEffectiveAgainst.MovementTypes.Count();
                    }
                }
            }

            return Page();
        }
    }
}