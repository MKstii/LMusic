namespace LMusic.Models
{
    public class Picture : IEntity
    {
        public int Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Path { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
