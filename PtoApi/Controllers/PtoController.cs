#define Primary
#if Primary
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PtoApi.Models;

#region PtoController
namespace PtoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PtoController : ControllerBase
    {
        private readonly PtoContext _context;
        #endregion

        public PtoController(PtoContext context)
        {
            _context = context;

            if (_context.PtoItems.Count() == 0)
            {
                // Create a new PtoItem if collection is empty,
                // which means you can't delete all PtoItems.
                _context.PtoItems.Add(new PtoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        // GET: api/Pto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PtoItem>>> GetPtoItems()
        {
            return await _context.PtoItems.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/Pto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PtoItem>> GetPtoItem(long id)
        {
            var PtoItem = await _context.PtoItems.FindAsync(id);

            if (PtoItem == null)
            {
                return NotFound();
            }

            return PtoItem;
        }
        #endregion
        #endregion

        #region snippet_Create
        // POST: api/Pto
        [HttpPost]
        public async Task<ActionResult<PtoItem>> PostPtoItem(PtoItem item)
        {
            _context.PtoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPtoItem), new { id = item.Id }, item);
        }
        #endregion

        #region snippet_Update
        // PUT: api/Pto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPtoItem(long id, PtoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Pto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePtoItem(long id)
        {
            var PtoItem = await _context.PtoItems.FindAsync(id);

            if (PtoItem == null)
            {
                return NotFound();
            }

            _context.PtoItems.Remove(PtoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
#endif