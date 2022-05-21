using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UTMUSIC.ActionFilters;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Web.Models;

namespace UTMUSIC.Web.Controllers
{
    public class SongController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFile _fileService;
        private readonly ISong _songService;
        private readonly IArtist _artistService;

        public SongController(IMapper mapper, IArtist artistService, ISong songService, IFile fileService)
        {
            _mapper = mapper;
            _songService = songService;
            _fileService = fileService;
            _artistService = artistService;
        }

        // GET /Song/Songs
        [HttpGet]
        public ActionResult Songs()
        {
            List<SongDbTable> songs = _songService.GetAllSongs();
            List<SongData> songsModel = new List<SongData>();

            foreach (var song in songs)
            {
                songsModel.Add(_mapper.Map<SongDbTable, SongData>(song));
            }

            return View(songsModel);
        }

        // POST /Song/Songs:searchLine
        [HttpPost]
        public ActionResult Songs(string searchLine)
        {
            if (string.IsNullOrWhiteSpace(searchLine))
            {
                return RedirectToAction("Songs");
            }

            HashSet<SongDbTable> uniqueSongs = new HashSet<SongDbTable>(_songService.SearchSongsByLine(searchLine));
            uniqueSongs.UnionWith(_artistService.SearchArtistsSongsByLine(searchLine));

            List<SongData> songsModels = new List<SongData>();

            foreach (var song in uniqueSongs)
            {
                songsModels.Add(_mapper.Map<SongDbTable, SongData>(song));
            }

            return View(songsModels);
        }

        // GET /Song/Create/:id
        [AdminMode]
        [HttpGet]
        public ActionResult Create(int id)
        {
            SongData songModel = new SongData();
            songModel.Artist = _artistService.GetArtistById(id);

            return View(songModel);
        }

        // POST /Song/Create/:id  
        [AdminMode]
        [HttpPost]
        public ActionResult Create(int id, SongData songModel, HttpPostedFileBase AudioPath)
        {
            songModel.Artist = _artistService.GetArtistById(id);
            SongDbTable newSong = _mapper.Map<SongData, SongDbTable>(songModel);
            
            newSong.AudioPath = _fileService.UploadAudio(AudioPath, songModel.Artist.Genre + "/" + songModel.Artist.Name + "/Songs/" + songModel.Name); ;

            _songService.AddSong(newSong);
            return RedirectToAction("Details","Artist", new { id = id });
        }
    }
}