namespace NLearner.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public required string Title {  get; set; }
        public required string Content {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool isDeleted {  get; set; }
        public required string UserId { get; set; }
        public Guid ProjectId {  get; set; }
    }
}
