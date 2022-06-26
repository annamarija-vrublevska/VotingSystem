using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Api.Models
{
    public class ItemForUpdateDto
    {
        [Required(ErrorMessage = "You should provide an item name.")]
        [MaxLength(50)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "You should provide a state of item.")]
        //public string State { get; set; }

        [Required(ErrorMessage = "You should provide an expire date of an item.")]
        public DateTime ExpireDate { get; set; }
    }
}
