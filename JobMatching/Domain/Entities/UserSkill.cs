using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public UserEntity User { get; set; }
    }
}