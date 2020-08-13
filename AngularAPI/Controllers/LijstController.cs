using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularAPI.Models;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LijstController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LijstController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Lijst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lijst>>> GetLijsten()
        {
            return await _context.Lijsten.ToListAsync();
        }

        // GET: api/Lijst/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lijst>> GetLijst(long id)
        {
            var lijst = await _context.Lijsten
                .Include(x => x.Items)
                .ThenInclude(x => x.stemmen)
                .Where(x => x.LijstID == id)
                .FirstAsync();

            if (lijst == null)
            {
                return NotFound();
            }

            return lijst;
        }

        [HttpGet("getWhereUserVoted/{id}")]
        public async Task<IEnumerable<Lijst>> GetLijstWhereUserVoted(long id)
        {
            var stemmen = await _context.Stemmen.Where(x => x.GebruikerID == id).ToListAsync();
            List<Lijst> lijsten = new List<Lijst>();
            List<Item> items = new List<Item>();

            foreach(Stem stem in stemmen)
            {
                var item = await _context.Items
                    .Where(x => x.ItemID == stem.ItemID)
                    .FirstAsync();
                var lijst = await _context.Lijsten
                    .Where(x => x.LijstID == item.LijstID)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.stemmen)
                    .FirstAsync();
                lijsten.Add(lijst);
            }

            return lijsten;
        }

        [HttpGet("getWhereUser/{id}")]
        public async Task<IEnumerable<Lijst>> GetLijstWhereUserId(int id)
        {
            var lijsten = await _context.Lijsten
                .Where(x => x.GebruikerID == id).ToListAsync();

            return lijsten;
        }

        // PUT: api/Lijst/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLijst(long id, Lijst lijst)
        {
            if (id != lijst.LijstID)
            {
                return BadRequest();
            }

            _context.Entry(lijst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LijstExists(id))
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

        // POST: api/Lijst
        [HttpPost]
        public async Task<ActionResult<Lijst>> PostLijst(Lijst lijst)
        {
            _context.Lijsten.Add(lijst);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLijst", new { id = lijst.LijstID }, lijst);
        }

        // DELETE: api/Lijst/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lijst>> DeleteLijst(long id)
        {
            var lijst = await _context.Lijsten.FindAsync(id);
            if (lijst == null)
            {
                return NotFound();
            }

            _context.Lijsten.Remove(lijst);
            await _context.SaveChangesAsync();

            return lijst;
        }

        private bool LijstExists(long id)
        {
            return _context.Lijsten.Any(e => e.LijstID == id);
        }
    }
}
