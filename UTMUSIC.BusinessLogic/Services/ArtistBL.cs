using System.Collections.Generic;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;
using UTMUSIC.Domain.Repositories;

namespace UTMUSIC.BusinessLogic.Services
{
    public class ArtistBL : IArtist
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IFile _fileService;

        public ArtistBL(IArtistRepository artistRepository, IFile fileService)
        {
            _artistRepository = artistRepository;
            _fileService = fileService;
        }

        public List<ArtistDbTable> GetAllArtists()
        {
            return _artistRepository.GetAllArtists();
        }

        public ArtistDbTable GetArtistById(int id)
        {
            ArtistDbTable foundArtist = _artistRepository.GetArtistById(id);

            return foundArtist;
        }

        public int AddArtist(ArtistDbTable artist)
        {
            List<ArtistDbTable> allArtist = _artistRepository.GetAllArtists();

            foreach (var Artist in allArtist)
            {
                if (Artist.Name == artist.Name)
                {
                    return 0;
                }
            }

            return _artistRepository.AddArtist(artist);
        }

        public Response UpdateArtist(ArtistDbTable artist)
        {
            ArtistDbTable foundArtist = _artistRepository.GetArtistById(artist.Id);

            if (foundArtist != null)
            {
                return new Response { Status = false, StatusMsg = "The artist isn't found." };
            }

            _artistRepository.UdpateArtist(artist);

            return new Response() { Status = true };
        }

        public Response DeleteArtist(int id)
        {

            ArtistDbTable foundArtist = _artistRepository.GetArtistById(id);

            if (foundArtist == null)
            {
                return new Response() { Status = false, StatusMsg = "There isn't the artist with such id" };
            }

            _fileService.DeleteDirFromDisk(foundArtist.PhotoPath);
            _artistRepository.DeleteArtist(id);

            return new Response() { Status = true };
        }

        public List<ArtistDbTable> SearchArtistsByLine(string searchLine)
        {
            return _artistRepository.SearchArtists(searchLine);
        }

        public List<SongDbTable> SearchArtistsSongsByLine(string searchLine)
        {
            List<ArtistDbTable> artists = _artistRepository.SearchArtists(searchLine);

            List<SongDbTable> songsToReturn = new List<SongDbTable>();
            foreach (var artist in artists)
            {
                songsToReturn.AddRange(artist.Songs);
            }

            return songsToReturn;
        }

    }
}
