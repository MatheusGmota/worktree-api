using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobMatching.Domain.Entities
{
    [Table("JOB")]
    public class JobEntity
    {
        [Key]
        [Column("ID_JOB")]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        [Column("JOB_LOCATION")]
        public string Location { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public string Salary { get; set; }

        public string Description { get; set; }
    }
}
