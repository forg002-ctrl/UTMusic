using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Domain.Repositories
{
    public interface ISongRepository
    {
        List<SongDbTable> GetAllSongs();
        SongDbTable GetSongById(int id);
        List<SongDbTable> GetSongsByArtist(ArtistDbTable artist);
        void AddSong(SongDbTable song);
        void UdpateSong(SongDbTable song);
        void DeleteSong(int id);
        List<SongDbTable> SearchSongs(string searchLine);
    }
}