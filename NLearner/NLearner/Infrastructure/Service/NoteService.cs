using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;
using NLearner.DTO.Notes;
using NLearner.Infrastructure.Persistence;

namespace NLearner.Infrastructure.Service
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _context;
        public NoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNoteAsync(Guid noteId, string userId)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == noteId && userId == x.UserId);
            if (note is not null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Note>> GetNotesByUserId(string userId) =>
            await _context.Notes.Where(x => x.UserId == userId).OrderByDescending(x => x.UpdatedDate).ToListAsync();

        public async Task<Note?> UpdateNoteAsync(NoteSaveRequest note, string userId)
        {
            var noteFromDb = await _context.Notes.FirstOrDefaultAsync(x => x.Id == note.Id && x.UserId == userId);

            if (noteFromDb is null)
                return null;

            noteFromDb.Title = note.Title;
            noteFromDb.Content = note.Content;
            noteFromDb.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return noteFromDb;
        }
        public async Task<Guid> CreateDraftAsync(string userId, Guid projectId)
        {
            var note = new Note()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = string.Empty,
                Content = string.Empty,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                isDeleted = false,
                ProjectId = projectId
            };
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note.Id;
        }
        public async Task<Note?> GetOwnedAsync(Guid noteId, string userId) =>
            await _context.Notes.FirstOrDefaultAsync(x => x.Id == noteId && x.UserId == userId);

        public async Task<IEnumerable<Note>> GetNotesByProjectIdAsync(Guid projectId) =>
            await _context.Notes.Where(note => note.ProjectId == projectId).OrderByDescending(note => note.UpdatedDate).ToListAsync();
    }
}