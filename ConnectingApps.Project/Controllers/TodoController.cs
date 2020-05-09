using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectingApps.Project.Database;
using ConnectingApps.Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectingApps.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] TodoItem value)
        {
            var response = await _todoRepository.SaveItem(value);
            return Ok(response.Id);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
