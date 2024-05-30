namespace LMusic.ViewModels.User
{
    public class PlaylistViewmodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public bool IsDefault { get; set; }
        public bool CanEdit { get; set; }
        public bool IsAdded { get; set; }
    }
}
