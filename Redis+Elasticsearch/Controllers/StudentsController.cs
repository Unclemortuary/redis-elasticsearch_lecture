using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Redis_Elasticsearch.Models;
using StackExchange.Redis;

namespace Redis_Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        public StudentsController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
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
            return memoryCache.Get<Student>(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Student student)
        {
            var random = new Random();
            int newId = random.Next();
            
            if (!memoryCache.TryGetValue(newId, out Student cachedStudent))
            {
                //compex calculations
                //...
                //...
                //...
                memoryCache.Set(newId, new Student(newId, student.Name, student.Surname));
            }
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
