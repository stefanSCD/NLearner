using Microsoft.AspNetCore.Identity;

namespace NLearner.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public required string UserId {  get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Deck> Decks { get; set; } = new List<Deck>();
    }
}
