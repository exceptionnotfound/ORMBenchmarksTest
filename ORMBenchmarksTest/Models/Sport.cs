namespace ORMBenchmarksTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Sport")]
    public partial class Sport
    {
        public Sport()
        {
            Teams = new HashSet<Team>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
