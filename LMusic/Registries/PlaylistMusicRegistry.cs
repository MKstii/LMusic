using LMusic.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LMusic.Registries
{
    public class PlaylistMusicRegistry : Registry<PlaylistMusic>
    {
        public List<Music> GetMusicByPlaylist(Playlist defPlaylist)
        {
            using (var db = new ContextDataBase())
            {
                var musics = db.PlaylistMusics.Where(x => x.PlaylistId == defPlaylist.Id).Include(x => x.Music).Select(x => x.Music);
                return musics.ToList();
            }
        }

        public PlaylistMusic GetByMusicAndPlyalist(Playlist playlist, Music music) 
        {
            using (var db = new ContextDataBase())
            {
                var result = db.PlaylistMusics.Where(x => x.PlaylistId == playlist.Id && x.MusicId == music.Id);
                return result.FirstOrDefault();
            }
        }
    }
}
