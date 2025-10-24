using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NLearner.DTO.Cards
{
    public class UpdateCardDto
    {
        [HiddenInput]
        [Required]
        public Guid Id { get; set; }

        [HiddenInput]
        [Required]
        public Guid DeckId { get; set; }

        [Required(ErrorMessage = "Front content is required.")]
        public required string Front { get; set; }

        [Required(ErrorMessage = "Back content is required.")]
        public required string Back { get; set; }
    }
}