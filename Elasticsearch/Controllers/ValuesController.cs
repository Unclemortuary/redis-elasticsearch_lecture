using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Data;
using Elasticsearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json.Serialization;

namespace Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static Uri ElasticUrl = new Uri("http://localhost:9200");

        private static IElasticClient GetClient()
        {
            var settings = new ConnectionSettings(ElasticUrl)
                .DefaultFieldNameInferrer((name) => new SnakeCaseNamingStrategy().GetPropertyName(name, false))
                .DisableDirectStreaming()
                .PrettyJson()
                .DefaultIndex("students");

            return new ElasticClient(settings);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("createIndex")]
        public async Task<bool> CreateIndex()
        {
            var client = new ElasticClient(ElasticUrl);

            var indexDescriptor = new CreateIndexDescriptor("students");

            indexDescriptor.Mappings(ms => ms.Map<Student>(m => m.AutoMap()));

            var request = new CreateIndexRequest("students", indexDescriptor);

            var result = await client.CreateIndexAsync(request);

            return result.IsValid;
        }


        [HttpGet("createIndexWithNamingStrategyForFields")]
        public async Task<bool> CreateIndexWithNamingStrategy()
        {
            var settings = new ConnectionSettings(ElasticUrl)
                .DefaultFieldNameInferrer((name) => new SnakeCaseNamingStrategy().GetPropertyName(name, false))
                .DefaultIndex("students");

            var client = new ElasticClient(settings);

            var indexDescriptor = new CreateIndexDescriptor("students");

            indexDescriptor.Mappings(ms => ms.Map<Student>(m => m.AutoMap()));

            var request = new CreateIndexRequest("students", indexDescriptor);

            var result = await client.CreateIndexAsync(request);

            return result.IsValid;
        }

        [HttpGet("bulk")]
        public async Task<bool> UsingOfBulk()
        {
            var bulkDescriptor = new BulkDescriptor();

            foreach (var student in Students.GetStudents)
            {
                bulkDescriptor.Index<Student>(op => op.Document(student).Id(student.Id));
            }

            var result = await GetClient().BulkAsync(bulkDescriptor);

            return result.IsValid;
        }

        [HttpGet("term")]
        public async Task<IReadOnlyList<Student>> UsingOfTerm([FromQuery]int id)
        {
            var response = await GetClient().SearchAsync<Student>(s => s.Query(q => q.Term(t => t.Field("id").Value(id))));

            if (response.IsValid)
                return response.Hits.Select(h => h.Source).ToList();
            else
                return null;
        }

        [HttpPost("terms")]
        public async Task<IReadOnlyList<Student>> UsingOfTerms([FromBody]params int[] ids)
        {
            var request = new SearchRequest("students");

            var query = new TermsQuery
            {
                Field = new Field("id"),
                Terms = ids.Cast<object>()
            };

            request.Query = query;
            request.Size = 100;
            request.From = 0;

            var response = await GetClient().SearchAsync<Student>(request);

            if (response.IsValid)
                return response.Hits.Select(h => h.Source).ToList();
            else
                return null;
        }

        [HttpGet("bool")]
        public async Task<IReadOnlyList<Student>> UsingOfBool()
        {
            var request = new SearchRequest("students");

            var firstQuery = new TermQuery
            {
                Field = new Field(ToSnakeCase(nameof(Student.PreviousProjectIds))),
                Value = 1
            };

            var secondQuery = new TermQuery
            {
                Field = new Field(ToSnakeCase(nameof(Student.PreviousProjectIds))),
                Value = 3
            };

            //var filter = new ScriptQuery
            //{
            //    Lang = "painless",
            //    Source = "doc['previous_project_ids'].length == 2"
            //};

            QueryContainer baseQuery = firstQuery & secondQuery;

            request.Query = baseQuery;

            var response = await GetClient().SearchAsync<Student>(request);

            if (response.IsValid)
                return response.Hits.Select(h => h.Source).ToList();
            else
                return null;
        }

        [HttpGet("agg")]
        public async Task<int> UsingOfAggregations()
        {
            const string aggregationName = "agg";

            var request = new SearchRequest("students")
            {
                Query = new MatchAllQuery(),
                Aggregations = new AggregationDictionary(),
                From = 0,
                Size = 5
            };

            var aggregationFilter = new CardinalityAggregation(aggregationName, ToSnakeCase(nameof(Student.PersonalScore)));

            request.Aggregations.Add(aggregationName, aggregationFilter);

            var response = await GetClient().SearchAsync<Student>(request);

            if (response.IsValid)
            {
                var aggregate = response.Aggregations.Cardinality(aggregationName);
                if (aggregate != null)
                    return Convert.ToInt32(aggregate.Value.Value);
            }

            return -1;
        }

        private string ToSnakeCase(string name) => new SnakeCaseNamingStrategy().GetPropertyName(name, false);
    }
}
