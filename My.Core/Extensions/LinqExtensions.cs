using My.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace My.Core
{
    public static class LinqExtensions
    {
        public static bool In(this long id, IEnumerable<long> list) {
            return list.Contains(id);
        }
        public static bool NotIn(this long id, IEnumerable<long> list)
        {
            return !list.Contains(id);
        }
        public static bool Like(this string prop, string val)
        {
            return val.Contains(prop);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            return System.Linq.Dynamic.DynamicQueryable.OrderBy(source, ordering, values);
        }
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
        {
            return System.Linq.Dynamic.DynamicQueryable.Where(source, predicate, values);
        }
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string predicate, params object[] values) {
            return System.Linq.Dynamic.DynamicQueryable.Where(source, predicate, values);
        }
        public static T2 MaxOrDefault<T1, T2>(this IQueryable<T1> query, Expression<Func<T1, T2>> selector)
        {

            try
            {
                return query.Max(selector);
            }
            catch { }
            return default(T2);

        }
        public static T2 MinOrDefault<T1, T2>(this IQueryable<T1> query, Expression<Func<T1, T2>> selector)
        {

            try
            {
                return query.Min(selector);
            }
            catch { }
            return default(T2);

        }
        public static PagerList<T> Paging<T>(this IQueryable<T> query, SearchModel search)
        {
            
            var q = query;
            
            if (search.q.IsNotNull())
                q = q.Where(search.q, search.v);
            var total = query.Count();
            if (total == 0)
                return new PagerList<T>(new List<T>(), search.page, search.size, total);

            if (!search.Orderby.IsNullOrWhiteSpace())
                q = q.OrderBy(search.Orderby);
            else
            {
                q = AddOrderby(q);
             
            }

            var q2 = q.Skip((search.page - 1) * search.size).Take(search.size);

            var list = q2.ToList();
            return new PagerList<T>(list, search.page, search.size, total);
        }
        public static IQueryable<T> AddOrderby<T>(this IQueryable<T> q)
        {
            var type = typeof(T);
            if (typeof(ITreeNode).IsAssignableFrom(type))
            {
                q = q.OrderBy("ParentId,Sort");
            }        
            else if (typeof(IUpdater).IsAssignableFrom(type))
                q = q.OrderBy("LastUpdatedTime desc");
            return q;
        }
   
    }
}
