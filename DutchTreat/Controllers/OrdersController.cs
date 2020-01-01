using System;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                var msg = "Failed to get orders";
                logger.LogError($"{msg}: {ex}");
                return BadRequest($"{msg}");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = repository.GetOrderById(id);

                if (order != null) return Ok(order);
                else return NotFound();
            }
            catch (Exception ex)
            {
                var msg = "Failed to get orders";
                logger.LogError($"{msg}: {ex}");
                return BadRequest($"{msg}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order model)
        {
            var msg = "Failed to save a new order";
            
            try
            {
                repository.AddEntity(model);
                if (repository.SaveAll())
                {
                    return CreatedAtAction(nameof(Get), new { id = model.Id}, model);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{msg}: {ex}");
            }

            return BadRequest($"{msg}");
        }
    }
}