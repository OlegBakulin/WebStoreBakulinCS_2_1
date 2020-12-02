using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStoreBakulin.ServiceHosting.Controllers
{
    [Route("api/[controller]")] //http://localhost:5001/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _value = Enumerable.Range(1, 35)
            .Select(i => $"Value {i}").ToList();
        // GET: api/<ValuesController>
       
        
        [HttpGet]
        public IEnumerable<string> Get() => _value;



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();

            if (id >= _value.Count)
                return NotFound();

            return _value[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _value.Add(value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest(); //400
            if (id >= _value.Count)
                return NotFound(); //404
            _value[id] = value;
            return Ok();
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest(); //400
            if (id >= _value.Count)
                return NotFound();

            _value.RemoveAt(id);
            return Ok();
        }
    }
}
