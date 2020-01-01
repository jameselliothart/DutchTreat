using System;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
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
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            var msg = "Failed to save a new order";
            
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        Id          = model.OrderId,
                        OrderDate   = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                    };

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    repository.AddEntity(newOrder);
                    if (repository.SaveAll())
                    {
                        var vm = new OrderViewModel()
                        {
                            OrderId     = newOrder.Id,
                            OrderDate   = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber,
                        };
                        return CreatedAtAction(nameof(Get), new { id = vm.OrderId}, vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
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