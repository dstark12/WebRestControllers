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
    public class OrderStatusController : ControllerBase, iController<OrderStatus>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var orderstatus = await _context.OrderStatuses.FindAsync(id);
            if (orderstatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(orderstatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderStatus>> Get(string id)
        {
            var address = await _context.OrderStatuses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> Get()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatus orderstatus)
        {
            _context.OrderStatuses.Add(orderstatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStatus", new { id = orderstatus.OrderStatusId }, orderstatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderStatus orderstatus)
        {
            {
            if (id != orderstatus.OrderStatusId)
            {
                return BadRequest();
            }
            _context.OrderStatuses.Update(orderstatus);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        private bool OrderStatusExists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}