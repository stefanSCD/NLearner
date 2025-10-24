namespace NLearner.Domain.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public required string Front {  get; set; }
        public required string Back {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid DeckId { get; set; }
    }
}
