using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NServiceBus.Common.Messages;

namespace NServiceBus.Client.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEndpointInstance _endpointInstance;

        public ValuesController(IEndpointInstance endpointInstance)
        {
            _endpointInstance = endpointInstance;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new RequestMessage()
            {
                Message = "This message sent from Client Side"
            };

            var response = await _endpointInstance.Request<ResponseMessage>(request);


            return Ok(response);
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
    }
}
