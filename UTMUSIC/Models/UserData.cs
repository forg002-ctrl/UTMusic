using System.Collections.Generic;
using UTMUSIC.Domain.Entities.SiteContent;

namespace UTMUSIC.Web.Models
{
    public class UserData
    {
        public string Username { get; set; }
        public List<PlaylistDbTable> Playlists { get; set; }
        //public List<SongDbTable>  { get; set; }
    }
}