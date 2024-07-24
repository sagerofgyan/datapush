using datacapture.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace datacapture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly Datacontext _context;

        public ValuesController(Datacontext context)
        {
            _context = context;
        }
       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetData")]
        public async Task<ActionResult<IEnumerable<Data>>> GetData()
        {
            return await _context.Datas.ToListAsync();
          
        }

             

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("PostData")]
        public async Task<ActionResult<Data>> PostData(Data data)
        {
            _context.Datas.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDatabyid", new { id = data.id }, data);
           // return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("DeleteData/{id?}")]       
        public async Task<ActionResult<Data>> GetDatabyid(int id)
        {
            var data = await _context.Datas.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        [Authorize(Roles = "Admin")]        
        [HttpDelete]
        [Route("DeleteData/{id?}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var data = await _context.Datas.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _context.Datas.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
