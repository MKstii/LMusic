namespace LMusic.Models
{
    public class Picture : IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public PictureType Type { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public Picture() { }

        public Picture(int id, string path, PictureType type, bool isDefault)
        {
            Id = id;
            Path = path;
            Type = type;
            IsDefault = isDefault;
            IsDeleted = false;
        }
        public int GetId()
        {
            return Id;
        }
    }
}
