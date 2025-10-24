using NLearner.ViewModels;

namespace NLearner.Application.Abstractions
{
    public interface ISidebarService
    {
        Task<SidebarViewModel> GetSidebarDataAsync(string userId);
    }
}
