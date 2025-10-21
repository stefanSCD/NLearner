using Microsoft.AspNetCore.Mvc;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;
using System.Security.Claims;

namespace NLearner.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly INoteService _noteService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProjectService _projectService;
        private readonly IDeckService _deckService;
        public SidebarViewComponent(INoteService noteService, IHttpContextAccessor contextAccessor, IProjectService projectService, IDeckService deckService)
        {
            _noteService = noteService;
            _contextAccessor = contextAccessor;
            _projectService = projectService;
            _deckService = deckService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxItems = 20)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Not connected.");
            var notes = await _noteService.GetNotesByUserId(userId);
            var projects = await _projectService.GetProjectsByUserIdAsync(userId);
            var orderedNotes = notes.OrderByDescending(x => x.UpdatedDate).Take(maxItems).Select(n => new SidebarNoteItem
            {
                Id = n.Id.ToString(),
                Title = string.IsNullOrWhiteSpace(n.Title) ? "(untitled)" : n.Title,
                UpdatedAt = n.UpdatedDate
            }).ToList();
            var orderedProjects = projects.OrderByDescending(x => x.UpdatedDate).Take(maxItems).Select(p => new SidebarProjectItem
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                UpdatedAt = p.UpdatedDate

            }).ToList();
            var recentDecks = await _deckService.GetRecentDecksByUserIdAsync(userId, 20);
            var deckItems = recentDecks.Select(d => new SidebarDeckItem
            {
                Id = d.Id,
                ProjectId = d.ProjectId,
                Name = d.Name
            }).ToList();
            var vm = new SidebarViewModel { 
                UserDisplayName = "User",
                Notes = orderedNotes,
                Projects = orderedProjects,
                TotalNotes = orderedNotes.Count(),
                TotalProjects = orderedProjects.Count(),
                Decks = deckItems
            };

            return View("default", vm);
        }
    }
}
