using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Repositories;

namespace UTMUSIC.BusinessLogic.Services
{
    public class AlbumBL : IAlbum
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IFile _fileService;


        public AlbumBL(IAlbumRepository albumRepository, IFile fileService)
        {
            _albumRepository = albumRepository;
            _fileService = fileService;
        }
        public void AddAlbum(AlbumDbTable album)
        {
            album.Artist.Albums.Add(album);
            _albumRepository.AddAlbum(album);
        }

        public void UpdateAlbum(AlbumDbTable album)
        {
            throw new NotImplementedException();
        }

        public void DeleteAlbum(int id)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDbTable> GetAllAlbums()
        {
            return _albumRepository.GetAllAlbums();
        }


        public AlbumDbTable GetAlbumById(int id)
        {
            return _albumRepository.GetAlbumById(id);
        }
        public List<AlbumDbTable> GetAlbumsByArtist(AlbumDbTable artist)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDbTable> SearchByLine(string searchLine)
        {
            return _albumRepository.SearchAlbums(searchLine);
        }

    }
}
