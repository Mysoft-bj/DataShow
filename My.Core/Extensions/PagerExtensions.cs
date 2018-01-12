using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using Newtonsoft.Json;
namespace My.Core
{
    public interface IPagerList
    {
        long PageIndex { get; set; }
        long PageSize { get; set; }
        long TotalCount { get; set; }
      

    }
    public interface IPagerList<Item> : IPagerList
    {
        IEnumerable<Item> Items { get; set; }


    }
    //public static class PageListExtension
    //{
    //    public static PagedList<T> Paging<T>(this IQueryable<T> query, string orderby, int pageIndex, int pageSize)
    //    {
    //        if (pageIndex == -1 || pageSize == -1)
    //        {
    //            var list = query.OrderUsingSortExpression(orderby.ToString()).ToList();
    //            return new PagedList<T>(list, 1, list.Count, list.Count);
    //        }
    //        else
    //        {
    //            int totalCount = query.Count();
    //            if (totalCount == 0)
    //                return new PagedList<T>();
    //            int skip = (pageIndex - 1) * pageSize;
    //            if (totalCount < skip)
    //            {
    //                pageIndex = 1;
    //            }
    //            query = query.OrderUsingSortExpression(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
    //            var ilist = query.ToList();
    //            return new PagedList<T>(ilist, pageIndex, pageSize, totalCount);
    //        }
    //    }
    //}
    /// <summary>
    /// 分页的容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerList<T> : IPagerList<T>, IPagerList
    {

        public PagerList(IEnumerable<T> items, long pageIndex, long pageSize, long totalCount)
        {
            Items=items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
       
        public PagerList()
        { }
        /// <summary>
        /// 元素的集合
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }
        /// <summary>
        /// 当前页数，以1开始
        /// </summary>
        [JsonProperty("page")]
        public long PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        [JsonProperty("size")]
        public long PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("total")]
        public long TotalCount { get; set; }       


    }

   
 
}
