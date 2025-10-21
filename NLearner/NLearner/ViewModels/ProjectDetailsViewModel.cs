using NLearner.Domain.Entities;

namespace NLearner.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
        public IEnumerable<Deck> Decks { get; set; } = new List<Deck>();
    }
}
