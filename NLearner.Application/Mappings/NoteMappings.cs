using NLearner.Domain.Entities;
using NLearner.DTO.Notes;
using NLearner.ViewModels;

namespace NLearner.Mappings
{
    public static class NoteMappings
    {
        // Domain -> VM (pentru Edit view)
        public static NoteEditViewModel ToEditViewModel(this Note n) =>
            new NoteEditViewModel
            {
                Id = n.Id,
                Title = n.Title ?? string.Empty,
                Content = n.Content ?? string.Empty,
                CreatedDate = n.CreatedDate,
                UpdatedDate = n.UpdatedDate,
                IsDeleted = n.isDeleted
            };

        // Listă Domain -> VM (utilă pentru liste, dacă vrei VM și acolo)
        public static IEnumerable<NoteEditViewModel> ToEditViewModels(this IEnumerable<Note> notes) =>
            notes.Select(n => n.ToEditViewModel());

        // VM -> DTO pentru salvare (acceptăm doar câmpurile editabile)
        public static NoteSaveRequest ToSaveRequest(this NoteEditViewModel vm) =>
            new NoteSaveRequest
            {
                Id = vm.Id,
                Title = vm.Title,
                Content = vm.Content
            };
    }
}
