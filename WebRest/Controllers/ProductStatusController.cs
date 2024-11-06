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
    public class ProductStatusController : ControllerBase, iController<ProductStatus>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var productstatus = await _context.ProductStatuses.FindAsync(id);
            if (productstatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(productstatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var address = await _context.ProductStatuses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus productstatus)
        {
            _context.ProductStatuses.Add(productstatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductStatus", new { id = productstatus.ProductStatusId }, productstatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus productstatus)
        {
            {
            if (id != productstatus.ProductStatusId)
            {
                return BadRequest();
            }
            _context.ProductStatuses.Update(productstatus);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatusExists(id))
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

        private bool ProductStatusExists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}