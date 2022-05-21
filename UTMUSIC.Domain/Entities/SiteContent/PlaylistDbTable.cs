using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.Domain.Entities.SiteContent
{
    public class PlaylistDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string PhotoPath { get; set; }

        [Required]
        public virtual UDbTable User { get; set; }

        [Required]
        public virtual List<SongDbTable> Songs { get; set; }
    }
}
