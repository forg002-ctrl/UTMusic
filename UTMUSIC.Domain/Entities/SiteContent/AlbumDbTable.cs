using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTMUSIC.Domain.Entities.SiteContent
{
    public class AlbumDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual ArtistDbTable Artist { get; set; }
        
        [Required]
        public string PhotoPath { get; set; }

        public virtual List<SongDbTable> Songs { get; set; }
    }
}
