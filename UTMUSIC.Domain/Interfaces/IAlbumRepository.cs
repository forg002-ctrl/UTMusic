using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Domain.Repositories
{
    public interface IAlbumRepository
    {
        List<AlbumDbTable> GetAllAlbums();
        AlbumDbTable GetAlbumById(int id);
        List<AlbumDbTable> GetAlbumsByArtist(ArtistDbTable artist);
        void AddAlbum(AlbumDbTable album);
        void UdpateAlbum(AlbumDbTable album);
        void DeleteAlbum(int id);
        void AddSongToAlbum(SongDbTable song, AlbumDbTable album);
        List<AlbumDbTable> SearchAlbums(string searchLine);
    }
}