using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.Api.Entities
{
    public class VoteHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("UserName")]
        public Voter? Voter { get; set; }
        public string UserName { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public long ItemId { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;
    }
}
