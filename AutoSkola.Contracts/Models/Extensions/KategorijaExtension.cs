using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Extensions
{
    public static class KategorijaExtension
    {
        public static IQueryable<AutoSkola.Data.Models.Kategorija> ApplyPaging(this IQueryable<AutoSkola.Data.Models.Kategorija> query, int currPage, int pageSize)
        {
            if (currPage <= 0)
                currPage = 1;
            if (pageSize <= 0 || pageSize > 100)
                pageSize = 20;
            query = query.Skip((currPage - 1) * pageSize)
                .Take(pageSize);
            return query;
        }
    }
}
