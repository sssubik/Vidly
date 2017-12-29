namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNameFieldInMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("Update MembershipTypes Set Name = 'As per Pay' where Id = 1");
            Sql("Update MembershipTypes Set Name = 'Monthly' where Id = 2");
            Sql("Update MembershipTypes Set Name = 'Quarterly' where Id = 3");
            Sql("Update MembershipTypes Set Name = 'Yearly' where Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
