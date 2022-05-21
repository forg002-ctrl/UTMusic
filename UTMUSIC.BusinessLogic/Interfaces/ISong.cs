using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface ISong
    {
        List<SongDbTable> GetAllSongs();
        SongDbTable GetSongById(int id);
        List<SongDbTable> GetSongsByArtist(ArtistDbTable artist);
        Response AddSong(SongDbTable song);
        Response UpdateSong(SongDbTable song);
        Response DeleteSong(int id);
        List<SongDbTable> SearchSongsByLine(string searchLine);
    }
}