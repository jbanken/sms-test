using System.Data.Entity;
namespace Data
{
    public partial class SMSDataModel : DbContext
    {
        public SMSDataModel()
            : base("name=TechboardDataModel")
        {
        }

        public virtual DbSet<Entity.MessageLog> MessageLogs { get; set; }
        public virtual DbSet<Entity.ThirdPartyService> ThirdPartyServices { get; set; }
        public virtual DbSet<Entity.APILog> APILogs { get; set; }

        public virtual DbSet<Entity.TwilioIncomingMessageCallback> TwilioIncomingMessageCallbacks { get; set; }
        public virtual DbSet<Entity.TwilioMessage> TwilioMessages { get; set; }
        public virtual DbSet<Entity.TwilioStatusCallback> TwilioStatusCallbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
