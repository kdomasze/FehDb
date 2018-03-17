using AutoMapper.QueryableExtensions;
using FehDb.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Extensions
{
    // Source: http://gunnarpeipman.com/2017/01/ef-core-paging/
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();
            
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static async Task<PagedResult<U>> GetPaged<T, U>(this IQueryable<T> query, int page, int pageSize) where U : class
        {
            var result = new PagedResult<U>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip)
                                        .Take(pageSize)
                                        .ProjectTo<U>()
                                        .ToListAsync();

            return result;
        }
    }
}
