using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public abstract class Specifications<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDecinding { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Specifications(Expression<Func<T, bool>>? criteria = null)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void SetOrderBy(Expression<Func<T, object>> Expression)
            => OrderBy = Expression;
        protected void SetOrderByDescinding(Expression<Func<T, object>> Expression)
           => OrderByDecinding = Expression;

        public int skip { get; private set; }
        public int take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageIndex , int pageSize)
        {
            IsPaginated = true;
            take = pageSize;
            skip = (pageIndex - 1) * pageSize;
        }

    }
}
