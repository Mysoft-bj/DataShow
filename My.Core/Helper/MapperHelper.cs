using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using My.Core;
using System.Linq.Expressions;
using System.Data;
using EmitMapper.Utils;
using System.Reflection;

namespace My.Core
{
    public static class Mapper
    {
        private static ObjectsMapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            var config = new DefaultMapConfig();
            config.NullSubstitution<int?, int>((o) => 0);
            config.NullSubstitution<bool?, bool>((o) => false);
            var mapper = ObjectMapperManager.DefaultInstance
                  .GetMapper<TFrom, TTo>(config);
         
            return mapper;
        }
        public static List<Destination> MapTo<Source, Destination>(this IEnumerable<Source> source)
        {

            var mapper = GetMapper<Source, Destination>();
            return (from s in source select mapper.Map(s)).ToList();
        }

      

        public static Destination Map<Source, Destination>(Source source, Destination to)
        {

            return GetMapper<Source, Destination>().Map(source, to);
        }
        public static Destination MapTo<Destination>(this object source)  
            where Destination : class,  new()
        {
            if (source == null)
                return default(Destination);
             var   to = new Destination();
             MethodInfo mapToInner = typeof(Mapper).GetMethod("Map");
             mapToInner = mapToInner.MakeGenericMethod(source.GetType(), typeof(Destination) );
             object[] args = { source, to };
             return mapToInner.Invoke(null, args) as Destination;
             
        }
        
    }
}
