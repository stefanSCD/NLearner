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
        public DateTime NextReviewDate { get; set; } = DateTime.UtcNow;
        public int IntervalDays { get; set; } = 0;
        public float EaseFactor { get; set; } = 2.5f;
        public int Repetitions { get; set; } = 0;
    }
}
