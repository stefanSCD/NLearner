using NLearner.Domain.Domain.Enums;

namespace NLearner.Application.DTO.Cards
{
    public class ReviewDto
    {
        public Guid CardId { get; set; }
        public ReviewQuality Quality { get; set; }
    }
}
