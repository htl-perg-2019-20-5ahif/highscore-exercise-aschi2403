using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ts_space_shooter_backend.Model
{
    public class HighScoreEntry
    {
        public int HighScoreEntryId { get; set; }
        public string PlayerInitials { get; set; }
        public int Points { get; set; }
    }
}
