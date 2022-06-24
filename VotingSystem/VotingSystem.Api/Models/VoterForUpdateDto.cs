using System.ComponentModel.DataAnnotations;
using VotingSystem.Api.Validations.ValidationAttributes;

namespace VotingSystem.Api.Models
{
    public class VoterForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Email]
        public string? Email { get; set; }
    }
}
