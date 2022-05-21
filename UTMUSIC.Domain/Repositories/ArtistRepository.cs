using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTMUSIC.Domain.DBModel;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Repositories;

namespace UTMUSIC.Domain
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly SiteContext _context;

        public ArtistRepository(SiteContext context)
        {
            _context = context;
        }

        public List<ArtistDbTable> GetAllArtists()
        {
            return _context.Artists.ToList();
        }

        public ArtistDbTable GetArtistById(int id)
        {
            return _context.Artists.FirstOrDefault(a => a.Id == id);
        }

        public int AddArtist(ArtistDbTable artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
            return artist.Id;
        }

        public void UdpateArtist(ArtistDbTable artist)
        {
            var entry = _context.Entry(artist);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteArtist(int id)
        {
            var artist = _context.Artists.Find(id);
            _context.Artists.Remove(artist);
            _context.SaveChanges();
        }

        public void AddSongToArtist(SongDbTable song)
        {
            song.Artist.Songs.Add(song);
            var entry = _context.Entry(song);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }


        public List<ArtistDbTable> SearchArtists(string searchLine)
        {
            return _context.Artists.Where(a => a.Name.Contains(searchLine)).ToList();
        }
    }
}
