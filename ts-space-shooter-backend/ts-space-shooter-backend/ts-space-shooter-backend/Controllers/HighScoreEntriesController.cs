using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ts_space_shooter_backend.Model;
using ts_space_shooter_backend.Services;

namespace ts_space_shooter_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighScoreEntriesController : ControllerBase
    {
        private readonly HighScoreContext _context;
        private readonly HighScoreEntryService _service;

        public HighScoreEntriesController(HighScoreContext context)
        {
            _context = context;
            _service = new HighScoreEntryService(_context);
        }

        // GET: api/HighScoreEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HighScoreEntry>>> GetHighScoreEntries()
        {
            return Ok(await _service.GetAllItemsAsync());
        }

        // GET: api/HighScoreEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HighScoreEntry>> GetHighScoreEntry(int id)
        {
            var highScoreEntry = await _service.GetHighScoreEntryAsync(id);

            if (highScoreEntry == null)
            {
                return NotFound();
            }

            return highScoreEntry;
        }

        // POST: api/HighScoreEntries
        [HttpPost]
        public async Task<ActionResult<HighScoreEntry>> PostHighScoreEntry(HighScoreEntry highScoreEntry)
        {
            await _service.AddHighScoreEntryAsync(highScoreEntry);

            return CreatedAtAction("GetHighScoreEntry", new { id = highScoreEntry.HighScoreEntryId }, highScoreEntry);
        }
    }
}
