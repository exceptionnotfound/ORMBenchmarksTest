namespace ORMBenchmarksTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public DateTime FoundingDate { get; set; }

        public int SportId { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
