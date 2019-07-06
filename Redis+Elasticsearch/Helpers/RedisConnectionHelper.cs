using StackExchange.Redis;
using System;

namespace Redis_Elasticsearch.Helpers
{
    public static class RedisConnectionHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect("localhost"));

        public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
