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
    public class CustomerAddressController : ControllerBase, iController<CustomerAddress>
    {
        private readonly WebRestOracleContext _context;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customeraddress = await _context.CustomerAddresses.FindAsync(id);
            if (customeraddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customeraddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerAddress>> Get(string id)
        {
            var address = await _context.CustomerAddresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> Post(CustomerAddress customeraddress)
        {
            _context.CustomerAddresses.Add(customeraddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerAddress", new { id = customeraddress.CustomerAddressId }, customeraddress);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CustomerAddress customeraddress)
        {
            {
            if (id != customeraddress.CustomerAddressId)
            {
                return BadRequest();
            }
            _context.CustomerAddresses.Update(customeraddress);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
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

        private bool CustomerAddressExists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}