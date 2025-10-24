using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLearner.DTO.Decks;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;
using System.Runtime.InteropServices;

namespace NLearner.Controllers
{
    [Authorize]
    [Route("projects/{projectId:guid}/decks")]
    public class DeckController : BaseController
    {
        private readonly IDeckService _deckService;
        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index(Guid projectId)
        {
            var decks = await _deckService.GetDecksByProjectIdAsync(projectId, CurrentUserId);
            ViewData["ProjectId"] = projectId;
            return View(decks);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Deck(Guid projectId, Guid id) 
        {
            var deck = await _deckService.GetDeckWithCardsViewModelAsync(id, CurrentUserId);

            if (deck is null || deck.ProjectId != projectId) {
                return NotFound();
            }
            
            return View(deck);
        }
        [HttpGet("new")]
        public IActionResult New(Guid projectId)
        {
            var model = new CreateDeckDto() { Name = "untitled" , ProjectId = projectId};
            return View(model);
        }
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Guid projectId, CreateDeckDto dto)
        {
            if (!ModelState.IsValid) {
                return View(dto);
            }
            var newDeck = await _deckService.CreateDeckAsync(dto, projectId, CurrentUserId);
            if (newDeck is null)
                return Forbid();
            return RedirectToAction(nameof(Deck), new {projectId, id=newDeck.Id});
        }
        [HttpPost("{id:guid}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid projectId, Guid id)
        {
            bool ok = await _deckService.DeleteDeckAsync(id, CurrentUserId);
            if (!ok)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Deck deleted successfully.";
            return RedirectToAction("Details", "Project", new { id = projectId });
        }
        [HttpGet("{id:guid}/study")]
        public async Task<IActionResult> Study(Guid projectId, Guid id)
        {
            var vm = await _deckService.GetDeckWithCardsViewModelAsync(id, CurrentUserId);
            if(vm is null || vm.ProjectId != projectId)
                return NotFound();
            return View("Study", vm);
        }
    }
}
