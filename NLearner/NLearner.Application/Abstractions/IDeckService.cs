using NLearner.Domain.Entities;
using NLearner.DTO.Cards;
using NLearner.DTO.Decks;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Service
{
    public interface IDeckService
    {
        Task<Deck?> CreateDeckAsync(CreateDeckDto dto, Guid projectId, string userId);
        Task<IEnumerable<Deck>> GetDecksByProjectIdAsync(Guid projectId, string userId);
        Task<StudyDeckViewModel?> GetDeckWithCardsViewModelAsync(Guid deckId, string userId);
        Task<bool> DeleteDeckAsync(Guid deckId, string userId);
        Task<Card?> AddCardToDeckAsync(CreateCardDto dto, Guid deckId, string userId);
        Task<Card?> UpdateCardAsync(UpdateCardDto dto, string userId);
        Task<bool> DeleteCardAsync(Guid cardId, string userId);
        Task<UpdateCardDto?> GetCardForUpdateAsync(Guid cardId, string userId);
        Task<IEnumerable<Deck>> GetRecentDecksByUserIdAsync(string userId, int count);
    }
}
