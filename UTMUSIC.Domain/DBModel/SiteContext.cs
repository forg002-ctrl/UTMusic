using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;

namespace UTMUSIC.Domain.DBModel
{
    public class SiteContext : DbContext
    {
        public SiteContext() : base("name=UTMusic") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SongDbTable> Songs { get; set; }
        public virtual DbSet<ArtistDbTable> Artists { get; set; }
        public virtual DbSet<AlbumDbTable> Albums { get; set; }
    }
}