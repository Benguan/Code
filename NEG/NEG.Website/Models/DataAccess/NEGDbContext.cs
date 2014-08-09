using System.Data.Entity;

namespace NEG.Website.Models.DataAccess
{
    public class NEGDbContext : DbContext
    {
        public NEGDbContext()
            : base("NEGWebsiteEntities")
        {

        }

        public DbSet<APICategory> APICategories { get; set; }
        public DbSet<APIDetailInfo> APIDetailInfos { get; set; }
        public DbSet<DemoDetailInfo> DemoDetailInfos { get; set; }

        public DbSet<ModuleCategory> ModuleCategories { get; set; }
        public DbSet<ModuleDetailInfo> ModuleDetailInfos { get; set; }
    }
}