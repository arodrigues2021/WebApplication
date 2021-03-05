using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Data;
using Domain;
using Newtonsoft.Json;

namespace cache
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly Config _config;

        private IDistributedCache redisCache;

        private DistributedCacheEntryOptions options;

        private static readonly Encoding encoding = Encoding.UTF8;

        public object MessagePackSerializer { get; private set; }

        public CacheService(IDistributedCache memoryCache, Config config)
        {
            this.redisCache = memoryCache;

            options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(config.redisCache.ExpirationMin))
            };

        }


        public bool IsExists(string key)
        {
            try
            {
                var response = this.redisCache.Get(key);

                return (response == null ? false : true);


            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }


        }


        public object Get<TEntity>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var elems = this.redisCache.Get(key);
            if (elems == null)
            {
                return null;
            }

            return (TEntity)(Deserialize<TEntity>(elems));

        }

        public void Set<TEntity>(string key, object value)
        {

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.redisCache.Set(key, Serialize<TEntity>(value), options);

        }

        private byte[] Serialize<TEntity>(object item)
        {
            //var type = item.GetType();
            var jsonString = JsonConvert.SerializeObject(item, typeof(TEntity), new JsonSerializerSettings());
            return encoding.GetBytes(jsonString);
        }

        private object Deserialize<TEntity>(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return JsonConvert.DeserializeObject(jsonString, typeof(TEntity));
        }


    }
}
