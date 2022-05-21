using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface IAlbum
    {
        void AddAlbum(AlbumDbTable album); //artist - who sings song
        void UpdateAlbum(AlbumDbTable album); //artist - who sings song
        void DeleteAlbum(int id); //artist - who sings song
        List<AlbumDbTable> GetAllAlbums();
        AlbumDbTable GetAlbumById(int id);
        List<AlbumDbTable> GetAlbumsByArtist(AlbumDbTable artist);
        List<AlbumDbTable> SearchByLine(string searchLine);
    }
}
