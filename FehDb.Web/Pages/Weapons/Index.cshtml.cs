using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FehDb.Web.Models;
using FehDb.Web.Models.Binding;
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
        public IList<WeaponType> WeaponTypes { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentSorting { get; set; }
        public string CurrentSearch { get; set; }
        
        public WeaponFilter Filter { get; set; }

        public IActionResult OnGet(WeaponFilter filter, string weaponType, int pageIndex = 1, string sortBy = "Name,Asc", string search = "")
        {
            if (pageIndex < 1) pageIndex = 1;
            
            Filter = filter;

            if (!string.IsNullOrEmpty(weaponType) && weaponType.Contains(' '))
            {
                var weaponTypeSplit = weaponType.Split(' ');

                var color = weaponTypeSplit[0];
                var arm = weaponTypeSplit[1];

                if (Filter.WeaponType == null) Filter.WeaponType = new WeaponTypeFilter();

                Filter.WeaponType.WeaponColor = Enum.Parse<Color>(color);
                Filter.WeaponType.WeaponArm = Enum.Parse<Arm>(arm);
            }

            CurrentPage = pageIndex;
            CurrentSorting = sortBy;
            CurrentSearch = search;

            HandleSorting(sortBy);

            var client = new RestClient("http://localhost:54359/api/v1");

            var request = new RestRequest("Weapons", Method.GET);

            BuildQueryForRequest(pageIndex, sortBy, search, request);

            var response = client.Execute(request);
            if (!response.IsSuccessful) return NotFound();

            Weapons = JsonConvert.DeserializeObject<PagedResult<Weapon>>(response.Content);
            if (Weapons == null) return NotFound();

            request = new RestRequest("Weapons/types", Method.GET);

            response = client.Execute(request);
            if (!response.IsSuccessful) return NotFound();

            WeaponTypes = JsonConvert.DeserializeObject<IList<WeaponType>>(response.Content);
            if (WeaponTypes == null) return NotFound();

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

        private void BuildQueryForRequest(int pageIndex, string sortBy, string search, RestRequest request)
        {
            request.AddQueryParameter("page", pageIndex.ToString());
            request.AddQueryParameter("sortBy", sortBy);
            request.AddQueryParameter("search", search);

            if (Filter.Refined != null)
                request.AddQueryParameter("refined", Filter.Refined.ToString());

            if (Filter.Exclusive != null)
                request.AddQueryParameter("exclusive", Filter.Exclusive.ToString());

            if (Filter.MightFrom != null)
                request.AddQueryParameter("MightFrom", Filter.MightFrom.ToString());
            if (Filter.MightTo != null)
                request.AddQueryParameter("MightTo", Filter.MightTo.ToString());

            if (Filter.RangeFrom != null)
                request.AddQueryParameter("RangeFrom", Filter.RangeFrom.ToString());
            if (Filter.RangeTo != null)
                request.AddQueryParameter("RangeTo", Filter.RangeTo.ToString());

            if (Filter.WeaponType != null)
            {
                request.AddQueryParameter("WeaponType.WeaponColor", Filter.WeaponType.WeaponColor.ToString());
                request.AddQueryParameter("WeaponType.WeaponArm", Filter.WeaponType.WeaponArm.ToString());
            }

            if (Filter.WeaponCost != null)
            {
                if (Filter.WeaponCost.SpCostFrom != null)
                    request.AddQueryParameter("WeaponCost.SpCostFrom", Filter.WeaponCost.SpCostFrom.ToString());
                if (Filter.WeaponCost.SpCostTo != null)
                    request.AddQueryParameter("WeaponType.WeaponArm", Filter.WeaponCost.SpCostTo.ToString());

                if (Filter.WeaponCost.MedalsFrom != null)
                    request.AddQueryParameter("WeaponCost.MedalsFrom", Filter.WeaponCost.MedalsFrom.ToString());
                if (Filter.WeaponCost.MedalsTo != null)
                    request.AddQueryParameter("WeaponType.MedalsTo", Filter.WeaponCost.MedalsTo.ToString());

                if (Filter.WeaponCost.StonesFrom != null)
                    request.AddQueryParameter("WeaponCost.StonesFrom", Filter.WeaponCost.StonesFrom.ToString());
                if (Filter.WeaponCost.StonesTo != null)
                    request.AddQueryParameter("WeaponType.StonesTo", Filter.WeaponCost.StonesTo.ToString());

                if (Filter.WeaponCost.DewFrom != null)
                    request.AddQueryParameter("WeaponCost.DewFrom", Filter.WeaponCost.DewFrom.ToString());
                if (Filter.WeaponCost.DewTo != null)
                    request.AddQueryParameter("WeaponType.DewTo", Filter.WeaponCost.DewTo.ToString());
            }
        }
    }
}