using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Redis_Elasticsearch.Models;
using StackExchange.Redis;
using Redis_Elasticsearch.Infrastructure;

namespace Redis_Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRedisConnectivity redis;

        public StudentsController(IRedisConnectivity redis)
        {
            this.redis = redis;
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
            IDatabase db = redis.Multiplexer.GetDatabase();

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
            IDatabase db = redis.Multiplexer.GetDatabase();

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
