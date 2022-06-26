namespace VotingSystem.Api.Models
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Volume { get; set; }
        public VoterDto Owner { get; set; }
        public string State { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
