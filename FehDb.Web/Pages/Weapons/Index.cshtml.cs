using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FehDb.Web.Models;
using FehDb.Web.Models.WeaponModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace FehDb.Web.Pages.Weapons
{
    public class IndexModel : PageModel
    {
        public PagedResult<Weapon> Weapons { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentSorting { get; set; }

        public IActionResult OnGet(int pageIndex = 1, string sortBy = "Name,Asc")
        {
            CurrentPage = pageIndex;
            CurrentSorting = sortBy;

            HandleSorting(sortBy);

            var client = new RestClient("http://localhost:54359/api/v1");

            var request = new RestRequest("Weapons", Method.GET);
            
            request.AddQueryParameter("page", pageIndex.ToString());
            request.AddQueryParameter("sortBy", sortBy);


            var response = client.Execute(request);
            if (!response.IsSuccessful) return NotFound();

            Weapons = JsonConvert.DeserializeObject<PagedResult<Weapon>>(response.Content);
            if(Weapons == null) return NotFound();

            return Page();
        }

        private void HandleSorting(string sortBy)
        {            
            switch (sortBy.Split(',')[0])
            {
                case "Name":
                    ViewData["NameSortParam"] = sortBy == "Name,Asc" ? "Name,Dec" : "Name,Asc";
                    ViewData["MightSortParam"] = "";
                    ViewData["RangeSortParam"] = "";
                    break;
                case "Might":
                    ViewData["MightSortParam"] = sortBy == "Might,Asc" ? "Might,Dec" : "Might,Asc";
                    ViewData["NameSortParam"] = "";
                    ViewData["RangeSortParam"] = "";
                    break;
                case "Range":
                    ViewData["RangeSortParam"] = sortBy == "Range,Asc" ? "Range,Dec" : "Range,Asc";
                    ViewData["NameSortParam"] = "";
                    ViewData["MightSortParam"] = "";
                    break;
            }
        }
    }
}