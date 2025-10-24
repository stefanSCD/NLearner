using NLearner.Domain.Entities;
using NLearner.DTO.Projects;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);
        Task<Project?> GetOwnedProjectAsync(Guid projectId, string userId);
        Task<Project> CreateProjectAsync(CreateProjectDto projectDto, string userId);
        Task<Project?> UpdateProjectAsync(UpdateProjectDto projectDto, string userId);
        Task<bool> DeleteProjectAsync(Guid projectId, string userId);
        Task<ProjectDetailsViewModel?> GetProjectDetailsViewModelAsync(Guid projectId, string userId);
    }
}
