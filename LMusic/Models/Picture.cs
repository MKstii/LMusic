namespace LMusic.Models
{
    public class Picture : IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public PictureType Type { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public Picture() { }

        public Picture(int id, string path, string fileName, PictureType type, bool isDefault)
        {
            Id = id;
            Path = path;
            FileName = fileName;
            Type = type;
            IsDefault = isDefault;
            IsDeleted = false;
        }
        public int GetId()
        {
            return Id;
        }
        public string GetFullPath()
        {
            return Path + "/" + FileName;
        }
    }
}
