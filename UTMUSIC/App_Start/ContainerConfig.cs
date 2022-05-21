using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using UTMUSIC;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.BusinessLogic.Services;
using UTMUSIC.Domain;
using UTMUSIC.Domain.DBModel;
using UTMUSIC.Domain.Repositories;
using UTMUSIC.Web;
using UTMUSIC.Web.Infrastructure;

namespace UTMUSIC
{
    public class ContainerConfig
    {
        public static void RegisterConfig(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterApiControllers(typeof(Global).Assembly);

            // User TODO
            //builder.RegisterType<SqlEpisodeRepository>().As<IEpisodeRepository>()
            //    .InstancePerRequest();

            //builder.RegisterType<EpisodeServiceImpl>().As<IEpisodeService>()
            //    .InstancePerRequest();

            // Artist
            builder.RegisterType<ArtistRepository>().As<IArtistRepository>()
                .InstancePerRequest();

            builder.RegisterType<ArtistBL>().As<IArtist>()
                .InstancePerRequest();

            // Song
            builder.RegisterType<SongRepository>().As<ISongRepository>()
                .InstancePerRequest();

            builder.RegisterType<SongBL>().As<ISong>()
                .InstancePerRequest();

            // Album
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>()
                .InstancePerRequest();

            builder.RegisterType<AlbumBL>().As<IAlbum>()
                .InstancePerRequest();

            // File
            builder.RegisterType<FileBL>().As<IFile>()
                .InstancePerRequest();

            // CONTEXT
            builder.RegisterType<SiteContext>().InstancePerRequest();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            //Register AutoMapper here using AutoFacModule class (Both methods works)
            //builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule<AutoFacModule>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    };
}