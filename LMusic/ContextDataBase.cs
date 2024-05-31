using LMusic.Additional;
using LMusic.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace LMusic
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase() { }
        private static bool isInit = false;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("LMusic");
            optionsBuilder.UseNpgsql("Host=mks-server.tplinkdns.com;Port=5432;Database=LMusic;Username=mksti;Password=mks");
            
            
            // Additional.DatabaseData будут объекты, которые надо будет засунуть в базу
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FriendRequest>()
                .HasOne(x => x.Requester)
                .WithMany(x => x.FriendRequestsListAsRequester)
                .HasForeignKey(x => x.RequesterId);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(x => x.Addressee)
                .WithMany(x => x.FriendRequestsListAsAddressee)
                .HasForeignKey(x => x.AddresseeId);

            modelBuilder.Entity<FriendsList>()
                .HasOne(x => x.User)
                .WithMany(x => x.FriendsList)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FriendsList>()
                .HasOne(x => x.Friend)
                .WithMany(x => x.FriendsListAsFriend)
                .HasForeignKey(x => x.FriendId);

            modelBuilder.Entity<User>()
                .Property(x => x.PictureId).HasDefaultValue(1);

            modelBuilder.Entity<Picture>()
                .Property(x => x.IsDeleted).HasDefaultValue(false);

            modelBuilder.Entity<Music>()
                .Property(x => x.IsDeleted).HasDefaultValue(false);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<FriendsList> FriendsLists { get; set; }
        public DbSet<PlaylistMusic> PlaylistMusics { get; set; }
        public DbSet<PlaylistUser> PlaylistUser { get; set; }
    }
}
