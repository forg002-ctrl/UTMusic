using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTMUSIC.Domain.DBModel;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Repositories;


namespace UTMUSIC.Domain
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly SiteContext _context;

        public AlbumRepository(SiteContext context)
        {
            _context = context;
        }

        public List<AlbumDbTable> GetAllAlbums()
        {
            return _context.Albums.ToList();
        }

        public AlbumDbTable GetAlbumById(int id)
        {
            return _context.Albums.FirstOrDefault(s => s.Id == id);
        }

        public List<AlbumDbTable> GetAlbumsByArtist(ArtistDbTable artist)
        {
            return _context.Albums.Where(s => s.Artist.Id == artist.Id).ToList();
        }

        public void AddAlbum(AlbumDbTable album)
        {
            foreach (var song in album.Songs)
            {
                var entry = _context.Entry(song);
                entry.State = EntityState.Modified;
            }

            _context.Albums.Add(album);
            _context.SaveChanges();
        }

        public void UdpateAlbum(AlbumDbTable album)
        {
            var entry = _context.Entry(album);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAlbum(int id)
        {
            var album = _context.Albums.Find(id);
            _context.Albums.Remove(album);
            _context.SaveChanges();
        }

        public void AddSongToAlbum(SongDbTable song, AlbumDbTable album)
        {
            album.Songs.Add(song);
            var entry = _context.Entry(song);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<AlbumDbTable> SearchAlbums(string searchLine)
        {
            return _context.Albums.Where(s => s.Name.Contains(searchLine)).ToList(); ;
        }
    }
}