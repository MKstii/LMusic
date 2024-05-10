﻿using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels;

namespace LMusic.Services
{
    public class MusicService : DbServiceAbstract<Music>
    {
        private MusicRegistry _musicRegistry;
        private PlaylistMusicRegistry _playlistMusicRegistry;
        private PictureService _pictureService;
        private PlaylistService _playlistService;  
        public MusicService() : base(new MusicRegistry())
        {
            _musicRegistry = (MusicRegistry)_registry;
            _pictureService = new PictureService();
            _playlistService = new PlaylistService();
            _playlistMusicRegistry = new PlaylistMusicRegistry();
        }

        public string CreatePath(User user)
        {
            return $"/audio/{user.TelegramId}";
        }

        public Music? CreateMusic(User user, string title, string musician, IFormFile audioFile, IFormFile? pictureFile, string webRoorPath) 
        {
            var music = new Music();
            music.FileName = audioFile.FileName;
            music.IsDeleted = false;
            music.Path = CreatePath(user);
            music.UserId = user.Id;

            if (pictureFile != null)
                music.PictureId = _pictureService.CreatePicture(user, pictureFile, PictureType.Music, webRoorPath).Id;
            else
                music.PictureId = _pictureService.GetDefaulMusicPicture().Id;

            music.Musician = musician;
            music.Title = title;

            Directory.CreateDirectory(webRoorPath + music.Path);
            using (var fileStream = new FileStream(webRoorPath + music.GetFullPath(), FileMode.Create))
            {
                audioFile.CopyTo(fileStream);
            }
            Add(music);

            // перенести в свой метод
            var defaultPlaylist = _playlistService.GetDefaultUserPlaylist(user);
            var playlistmusic = new PlaylistMusic();
            playlistmusic.PlaylistId = defaultPlaylist.Id;
            playlistmusic.MusicId = music.Id;
            new PlaylistMusicRegistry().Add(playlistmusic);
            //

            return music;
        }

        public List<Music> GetFavoriteMusicByUser(User user, UserAccess access)
        {
            if (user.Privacy == Privacy.ForMe) return new List<Music>();
            if (user.Privacy == Privacy.ForFriends && access == UserAccess.User) return new List<Music>();
            var defaultUserPlaylist = _playlistService.GetDefaultUserPlaylist(user);
            return _playlistMusicRegistry.GetMusicByPlaylist(defaultUserPlaylist);
        }

        public MusicViewmodel GetViewModel(Music music)
        {
            var viewmodel = new MusicViewmodel();
            viewmodel.Title = music.Title;
            viewmodel.Musician = music.Musician;
            viewmodel.Id = music.Id;
            viewmodel.PhotoPath = _pictureService.GetMusicAvatar(music).GetFullPath();
            viewmodel.MusicPath = music.GetFullPath();
            return viewmodel;
        }
    }
}