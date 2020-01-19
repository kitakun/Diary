namespace Kitakun.TagDiary.Services
{
    using Autofac;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Services.Infrastructure;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EncoderRC4>().As<IEncrypter>().SingleInstance();
            builder.RegisterType<MapperService>().As<IMapperService>().InstancePerLifetimeScope();

            builder.RegisterType<TagsService>().As<ITagsService>().InstancePerLifetimeScope();
            builder.RegisterType<SpaceOwnerService>().As<ISpaceOwnerService>().InstancePerLifetimeScope();
            builder.RegisterType<DiaryRecordService>().As<IDiaryRecordService>().InstancePerLifetimeScope();
        }
    }
}
