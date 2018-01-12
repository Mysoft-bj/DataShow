
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using My.Core;
using StackExchange.Redis;
using Castle.MicroKernel.Registration;


namespace My.Core
{
    public static class RedisHelper
    {
        public static void Init()
        {
            var container = IocManager.IocContainer;
            var si = IocManager.Resolve<SiteInfo>();
            //注册redis
            if (!container.Kernel.HasComponent(typeof(ConnectionMultiplexer)))
                container.Register(Component.For<ConnectionMultiplexer>().UsingFactoryMethod((ioc) =>
                {
                    return ConnectionMultiplexer.Connect(si.RedisHost);
                }).LifestyleSingleton());


            if (!container.Kernel.HasComponent(typeof(IDatabase)))
                container.Register(Component.For<IDatabase>().UsingFactoryMethod((ioc) =>
                    ioc.Resolve<ConnectionMultiplexer>().GetDatabase())
                    .LifestylePerWebRequest());
            ConnectionMultiplexer.Connect(si.RedisHost + ",AllowAdmin=true").GetServer(si.RedisHost).FlushDatabase();
        }

        public static T GetEntity<T>(object key) where T : IEntity
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            var hashName = typeof(T).Name;
            string entityByte = redis.HashGet(hashName, key.ToString());
            if (entityByte.IsNullOrWhiteSpace()) return default(T);
            return entityByte.ToObject<T>();
        }

        public static void SetEntity<T>(T entity) where T : IEntity {
            var key = entity.GetValue("Id");
            var typeName = entity.GetType().Name;
            IDatabase redis = IocManager.Resolve<IDatabase>();
            var hashName = typeof(T).Name;          
            redis.HashSetAsync(hashName, key.ToString(), entity.ToJson());
        }
        public static void Remove<T>(object key) 
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            var hashName = typeof(T).Name;
            redis.HashDelete(hashName, key.ToString());
        }
        public static void RemoveAll<T>() where T : class
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            var hashName = typeof(T).Name;
            redis.KeyDelete(hashName);
        }
        //public static T GetOrSet<TKEY, T>(TKEY key, Func<TKEY, T> getFunc) where T : class
        //{         
        //    var entity = Get<T>(key);
        //    IDatabase redis = IocManager.Resolve<IDatabase>();
        //    if (entity == null)
        //    {
             
        //    }
        //    return entity;
        //}
        public static void Set(string key, string value, int secondExpire)
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            var expire = new TimeSpan(0, 0, secondExpire);
            redis.StringSet(key, value, expire);
        }
        public static string Get(string key)
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            return redis.StringGet(key);


        }

        public static List<T> CacheList<T>(string keyPrefix, Guid key, Func<Guid, List<T>> getListFunc) where T : Entity
        {
            List<T> list = null;
            var cacheKey = keyPrefix+":" + key.ToString();
            IDatabase redis = IocManager.Resolve<IDatabase>();

            string entityByte = redis.StringGet(cacheKey);
            if (entityByte.IsNotNull())
                list = entityByte.ToObjectList<T>();
            if (list == null)
            {
                list = getListFunc(key);
                redis.StringSet(cacheKey, list.ToJson());
            }
            return list;

        }
        public static void CacheList(string keyPrefix,Guid key)
        {
            IDatabase redis = IocManager.Resolve<IDatabase>();
            redis.KeyDeleteAsync(keyPrefix + ":" + key.ToString());

        }


    }
}
