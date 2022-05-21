using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface IPlaylist
    {
        List<AlbumDbTable> GetAlbumsByUser(Domain.Entities.User.UserMinimal user);
    }
}
