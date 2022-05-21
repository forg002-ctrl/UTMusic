using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Web.Models
{
    public class AlbumData
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Album's Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public virtual ArtistDbTable Artist { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose file to upload.")]
        [Display(Name = "Photo File")]
        public string PhotoPath { get; set; }

        [Required]
        [Display(Name = "List of Songs")]
        public virtual List<SongDbTable> Songs { get; set; }
        public List<int> SongsIds { get; set; }

        public SelectList SongsToChoose { get; set; }
    }
}