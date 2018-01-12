using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using My.Domain;
namespace My.Core
{


    public static class TreeNodeExtensions
    {
        public static List<T> ToTreeNode<T>(this IEnumerable<T> source) where T : Entity, ITreeNode<T>, new()
        {
            var list = new List<T>();
            var sonNodes = source.OrderBy(c => c.Sort).ThenBy(c => c.Sort);
            var dict = new Dictionary<Guid, T>();

            foreach (var entity in sonNodes)
            {

                T parentNode;

                if (dict.TryGetValue(entity.ParentId, out parentNode))
                {
                    if (parentNode.Childen == null)
                        parentNode.Childen = new List<T>();
                    parentNode.Childen.Add(entity);
                }
                else
                    list.Add(entity);



                dict[(Guid)entity.GetId()] = entity;

            }
            return list;
        }

    }
}
