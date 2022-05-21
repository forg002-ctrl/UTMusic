using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UTMUSIC.Domain.Enums;

namespace UTMUSIC.Domain.Entities.SiteContent
{
    public class ArtistDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        [Required]
        public string ArtistInfo { get; set; }

        [Required]
        public virtual AGenre Genre { get; set; }
        
        public virtual List<SongDbTable> PopularSongs { get; set; } //For artist page
        
        public virtual List<AlbumDbTable> PopularAlbums { get; set; } //For artist page
        
        public virtual List<AlbumDbTable> Albums { get; set; }
        
        public virtual List<SongDbTable> Songs { get; set; }
    }
}