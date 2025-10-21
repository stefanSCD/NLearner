using System.ComponentModel.DataAnnotations;

namespace NLearner.DTO.Projects
{
    public class UpdateProjectDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is mandatory.")]
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
