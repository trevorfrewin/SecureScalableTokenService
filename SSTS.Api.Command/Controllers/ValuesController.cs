﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSTS.Library.ConfigurationManagement;

namespace SSTS.Api.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfigurationManagementSource ConfigurationManagementSource { get; private set; }

        public ILogger<ValuesController> Logger { get; private set; }

        public ValuesController(IConfigurationManagementSource configurationManagementSource, ILogger<ValuesController> logger)
        {
            this.ConfigurationManagementSource = configurationManagementSource;
            this.Logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var scope = Request.HttpContext.TraceIdentifier;
            using (this.Logger.BeginScope(scope))
            {
                this.Logger.LogWarning("[Scope:{0}] Some details of the Warning that is being logged", scope);

                var innerException = new NullReferenceException("FieldNameDodgy");
                this.Logger.LogError(new ArgumentException("fake exception", innerException), "[Scope:{0}] Extra detail", scope);

                var configurationManagement = this.ConfigurationManagementSource.Load("SSTS.First.One");
                var reload = this.ConfigurationManagementSource.Load("SSTS.First.One");
                return new string[] { "value1", "value2" };
            }
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
