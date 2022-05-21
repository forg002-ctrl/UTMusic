using AutoMapper;
using UTMUSIC.Domain.Entities.SiteContent;
using UTMUSIC.Domain.Entities.User;
using UTMUSIC.Web.Models;

namespace UTMUSIC.Web.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserLogin, ULoginData>();

            CreateMap<ArtistData, ArtistDbTable>();

            CreateMap<ArtistDbTable, ArtistData>();
            
            CreateMap<SongDbTable, SongData>();
            
            CreateMap<SongData, SongDbTable > ();

            CreateMap<AlbumData, AlbumDbTable>();

            CreateMap<AlbumDbTable, AlbumData>();
        }
    }
}