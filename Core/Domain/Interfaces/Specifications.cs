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

        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public void SetOrderBy(Expression<Func<T, object>> Expression)
            => OrderBy = Expression;
        public void SetOrderByDescinding(Expression<Func<T, object>> Expression)
           => OrderByDecinding = Expression;

    }
}
