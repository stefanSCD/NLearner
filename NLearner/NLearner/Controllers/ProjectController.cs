using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLearner.DTO.Projects;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;
using System.Threading.Tasks;

namespace NLearner.Controllers
{
    [Authorize]
    [Route("projects")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsByUserIdAsync(CurrentUserId);
            return View(projects);
        }
        [HttpGet("new")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return View(projectDto);
            }
            await _projectService.CreateProjectAsync(projectDto, CurrentUserId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost("{id:guid}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var succes = await _projectService.DeleteProjectAsync(id, CurrentUserId);
            if (!succes)
                return NotFound();
            TempData["SuccessMessage"] = "Project deleted succesfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var project = await _projectService.GetProjectDetailsAsync(id, CurrentUserId);
            if(project == null)
                return NotFound();
            var viewModel = new ProjectDetailsViewModel
            {
                ProjectId = id,
                ProjectName = project.Name,
                Notes = project.Notes.OrderByDescending(n => n.UpdatedDate),
                Decks = project.Decks.OrderByDescending(d => d.UpdatedDate)
            };
            return View(viewModel);
        }
    }
}
