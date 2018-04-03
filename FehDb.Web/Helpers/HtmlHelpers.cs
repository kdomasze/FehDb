using FehDb.Web.Pages.Weapons;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace FehDb.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent SortLink(this IHtmlHelper<IndexModel> helper, string text, IndexModel model, string sortParam)
        {
            StringBuilder output = new StringBuilder();
            var sortText = string.IsNullOrEmpty((string)model.ViewData[sortParam]) ? text + ",Dec" : model.ViewData[sortParam];

            var routeValues = CreateRouteToCurrentPage(helper);
            if (routeValues.ContainsKey("sortBy"))
                routeValues["sortBy"] = sortText;
            else
                routeValues.Add("sortBy", sortText);

            var content = helper.RouteLink(text, routeValues);
            
            output.Append(GetString(content));
            if ((string)model.ViewData[sortParam] == text + ",Asc") output.Append("&#9662;");
            if ((string)model.ViewData[sortParam] == text + ",Dec") output.Append("&#9652;");

            return new HtmlString(output.ToString());
        }

        public static IHtmlContent PageLink(this IHtmlHelper<IndexModel> helper, string text, int page)
        {
            StringBuilder output = new StringBuilder();

            var routeValues = CreateRouteToCurrentPage(helper);

            if (routeValues.ContainsKey("pageIndex"))
                routeValues["pageIndex"] = page;
            else
                routeValues.Add("pageIndex", page);

            var content = helper.RouteLink(text, routeValues);
            
            output.Append(GetString(content));
            return new HtmlString(output.ToString());
        }

        private static string GetString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        private static RouteValueDictionary CreateRouteToCurrentPage(IHtmlHelper<IndexModel> helper)
        {
            RouteValueDictionary routeValues
                 = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            NameValueCollection queryString
                 = HttpUtility.ParseQueryString(helper.ViewContext.HttpContext.Request.QueryString.Value);

            foreach (string key in queryString.Cast<string>())
            {
                routeValues[key] = queryString[key];
            }

            return routeValues;
        }
    }
}
