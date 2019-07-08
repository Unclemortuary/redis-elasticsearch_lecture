using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Redis_Elasticsearch.Models;
using StackExchange.Redis;
using Redis_Elasticsearch.Helpers;

namespace Redis_Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private static readonly ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost");

        public StudentsController(IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "Students" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            IDatabase db = RedisConnectionHelper.Connection.GetDatabase();

            var entries = db.HashGetAll(id.ToString());
            string name = null, surname = null;

            if (entries.Length > 0)
            {
                foreach (var item in entries)
                {
                    if (item.Name == "name")
                        name = item.Value;
                    else
                        surname = item.Value;
                }
                return new Student(id, name, surname);
            }
            else
                return null;
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Student student)
        {
            var random = new Random();
            int newId = random.Next();
            //compex calculations
            //...
            //...
            //...
            IDatabase db = RedisConnectionHelper.Connection.GetDatabase();

            if (db.HashKeys(newId.ToString()).Length == 0)
            {
                HashEntry[] studentHashEntries =
                {
                    new HashEntry("name", student.Name),
                    new HashEntry("surname", student.Surname)
                };
                db.HashSet(newId.ToString(), studentHashEntries);
                return newId;
            }
            else
                return 0;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
