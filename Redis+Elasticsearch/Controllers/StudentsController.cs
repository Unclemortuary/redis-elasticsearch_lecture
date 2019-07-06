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
            var serializedStudent = distributedCache.GetString(id.ToString());
            if (serializedStudent != null)
            {
                var deserializedStudent = JsonConvert.DeserializeObject<Student>(serializedStudent);
                return deserializedStudent;
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
            if (distributedCache.Get(newId.ToString()) == null)
            {
                var newStudent = new Student(newId, student.Name, student.Surname);
                var serializedStudent = JsonConvert.SerializeObject(newStudent);
                distributedCache.SetString(newId.ToString(), serializedStudent);
            }
            var arr = distributedCache.Get(newId.ToString());
            return newId;
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
