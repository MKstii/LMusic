﻿namespace LMusic.Models
{
    public class Music : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PictureId {  get; set; }
        public Picture Picture { get; set; }
        public string Title { get; set; }
        public string Musician { get; set; }
        public string Path { get; set; }
        public List<PlaylistMusic> Playlists { get; set; }
        public Privacy Privacy { get; set; }
        public bool IsDeleted { get; set; }

        public Music() { }

        public Music(int id, int userId, int pictureId, string title, 
            string musician, string path, Privacy privacy)
        {
            Id = id;
            UserId = userId;
            PictureId = pictureId;
            Title = title;
            Musician = musician;
            Path = path;
            Privacy = privacy;
            IsDeleted = false;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
