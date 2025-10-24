using NLearner.Domain.Entities;

namespace NLearner.DTO.Notes
{
    public class NoteCreateRequest
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool isDeleted { get; set; }
        public required string UserId { get; set; }
        public Guid ProjectId { get; set; }

        public Note ToNote() => new Note()
        {
            Id = Id,
            Title = Title,
            Content = Content,
            CreatedDate = CreatedDate,
            UpdatedDate = DateTime.UtcNow,
            isDeleted = isDeleted,
            UserId = UserId,
            ProjectId = ProjectId
        };
    }
}
