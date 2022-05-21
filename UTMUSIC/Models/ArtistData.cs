using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UTMUSIC.Domain.Entities;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Enums;


namespace UTMUSIC.Web.Models
{
    public class ArtistData
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose file to upload.")]
        [Display(Name = "Artist's Photo")]
        public string PhotoPath { get; set; }
        
        [Required]
        [Display(Name = "Artist's Info")]
        public string ArtistInfo { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public AGenre Genre { get; set; }
        
        public virtual List<SongDbTable> PopularSongs { get; set; } //For artist page
        public virtual List<AlbumDbTable> PopularAlbums { get; set; } //For artist page
        public virtual List<AlbumDbTable> Albums { get; set; }
        public virtual List<SongDbTable> Songs { get; set; }
    }
}