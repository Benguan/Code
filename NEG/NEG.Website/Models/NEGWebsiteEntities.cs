using System.Data.Entity;

namespace NEG.Website.Models
{
    public class NEGWebsiteEntities : DbContext
    {
        public DbSet<APICategory> APICategories { get; set; }
        public DbSet<APIDetailInfo> APIDetailInfos { get; set; }
        public DbSet<DemoDetailInfo> DemoDetailInfos { get; set; }

    }
}