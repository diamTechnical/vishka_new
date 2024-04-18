using GroceryStoreAPI.DataAccess;
using GroceryStoreAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/Consumer")]
    [ApiController]
    public class ConsumerController : Controller
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IDataAccessProvider _dataAccessProvider;

        public ConsumerController(ILogger<ConsumerController> logger, IDataAccessProvider dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }
//        {“task_name”: “Buy Grocery”, “tags”: [“household”, “weekly stuff”],
//“due_date”: "2024-04-09", “color”: “beige”,
//“assigned_to”: “bala”, “status”: “PENDING”}


    [HttpGet("GetConsumerRecord")]
        public IEnumerable<Consumer> Get()
        {
            return _dataAccessProvider.GetConsumerRecords();
        }

        [HttpPost("AddConsumerRecord")]
        public IActionResult Create([FromBody] Consumer Consumer)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                Consumer.id = Convert.ToInt32(obj);
                _dataAccessProvider.AddConsumerRecord(Consumer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetConsumerById/{id}")]
        public Consumer Details(int id)
        {
            return _dataAccessProvider.GetConsumerSingleRecord(id);
        }

        [HttpGet("GetConsumerSearch/{task_name}/{tag}/{status}")]
        public List<Consumer> Search(string task_name, string tag,  string status)
        {
            return _dataAccessProvider.GetConsumerSearch(task_name,tag,status);
        }

        [HttpPut("UpdateConsumerRecord")]
        public IActionResult Edit([FromBody] Consumer Consumer)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateConsumerRecord(Consumer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteConsumerRecord/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetConsumerSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteConsumerRecord(id);
            return Ok();
        }
    }
}
