using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UTMUSIC.ActionFilters;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.BusinessLogic.Services;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Web.Models;


namespace UTMUSIC.Web.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArtist _artistService;
        private readonly IFile _fileService;

        public ArtistController(IMapper mapper, IArtist artistService, IFile fileService)
        {
            _artistService = artistService;
            _fileService = fileService;
            _mapper = mapper;

        }

        // GET /Artist/Artist:id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var artist = _artistService.GetArtistById(id);

            if (artist == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "bad ID param");
            }

            ArtistData artistModel = _mapper.Map<ArtistDbTable, ArtistData>(artist);

            return View(artistModel);
        }

        // GET /Artist/Artists
        [HttpGet]
        public ActionResult Artists()
        {
            List<ArtistDbTable> artists = _artistService.GetAllArtists();
            List<ArtistData> artistsModels = new List<ArtistData>();
            foreach (var artist in artists)
            {
                artistsModels.Add(_mapper.Map<ArtistDbTable, ArtistData>(artist));
            }

            return View(artistsModels);
        }

        // POST /Artist/Artists:searchLine
        [HttpPost]
        public ActionResult Artists(string searchLine)
        {
            if (string.IsNullOrWhiteSpace(searchLine))
            {
                return RedirectToAction("Artists");
            }

            List<ArtistDbTable> artists = _artistService.SearchArtistsByLine(searchLine);
            List<ArtistData> artistsModels = new List<ArtistData>();
            foreach (var artist in artists)
            {
                artistsModels.Add(_mapper.Map<ArtistDbTable, ArtistData>(artist));
            }

            return View(artistsModels);
        }

        // GET /Artist/Create 
        [AdminMode]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        // POST /Artist/Create  
        [AdminMode]
        [HttpPost]
        public ActionResult Create(ArtistData artistModel, HttpPostedFileBase PhotoPath)
        {
            ArtistDbTable newArtist = _mapper.Map<ArtistData, ArtistDbTable> (artistModel);
            newArtist.PhotoPath = _fileService.UploadImage(PhotoPath, newArtist.Genre + "/" + newArtist.Name + "/");;

            int newArtistId = _artistService.AddArtist(newArtist);
            if (newArtistId != 0)
            {
                return RedirectToAction("Details", new {id = newArtistId});
            }

            ModelState.AddModelError("", "Artist with such name has been already added.");
            return View(artistModel);
        }

        // GET /Artist/Update:id
        [HttpGet]
        [AdminMode]
        public ActionResult Update(int id)          //TODO
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "bad ID param");
            }

            ArtistData artistModel = _mapper.Map<ArtistDbTable, ArtistData>(artist);

            return View(artistModel);
        }

        // POST /Artist/Update:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminMode]
        public ActionResult Update(int id, ArtistData artistModel)      //TODO
        {
            ArtistDbTable artist = _mapper.Map<ArtistData, ArtistDbTable>(artistModel);

            var isArtistUpdated = _artistService.UpdateArtist(artist);
            if (isArtistUpdated.Status)
            {
                return RedirectToAction("Details", new { id = artist.Id });
            }

            ModelState.AddModelError("", isArtistUpdated.StatusMsg);
            return View();
        }


        // GET /Artist/Delete:id
        [HttpGet]
        [AdminMode]
        public ActionResult Delete(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }

            ArtistData artistModel = _mapper.Map<ArtistDbTable, ArtistData>(artist);

            return View(artistModel);
        }

        // POST /Artist/Delete:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminMode]
        public ActionResult DeleteConfirmed(int id)
        {
            _artistService.DeleteArtist(id);

            return RedirectToAction("Artists");
        }
    }
}