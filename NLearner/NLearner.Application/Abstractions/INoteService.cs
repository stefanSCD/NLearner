using NLearner.Domain.Entities;
using NLearner.DTO.Notes;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Service
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetNotesByUserId(string userId);
        Task<IEnumerable<Note>> GetNotesByProjectIdAsync(Guid projectId);
        Task<bool> DeleteNoteAsync(Guid noteId, string userId);
        Task<Note?> UpdateNoteAsync(NoteSaveRequest note, string userId);
        Task<Guid> CreateDraftAsync(string userId, Guid projectId);
        Task<NoteEditViewModel?> GetOwnedNoteViewModelAsync(Guid noteId, string userId);
    }
}
