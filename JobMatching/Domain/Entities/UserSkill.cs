using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobMatching.Domain.Entities
{
    [Table("tb_user_skills")]
    public class UserSkill
    {
        [Key]
        public int Id { get; set; }

        [Column("skill")]
        public string SkillName { get; set; }

        [Column("ID_USER")]
        public int UserId { get; set; }

        public UserEntity User { get; set; }
    }
}