namespace NLearner.ViewModels
{
    public class SidebarViewModel
    {
        public List<SidebarNoteItem> Notes { get; set; } = new();
        public List<SidebarProjectItem> Projects { get; set; } = new();
        public List<SidebarDeckItem> Decks { get; set; } = new();
        public int TotalNotes { get; set; }
        public int TotalProjects { get; set; }
    }
}
