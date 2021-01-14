namespace Kitakun.TagDiary.Persistance
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Core.Domain.Users;

    public sealed class DiaryDbContext : DbContext, IDiaryDbContext
    {
        private readonly IConfiguration _config;

        public DbSet<SpaceOwner> SpaceOwners { get; set; }

        public DbSet<DiaryRecord> DiaryRecords { get; set; }

        public DbSet<SpaceOwnerTags> SpaceOwnerTags { get; set; }

        public DbSet<DiaryUser> DiaryUsers { get; set; }

        public DbSet<DiaryUserExternalDataEntity> DiaryUserExternalDataEntities { get; set; }

#if !(MIGRATION)
        public DiaryDbContext(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
#endif

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options
#if MIGRATION
                .UseNpgsql("User ID=migrator;Password=migrator;Host=localhost;Port=5432;Database=diary;Pooling=true;");
#else
                .UseNpgsql(_config.GetValue<string>("DBInfo:ConnectionString"));
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public Task SaveChangesAsync() => base.SaveChangesAsync();
    }
}
