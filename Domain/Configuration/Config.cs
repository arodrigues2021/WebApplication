using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Config
    {
        public ConnectionStrings connectionStrings { get; set; }
        public AppSettings appSettings { get; set; }
        public RedisCache redisCache { get; set; }

    }

    public class ConnectionStrings
    {
        public string Database { get; set; }
    }

    public class AppSettings
    {
        public string numdiascambio { get; set; }
        public string Secret { get; set; }
        public double dias { get; set; }
    }

    public class RedisCache
    {
        public int ExpirationMin { get; set; }
    }
}
