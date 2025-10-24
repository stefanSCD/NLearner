using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NLearner.DTO.Decks
{
    public class CreateDeckDto
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Deck name")]
        [MaxLength(100)]
        public required string Name { get; set; }
        [HiddenInput]
        [Required]
        public Guid ProjectId { get; set; }
    }
}
