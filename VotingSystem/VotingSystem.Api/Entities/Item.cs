using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystem.Api.Enums;

namespace VotingSystem.Api.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long Volume { get; set; } = 0;
        [ForeignKey("UserName")]
        public Voter? Owner { get; set; }
        public string UserName { get; set; }
        public StateType State { get; set; } = StateType.New;
        public DateTime ExpireDate { get; set; }
    }
}
