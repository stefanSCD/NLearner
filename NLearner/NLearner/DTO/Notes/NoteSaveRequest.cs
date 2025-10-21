using NLearner.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NLearner.DTO.Notes
{
    public class NoteSaveRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string Content { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
