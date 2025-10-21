using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;
using NLearner.DTO.Projects;
using NLearner.Infrastructure.Persistence;

namespace NLearner.Infrastructure.Service
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Project> CreateProjectAsync(CreateProjectDto projectDto, string userId)
        {
            var newProject = new Project() { 
            Name = projectDto.Name,
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            UserId = userId
            };
            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId, string userId)
        {
            var project = await GetOwnedProjectInternalAsync(projectId, userId);
            if (project == null)
            {
                return false;
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Project?> GetOwnedProjectAsync(Guid projectId, string userId) =>
            await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId && x.UserId == userId);

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId) =>
            await _context.Projects.Where(x => x.UserId == userId).ToListAsync();

        public async Task<Project?> GetProjectDetailsAsync(Guid projectId, string userId) =>
            await _context.Projects.Include(p => p.Notes).Include(p => p.Decks).FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

        public async Task<Project?> UpdateProjectAsync(UpdateProjectDto projectDto, string userId)
        {
            var project = await GetOwnedProjectAsync(projectDto.Id, userId);
            if (project is not null)
            {
                project.Name = projectDto.Name;
                project.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return project;
        }
        private async Task<Project?> GetOwnedProjectInternalAsync(Guid projectId, string userId) =>
            await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId && x.UserId == userId);
    }
}
