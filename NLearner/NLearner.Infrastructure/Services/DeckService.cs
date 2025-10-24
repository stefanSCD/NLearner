using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;
using NLearner.DTO.Cards;
using NLearner.DTO.Decks;
using NLearner.Infrastructure.Persistence;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Service
{
    public class DeckService : IDeckService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IProjectService _projectService;
        public DeckService(IDbContextFactory<AppDbContext> contextFactory, IProjectService projectService)
        {
            _contextFactory = contextFactory;
            _projectService = projectService;
        }
        public async Task<Card?> AddCardToDeckAsync(CreateCardDto dto, Guid deckId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync(); 
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
            await using var _context = await _contextFactory.CreateDbContextAsync();
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
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var (card, project) = await GetOwnedCardAsync(cardId, userId);
            if (card is null || project is null)
                return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDeckAsync(Guid deckId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var deck = await _context.Decks.FirstOrDefaultAsync(x => x.Id == deckId);
            
            if (deck is null)
                return false;

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return false;

            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UpdateCardDto?> GetCardForUpdateAsync(Guid cardId, string userId)
        {
            var (card, project) = await GetOwnedCardAsync(cardId, userId);
            if (card is null || project is null)
                return null;

            var model = new UpdateCardDto()
            {
                Id = card.Id,
                Front = card.Front,
                Back = card.Back,
                DeckId = card.DeckId
            };

            return model;
        }

        public async Task<IEnumerable<Deck>> GetDecksByProjectIdAsync(Guid projectId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var project = await _projectService.GetOwnedProjectAsync(projectId, userId);
            if(project is null) return Enumerable.Empty<Deck>();
            return await _context.Decks.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public async Task<StudyDeckViewModel?> GetDeckWithCardsViewModelAsync(Guid deckId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var deck = await _context.Decks.Include(d => d.Cards).FirstOrDefaultAsync(x => x.Id == deckId);
            if(deck is null) return null;
            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return null;

            var vm = new StudyDeckViewModel()
            {
                DeckId = deck.Id,
                ProjectId = project.Id,
                DeckName = deck.Name,
                Cards = deck.Cards.ToList()
            };

            return vm;
        }

        public async Task<IEnumerable<Deck>> GetRecentDecksByUserIdAsync(string userId, int count)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var userProjectIds = _context.Projects.Where(p => p.UserId == userId).Select(p => p.Id);
            return await _context.Decks
                .Where(deck => userProjectIds.Contains(deck.ProjectId))
                .OrderByDescending(deck => deck.UpdatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Card?> UpdateCardAsync(UpdateCardDto dto, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var (card, project) = await GetOwnedCardAsync(dto.Id, userId);
            if(card is null || project is null)
                return null;
            card.Front = dto.Front;
            card.Back = dto.Back;
            card.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return card;
        }

        private async Task<(Card? card, Project? project)> GetOwnedCardAsync(Guid cardId ,string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == cardId);
            if (card is null) return (null, null);

            var deck = await _context.Decks.FindAsync(card.DeckId);
            if (deck is null) return (null, null);

            var project = await _projectService.GetOwnedProjectAsync(deck.ProjectId, userId);
            if (project is null) return (null, null);

            return (card, project);
        }
    }
}
