using FehDb.Web.Pages.Weapons;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FehDb.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent SortLink(this IHtmlHelper<IndexModel> helper, string text, string sortParam, int page)
        {
            StringBuilder output = new StringBuilder();
            var sortText = string.IsNullOrEmpty(sortParam) ? text + ",Dec" : sortParam;

            var content = helper.ActionLink(text, "", new { sortBy = sortText, pageIndex = page });

            output.Append(GetString(content));
            if (sortParam == text + ",Asc") output.Append("&#9662;");
            if (sortParam == text + ",Dec") output.Append("&#9652;");

            return new HtmlString(output.ToString());
        }

        private static string GetString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
