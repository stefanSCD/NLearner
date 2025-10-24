namespace NLearner.Domain.Entities
{
    public class Deck
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public Guid ProjectId { get; set; }
    }
}
