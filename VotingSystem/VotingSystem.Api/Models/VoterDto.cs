using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using VotingSystem.Api.Validations.ValidationAttributes;

namespace VotingSystem.Api.Models
{
    public class VoterDto
    {
        [Required(ErrorMessage = "You should provide a user name.")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You should provide a name.")]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(50)]
        [Email]
        public string? Email { get; set; }
    }
}
