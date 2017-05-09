using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace CoreWebAPI.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {
        private const string EndpointUri = "https://ursenix.documents.azure.com:443/";
        private const string PrimaryKey = "2ihOfQU6p10WD84FzxlvljrQFzA5SRJV2qlCTp0OJ8gnD8CbB03C2IBWBTHwPqHV46GccRbibLNKhQ7DWpzeBA==";
        private DocumentClient client;

        // GET api/values
        [HttpGet]
		[Route("test")]
        public IEnumerable<string> Get()
        {
            return new string[] { "senthil", "kumaran" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("getDocumentDBRecords")]
        public IEnumerable<Person> GetPersons()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Person> familyQuery = this.client.CreateDocumentQuery<Person>(
                UriFactory.CreateDocumentCollectionUri("sampledatabase", "samplecollection"), queryOptions)
                .Where(f => f.age > 3);

            return familyQuery.AsEnumerable();
        
        }
    }
}
