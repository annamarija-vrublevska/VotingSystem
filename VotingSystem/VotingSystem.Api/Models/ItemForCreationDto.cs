using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Api.Models
{
    public class ItemForCreationDto
    {
        [Required(ErrorMessage = "You should provide an item name.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should provide an expire date of an item.")]
        public DateTime ExpireDate { get; set; }
    }
}
