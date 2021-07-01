using System;
using System.Linq;
using System.Linq.Expressions;

namespace ChallengeNubimetrics.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool should, Expression<Func<T, bool>> expression)
            => should
                ? query.Where(expression)
                : query;
    }
}