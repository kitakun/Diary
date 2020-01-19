namespace Kitakun.TagDiary.Persistance
{
    using Autofac;

    public class PersistanceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DiaryDbContext>()
                .AsSelf()
                .As<IDiaryDbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
