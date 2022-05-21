using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTMUSIC.Domain.DBModel;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Repositories;


namespace UTMUSIC.Domain
{
    public class SongRepository : ISongRepository
    {
        private readonly SiteContext _context;

        public SongRepository(SiteContext context)
        {
            _context = context;
        }

        public List<SongDbTable> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        public SongDbTable GetSongById(int id)
        {
            return _context.Songs.FirstOrDefault(s => s.Id == id);
        }

        public List<SongDbTable> GetSongsByArtist(ArtistDbTable artist)
        {
            return _context.Songs.Where(s => s.Artist.Id == artist.Id).ToList();
        }

        public void AddSong(SongDbTable song)
        {
            _context.Songs.Add(song);
            _context.SaveChanges();
        }

        public void UdpateSong(SongDbTable song)
        {
            var entry = _context.Entry(song);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSong(int id)
        {
            var song = _context.Songs.Find(id);
            _context.Songs.Remove(song);
            _context.SaveChanges();
        }

        public List<SongDbTable> SearchSongs(string searchLine)
        {
            return _context.Songs.Where(s => s.Name.Contains(searchLine)).ToList(); ;
        }
    }
}
