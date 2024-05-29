using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels.User;
using Npgsql.PostgresTypes;
using System.Runtime.CompilerServices;

namespace LMusic.Services
{
    public class MusicService : DbServiceAbstract<Music>
    {
        private MusicRegistry _musicRegistry;
        private PlaylistMusicRegistry _playlistMusicRegistry;
        private PictureService _pictureService;
        private PlaylistService _playlistService;
        private FriendService _friendService;
        public MusicService() : base(new MusicRegistry())
        {
            _musicRegistry = (MusicRegistry)_registry;
            _pictureService = new PictureService();
            _playlistService = new PlaylistService();
            _playlistMusicRegistry = new PlaylistMusicRegistry();
            _friendService = new FriendService();
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
            var defaultUserPlaylist = _playlistService.GetDefaultUserPlaylist(user);
            return _playlistMusicRegistry.GetMusicByPlaylist(defaultUserPlaylist);
        }

        public MusicViewmodel GetViewModel(Music music, User requester)
        {
            var viewmodel = new MusicViewmodel();
            viewmodel.Title = music.Title;
            viewmodel.Musician = music.Musician;
            viewmodel.Id = music.Id;
            viewmodel.PhotoPath = _pictureService.GetMusicAvatar(music).GetFullPath();
            viewmodel.MusicPath = music.GetFullPath();
            viewmodel.CanEdit = music.UserId == requester.Id;
            return viewmodel;
        }

        public Music GetMusic(int musicId)
        {
            Music music = _musicRegistry.FindWithIncludeUser(musicId);
            return music;
        }

        public void AddMusicToUser(Music music, User user)
        {
            var playlist = _playlistService.GetDefaultUserPlaylist(user);
            AddMusicToPlaylist(music, playlist);
        }

        public bool UserHasMusic(Music music, User user)
        {
            var playlist = _playlistService.GetDefaultUserPlaylist(user);
            return PlaylistHasMusic(music, playlist);
        }

        public bool PlaylistHasMusic(Music music, Playlist playlist)
        {
            var result = _playlistMusicRegistry.GetByMusicAndPlyalist(playlist, music);
            return result != null;
        }

        public void AddMusicToPlaylist(Music music, Playlist playlist)
        {
            PlaylistMusic playlistMusic = new PlaylistMusic()
            {
                MusicId = music.Id,
                PlaylistId = playlist.Id,
            };
            _playlistMusicRegistry.Add(playlistMusic);
        }
        public void DeleteMusicFromPlaylist(Playlist playlist, Music music)
        {
            var obj = _playlistMusicRegistry.GetByMusicAndPlyalist(playlist, music);
            _playlistMusicRegistry.Delete(obj);
        }

        public void DeleteMusic(Music music)
        {
            _musicRegistry.Delete(music);
            
            //music.IsDeleted = true;
            //_musicRegistry.Update(music);
        }

        public void UpdateMusic(User user, Music music, string? title, string? musician, IFormFile? musicPicture, string webRootPath)
        {
            if (title != null) 
                music.Title = title;
            if (musician != null)
                music.Musician = musician;
            if (musicPicture != null)
                music.PictureId = _pictureService.CreatePicture(user, musicPicture, PictureType.Music, webRootPath).Id;
            _musicRegistry.Update(music);

        }
    }
}
