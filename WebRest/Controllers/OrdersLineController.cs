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
    public class OrdersLineController : ControllerBase, iController<OrdersLine>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ordersline = await _context.OrdersLines.FindAsync(id);
            if (ordersline == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(ordersline);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrdersLine>> Get(string id)
        {
            var address = await _context.OrdersLines.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> Get()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrdersLine>> Post(OrdersLine ordersline)
        {
            _context.OrdersLines.Add(ordersline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdersLine", new { id = ordersline.OrdersLineId }, ordersline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrdersLine ordersline)
        {
            {
            if (id != ordersline.OrdersLineId)
            {
                return BadRequest();
            }
            _context.OrdersLines.Update(ordersline);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersLineExists(id))
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

        private bool OrdersLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}