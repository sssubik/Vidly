namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres(Name) Values('Comedy')");
            Sql("Insert into Genres(Name) Values('Romance')");
            Sql("Insert into Genres(Name) Values('Thriller')");
            Sql("Insert into Genres(Name) Values('Horror')");
        }
        
        public override void Down()
        {
        }
    }
}
