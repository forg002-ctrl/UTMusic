using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMUSIC.Domain.Entities.SiteContent
{
    public class SongDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AudioPath { get; set; }
        
        public string ImagePath { get; set; }

        [Required]
        public virtual ArtistDbTable Artist { get; set; }
    }
}
