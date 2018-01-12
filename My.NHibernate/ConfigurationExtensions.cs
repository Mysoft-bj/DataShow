

using System.Data;
using System.Data.Common;
using System.Text;

using NHibernate.Exceptions;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using System;
using FluentNHibernate.Mapping;

namespace My.NHibernate

{
  public static class ConfigurationExtensions {
      public static Configuration CacheEntity(this Configuration cfg,  Type type)
   
      {


          cfg.SetCacheConcurrencyStrategy(type.FullName,
                EntityCacheUsageParser.ToString( EntityCacheUsage.NonStrictReadWrite), "My.DomainModel");
         
          //foreach (var collection in ecc.Collections)
          //{
          //    configuration.SetCollectionCacheConcurrencyStrategy(collection.Key,
          //        EntityCacheUsageParser.ToString(collection.Value.Strategy), collection.Value.RegionName);
          //}
          return cfg;
      }
  }

  //public class testMap : ClassMap<My.Domain.User> {
  //    public testMap() {
  //        Id(o => o.Id).GeneratedBy.Assigned();
  //    }
  //}
}