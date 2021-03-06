using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystem.Api.Validations.ValidationAttributes;

namespace VotingSystem.Api.Entities
{
    public class Voter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Email]
        public string? Email { get; set; }
    }
}
