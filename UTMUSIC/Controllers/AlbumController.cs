using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UTMUSIC.ActionFilters;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Constants;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Web.Models;

namespace UTMUSIC.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFile _fileService;
        private readonly IAlbum _albumService;
        private readonly IArtist _artistService;
        private readonly ISong _songService;

        public AlbumController(IMapper mapper, IArtist artistService, IAlbum albumService, ISong songService,
            IFile fileService)
        {
            _mapper = mapper;
            _albumService = albumService;
            _songService = songService;
            _fileService = fileService;
            _artistService = artistService;
        }

        // GET: Album
        [AdminMode]
        [HttpGet]
        public ActionResult Create(int id)
        {
            AlbumData albumModel = new AlbumData();
            albumModel.Artist = _artistService.GetArtistById(id);
            albumModel.Songs = _songService.GetSongsByArtist(albumModel.Artist);

            foreach (var song in albumModel.Songs)
            {
                for (int i = 0; i < albumModel.Artist.Albums.Count; ++i)
                {
                    if (albumModel.Artist.Albums[i].Songs.Contains(song))
                    {
                        albumModel.Songs.Remove(song);
                        break;
                    }
                }
            }

            albumModel.SongsToChoose = new SelectList(albumModel.Songs.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }),
                "Value", "Text");

            return View(albumModel);
        }

        [HttpPost]
        [AdminMode]
        public ActionResult Create(int id, AlbumData albumModel, HttpPostedFileBase PhotoPath)
        {
            albumModel.Artist = _artistService.GetArtistById(id);
            var album = _mapper.Map<AlbumData, AlbumDbTable>(albumModel);

            album.PhotoPath = _fileService.SaveImageOnDisk(PhotoPath,
                album.Artist.Genre + "/" + album.Artist.Name + "/Albums/" + album.Name + "/");

            foreach (var songId in albumModel.SongsIds)
            {
                album.Songs.Add(_songService.GetSongById(songId));
                album.Songs.Last().ImagePath = album.PhotoPath;
                string sourceFile = Constants.FullPath + album.Songs.Last().AudioPath.Replace("~/Content/DbData/", "");
                album.Songs.Last().AudioPath = album.Songs.Last().AudioPath.Replace("Songs", "Albums/" + album.Name);
                string destinationFile =
                    Constants.FullPath + album.Songs.Last().AudioPath.Replace("~/Content/DbData/", "");
                System.IO.File.Move(sourceFile, destinationFile);
            }

            _albumService.AddAlbum(album);
            return RedirectToAction("Artist", "Artist", new {id = albumModel.Id});
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            AlbumData album = _mapper.Map<AlbumDbTable, AlbumData>(_albumService.GetAlbumById(id));

            return View(album);
        }

        // GET /Album/Albums
        [HttpGet]
        public ActionResult Albums()
        {
            List<AlbumDbTable> albums = _albumService.GetAllAlbums();
            List<AlbumData> albumsModels = new List<AlbumData>();
            foreach (var album in albums)
            {
                albumsModels.Add(_mapper.Map<AlbumDbTable, AlbumData>(album));
            }

            return View(albumsModels);
        }

        // POST /Artist/Artists:searchLine
        [HttpPost]
        public ActionResult Albums(string searchLine)
        {
            if (string.IsNullOrWhiteSpace(searchLine))
            {
                return RedirectToAction("Albums");
            }

            List<AlbumDbTable> albums = _albumService.SearchByLine(searchLine);
            List<AlbumData> albumsModels = new List<AlbumData>();
            foreach (var album in albums)
            {
                albumsModels.Add(_mapper.Map<AlbumDbTable, AlbumData>(album));
            }

            return View(albumsModels);
        }

    }
}