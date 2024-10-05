using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public static class SpecificationsEvaloter
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery , Specifications<T> specifications) where T : class
        {
            var Query = inputQuery;
            if(specifications.Criteria is not null) Query=Query.Where(specifications.Criteria);


            Query = specifications.Includes.Aggregate(Query, (currentQuery, IncludeExpression) =>
            currentQuery.Include(IncludeExpression));

            if (specifications.OrderBy is not null)
                Query = Query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDecinding is not null)
                Query = Query.OrderByDescending(specifications.OrderByDecinding);

                return Query;
        }
    }
}
