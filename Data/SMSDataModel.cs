using System.Data.Entity;
namespace Data
{
    public partial class SMSDataModel : DbContext
    {
        public SMSDataModel()
            : base("name=SMSDataModel")
        {
        }

        public virtual DbSet<Entity.MessageLog> MessageLogs { get; set; }
        public virtual DbSet<Entity.ThirdPartyService> ThirdPartyServices { get; set; }
        public virtual DbSet<Entity.APILog> APILogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
