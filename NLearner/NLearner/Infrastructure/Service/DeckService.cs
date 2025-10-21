using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;
using NLearner.DTO.Cards;
using NLearner.DTO.Decks;
using NLearner.Infrastructure.Persistence;

namespace NLearner.Infrastructure.Service
{
    public class DeckService : IDeckService
    {
        private readonly AppDbContext _context;
        private readonly IProjectService _projectService;
        public DeckService(AppDbContext context, IProjectService projectService)
        {
            _context = context;
            _projectService = projectService;
        }
        public async Task<Card?> AddCardToDeckAsync(CreateCardDto dto, Guid deckId, string userId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck is null) return null;

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return null;

            var card = new Card()
            {
                Id = Guid.NewGuid(),
                Back = dto.Back,
                Front = dto.Front,
                DeckId = deckId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Deck?> CreateDeckAsync(CreateDeckDto dto, Guid projectId, string userId)
        {
            var project = await _projectService.GetOwnedProjectAsync(projectId, userId);
            if (project is null)
                return null;

            var deck = new Deck()
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Cards = new List<Card>(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Name = dto.Name
            };

            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return deck;
        }

        public async Task<bool> DeleteCardAsync(Guid cardId, string userId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == cardId);
            if (card is null)
                return false;
            var deck = await _context.Decks.FindAsync(card.DeckId);
            if (deck is null) return false;

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDeckAsync(Guid deckId, string userId)
        {
            var deck = await _context.Decks.FirstOrDefaultAsync(x => x.Id == deckId);
            
            if (deck is null)
                return false;

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return false;

            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Card?> GetCardAsync(Guid cardId, string userId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if(card is null) return null;
            var deck = await _context.Decks.FindAsync(card.DeckId);
            if (deck is null) return null;
            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return null;

            return card;

        }

        public async Task<IEnumerable<Deck>> GetDecksByProjectIdAsync(Guid projectId, string userId)
        {
            var project = await _projectService.GetOwnedProjectAsync(projectId, userId);
            if(project is null) return Enumerable.Empty<Deck>();
            return await _context.Decks.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public async Task<Deck?> GetDeckWithCardsAsync(Guid deckId, string userId)
        {
            var deck = await _context.Decks.Include(d => d.Cards).FirstOrDefaultAsync(x => x.Id == deckId);
            if(deck is null) return null;
            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return null;
            return deck;
        }

        public async Task<IEnumerable<Deck>> GetRecentDecksByUserIdAsync(string userId, int count)
        {
            var userProjectIds = _context.Projects.Where(p => p.UserId == userId).Select(p => p.Id);
            return await _context.Decks
                .Where(deck => userProjectIds.Contains(deck.ProjectId))
                .OrderByDescending(deck => deck.UpdatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Card?> UpdateCardAsync(UpdateCardDto dto, string userId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if(card is null) return null;

            var deck = await _context.Decks.FindAsync(card.DeckId);
            if(deck is null) return null;

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return null;

            card.Front = dto.Front;
            card.Back = dto.Back;
            card.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return card;
        }
    }
}
