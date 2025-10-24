using Microsoft.AspNetCore.Mvc;
using NLearner.Application.Abstractions;
using NLearner.ViewModels;
using System.Security.Claims;

namespace NLearner.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ISidebarService _service;
        public SidebarViewComponent(ISidebarService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return View("Default", new SidebarViewModel());
            }

            var viewModel = await _service.GetSidebarDataAsync(userId);

            return View("Default", viewModel);
        }
    }
}
