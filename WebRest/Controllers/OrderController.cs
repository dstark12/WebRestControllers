using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, iController<Order>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var address = await _context.Orders.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrdersId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Order order)
        {
            {
            if (id != order.OrdersId)
            {
                return BadRequest();
            }
            _context.Orders.Update(order);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.OrdersId == id);
        }
    }
}