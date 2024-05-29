namespace LMusic.ViewModels.User
{
    public class MusicViewmodel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Musician { get; set; }
        public string PhotoPath { get; set; }
        public string MusicPath { get; set; }
        public bool CanEdit { get; set; }
    }
}
