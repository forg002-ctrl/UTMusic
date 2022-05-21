using System.Collections.Generic;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;
using UTMUSIC.Domain.Repositories;

namespace UTMUSIC.BusinessLogic.Services
{
    public class SongBL : ISong
    {
        private readonly ISongRepository _songRepository;
        private readonly IFile _fileService;
        private readonly IArtistRepository _artistRepository;

        public SongBL(ISongRepository songRepository, IFile fileService, IArtistRepository artistRepository)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _fileService = fileService;
        }

        public List<SongDbTable> GetAllSongs()
        {
            return _songRepository.GetAllSongs();
        }

        public SongDbTable GetSongById(int id)
        {
            return _songRepository.GetSongById(id);
        }

        public List<SongDbTable> GetSongsByArtist(ArtistDbTable artist)
        {
            return _songRepository.GetSongsByArtist(artist);
        }

        public Response AddSong(SongDbTable song)
        {
            //List<SDbTable> allSongsOfArtist = _songRepository.GetSongsByArtist(song.Artist);

            //foreach (var songOfArtist in allSongsOfArtist)
            //{
            //    if (songOfArtist.Name == song.Name)
            //    {
            //        return new Response { Status = false, StatusMsg = "The song with such name already exists." };
            //    }
            //}
            _songRepository.AddSong(song);
            _artistRepository.AddSongToArtist(song);

            return new Response() { Status = true };
        }

        public Response UpdateSong(SongDbTable song)
        {
            SongDbTable foundSong = _songRepository.GetSongById(song.Id);

            if (foundSong != null)
            {
                return new Response { Status = false, StatusMsg = "The song isn't found." };
            }

            _songRepository.UdpateSong(song);

            return new Response() { Status = true };
        }

        public Response DeleteSong(int id)
        {
            SongDbTable foundSong = _songRepository.GetSongById(id);
                
            if (foundSong == null)
            {
                return new Response() { Status = false, StatusMsg = "There isn't the song with such id" };
            }

            //_fileService.DeleteFileFromDisk();  TODO
            _songRepository.DeleteSong(id);

            return new Response() { Status = true };
        }

        public List<SongDbTable> SearchSongsByLine(string searchLine)
        {
            return _songRepository.SearchSongs(searchLine);
        }
    }
}
