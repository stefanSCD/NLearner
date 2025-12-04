using NLearner.Domain.Entities;

namespace NLearner.ViewModels
{
    public class StudyDeckViewModel
    {
        public Guid DeckId { get; set; }
        public Guid ProjectId { get; set; }
        public string DeckName { get; set; }
        public List<Card> Cards { get; set; } = new();
    }
}
