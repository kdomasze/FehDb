using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Binding
{
    [ModelBinder]
    public class Query
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string SortBy { get; set; } = null;
        public string Search { get; set; } = null;
    }
}
