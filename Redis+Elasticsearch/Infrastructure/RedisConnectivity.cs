using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redis_Elasticsearch.Infrastructure
{
    internal class RedisConnectivity : IRedisConnectivity
    {
        public ConnectionMultiplexer Multiplexer { get; } = ConnectionMultiplexer.Connect("localhost");
    }
}
