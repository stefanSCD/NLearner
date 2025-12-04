using Microsoft.AspNetCore.Http;
using NLearner.Application.Abstractions;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Services
{
    public class SidebarService : ISidebarService
    {
        private readonly INoteService _noteService;
        private readonly IProjectService _projectService;
        private readonly IDeckService _deckService;
        public SidebarService(INoteService noteService, IProjectService projectService, IDeckService deckService)
        {
            _deckService = deckService;
            _noteService = noteService;
            _projectService = projectService;
        }
        public async Task<SidebarViewModel> GetSidebarDataAsync(string userId)
        {
            var projectsTask = _projectService.GetProjectsByUserIdAsync(userId);
            var notesTask = _noteService.GetNotesByUserId(userId);
            var decksTask = _deckService.GetRecentDecksByUserIdAsync(userId, 10);
            await Task.WhenAll(projectsTask, notesTask, decksTask);

            var projects = (await projectsTask)
                .Select(x => new SidebarProjectItem { Id = x.Id, Name = x.Name, UpdatedAt = x.UpdatedDate })
                .ToList();

            var notes = (await notesTask)
                .Select(x => new SidebarNoteItem { Id = x.Id, Title = x.Title, UpdatedAt = x.UpdatedDate })
                .ToList();

            var decks = (await decksTask)
                .Select(x => new SidebarDeckItem { Id = x.Id, Name = x.Name, ProjectId = x.ProjectId})
                .ToList();

            return new SidebarViewModel
            {
                Projects = projects,
                Notes = notes,
                Decks = decks,
                TotalNotes = notes.Count(),
                TotalProjects = projects.Count()
            };
        }
    }
}
