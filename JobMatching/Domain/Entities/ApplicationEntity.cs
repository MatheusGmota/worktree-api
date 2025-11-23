using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobMatching.Domain.Entities
{
    [Table("APPLICATION")]
    public class ApplicationEntity
    {
        [Key]
        [Column("ID_APPLICATION")]
        public int Id { get; set; }

        [Column("ID_JOB")]
        public int JobId { get; set; }

        public virtual JobEntity Job { get; set; }

        [Column("candidate_id")]
        public int CandidateId { get; set; }

        public virtual UserEntity Candidate { get; set; }

        public string CoverLetter = string.Empty;

        [Column("APP_STATUS")]
        public string Status { get; set; } = "PENDING";
    }
}
