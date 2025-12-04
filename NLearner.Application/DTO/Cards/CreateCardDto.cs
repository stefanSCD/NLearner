using System.ComponentModel.DataAnnotations;

namespace NLearner.DTO.Cards
{
    public class CreateCardDto
    {
        [Required(ErrorMessage = "Front of the card is mandatory.")]
        public required string Front {  get; set; }
        [Required(ErrorMessage = "Back of the card is mandatory.")]
        public required string Back { get; set; }
    }
}
