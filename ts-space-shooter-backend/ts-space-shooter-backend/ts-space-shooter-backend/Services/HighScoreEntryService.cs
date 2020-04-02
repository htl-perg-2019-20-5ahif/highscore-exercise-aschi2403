using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ts_space_shooter_backend.Model;

namespace ts_space_shooter_backend.Services
{
    public class HighScoreEntryService
    {
        private HighScoreContext _context;

        public HighScoreEntryService(HighScoreContext context)
        {
            _context = context;
        }

        public async Task<List<HighScoreEntry>> GetAllItemsAsync()
        {
            var list = await _context.HighScoreEntries.ToListAsync();
            return list.OrderByDescending(p => p.Points).ToList();
        }

        public async Task<HighScoreEntry> GetHighScoreEntryAsync(int id)
        {
            var highScoreEntry = await _context.HighScoreEntries.FindAsync(id);

            return highScoreEntry;
        }

        public async Task AddHighScoreEntryAsync(HighScoreEntry highScoreEntry)
        {
            if (_context.HighScoreEntries.ToList().Count >= 10)
            {
                var orderedList = _context.HighScoreEntries.OrderBy(p => p.Points);

                _context.Remove(orderedList.Last());

                _context.Add(highScoreEntry);
            }
            else
            {
                _context.HighScoreEntries.Add(highScoreEntry);
            }

            await _context.SaveChangesAsync();
        }

    }
}
