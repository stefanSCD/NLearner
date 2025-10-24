using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLearner.DTO.Cards;
using NLearner.Infrastructure.Service;

namespace NLearner.Controllers
{
    [Authorize]
    [Route("projects/{projectId:guid}/decks/{deckId:guid}/cards")]
    public class CardController : BaseController
    {
        private readonly IDeckService _deckService;
        public CardController(IDeckService deckService)
        {
            _deckService = deckService;
        }
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Guid projectId, Guid deckId, CreateCardDto dto)
        {
            if (!ModelState.IsValid) {
                TempData["ErrorMessage"] = "Card fields are required.";
                return RedirectToAction("Deck", "Deck", new { projectId, id = deckId });
            }
            var newCard = await _deckService.AddCardToDeckAsync(dto, deckId, CurrentUserId);
            if (newCard is null)
                return Forbid();
            return RedirectToAction("Deck", "Deck", new { projectId, id = deckId });
        }
        [HttpPost("{id:guid}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid projectId, Guid deckId, Guid id)
        {
            var success = await _deckService.DeleteCardAsync(id, CurrentUserId);
            if (!success)
                return NotFound();
            return RedirectToAction("Deck","Deck",new {projectId, id = deckId});
        }
        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit(Guid projectId, Guid deckId, Guid id)
        {
            var model = await _deckService.GetCardForUpdateAsync(id, CurrentUserId);
            if (model is null || deckId != model.DeckId)
                return NotFound();
            
            ViewData["ProjectId"] = projectId;
            return View(model);
        }
        [HttpPost("{id:guid}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid projectId, Guid deckId,Guid id, UpdateCardDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ViewData["ProjectId"] = projectId;
                return View(dto);
            }
            if (dto.DeckId != deckId)
                return BadRequest();
            var updatedCard = await _deckService.UpdateCardAsync(dto, CurrentUserId);
            if(updatedCard is null) return NotFound();
            return RedirectToAction("Deck", "Deck", new { projectId, id = deckId });
        }
    }
}
