using System.ComponentModel.DataAnnotations;

namespace NLearner.DTO.Projects
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Project name is mandatory.")]
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
