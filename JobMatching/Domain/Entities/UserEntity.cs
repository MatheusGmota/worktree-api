using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobMatching.Domain.Entities
{
    [Table("USER_table")]
    [Index(nameof(Name), IsUnique = true)]
    public class UserEntity
    {
        [Key]
        [Column("ID_USER")]
        public int Id { get; set; }

        [Required]
        [Column("USER_NAME")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("user_password")]
        public string Password { get; set; }

        [Required]
        [Column("USER_ROLE")]
        public string Role { get; set; }

        [Column("user_description")]
        public string? Description { get; set; }
        public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    }
}

