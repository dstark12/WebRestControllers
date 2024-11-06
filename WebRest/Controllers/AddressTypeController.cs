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
    public class AddressTypeController : ControllerBase, iController<AddressType>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var addresstype = await _context.AddressTypes.FindAsync(id);
            if (addresstype == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(addresstype);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var address = await _context.AddressTypes.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AddressType>> Post(AddressType addresstype)
        {
            _context.AddressTypes.Add(addresstype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = addresstype.AddressTypeId }, addresstype);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressType addresstype)
        {
            {
            if (id != addresstype.AddressTypeId)
            {
                return BadRequest();
            }
            _context.AddressTypes.Update(addresstype);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        private bool AddressTypeExists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}