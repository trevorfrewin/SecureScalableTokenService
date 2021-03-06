﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SSTS.Library.ConfigurationManagement;

namespace SSTS.Api.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfigurationManagementSource ConfigurationManagementSource { get; private set;}

        public ValuesController(IConfigurationManagementSource configurationManagementSource)
        {
            this.ConfigurationManagementSource = configurationManagementSource;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var configurationManagement = this.ConfigurationManagementSource.Load("SSTS.First.One");
            var reload = this.ConfigurationManagementSource.Load("SSTS.First.One");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
