using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMUSIC.Domain.Entities;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface IArtist
    {
        int AddArtist(ArtistDbTable artist);
        Response DeleteArtist(int id);
        Response UpdateArtist(ArtistDbTable artist);
        List<ArtistDbTable> GetAllArtists();
        ArtistDbTable GetArtistById(int id);
        List<ArtistDbTable> SearchArtistsByLine(string searchLine);
        List<SongDbTable> SearchArtistsSongsByLine(string searchLine);
    }
}