using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLearner.Domain.Entities;
using NLearner.DTO.Notes;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;

namespace NLearner.Controllers
{
    [Authorize]
    [Route("projects/{projectId:guid}/notes")]
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly IProjectService _projectService;
        public NoteController(INoteService noteService, IProjectService projectService)
        {
            _noteService = noteService;
            _projectService = projectService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index(Guid projectId)
        {
            var project = await _projectService.GetOwnedProjectAsync(projectId, CurrentUserId);
            if (project == null) return NotFound();

            var notes = await _noteService.GetNotesByProjectIdAsync(projectId);
            return View(notes);
        }
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Guid projectId)
        {
            var projectExists = await _projectService.GetOwnedProjectAsync(projectId, CurrentUserId);
            if (projectExists is null) return Forbid();

            var noteId = await _noteService.CreateDraftAsync(CurrentUserId, projectId);
            return RedirectToAction(nameof(Edit), new { projectId, id = noteId });
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Edit(Guid projectId, Guid id)
        {
            var vm = await _noteService.GetOwnedNoteViewModelAsync(projectId, CurrentUserId);
            if (vm is null || vm.ProjectId != projectId)
            {
                return NotFound();
            }
            return View(vm);
        }
        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Guid projectId, NoteEditViewModel req)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", req);
            }
            var saveRequest = new NoteSaveRequest { Id = req.Id , Title = req.Title, Content = req.Content};
            var updatedNote = await _noteService.UpdateNoteAsync(saveRequest, CurrentUserId);
            TempData["IsSaved"] = "Saved";
            return RedirectToAction(nameof(Edit), new { projectId = updatedNote!.ProjectId, id = req.Id });

        }
        [HttpPost("{id:guid}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid projectId, Guid id)
        {
            var success = await _noteService.DeleteNoteAsync(id, CurrentUserId);

            if (!success)
            {
                return NotFound();
            }
            TempData["IsDeleted"] = "Note was deleted.";
            return RedirectToAction("Details", "Project", new {id = projectId});
        }
    }
}
