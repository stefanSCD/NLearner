namespace NLearner.ViewModels
{
    public class SidebarViewModel
    {
        public string UserDisplayName { get; set; } = "User";
        public List<SidebarNoteItem> Notes { get; set; } = new();
        public List<SidebarProjectItem> Projects { get; set; } = new();
        public List<SidebarDeckItem> Decks { get; set; } = new();
        public int TotalNotes { get; set; }
        public int TotalProjects { get; set; }
        public string? SelectedNoteId { get; set; }
        public string? SelectedProjectId { get; set; }
    }
}
