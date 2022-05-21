using System.ComponentModel.DataAnnotations;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Web.Models
{
    public class SongData
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Song's Name")]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose file to upload.")]
        [Display(Name = "Audio File")]
        public string AudioPath { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public virtual ArtistDbTable Artist { get; set; }

        public string ImagePath { get; set; }

    }
}