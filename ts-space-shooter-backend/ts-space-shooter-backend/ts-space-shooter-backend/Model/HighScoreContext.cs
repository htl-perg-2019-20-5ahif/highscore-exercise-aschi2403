using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ts_space_shooter_backend.Model
{
    public class HighScoreContext: DbContext
    {
        public DbSet<HighScoreEntry> HighScoreEntries { get; set; }

        public HighScoreContext(DbContextOptions<HighScoreContext> dbContextOptions): base(dbContextOptions)
        {
          
        }
    }
}
