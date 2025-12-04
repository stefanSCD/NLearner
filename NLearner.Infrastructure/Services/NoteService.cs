using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;
using NLearner.DTO.Notes;
using NLearner.Infrastructure.Persistence;
using NLearner.ViewModels;

namespace NLearner.Infrastructure.Service
{
    public class NoteService : INoteService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public NoteService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> DeleteNoteAsync(Guid noteId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == noteId && userId == x.UserId);
            if (note is not null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Note>> GetNotesByUserId(string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var notes = await _context.Notes.Where(x => x.UserId == userId).OrderByDescending(x => x.UpdatedDate).ToListAsync();
            return notes;
        }

        public async Task<Note?> UpdateNoteAsync(NoteSaveRequest note, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
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
            await using var _context = await _contextFactory.CreateDbContextAsync();
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
        public async Task<NoteEditViewModel?> GetOwnedNoteViewModelAsync(Guid noteId, string userId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == noteId && x.UserId == userId);
            if (note == null) return null;
            return new NoteEditViewModel
            {
                Id = note.Id,
                ProjectId = note.ProjectId,
                Title = note.Title ?? string.Empty,
                Content = note.Content ?? string.Empty
            };
        }

        public async Task<IEnumerable<Note>> GetNotesByProjectIdAsync(Guid projectId)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Notes.Where(note => note.ProjectId == projectId).OrderByDescending(note => note.UpdatedDate).ToListAsync();
        }
    }
}