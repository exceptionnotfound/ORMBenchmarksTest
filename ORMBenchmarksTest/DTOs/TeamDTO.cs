using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DTOs
{
    [DebuggerDisplay("Id = {Id}")]
    public class TeamDTO
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public string Name { get; set; }
        public DateTime FoundingDate { get; set; }

        public List<PlayerDTO> Players { get; set; }
    }
}
