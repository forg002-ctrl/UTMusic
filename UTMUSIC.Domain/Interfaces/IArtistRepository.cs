using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Domain.Repositories
{
    public interface IArtistRepository
    {
        List<ArtistDbTable> GetAllArtists();
        ArtistDbTable GetArtistById(int id);
        int AddArtist(ArtistDbTable artist);
        void UdpateArtist(ArtistDbTable artist);
        void DeleteArtist(int id);
        void AddSongToArtist(SongDbTable song);
        List<ArtistDbTable> SearchArtists(string searchLine);

    }
}
